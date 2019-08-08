using System;
using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
using Serilog;
using SharedModel.Model;

namespace OrderProcessing
{
    public class OrderProcessor:IOrderProcessor
    {
        private readonly IWorkflowStepServiceLocator _workflowStepServiceLocator;
        

        public OrderProcessor(IWorkflowStepServiceLocator workflowStepServiceLocator)
        {
            _workflowStepServiceLocator = workflowStepServiceLocator;
            
        }

        public async Task<Result<string>> ProcessAsync(Order order)
        {

            try
            {
                var orderWorkflow = new OrderWorkflow();

                var orderContext = new OrderContext(order);

                var orderworkflowState = orderWorkflow.Next();
                while (orderworkflowState != State.Invalid && orderworkflowState != State.Completed)
                {
                    var workFlowStep = _workflowStepServiceLocator.GetNextStep(orderworkflowState);
                    var result = await workFlowStep.Run(orderContext);

                    if (result.IsFailure)
                    {
                        orderWorkflow.Abort();
                        return result;
                    }

                    orderworkflowState = orderWorkflow.Next();
                }

                return Result.Ok("Order completed");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                
                return Result.Fail<string>(ex.Message);
            }
        }
    }

    public class OrderContext
    {
        public Order Order { get; }
        public Customer Customer { get; set; }

        public OrderContext(Order order)
        {
            Order = order;
        }
    }
}

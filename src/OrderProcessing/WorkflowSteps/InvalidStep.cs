using System.Threading.Tasks;
using SharedModel.Model;

namespace OrderProcessing.WorkflowSteps
{
    public class InvalidStep: IWorkflowStep
    {
        public Task<Result<string>> Run(OrderContext context)
        {
            //Roll back any changes

            return Task.FromResult(Result.Ok("Order Aborted"));
        }
    }
}

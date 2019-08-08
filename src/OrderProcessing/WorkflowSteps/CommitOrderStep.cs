using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
using SharedModel.Repositories;

namespace OrderProcessing.WorkflowSteps
{
    public class CommitOrderStep:IWorkflowStep
    {
        private readonly ICustomerRepository _customerRepository;

        public CommitOrderStep(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<Result<string>> Run(OrderContext context)
        {
            _customerRepository.SaveCustomer(context.Customer);

            //Save to order repo
            //Update Inventory
            
            return Task.FromResult(Result.Ok("Order saved"));
        }
    }
}

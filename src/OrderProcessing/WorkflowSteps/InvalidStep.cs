using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
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

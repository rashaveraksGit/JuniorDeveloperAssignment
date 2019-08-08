using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Raptor.Core.Utilities;
using Raptor.Core.Utilities.Domain;
using SharedModel.Model;
using SharedModel.Services;

namespace OrderProcessing.WorkflowSteps
{
    public class SendConfirmationEmailStep:IWorkflowStep
    {
        private readonly IEmailSender _emailSender;

        public SendConfirmationEmailStep(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task<Result<string>> Run(OrderContext context)
        {
            var orderString = JsonHelper.Serialize(context.Order);
            _emailSender.Send(orderString);
            return Task.FromResult(Result.Ok("Email sent"));
        }
    }
}

using System.Threading.Tasks;
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
            
            _emailSender.Send("Order content here");
            return Task.FromResult(Result.Ok("Email sent"));
        }
    }
}

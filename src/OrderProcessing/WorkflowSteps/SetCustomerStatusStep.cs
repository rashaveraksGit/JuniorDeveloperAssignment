using System.Linq;
using System.Threading.Tasks;
using OrderProcessing.Helpers;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessing.WorkflowSteps
{
    public class SetCustomerStatusStep:IWorkflowStep
    {
        private readonly ICustomerStatusLevelRepository _customerStatusLevelRepository;
        private readonly ICustomerStatusSettings _customerStatusSettings;

        public SetCustomerStatusStep(ICustomerStatusLevelRepository customerStatusLevelRepository, ICustomerStatusSettings customerStatusSettings)
        {
            _customerStatusLevelRepository = customerStatusLevelRepository;
            _customerStatusSettings = customerStatusSettings;
        }
        public Task<Result<string>> Run(OrderContext context)
        {
            var customerStatusRules = _customerStatusLevelRepository.GetCustomerStatuses();

            if (context.Customer.Status.Level == customerStatusRules.Max(c => c.Level))
                return Task.FromResult(Result.Ok("Status resolved"));

            var nextStatusLevel = _customerStatusLevelRepository.GetNextLevel(context.Customer.Status.Level);

            if (nextStatusLevel == null)
                return Task.FromResult(Result.Fail<string>("Next statuslevel missing"));
            
            if(context.Customer.IsQualifiedForNextLevel(nextStatusLevel,_customerStatusSettings.MinimumQuarantineIntervalInDays))
            {
                context.Customer.SetStatus(new Status(nextStatusLevel.Name, nextStatusLevel.Level));
            }

            

            return Task.FromResult(Result.Ok("Status resolved"));
        }

      
    }

    public interface ICustomerStatusSettings
    {
        int MinimumQuarantineIntervalInDays { get; set; }
    }
}

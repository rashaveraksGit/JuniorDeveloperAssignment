using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
using SharedModel.Repositories;

namespace OrderProcessing.WorkflowSteps
{
    public class SetDiscountStep:IWorkflowStep
    {
        private readonly ICustomerStatusLevelRepository _customerStatusLevelRepository;

        public SetDiscountStep(ICustomerStatusLevelRepository customerStatusLevelRepository)
        {
            _customerStatusLevelRepository = customerStatusLevelRepository;
        }
        public Task<Result<string>> Run(OrderContext context)
        {

            var statusRule = _customerStatusLevelRepository.GetLevel(context.Customer.Status.Level);
            context.Order.DiscountPercentage = statusRule.Discount;
            context.Order.DíscountLevel = statusRule.Name;

            return Task.FromResult(Result.Ok("Calculated discount"));
        }
    }
}

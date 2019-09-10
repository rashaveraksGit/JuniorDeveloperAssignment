using System.Threading.Tasks;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessing.WorkflowSteps
{
    public class GetCustomerStep : IWorkflowStep
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerStatusLevelRepository _customerStatusLevelRepository;

        public GetCustomerStep(ICustomerRepository customerRepository, ICustomerStatusLevelRepository customerStatusLevelRepository)
        {
            _customerRepository = customerRepository;
            _customerStatusLevelRepository = customerStatusLevelRepository;
        }
        public Task<Result<string>> Run(OrderContext context)
        {
            var maybeCustomer = _customerRepository.TryGetCustomerFromEmail(context.Order.EmailAddress);

            if (maybeCustomer ==null)
            {
                var startLevel = _customerStatusLevelRepository.GetStartLevel();
                context.Customer = new Customer(context.Order.EmailAddress, context.Order.FullName, new Status(startLevel.Name, startLevel.Level));
               
            }
            else
                context.Customer = maybeCustomer;


            //Add the current order to the history
            context.Customer.OrderHistory.Add(context.Order);

            return Task.FromResult(Result.Ok("Got Customer"));
        }
    }
}

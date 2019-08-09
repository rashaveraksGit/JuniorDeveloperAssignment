using SharedModel.Model;

namespace SharedModel.Repositories
{
    public interface ICustomerRepository
    {
        Customer TryGetCustomerFromEmail(string emailAddress);
        void SaveCustomer(Customer customer);

    }
}
using System.Collections.Generic;
using SharedModel.Model;

namespace SharedModel.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        //Emulates DB in mem
        private static Dictionary<string, Customer> _customers = new Dictionary<string, Customer>();
        public Customer TryGetCustomerFromEmail(string emailAddress)
        {
            if (!_customers.ContainsKey(emailAddress.ToLowerInvariant()))
                return null;
            return _customers[emailAddress.ToLowerInvariant()];

        }

        public void SaveCustomer(Customer customer)
        {
            if (!_customers.ContainsKey(customer.Email.ToLowerInvariant()))
                _customers.Add(customer.Email.ToLowerInvariant(), customer);
            else
                _customers[customer.Email.ToLowerInvariant()] = customer;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedModel.Model;

namespace SharedModel.Repositories
{
   
    public class CustomerStatusLevelRepository:ICustomerStatusLevelRepository
    {

        public CustomerStatusLevel GetStartLevel()
        {
            return GetCustomerStatuses().First();
        }

        public CustomerStatusLevel GetNextLevel(int level)
        {

            return GetCustomerStatuses().FirstOrDefault(c => c.Level == level + 1);
        }

        public CustomerStatusLevel GetLevel(int statusLevel)
        {
            return GetCustomerStatuses().FirstOrDefault(c => c.Level == statusLevel);
        }


        //Datalayer, emulates data coming from a DB
        public List<CustomerStatusLevel> GetCustomerStatuses()
        {
            return new List<CustomerStatusLevel>()
            {
                new CustomerStatusLevel()
                {
                    Name = "Regular",
                    Level = 0,
                    MinimumAmountCriteria = 0,
                    NumberOfDaysCriteria = 0,
                    NumberOfOrdersCriteria = 0,
                    Discount = 0
                },
                new CustomerStatusLevel()
                {
                    Name = "Silver",
                    Level = 1,
                    MinimumAmountCriteria = 300,
                    NumberOfDaysCriteria = 30,
                    NumberOfOrdersCriteria = 2,
                    Discount = 10
                    
                },
                new CustomerStatusLevel()
                {
                    Name = "Gold",
                    Level = 2,
                    MinimumAmountCriteria = 600,
                    NumberOfDaysCriteria = 30,
                    NumberOfOrdersCriteria = 1,
                    Discount = 15
                },
                new CustomerStatusLevel()
                {
                    Name = "Platinum",
                    Level = 3,
                    MinimumAmountCriteria = 1000,
                    NumberOfDaysCriteria = 30,
                    NumberOfOrdersCriteria = 1,
                    Discount = 20
                },
            };
        }
    }
}

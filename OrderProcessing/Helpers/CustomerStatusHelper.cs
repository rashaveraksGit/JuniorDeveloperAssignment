using System;
using System.Linq;
using SharedModel.Model;

namespace OrderProcessing.Helpers
{
    public static class CustomerStatusHelper
    {
        public static bool IsQualifiedForNextLevel(this Customer customer, CustomerStatusLevel nextStatusLevel, int minimumQuarantineIntervalInDays)
        {
            if (customer.OrderHistory.Count == 0)
                return false;

            if ((DateTime.UtcNow - customer.Status.Date).TotalDays < minimumQuarantineIntervalInDays)
                return false;

           

            var criteriaDate = DateTime.UtcNow.AddDays(-nextStatusLevel.NumberOfDaysCriteria);

            var ordersWithinDateCriteria = customer.OrderHistory.Where(o => o.OrderDate >= criteriaDate).ToList();

            
            if (!ordersWithinDateCriteria.Any())
                return false;


            if (ordersWithinDateCriteria.Count < nextStatusLevel.NumberOfOrdersCriteria)
                return false;

            if (ordersWithinDateCriteria.SelectMany(o => o.Basket).Sum(b => b.Price) < nextStatusLevel.MinimumAmountCriteria)
                return false;

            return true;
        }
    }
}

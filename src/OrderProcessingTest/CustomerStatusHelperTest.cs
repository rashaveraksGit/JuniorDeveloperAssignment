using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OrderProcessing.Helpers;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessingTest
{
    public class CustomerStatusHelperTest
    {
        [Test]
        public void WhenRegularCustomerHasNoPriorHistoryThenNotQualified()
        {

            var customer = GetCustomer("Regular",
                0,
                new List<Order>() {GetOrder(DateTime.UtcNow, 299)});

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified=customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsFalse(isQualified);
        }

        [Test]
        public void WhenRegularCustomerHasTwoOrdersWithin30DaysWithAValueMoreThan300ThenHeIsQualified()
        {
            var customer = GetCustomer("Regular",
                0,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 299),
                    GetOrder(DateTime.UtcNow.AddDays(-10),100)
                });

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsTrue(isQualified);
        }

        [Test]
        public void WhenRegularCustomerHasTwoOrdersWithin30DaysWithAValueLessThan300ThenHeIsNotQualified()
        {
            var customer = GetCustomer("Regular",
                0,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 30),
                    GetOrder(DateTime.UtcNow.AddDays(-10),100)
                });

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsFalse(isQualified);
        }

        [Test]
        public void WhenRegularCustomerHasTwoOrdersWithMoreThan30DaysThenHeIsNotQualified()
        {
            var customer = GetCustomer("Regular",
                0,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 30),
                    GetOrder(DateTime.UtcNow.AddDays(-40),100)
                });

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsFalse(isQualified);
        }


        [Test]
        public void WhenSilverCustomerHasOrdersWithin30DaysWithAValueOfOver600ThenHeIsQualified()
        {
            var customer = GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 700),
                 
                });

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsTrue(isQualified);
        }


        [Test]
        public void WhenSilverCustomerWasLastBumpedLessThanSevenDaysAgoHeDoesNotQualify()
        {
            var customer = GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 700),

                },DateTime.UtcNow.AddDays(-1));

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsFalse(isQualified);
        }

        [Test]
        public void WhenGoldCustomerHasOrdersWithin30DaysWithAValueOfOver2000ThenHeIsQualified()
        {
            var customer = GetCustomer("Gold",
                2,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 2000),

                });

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsTrue(isQualified);
        }

        [Test]
        public void WhenGoldCustomerHasOrdersWithin30DaysWithAValueLessThan1000ThenHeIsNotQualified()
        {
            var customer = GetCustomer("Gold",
                2,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 700),

                }, DateTime.UtcNow.AddDays(-1));

            var statusRepo = new CustomerStatusLevelRepository();

            var nextLevel = statusRepo.GetNextLevel(customer.Status.Level);

            var isQualified = customer.IsQualifiedForNextLevel(nextLevel, 7);

            Assert.IsFalse(isQualified);
        }

        [Test]
        public void WhenSilverCustomerHasOrdersWithin30DaysWithAValueOfOver2000ThenHeIsNotQualifiedForPlatinum()
        {
            var customer = GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    GetOrder(DateTime.UtcNow, 2000),

                });

            var statusRepo = new CustomerStatusLevelRepository();

            var platinumLevel = statusRepo.GetNextLevel(customer.Status.Level + 1);

            var isQualified = customer.IsQualifiedForNextLevel(platinumLevel, 7);

            Assert.IsFalse(isQualified);
        }

        public static Order GetOrder(DateTime orderDate,double basketValue)
        {
            return new Order()
            {
                EmailAddress = "gnu@email.dk",
                OrderDate = orderDate,
                Basket = new BasketItem[] {new BasketItem() {ItemId=1,Price= basketValue}}
            };
        }


        public static  Customer GetCustomer(string state,int level,List<Order> orderHistory,DateTime? lastBumpedDate =null)
        {

            var status = new Status(state, level)
            {
                Date = lastBumpedDate ?? DateTime.MinValue
            };
            return new Customer("gnu@email.dk", "Thomas Garp",status)
            {
                OrderHistory = orderHistory
            };
        }
    }
}

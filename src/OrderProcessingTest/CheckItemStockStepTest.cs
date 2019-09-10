using NUnit.Framework;
using OrderProcessing.WorkflowSteps;
using SharedModel.Model;
using SharedModel.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingTest
{
    class CheckItemStockStepTest
    {
        [Test]
        public async Task WhenAllItemsAreInStockThenReturnOK()
        {
            var stockItemCheckStep = new CheckItemStockStep(new ProductRepository());
            var customer = CustomerStatusHelperTest.GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    CustomerStatusHelperTest.GetOrder(DateTime.UtcNow, 700),

                });

            var order = GetOrder(DateTime.UtcNow, 700, 1);
            var context = new OrderContext(order)
            {
                Customer = customer,
            };

            var result = await stockItemCheckStep.Run(context);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("All items are in stock", result.Value);
        }

        [Test]
        public async Task WhenNotAllItemsAreInStockThenReturnFail()
        {
            var stockItemCheckStep = new CheckItemStockStep(new ProductRepository());
            var customer = CustomerStatusHelperTest.GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    CustomerStatusHelperTest.GetOrder(DateTime.UtcNow, 700),

                });

            var order = GetOrder(DateTime.UtcNow, 700, 3);
            var context = new OrderContext(order)
            {
                Customer = customer,
            };

            var result = await stockItemCheckStep.Run(context);

            Assert.IsTrue(result.IsFailure);
        }

        public static Order GetOrder(DateTime orderDate, double basketValue, int itemId)
        {
            return new Order()
            {
                EmailAddress = "gnu@email.dk",
                OrderDate = orderDate,
                Basket = new BasketItem[] { new BasketItem() { ItemId = itemId, Price = basketValue } }
            };
        }
    }
}

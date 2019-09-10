using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using OrderProcessing.WorkflowSteps;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessingTest
{
    public class SetCustomerStatusStepTest
    {
        [Test]
        public async Task ShouldBumpCustomer()
        {
            var sut = new SetCustomerStatusStep(new CustomerStatusLevelRepository(), new TestCustomerStatusSettings());

            var customer = CustomerStatusHelperTest.GetCustomer("Silver",
                1,
                new List<Order>()
                {
                    CustomerStatusHelperTest.GetOrder(DateTime.UtcNow, 700),

                });

            var order = CustomerStatusHelperTest.GetOrder(DateTime.UtcNow, 700);
            var context = new OrderContext(order)
            {
                Customer = customer,
            };


            var result = await sut.Run(
               context

                );

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2,context.Customer.Status.Level );
        }
    }

    public class TestCustomerStatusSettings : ICustomerStatusSettings
    {

        public TestCustomerStatusSettings()
        {
            MinimumQuarantineIntervalInDays = 7;
        }
        public int MinimumQuarantineIntervalInDays { get; set; }
    }
}

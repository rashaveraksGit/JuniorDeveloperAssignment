using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OrderProcessing;
using OrderProcessing.Bootstrapper;
using Serilog;
using SharedModel.Model;
using Unity;

namespace OrderProcessingTest
{
    public class OrderProcessorTest
    {
        public UnityContainer Container { get; set; }
        [OneTimeSetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            Container = BootStrapper.Initialize();
        }

        [Test]

        public async Task ShouldGiveRegularCustomerOrder()
        {
            var orderProcessor = Container.Resolve<IOrderProcessor>();

            var order = new Order()
            {
                FullName = "Thomas Garp",
                EmailAddress = "tga@raptor.dk",
                Basket = new[]
                {
                    new BasketItem() {ItemId = 1,  Price = 299.95},
                    new BasketItem() {ItemId = 2,  Price = 50},
                }

            };

            var result = await orderProcessor.ProcessAsync(order);


            Assert.IsTrue(result.IsSuccess);
        }

        [Test]

        public async Task ShouldGiveSilverCustomerOrder()
        {
            var orderProcessor = Container.Resolve<IOrderProcessor>();

            var order = new Order()
            {
                FullName = "Thomas Garp",
                EmailAddress = "tga@raptor.dk",
                OrderDate = DateTime.UtcNow,
                Basket = new[]
                {
                    new BasketItem() {ItemId = 1, Price = 299.95},
                    new BasketItem() {ItemId = 1, Price = 50},
                }

            };

            var result = await orderProcessor.ProcessAsync(order);


            var order1 = new Order()
            {
                FullName = "Thomas Garp",
                EmailAddress = "tga@raptor.dk",
                OrderDate = DateTime.UtcNow.AddDays(-10),
                Basket = new[]
                {
                    new BasketItem() {ItemId = 1,  Price = 200},
                  
                }

            };

            var result1 = await orderProcessor.ProcessAsync(order1);

            Assert.IsTrue(result1.IsSuccess);
        }

        [Test]
        public void GetWorkFlowAsDotGraph()
        {

            var workflow = new OrderWorkflow();
            Console.Write(workflow.ToDotGraph());

            //The output json can be visualized by a dot graph visualizer, for instance http://www.webgraphviz.com/
        }
    }
}

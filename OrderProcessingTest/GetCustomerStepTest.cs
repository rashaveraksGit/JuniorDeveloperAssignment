using Moq;
using NUnit.Framework;
using OrderProcessing;
using OrderProcessing.WorkflowSteps;
using SharedModel.Model;
using SharedModel.Repositories;
using SharedModel.Services;

namespace OrderProcessingTest
{
    [TestFixture]
    public class GetCustomerStepTest
    {
        [Test]
        public void ShouldPullCustomerFromRepo()
        {
            var customer = new Customer("gnu@email.dk", "Hans",new Status("gnu",0));
            var customerRepo = new Mock<ICustomerRepository>();
            customerRepo.Setup(c => c.TryGetCustomerFromEmail(It.IsAny<string>()))
                .Returns(customer);


            var sut = new GetCustomerStep(customerRepo.Object,new CustomerStatusLevelRepository());

            var orderContext = new OrderContext(new Order() { EmailAddress=customer.Email});

            sut.Run(orderContext);

            customerRepo.Verify(c => c.TryGetCustomerFromEmail(customer.Email), Times.Once);
            Assert.IsNotNull(orderContext.Customer);
            Assert.AreEqual(customer.Email,orderContext.Customer.Email);
        }

        [Test]
        public void ShouldCreateANewCustomer()
        {
            var email = "gnu@email.dk";
            var customerRepo = new Mock<ICustomerRepository>();
            customerRepo.Setup(c => c.TryGetCustomerFromEmail(It.IsAny<string>()))
                .Returns((Customer)null);
            var sut = new GetCustomerStep(customerRepo.Object,new CustomerStatusLevelRepository());

            var orderContext = new OrderContext(new Order() { EmailAddress = email });

            sut.Run(orderContext);

            
            Assert.IsNotNull(orderContext.Customer);
            Assert.AreEqual(email, orderContext.Customer.Email);
        }
    }
}

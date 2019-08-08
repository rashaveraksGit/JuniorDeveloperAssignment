using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessing
{
    public class MessageHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderProcessor _orderProcessor;

        public MessageHandler(IProductRepository productRepository, IOrderProcessor orderProcessor)
        {
            _productRepository = productRepository;
            _orderProcessor = orderProcessor;
        }

        //Converts a message (for instance from a message queue) into an order, and starts the orderflow
        public async Task HandleMessage(Message message)
        {

            
            var orderItems = message.Basket.Select(b => _productRepository.Products[b]);

            var order = new Order()
            {
                FullName = message.FullName,
                EmailAddress = message.Emailaddress,
                Basket = orderItems.ToArray()

            };

            await _orderProcessor.ProcessAsync(order);


        }
    }
}

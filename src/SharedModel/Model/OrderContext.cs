namespace SharedModel.Model
{
    public class OrderContext
    {
        public Order Order { get; }
        public Customer Customer { get; set; }

        public OrderContext(Order order)
        {
            Order = order;
        }
    }
}
using System;
using System.Linq;


namespace SharedModel.Model
{
    public class Order
    {

        public Order()
        {
            OrderDate = DateTime.UtcNow;
            OrderId = Guid.NewGuid();
        }

        public DateTime OrderDate { get; set; }

        public Guid OrderId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }

        public BasketItem[] Basket { get; set; }
        public string DíscountLevel { get; set; }

        public double DiscountPercentage { get; set; }

        public double Discount => (TotalPrice * DiscountPercentage) / 100;

        public double TotalPrice
        {
            get { return Basket.Sum(b => b.Price); }
        }

        public double DiscountPrice => TotalPrice - Discount;
    }
}
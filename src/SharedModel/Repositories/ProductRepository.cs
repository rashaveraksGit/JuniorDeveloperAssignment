using System;
using System.Collections.Generic;
using System.Text;
using SharedModel.Model;

namespace SharedModel.Repositories
{
    public class ProductRepository:IProductRepository
    {

        public ProductRepository()
        {

            //Emulated datalayer coming from DB
            Products = new Dictionary<int, Item>()
            {
                {
                    1,
                    new Item() {Id = 1, Name = "Shoes", Price = 299.95, ItemsInStock = 2}
                },
                {
                    2,
                    new Item() {Id = 2, Name = "Belt", Price = 50,ItemsInStock = 5}
                },
                {
                    3,
                    new Item() {Id = 3, Name = "Shorts", Price = 200, ItemsInStock = 0}
                }

            };
        }

       
        public Dictionary<int, Item> Products { get; }
    }

    public interface IProductRepository
    {
        Dictionary<int, Item> Products { get; }
    }
}

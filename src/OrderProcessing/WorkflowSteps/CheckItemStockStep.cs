using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedModel.Model;
using SharedModel.Repositories;

namespace OrderProcessing.WorkflowSteps
{
   
    public class CheckItemStockStep : IWorkflowStep
    {
        private readonly IProductRepository _productRepository;
        public CheckItemStockStep(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result<string>> Run(OrderContext context)
        {
            var missingItemIds = new List<int>();

            var products = _productRepository.Products;
            foreach( var item in context.Order.Basket)
            {
                if(!products.ContainsKey(item.ItemId))
                    return Task.FromResult(Result.Fail<string>("Invalid ItemId"));

                if (products[item.ItemId].ItemsInStock == 0)
                    missingItemIds.Add(item.ItemId);
                    //return Task.FromResult(Result.Fail<string>($"Item with id {item.ItemId} was not in stock"));
            }
            if(missingItemIds.Count == 0)
                return Task.FromResult(Result.Ok("All items are in stock"));

            return Task.FromResult(Result.Fail<string>($"Items with ids {string.Join(',', missingItemIds)} were not in stock"));
        }
    }
}

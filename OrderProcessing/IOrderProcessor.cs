using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
using SharedModel.Model;

namespace OrderProcessing
{
    public interface IOrderProcessor
    {
        Task<Result<string>> ProcessAsync(Order order);
    }
}
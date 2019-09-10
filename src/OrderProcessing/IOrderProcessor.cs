using System.Threading.Tasks;
using SharedModel.Model;

namespace OrderProcessing
{
    public interface IOrderProcessor
    {
        Task<Result<string>> ProcessAsync(Order order);
    }
}
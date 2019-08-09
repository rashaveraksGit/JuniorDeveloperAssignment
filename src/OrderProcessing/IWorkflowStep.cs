using System.Threading.Tasks;
using SharedModel.Model;

namespace OrderProcessing
{
    public interface IWorkflowStep
    {
        Task<Result<string>> Run(OrderContext context);
    }
}
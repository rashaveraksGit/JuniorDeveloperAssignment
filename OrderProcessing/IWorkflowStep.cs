using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;

namespace OrderProcessing
{
    public interface IWorkflowStep
    {
        Task<Result<string>> Run(OrderContext context);
    }
}
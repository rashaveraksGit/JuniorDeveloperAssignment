using System.Threading.Tasks;
using Raptor.Core.Utilities.Domain;
using SharedModel.Model;

namespace OrderProcessing
{
    public interface IWorkflowStep
    {
        Task<Result<string>> Run(OrderContext context);
    }
}
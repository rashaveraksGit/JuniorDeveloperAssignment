using System.Collections.Generic;
using SharedModel.Model;

namespace SharedModel.Repositories
{
    public interface ICustomerStatusLevelRepository
    {
        List<CustomerStatusLevel> GetCustomerStatuses();
        CustomerStatusLevel GetStartLevel();
        CustomerStatusLevel GetNextLevel(int level);
        CustomerStatusLevel GetLevel(int statusLevel);
    }
}
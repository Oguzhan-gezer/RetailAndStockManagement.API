using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailAndStockManagement.Data
{
    public interface IRetailAndStockManagementContext
    {
        Task<int> SaveChangesAsync();
    }
}

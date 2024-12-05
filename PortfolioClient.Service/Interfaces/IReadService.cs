using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClient.Service.Interfaces
{
    public interface IReadService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(string endpoint, string objectName);
        Task<TEntity> GetAsync(string endpoint, string id);

    }
}

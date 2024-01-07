using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace PersonalFinanceAssistant
{
    public interface IPersonalFinanceAssistantRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        Task<TEntity?> GetAsync(TKey id, bool withDetails = false, bool noTracking = false, bool notFoundException = true);
        Task<List<TEntity>> GetListAsync(bool withDetails = false, bool noTracking = false);
        Task<IQueryable<TEntity>> GetQueryableAsync(bool withDetails = false, bool noTracking = false);
    }
}

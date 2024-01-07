using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.EntityFrameworkCore;
using PersonalFinanceAssistant.FinanceOperations;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace PersonalFinanceAssistant.Repositories
{
    public class FinanceOperationsRepository : PersonalFinanceAssistantRepository<FinanceOperation, int>, IFinanceOperationsRepository
    {
        public FinanceOperationsRepository(IDbContextProvider<PersonalFinanceAssistantDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        override protected async Task<IQueryable<FinanceOperation>> GetDetailedQueryAsync(bool noTracking)
        {
            var query = await base.GetQueryableAsync();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            query = query
                .Include(x => x.FinanceAccount)
                .Include(x => x.Good).ThenInclude(x => x.Category)
                ;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            return query;
        }
    }
}

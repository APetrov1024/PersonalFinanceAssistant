using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.EntityFrameworkCore;
using PersonalFinanceAssistant.FinanceAccounts;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace PersonalFinanceAssistant.Repositories
{
    public class FinanceAccountsRepository : PersonalFinanceAssistantRepository<FinanceAccount, int>, IFinanceAccountsRepository
    {
        public FinanceAccountsRepository(IDbContextProvider<PersonalFinanceAssistantDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        override protected async Task<IQueryable<FinanceAccount>> GetDetailedQueryAsync(bool noTracking)
        {
            var query = await base.GetQueryableAsync();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            //query = query.Include(x => x.Property);
            return query;
        }
    }
}

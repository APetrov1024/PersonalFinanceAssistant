using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.EntityFrameworkCore;
using PersonalFinanceAssistant.Goods;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace PersonalFinanceAssistant.Repositories
{
    public class GoodsRepository : PersonalFinanceAssistantRepository<Good, int>, IGoodsRepository
    {
        public GoodsRepository(IDbContextProvider<PersonalFinanceAssistantDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
            
        }

        override protected async Task<IQueryable<Good>> GetDetailedQueryAsync(bool noTracking)
        {
            var query = await base.GetQueryableAsync();
            if (noTracking)
            { 
                query = query.AsNoTracking();
            }
            query = query.Include(x => x.Category);
            return query;
        }
    }
}

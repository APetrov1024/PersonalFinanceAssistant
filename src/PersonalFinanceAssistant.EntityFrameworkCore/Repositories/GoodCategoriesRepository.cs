using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.EntityFrameworkCore;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace PersonalFinanceAssistant.Repositories
{
    public class GoodCategoriesRepository : PersonalFinanceAssistantRepository<GoodCategory, int>, IGoodCategoriesRepository
    {
        public GoodCategoriesRepository(IDbContextProvider<PersonalFinanceAssistantDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
            
        }

        //TODO: эта сущность имеет древовидную структуру, не нашел способа грузить все дерево средствами EF, кроме Lazy Loading
        // которой не хотелось бы использовать. Похоже нужен отдельный метод для загрузки

        override protected async Task<IQueryable<GoodCategory>> GetDetailedQueryAsync(bool noTracking)
        {
            var query = await base.GetQueryableAsync();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            query = query
                .Include(x => x.ParentCategory)
                .Include(x => x.ChildCategories)
                ;
            return query;
        }
    }
}

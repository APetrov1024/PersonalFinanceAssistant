using Microsoft.EntityFrameworkCore;
using PersonalFinanceAssistant.EntityFrameworkCore;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PersonalFinanceAssistant.Repositories
{
    public abstract class PersonalFinanceAssistantRepository<TEntity, TKey>
        : EfCoreRepository<PersonalFinanceAssistantDbContext, TEntity, TKey>, 
          IPersonalFinanceAssistantRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {
        public PersonalFinanceAssistantRepository(IDbContextProvider<PersonalFinanceAssistantDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            
        }

        virtual public async Task<TEntity?> GetAsync(TKey id, bool withDetails = false, bool noTracking = false, bool notFoundException = true)
        {
            TEntity? entity = null;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var query = (await this.GetQueryAsync(withDetails, noTracking))
                .Where(x => x.Id.Equals(id));
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            entity = await AsyncExecuter.SingleOrDefaultAsync(query);
            if (entity == null && notFoundException)
            {
                throw new EntityNotFoundException(typeof(Good), id);
            }
            return entity;
        }

        virtual public async Task<List<TEntity>> GetListAsync(bool withDetails = false, bool noTracking = false)
        {
            var query = await this.GetQueryAsync(withDetails, noTracking);
            return await AsyncExecuter.ToListAsync(query);
        }

        virtual public async Task<IQueryable<TEntity>> GetQueryableAsync(bool withDetails = false, bool noTracking = false)
        {
            return await this.GetQueryAsync(withDetails, noTracking);
        }

        virtual protected async Task<IQueryable<TEntity>> GetQueryAsync(bool withDetails, bool noTracking)
        {
            return withDetails ? await this.GetDetailedQueryAsync(noTracking) : await this.GetSimpleQueryAsync(noTracking);
        }

        virtual protected async Task<IQueryable<TEntity>> GetSimpleQueryAsync(bool noTracking)
        {
            var query = await base.GetQueryableAsync();
            return noTracking ? query.AsNoTracking() : query;
        }

        abstract protected Task<IQueryable<TEntity>> GetDetailedQueryAsync(bool noTracking);
    }
}

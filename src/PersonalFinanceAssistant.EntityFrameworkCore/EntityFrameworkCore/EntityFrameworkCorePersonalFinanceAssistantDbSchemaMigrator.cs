using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinanceAssistant.Data;
using Volo.Abp.DependencyInjection;

namespace PersonalFinanceAssistant.EntityFrameworkCore;

public class EntityFrameworkCorePersonalFinanceAssistantDbSchemaMigrator
    : IPersonalFinanceAssistantDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePersonalFinanceAssistantDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the PersonalFinanceAssistantDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PersonalFinanceAssistantDbContext>()
            .Database
            .MigrateAsync();
    }
}

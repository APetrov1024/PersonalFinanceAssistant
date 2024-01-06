using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PersonalFinanceAssistant.Data;

/* This is used if database provider does't define
 * IPersonalFinanceAssistantDbSchemaMigrator implementation.
 */
public class NullPersonalFinanceAssistantDbSchemaMigrator : IPersonalFinanceAssistantDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

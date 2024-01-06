using PersonalFinanceAssistant.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PersonalFinanceAssistant.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PersonalFinanceAssistantEntityFrameworkCoreModule),
    typeof(PersonalFinanceAssistantApplicationContractsModule)
    )]
public class PersonalFinanceAssistantDbMigratorModule : AbpModule
{
}

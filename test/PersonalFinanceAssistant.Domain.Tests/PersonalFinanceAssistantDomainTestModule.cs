using Volo.Abp.Modularity;

namespace PersonalFinanceAssistant;

[DependsOn(
    typeof(PersonalFinanceAssistantDomainModule),
    typeof(PersonalFinanceAssistantTestBaseModule)
)]
public class PersonalFinanceAssistantDomainTestModule : AbpModule
{

}

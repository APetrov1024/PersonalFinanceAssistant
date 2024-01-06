using Volo.Abp.Modularity;

namespace PersonalFinanceAssistant;

[DependsOn(
    typeof(PersonalFinanceAssistantApplicationModule),
    typeof(PersonalFinanceAssistantDomainTestModule)
)]
public class PersonalFinanceAssistantApplicationTestModule : AbpModule
{

}

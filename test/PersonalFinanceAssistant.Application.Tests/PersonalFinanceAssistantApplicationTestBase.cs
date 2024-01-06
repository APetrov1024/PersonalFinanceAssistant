using Volo.Abp.Modularity;

namespace PersonalFinanceAssistant;

public abstract class PersonalFinanceAssistantApplicationTestBase<TStartupModule> : PersonalFinanceAssistantTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

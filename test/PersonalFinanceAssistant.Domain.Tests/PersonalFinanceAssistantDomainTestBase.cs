using Volo.Abp.Modularity;

namespace PersonalFinanceAssistant;

/* Inherit from this class for your domain layer tests. */
public abstract class PersonalFinanceAssistantDomainTestBase<TStartupModule> : PersonalFinanceAssistantTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

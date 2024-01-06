using PersonalFinanceAssistant.Samples;
using Xunit;

namespace PersonalFinanceAssistant.EntityFrameworkCore.Domains;

[Collection(PersonalFinanceAssistantTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<PersonalFinanceAssistantEntityFrameworkCoreTestModule>
{

}

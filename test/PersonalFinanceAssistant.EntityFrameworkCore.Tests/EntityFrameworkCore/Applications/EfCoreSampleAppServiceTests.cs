using PersonalFinanceAssistant.Samples;
using Xunit;

namespace PersonalFinanceAssistant.EntityFrameworkCore.Applications;

[Collection(PersonalFinanceAssistantTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<PersonalFinanceAssistantEntityFrameworkCoreTestModule>
{

}

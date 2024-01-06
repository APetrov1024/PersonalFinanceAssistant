using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PersonalFinanceAssistant.Web;

[Dependency(ReplaceServices = true)]
public class PersonalFinanceAssistantBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PersonalFinanceAssistant";
}

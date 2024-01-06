using PersonalFinanceAssistant.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PersonalFinanceAssistant.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PersonalFinanceAssistantController : AbpControllerBase
{
    protected PersonalFinanceAssistantController()
    {
        LocalizationResource = typeof(PersonalFinanceAssistantResource);
    }
}

using PersonalFinanceAssistant.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace PersonalFinanceAssistant.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PersonalFinanceAssistantPageModel : AbpPageModel
{
    protected PersonalFinanceAssistantPageModel()
    {
        LocalizationResourceType = typeof(PersonalFinanceAssistantResource);
    }
}

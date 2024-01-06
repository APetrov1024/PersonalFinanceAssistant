using System;
using System.Collections.Generic;
using System.Text;
using PersonalFinanceAssistant.Localization;
using Volo.Abp.Application.Services;

namespace PersonalFinanceAssistant;

/* Inherit your application services from this class.
 */
public abstract class PersonalFinanceAssistantAppService : ApplicationService
{
    protected PersonalFinanceAssistantAppService()
    {
        LocalizationResource = typeof(PersonalFinanceAssistantResource);
    }
}

using Volo.Abp.Settings;

namespace PersonalFinanceAssistant.Settings;

public class PersonalFinanceAssistantSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(PersonalFinanceAssistantSettings.MySetting1));
    }
}

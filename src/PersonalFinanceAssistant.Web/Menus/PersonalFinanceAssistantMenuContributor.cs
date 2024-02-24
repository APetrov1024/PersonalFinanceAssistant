using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using PersonalFinanceAssistant.Localization;
using PersonalFinanceAssistant.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace PersonalFinanceAssistant.Web.Menus;

public class PersonalFinanceAssistantMenuContributor : IMenuContributor
{
    private int Order { get; set; } = 0;
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<PersonalFinanceAssistantResource>();
        context.Menu.Items.Insert(
           0,
           new ApplicationMenuItem(
               PersonalFinanceAssistantMenus.Home,
               l["Menu:Home"],
               "~/",
               icon: "fas fa-home",
               order: Order++
           )
        );
        Catalogs(context, l);
        Administration(context, l);

        return Task.CompletedTask;
    }

    private void Administration(MenuConfigurationContext context, IStringLocalizer l)
    {
        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, Order++);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, Order++);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, Order++);
    }

    private void Catalogs(MenuConfigurationContext context, IStringLocalizer l)
    {
        var catalogs = new ApplicationMenuItem(
                PersonalFinanceAssistantMenus.Catalogs.Group,
                "Справочники",
                order: Order++
            );
        context.Menu.Items.Insert( 0, catalogs );
        catalogs.AddItem(new ApplicationMenuItem(
                PersonalFinanceAssistantMenus.Catalogs.GoodsAndCategories,
                "Товары и категории",
                "~/Catalogs/GoodsAndCategories/GoodsAndCategories",
                order: Order++
            ));
        catalogs.AddItem(new ApplicationMenuItem(
                PersonalFinanceAssistantMenus.Catalogs.GoodsAndCategories,
                "Счета",
                "~/Catalogs/FinanceAccounts/FinanceAccounts",
                order: Order++
            ));
    }
}

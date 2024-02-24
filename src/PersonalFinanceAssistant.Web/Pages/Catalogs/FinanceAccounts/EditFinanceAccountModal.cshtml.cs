using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalFinanceAssistant.Catalogs.FinanceAccounts;
using PersonalFinanceAssistant.Catalogs.SelectLists;
using PersonalFinanceAssistant.CommonDtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Web.Pages.Catalogs.FinanceAccounts
{
    public class EditFinanceAccountModalModel : PersonalFinanceAssistantPageModel
    {
        private readonly ICurrenciesSelectListAppService _currenciesSelectListAppService;
        private readonly IFinanceAccountsAppService _financeAccountsAppService;

        public EditFinanceAccountModalModel(
            ICurrenciesSelectListAppService currenciesSelectListAppService,
            IFinanceAccountsAppService financeAccountsAppService)
        {
            _currenciesSelectListAppService = currenciesSelectListAppService;
            _financeAccountsAppService = financeAccountsAppService;
        }

        public EditFinanceAccountModalVM VM { get; set; } = new EditFinanceAccountModalVM();

        public async Task OnGetAsync(int? id)
        {
            VM.ModalCaption = id.HasValue ? "Редактирование счета" : "Новый счет";
            var currencies = await _currenciesSelectListAppService.GetListAsync();
            VM.Currencies = ObjectMapper.Map<List<SelectListItemDto<int>>, List<SelectListItem>>(currencies);
            if (id.HasValue)
            {
                var account = await _financeAccountsAppService.GetAsync(id.Value);
                VM.CurrencyId = account.CurrencyId;
                VM.IsDefault = account.IsDefault;
                VM.Name = account.Name;
            }
            else
            { 
                VM.CurrencyId = currencies.First().Value;
            }
        }


    }

    public class EditFinanceAccountModalVM
    {
        public string ModalCaption { get; set; } = string.Empty;

        [Display(Name = "Валюта")]
        public int CurrencyId { get; set; }

        [Display(Name = "Использовать по умолчанию")]
        public bool IsDefault { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;
        public List<SelectListItem> Currencies { get; set; } = new List<SelectListItem>();
    }
}

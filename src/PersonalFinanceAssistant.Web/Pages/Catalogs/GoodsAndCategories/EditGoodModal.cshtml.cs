using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalFinanceAssistant.Catalogs.Goods;
using PersonalFinanceAssistant.Goods;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Web.Pages.Catalogs.GoodsAndCategories
{
    public class EditGoodModalModel : PersonalFinanceAssistantPageModel
    {
        private readonly IGoodsAppService _goodsAppService;

        public EditGoodModalModel(IGoodsAppService goodsAppService)
        {
            _goodsAppService = goodsAppService;
        }
        
        public EditGoodModalVM VM { get; set; }
        
        public async Task OnGetAsync(int? id)
        {
            VM = new EditGoodModalVM
            {
                ModalCaption = id.HasValue ? "Редактирование товара" : "Добавление товара",
            };
            if (id.HasValue)
            {
                var good = await _goodsAppService.GetAsync(id.Value);
                VM.Name = good.Name;
            }
        }
    }
}
public class EditGoodModalVM
{
    public string ModalCaption { get; set; } = string.Empty;

    [Required]
    [MaxLength(GoodConsts.MaxNameLength)]
    [Display(Name = "Имя")]
    public string Name { get; set; } = string.Empty;

}
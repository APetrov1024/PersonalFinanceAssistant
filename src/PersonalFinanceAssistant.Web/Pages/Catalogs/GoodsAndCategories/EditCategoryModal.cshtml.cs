using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalFinanceAssistant.Catalogs.GoodCategories;
using PersonalFinanceAssistant.Goods;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Web.Pages.Catalogs.GoodsAndCategories
{
    public class EditCategoryModalModel : PersonalFinanceAssistantPageModel
    {
        private readonly IGoodCategoriesAppService _goodCategoriesAppService;

        public EditCategoryModalModel(IGoodCategoriesAppService goodCategoriesAppService)
        {
            _goodCategoriesAppService = goodCategoriesAppService;
        }

        public EditCategoryModalVM VM { get; set; }
        public async Task OnGetAsync(int? id)
        {
            VM = new EditCategoryModalVM
            {
                ModalCaption = id.HasValue ? "Редактирование категории" : "Добавление категории",
            };
            if (id.HasValue)
            { 
                var category = await _goodCategoriesAppService.GetAsync(id.Value);
                VM.Name = category.Name;
            }
        }
    }
}

public class EditCategoryModalVM
{ 
    public string ModalCaption { get; set; } = string.Empty;

    [Required]
    [MaxLength(GoodConsts.MaxNameLength)]
    [Display(Name = "Имя")]
    public string Name { get; set; } = string.Empty;

}
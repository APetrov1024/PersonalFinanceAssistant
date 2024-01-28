using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Catalogs.GoodCategories
{
    public interface IGoodCategoriesAppService
    {
        Task<GoodCategoryDto> GetAsync(int id);
        Task<List<GoodCategoryDto>> GetListAsync(GoodCategoriesListRequestDto dto);
        Task<GoodCategoryDto> CreateAsync(CreateUpdateGoodCategoryDto dto);
        Task<GoodCategoryDto> UpdateAsync(int id, CreateUpdateGoodCategoryDto dto);
        Task DeleteAsync(int id);
    }
}

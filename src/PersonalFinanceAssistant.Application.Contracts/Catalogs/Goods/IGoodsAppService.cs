using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Catalogs.Goods
{
    public interface IGoodsAppService
    {
        Task<GoodDto> GetAsync(int id);
        Task<List<GoodDto>> GetListAsync(int? categoryId);
        Task<GoodDto> CreateAsync(CreateUpdateGoodDto dto);
        Task<GoodDto> UpdateAsync(int id, CreateUpdateGoodDto dto);
        Task DeleteAsync(int id);
    }
}

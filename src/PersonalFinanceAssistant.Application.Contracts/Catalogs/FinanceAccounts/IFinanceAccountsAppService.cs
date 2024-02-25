using PersonalFinanceAssistant.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PersonalFinanceAssistant.Catalogs.FinanceAccounts
{
    public interface IFinanceAccountsAppService : IApplicationService
    {
        Task<FinanceAccountDto> GetAsync(int id);
        Task<List<FinanceAccountDto>> GetListAsync();
        Task<List<SelectListItemDto<int>>> GetSelectListAsync();
        Task<FinanceAccountDto> CreateAsync(CreateUpdateFinanceAccountDto dto);
        Task<FinanceAccountDto> UpdateAsync(int id, CreateUpdateFinanceAccountDto dto);
        Task DeleteAsync(int id);
    }
}

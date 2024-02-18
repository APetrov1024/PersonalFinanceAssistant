using PersonalFinanceAssistant.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace PersonalFinanceAssistant.Catalogs.SelectLists
{
    public interface ICurrenciesSelectListAppService: IApplicationService
    {
        Task<List<SelectListItemDto<int>>> GetListAsync();
    }
}

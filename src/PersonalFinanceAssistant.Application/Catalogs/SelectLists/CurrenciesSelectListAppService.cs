using AutoMapper.Internal.Mappers;
using PersonalFinanceAssistant.CommonDtos;
using PersonalFinanceAssistant.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PersonalFinanceAssistant.Catalogs.SelectLists
{
    public class CurrenciesSelectListAppService : PersonalFinanceAssistantAppService, ICurrenciesSelectListAppService
    {
        private readonly IRepository<Currency, int> _currencyRepository;

        public CurrenciesSelectListAppService(IRepository<Currency, int> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<SelectListItemDto<int>>> GetListAsync()
        {
            var entities = await _currencyRepository.GetListAsync();
            return ObjectMapper.Map<List<Currency>, List<SelectListItemDto<int>>>(entities);
        }
    }
}

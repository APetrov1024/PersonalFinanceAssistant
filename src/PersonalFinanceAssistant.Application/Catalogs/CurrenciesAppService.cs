using AutoMapper.Internal.Mappers;
using PersonalFinanceAssistant.Catalogs.SelectLists;
using PersonalFinanceAssistant.CommonDtos;
using PersonalFinanceAssistant.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PersonalFinanceAssistant.Catalogs
{
    public class CurrenciesAppService : PersonalFinanceAssistantAppService, ICurrenciesAppService
    {
        private readonly IRepository<Currency, int> _currencyRepository;

        public CurrenciesAppService(IRepository<Currency, int> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<List<SelectListItemDto<int>>> GetSelectListAsync()
        {
            var entities = await _currencyRepository.GetListAsync();
            return ObjectMapper.Map<List<Currency>, List<SelectListItemDto<int>>>(entities);
        }
    }
}

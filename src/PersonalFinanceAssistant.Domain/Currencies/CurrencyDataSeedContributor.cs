using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace PersonalFinanceAssistant.Currencies
{
    public class CurrencyDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        // add currency here
        private List<Currency> allCurrencies = new List<Currency> { 
            new Currency { Name = "Российский рубль", Alpha3Code = "RUB" },
            new Currency { Name = "Доллар США", Alpha3Code = "USD" },
            new Currency { Name = "Евро", Alpha3Code = "EUR" },
        };

        private readonly IRepository<Currency, int> _currencyRepository;

        public CurrencyDataSeedContributor(IRepository<Currency, int> currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _currencyRepository.InsertManyAsync(await GetCurrenciesToSeedAsync());
        }

        private async Task<List<Currency>> GetCurrenciesToSeedAsync() 
        { 
            var exists = await _currencyRepository.GetListAsync();
            return allCurrencies
                .Where(x => !exists.Exists(y => y.Alpha3Code.Trim().ToLower() == x.Alpha3Code.Trim().ToLower() || y.Name.Trim().ToLower() == x.Name.Trim().ToLower()))
                .ToList();
        }
    }
}

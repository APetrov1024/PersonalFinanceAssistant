using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceAssistant.FinanceAccounts;

namespace PersonalFinanceAssistant.Catalogs.FinanceAccounts
{
    public class FinanceAccountsAutoMapperProfile : PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public FinanceAccountsAutoMapperProfile()
        {
            CreateMap<FinanceAccount, FinanceAccountDto>()
                .ForMember(x => x.Name, map => map.MapFrom(src => src.IsDeleted ? $"{src.Name} (Архивный)" : src.Name))
                .ForMember(x => x.CurrencyName, map => map.MapFrom(src => ValueOrDefault(src, "Currency.Name", string.Empty)));
            CreateMap<CreateUpdateFinanceAccountDto, FinanceAccount>();
        }
    }
}

using PersonalFinanceAssistant.CommonDtos;
using PersonalFinanceAssistant.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Catalogs
{
    public class CurrenciesAutoMapperProfile : PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public CurrenciesAutoMapperProfile()
        {
            CreateMap<Currency, SelectListItemDto<int>>()
                .ForMember(x => x.Value, map => map.MapFrom(src => src.Id))
                .ForMember(x => x.Text, map => map.MapFrom(src => src.DisplayName));

        }
    }
}

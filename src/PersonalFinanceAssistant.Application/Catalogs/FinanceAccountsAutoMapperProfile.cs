using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceAssistant.Catalogs.FinanceAccounts;
using PersonalFinanceAssistant.CommonDtos;
using PersonalFinanceAssistant.FinanceAccounts;

namespace PersonalFinanceAssistant.Catalogs
{
    public class FinanceAccountsAutoMapperProfile : PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public FinanceAccountsAutoMapperProfile()
        {
            CreateMap<FinanceAccount, FinanceAccountDto>()
                .ForMember(x => x.Name, map => map.MapFrom(src => StringWithSoftDeleteMark(src, "Name", string.Empty, false, DefaultSoftDeleteMark)))
                .ForMember(x => x.CurrencyName, map => map.MapFrom(src => ValueOrDefault(src, "Currency.Name", string.Empty)));
            CreateMap<CreateUpdateFinanceAccountDto, FinanceAccount>();
            CreateMap<FinanceAccount, SelectListItemDto<int>>()
                .ForMember(x => x.Value, map => map.MapFrom(src => src.Id))
                .ForMember(x => x.Text, map => map.MapFrom(src => MapSelectListItemText(src)));
        }

        private string MapSelectListItemText(FinanceAccount src)
        {
            var name = StringWithSoftDeleteMark(src, "Name");
            return $"{name} ({ValueOrDefault(src, "Currency.Alpha3Code", string.Empty)})";
        }
    }
}

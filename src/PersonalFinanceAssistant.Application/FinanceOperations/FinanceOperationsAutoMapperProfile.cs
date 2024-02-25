using PersonalFinanceAssistant.FinanceAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public class FinanceOperationsAutoMapperProfile: PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public FinanceOperationsAutoMapperProfile()
        {
            CreateMap<FinanceOperation, FinanceOperationDto>()
                .ForMember(x => x.FinanceAccountName, map => map.MapFrom(src => StringWithSoftDeleteMark(src.FinanceAccount, "Name", string.Empty, false, DefaultSoftDeleteMark)))
                .ForMember(x => x.GoodName, map => map.MapFrom(src => StringWithSoftDeleteMark(src.Good, "Name", string.Empty, false, DefaultSoftDeleteMark)))
                .ForMember(x => x.Date, map => map.MapFrom(src => src.CreationTime));
        }
    }
}

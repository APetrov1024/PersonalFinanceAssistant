using AutoMapper;
using PersonalFinanceAssistant.Catalogs.Goods;
using PersonalFinanceAssistant.CommonDtos;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Catalogs
{
    public class GoodsAutoMapperProfile: PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public GoodsAutoMapperProfile()
        {
            CreateMap<Good, GoodDto>()
                .ForMember(x => x.Name, map => map.MapFrom(src => StringWithSoftDeleteMark(src, "Name", string.Empty, false, DefaultSoftDeleteMark)))
                .ForMember(x => x.CategoryName, map => map.MapFrom(src => ValueOrDefault(src, "Category.Name", string.Empty)))
                ;
            CreateMap<CreateUpdateGoodDto, Good>();
            CreateMap<Good, SelectListItemDto<int>>()
               .ForMember(x => x.Value, map => map.MapFrom(src => src.Id))
               .ForMember(x => x.Text, map => map.MapFrom(src => StringWithSoftDeleteMark(src, "Name", string.Empty, false, DefaultSoftDeleteMark)));
        }

    }
}

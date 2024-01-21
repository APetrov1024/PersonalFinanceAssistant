using AutoMapper;
using PersonalFinanceAssistant.Catalogs.Goods;
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
                .ForMember(x => x.CategoryName, map => map.MapFrom(src => ValueOrDefault(src, "Category.Name", string.Empty)))
                ;
            CreateMap<CreateUpdateGoodDto, Good>();
        }

    }
}

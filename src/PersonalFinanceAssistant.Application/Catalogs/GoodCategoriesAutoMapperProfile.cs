using PersonalFinanceAssistant.Catalogs.GoodCategories;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Catalogs
{
    public class GoodCategoriesAutoMapperProfile: PersonalFinanceAssistantApplicationAutoMapperProfile
    {
        public GoodCategoriesAutoMapperProfile()
        {
            CreateMap<GoodCategory, GoodCategoryDto>()
                .ForMember(x => x.Name, map => map.MapFrom(src => StringWithSoftDeleteMark(src, "Name", string.Empty, false, DefaultSoftDeleteMark)));
            CreateMap<GoodCategory, GoodCategoryListItemDto>()
                .ForMember(x => x.HasChilds, map => map.MapFrom(src => ValueOrDefault(src, "ChildCategories.Count", 0) > 0))
                .ForMember(x => x.Name, map => map.MapFrom(src => StringWithSoftDeleteMark(src, "Name", string.Empty, false, DefaultSoftDeleteMark)));
            CreateMap<CreateUpdateGoodCategoryDto, GoodCategory>();
        }
    }
}

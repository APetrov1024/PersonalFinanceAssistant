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
            CreateMap<GoodCategory, GoodCategoryDto>();
            CreateMap<CreateUpdateGoodCategoryDto, GoodCategory>();
        }
    }
}

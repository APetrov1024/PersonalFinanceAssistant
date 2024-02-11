using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace PersonalFinanceAssistant.Catalogs.GoodCategories
{
    public class GoodCategoryListItemDto: GoodCategoryBaseDto
    {
        public bool HasChilds { get; set; }
    }
}

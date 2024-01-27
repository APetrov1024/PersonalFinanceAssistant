using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace PersonalFinanceAssistant.Catalogs.GoodCategories
{
    public class GoodCategoryDto: EntityDto<int>
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }

    }
}

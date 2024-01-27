using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalFinanceAssistant.Catalogs.GoodCategories
{
    public class CreateUpdateGoodCategoryDto
    {
        [Required]
        [MaxLength(GoodCategoryConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }

    }
}

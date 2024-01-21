using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PersonalFinanceAssistant.Catalogs.Goods
{
    public class CreateUpdateGoodDto
    {
        [Required]
        [MaxLength(GoodConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
    }
}

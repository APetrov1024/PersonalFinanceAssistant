using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace PersonalFinanceAssistant.Catalogs.Goods
{
    public class GoodDto: EntityDto<int>
    {
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}

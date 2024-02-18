using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace PersonalFinanceAssistant.Catalogs.FinanceAccounts
{
    public class FinanceAccountDto : EntityDto<int>
    {
        public string Name { get; set; } = string.Empty;
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public bool IsDefault { get; set; }
    }
}

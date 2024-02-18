using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceAssistant.Catalogs.FinanceAccounts
{
    public class CreateUpdateFinanceAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public int CurrencyId { get; set; }
        public bool IsDefault { get; set; }
    }
}

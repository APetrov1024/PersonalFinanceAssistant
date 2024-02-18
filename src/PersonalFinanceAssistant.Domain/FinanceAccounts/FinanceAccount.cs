using PersonalFinanceAssistant.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace PersonalFinanceAssistant.FinanceAccounts
{
    public class FinanceAccount: FullAuditedEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public Guid OwnerId { get; set; }
        public IdentityUser? Owner { get; set; }
        public bool IsDefault { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}

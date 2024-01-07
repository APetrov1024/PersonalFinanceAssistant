using PersonalFinanceAssistant.FinanceAccounts;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public class FinanceOperation: FullAuditedEntity<int>
    {
        public decimal Value { get; set; }
        public int FinanceAccountId { get; set; }
        public FinanceAccount? FinanceAccount { get; set; }
        public Guid OwnerId { get; set; }
        public IdentityUser? Owner { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int? GoodId { get; set; }
        public Good? Good { get; set; }
        public decimal? Quantity { get; set; }
    }
}

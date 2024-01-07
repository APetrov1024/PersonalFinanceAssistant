using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace PersonalFinanceAssistant.Goods
{
    public class Good: FullAuditedEntity<int>
    {
        public Guid OwnerId { get; set; }
        public IdentityUser? Owner { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public GoodCategory? Category { get; set; }
    }
}

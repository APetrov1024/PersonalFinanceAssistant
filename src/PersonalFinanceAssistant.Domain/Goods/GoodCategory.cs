using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace PersonalFinanceAssistant.Goods
{
    public class GoodCategory: FullAuditedEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public GoodCategory? ParentCategory { get; set; }
        public List<GoodCategory> ChildCategories { get; set; } = new List<GoodCategory>();
        public List<Good> Goods { get; set; } = new List<Good>();
        public Guid OwnerId { get; set; }
        public IdentityUser? Owner { get; set; }
    }
}

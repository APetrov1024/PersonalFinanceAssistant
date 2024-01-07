using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Goods
{
    public interface IGoodCategoriesRepository : IPersonalFinanceAssistantRepository<GoodCategory, int>
    {
    }
}

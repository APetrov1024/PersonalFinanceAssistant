using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.FinanceAccounts
{
    public interface IFinanceAccountsRepository : IPersonalFinanceAssistantRepository<FinanceAccount, int>
    {
    }
}

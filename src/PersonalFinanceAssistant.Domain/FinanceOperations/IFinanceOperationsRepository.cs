using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public interface IFinanceOperationsRepository : IPersonalFinanceAssistantRepository<FinanceOperation, int>
    {
    }
}

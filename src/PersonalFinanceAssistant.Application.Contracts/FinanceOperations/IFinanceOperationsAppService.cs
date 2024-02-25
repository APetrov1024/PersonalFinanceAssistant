using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public interface IFinanceOperationsAppService
    {
        Task<FinanceOperationDto> GetAsync(int id);
        Task<List<FinanceOperationDto>> GetListAsync(string requestJson);
    }
}

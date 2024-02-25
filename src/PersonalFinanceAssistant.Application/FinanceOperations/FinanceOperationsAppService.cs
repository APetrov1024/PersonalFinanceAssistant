using PersonalFinanceAssistant.CommonDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Json;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public class FinanceOperationsAppService: PersonalFinanceAssistantAppService, IFinanceOperationsAppService
    {
        private readonly IFinanceOperationsRepository _financeOperationsRepository;
        private readonly IDataFilter _dataFilter;
        private readonly IJsonSerializer _jsonSerializer;
        
        public FinanceOperationsAppService(
            IFinanceOperationsRepository financeOperationsRepository,
            IDataFilter dataFilter,
            IJsonSerializer jsonSerializer) 
        { 
            _financeOperationsRepository = financeOperationsRepository;
            _dataFilter = dataFilter;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<FinanceOperationDto> GetAsync(int id)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            { 
                var operation = await _financeOperationsRepository.GetAsync(id, withDetails:true, noTracking: true);
                return ObjectMapper.Map<FinanceOperation, FinanceOperationDto>(operation);
            }
        }

        public async Task<List<FinanceOperationDto>> GetListAsync(string requestJson)
        {
            var request = _jsonSerializer.Deserialize<FilteredResultRequestDto>(requestJson);
            using (_dataFilter.Disable<ISoftDelete>()) 
            {
                var baseQuery = (await _financeOperationsRepository.GetQueryableAsync(withDetails: true, noTracking: true))
                    .Where(x => x.IsDeleted == false);
                var filteredQuery = FilterBy(request.Filters, baseQuery);
                var sortedQuery = filteredQuery.OrderBy(x => x.CreationTime);
                var entities = await AsyncExecuter.ToListAsync(filteredQuery);
                return ObjectMapper.Map<List<FinanceOperation>, List<FinanceOperationDto>>(entities);
            }
        }
    }
}

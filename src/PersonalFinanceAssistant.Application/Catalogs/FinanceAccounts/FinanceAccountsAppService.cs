using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceAssistant.FinanceAccounts;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace PersonalFinanceAssistant.Catalogs.FinanceAccounts
{
    public class FinanceAccountsAppService : PersonalFinanceAssistantAppService, IFinanceAccountsAppService
    {
        private readonly IFinanceAccountsRepository _financeAccountsRepository;

        public FinanceAccountsAppService(IFinanceAccountsRepository financeAccountsRepository)
        {
            _financeAccountsRepository = financeAccountsRepository;
        }

        public async Task<FinanceAccountDto> GetAsync(int id)
        {
            var account = await _financeAccountsRepository.GetAsync(id, withDetails: true, noTracking: true);
            return ObjectMapper.Map<FinanceAccount, FinanceAccountDto>(account);
        }

        public async Task<List<FinanceAccountDto>> GetListAsync()
        {
            var accounts = await _financeAccountsRepository.GetListAsync(withDetails: true, noTracking: true);
            return ObjectMapper.Map<List<FinanceAccount>, List<FinanceAccountDto>>(accounts);
        }

        public async Task<FinanceAccountDto> CreateAsync(CreateUpdateFinanceAccountDto dto)
        {
            ValidateCreateUpdateDto(dto);
            var account = ObjectMapper.Map<CreateUpdateFinanceAccountDto, FinanceAccount>(dto);
            account.OwnerId = CurrentUser.Id.Value;
            account = await _financeAccountsRepository.InsertAsync(account, autoSave: true);
            return ObjectMapper.Map<FinanceAccount, FinanceAccountDto>(account);
        }

        public async Task<FinanceAccountDto> UpdateAsync(int id, CreateUpdateFinanceAccountDto dto)
        {
            ValidateCreateUpdateDto(dto);
            var account = await _financeAccountsRepository.GetAsync(id, withDetails: true);
            if (account.CurrencyId != dto.CurrencyId) throw new UserFriendlyException("Изменение валюты счета пока не поддерживается");
            ObjectMapper.Map<FinanceAccount, FinanceAccountDto>(account);
            await _financeAccountsRepository.UpdateAsync(account);
            return ObjectMapper.Map<FinanceAccount, FinanceAccountDto>(account);
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _financeAccountsRepository.GetAsync(id, notFoundException: false);
            if (account != null)
            {
                if (account.Value != 0) throw new UserFriendlyException("Нельзя удалить счет на котором остались деньги. Сначала переведите деньги на другой счет.");
                var count = await _financeAccountsRepository.CountAsync();
                if (count == 1) throw new UserFriendlyException("У вас должен быть как минимум один счет");
                await _financeAccountsRepository.DeleteAsync(account);
            }
        }

        private void ValidateCreateUpdateDto(CreateUpdateFinanceAccountDto dto)
        {
            if (dto.Name.IsNullOrWhiteSpace()) throw new UserFriendlyException("Имя не должно быть пустым");
        }

    }
}

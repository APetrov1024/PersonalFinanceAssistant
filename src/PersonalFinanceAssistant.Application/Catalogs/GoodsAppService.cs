using PersonalFinanceAssistant.Catalogs.Goods;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace PersonalFinanceAssistant.Catalogs
{
    public class GoodsAppService: PersonalFinanceAssistantAppService, IGoodsAppService
    {
        private readonly IGoodsRepository _goodsRepository;

        public GoodsAppService(
            IGoodsRepository goodsRepository
            )
        {
            _goodsRepository = goodsRepository;
        }

        public async Task<GoodDto> GetAsync(int id)
        {
            var good = await _goodsRepository.GetAsync(id, withDetails: true, noTracking: true);
            return ObjectMapper.Map<Good, GoodDto>(good);
        }

        public async Task<List<GoodDto>> GetListAsync(GoodsListRequestDto dto)
        {
            var query = (await _goodsRepository.GetQueryableAsync(withDetails: true, noTracking: true))
                .Where(x => x.CategoryId == dto.CategoryId);
            var goods = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Good>, List<GoodDto>>(goods);
        }

        public async Task<GoodDto> CreateAsync(CreateUpdateGoodDto dto)
        {
            ValidateCreateUpdateDto(dto);
            var good = ObjectMapper.Map<CreateUpdateGoodDto, Good>(dto);
            good.OwnerId = CurrentUser.Id.Value;
            good = await _goodsRepository.InsertAsync(good, autoSave: true);
            return ObjectMapper.Map<Good, GoodDto>(good);
        }

        public async Task<GoodDto> UpdateAsync(int id, CreateUpdateGoodDto dto)
        {
            ValidateCreateUpdateDto(dto);
            var good = await _goodsRepository.GetAsync(id, withDetails: true);
            ObjectMapper.Map<CreateUpdateGoodDto, Good>(dto, good);
            await _goodsRepository.UpdateAsync(good);
            return ObjectMapper.Map<Good, GoodDto>(good);
        }

        public async Task DeleteAsync(int id)
        { 
            await _goodsRepository.DeleteAsync(id);
        }

        private void ValidateCreateUpdateDto(CreateUpdateGoodDto dto)
        {
            if (dto.Name.IsNullOrWhiteSpace()) throw new UserFriendlyException("Имя не должно быть пустым");
        }
    }
}

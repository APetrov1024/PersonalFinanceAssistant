using PersonalFinanceAssistant.Catalogs.GoodCategories;
using PersonalFinanceAssistant.Goods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace PersonalFinanceAssistant.Catalogs
{
    public class GoodCategoriesAppService : PersonalFinanceAssistantAppService, IGoodCategoriesAppService
    {
        private readonly IGoodCategoriesRepository _goodCategoriesRepository;
        public GoodCategoriesAppService(
            IGoodCategoriesRepository goodCategoriesRepository) 
        { 
            _goodCategoriesRepository = goodCategoriesRepository;
        }

        public async Task<GoodCategoryDto> GetAsync(int id)
        { 
            var category = await _goodCategoriesRepository.GetAsync(id, withDetails:false, noTracking: true);
            return ObjectMapper.Map<GoodCategory, GoodCategoryDto>(category);
        }

        public async Task<List<GoodCategoryDto>> GetListAsync(GoodCategoriesListRequestDto dto)
        {
            var query = (await _goodCategoriesRepository.GetQueryableAsync(withDetails: false, noTracking: true))
                .Where(x => x.ParentCategoryId == dto.ParentId);
            var categories = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<GoodCategory>, List<GoodCategoryDto>>(categories);
        }

        public async Task<GoodCategoryDto> CreateAsync(CreateUpdateGoodCategoryDto dto)
        { 
            ValidateCreateUpdateDto(dto);
            var category = ObjectMapper.Map<CreateUpdateGoodCategoryDto, GoodCategory>(dto);
            category.OwnerId = CurrentUser.Id.Value;
            category = await _goodCategoriesRepository.InsertAsync(category);
            return ObjectMapper.Map<GoodCategory, GoodCategoryDto>(category);
        }

        public async Task<GoodCategoryDto> UpdateAsync(int id, CreateUpdateGoodCategoryDto dto)
        {
            ValidateCreateUpdateDto(dto);
            var category = await _goodCategoriesRepository.GetAsync(id, withDetails: true);
            ObjectMapper.Map<CreateUpdateGoodCategoryDto, GoodCategory>(dto, category);
            await _goodCategoriesRepository.UpdateAsync(category);
            return ObjectMapper.Map<GoodCategory, GoodCategoryDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _goodCategoriesRepository.GetAsync(id, withDetails: true, noTracking: true, notFoundException: false);
            if (category == null) return;
            if (category.Goods.Count > 0) throw new UserFriendlyException("Нельзя удалить категорию, т.к. она содержит товары");
            if (category.ChildCategories.Count > 0) throw new UserFriendlyException("Нельзя удалить категорию, т.к. она содержит подкатегории");
            await _goodCategoriesRepository.DeleteAsync(id);
        }

        private void ValidateCreateUpdateDto(CreateUpdateGoodCategoryDto dto)
        {
            if (dto.Name.IsNullOrWhiteSpace()) throw new UserFriendlyException("Имя не должно быть пустым");
        }
    }
}

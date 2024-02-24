using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalFinanceAssistant.CommonDtos;

namespace PersonalFinanceAssistant.Web;

public class PersonalFinanceAssistantWebAutoMapperProfile : Profile
{
    public PersonalFinanceAssistantWebAutoMapperProfile()
    {
        CreateMap<SelectListItemDto<int>, SelectListItem>()
            .ForMember(x => x.Value, map => map.MapFrom(src => src.Value.ToString()));
    }
}

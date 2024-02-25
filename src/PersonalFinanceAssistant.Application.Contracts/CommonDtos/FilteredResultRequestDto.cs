using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceAssistant.CommonDtos
{
    public class FilteredResultRequestDto
    {
        public List<FilterDto> Filters { get; set; }
    }
}

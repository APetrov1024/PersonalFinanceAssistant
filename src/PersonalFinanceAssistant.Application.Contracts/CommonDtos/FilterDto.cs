using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceAssistant.CommonDtos
{
    public class FilterDto
    {
        public string Field { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public object Value { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceAssistant.CommonDtos
{
    public class SelectListItemDto<T>
    {
        public T? Value { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalFinanceAssistant.FinanceOperations
{
    public class FinanceOperationDto
    {
        public decimal Value { get; set; }
        public int FinanceAccountId { get; set; }
        public string FinanceAccountName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int? GoodId { get; set; }
        public string GoodName { get; set;} = string.Empty;
        public decimal? Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}

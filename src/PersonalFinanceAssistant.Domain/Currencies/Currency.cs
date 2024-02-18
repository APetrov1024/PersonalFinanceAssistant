using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace PersonalFinanceAssistant.Currencies
{
    public class Currency: Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Alpha3Code { get; set; } = string.Empty;

        [NotMapped]
        public string DisplayName 
        {
            get => $"{Name} ({Alpha3Code})";
        }
    }
}

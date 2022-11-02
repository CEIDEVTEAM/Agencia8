using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Concept
{
    public class ConceptCompleteDTO
    {
        public decimal Id { get; set; }
        public decimal Value { get; set; }
        public decimal ParamId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal PeriodId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

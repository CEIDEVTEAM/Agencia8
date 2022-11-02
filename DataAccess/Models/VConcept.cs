using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class VConcept
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

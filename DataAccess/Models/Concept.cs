using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Concept
    {
        public decimal Id { get; set; }
        public decimal ParamId { get; set; }
        public decimal Value { get; set; }
        public decimal PeriodId { get; set; }
        public DateTime AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual ProjectionParam Param { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}

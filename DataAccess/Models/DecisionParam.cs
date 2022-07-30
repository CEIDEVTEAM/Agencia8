using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DecisionParam
    {
        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string ActiveFlag { get; set; } = null!;
        public DateTime? UpdRow { get; set; }
    }
}

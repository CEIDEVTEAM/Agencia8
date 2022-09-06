using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DecisionSupport
    {
        public decimal Id { get; set; }
        public string? RecomendedDecision { get; set; }
        public string? Description { get; set; }
        public DateTime? AddRow { get; set; }

        public virtual Candidate IdNavigation { get; set; } = null!;

    }
}

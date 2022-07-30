using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DecisionSupport
    {
        public DecisionSupport()
        {
            Candidates = new HashSet<Candidate>();
        }

        public decimal Id { get; set; }
        public DateTime? Date { get; set; }
        public string? RecomendedDecision { get; set; }
        public string? Description { get; set; }
        public DateTime? AddRow { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ProcedureStep
    {
        public decimal Id { get; set; }
        public string StepType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal IdCandidate { get; set; }
        public decimal UpdUser { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual Candidate IdNavigation { get; set; } = null!;
    }
}

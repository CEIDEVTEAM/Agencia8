using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ProcedureStep
    {
        public decimal Id { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } = null!;
        public string IsComplete { get; set; } = null!;
        public decimal IdCandidate { get; set; }
        public decimal IdUser { get; set; }
        public decimal IdStepType { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual User Id1 { get; set; } = null!;
        public virtual Candidate IdNavigation { get; set; } = null!;
        public virtual StepType IdStepTypeNavigation { get; set; } = null!;
    }
}

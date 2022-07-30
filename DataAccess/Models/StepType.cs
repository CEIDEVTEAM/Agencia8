using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class StepType
    {
        public StepType()
        {
            ProcedureSteps = new HashSet<ProcedureStep>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string AactiveFlag { get; set; } = null!;
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual ICollection<ProcedureStep> ProcedureSteps { get; set; }
    }
}

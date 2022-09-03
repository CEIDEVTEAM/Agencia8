using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class ProcedureStepDTO
    {
        public string StepType { get; set; }
        public string Description { get; set; }
        public decimal IdCandidate { get; set; }
        public decimal UpdUser { get; set; }
        public string AddRow { get; set; }
    }
}

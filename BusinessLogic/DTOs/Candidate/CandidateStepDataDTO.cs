using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class CandidateStepDataDTO
    {
        public List<ProcedureStepDTO> colProcedureStep { get; set; }
        public List<stepType> colStepTypes { get; set; }
        public CandidateCreationFrontDTO candidate { get; set; }

        public class stepType
        {
            public string id { get; set; }
            public string name { get; set; }
        }

    }
}

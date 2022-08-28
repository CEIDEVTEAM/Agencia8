using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string PersonalDocument { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PersonalAddress { get; set; }
        public string Phone { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public decimal? Number { get; set; }
        public decimal? IdDecisionSupport { get; set; }
    }
}

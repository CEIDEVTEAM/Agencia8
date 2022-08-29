using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class CandidateCreationFrontDTO
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string personalDocument { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string personalAddress { get; set; }
        public string phone { get; set; }
        public string condition { get; set; }
        public string status { get; set; }
        public decimal? number { get; set; }
        public decimal? idDecisionSupport { get; set; }


        //Contact person
        public string cpName { get; set; }
        public string cpLastName { get; set; }
        public string cpPhone { get; set; }
        public string bond { get; set; }

        //Shop data
        public string? cName { get; set; }
        public string? cPhone { get; set; }
        public string? address { get; set; }
        public string? neighborhood { get; set; }
        public string? shopType { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
    }
}

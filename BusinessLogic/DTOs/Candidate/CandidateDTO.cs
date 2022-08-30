using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class CandidateDTO
    {
  
        public decimal Id { get; set; }
        public string name { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public DateTime birthDate { get; set; }
        public string personalDocument { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public string personalAddress { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string status { get; set; } = null!;
        public DateTime? addRow { get; set; }
        public decimal? number { get; set; }
        public decimal? idShopData { get; set; }
        public string? cName { get; set; }
        public string? cPhone { get; set; }
        public string? address { get; set; }
        public string? neighborhood { get; set; }
        public string? shopType { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string cpName { get; set; } = null!;
        public string cpLastName { get; set; } = null!;
        public string cpPhone { get; set; } = null!;
        public string bond { get; set; } = null!;
    }
}

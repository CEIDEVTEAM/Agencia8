using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class DesitionSupportDTO
    {
        public decimal Id { get; set; }
        public DateTime? Date { get; set; }
        public string? RecomendedDecision { get; set; }
        public string? Description { get; set; }
        public DateTime? AddRow { get; set; }
    }
}

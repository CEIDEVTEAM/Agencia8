using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Period
{
    public class PeriodDTO
    {
        public decimal Id { get; set; }
        public string Description { get; set; } = null!;
        public string ReferenceDate { get; set; }
        public string? ActiveFlag { get; set; } = null!;
        public DateTime AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

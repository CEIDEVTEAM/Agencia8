using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class ExternalDependentDTO
    {
        public decimal Number { get; set; }
        public decimal? AgencyNumber { get; set; }
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Neighborhood { get; set; }
        public string? Address { get; set; } = null!;
        public string? Condition { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ExternalDependent
    {
        public decimal Number { get; set; }
        public decimal AgencyNumber { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Neighborhood { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

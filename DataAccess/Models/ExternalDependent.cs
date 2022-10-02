using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ExternalDependent
    {
        public decimal Number { get; set; }
        public decimal? AgencyNumber { get; set; }
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Neighborhood { get; set; }
        public string Address { get; set; } = null!;
        public string? Condition { get; set; } 
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
        public decimal Id { get; set; }
        public string ActiveFlag { get; set; }
    }
}

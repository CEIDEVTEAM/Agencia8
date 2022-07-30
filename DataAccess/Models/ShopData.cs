using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ShopData
    {
        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string Address { get; set; } = null!;
        public string Neighborhood { get; set; } = null!;
        public string ShopType { get; set; } = null!;
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? NumberDependent { get; set; }
        public decimal? IdCandidate { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual Candidate? IdCandidateNavigation { get; set; }
        public virtual Dependet? NumberDependentNavigation { get; set; }
    }
}

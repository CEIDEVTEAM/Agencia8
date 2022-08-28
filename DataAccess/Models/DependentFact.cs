using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class DependentFact
    {
        public decimal Id { get; set; }
        public decimal? IdDependent { get; set; }
        public string? FactType { get; set; }
        public string? Description { get; set; }
        public decimal? UpdUserId { get; set; }
        public DateTime? AddRow { get; set; }

        public virtual Dependent? IdDependentNavigation { get; set; }
    }
}

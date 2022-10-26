using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ProjectionParam
    {
        public ProjectionParam()
        {
            Concepts = new HashSet<Concept>();
        }

        public decimal Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Usage { get; set; }
        public decimal? ActualDefaultValue { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
    }
}

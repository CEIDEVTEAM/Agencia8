using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Period
    {
        public Period()
        {
            Concepts = new HashSet<Concept>();
            Raspadita = new HashSet<Raspadita>();
        }

        public decimal Id { get; set; }
        public string Description { get; set; } = null!;
        public DateTime ReferenceDate { get; set; }
        public string ActiveFlag { get; set; } = null!;
        public DateTime AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
        public virtual ICollection<Raspadita> Raspadita { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class ContactPerson
    {
        public decimal Number { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Bond { get; set; } = null!;
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual Dependet NumberNavigation { get; set; } = null!;
    }
}

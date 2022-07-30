using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Dependet
    {
        public Dependet()
        {
            ShopData = new HashSet<ShopData>();
        }

        public decimal Number { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string PersonalDocument { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public string PersonalAddress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public decimal PatentNamber { get; set; }
        public decimal? UpdUserId { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual ContactPerson ContactPerson { get; set; } = null!;
        public virtual ICollection<ShopData> ShopData { get; set; }
    }
}

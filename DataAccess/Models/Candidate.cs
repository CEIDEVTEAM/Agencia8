using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            ShopData = new HashSet<ShopData>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string PersonalDocument { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public string PersonalAddress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Condition { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal? Number { get; set; }
        public decimal? IdDecisionSupport { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual DecisionSupport? IdDecisionSupportNavigation { get; set; }
        public virtual ContactPerson ContactPerson { get; set; } = null!;
        public virtual ProcedureStep ProcedureStep { get; set; } = null!;
        public virtual ICollection<ShopData> ShopData { get; set; }
    }
}

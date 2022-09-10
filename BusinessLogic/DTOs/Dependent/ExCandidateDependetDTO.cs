using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class ExCandidateDependetDTO
    {
        public decimal Id { get; set; }
        public string Type { get; set; } = null!;
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
        public string? AddRow { get; set; }
        public decimal? IdShopData { get; set; }
        public string? NameShopData { get; set; }
        public string? PhoneShopData { get; set; }
        public string? Address { get; set; }
        public string? Neighborhood { get; set; }
        public string? ShopType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal IdContactPerson { get; set; }
        public string NameContactPerson { get; set; } = null!;
        public string LastNameContactPerson { get; set; } = null!;
        public string PhoneContactPerson { get; set; } = null!;
        public string BondContactPerson { get; set; } = null!;
    }
}

using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.ShopData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class DependentCreationFrontDTO
    {
        public decimal? Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDate { get; set; }
        public string PersonalDocument { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; } = null!;
        public string PersonalAddress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime? AddRow { get; set; }
        public decimal Number { get; set; }
        public decimal? PatentNamber { get; set; }
        public string Condition { get; set; }
        public decimal? IdShopData { get; set; }
        public string? NameShopData { get; set; }
        public string? PhoneShopData { get; set; }
        public string? Address { get; set; }
        public string? Neighborhood { get; set; }
        public string? ShopType { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string NameContactPerson { get; set; } 
        public string LastNameContactPerson { get; set; } 
        public string PhoneContactPerson { get; set; }
        public string Bond { get; set; } 
        public decimal IdContactPerson { get; set; }

    }
}

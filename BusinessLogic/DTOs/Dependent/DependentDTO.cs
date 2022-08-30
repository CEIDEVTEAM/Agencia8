using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class DependentDTO
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalDocument { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string PersonalAddress { get; set; }
        public string Phone { get; set; }
        public DateTime? AddRow { get; set; }
        public decimal Number { get; set; }
        public decimal? PatentNamber { get; set; }
        public decimal? IdShopData { get; set; }
        public string? NameShopData { get; set; }
        public string? PhoneShopData { get; set; }
        public string? Address { get; set; }
        public string? Neighborhood { get; set; }
        public string? ShopType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string NameContactPerson { get; set; }
        public string LastNameContactPerson { get; set; }
        public string PhoneContactPerson { get; set; }
        public string Bond { get; set; }
    }
}

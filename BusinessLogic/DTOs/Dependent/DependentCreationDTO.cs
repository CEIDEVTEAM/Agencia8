using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.ShopData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class DependentCreationDTO
    {
        public decimal Id { get; set; }

        public decimal Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalDocument { get; set; }
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; }
        public string PersonalAddress { get; set; }
        public string Phone { get; set; }
        public string Condition { get; set; }
        public decimal? PatentNamber { get; set; }

        public ContactPersonCreationDTO ContactPerson { get; set; }
        public ShopDataCreationDTO ShopData { get; set; }

    }
}

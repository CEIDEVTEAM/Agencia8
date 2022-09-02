using BusinessLogic.DTOs.ContactPerson;
using BusinessLogic.DTOs.ShopData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class DependentFactCreationFrontDTO
    {
        public decimal? IdDependent { get; set; }
        public string? FactType { get; set; }
        public string? Description { get; set; }
        public decimal? UpdUserId { get; set; }

    }
}

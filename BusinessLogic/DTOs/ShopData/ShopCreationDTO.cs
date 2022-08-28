using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.ShopData
{
    public class ShopDataCreationDTO
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string ShopType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? NumberDependent { get; set; }
        public decimal? IdCandidate { get; set; }
        public decimal? IdDependent { get; set; }

    }
}

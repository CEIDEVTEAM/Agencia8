using BusinessLogic.DTOs.Generals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Candidate
{
    public class RecomendedDecisionDTO
    {

        public string Neighborhood { get; set; }
        public string NeighborhoodPotential { get; set; }
        public string RecomendedDecision { get; set; }
        public string Description { get; set; }
        public int AgencyShops { get; set; }
        public int ExternalAgencyShops { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<DistanceResponseDTO> ShopCoordinates { get; set; }
    }
}

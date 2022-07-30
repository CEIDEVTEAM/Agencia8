using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class LtShopData
    {
        public decimal Id { get; set; }
        public decimal? IdShopData { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Neighborhood { get; set; }
        public string? ShopType { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? NumberDependent { get; set; }
        public decimal? IdCandidate { get; set; }
        public DateTime? AddRow { get; set; }
        public string? Action { get; set; }
        public decimal? IdUser { get; set; }
    }
}

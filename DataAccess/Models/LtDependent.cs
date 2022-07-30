using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class LtDependent
    {
        public decimal Id { get; set; }
        public decimal? Number { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PersonalDocument { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? PersonalAddress { get; set; }
        public string? Phone { get; set; }
        public string? Condition { get; set; }
        public decimal? PatentNamber { get; set; }
        public DateTime? AddRow { get; set; }
        public string? Action { get; set; }
        public decimal? IdUser { get; set; }
    }
}

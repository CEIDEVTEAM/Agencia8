using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class LtAuthentication
    {
        public decimal Id { get; set; }
        public decimal UserId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime? AddRow { get; set; }
    }
}

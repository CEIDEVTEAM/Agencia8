using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class VUser
    {
        public decimal Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
        public string? RoleName { get; set; }
    }
}

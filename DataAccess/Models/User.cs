using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public decimal Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal? IdRole { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }

        public virtual Role? IdRoleNavigation { get; set; }
        public virtual ProcedureStep ProcedureStep { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PermissionRole
    {
        public decimal IdRole { get; set; }
        public decimal IdPermission { get; set; }
        public DateTime AddRow { get; set; }

        public virtual Role IdRole1 { get; set; } = null!;
        public virtual Permission IdRoleNavigation { get; set; } = null!;
    }
}

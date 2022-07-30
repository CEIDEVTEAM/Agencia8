using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Permission
    {
        public Permission()
        {
            PermissionRoles = new HashSet<PermissionRole>();
        }

        public decimal Id { get; set; }
        public string Feature { get; set; } = null!;
        public DateTime AddRow { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}

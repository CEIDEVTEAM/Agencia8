using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            PermissionRoles = new HashSet<PermissionRole>();
            Users = new HashSet<User>();
        }

        public decimal Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? AddRow { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}

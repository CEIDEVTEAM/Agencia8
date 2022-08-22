using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class UserDTO
    {
        public decimal Id { get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal? IdRole { get; set; }
        public string? AddRow { get; set; }
        public string? UpdRow { get; set; }
        public string? ActiveFlag { get; set; }

        public List<string> Resources { get; set; } = null;
        public string RoleName { get; set; } = null;

    }
}

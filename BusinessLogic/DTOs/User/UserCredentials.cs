using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.User
{
    public class UserCredentials
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

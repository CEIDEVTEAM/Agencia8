using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.ContactPerson
{
    public class ContactPersonCreationDTO
    {
        public decimal Id { get; set; }
        public decimal? IdDependent { get; set; }
        public decimal? IdCandidate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Bond { get; set; }
    }
}

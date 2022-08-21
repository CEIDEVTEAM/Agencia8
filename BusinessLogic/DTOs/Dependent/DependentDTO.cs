using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Dependent
{
    public class DependentDTO
    {
        public decimal Number { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalDocument { get; set; }
        public string Gender { get; set; } = null!;
        public string MaritalStatus { get; set; }
        public string PersonalAddress { get; set; }
        public string Phone { get; set; }
        public string Condition { get; set; }
        public decimal PatentNamber { get; set; }
    }
}

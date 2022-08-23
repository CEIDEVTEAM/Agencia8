using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.DecisionParam
{
    public class DecisionParamDTO
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? UpdRow { get; set; }

    }
}

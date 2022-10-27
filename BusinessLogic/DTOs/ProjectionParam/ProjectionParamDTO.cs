using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.ProjectionParam
{
    public class ProjectionParamDTO
    {
        public decimal Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Usage { get; set; }
        public decimal? ActualDefaultValue { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

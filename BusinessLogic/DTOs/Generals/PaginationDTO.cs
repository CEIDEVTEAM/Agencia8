using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Generals
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 50;
        
        public string Search { get; set; } = "";
        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}

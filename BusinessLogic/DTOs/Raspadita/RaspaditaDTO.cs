using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.Raspadita
{
    public class RaspaditaDTO
    {
        public decimal Id { get; set; }
        public string? Agencia { get; set; }
        public decimal? Aciertos { get; set; }
        public decimal? Apuestas { get; set; }
        public decimal? Utilidad { get; set; }
        public decimal? Partida { get; set; }
        public decimal? PeriodId { get; set; }
        public DateTime? AddRow { get; set; }
        public DateTime? UpdRow { get; set; }
    }
}

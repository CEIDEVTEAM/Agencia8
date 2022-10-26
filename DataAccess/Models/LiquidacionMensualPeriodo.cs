using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class LiquidacionMensualPeriodo
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Agencia { get; set; }
        public string? Juego { get; set; }
        public decimal? ApuestasVespertinas { get; set; }
        public decimal? ApuestasNocturnas { get; set; }
        public decimal? AciertosVespertinos { get; set; }
        public decimal? AciertosNocturnos { get; set; }
        public decimal? Aportes { get; set; }
        public decimal? IdPeriodo { get; set; }

        public virtual Period? IdPeriodoNavigation { get; set; }
    }
}

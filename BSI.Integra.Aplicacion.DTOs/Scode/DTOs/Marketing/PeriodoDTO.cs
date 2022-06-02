using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PeriodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicialFinanzas { get; set; }
        public DateTime FechaFinFinanzas { get; set; }
        public DateTime? FechaInicialRepIngresos { get; set; }
        public DateTime? FechaFinRepIngresos { get; set; }
        public string NombreUsuario { get; set; }
    }
}

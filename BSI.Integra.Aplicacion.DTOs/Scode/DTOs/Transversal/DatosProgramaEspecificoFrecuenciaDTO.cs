using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosProgramaEspecificoFrecuenciaDTO
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Frecuencia { get; set; }
        public int NroSesiones { get; set; }
        public int? IdFrecuencia { get; set; }
    }
}

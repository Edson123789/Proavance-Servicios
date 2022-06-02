using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltrosRegistrarOportunidadDTO
    {
        public string CentrosCosto { get; set; }
        public string Asesores { get; set; }
        public string TiposDato { get; set; }
        public string Origenes { get; set; }
        public string FasesOportunidad { get; set; }       
        public string contacto { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
    }
}

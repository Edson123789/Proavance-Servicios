using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConsultaRucDTO
    {
        public string RUC { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string Condicion { get; set; }
        public string Estado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaInscripcion { get; set; }
        public string TipoContribuyente { get; set; }
        public string Actividad { get; set; }
    }
}

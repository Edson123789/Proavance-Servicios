using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActualizarFechaPorSesionDTO
    {
        public int SesionId { get; set; }
        public DateTime fecha { get; set; }
        public string Comentario { get; set; }
        public bool recorrerFecha { get; set; }
        public bool esFechaInicio { get; set; }
        public string Usuario { get; set; }

    }
}

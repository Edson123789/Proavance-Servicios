using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RaListadoMinimoCursoTrabajoAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoEntrega { get; set; }
        public bool Estado { get; set; }
        public DateTime? FechaEntrega { get; set; }
    }
}

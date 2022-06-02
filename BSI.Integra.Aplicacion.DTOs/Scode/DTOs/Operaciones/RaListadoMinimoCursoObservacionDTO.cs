using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RaListadoMinimoCursoObservacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Observacion { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellidos { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}

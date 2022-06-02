using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdClasificacionUbicacionDocente { get; set; }

        public string NombreUsuario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FacebookAudienciaDTO
    {
        public int IdFiltroSegmento { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Cuenta { get; set; }
        public string Pais { get; set; }
        public string Usuario { get; set; }
        public List<FacebookAudienciaDatosAlumnoDTO> Alumnos { get; set; }
        
    }
}

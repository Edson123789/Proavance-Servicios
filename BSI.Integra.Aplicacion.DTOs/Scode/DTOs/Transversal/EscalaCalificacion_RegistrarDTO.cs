using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EscalaCalificacion_RegistrarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EscalaCalificacionDetalle_RegistrarDTO> ListadoDetalle { get; set; }

        public string NombreUsuario { get; set; }
    }
}

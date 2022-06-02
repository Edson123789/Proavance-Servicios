using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class NotaAulaVirtualDTO
    {
        public AlumnoNotaAulaVirtualDTO DatoAlumno { get; set; }
        public List<DatoNotaAulaVirtualDTO> ListadoNotas { get; set; }

        public NotaAulaVirtualDTO() {
            this.ListadoNotas = new List<DatoNotaAulaVirtualDTO>();
        }
    }
}

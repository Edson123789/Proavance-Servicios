using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ControlDocumentoAlumnoDTO
    {
        public int IdControlDocAlumno { get; set; }
        public int IdCriterioCalificacion { get; set; }
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        public string Observaciones { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string NombreUsuario{ get; set; }
    }
}

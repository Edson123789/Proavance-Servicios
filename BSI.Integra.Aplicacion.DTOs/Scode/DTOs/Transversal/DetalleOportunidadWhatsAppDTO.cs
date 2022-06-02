using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetalleOportunidadWhatsAppDTO
    {
        public string NombreAlumno { get; set; }
        public string CentroCosto { get; set; }
        public string ProgramaGeneral { get; set; }
        public string Email1 { get; set; }
        public string FaseOportunidad { get; set; }
        public bool? EnSeguimiento { get; set; }
    }
}

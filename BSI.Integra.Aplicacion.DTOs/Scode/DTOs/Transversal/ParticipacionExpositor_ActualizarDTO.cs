using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ParticipacionExpositor_ActualizarDTO
    {
        public int Id { get; set; }
        public int? IdExpositorConfirmado { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
        public string Usuario { get; set; }
    }
}

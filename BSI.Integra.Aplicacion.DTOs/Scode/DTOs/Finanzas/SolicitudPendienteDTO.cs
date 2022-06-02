using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SolicitudPendienteDTO
    {
        public int Version { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime Fecha { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
        public string AprobadoPorNombre { get; set; }
        public string Cambios { get; set; }
        public string Enviados { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaCabeceraCambioAprobarDTO
    {
        public string CodigoMatricula{ get; set; }
        public string IdsCambios { get; set; }
        public int Version { get; set; }
        public string NombreSolicitante { get; set; }
        public int IdPersonalAprobador { get; set; }//quien aprueba
        public string Observacion { get; set; }
        public string Cambios { get; set; }
    }
}

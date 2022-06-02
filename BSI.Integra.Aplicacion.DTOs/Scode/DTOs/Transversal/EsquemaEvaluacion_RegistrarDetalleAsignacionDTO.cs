using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_RegistrarDetalleAsignacionDTO
    {
        public int? Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int? IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoInstrucciones { get; set; }
    }
}

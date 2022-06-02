using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialEnvioDetalleDTO
    {
        public int Id { get; set; }
        public int IdMaterialEnvio { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstadoRecepcion { get; set; }
        public int IdPersonalReceptor { get; set; }
        public int CantidadEnvio { get; set; }
        public int CantidadRecepcion { get; set; }
        public string ComentarioEnvio { get; set; }
        public string ComentarioRecepcion { get; set; }
    }
}

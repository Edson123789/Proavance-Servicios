using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialEnvioDTO
    {
        public int Id { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string NombreUsuario { get; set; }
        public List<MaterialEnvioDetalleDTO> ListaMaterialEnvioDetalle { get; set; }
    }
}

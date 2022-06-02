using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MaterialEnvioDetalleCompletoDTO
    {
        [Optional]
        public MaterialEnvioDTO MaterialEnvio { get; set; }
        public List<MaterialEnvioDetalleDTO> ListaMaterialEnvioDetalle { get; set; }
        public string NombreUsuario { get; set; }
    }
}

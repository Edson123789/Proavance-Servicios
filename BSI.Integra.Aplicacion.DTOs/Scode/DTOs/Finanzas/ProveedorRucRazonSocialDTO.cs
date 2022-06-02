using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorRucRazonSocialDTO
    {
        public int Id { get; set; }
        public string NroDocIdentidad{ get; set; }
        public string RazonSocial { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdPais { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoPartnerDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdToncalPartner { get; set; }
        public int NombrePartner { get; set; }
        public int NombreUsuario { get; set; }
    }
}

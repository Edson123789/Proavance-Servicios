using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralConfiguracionPlantillaDTO
    {
        public int Id { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int? IdPlantillaPosterior { get; set; }
        public int? IdPlantillaBase { get; set; }
        public int? IdPgeneral { get; set; }
        public DateTime? UltimaFechaRemplazarCertificado { get; set; }
        public bool? RemplazarCertificados { get; set; }
        public int Usuario { get; set; }
        public List<PgeneralConfiguracionPlantillaDetalleDTO> detalle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificacionCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPartner { get; set; }
        public int Costo { get; set; }
        public int IdMoneda { get; set; }
        public int IdCertificacionTipo { get; set; }

        public List<CertificacionPGeneralDTO> PGenerales { get; set; }
        public string Usuario { get; set; }
    }
}

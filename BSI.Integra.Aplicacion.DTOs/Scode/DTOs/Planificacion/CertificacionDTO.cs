using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPartner { get; set; }
        public int Costo { get; set; }
        public int IdMoneda { get; set; }
        public int IdCertificacionTipo { get; set; }

        
    }
}

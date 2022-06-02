using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoBrochureDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreEnCertificado { get; set; }
        public string Contenido { get; set; }
        public int TotalHoras { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public bool Activo { get; set; }
        public string NombreUsuario { get; set; }
    }
}

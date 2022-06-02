using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoPartnerComplementoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string MencionEnCertificado { get; set; }
        public string FrontalCentral { get; set; }
        public string FrontalInferiorIzquierda { get; set; }
        public string PosteriorCentral { get; set; }
        public string PosteriorInferiorIzquierda { get; set; }
        public bool Activo { get; set; }
        public string NombreUsuario { get; set; }
    }
}

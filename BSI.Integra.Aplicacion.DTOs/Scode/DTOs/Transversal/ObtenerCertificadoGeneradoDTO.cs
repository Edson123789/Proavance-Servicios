using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ObtenerCertificadoGeneradoDTO
    {
        public int Id { get; set; }
        public string NombrePlantilla { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UrlDocumento { get; set; }
    }

    public class CertificadoGeneradoAutomatico
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaEmision { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public string NombreArchivo { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdCronogramaPagoTarifario { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class NombreCertificadoDTO
    {
        public string NombreArchivo { get; set; }
    }
    
}

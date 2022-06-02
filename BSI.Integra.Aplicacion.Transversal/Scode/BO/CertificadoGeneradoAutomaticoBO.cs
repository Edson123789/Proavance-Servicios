using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoGeneradoAutomaticoBO : BaseBO
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

        public CertificadoGeneradoAutomaticoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}

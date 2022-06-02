using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoGeneradoAutomaticoContenidoBO : BaseBO
    {        
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public string NombreAlumno { get; set; }
        public string NombrePrograma { get; set; }
        public int? DuracionPespecifico { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Ciudad { get; set; }
        public int? EscalaCalificacion { get; set; }
        public string FechaInicioCapacitacion { get; set; }
        public string FechaFinCapacitacion { get; set; }
        public int? CalificacionPromedio { get; set; }
        public string FechaEmisionCertificado { get; set; }
        public int? CorrelativoConstancia { get; set; }
        public string CronogramaNota { get; set; }
        public string CronogramaAsistencia { get; set; }
        public int? IdMigracion { get; set; }
        public string EstructuraCurricular { get; set; }
        public string CodigoPartner { get; set; }
        public string CodigoQr { get; set; }

        public CertificadoGeneradoAutomaticoContenidoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}

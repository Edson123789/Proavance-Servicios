using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCertificadoGeneradoAutomaticoContenido
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string EstructuraCurricular { get; set; }
        public string CodigoPartner { get; set; }
        public string CodigoQr { get; set; }
    }
}

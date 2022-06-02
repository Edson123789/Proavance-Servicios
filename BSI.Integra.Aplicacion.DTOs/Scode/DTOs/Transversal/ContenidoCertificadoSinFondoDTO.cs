using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContenidoCertificadoSinFondoDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCertificadoGeneradoAutomatico { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public string CodigoCertificado { get; set; }
        public string NombreAlumno { get; set; }
        public string NombrePrograma { get; set; }
        public int? DuracionPespecifico { get; set; }
        public string Ciudad { get; set; }
        public int? EscalaCalificacion { get; set; }
        public string EstructuraCurricular { get; set; }
        public string CodigoPartner { get; set; }
        public string CodigoQR { get; set; }
        public string FechaInicioCapacitacion { get; set; }
        public string FechaFinCapacitacion { get; set; }
        public string FechaEmisionCertificado { get; set; }
    }
}

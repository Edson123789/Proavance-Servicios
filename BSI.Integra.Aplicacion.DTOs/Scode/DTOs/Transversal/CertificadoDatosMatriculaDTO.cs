using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CertificadoDatosMatriculaDTO
    {
        public int Id { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoCertificado { get; set; }
        public string NombrePrograma { get; set; }
        public string FechaInicioCapacitacion { get; set; }
        public string FechaFinCapacitacion { get; set; }
        public string Ciudad { get; set; }
        public int? CalificacionPromedio { get; set; }
        public int? EscalaCalificacion { get; set; }
        public int? DuracionPespecifico { get; set; }
        public string FechaEmisionCertificado { get; set; }
    }
}

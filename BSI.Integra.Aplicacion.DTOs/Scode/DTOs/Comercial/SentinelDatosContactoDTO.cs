using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelDatosContactoDTO
    {
        public string Dni { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string NombreAlterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Ubigeo { get; set; }
        public string Distrito { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Rangosueldo { get; set; }
        public string RucEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Cargo { get; set; }
        public string CIIU { get; set; }
        public string ActividadEconomica { get; set; }
        public string Direccion { get; set; }
        public string SemaforoActual { get; set; }
        public string SemaforoPrevio { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public virtual ICollection<AlumnosSentinelLineasCreditoDTO> lineaCredito { get; set; }
        public virtual ICollection<AlumnosSentinelLineasDeudaDTO> lineaDeuda { get; set; }
    }
    public class SentinelDatosCabeceraDTO
    {
        public string Mensaje { get; set; }
        public string Color { get; set; }
    }
}

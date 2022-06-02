using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadAlumnoDTO
    {
        public string DiaFechaActual { get; set; }
        public string NombreMesFechaActual { get; set; }
        public string AnioFechaActual { get; set; }
        public string MontoTotal { get; set; }
        public string DuracionMesesPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public string CronogramaPagoCompleto { get; set; }
        public string Anexo1EstructuraCurricular { get; set; }
        public string Anexo2Certificacion { get; set; }
        public string Version { get; set; }
        public int IdPEspecifico { get; set; }
        public OportunidadAlumnoDetalleDTO OportunidadAlumno { get; set; }
        private readonly DateTime FechaActual = DateTime.Now;
        public OportunidadAlumnoDTO() {
            DiaFechaActual = FechaActual.Day.ToString();
            NombreMesFechaActual = FechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
            AnioFechaActual = FechaActual.Year.ToString();
            OportunidadAlumno = new OportunidadAlumnoDetalleDTO();
        }
    }
    public class OportunidadAlumnoDetalleDTO{
        public string Nombre1{ get; set; }
        public string NombreCompleto { get; set; }
        public string NroDocumento { get; set; }
        public string Direccion { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombrePais { get; set; }
        public string Email1 { get; set; }
    }
}

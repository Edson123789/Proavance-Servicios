using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteLandingPagePortalDTO
    {
        public Guid Id { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo1 { get; set; }
        public string Fijo { get; set; }
        public string Movil { get; set; }
        public string Formacion { get; set; }
        public string Trabajo { get; set; }
        public string Cargo { get; set; }
        public string Industria { get; set; }
        public int? Pais { get; set; }
        public int? Region { get; set; }
        public string Ip { get; set; }
        public string FechaCreacion { get; set; }
        public string HoraCreacion { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Origen { get; set; }
        public string Campanha { get; set; }
        public string Proveedor { get; set; }
        public bool Procesado { get; set; }
        public string Formulario { get; set; }
        public Guid? IdCategoriaDato { get; set; }
        public Guid? IdCampania { get; set; }
        public string CategoriaDato { get; set; }
        public string EstadoOportunidad { get; set; }
    }


    public class ReporteLandingPagePortalFacebookDTO {
        public string Id { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Movil { get; set; }
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string Region { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public string Categoria { get; set; }
        public string Campania { get; set; }
        public bool? Procesado { get; set; }
        public string Formulario { get; set; }
        public string EstadoOportunidad { get; set; }
    }
}

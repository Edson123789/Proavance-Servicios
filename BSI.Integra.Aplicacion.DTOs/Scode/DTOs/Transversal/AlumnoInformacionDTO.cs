using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoInformacionDTO
    {
		public int IdClasificacionPersona { get; set; } //IdClasificacionPersona
        public int Id { get; set; } //IdAlumno
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Genero { get; set; }
        public string Parentesco { get; set; }
        public string TelefonoFamiliar { get; set; }
        public string NombreFamiliar { get; set; }
        public string Empresa { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdAFormacion { get; set; }
        public string AFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public string ATrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Industria { get; set; }
        public int? IdReferido { get; set; }
        public string Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public string Telefono2 { get; set; }
        public string Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidad_Inicial { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public int? IdEstadoContactoWhatsApp { get; set; }
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }
        public string RutaBandera { get; set; }
    }
}

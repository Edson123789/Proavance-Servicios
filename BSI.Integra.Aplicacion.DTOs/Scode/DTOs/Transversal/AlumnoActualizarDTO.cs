using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Celular { get; set; }
        public string Celular2 { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Genero { get; set; }
        public string Parentesco { get; set; }
        public string NombreFamiliar { get; set; }
        public string TelefonoFamiliar { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdAtrabajo { get; set; }
        public string Atrabajo { get; set; }
        public int? IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int? IdAformacion { get; set; }
        public string Aformacion { get; set; }
        public int? IdIndustria { get; set; }        
        public string Industria { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int? IdCodigoPais { get; set; }
    }
    public class AlumnoActualizarEmailPrincipalDTO
    {
        public int IdAlumno { get; set; }
        public string EmailAPrincipal { get; set; }
    }
}

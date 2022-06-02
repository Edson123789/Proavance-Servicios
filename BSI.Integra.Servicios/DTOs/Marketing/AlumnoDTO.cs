using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.DTOs
{
    public class AlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public int? Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NivelFormacion { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string EstadoCivil { get; set; }
        public string TelefonoFamiliar { get; set; }
        public string NombreFamiliar { get; set; }
        public string Parentesco { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string TelefonoTrabajoAnexo { get; set; }
        public string Genero { get; set; }
        public string Skype { get; set; }
        public string Fax { get; set; }
        public int? IdPais { get; set; }
        public string UbigeoPais { get; set; }
        public string UbigeoDepartamento { get; set; }
        public string UbigeoProvincia { get; set; }
        public string UbigeoCiudad { get; set; }
        public string UbigeoDistrito { get; set; }
        public string DireccionCalle { get; set; }
        public string DireccionAv { get; set; }
        public string DireccionZona { get; set; }
        public string DireccionComp { get; set; }
        public string DireccionTorre { get; set; }
        public string DireccionEdificio { get; set; }
        public string DireccionDpto { get; set; }
        public string DireccionUrb { get; set; }
        public string DireccionMz { get; set; }
        public string DireccionLt { get; set; }
        public string ReferenciaDetallada { get; set; }
        public string HoraMaxima { get; set; }
        public string Puesto { get; set; }
        public string AniversarioBodas { get; set; }
        public string NroHijo { get; set; }
        public bool? ValidacionTelefonica { get; set; }
        public string FaseContacto { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdAformacion { get; set; }
        public string Aformacion { get; set; }
        public int? IdAtrabajo { get; set; }
        public string Atrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Industria { get; set; }
        public int? IdReferido { get; set; }
        public string Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdCodigoCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        public string Telefono2 { get; set; }
        public string Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidadInicial { get; set; }
        public string UsClave { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public bool? DeSuscrito { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        //Adicionales
        public string Paquete { get; set; }
        public string IdOportunidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoPersonal { get; set; }
        public string Email { get; set; }
        public string Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public bool? Activo { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? IdSexo { get; set; }
        public int? IdEstadocivil { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdRegion { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string CiudadDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string NombreCuspp { get; set; }
        public string DistritoDireccion { get; set; }
    }
}

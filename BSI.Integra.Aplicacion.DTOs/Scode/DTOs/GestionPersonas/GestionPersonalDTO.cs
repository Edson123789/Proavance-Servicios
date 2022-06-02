using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GestionPersonalDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Area { get; set; }
        public string AsesorCoordinador { get; set; }
        public string Email { get; set; }
        public string Anexo { get; set; }
        public string Central { get; set; }
        public string Jefe { get; set; }
        public string AreaAbrev { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int IdCentral { get; set; }
        public int IdJefe { get; set; }
        public int IdArea { get; set; }
        public bool Estado { get; set; }
        public bool Activo { get; set; }
		public string Id3CX { get; set; }
		public string Password3CX { get; set; }
		public int? IdGmailCliente { get; set; }
		public string PasswordCorreo { get; set; }
        public int? UsuarioAsterisk { get; set; }
        public string ContrasenaAsterisk { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }

    public class GestionPersonalUsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveIntegra { get; set; }
        public string Email { get; set; }
        public string Anexo { get; set; }
        public int? IdCentralTelefonica { get; set; }
        public string Id3CX { get; set; }
        public string Password3CX { get; set; }
        public string Usuario { get; set; }
        public string PasswordCorreo { get; set; }
        public int IdUsuarioRol { get; set; }
    }
}

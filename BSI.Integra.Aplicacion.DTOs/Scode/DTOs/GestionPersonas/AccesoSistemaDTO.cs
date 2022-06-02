using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class AccesoSistemaDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveIntegra { get; set; }
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
        public int? IdCentral { get; set; }
        public int IdJefe { get; set; }
        public int IdArea { get; set; }
        public bool Estado { get; set; }
        public bool Activo { get; set; }
        public string Id3CX { get; set; }
        public string Password3CX { get; set; }
        public int? IdGmailCliente { get; set; }
        public string PasswordCorreo { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdUsuarioRol { get; set; }
        public string UsuarioRol { get; set; }
    }

    public class DatosPersonalUsuarioDTO
    {
        public int ID { get; set; }
        public string PassIntegra { get; set; }
        public string Anexo { get; set; }
        public string CentralTelefonica { get; set; }
        public bool? Activo { get; set; }
        public string Id3CX { get; set; }
        public string Pass3CX { get; set; }
        public string PassAplicacion { get; set; }
    }
}

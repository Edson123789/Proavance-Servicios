using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppUsuarioCredencialDTO
    {
        public int Id { get; set; }
        public int IdWhatsAppUsuario { get; set; }
        public int IdWhatsAppConfiguracion { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }

    public class WhatsAppUsuariosDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string Nombres { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }

    public class WhatsAppPersonalDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Rol { get; set; }
        public string UserName { get; set; }
    }

    public class CredencialTokenExpiraDTO
    {
        public int IdWhatsAppUsuario { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime ExpiresAfter { get; set; }
    }

    public class CredencialUsuarioLoginDTO
    {
        public int IdWhatsAppUsuario { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
    }
}

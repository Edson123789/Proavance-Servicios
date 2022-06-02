using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GestionUsuariosDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string AreaTrabajo { get; set; }
        public int RolId { get; set; }
        public int PerId { get; set; }
        public string UsClave { get; set; }
        public int IdUsuario { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
   public class IntegraAspNetUserActualizarDTO
    {
        public string UsClave { get; set; }
        public int PerId { get; set; }
        public int RolId { get; set; }
        public string AreaTrabajo { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool Estado { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalRolAreaDTO
    {
        public int IdRol { get; set; }
        public int IdArea { get; set; }
    }

    public class PersonalRolAreaTipoPersonalDTO
    {
        public int IdRol { get; set; }
        public int IdArea { get; set; }
        public string Rol { get; set; }
        public string Area { get; set; }
        public string TipoPersonal { get; set; }
    }
}

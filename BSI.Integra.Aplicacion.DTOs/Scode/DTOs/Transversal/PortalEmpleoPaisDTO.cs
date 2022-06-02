using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PortalEmpleoPaisDTO
    {
        public int Id { get; set; }
        public int? IdPortalEmpleo { get; set; }
        public int? IdPais { get; set; }
        public string Usuario { get; set; }
    }
}

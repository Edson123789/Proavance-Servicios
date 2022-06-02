using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DominioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IpPublico { get; set; }
        public string IpPrivado { get; set; }
        public string Usuario { get; set; }
    }
}

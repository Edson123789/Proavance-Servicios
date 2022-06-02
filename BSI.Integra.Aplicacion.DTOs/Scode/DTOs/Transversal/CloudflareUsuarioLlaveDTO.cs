using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CloudflareUsuarioLlaveDTO
    {
        public int Id { get; set; }
        public string AuthKey { get; set; }
        public string AuthEmail { get; set; }
        public string AccountId { get; set; }
        public int? IdPersonal { get; set; }
        public bool Activar { get; set; }
        public string Usuario { get; set; }
    }
}

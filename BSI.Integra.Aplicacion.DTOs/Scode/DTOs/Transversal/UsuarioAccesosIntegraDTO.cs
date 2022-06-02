using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class UsuarioAccesosIntegraDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string IpUsuario { get; set; }
        public string Cookie { get; set; }
        public bool Habilitado { get; set; }
    }

    public class CantidadIpAccesosIntegraDTO
    {
        public int IdAccesosIntegraLog { get; set; }
        public int Cantidad { get; set; }
        public string Fecha { get; set; }
    }
}

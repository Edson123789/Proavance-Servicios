using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MandrilClickDTO
    {
        public int IdMandril { get; set; }
        public string Ip { get; set; }
        public string Ubicacion { get; set; }
        public DateTime Ts { get; set; }
        public string UserAgent { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Url { get; set; }
    }
}

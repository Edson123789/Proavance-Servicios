using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string Ruta { get; set; }
        public string Parametros { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public string Tipo { get; set; }
        public int? IdPadre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

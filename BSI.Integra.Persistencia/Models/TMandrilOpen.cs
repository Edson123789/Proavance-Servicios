using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMandrilOpen
    {
        public int Id { get; set; }
        public int? IdMandril { get; set; }
        public string Ip { get; set; }
        public string Ubicacion { get; set; }
        public DateTime Ts { get; set; }
        public string UserAgent { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

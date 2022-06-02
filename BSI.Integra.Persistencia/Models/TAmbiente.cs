using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAmbiente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdTipoAmbiente { get; set; }
        public int Capacidad { get; set; }
        public bool Virtual { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

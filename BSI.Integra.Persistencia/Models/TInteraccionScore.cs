using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionScore
    {
        public int Id { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int? IdPespecifico { get; set; }
        public int IdCategoriaInteraccion { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int IdSubCategoriaInteraccion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

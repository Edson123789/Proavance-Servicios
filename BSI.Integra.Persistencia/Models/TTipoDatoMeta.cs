using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoDatoMeta
    {
        public int Id { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public int IdTipoDato { get; set; }
        public int Meta { get; set; }
        public int MetaGlobal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TTipoDato IdTipoDatoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoDato
    {
        public TTipoDato()
        {
            TDatoOportunidadAreaVenta = new HashSet<TDatoOportunidadAreaVenta>();
            TModeloGeneralTipoDato = new HashSet<TModeloGeneralTipoDato>();
            TTipoDatoMeta = new HashSet<TTipoDatoMeta>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TDatoOportunidadAreaVenta> TDatoOportunidadAreaVenta { get; set; }
        public virtual ICollection<TModeloGeneralTipoDato> TModeloGeneralTipoDato { get; set; }
        public virtual ICollection<TTipoDatoMeta> TTipoDatoMeta { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSubAreaCapacitacion
    {
        public TSubAreaCapacitacion()
        {
            TAsesorSubAreaCapacitacionDetalle = new HashSet<TAsesorSubAreaCapacitacionDetalle>();
            TCampaniaGeneralDetalleSubArea = new HashSet<TCampaniaGeneralDetalleSubArea>();
            TPreguntaFrecuenteSubArea = new HashSet<TPreguntaFrecuenteSubArea>();
            TSubAreaCampaniaMailingDetalle = new HashSet<TSubAreaCampaniaMailingDetalle>();
            TSubAreaParametroSeoPw = new HashSet<TSubAreaParametroSeoPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdSubArea { get; set; }
        public string DescripcionHtml { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string AliasFacebook { get; set; }

        public virtual ICollection<TAsesorSubAreaCapacitacionDetalle> TAsesorSubAreaCapacitacionDetalle { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleSubArea> TCampaniaGeneralDetalleSubArea { get; set; }
        public virtual ICollection<TPreguntaFrecuenteSubArea> TPreguntaFrecuenteSubArea { get; set; }
        public virtual ICollection<TSubAreaCampaniaMailingDetalle> TSubAreaCampaniaMailingDetalle { get; set; }
        public virtual ICollection<TSubAreaParametroSeoPw> TSubAreaParametroSeoPw { get; set; }
    }
}

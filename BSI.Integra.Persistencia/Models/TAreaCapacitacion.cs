using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAreaCapacitacion
    {
        public TAreaCapacitacion()
        {
            TAreaCampaniaMailingDetalle = new HashSet<TAreaCampaniaMailingDetalle>();
            TAreaParametroSeoPw = new HashSet<TAreaParametroSeoPw>();
            TAsesorAreaCapacitacionDetalle = new HashSet<TAsesorAreaCapacitacionDetalle>();
            TCampaniaGeneralDetalleArea = new HashSet<TCampaniaGeneralDetalleArea>();
            TPreguntaFrecuenteArea = new HashSet<TPreguntaFrecuenteArea>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImgPortada { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgPortadaAlt { get; set; }
        public string ImgSecundariaAlt { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdArea { get; set; }
        public bool EsWeb { get; set; }
        public string DescripcionHtml { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAreaCampaniaMailingDetalle> TAreaCampaniaMailingDetalle { get; set; }
        public virtual ICollection<TAreaParametroSeoPw> TAreaParametroSeoPw { get; set; }
        public virtual ICollection<TAsesorAreaCapacitacionDetalle> TAsesorAreaCapacitacionDetalle { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleArea> TCampaniaGeneralDetalleArea { get; set; }
        public virtual ICollection<TPreguntaFrecuenteArea> TPreguntaFrecuenteArea { get; set; }
    }
}

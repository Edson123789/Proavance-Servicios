using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaGeneralDetalle
    {
        public TCampaniaGeneralDetalle()
        {
            TCampaniaGeneralDetalleArea = new HashSet<TCampaniaGeneralDetalleArea>();
            TCampaniaGeneralDetallePrograma = new HashSet<TCampaniaGeneralDetallePrograma>();
            TCampaniaGeneralDetalleResponsable = new HashSet<TCampaniaGeneralDetalleResponsable>();
            TCampaniaGeneralDetalleSubArea = new HashSet<TCampaniaGeneralDetalleSubArea>();
        }

        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public bool? NoIncluyeWhatsaap { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public bool EnEjecucion { get; set; }

        public virtual TCampaniaGeneral IdCampaniaGeneralNavigation { get; set; }
        public virtual TCentroCosto IdCentroCostoNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleArea> TCampaniaGeneralDetalleArea { get; set; }
        public virtual ICollection<TCampaniaGeneralDetallePrograma> TCampaniaGeneralDetallePrograma { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleResponsable> TCampaniaGeneralDetalleResponsable { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalleSubArea> TCampaniaGeneralDetalleSubArea { get; set; }
    }
}

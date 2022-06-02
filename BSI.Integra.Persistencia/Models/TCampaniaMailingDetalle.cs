using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaMailingDetalle
    {
        public TCampaniaMailingDetalle()
        {
            TAreaCampaniaMailingDetalle = new HashSet<TAreaCampaniaMailingDetalle>();
            TCampaniaMailingDetallePrograma = new HashSet<TCampaniaMailingDetallePrograma>();
            TSubAreaCampaniaMailingDetalle = new HashSet<TSubAreaCampaniaMailingDetalle>();
        }

        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string Subject { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        public int EstadoEnvio { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Campania { get; set; }
        public string CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool? EsSubidaManual { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; }
        public virtual ICollection<TAreaCampaniaMailingDetalle> TAreaCampaniaMailingDetalle { get; set; }
        public virtual ICollection<TCampaniaMailingDetallePrograma> TCampaniaMailingDetallePrograma { get; set; }
        public virtual ICollection<TSubAreaCampaniaMailingDetalle> TSubAreaCampaniaMailingDetalle { get; set; }
    }
}

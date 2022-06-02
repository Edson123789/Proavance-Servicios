using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoLista
    {
        public TConjuntoLista()
        {
            TConjuntoListaDetalle = new HashSet<TConjuntoListaDetalle>();
            TSeguimientoPreProcesoListaWhatsApp = new HashSet<TSeguimientoPreProcesoListaWhatsApp>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int IdFiltroSegmento { get; set; }
        public byte NroListasRepeticionContacto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? ConsiderarYaEnviados { get; set; }

        public virtual ICollection<TConjuntoListaDetalle> TConjuntoListaDetalle { get; set; }
        public virtual ICollection<TSeguimientoPreProcesoListaWhatsApp> TSeguimientoPreProcesoListaWhatsApp { get; set; }
    }
}

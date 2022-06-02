using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCategoriaObjetoFiltro
    {
        public TCategoriaObjetoFiltro()
        {
            TCampaniaGeneral = new HashSet<TCampaniaGeneral>();
            TConjuntoListaDetalleValor = new HashSet<TConjuntoListaDetalleValor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreObjeto { get; set; }
        public bool EsTabla { get; set; }
        public bool AplicaConjuntoLista { get; set; }
        public bool AplicaFiltroSegmento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TCampaniaGeneral> TCampaniaGeneral { get; set; }
        public virtual ICollection<TConjuntoListaDetalleValor> TConjuntoListaDetalleValor { get; set; }
    }
}

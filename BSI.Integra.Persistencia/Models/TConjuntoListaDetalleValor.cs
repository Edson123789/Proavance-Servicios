using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoListaDetalleValor
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCategoriaObjetoFiltro IdCategoriaObjetoFiltroNavigation { get; set; }
        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; }
    }
}

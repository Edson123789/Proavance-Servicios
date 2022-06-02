using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSeguimientoPreProcesoListaWhatsApp
    {
        public int Id { get; set; }
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int IdConjuntoLista { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConjuntoLista IdConjuntoListaNavigation { get; set; }
        public virtual TEstadoSeguimientoPreProcesoListaWhatsApp IdEstadoSeguimientoPreProcesoListaWhatsAppNavigation { get; set; }
    }
}

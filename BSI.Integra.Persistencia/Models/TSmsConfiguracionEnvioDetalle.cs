using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSmsConfiguracionEnvioDetalle
    {
        public int Id { get; set; }
        public int IdSmsConfiguracionLogEjecucion { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int? ConjuntoListaNroEjecucion { get; set; }
        public string Mensaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConjuntoListaResultado IdConjuntoListaResultadoNavigation { get; set; }
        public virtual TSmsConfiguracionLogEjecucion IdSmsConfiguracionLogEjecucionNavigation { get; set; }
    }
}

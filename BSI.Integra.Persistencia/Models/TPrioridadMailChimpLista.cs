using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPrioridadMailChimpLista
    {
        public int Id { get; set; }
        public int IdCampaniaMailing { get; set; }
        public int? IdCampaniaMailingDetalle { get; set; }
        public string AsuntoLista { get; set; }
        public string Contenido { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string Alias { get; set; }
        public string Etiquetas { get; set; }
        public string IdCampaniaMailchimp { get; set; }
        public string IdListaMailchimp { get; set; }
        public bool? Enviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? NroIntentos { get; set; }
        public bool? EsSubidoCorrectamente { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? CantidadEnviadosMailChimp { get; set; }
        public int? CantidadAperturaUnica { get; set; }
        public int? CantidadReboteSuave { get; set; }
        public int? CantidadReboteDuro { get; set; }
        public int? CantidadReboteSintaxis { get; set; }
        public decimal? TasaApertura { get; set; }
        public int? CantidadClicUnico { get; set; }
        public int? CantidadTotalClic { get; set; }
        public int? CantidadReporteAbuso { get; set; }
        public int? CantidadDesuscritos { get; set; }
        public decimal? TasaClic { get; set; }

        public virtual TCampaniaMailing IdCampaniaMailingNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
    }
}

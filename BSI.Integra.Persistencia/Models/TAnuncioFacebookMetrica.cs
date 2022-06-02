using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAnuncioFacebookMetrica
    {
        public int Id { get; set; }
        public int? IdAnuncioFacebook { get; set; }
        public decimal? Gasto { get; set; }
        public int? IdMoneda { get; set; }
        public int? Impresiones { get; set; }
        public int? CantidadClicsUnicos { get; set; }
        public int? CantidadClics { get; set; }
        public int? Alcance { get; set; }
        public DateTime? FechaConsulta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? CantidadClicsEnlace { get; set; }

        public virtual TAnuncioFacebook IdAnuncioFacebookNavigation { get; set; }
        public virtual TMoneda IdMonedaNavigation { get; set; }
    }
}

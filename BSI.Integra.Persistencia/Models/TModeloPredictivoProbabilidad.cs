using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloPredictivoProbabilidad
    {
        public int Id { get; set; }
        public int IdModeloPredictivoTipo { get; set; }
        public int Tipo { get; set; }
        public int IdOportunidad { get; set; }
        public decimal Probabilidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TModeloPredictivoTipo IdModeloPredictivoTipoNavigation { get; set; }
        public virtual TOportunidad IdOportunidadNavigation { get; set; }
    }
}

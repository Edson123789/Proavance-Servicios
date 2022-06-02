using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaCabeceraCambio
    {
        public int Id { get; set; }
        public int IdCronogramaTipoModificacion { get; set; }
        public int? SolicitadoPor { get; set; }
        public int? AprobadoPor { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

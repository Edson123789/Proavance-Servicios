using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCajaPorRendirCabecera
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string Codigo { get; set; }
        public int Anho { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool EsRendido { get; set; }
        public decimal MontoDevolucion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}

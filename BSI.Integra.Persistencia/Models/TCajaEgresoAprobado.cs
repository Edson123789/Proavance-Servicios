using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCajaEgresoAprobado
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string CodigoRec { get; set; }
        public string Anho { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string Origen { get; set; }
        public DateTime FechaCreacionRegistro { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}

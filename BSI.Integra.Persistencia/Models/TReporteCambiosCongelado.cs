using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteCambiosCongelado
    {
        public int Id { get; set; }
        public int IdCronogramaPagoDetalleModLogFinal { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int? IdCronograma { get; set; }
        public DateTime? FechaCongelamiento { get; set; }
        public string CodigoMatricula { get; set; }
        public string MensajeSistema { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? FechaCambio { get; set; }
    }
}

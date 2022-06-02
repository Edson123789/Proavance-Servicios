using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CalidadLlamadaLogBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProblemaLlamada { get; set; }
        public int IdCalidadLlamada { get; set; }
        public int? IdActividadDetalle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public CalidadLlamadaLogBO() {
        }
    }
}

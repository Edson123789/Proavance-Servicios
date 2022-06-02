using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class HoraBloqueadaBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public HoraBloqueadaBO() {
        }
    }
}

using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class EstadoActividadDetalleBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public EstadoActividadDetalleBO() {
        }
    }
}

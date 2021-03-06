using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProveedorLogBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string CampoActualizado { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public ProveedorLogBO()
        {
        }

        public ProveedorLogBO(int IdProveedor, string CampoActualizado, string ValorAnterior, string ValorNuevo, string Usuario)
        {
            this.IdProveedor = IdProveedor;
            this.CampoActualizado = CampoActualizado;
            this.ValorAnterior = ValorAnterior;
            this.ValorNuevo = ValorNuevo;
            this.Estado = true;
            this.UsuarioCreacion = Usuario;
            this.UsuarioModificacion = Usuario;
            this.FechaCreacion = DateTime.Now;
            this.FechaModificacion = DateTime.Now;
        }
    }
}

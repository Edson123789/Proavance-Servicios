using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoDescuento
    {
        public TTipoDescuento()
        {
            TTipoDescuentoAsesorCoordinadorPw = new HashSet<TTipoDescuentoAsesorCoordinadorPw>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Formula { get; set; }
        public int? PorcentajeGeneral { get; set; }
        public int? PorcentajeMatricula { get; set; }
        public int? FraccionesMatricula { get; set; }
        public int? PorcentajeCuotas { get; set; }
        public int? CuotasAdicionales { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TTipoDescuentoAsesorCoordinadorPw> TTipoDescuentoAsesorCoordinadorPw { get; set; }
    }
}

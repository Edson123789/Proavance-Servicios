using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMontoPagoCronograma
    {
        public TMontoPagoCronograma()
        {
            TMontoPagoCronogramaDetalle = new HashSet<TMontoPagoCronogramaDetalle>();
        }

        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; }
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string CodigoMatricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TMontoPagoCronogramaDetalle> TMontoPagoCronogramaDetalle { get; set; }
    }
}

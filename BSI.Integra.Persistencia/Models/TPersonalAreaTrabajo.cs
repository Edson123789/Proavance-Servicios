using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalAreaTrabajo
    {
        public TPersonalAreaTrabajo()
        {
            TLlamadaWebphone = new HashSet<TLlamadaWebphone>();
            TOcurrencia = new HashSet<TOcurrencia>();
            TPuestoTrabajo = new HashSet<TPuestoTrabajo>();
            TPuestoTrabajoRelacionDetalle = new HashSet<TPuestoTrabajoRelacionDetalle>();
            TPuestoTrabajoRelacionInterna = new HashSet<TPuestoTrabajoRelacionInterna>();
            TUrlContenedorPermisos = new HashSet<TUrlContenedorPermisos>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TLlamadaWebphone> TLlamadaWebphone { get; set; }
        public virtual ICollection<TOcurrencia> TOcurrencia { get; set; }
        public virtual ICollection<TPuestoTrabajo> TPuestoTrabajo { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionDetalle> TPuestoTrabajoRelacionDetalle { get; set; }
        public virtual ICollection<TPuestoTrabajoRelacionInterna> TPuestoTrabajoRelacionInterna { get; set; }
        public virtual ICollection<TUrlContenedorPermisos> TUrlContenedorPermisos { get; set; }
    }
}

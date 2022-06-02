using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TUrlBlockStorage
    {
        public TUrlBlockStorage()
        {
            TRegistroCertificadoFisicoGenerado = new HashSet<TRegistroCertificadoFisicoGenerado>();
            TUrlContenedorPermisos = new HashSet<TUrlContenedorPermisos>();
            TUrlSubContenedor = new HashSet<TUrlSubContenedor>();
        }

        public int Id { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }
        public int IdProveedorNube { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsVisibleModuloArchivos { get; set; }
        public bool? AplicaSubcontenedores { get; set; }
        public bool? AplicaSubidaMultiple { get; set; }

        public virtual ICollection<TRegistroCertificadoFisicoGenerado> TRegistroCertificadoFisicoGenerado { get; set; }
        public virtual ICollection<TUrlContenedorPermisos> TUrlContenedorPermisos { get; set; }
        public virtual ICollection<TUrlSubContenedor> TUrlSubContenedor { get; set; }
    }
}

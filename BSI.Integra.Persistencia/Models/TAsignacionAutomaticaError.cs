using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAsignacionAutomaticaError
    {
        public int Id { get; set; }
        public string Campo { get; set; }
        public string Descripcion { get; set; }
        public int? IdContacto { get; set; }
        public int? IdAsignacionAutomatica { get; set; }
        public int? IdAsignacionAutomaticaTipoError { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

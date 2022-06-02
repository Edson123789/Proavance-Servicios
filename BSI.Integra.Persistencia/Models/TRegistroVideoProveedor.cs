using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegistroVideoProveedor
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdTipoProveedorVideo { get; set; }
        public string VideoId { get; set; }
        public string Nombre { get; set; }
        public string NombreReproductor { get; set; }
        public int Tiempo { get; set; }
        public bool VideoActivo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRegionCiudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public int? CodigoBs { get; set; }
        public string DenominacionBs { get; set; }
        public string NombreCorto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

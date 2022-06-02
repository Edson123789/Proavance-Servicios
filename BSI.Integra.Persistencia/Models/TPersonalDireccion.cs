using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPersonalDireccion
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Distrito { get; set; }
        public string TipoVia { get; set; }
        public string NombreVia { get; set; }
        public string Manzana { get; set; }
        public int? Lote { get; set; }
        public string TipoZonaUrbana { get; set; }
        public string NombreZonaUrbana { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAscenso
    {
        public int Id { get; set; }
        public string CargoMercado { get; set; }
        public bool Activo { get; set; }
        public string FechaPublicacion { get; set; }
        public int SueldoMin { get; set; }
        public int IdMoneda { get; set; }
        public int IdAreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int IdPortalEmpleo { get; set; }
        public int IdCargo { get; set; }
        public int? IdEmpresa { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int IdRegionCiudad { get; set; }
        public string UrlOferta { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid IdMigracion { get; set; }
    }
}

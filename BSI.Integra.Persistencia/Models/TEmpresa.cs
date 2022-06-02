using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEmpresa
    {
        public TEmpresa()
        {
            TFur = new HashSet<TFur>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        public string IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? Ciiu { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TFur> TFur { get; set; }
    }
}

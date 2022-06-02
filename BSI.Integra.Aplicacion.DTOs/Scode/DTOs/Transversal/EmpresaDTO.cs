using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EmpresaDTO
    {
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
        public string usuario { get; set; }
    }
}

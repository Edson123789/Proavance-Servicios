using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSueldoIndividual
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Industria { get; set; }
        public int? IdIndustria { get; set; }
        public string TamanioEmpresa { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public string EmpresaNombre { get; set; }
        public int? IdEmpresa { get; set; }
        public string Cargo { get; set; }
        public int? IdCargo { get; set; }
        public string AreaTrabajo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string Ciudad { get; set; }
        public int? IdCodigoCiudad { get; set; }
        public string Pais { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? SeLimiteInferior { get; set; }
        public int? SeLimiteSuperior { get; set; }
        public double? SePromedio { get; set; }
        public string OrigenInformacion { get; set; }
        public bool? Incluir { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

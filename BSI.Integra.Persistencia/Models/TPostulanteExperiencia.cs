using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteExperiencia
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int? IdEmpresa { get; set; }
        public string OtraEmpresa { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string NombreJefe { get; set; }
        public string NumeroJefe { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? AlaActualidad { get; set; }
        public bool? EsUltimoEmpleo { get; set; }
        public decimal? Salario { get; set; }
        public string Funcion { get; set; }
        public decimal? SalarioComision { get; set; }
        public int? IdMoneda { get; set; }
    }
}

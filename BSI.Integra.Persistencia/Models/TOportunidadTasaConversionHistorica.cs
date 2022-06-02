using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidadTasaConversionHistorica
    {
        public int Id { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdPgeneral { get; set; }
        public int IdPrograma { get; set; }
        public int IdPespecifico { get; set; }
        public string NombrePespecifico { get; set; }
        public int? IdOportunidad { get; set; }
        public string NombreContacto { get; set; }
        public int? IdcategoriaDato { get; set; }
        public string NombreCategoria { get; set; }
        public double? ValorCategoriaD { get; set; }
        public int? IdAformacion { get; set; }
        public string NombreAformacion { get; set; }
        public double? ValorAformacion { get; set; }
        public int? IdCargo { get; set; }
        public string NombreCargo { get; set; }
        public double? ValorCargo { get; set; }
        public int? IdAtrabajo { get; set; }
        public string NombreAtrabajo { get; set; }
        public double? ValorAtrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string NombreIndustria { get; set; }
        public double? ValorIndustria { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public double? ValorPais { get; set; }
        public double? SumaModelo { get; set; }
        public double? Probabilidad { get; set; }
        public string ProbabilidaDesc { get; set; }
        public DateTime FechaImportacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

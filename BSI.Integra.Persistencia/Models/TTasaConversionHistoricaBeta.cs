using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTasaConversionHistoricaBeta
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public DateTime Fecha { get; set; }
        public int? TiempoCapacitacion { get; set; }
        public int? TiempoCapacitacionValidacion { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public string Tipo { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdCampania { get; set; }
        public int? IdAformacion { get; set; }
        public int? Idcargo { get; set; }
        public int? IdAtrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCodigoPais { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Ciudad { get; set; }
        public string ProbabilidadDesc { get; set; }
        public double? ProbabilidadActual { get; set; }
        public double? ValorCategoriaD { get; set; }
        public double? ValorAformacion { get; set; }
        public double? ValorCargo { get; set; }
        public double? ValorAtrabajo { get; set; }
        public double? ValorIndustria { get; set; }
        public double? ValorPais { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCoordinador { get; set; }
        public DateTime? FechaCierre { get; set; }
        public bool? ReasignadoIp { get; set; }
        public int? AsesorAnt { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaReasignacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadMaxima { get; set; }
        public DateTime? FechaPrimerContacto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

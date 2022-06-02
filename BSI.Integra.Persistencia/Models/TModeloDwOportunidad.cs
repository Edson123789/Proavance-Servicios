using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloDwOportunidad
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadMaxima { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdCentroCostoActual { get; set; }
        public string CentroCostoInicial { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdFaseOportunidadMaximaInicial { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string NombreCompleto { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaCreacionAlumno { get; set; }
        public int IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string Email1 { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}

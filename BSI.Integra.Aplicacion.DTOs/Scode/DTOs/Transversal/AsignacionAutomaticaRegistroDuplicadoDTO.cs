using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaRegistroDuplicadoDTO
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public string Tipodato { get; set; }
        public int? IdTipodato { get; set; }
        public string Origen { get; set; }
        public int? IdOrigen { get; set; }
        public string CodigoFase { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Formacion { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string Trabajo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string Industria { get; set; }
        public int? IdIndustria { get; set; }
        public string Cargo { get; set; }
        public int? IdCargo { get; set; }
        public string NombrePais { get; set; }
        public int? IdPais { get; set; }
        public string NombreCiudad { get; set; }
        public string IdCiudad { get; set; }
        public bool? Validado { get; set; }
        public bool? Corregido { get; set; }
        public string OrigenCampania { get; set; }
        public string IdCampaniaScoring { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAsignacionAutomaticaOrigen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadPortal { get; set; }
    }
}

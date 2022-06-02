using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaRegistroErroneoDTO
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
        public string OrigenCampania { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAsignacionAutomaticaOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string IdFaseOportunidadPortal { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public int? IdAsignacionAutomaticaTipoError { get; set; }
        public bool? EStadoAsignacion { get; set; }
        public bool? EstadoError { get; set; }
        public List<AsignacionAutomaticaErrorDTO> Errores { get; set; }



    }
}

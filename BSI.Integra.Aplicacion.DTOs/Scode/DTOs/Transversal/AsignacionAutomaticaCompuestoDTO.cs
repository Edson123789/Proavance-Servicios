using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdContacto { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdCargo { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public bool? Validado { get; set; }
        public bool? Corregido { get; set; }
        public string OrigenCampania { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAsignacionAutomaticaOrigen { get; set; }
        public int? IdCampaniaScoring { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdTiempoCapacitacion { get; set; }
        public int? IdCategoriaDato { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public double? ProbabilidadActual { get; set; }
        public int? LongTelefono { get; set; }
        public int? LongCelular { get; set; }
        public string ProbabilidadActualDesc { get; set; }
        public int? IdPagina { get; set; }
        public DateTime? FechaProgramada { get; set; }
    }
}

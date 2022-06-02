using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class OportunidadDTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOrigen { get; set; }
        public int IdAlumno { get; set; }
        public string UltimoComentario { get; set; }
        public int IdActividadDetalleUltima { get; set; }
        public int IdActividadCabeceraUltima { get; set; }
        public int IdEstadoActividadDetalleUltimoEstado { get; set; }
        public string UltimaFechaProgramada { get; set; }
        public string UltimaHoraProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCampaniaScoring { get; set; }
        public int IdFaseOportunidadIp { get; set; }
        public int IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public Guid? IdFaseOportunidadPortal { get; set; }
        public int IdFaseOportunidadPf { get; set; }
        public string CodigoPagoIc { get; set; }
        public int FlagVentaCruzada { get; set; }
        public int IdTiempoCapacitacion { get; set; }
        public int IdTiempoCapacitacionValidacion { get; set; }
        public int IdSubCategoriaDato { get; set; }
        public int IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string CodMailing { get; set; }
        public int IdPagina { get; set; }
        public bool FasesActivas { get; set; }

        public int? IdTipoInteraccion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }



    }
}

using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: OportunidadLogBO
    ///Autor: Edgar S.
    ///Fecha: 08/02/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_OportunidadLog
    ///</summary>
    public class OportunidadLogBO : BaseBO
    {
        ///Propiedades		                Significado
        ///-------------	                -----------------------
        ///IdOportunidad                    Id de Oportunidad
        ///IdCentroCosto                    Id de Centro de Costo
        ///IdPersonalAsignado               Id de Personal Asignada
        ///IdTipoDato                       Id de Tipo de Dato
        ///IdFaseOportunidadAnt             Id de Fase Anterior de Oportunidad
        ///IdFaseOportunidad                Id de Fase de Oportunidad
        ///IdOrigen                         Id de Origen
        ///IdContacto                       Id de Contacto
        ///FechaLog                         Fecha de Log
        ///IdActividadDetalle               Id de Actividad Detalle
        ///IdOcurrencia                     Id de Ocurrencia
        ///IdOcurrenciaActividad            Id de Ocurrencia de Actividad
        ///Comentario                       Comentario
        ///IdCategoriaOrigen                Id de Categoría Origen
        ///IdConjuntoAnuncio                Id de Conjunto Anuncio
        ///IdFaseOportunidadIp              Id de Fase de Oportunidad IP
        ///IdFaseOportunidadIc              Id de Fase de Oportunidad IC
        ///FechaEnvioFaseOportunidadPf      Fecha de Envío de Oportunidad de Fase PF
        ///FechaPagoFaseOportunidadPf       Fecha de Pago de Oportunidad Fase PF
        ///FechaPagoFaseOportunidadIc       Fecha de Pago de Oportunidad Fase IC
        ///FasesActivas                     Fases Activas
        ///FechaRegistroCampania            Fecha Registro de Campaña
        ///IdFaseOportunidadPf              Id de Fase Oportunidad PF
        ///CodigoPagoIc                     Código de Pago Oportunidad IC
        ///IdAsesorAnt                      Id de Asesor Anterior
        ///IdCentroCostoAnt                 Id de Centro de Costo Anterior
        ///FechaFinLog                      Fecha Fin de Log
        ///FechaCambioFase                  Fecha de cambio de fase
        ///CambioFase                       Cambio de Fase
        ///FechaCambioFaseIs                Fecha de cambio de fase IS
        ///CambioFaseIs                     Cambio de Fase IS
        ///FechaCambioFaseAnt               Fecha de Cambio de fase anterior
        ///FechaCambioAsesor                Fecha de cambio de Asesor
        ///FechaCambioAsesorAnt             Fecha de cambio de Asesor Anterior
        ///CambioFaseAsesor                 Cambio de Fase Asesor
        ///CicloRn2                         Ciclo RN2
        ///IdSubCategoriaDato               Id de dato subcategoría
        ///IdMigracion                      Id de Migración
        ///IdClasificacionPersona           Id de Clasificación Persona
        ///IdPersonalAreaTrabajo            Id de Área de trabajo de personal
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidadAnt { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string Comentario { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public bool? FasesActivas { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string CodigoPagoIc { get; set; }
        public int? IdAsesorAnt { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public DateTime? FechaFinLog { get; set; }
        public DateTime? FechaCambioFase { get; set; }
        public bool? CambioFase { get; set; }
        public DateTime? FechaCambioFaseIs { get; set; }
        public bool? CambioFaseIs { get; set; }
        public DateTime? FechaCambioFaseAnt { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnt { get; set; }
        public int? CambioFaseAsesor { get; set; }
        public int? CicloRn2 { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
		public int? IdClasificacionPersona { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public OportunidadLogBO() {
        }
    }
}

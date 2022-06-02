using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Net;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: OportunidadBO
    ///Autor: Edgar S.
    ///Fecha: 08/02/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_Oportunidad
    ///</summary>
    public class OportunidadBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdCentroCosto                            Id de Centro de Costo
        ///IdPersonalAsignado                       Id de Personal Asignado
        ///IdTipoDato                               Id de Tipo de dato
        ///IdFaseOportunidad                        Id de Fase de Oportunidad
        ///IdOrigen                                 Id Origen
        ///IdAlumno                                 Id de alumno
        ///UltimoComentario                         Ultimo Comentario de Oportunidad
        ///IdActividadDetalleUltima                 Id de detalles de última actividad
        ///IdActividadCabeceraUltima                Id de cabecera de Actividad
        ///IdEstadoActividadDetalleUltimoEstado     Id de estado de actividad de último estado
        ///UltimaFechaProgramada                    Última fecha Programada de Oportunidad
        ///IdEstadoOportunidad                      Id de Estado de Oportunidad
        ///IdEstadoOcurrenciaUltimo                 Id de último Estado de Ocurrencia
        ///IdFaseOportunidadMaxima                  Id de Fase de Oportunidad Máxima
        ///IdFaseOportunidadInicial                 Id de Fase de Oportunidad Inicial
        ///IdCategoriaOrigen                        Id de categoría origen
        ///IdConjuntoAnuncio                        Id de Conjunto Anuncio
        ///IdAnuncioFacebook                        Id del anuncio de facebook (PK de la tabla mkt.T_AnuncioFacebook)
        ///IdCampaniaScoring                        Id de Campaña Scoring
        ///IdFaseOportunidadIp                      Id de Fase de Oportunidad IP
        ///IdFaseOportunidadIc                      Id de Fase de Oportunidad IC
        ///FechaEnvioFaseOportunidadPf              Fecha de Envio de Oportunidad de Fase PF
        ///FechaPagoFaseOportunidadPf               Fecha de Pago de Oportunidad Fase PF
        ///FechaPagoFaseOportunidadIc               Fecha de Pago de Oportunidad Fase IC
        ///FechaRegistroCampania                    Fecha de Registro de Campaña
        ///IdFaseOportunidadPortal                  Id de Fase Oportunidad en el Portal
        ///IdFaseOportunidadPf                      Id de Fase Oportunidad PF
        ///CodigoPagoIc                             Código de Pago Oportunidad IC
        ///FlagVentaCruzada                         Bandera Indicador de Venta Cruzada
        ///IdTiempoCapacitacion                     Id de Tiempo de Capacitación
        ///IdTiempoCapacitacionValidacion           Id de Tiempo de Validación de Capacitación
        ///IdSubCategoriaDato                       Id de subcategoría de Dato
        ///IdInteraccionFormulario                  Id de Interacción en Formulario
        ///UrlOrigen                                Url de Origen
        ///FechaPaso2                               Fecha de Paso 2
        ///Paso2                                    Paso 2
        ///CodMailing                               Código de mailing
        ///IdPagina                                 Id de Página
        ///ValorProbabilidad                        Valor de probabilidad
        ///IdMigracion                              Id de migración
        ///IdTipoInteraccion                        Id tipo de Interacción
        ///NroSolicitud                             Número de Solicitud
        ///NroSolicitudPorArea                      Número de Solicitud por Área
        ///NroSolicitudPorSubArea                   Número de Solicitud por SubÁrea
        ///NroSolicitudPorProgramaGeneral           Número de Solicitud por Programa General
        ///NroSolicitudPorProgramaEspecifico        Número de Solicitud por Programa Específico
        ///IdClasificacionPersona                   Id de clasificación de persona
        ///IdPersonalAreaTrabajo                    Id de Personal de área de trabajo
        ///IdPadre                                  Id Padre
        ///ValidacionCorrecta                       Flag para verificar si la validacion es correcta
        public int? IdCentroCosto { get; set; }
        public int IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdOrigen { get; set; }
        public int IdAlumno { get; set; }
        public string UltimoComentario { get; set; }
        public int IdActividadDetalleUltima { get; set; }
        public int IdActividadCabeceraUltima { get; set; }
        public int IdEstadoActividadDetalleUltimoEstado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int? IdAnuncioFacebook { get; set; }
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
        public int? FlagVentaCruzada { get; set; }
        public int IdTiempoCapacitacion { get; set; }
        public int IdTiempoCapacitacionValidacion { get; set; }
        public int IdSubCategoriaDato { get; set; }
        public int IdInteraccionFormulario { get; set; }
        public string UrlOrigen { get; set; }
        public DateTime? FechaPaso2 { get; set; }
        public bool? Paso2 { get; set; }
        public string CodMailing { get; set; }
        public int IdPagina { get; set; }
        public decimal? ValorProbabilidad { get; set; } // se usa para almacenar el texto de la probabilidad 
        public Guid? IdMigracion { get; set; }
        public int IdTipoInteraccion { get; set; }
        public int? NroSolicitud { get; set; }
        public int? NroSolicitudPorArea { get; set; }
        public int? NroSolicitudPorSubArea { get; set; }
        public int? NroSolicitudPorProgramaGeneral { get; set; }
        public int? NroSolicitudPorProgramaEspecifico { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPadre { get; set; }
        public bool? ValidacionCorrecta { get; set; }

        //BO

        public List<ActividadDetalleBO> Actividades;
        public ActividadDetalleBO ActividadAntigua;
        public ActividadDetalleBO ActividadNueva;
        public ActividadDetalleBO ActividadNuevaProgramarActividad;
        public OportunidadLogBO OportunidadLogAntigua;
        public OportunidadLogBO OportunidadLogNueva;
        public OportunidadCompetidorBO OportunidadCompetidor;
        public CalidadProcesamientoBO CalidadProcesamiento;
        public List<SolucionClienteByActividadBO> ListaSoluciones;
        public ComprobantePagoOportunidadBO ComprobantePago;
        public PreCalculadaCambioFaseBO PreCalculadaCambioFase;
        public ModeloDataMiningBO ModeloDataMining;
        public AsignacionOportunidadBO AsignacionOportunidad;
        public AsignacionOportunidadLogBO AsignacionOportunidadLog;
        public AlumnoBO Alumno;
        public ExpositorBO Expositor;

        private string usuario;
        //Persistencia
        private OportunidadRepositorio _repOportunidad;
        private OportunidadLogRepositorio repOportunidadLog;
        private LlamadaActividadRepositorio _repLlamadaActividad;
        private OcurrenciaRepositorio _repOcurrencia;
        private FaseOportunidadRepositorio _repFaseOportunidad;
        private CalidadProcesamientoRepositorio _repCalidadProcesamiento;
        private PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private ActividadDetalleRepositorio repActividadDetalle;
        private OcurrenciaAlternoRepositorio _repOcurrenciaAlterno;
        private OcurrenciaActividadAlternoRepositorio _repOcurrenciaActividadAlterno;
        private PersonalRepositorio _repPersonal;

        private Dictionary<string, Func<EtiquetaParametroDTO, string>> etiquetas;

        public OportunidadBO()
        {
        }
        public OportunidadBO(string usuarioAsignado, integraDBContext context)
        {
            usuario = usuarioAsignado;
            _repOportunidad = new OportunidadRepositorio(context);
            repOportunidadLog = new OportunidadLogRepositorio(context);

        }
        public OportunidadBO(integraDBContext context)
        {
            _repOportunidad = new OportunidadRepositorio(context);
            repOportunidadLog = new OportunidadLogRepositorio(context);
        }

        public OportunidadBO(int id, string usuarioAsignado, integraDBContext context)
        {
            usuario = usuarioAsignado;
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            repOportunidadLog = new OportunidadLogRepositorio(context);
            _repLlamadaActividad = new LlamadaActividadRepositorio(context);
            _repOcurrencia = new OcurrenciaRepositorio(context);
            _repOportunidad = new OportunidadRepositorio(context);
            _repFaseOportunidad = new FaseOportunidadRepositorio(context);
            _repCalidadProcesamiento = new CalidadProcesamientoRepositorio(context);
            repActividadDetalle = new ActividadDetalleRepositorio(context);
            _repOcurrenciaActividadAlterno = new OcurrenciaActividadAlternoRepositorio(context);
            _repOcurrenciaAlterno = new OcurrenciaAlternoRepositorio(context);
            _repPersonal = new PersonalRepositorio(context);

            var Oportunidad = _repOportunidad.FirstById(id);
            this.Id = Oportunidad.Id;
            this.IdCentroCosto = Oportunidad.IdCentroCosto;
            this.IdPersonalAsignado = Oportunidad.IdPersonalAsignado;
            this.IdTipoDato = Oportunidad.IdTipoDato;
            this.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
            this.IdOrigen = Oportunidad.IdOrigen;
            this.IdAlumno = Oportunidad.IdAlumno;
            this.UltimoComentario = Oportunidad.UltimoComentario;
            this.IdActividadDetalleUltima = Oportunidad.IdActividadDetalleUltima;
            this.IdActividadCabeceraUltima = Oportunidad.IdActividadCabeceraUltima;
            this.IdEstadoActividadDetalleUltimoEstado = Oportunidad.IdEstadoActividadDetalleUltimoEstado;
            this.UltimaFechaProgramada = Oportunidad.UltimaFechaProgramada;
            this.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
            this.IdEstadoOcurrenciaUltimo = Oportunidad.IdEstadoOcurrenciaUltimo;
            this.IdFaseOportunidadMaxima = Oportunidad.IdFaseOportunidadMaxima;
            this.IdFaseOportunidadInicial = Oportunidad.IdFaseOportunidadInicial;
            this.IdCategoriaOrigen = Oportunidad.IdCategoriaOrigen;
            this.IdConjuntoAnuncio = Oportunidad.IdConjuntoAnuncio;
            this.IdCampaniaScoring = Oportunidad.IdCampaniaScoring;
            this.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
            this.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
            this.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
            this.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
            this.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
            this.FechaRegistroCampania = Oportunidad.FechaRegistroCampania;
            this.IdFaseOportunidadPortal = Oportunidad.IdFaseOportunidadPortal;
            this.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
            this.CodigoPagoIc = Oportunidad.CodigoPagoIc;
            this.FlagVentaCruzada = Oportunidad.FlagVentaCruzada;
            this.IdTiempoCapacitacion = Oportunidad.IdTiempoCapacitacion;
            this.IdTiempoCapacitacionValidacion = Oportunidad.IdTiempoCapacitacionValidacion;
            this.IdSubCategoriaDato = Oportunidad.IdSubCategoriaDato;
            this.IdInteraccionFormulario = Oportunidad.IdInteraccionFormulario;
            this.UrlOrigen = Oportunidad.UrlOrigen;
            this.FechaPaso2 = Oportunidad.FechaPaso2;
            this.Paso2 = Oportunidad.Paso2;
            this.CodMailing = Oportunidad.CodMailing;
            this.IdPagina = Oportunidad.IdPagina;
            this.FechaCreacion = Oportunidad.FechaCreacion;
            this.FechaModificacion = Oportunidad.FechaModificacion;
            this.Estado = Oportunidad.Estado;
            this.UsuarioCreacion = Oportunidad.UsuarioCreacion;
            this.UsuarioModificacion = Oportunidad.UsuarioModificacion;
            this.RowVersion = Oportunidad.RowVersion;
            this.IdMigracion = Oportunidad.IdMigracion;
            this.NroSolicitud = Oportunidad.NroSolicitud;
            this.NroSolicitudPorArea = Oportunidad.NroSolicitudPorArea;
            this.NroSolicitudPorSubArea = Oportunidad.NroSolicitudPorSubArea;
            this.NroSolicitudPorProgramaGeneral = Oportunidad.NroSolicitudPorProgramaGeneral;
            this.NroSolicitudPorProgramaEspecifico = Oportunidad.NroSolicitudPorProgramaEspecifico;
            this.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
            this.IdPersonalAreaTrabajo = Oportunidad.IdPersonalAreaTrabajo;
            this.IdPadre = Oportunidad.IdPadre;

        }


        public OportunidadBO(int id, string usuarioAsignado)
        {
            usuario = usuarioAsignado;
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            repOportunidadLog = new OportunidadLogRepositorio();
            _repLlamadaActividad = new LlamadaActividadRepositorio();
            _repOcurrencia = new OcurrenciaRepositorio();
            _repOportunidad = new OportunidadRepositorio();
            _repFaseOportunidad = new FaseOportunidadRepositorio();
            _repCalidadProcesamiento = new CalidadProcesamientoRepositorio();
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
            _repOcurrenciaActividadAlterno = new OcurrenciaActividadAlternoRepositorio();
            _repOcurrenciaAlterno = new OcurrenciaAlternoRepositorio();

            var Entidad = this.GetType();
            //InicializarValidadoresGenerales(this.GetType().Name, Entidad);

            var Oportunidad = _repOportunidad.FirstById(id);
            this.Id = Oportunidad.Id;
            this.IdCentroCosto = Oportunidad.IdCentroCosto;
            this.IdPersonalAsignado = Oportunidad.IdPersonalAsignado;
            this.IdTipoDato = Oportunidad.IdTipoDato;
            this.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
            this.IdOrigen = Oportunidad.IdOrigen;
            this.IdAlumno = Oportunidad.IdAlumno;
            this.UltimoComentario = Oportunidad.UltimoComentario;
            this.IdActividadDetalleUltima = Oportunidad.IdActividadDetalleUltima;
            this.IdActividadCabeceraUltima = Oportunidad.IdActividadCabeceraUltima;
            this.IdEstadoActividadDetalleUltimoEstado = Oportunidad.IdEstadoActividadDetalleUltimoEstado;
            this.UltimaFechaProgramada = Oportunidad.UltimaFechaProgramada;
            this.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
            this.IdEstadoOcurrenciaUltimo = Oportunidad.IdEstadoOcurrenciaUltimo;
            this.IdFaseOportunidadMaxima = Oportunidad.IdFaseOportunidadMaxima;
            this.IdFaseOportunidadInicial = Oportunidad.IdFaseOportunidadInicial;
            this.IdCategoriaOrigen = Oportunidad.IdCategoriaOrigen;
            this.IdConjuntoAnuncio = Oportunidad.IdConjuntoAnuncio;
            this.IdCampaniaScoring = Oportunidad.IdCampaniaScoring;
            this.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
            this.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
            this.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
            this.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
            this.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
            this.FechaRegistroCampania = Oportunidad.FechaRegistroCampania;
            this.IdFaseOportunidadPortal = Oportunidad.IdFaseOportunidadPortal;
            this.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
            this.CodigoPagoIc = Oportunidad.CodigoPagoIc;
            this.FlagVentaCruzada = Oportunidad.IdTiempoCapacitacion;
            this.IdTiempoCapacitacion = Oportunidad.IdTiempoCapacitacion;
            this.IdTiempoCapacitacionValidacion = Oportunidad.IdTiempoCapacitacionValidacion;
            this.IdSubCategoriaDato = Oportunidad.IdSubCategoriaDato;
            this.IdInteraccionFormulario = Oportunidad.IdInteraccionFormulario;
            this.UrlOrigen = Oportunidad.UrlOrigen;
            this.FechaPaso2 = Oportunidad.FechaPaso2;
            this.Paso2 = Oportunidad.Paso2;
            this.CodMailing = Oportunidad.CodMailing;
            this.IdPagina = Oportunidad.IdPagina;
            this.FechaCreacion = Oportunidad.FechaCreacion;
            this.FechaModificacion = Oportunidad.FechaModificacion;
            this.Estado = Oportunidad.Estado;
            this.UsuarioCreacion = Oportunidad.UsuarioCreacion;
            this.UsuarioModificacion = Oportunidad.UsuarioModificacion;
            this.RowVersion = Oportunidad.RowVersion;
            this.IdMigracion = Oportunidad.IdMigracion;
            this.NroSolicitud = Oportunidad.NroSolicitud;
            this.NroSolicitudPorArea = Oportunidad.NroSolicitudPorArea;
            this.NroSolicitudPorSubArea = Oportunidad.NroSolicitudPorSubArea;
            this.NroSolicitudPorProgramaGeneral = Oportunidad.NroSolicitudPorProgramaGeneral;
            this.NroSolicitudPorProgramaEspecifico = Oportunidad.NroSolicitudPorProgramaEspecifico;
        }

        /// <summary>
        /// Validación condiciones generales
        /// </summary>
        /// <param name="nombreClass"> nombre de clase </param>
        /// <param name="entidad"> Entidad </param>
        /// <returns></returns>
        public void InicializarValidadoresGenerales(string nombreClass, Type entidad)
        {


            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdCentroCosto").Name, "Centro Costo");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdPersonalAsignado").Name, "Asignado a Actividad");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdTipoDato").Name, "Tipo dato");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdFaseOportunidad").Name, "Fase Oportunidad");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdOrigen").Name, "Origen ");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdAlumno").Name, "Id Alumno");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdActividadDetalleUltima").Name, "Actviidad Detalle");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdActividadCabeceraUltima").Name, "Cabacera Ultima");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdEstadoActividadDetalleUltimoEstado").Name, "Estado Actividad");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdEstadoOportunidad").Name, "Estado Oportunidad");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdFaseOportunidadMaxima").Name, "Fase Maxima");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdCategoriaOrigen").Name, "Categoria Origen");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdPagina").Name, "Origen Portal");
            ValidateRequiredStringProperty(nombreClass, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("IdSubCategoriaDato").Name, "Sub Categoria");

        }

        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="mantenerestadooportunidad"> Confirmación para mantener el estado de oportunidad </param>
        /// <param name="datosOportunidad"> Datos de Oportunidad </param>
        /// <returns> Vacio </returns>
        public void FinalizarActividad(bool mantenerestadooportunidad, OportunidadDTO datosOportunidad)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                OportunidadLogAntigua = repOportunidadLog.ObtenerUltimoOportunidadLog(ActividadAntigua.IdOportunidad);

                var fechaFinLLamada = DateTime.Now;

                if (ActividadAntigua.IdOcurrencia == 0)
                    throw new ArgumentException("Debe seleccionar una ocurrencia");

                ActividadAntigua.DuracionReal = 13;
                ActividadAntigua.IdCentralLlamada = 3;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _repOcurrencia.ObtenerOcurrenciaPorActividad(ActividadAntigua.IdOcurrencia);

                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_repOcurrencia.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                }


                ActividadAntigua.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;

                // Actualizar Actividad Detalle
                if (!repActividadDetalle.Exist(ActividadAntigua.Id)) throw new Exception("La oportunidad no tiene actividad detalle");

                ActividadNueva = repActividadDetalle.FirstById(ActividadAntigua.Id);
                ActividadNueva.FechaReal = DateTime.Now;
                ActividadNueva.DuracionReal = ActividadAntigua.DuracionReal;
                ActividadNueva.IdEstadoActividadDetalle = ActividadAntigua.IdEstadoActividadDetalle;
                ActividadNueva.Comentario = ActividadAntigua.Comentario;
                ActividadNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                ActividadNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                ActividadNueva.IdCentralLlamada = ActividadAntigua.IdCentralLlamada;
                ActividadNueva.FechaModificacion = DateTime.Now;
                ActividadNueva.UsuarioModificacion = usuario;
                ActividadNueva.IdClasificacionPersona = ActividadAntigua.IdClasificacionPersona;

                if (ocurrencia.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo)
                {
                    this.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                }

                this.UltimoComentario = ActividadAntigua.Comentario;
                this.UltimaFechaProgramada = ActividadAntigua.FechaProgramada == null ? DateTime.Now : ActividadAntigua.FechaProgramada.Value;

                this.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                this.IdActividadDetalleUltima = ActividadAntigua.Id;

                if (datosOportunidad.IdEstadoOportunidad != 0 && datosOportunidad.IdEstadoOportunidad == ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada && mantenerestadooportunidad)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
                else
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadEjecutada;
                }

                this.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (this.IdFaseOportunidad != 0 && datosOportunidad.IdFaseOportunidad != this.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    this.IdFaseOportunidadMaxima = _repFaseOportunidad.GetFaseMaxima(datosOportunidad.IdFaseOportunidad, this.IdFaseOportunidad);
                }
                this.FechaModificacion = DateTime.Now;
                this.UsuarioModificacion = usuario;

                //Guardar Log
                OportunidadLogNueva = new OportunidadLogBO();
                OportunidadLogNueva.IdClasificacionPersona = OportunidadLogAntigua.IdClasificacionPersona;
                OportunidadLogNueva.IdPersonalAreaTrabajo = OportunidadLogAntigua.IdPersonalAreaTrabajo;
                OportunidadLogNueva.IdCentroCosto = this.IdCentroCosto;
                OportunidadLogNueva.IdPersonalAsignado = this.IdPersonalAsignado;
                OportunidadLogNueva.IdTipoDato = this.IdTipoDato;
                OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                OportunidadLogNueva.IdFaseOportunidad = this.IdFaseOportunidad;
                OportunidadLogNueva.IdOrigen = this.IdOrigen;
                OportunidadLogNueva.IdContacto = this.IdAlumno;
                OportunidadLogNueva.FechaFinLog = OportunidadLogAntigua.FechaLog;
                OportunidadLogNueva.IdCentroCostoAnt = OportunidadLogAntigua.IdCentroCosto;
                OportunidadLogNueva.IdAsesorAnt = OportunidadLogAntigua.IdPersonalAsignado;
                OportunidadLogNueva.FechaLog = DateTime.Now;
                OportunidadLogNueva.IdActividadDetalle = ActividadAntigua.Id;
                OportunidadLogNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                OportunidadLogNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                OportunidadLogNueva.Comentario = this.UltimoComentario;
                OportunidadLogNueva.IdOportunidad = this.Id;
                OportunidadLogNueva.IdCategoriaOrigen = this.IdCategoriaOrigen;
                OportunidadLogNueva.IdSubCategoriaDato = this.IdSubCategoriaDato;
                OportunidadLogNueva.FechaRegistroCampania = FechaRegistroCampania;
                OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                OportunidadLogNueva.IdClasificacionPersona = this.IdClasificacionPersona;
                OportunidadLogNueva.IdPersonalAreaTrabajo = this.IdPersonalAreaTrabajo;

                if (OportunidadLogNueva.IdFaseOportunidadAnt != OportunidadLogNueva.IdFaseOportunidad)
                {
                    OportunidadLogNueva.CambioFase = true;
                    OportunidadLogNueva.FechaCambioFase = OportunidadLogNueva.FechaLog;
                    OportunidadLogNueva.FechaCambioFaseAnt = OportunidadLogAntigua.FechaCambioFase;
                    OportunidadLogNueva.CambioFaseAsesor = 1;
                    OportunidadLogNueva.FechaCambioAsesor = OportunidadLogNueva.FechaLog;
                    OportunidadLogNueva.FechaCambioAsesorAnt = OportunidadLogAntigua.FechaCambioAsesor;

                    if (OportunidadLogNueva.IdFaseOportunidad != 0 && OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                    {
                        OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogNueva.FechaLog;
                        OportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogNueva.FechaCambioFaseIs;
                        OportunidadLogNueva.CambioFaseIs = false;
                    }

                    if (OportunidadLogNueva.IdFaseOportunidad != 0 && OportunidadLogNueva.IdFaseOportunidadAnt != 0 && OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 && OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2)
                    {
                        OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2 + 1;
                    }
                    else
                    {
                        OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2;
                    }
                }
                else
                {
                    OportunidadLogNueva.CambioFase = false;
                    OportunidadLogNueva.FechaCambioFase = OportunidadLogAntigua.FechaCambioFase;
                    OportunidadLogNueva.FechaCambioFaseAnt = OportunidadLogAntigua.FechaCambioFase;//ultima 1***
                    OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogAntigua.FechaCambioFaseIs;
                    OportunidadLogNueva.CambioFaseIs = false;
                    OportunidadLogNueva.CambioFaseAsesor = 0;
                    OportunidadLogNueva.FechaCambioAsesor = OportunidadLogAntigua.FechaCambioAsesor;
                    OportunidadLogNueva.FechaCambioAsesorAnt = OportunidadLogAntigua.FechaCambioAsesor;
                    OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2;
                }
                OportunidadLogNueva.FechaCreacion = DateTime.Now;
                OportunidadLogNueva.FechaModificacion = DateTime.Now;
                OportunidadLogNueva.UsuarioCreacion = usuario;
                OportunidadLogNueva.UsuarioModificacion = usuario;
                OportunidadLogNueva.Estado = true;

                if (OportunidadLogNueva.IdFaseOportunidadAnt != OportunidadLogNueva.IdFaseOportunidad)
                {
                    PreCalculadaCambioFase = new PreCalculadaCambioFaseBO();
                    PreCalculadaCambioFase.IdPersonal = OportunidadLogNueva.IdPersonalAsignado;
                    PreCalculadaCambioFase.Fecha = DateTime.Now;
                    PreCalculadaCambioFase.IdCategoriaOrigen = OportunidadLogNueva.IdCategoriaOrigen;
                    PreCalculadaCambioFase.IdCentroCosto = OportunidadLogNueva.IdCentroCosto;
                    PreCalculadaCambioFase.IdFaseOportunidadDestino = OportunidadLogNueva.IdFaseOportunidad;
                    PreCalculadaCambioFase.IdFaseOportunidadOrigen = OportunidadLogNueva.IdFaseOportunidadAnt;
                    PreCalculadaCambioFase.IdOrigen = this.IdOrigen;
                    PreCalculadaCambioFase.IdTipoDato = this.IdTipoDato;
                    PreCalculadaCambioFase.FechaCreacion = DateTime.Now;
                    PreCalculadaCambioFase.FechaModificacion = DateTime.Now;
                    PreCalculadaCambioFase.UsuarioCreacion = usuario;
                    PreCalculadaCambioFase.UsuarioModificacion = usuario;
                    PreCalculadaCambioFase.Estado = true;

                }

                if (OportunidadCompetidor != null && OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    //_repCalidadProcesamiento.EliminarOportunidadCompetidorDetalle(OportunidadCompetidor.Id, usuario);
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioModificacion = usuario;
                }

                ActividadAntigua = null;
                OportunidadLogAntigua = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '-' + flagError);
            }

        }

        /// Autor: Jashin Salazar
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="mantenerestadooportunidad"> Confirmación para mantener el estado de oportunidad </param>
        /// <param name="datosOportunidad"> Datos de Oportunidad </param>
        /// <returns> Vacio </returns>
        public void FinalizarActividadAlterno(bool mantenerestadooportunidad, OportunidadDTO datosOportunidad, int? IdOcurrenciaActividadAlterno)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                OportunidadLogAntigua = repOportunidadLog.ObtenerUltimoOportunidadLog(ActividadAntigua.IdOportunidad);

                var fechaFinLLamada = DateTime.Now;

                if (ActividadAntigua.IdOcurrenciaAlterno == 0)
                    throw new ArgumentException("Debe seleccionar una ocurrencia");

                ActividadAntigua.DuracionReal = 13;
                ActividadAntigua.IdCentralLlamada = 3;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _repOcurrenciaAlterno.ObtenerOcurrenciaPorActividad((int)ActividadAntigua.IdOcurrenciaAlterno);
                var ocurrenciaActividad = _repOcurrenciaActividadAlterno.ObtenerOcurrenciaActividadPorId(IdOcurrenciaActividadAlterno);

                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_repOcurrencia.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                }
                //MEXICO
                var diferenciaHoraria = _repPersonal.ObtenerDiferenciaHoraria(this.IdPersonalAsignado);
                ActividadAntigua.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;

                // Actualizar Actividad Detalle
                if (!repActividadDetalle.Exist(ActividadAntigua.Id)) throw new Exception("La oportunidad no tiene actividad detalle");

                ActividadNueva = repActividadDetalle.FirstById(ActividadAntigua.Id);

                var _fechareal= DateTime.Now;
                //if (this.IdPersonalAsignado == 5164 || this.IdPersonalAsignado == 5165 || this.IdPersonalAsignado == 5126 || this.IdPersonalAsignado == 5166 || this.IdPersonalAsignado == 5068)
                //{
                //    _fechareal = DateTime.Now.AddHours(-1);
                //}
                if (diferenciaHoraria.Valor != null)
                {
                    _fechareal = DateTime.Now.AddHours((int)diferenciaHoraria.Valor);
                }

                ActividadNueva.FechaReal = _fechareal;

                ActividadNueva.DuracionReal = ActividadAntigua.DuracionReal;
                ActividadNueva.IdEstadoActividadDetalle = ActividadAntigua.IdEstadoActividadDetalle;
                ActividadNueva.Comentario = ActividadAntigua.Comentario;
                //ActividadNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                //ActividadNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                ActividadNueva.IdCentralLlamada = ActividadAntigua.IdCentralLlamada;
                ActividadNueva.FechaModificacion = DateTime.Now;
                ActividadNueva.UsuarioModificacion = usuario;
                ActividadNueva.IdClasificacionPersona = ActividadAntigua.IdClasificacionPersona;
                ActividadNueva.IdOcurrenciaAlterno = ActividadAntigua.IdOcurrenciaAlterno;
                ActividadNueva.IdOcurrenciaActividadAlterno = ActividadAntigua.IdOcurrenciaActividadAlterno;

                if (ocurrenciaActividad.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo)
                {
                    this.IdFaseOportunidad = (int)ocurrenciaActividad.IdFaseOportunidad;
                }

                this.UltimoComentario = ActividadAntigua.Comentario;
                this.UltimaFechaProgramada = ActividadAntigua.FechaProgramada == null ? DateTime.Now : ActividadAntigua.FechaProgramada.Value;

                this.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                this.IdActividadDetalleUltima = ActividadAntigua.Id;

                if (datosOportunidad.IdEstadoOportunidad != 0 && datosOportunidad.IdEstadoOportunidad == ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada && mantenerestadooportunidad)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
                else
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadEjecutada;
                }

                this.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (this.IdFaseOportunidad != 0 && datosOportunidad.IdFaseOportunidad != this.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    this.IdFaseOportunidadMaxima = _repFaseOportunidad.GetFaseMaxima(datosOportunidad.IdFaseOportunidad, this.IdFaseOportunidad);
                }
                this.FechaModificacion = DateTime.Now;
                this.UsuarioModificacion = usuario;

                //Guardar Log
                OportunidadLogNueva = new OportunidadLogBO();
                OportunidadLogNueva.IdClasificacionPersona = OportunidadLogAntigua.IdClasificacionPersona;
                OportunidadLogNueva.IdPersonalAreaTrabajo = OportunidadLogAntigua.IdPersonalAreaTrabajo;
                OportunidadLogNueva.IdCentroCosto = this.IdCentroCosto;
                OportunidadLogNueva.IdPersonalAsignado = this.IdPersonalAsignado;
                OportunidadLogNueva.IdTipoDato = this.IdTipoDato;
                OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                OportunidadLogNueva.IdFaseOportunidad = this.IdFaseOportunidad;
                OportunidadLogNueva.IdOrigen = this.IdOrigen;
                OportunidadLogNueva.IdContacto = this.IdAlumno;
                OportunidadLogNueva.FechaFinLog = OportunidadLogAntigua.FechaLog;
                OportunidadLogNueva.IdCentroCostoAnt = OportunidadLogAntigua.IdCentroCosto;
                OportunidadLogNueva.IdAsesorAnt = OportunidadLogAntigua.IdPersonalAsignado;

                var _fechalog = DateTime.Now;
                //if (this.IdPersonalAsignado == 5164 || this.IdPersonalAsignado == 5165 || this.IdPersonalAsignado == 5126 || this.IdPersonalAsignado == 5166 || this.IdPersonalAsignado == 5068)
                //{
                //    _fechalog = DateTime.Now.AddHours(-1);
                //}

                //MEXICO
                if (diferenciaHoraria.Valor != null)
                {
                    _fechalog = DateTime.Now.AddHours((int)diferenciaHoraria.Valor);
                }

                OportunidadLogNueva.FechaLog = _fechalog;
                OportunidadLogNueva.IdActividadDetalle = ActividadAntigua.Id;
                //OportunidadLogNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                //OportunidadLogNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                OportunidadLogNueva.Comentario = this.UltimoComentario;
                OportunidadLogNueva.IdOportunidad = this.Id;
                OportunidadLogNueva.IdCategoriaOrigen = this.IdCategoriaOrigen;
                OportunidadLogNueva.IdSubCategoriaDato = this.IdSubCategoriaDato;
                OportunidadLogNueva.FechaRegistroCampania = FechaRegistroCampania;
                OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                OportunidadLogNueva.IdClasificacionPersona = this.IdClasificacionPersona;
                OportunidadLogNueva.IdPersonalAreaTrabajo = this.IdPersonalAreaTrabajo;
                OportunidadLogNueva.IdOcurrenciaAlterno = ActividadAntigua.IdOcurrenciaAlterno;
                OportunidadLogNueva.IdOcurrenciaActividadAlterno = ActividadAntigua.IdOcurrenciaActividadAlterno;

                if (OportunidadLogNueva.IdFaseOportunidadAnt != OportunidadLogNueva.IdFaseOportunidad)
                {
                    OportunidadLogNueva.CambioFase = true;
                    OportunidadLogNueva.FechaCambioFase = OportunidadLogNueva.FechaLog;
                    OportunidadLogNueva.FechaCambioFaseAnt = OportunidadLogAntigua.FechaCambioFase;
                    OportunidadLogNueva.CambioFaseAsesor = 1;
                    OportunidadLogNueva.FechaCambioAsesor = OportunidadLogNueva.FechaLog;
                    OportunidadLogNueva.FechaCambioAsesorAnt = OportunidadLogAntigua.FechaCambioAsesor;

                    if (OportunidadLogNueva.IdFaseOportunidad != 0 && OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
                    {
                        OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogNueva.FechaLog;
                        OportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogNueva.FechaCambioFaseIs;
                        OportunidadLogNueva.CambioFaseIs = false;
                    }

                    if (OportunidadLogNueva.IdFaseOportunidad != 0 && OportunidadLogNueva.IdFaseOportunidadAnt != 0 && OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 && OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2)
                    {
                        OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2 + 1;
                    }
                    else
                    {
                        OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2;
                    }
                }
                else
                {
                    OportunidadLogNueva.CambioFase = false;
                    OportunidadLogNueva.FechaCambioFase = OportunidadLogAntigua.FechaCambioFase;
                    OportunidadLogNueva.FechaCambioFaseAnt = OportunidadLogAntigua.FechaCambioFase;//ultima 1***
                    OportunidadLogNueva.FechaCambioFaseIs = OportunidadLogAntigua.FechaCambioFaseIs;
                    OportunidadLogNueva.CambioFaseIs = false;
                    OportunidadLogNueva.CambioFaseAsesor = 0;
                    OportunidadLogNueva.FechaCambioAsesor = OportunidadLogAntigua.FechaCambioAsesor;
                    OportunidadLogNueva.FechaCambioAsesorAnt = OportunidadLogAntigua.FechaCambioAsesor;
                    OportunidadLogNueva.CicloRn2 = OportunidadLogAntigua.CicloRn2;
                }
                OportunidadLogNueva.FechaCreacion = DateTime.Now;
                OportunidadLogNueva.FechaModificacion = DateTime.Now;
                OportunidadLogNueva.UsuarioCreacion = usuario;
                OportunidadLogNueva.UsuarioModificacion = usuario;
                OportunidadLogNueva.Estado = true;

                if (OportunidadLogNueva.IdFaseOportunidadAnt != OportunidadLogNueva.IdFaseOportunidad)
                {
                    PreCalculadaCambioFase = new PreCalculadaCambioFaseBO();
                    PreCalculadaCambioFase.IdPersonal = OportunidadLogNueva.IdPersonalAsignado;
                    PreCalculadaCambioFase.Fecha = DateTime.Now;
                    PreCalculadaCambioFase.IdCategoriaOrigen = OportunidadLogNueva.IdCategoriaOrigen;
                    PreCalculadaCambioFase.IdCentroCosto = OportunidadLogNueva.IdCentroCosto;
                    PreCalculadaCambioFase.IdFaseOportunidadDestino = OportunidadLogNueva.IdFaseOportunidad;
                    PreCalculadaCambioFase.IdFaseOportunidadOrigen = OportunidadLogNueva.IdFaseOportunidadAnt;
                    PreCalculadaCambioFase.IdOrigen = this.IdOrigen;
                    PreCalculadaCambioFase.IdTipoDato = this.IdTipoDato;
                    PreCalculadaCambioFase.FechaCreacion = DateTime.Now;
                    PreCalculadaCambioFase.FechaModificacion = DateTime.Now;
                    PreCalculadaCambioFase.UsuarioCreacion = usuario;
                    PreCalculadaCambioFase.UsuarioModificacion = usuario;
                    PreCalculadaCambioFase.Estado = true;

                }

                if (OportunidadCompetidor != null && OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    //_repCalidadProcesamiento.EliminarOportunidadCompetidorDetalle(OportunidadCompetidor.Id, usuario);
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioModificacion = usuario;
                }

                ActividadAntigua = null;
                OportunidadLogAntigua = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '-' + flagError);
            }

        }

        /// <summary>
        /// Valida si existe una oportunidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto"></param>
        /// <param name="idCentroCosto"></param>
        /// <param name="idOcurrencia"></param>
        /// <returns> Bool </returns>
        public bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            return _repOportunidad.ValidarRN2(idContacto, idCentroCosto, idOcurrencia);
        }

        /// <summary>
        /// Genera oportunidad desde el registro de asignacion automatica
        /// </summary>
        /// <param name="objAsignacionAutomatica">Objeto de clase objAsignacionAutomatica</param>
        /// <param name="ventaCruzada">Flag para determinar si viene de venta cruzada</param>
        /// <param name="usuario">Usuario que realiza la creacion de la oportunidad</param>
        /// <param name="idTipoCategoriaOrigen">Id del tipo de categoria origen (PK de la tabla mkt.T_TipoCategoriaOrigen)</param>
        public void GenerarOportunidad(AsignacionAutomaticaBO objAsignacionAutomatica, bool ventaCruzada, string usuario, int idTipoCategoriaOrigen)
        {
            var asesorAsignacionAutomatica = 125;
            var tipoDatoLanzamiento = ValorEstatico.IdTipoDatoLanzamiento;

            if (objAsignacionAutomatica.IdAlumno != 0 && this.Alumno.Asociado == true) //si el alumno esta en finanzas no actualizamos todos sus campos
            {
                // Reemplazamos los nuevos campos del registro
                this.Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria != 0 && objAsignacionAutomatica.IdIndustria != null ? objAsignacionAutomatica.IdIndustria : this.Alumno.IdIndustria;
                this.Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion != 0 && objAsignacionAutomatica.IdAreaFormacion != null ? objAsignacionAutomatica.IdAreaFormacion : this.Alumno.IdAformacion;
                this.Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo != 0 && objAsignacionAutomatica.IdAreaTrabajo != null ? objAsignacionAutomatica.IdAreaTrabajo : this.Alumno.IdAtrabajo;
                this.Alumno.IdCargo = objAsignacionAutomatica.IdCargo != 0 && objAsignacionAutomatica.IdCargo != null ? objAsignacionAutomatica.IdCargo : this.Alumno.IdCargo;

                this.Alumno.Celular = objAsignacionAutomatica.Celular;
                this.Alumno.IdCodigoRegionCiudad = (objAsignacionAutomatica.IdCiudad == -1 || objAsignacionAutomatica.IdCiudad == 0) ? this.Alumno.IdCodigoRegionCiudad : objAsignacionAutomatica.IdCiudad;
                this.Alumno.IdCodigoPais = (objAsignacionAutomatica.IdPais == -1 || objAsignacionAutomatica.IdPais == 0) ? this.Alumno.IdCodigoPais : objAsignacionAutomatica.IdPais;
                this.Alumno.Email2 = objAsignacionAutomatica.Email;
                this.Alumno.UsuarioModificacion = "SYSTEM";
                this.Alumno.FechaModificacion = DateTime.Now;
            }
            else if (objAsignacionAutomatica.IdAlumno != 0 && objAsignacionAutomatica.IdAlumno != null)
            {
                // Reemplazamos los nuevos campos del registro
                this.Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria != 0 && objAsignacionAutomatica.IdIndustria != null ? objAsignacionAutomatica.IdIndustria : this.Alumno.IdIndustria;
                this.Alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                this.Alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                this.Alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                this.Alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                this.Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion != 0 && objAsignacionAutomatica.IdAreaFormacion != null ? objAsignacionAutomatica.IdAreaFormacion : this.Alumno.IdAformacion;
                this.Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo != 0 && objAsignacionAutomatica.IdAreaTrabajo != null ? objAsignacionAutomatica.IdAreaTrabajo : this.Alumno.IdAtrabajo;
                this.Alumno.IdCargo = objAsignacionAutomatica.IdCargo != 0 && objAsignacionAutomatica.IdCargo != null ? objAsignacionAutomatica.IdCargo : this.Alumno.IdCargo;
                this.Alumno.Celular = objAsignacionAutomatica.Celular;
                this.Alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                this.Alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                this.Alumno.Telefono = objAsignacionAutomatica.Telefono;
                this.Alumno.Email1 = objAsignacionAutomatica.Email;
                this.Alumno.Email2 = objAsignacionAutomatica.Email;
                this.Alumno.Estado = true;
                this.Alumno.UsuarioModificacion = "SYSTEM";
                this.Alumno.FechaModificacion = DateTime.Now;
            }
            else
            {
                //Reemplazamos los nuevos campos del registro
                this.Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria;
                this.Alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                this.Alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                this.Alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                this.Alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                this.Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion;
                this.Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo;
                this.Alumno.IdCargo = objAsignacionAutomatica.IdCargo;
                this.Alumno.Celular = objAsignacionAutomatica.Celular;
                this.Alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                this.Alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                this.Alumno.Telefono = objAsignacionAutomatica.Telefono;
                this.Alumno.Email1 = objAsignacionAutomatica.Email;
                this.Alumno.Email2 = objAsignacionAutomatica.Email;
                this.Alumno.Estado = true;
                this.Alumno.UsuarioCreacion = "GENOPO";
                this.Alumno.UsuarioModificacion = "SYSTEM";
                this.Alumno.FechaModificacion = DateTime.Now;
                this.Alumno.FechaCreacion = DateTime.Now;
            }
            //Generamos la Oportunidad
            //Si el tipo de dato no es LANZAMIENTO, lo asignamos al asesor de Asignacion Automatica
            this.IdPersonalAsignado = asesorAsignacionAutomatica;


            this.IdFaseOportunidad = objAsignacionAutomatica.IdFaseOportunidad == null ? 0 : objAsignacionAutomatica.IdFaseOportunidad.Value;
            this.IdCentroCosto = objAsignacionAutomatica.IdCentroCosto == null ? 0 : objAsignacionAutomatica.IdCentroCosto.Value;
            this.IdOrigen = objAsignacionAutomatica.IdOrigen == null ? 0 : objAsignacionAutomatica.IdOrigen.Value;
            this.IdTipoDato = objAsignacionAutomatica.IdTipoDato == null ? 0 : objAsignacionAutomatica.IdTipoDato.Value;
            this.IdConjuntoAnuncio = objAsignacionAutomatica.IdConjuntoAnuncio == null ? 0 : objAsignacionAutomatica.IdConjuntoAnuncio.Value;

            this.IdAnuncioFacebook = objAsignacionAutomatica.IdAnuncioFacebook;

            this.IdTiempoCapacitacion = objAsignacionAutomatica.IdTiempoCapacitacion == null ? 0 : objAsignacionAutomatica.IdTiempoCapacitacion.Value;
            this.IdPagina = objAsignacionAutomatica.IdPagina == null ? 0 : objAsignacionAutomatica.IdPagina.Value;
            this.FechaRegistroCampania = objAsignacionAutomatica.FechaRegistroCampania.Value;
            this.IdFaseOportunidadPortal = objAsignacionAutomatica.IdFaseOportunidadPortal == null ? Guid.Empty : objAsignacionAutomatica.IdFaseOportunidadPortal;
            this.IdCampaniaScoring = objAsignacionAutomatica.IdCampaniaScoring == null ? 0 : objAsignacionAutomatica.IdCampaniaScoring.Value;
            this.UltimaFechaProgramada = objAsignacionAutomatica.FechaProgramada;
            this.UltimoComentario = "Sin Comentario";
            this.IdAlumno = this.Alumno.Id == 0 ? this.IdAlumno : this.Alumno.Id;
            this.IdCategoriaOrigen = objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value;
            this.IdInteraccionFormulario = objAsignacionAutomatica.IdInteraccionFormulario == null ? 0 : objAsignacionAutomatica.IdInteraccionFormulario.Value;
            this.UrlOrigen = objAsignacionAutomatica.UrlOrigen;
            this.IdSubCategoriaDato = objAsignacionAutomatica.IdSubCategoriaDato == null ? 0 : objAsignacionAutomatica.IdSubCategoriaDato.Value;
            this.IdClasificacionPersona = objAsignacionAutomatica.idClasificacionPersona;
            this.IdPersonalAreaTrabajo = 8;

            if (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)//16=>TipoCategoriaOrigen = Chat , 38=>TipoCategoriaOrigen = Chat2
            {
                //obtenemos el idasesor asignado a ese programa por su cc segun chat
                var asignadoChat = _repOportunidad.ObtenerIdPersonalAsignadoChat(this.IdAlumno, this.IdCentroCosto.Value);
                if (asignadoChat == null || asignadoChat.IdAsesor == 0)
                {
                    //nada
                }
                else
                {
                    this.IdPersonalAsignado = asignadoChat.IdAsesor;
                }
            }
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Programar Actividad
        /// </summary>
        /// <returns> Vacio </returns>
        public void ProgramaActividad()
        {
            try
            {
                var ocurrencia = _repOcurrencia.ObtenerOcurrenciaPorActividad(ActividadNueva.IdOcurrencia);
                if (ActividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }

                if (ActividadNueva.IdOcurrencia == 0 || ActividadNueva.IdOcurrencia == null)
                {
                    ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                    //guardar el logger
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        if (_repOcurrencia.ValidarEstadoOcurrencia(ocurrencia.Id))
                        {
                            ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                        }
                        if (ocurrencia.IdActividadCabecera == null)
                        {
                            if (!(ActividadNueva.IdActividadCabecera == null))
                            {
                                ActividadNueva.IdActividadCabecera = ActividadNueva.IdActividadCabecera;
                            }
                            else
                            {
                                ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                            }
                        }
                        else
                        {
                            if (this.IdFaseOportunidad == 22 && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                                                                                      //if (this.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                            {
                                ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                            }
                            else
                            {
                                ActividadNueva.IdActividadCabecera = ocurrencia.IdActividadCabecera;
                            }
                        }
                    }
                }
                ActividadNuevaProgramarActividad = new ActividadDetalleBO();
                ActividadNuevaProgramarActividad.IdOportunidad = ActividadNueva.IdOportunidad;
                ActividadNuevaProgramarActividad.IdAlumno = ActividadNueva.IdAlumno;
                ActividadNuevaProgramarActividad.Actor = "A";
                ActividadNuevaProgramarActividad.FechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                ActividadNuevaProgramarActividad.IdEstadoActividadDetalle = EstadoActividadDetalleBO.EstadoActividadDetalleNoEjecutado;
                ActividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                ActividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                ActividadNuevaProgramarActividad.UsuarioCreacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.UsuarioModificacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.Estado = true;
                ActividadNuevaProgramarActividad.IdActividadCabecera = ActividadNueva.IdActividadCabecera;
                ActividadNuevaProgramarActividad.IdOcurrencia = ActividadNueva.IdOcurrencia;
                ActividadNuevaProgramarActividad.IdOcurrenciaActividad = ActividadNueva.IdOcurrenciaActividad;
                ActividadNuevaProgramarActividad.IdClasificacionPersona = this.IdClasificacionPersona;
                //Actualiza Oportunidad

                this.IdActividadDetalleUltima = ActividadNueva.Id;
                this.IdActividadCabeceraUltima = ActividadNueva.IdActividadCabecera.Value;
                this.IdEstadoActividadDetalleUltimoEstado = ActividadNueva.IdEstadoActividadDetalle;
                this.UltimaFechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                this.UltimoComentario = ActividadNueva.Comentario;

                if (ActividadNueva.IdOcurrencia == 35) //OCURRENCIA_ASIGNACION_MANUAL
                {
                    this.IdEstadoOportunidad = EstadoOportunidadBO.estadoReasignada;
                }
                else
                {
                    this.IdEstadoOportunidad = this.UltimaFechaProgramada.HasValue ? ValorEstatico.IdEstadoOportunidadProgramada : ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                var grupoPrelanzamiento = _repOcurrencia.ValidarGrupoPreLanzamiento(this.IdCategoriaOrigen);

                if (this.IdEstadoOportunidad == 2 && ocurrencia.IdEstadoOcurrencia == 2 && grupoPrelanzamiento == 1 && this.IdFaseOportunidad == 2)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                if (this.IdEstadoOportunidad != 0 && this.IdEstadoOportunidad.Equals(ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada) && false)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 26/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Programar Actividad alternos
        /// </summary>
        /// <returns> Vacio </returns>
        public void ProgramaActividadAlterno()
        {
            try
            {
                //var ocurrencia = _repOcurrenciaAlterno.ObtenerOcurrenciaPorActividad(ActividadNueva.IdOcurrencia);
                var ocurrencia = _repOcurrenciaAlterno.ObtenerOcurrenciaPorActividad((int)ActividadNueva.IdOcurrenciaAlterno);
                var ocurrenciaActividad = _repOcurrenciaActividadAlterno.ObtenerOcurrenciaActividadPorId((int)ActividadNueva.IdOcurrenciaActividadAlterno);
                if (ActividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }

                if (ActividadNueva.IdOcurrenciaAlterno == 0 || ActividadNueva.IdOcurrenciaAlterno == null)
                {
                    ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                    //guardar el logger
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        if (_repOcurrencia.ValidarEstadoOcurrencia(ocurrencia.Id))
                        {
                            ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                        }
                        if (ocurrenciaActividad.IdActividadCabeceraProgramada == null)
                        {
                            if (!(ocurrenciaActividad.IdActividadCabeceraProgramada == null))
                            {
                                ActividadNueva.IdActividadCabecera = ocurrenciaActividad.IdActividadCabeceraProgramada;
                            }
                            else
                            {
                                ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                            }
                        }
                        else
                        {
                            if (this.IdFaseOportunidad == 22 && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                                                                                      //if (this.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                            {
                                ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this.IdFaseOportunidad, this.IdTipoDato, this.IdPersonalAreaTrabajo, this.IdActividadCabeceraUltima);
                            }
                            else
                            {
                                ActividadNueva.IdActividadCabecera = ocurrenciaActividad.IdActividadCabeceraProgramada;
                            }
                        }
                    }
                }
                ActividadNuevaProgramarActividad = new ActividadDetalleBO();
                ActividadNuevaProgramarActividad.IdOportunidad = ActividadNueva.IdOportunidad;
                ActividadNuevaProgramarActividad.IdAlumno = ActividadNueva.IdAlumno;
                ActividadNuevaProgramarActividad.Actor = "A";
                ActividadNuevaProgramarActividad.FechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                ActividadNuevaProgramarActividad.IdEstadoActividadDetalle = EstadoActividadDetalleBO.EstadoActividadDetalleNoEjecutado;
                ActividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                ActividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                ActividadNuevaProgramarActividad.UsuarioCreacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.UsuarioModificacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.Estado = true;
                ActividadNuevaProgramarActividad.IdActividadCabecera = ActividadNueva.IdActividadCabecera;
                //ActividadNuevaProgramarActividad.IdOcurrencia = ActividadNueva.IdOcurrencia;
                //ActividadNuevaProgramarActividad.IdOcurrenciaActividad = ActividadNueva.IdOcurrenciaActividad;
                ActividadNuevaProgramarActividad.IdClasificacionPersona = this.IdClasificacionPersona;
                ActividadNuevaProgramarActividad.IdOcurrenciaAlterno = ActividadNueva.IdOcurrenciaAlterno;
                ActividadNuevaProgramarActividad.IdOcurrenciaActividadAlterno = ActividadNueva.IdOcurrenciaActividadAlterno;
                //Actualiza Oportunidad

                this.IdActividadDetalleUltima = ActividadNueva.Id;
                this.IdActividadCabeceraUltima = ActividadNueva.IdActividadCabecera.Value;
                this.IdEstadoActividadDetalleUltimoEstado = ActividadNueva.IdEstadoActividadDetalle;
                this.UltimaFechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                this.UltimoComentario = ActividadNueva.Comentario;

                if (ActividadNueva.IdOcurrenciaAlterno == 35) //OCURRENCIA_ASIGNACION_MANUAL
                {
                    this.IdEstadoOportunidad = EstadoOportunidadBO.estadoReasignada;
                }
                else
                {
                    this.IdEstadoOportunidad = this.UltimaFechaProgramada.HasValue ? ValorEstatico.IdEstadoOportunidadProgramada : ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                var grupoPrelanzamiento = _repOcurrencia.ValidarGrupoPreLanzamiento(this.IdCategoriaOrigen);

                if (this.IdEstadoOportunidad == 2 && ocurrencia.IdEstadoOcurrencia == 2 && grupoPrelanzamiento == 1 && this.IdFaseOportunidad == 2)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                if (this.IdEstadoOportunidad != 0 && this.IdEstadoOportunidad.Equals(ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada) && false)
                {
                    this.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene etiquetas
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns> Retorna cadena de etiquetas </returns>
        /// <returns> List<string[]> </returns>
        private List<string[]> obtenerEtiquetas(string cadena)
        {
            List<string> etiquetas = cadena.Split('{').Where(o => o.Contains("}")).Select(o => o.Split('}').First()).ToList();

            List<string[]> rpta = new List<string[]>();
            foreach (string etiqueta in etiquetas)
            {
                string[] etqVal = new string[2];
                etqVal[0] = etiqueta;
                etqVal[1] = "";
                rpta.Add(etqVal);
            }

            return rpta;
        }

        /// <summary>
        /// Genera oportunidad de operaciones
        /// </summary>
        /// <param name="integraDBContext"> Contexto de Integra </param>
        /// <returns> Vacio </returns>
        public void GenerarOportunidadOperaciones(integraDBContext integraDBContext)
        {
            AlumnoRepositorio _repAlumno = new AlumnoRepositorio(integraDBContext);
            this.Alumno = _repAlumno.FirstById(this.IdAlumno);
        }

        /// <summary>
        /// Genera oportunidad de operaciones con parámetros
        /// </summary>
        /// <param name="idOportunidad"> Id de oportunidad </param>
        /// <param name="usuario"> Usuario </param>
        /// <param name="idCentroCosto"> Id de centro de costo </param>
        /// <param name="idActividadCabecera"> Id de Actividad de Cabecera </param>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="idMatriculaCabecera"> Id de Matrícula de Cabecera </param>
        /// <returns> oportunidadBO a partir de un string serializado : OportunidadBO </returns>
        public OportunidadBO GenerarOportunidadOperacionesConParametros(int idOportunidad, string usuario, int idCentroCosto, int idActividadCabecera, int idPersonal, int idMatriculaCabecera)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                string res = "";
                string URI = "https://integraV4-servicios.bsginstitute.com/api/Oportunidad/GenerarOportunidadOperaciones/" + idOportunidad + "/" + usuario + "/" + idCentroCosto + "/" + idActividadCabecera + "/" + idPersonal + "/" + idMatriculaCabecera;
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    res = wc.DownloadString(URI);
                }
                return _repOportunidad.ObtenerOportunidadOperaciones(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Finaliza Actividad de Asignación Manual de Operaciones   
        /// </summary>
        /// <param name="mantenerestadooportunidad"> Bool de confirmación para mantener la fase de oportunidad </param>
        /// <param name="datosOportunidad"> Información de oportunidad </param>
        /// <returns></returns>        
        public void FinalizarActividadAsignacionManualOperaciones(bool mantenerestadooportunidad, OportunidadDTO datosOportunidad)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                OportunidadLogAntigua = repOportunidadLog.GetBy(w => w.IdOportunidad == ActividadAntigua.IdOportunidad).OrderByDescending(y => y.FechaCreacion).FirstOrDefault();

                var fechaFinLLamada = DateTime.Now;

                flagError = "ObtenerOcurrenciaPorActividad";

                ActividadNueva = repActividadDetalle.FirstById(ActividadAntigua.Id);
                ActividadNueva.FechaReal = DateTime.Now;
                ActividadNueva.DuracionReal = ActividadAntigua.DuracionReal;
                ActividadNueva.IdEstadoActividadDetalle = ActividadAntigua.IdEstadoActividadDetalle;
                ActividadNueva.Comentario = ActividadAntigua.Comentario;
                ActividadNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                ActividadNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                ActividadNueva.IdCentralLlamada = ActividadAntigua.IdCentralLlamada;
                ActividadNueva.FechaModificacion = DateTime.Now;
                ActividadNueva.UsuarioModificacion = usuario;
                ActividadNueva.IdClasificacionPersona = ActividadAntigua.IdClasificacionPersona;

                this.UltimoComentario = ActividadAntigua.Comentario;
                this.IdActividadDetalleUltima = ActividadAntigua.Id;

                this.FechaModificacion = DateTime.Now;
                this.UsuarioModificacion = usuario;

                //Guardar Log
                OportunidadLogNueva = new OportunidadLogBO();
                if (OportunidadLogAntigua == null)
                {
                    OportunidadLogNueva.IdClasificacionPersona = this.IdClasificacionPersona;
                    OportunidadLogNueva.IdPersonalAreaTrabajo = this.IdPersonalAreaTrabajo;
                    OportunidadLogNueva.FechaFinLog = DateTime.Now;
                    OportunidadLogNueva.IdCentroCostoAnt = this.IdCentroCosto;
                    OportunidadLogNueva.IdAsesorAnt = this.IdPersonalAsignado;
                }
                else
                {
                    OportunidadLogNueva.IdClasificacionPersona = OportunidadLogAntigua.IdClasificacionPersona;
                    OportunidadLogNueva.IdPersonalAreaTrabajo = OportunidadLogAntigua.IdPersonalAreaTrabajo;
                    OportunidadLogNueva.FechaFinLog = OportunidadLogAntigua.FechaLog;
                    OportunidadLogNueva.IdCentroCostoAnt = OportunidadLogAntigua.IdCentroCosto;
                    OportunidadLogNueva.IdAsesorAnt = OportunidadLogAntigua.IdPersonalAsignado;

                }

                OportunidadLogNueva.IdCentroCosto = this.IdCentroCosto;
                OportunidadLogNueva.IdPersonalAsignado = this.IdPersonalAsignado;
                OportunidadLogNueva.IdTipoDato = this.IdTipoDato;
                OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                OportunidadLogNueva.IdFaseOportunidad = this.IdFaseOportunidad;
                OportunidadLogNueva.IdOrigen = this.IdOrigen;
                OportunidadLogNueva.IdContacto = this.IdAlumno;

                OportunidadLogNueva.FechaLog = DateTime.Now;
                OportunidadLogNueva.IdActividadDetalle = ActividadAntigua.Id;
                OportunidadLogNueva.IdOcurrencia = ActividadAntigua.IdOcurrencia;
                OportunidadLogNueva.IdOcurrenciaActividad = ActividadAntigua.IdOcurrenciaActividad;
                OportunidadLogNueva.Comentario = this.UltimoComentario;
                OportunidadLogNueva.IdOportunidad = this.Id;
                OportunidadLogNueva.IdCategoriaOrigen = this.IdCategoriaOrigen;
                OportunidadLogNueva.IdSubCategoriaDato = this.IdSubCategoriaDato;
                OportunidadLogNueva.FechaRegistroCampania = FechaRegistroCampania;
                OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                OportunidadLogNueva.IdClasificacionPersona = this.IdClasificacionPersona;
                OportunidadLogNueva.IdPersonalAreaTrabajo = this.IdPersonalAreaTrabajo;
                OportunidadLogNueva.CambioFase = false;
                OportunidadLogNueva.CambioFaseIs = false;
                OportunidadLogNueva.CambioFaseAsesor = 0;
                OportunidadLogNueva.FechaCreacion = DateTime.Now;
                OportunidadLogNueva.FechaModificacion = DateTime.Now;
                OportunidadLogNueva.UsuarioCreacion = usuario;
                OportunidadLogNueva.UsuarioModificacion = usuario;
                OportunidadLogNueva.Estado = true;

                ActividadAntigua = null;
                OportunidadLogAntigua = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + flagError);
            }

        }
        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Programa Actividades de Asignación Manual de operaciones    
        /// </summary>
        /// <param></param>
        /// <returns></returns>        
        public void ProgramaActividadAsignacionManualOperaciones()
        {
            try
            {

                if (ActividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }
                ActividadNueva.IdActividadCabecera = 63;
                ActividadNuevaProgramarActividad = new ActividadDetalleBO();
                ActividadNuevaProgramarActividad.IdOportunidad = ActividadNueva.IdOportunidad;
                ActividadNuevaProgramarActividad.IdAlumno = ActividadNueva.IdAlumno;
                ActividadNuevaProgramarActividad.Actor = "A";
                ActividadNuevaProgramarActividad.FechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                ActividadNuevaProgramarActividad.IdEstadoActividadDetalle = EstadoActividadDetalleBO.EstadoActividadDetalleNoEjecutado;
                ActividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                ActividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                ActividadNuevaProgramarActividad.UsuarioCreacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.UsuarioModificacion = this.UsuarioModificacion;
                ActividadNuevaProgramarActividad.Estado = true;
                ActividadNuevaProgramarActividad.IdActividadCabecera = ActividadNueva.IdActividadCabecera;
                ActividadNuevaProgramarActividad.IdOcurrencia = ActividadNueva.IdOcurrencia;
                ActividadNuevaProgramarActividad.IdOcurrenciaActividad = ActividadNueva.IdOcurrenciaActividad;
                ActividadNuevaProgramarActividad.IdClasificacionPersona = ActividadNueva.IdClasificacionPersona;

                this.IdActividadDetalleUltima = ActividadNueva.Id;
                this.IdActividadCabeceraUltima = ActividadNueva.IdActividadCabecera.Value;
                this.IdEstadoActividadDetalleUltimoEstado = ActividadNueva.IdEstadoActividadDetalle;
                this.UltimaFechaProgramada = this.UltimaFechaProgramada.HasValue ? this.UltimaFechaProgramada.Value : default(DateTime);
                this.UltimoComentario = ActividadNueva.Comentario;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtener probabildiades de modelos predictivos
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> Obtiene Probabilidad según un modelo predictivo : ProbabilidadOportunidadResumenDTO </returns>
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivo(int idOportunidad)
        {
            ProbabilidadOportunidadResumenDTO probabilidad;

            using (WebClient wc = new WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    //var url = $"https://integrav4-ia-modelopredictivo.bsginstitute.com/api/ProbalidadInscripcionV4/ObtenerProbabilidad?idOportunidad={idOportunidad}";
                    var url = $"https://integrav4-ia-modelopredictivo.bsginstitute.com/api/ProbalidadInscripcionV4/ObtenerProbabilidad?idOportunidad={idOportunidad}";
                    var rpta = wc.DownloadString(new Uri(url));
                    probabilidad = JsonConvert.DeserializeObject<ProbabilidadOportunidadResumenDTO>(rpta);
                    return probabilidad;
                }
                catch (WebException e)
                {
                    string responseText = "";

                    var responseStream = e.Response?.GetResponseStream();

                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseText = reader.ReadToEnd();
                        }
                    }

                    if (string.IsNullOrEmpty(responseText))
                        responseText = e.Message;

                    throw new Exception(responseText);
                }
            }
        }
    }

    public class ActividadCabeceraUtil
    {
        private static Dictionary<int, Dictionary<int, int>> diccionarioActividadesCabecera = new Dictionary<int, Dictionary<int, int>>();

        private static readonly int faseBNC = ValorEstatico.IdFaseOportunidadBNC;
        private static readonly int faseIC = ValorEstatico.IdFaseOportunidadIC;
        private static readonly int faseIP = ValorEstatico.IdFaseOportunidadIP;
        private static readonly int faseIS = ValorEstatico.IdFaseOportunidadIS;
        private static readonly int faseIT = ValorEstatico.IdFaseOportunidadIT;
        private static readonly int fasePF = ValorEstatico.IdFaseOportunidadPF;
        private static readonly int faseRN = ValorEstatico.IdFaseOportunidadRN;
        private static readonly int faseRN2 = ValorEstatico.IdFaseOportunidadRN2;

        private static readonly int tipoDatoHistorico = ValorEstatico.IdTipoDatoHistorico;
        private static readonly int tipoDatoLanzamiento = ValorEstatico.IdTipoDatoLanzamiento;

        private static readonly int valorVacio = 0;

        private static readonly int llamadaDeConfirmacionDePago = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionPago;
        private static readonly int llamadaDeConfirmacionDeLlenadoDeFichaDeMatricula = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRegistroPW;
        private static readonly int llamadaDeContactoInicial = ValorEstatico.IdActividadCabeceraLlamadaContactoInicial;
        private static readonly int llamadaDeConfirmacionDeRevisionDeInformacion = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRevisionInfo;
        private static readonly int llamadaDeCierre = ValorEstatico.IdActividadCabeceraLlamadaCierre;
        private static readonly int llamadaDeConfirmacionDeInteresPorElPrograma = ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis;
        private static readonly int llamadaConfirmacionDeSeguimientoDeRN = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN;
        private static readonly int llamadaConfirmacionDeEnvioDeDocumentos = ValorEstatico.IdActividadCabeceraLlamadaConfirEnvioDoc;
        private static readonly int envioCorreoConInformacionInicial = ValorEstatico.IdActividadCabeceraPrimerContactoClienteProbMedia;

        //operaciones 
        private static readonly int IdActividadCabeceraLlamadaSeguimiento = ValorEstatico.IdActividadCabeceraLlamadaSeguimiento;

        /// <summary>
        /// Construye diccionario
        /// </summary>
        /// <param name="llave"> Clave de diccionario </param>
        /// <param name="valor"> Valor de diccionario </param>
        /// <returns> Vacio </returns>
        private static Dictionary<int, int> BuilderDictionary(int llave, int valor)
        {
            var dic = new Dictionary<int, int>
            {
                { llave, valor }
            };
            return dic;
        }

        /// Autor: ----------
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Inicializa Actividades Cabecera
        /// </summary>
        /// <returns> Vacio </returns>
        private static void InitActividadesCabecera()
        {
            diccionarioActividadesCabecera.Add(faseBNC, BuilderDictionary(tipoDatoHistorico, llamadaDeConfirmacionDeInteresPorElPrograma));
            diccionarioActividadesCabecera[faseBNC].Add(tipoDatoLanzamiento, llamadaDeContactoInicial);
            diccionarioActividadesCabecera.Add(faseIT, BuilderDictionary(valorVacio, llamadaDeConfirmacionDeRevisionDeInformacion));
            diccionarioActividadesCabecera.Add(faseIP, BuilderDictionary(valorVacio, llamadaDeCierre));
            diccionarioActividadesCabecera.Add(fasePF, BuilderDictionary(valorVacio, llamadaDeConfirmacionDeLlenadoDeFichaDeMatricula));
            diccionarioActividadesCabecera.Add(faseIC, BuilderDictionary(valorVacio, llamadaDeConfirmacionDePago));
            diccionarioActividadesCabecera.Add(faseIS, BuilderDictionary(valorVacio, llamadaConfirmacionDeEnvioDeDocumentos));
            diccionarioActividadesCabecera.Add(faseRN, BuilderDictionary(valorVacio, llamadaConfirmacionDeSeguimientoDeRN));
            diccionarioActividadesCabecera.Add(faseRN2, BuilderDictionary(valorVacio, llamadaDeConfirmacionDeInteresPorElPrograma));
        }


        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Actividad Cabecera
        /// </summary>
        /// <param name="idFase"> Id de Fase </param>
        /// <param name="idTipodato"> Id de tipo de dato </param>
        /// <param name="idPersonaAreaTrabajo"> Id de Área de trabajo de personal </param>
        /// <param name="idActividadCabecera"> Id de actividad cabecera </param>
        /// <param name="probabilidad"> Valor de probabilidad </param>
        /// <returns> Diccionario clave valor : Dictionary<int, int> </returns>
        public static int ObtenerActividadCabecera(int idFase, int idTipodato, int? idPersonaAreaTrabajo, int? idActividadCabecera, decimal? probabilidad = -1)
        {
            if (idPersonaAreaTrabajo.HasValue)
            {
                if (idPersonaAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                {
                    if (idActividadCabecera.HasValue)
                    {
                        return idActividadCabecera.Value;
                    }
                    else
                    {
                        return ValorEstatico.IdActividadCabeceraLlamadaSeguimiento;
                    }

                }
            }

            if (probabilidad.Equals(-1)) return envioCorreoConInformacionInicial;

            if (diccionarioActividadesCabecera == null || (diccionarioActividadesCabecera != null && diccionarioActividadesCabecera.Count == 0))
            {
                InitActividadesCabecera();
            }
            //Buscamos la actividad Cabecera
            //Default
            if (!diccionarioActividadesCabecera.ContainsKey(idFase))
            {
                return llamadaDeConfirmacionDeInteresPorElPrograma;
            }
            else
            {
                var dic = diccionarioActividadesCabecera[idFase];
                if (dic.ContainsKey(idTipodato))
                {
                    return dic[idTipodato];
                }
                else if (dic.ContainsKey(valorVacio))
                {
                    return dic[valorVacio];
                }
                return llamadaDeConfirmacionDeInteresPorElPrograma;
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/FiltroSegmento
    /// Autor: Wilber Choque - Richard Zenteno - Johan Cayo - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Configura las actividades automaticas de la interfaz FiltroSegmento
    /// </summary>
    [Route("api/FiltroSegmento")]
    public class FiltroSegmentoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly FiltroSegmentoRepositorio _repFiltroSegmento;
        public FiltroSegmentoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroSegmentoPanel()
        {
            try
            {
                FiltroSegmentoRepositorio _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
                return Ok(_repFiltroSegmento.ObtenerFiltroSegmentoPanel());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDataCombosFiltroSegmento()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                var listaAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro();

                var _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                var listaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerSubAreasParaFiltro();

                var _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                var listaProgramaGeneral = _repPGeneral.ObtenerProgramasFiltroDeSubAreasCapacitacion();

                var _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                var listaProgramaEspecifico = _repPEspecifico.ObtenerListaProgramaEspecificoParaFiltroDeProgramaGeneral();

                var _repFaseOportunidad  = new FaseOportunidadRepositorio(_integraDBContext);
                var listaFaseOportunidad = _repFaseOportunidad.ObtenerTodoFiltro();

                var _prePais = new PaisRepositorio(_integraDBContext);
                var listaPais = _prePais.ObtenerPaisesCombo();

                var _repCiudad = new CiudadRepositorio(_integraDBContext);
                var listaCiudad = _repCiudad.ObtenerCiudadesPorPais();

                var _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                var listaTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro();

                var _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
                var listaCategoriaOrigen = _repCategoriaOrigen.ObtenerCategoriaFiltroGrupo();

                var _repCargo = new CargoRepositorio(_integraDBContext);
                var listaCargo = _repCargo.ObtenerCargoFiltro();

                var _repIndustria = new IndustriaRepositorio(_integraDBContext);
                var listaIndustria = _repIndustria.ObtenerIndustriaFiltro();

                var  _repAreaFormacion  = new AreaFormacionRepositorio(_integraDBContext);
                var listaAreaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro();

                var _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                var listaAreaTrabajo = _repAreaTrabajo.ObtenerAreasTrabajo();


                var _repOperadorComparacion = new OperadorComparacionRepositorio(_integraDBContext);
                var listaOperadorComparacion = _repOperadorComparacion.ObtenerListaOperadorComparacion();

                var listaOperadorComparacionValida = new List<int>() { 1, 2, 3, 4, 5 };
                listaOperadorComparacion = listaOperadorComparacion.Where(x => listaOperadorComparacionValida.Contains(x.Id)).ToList();

                var _repTipoFormulario = new TipoFormularioRepositorio(_integraDBContext);
                var listaTipoFormulario = _repTipoFormulario.ObtenerListaTipoFormulario();

                var _repTipoInteraccion = new TipoInteraccionRepositorio(_integraDBContext);
                var listaTipoInteraccion = _repTipoInteraccion.ObtenerPorTipoInteraccionGeneralFormulario();

                var _repTiempoFrecuencia = new TiempoFrecuenciaRepositorio(_integraDBContext);
                var listaTiempoFrecuencia = _repTiempoFrecuencia.ObtenerListaTiempoFrecuencia();
                var listaTiempoFrecuenciaEnvioAutomaticoCreacion = listaTiempoFrecuencia;
                var listaTiempoFrecuenciaEnvioAutomaticoUltima = listaTiempoFrecuencia;

                var listaTiempoFrecuenciaValida = new List<int>() { 2, 3, 4, 5 };
                listaTiempoFrecuencia = listaTiempoFrecuencia.Where(x => listaTiempoFrecuenciaValida.Contains(x.Id)).ToList();

                var listaTiempoFrecuenciaEnvioAutomaticoCreacionValida = new List<int>() { 2, 6, 7 };
                listaTiempoFrecuenciaEnvioAutomaticoCreacion = listaTiempoFrecuenciaEnvioAutomaticoCreacion.Where(x => listaTiempoFrecuenciaEnvioAutomaticoCreacionValida.Contains(x.Id)).ToList();
                var listaTiempoFrecuenciaEnvioAutomaticoUltimaValida = new List<int>() { 2, 3, 6 };
                listaTiempoFrecuenciaEnvioAutomaticoUltima = listaTiempoFrecuenciaEnvioAutomaticoUltima.Where(x => listaTiempoFrecuenciaEnvioAutomaticoUltimaValida.Contains(x.Id)).ToList();

                var _repEstadoActividadDetalle = new EstadoActividadDetalleRepositorio(_integraDBContext);
                var listaEstadoActividadDetalle = _repEstadoActividadDetalle.ObtenerDetalleActividadFiltroCodigo();


                var _repProbabilidadRegistroPW = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
                var listaProbabilidadRegistro = _repProbabilidadRegistroPW.ObtenerTodoFiltro();

                var _repEmbudoNivel = new EmbudoNivelRepositorio(_integraDBContext);
                var listaEmbudoNivel = _repEmbudoNivel.ObtenerEmbudoNivel();

                //Nuevos tabs
                var  _repFiltroSegmentoTipoContacto = new FiltroSegmentoTipoContactoRepositorio(_integraDBContext);
                var listaFiltroSegmentoTipoContacto = _repFiltroSegmentoTipoContacto.ObtenerTodoFiltro();

                var _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                var listaModalidadCurso = _repModalidadCurso.ObtenerTodoFiltro();

                var _repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);
                var listaEstadoPagoMatricula = _repEstadoPagoMatricula.ObtenerTodoFiltro();

                var _repCriterioDoc = new CriterioDocRepositorio(_integraDBContext);
                var listaCriterioDoc = _repCriterioDoc.ObtenerTodoSeleccionar();

                var _repTipoDato = new TipoDatoRepositorio(_integraDBContext);
                var listaTipoDato = _repTipoDato.ObtenerFiltro();

                var _repOrigen = new OrigenRepositorio(_integraDBContext);
                var listaOrigen = _repOrigen.ObtenerTodoFiltro();

                var _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
                var listaFiltroSegmento = _repFiltroSegmento.ObtenerTodoFiltro();

                ////Sesiones
                //var listaEstadoSesion = new[] {
                //    new { Id = 1, Nombre = "Todos" },
                //    new { Id = 2, Nombre = "Ejecutado" },
                //    new { Id = 3, Nombre = "PorEjecutar" },
                //    new { Id = 4, Nombre = "Planificada" }
                //}.ToList();

             
                var listaEstadoLlamadaTelefonica = new[] {
                    //new { Id = 1, Nombre = "Todos" },
                    new { Id = 2, Nombre = "Contesto" },
                    new { Id = 3, Nombre = "No contesto" }
                }.ToList();

                var listaAvanceAcademico = new[] {
                    //new { Id = 1, Nombre = "Todos" },
                    new { Id = 2, Nombre = "Al dia" },
                    new { Id = 3, Nombre = "Adelantado" },
                    new { Id = 4, Nombre = "Atrasado" }
                }.ToList();

                var listaEstadoPagos = new[] {
                    //new { Id = 1, Nombre = "Todos" },
                    new { Id = 2, Nombre = "Al dia" },
                    new { Id = 3, Nombre = "Adelantado" },
                    new { Id = 4, Nombre = "Atrasado" }
                }.ToList();

                var _repEstadoMatricula = new EstadoMatriculaRepositorio(_integraDBContext);
                //var _repSubEstadoMatricula = new SubEstadoMatriculaRepositorio(_integraDBContext);
                var listaEstadoMatricula = _repEstadoMatricula.ObtenerTodoFiltro();
                //var listaSubEstadoMatricula = _repSubEstadoMatricula.ObtenerTodoFiltro();


                var listaSubEstadoMatricula = new[] {
                    new { Id = 1, Nombre = "PF calificado por emitir certificado", IdEstadoMatricula = 5 },
                    new { Id = 2, Nombre = "PF en calificación" , IdEstadoMatricula = 5 },
                    new { Id = 4, Nombre = "PF pendiente de envio" , IdEstadoMatricula = 5 },
                    new { Id = 5, Nombre = "Por emitir certificado aprobado (No aplica PF)" , IdEstadoMatricula = 5 },
                    new { Id = 6, Nombre = "Por emitir certificado completado" , IdEstadoMatricula = 5 },
                    new { Id = 7, Nombre = "Por emitir certificado modulos" , IdEstadoMatricula = 5 },

                    new { Id = 8, Nombre = "Aprobado BSGI + Partner" , IdEstadoMatricula = 12 },
                    new { Id = 9, Nombre = "Aprobado BSGI - Pendiente Partner" , IdEstadoMatricula = 12 },
                    new { Id = 10, Nombre = "Aprobado BSGI" , IdEstadoMatricula = 12 },
                    new { Id = 11, Nombre = "Completado BSGI (solo es para casos donde hay PF)" , IdEstadoMatricula = 12 },
                    new { Id = 12, Nombre = "Modulos BSGI" , IdEstadoMatricula = 12 },

                    new { Id = 13, Nombre = "Cuota Atraso" , IdEstadoMatricula = 1 },
                    new { Id = 14, Nombre = "Cuota AlDia" , IdEstadoMatricula = 1 },
                    new { Id = 15, Nombre = "Seguimiento Academico" , IdEstadoMatricula = 1 }

                }.ToList();

                var listaEstadoAcademico = new[] {
                    new { Id = 1, Nombre = "Autoevaluaciones vencidas hace" },
                    new { Id = 2, Nombre = "Autoevaluaciones por vencer dentro de" },
                    //new { Id = 3, Nombre = "Autoevaluacion al dia" }
                }.ToList();

                var listaEstadoPago = new[] {
                    new { Id = 1, Nombre = "Cuotas vencidas hace" },
                    new { Id = 2, Nombre = "Cuotas por vencer dentro de" },
                    //new { Id = 3, Nombre = "Cuota al dia" }
                }.ToList();

                var listaEstadoSesion = new[] {
                    new { Id = 1, Nombre = "Sesion proxima dentro de" },
                    new { Id = 2, Nombre = "Sesion pasada hace" },
                    //new { Id = 3, Nombre = "Cuota al dia" }
                }.ToList();


                var listaEstadoSesionWebinar = new[] {
                    new { Id = 1, Nombre = "Sesiones webinar por confirmar dentro de " },
                    new { Id = 2, Nombre = "Recordatorio asistencia webinar dentro de" },
                    new { Id = 3, Nombre = "Sesion webinar dentro de (almenos un alumno webinar confirmado)" }
                }.ToList();


                var _repActividadCabecera = new ActividadCabeceraRepositorio(_integraDBContext);
                var listaActividadCabecera = _repActividadCabecera.ObtenerFiltro();

                var _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                var listaOcurrencia = _repOcurrencia.ObtenerFiltro();

                ///TODO - Poner repositorio de T_Tarifario
                var _repTarifario = new TarifarioRepositorio(_integraDBContext);
                var listaTarifario = _repTarifario.ObtenerTodoFiltro();

                var listasFiltro = new
                {
                    listaAreaCapacitacion,
                    listaSubAreaCapacitacion,
                    listaProgramaGeneral,
                    listaProgramaEspecifico,
                    listaFaseOportunidad,
                    listaPais,
                    listaCiudad,
                    listaTipoCategoriaOrigen,
                    listaCategoriaOrigen,
                    listaCargo,
                    listaIndustria,
                    listaAreaFormacion,
                    listaAreaTrabajo,
                    listaActividadCabecera,
                    listaOcurrencia,
                    listaOperadorComparacion,
                    listaTipoFormulario,
                    listaTipoInteraccion,
                    listaTiempoFrecuencia,
                    listaTiempoFrecuenciaEnvioAutomaticoCreacion,
                    listaTiempoFrecuenciaEnvioAutomaticoUltima,
                    listaEstadoActividadDetalle,
                    listaProbabilidadRegistro,
                    listaEmbudoNivel,
                    listaFiltroSegmentoTipoContacto,
                    listaModalidadCurso,
                    listaEstadoPagoMatricula,
                    listaCriterioDoc,
                    listaEstadoSesion,
                    listaEstadoSesionWebinar,
                    listaEstadoLlamadaTelefonica,
                    listaAvanceAcademico,
                    listaEstadoPagos,
                    listaTipoDato,
                    listaOrigen,
                    listaFiltroSegmento,
                    listaEstadoPago,
                    listaEstadoAcademico,
                    listaEstadoMatricula,
                    listaSubEstadoMatricula,
                    listaTarifario
                };
                return Ok(listasFiltro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdFiltroSegmento}")]
        [HttpGet]
        public ActionResult ObtenerDetalleFiltroSegmento(int IdFiltroSegmento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroSegmentoBO filtroSegmento = new FiltroSegmentoBO(_integraDBContext)
                {
                    Id = IdFiltroSegmento
                };
                return Ok(filtroSegmento.ObtenerFiltroValorPorIdFiltroSegmento());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] FiltroSegmentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var filtroSegmento = new FiltroSegmentoBO(_integraDBContext);
                return Ok(filtroSegmento.Insertar(Json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] FiltroSegmentoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroSegmentoRepositorio _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);

                if (_repFiltroSegmento.Exist(Json.Id))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var filtroSegmento = new FiltroSegmentoBO(Json.Id, _integraDBContext)
                        {
                            Nombre = Json.Nombre,
                            Descripcion = Json.Descripcion,
                            IdFiltroSegmentoTipoContacto = Json.IdFiltroSegmentoTipoContacto,
                            FechaInicioCreacionUltimaOportunidad = Json.FechaInicioCreacionUltimaOportunidad,
                            FechaFinCreacionUltimaOportunidad = Json.FechaFinCreacionUltimaOportunidad,
                            FechaInicioModificacionUltimaActividadDetalle = Json.FechaInicioModificacionUltimaActividadDetalle,
                            FechaFinModificacionUltimaActividadDetalle = Json.FechaFinModificacionUltimaActividadDetalle,
                            FechaInicioProgramacionUltimaActividadDetalleRn2 = Json.FechaInicioProgramacionUltimaActividadDetalleRn2,
                            FechaFinProgramacionUltimaActividadDetalleRn2 = Json.FechaFinProgramacionUltimaActividadDetalleRn2,
                            EsRn2 = Json.EsRn2,
                            IdOperadorComparacionNroOportunidades = Json.IdOperadorComparacionNroOportunidades,
                            NroOportunidades = Json.NroOportunidades,
                            IdOperadorComparacionNroSolicitudInformacion = Json.IdOperadorComparacionNroSolicitudInformacion,
                            NroSolicitudInformacion = Json.NroSolicitudInformacion,

                            //
                            IdOperadorComparacionNroSolicitudInformacionPg = Json.IdOperadorComparacionNroSolicitudInformacionPg,
                            NroSolicitudInformacionPg = Json.NroSolicitudInformacionPg,
                            IdOperadorComparacionNroSolicitudInformacionArea = Json.IdOperadorComparacionNroSolicitudInformacionArea,
                            NroSolicitudInformacionArea = Json.NroSolicitudInformacionArea,
                            IdOperadorComparacionNroSolicitudInformacionSubArea = Json.IdOperadorComparacionNroSolicitudInformacionSubArea,
                            NroSolicitudInformacionSubArea = Json.NroSolicitudInformacionSubArea,

                            FechaInicioFormulario = Json.FechaInicioFormulario,
                            FechaFinFormulario = Json.FechaFinFormulario,
                            FechaInicioChatIntegra = Json.FechaInicioChatIntegra,
                            FechaFinChatIntegra = Json.FechaFinChatIntegra,
                            IdOperadorComparacionTiempoMaximoRespuestaChatOnline = Json.IdOperadorComparacionTiempoMaximoRespuestaChatOnline,
                            TiempoMaximoRespuestaChatOnline = Json.TiempoMaximoRespuestaChatOnline,
                            IdOperadorComparacionNroPalabrasClienteChatOnline = Json.IdOperadorComparacionNroPalabrasClienteChatOnline,
                            NroPalabrasClienteChatOnline = Json.NroPalabrasClienteChatOnline,
                            IdOperadorComparacionTiempoPromedioRespuestaChatOnline = Json.IdOperadorComparacionTiempoPromedioRespuestaChatOnline,
                            TiempoPromedioRespuestaChatOnline = Json.TiempoPromedioRespuestaChatOnline,
                            IdOperadorComparacionNroPalabrasClienteChatOffline = Json.IdOperadorComparacionNroPalabrasClienteChatOffline,
                            NroPalabrasClienteChatOffline = Json.NroPalabrasClienteChatOffline,

                            FechaInicioCorreo = Json.FechaInicioCorreo,
                            FechaFinCorreo = Json.FechaFinCorreo,
                            IdOperadorComparacionNroCorreosAbiertos = Json.IdOperadorComparacionNroCorreosAbiertos,
                            NroCorreosAbiertos = Json.NroCorreosAbiertos,
                            IdOperadorComparacionNroCorreosNoAbiertos = Json.IdOperadorComparacionNroCorreosNoAbiertos,
                            NroCorreosNoAbiertos = Json.NroCorreosNoAbiertos,
                            IdOperadorComparacionNroClicksEnlace = Json.IdOperadorComparacionNroClicksEnlace,
                            NroClicksEnlace = Json.NroClicksEnlace,
                            EsSuscribirme = Json.EsSuscribirme,
                            EsDesuscribirme = Json.EsDesuscribirme,

                            IdOperadorComparacionNroCorreosAbiertosMailChimp = Json.IdOperadorComparacionNroCorreosAbiertosMailChimp,
                            NroCorreosAbiertosMailChimp = Json.NroCorreosAbiertosMailChimp,
                            IdOperadorComparacionNroCorreosNoAbiertosMailChimp = Json.IdOperadorComparacionNroCorreosNoAbiertosMailChimp,
                            NroCorreosNoAbiertosMailChimp = Json.NroCorreosNoAbiertosMailChimp,
                            IdOperadorComparacionNroClicksEnlaceMailChimp = Json.IdOperadorComparacionNroClicksEnlaceMailChimp,
                            NroClicksEnlaceMailChimp = Json.NroClicksEnlaceMailChimp,

                            ConsiderarFiltroGeneral = Json.ConsiderarFiltroGeneral,
                            ConsiderarFiltroEspecifico = Json.ConsiderarFiltroEspecifico,
                            TieneVentaCruzada = Json.TieneVentaCruzada,
                            IdOperadorComparacionNroTotalLineaCreditoVigente = Json.IdOperadorComparacionNroTotalLineaCreditoVigente,
                            NroTotalLineaCreditoVigente = Json.NroTotalLineaCreditoVigente,
                            IdOperadorComparacionMontoTotalLineaCreditoVigente = Json.IdOperadorComparacionMontoTotalLineaCreditoVigente,
                            MontoTotalLineaCreditoVigente = Json.MontoTotalLineaCreditoVigente,
                            IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = Json.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                            MontoMaximoOtorgadoLineaCreditoVigente = Json.MontoMaximoOtorgadoLineaCreditoVigente,
                            IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = Json.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                            MontoMinimoOtorgadoLineaCreditoVigente = Json.MontoMinimoOtorgadoLineaCreditoVigente,
                            IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = Json.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                            NroTotalLineaCreditoVigenteVencida = Json.NroTotalLineaCreditoVigenteVencida,
                            IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = Json.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                            MontoTotalLineaCreditoVigenteVencida = Json.MontoTotalLineaCreditoVigenteVencida,
                            IdOperadorComparacionNroTcOtorgada = Json.IdOperadorComparacionNroTcOtorgada,

                            NroTcOtorgada = Json.NroTcOtorgada,
                            IdOperadorComparacionMontoTotalOtorgadoEnTcs = Json.IdOperadorComparacionMontoTotalOtorgadoEnTcs,
                            MontoTotalOtorgadoEnTcs = Json.MontoTotalOtorgadoEnTcs,
                            IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = Json.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                            MontoMaximoOtorgadoEnUnaTc = Json.MontoMaximoOtorgadoEnUnaTc,
                            IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = Json.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                            MontoMinimoOtorgadoEnUnaTc = Json.MontoMinimoOtorgadoEnUnaTc,
                            IdOperadorComparacionMontoDisponibleTotalEnTcs = Json.IdOperadorComparacionMontoDisponibleTotalEnTcs,
                            MontoDisponibleTotalEnTcs = Json.MontoDisponibleTotalEnTcs,
                            FechaInicioLlamada = Json.FechaInicioLlamada,
                            FechaFinLlamada = Json.FechaFinLlamada,
                            IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = Json.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                            DuracionPromedioLlamadaPorOportunidad = Json.DuracionPromedioLlamadaPorOportunidad,
                            IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = Json.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                            DuracionTotalLlamadaPorOportunidad = Json.DuracionTotalLlamadaPorOportunidad,
                            IdOperadorComparacionNroLlamada = Json.IdOperadorComparacionNroLlamada,
                            NroLlamada = Json.NroLlamada,
                            IdOperadorComparacionDuracionLlamada = Json.IdOperadorComparacionDuracionLlamada,
                            DuracionLlamada = Json.DuracionLlamada,
                            IdOperadorComparacionTasaEjecucionLlamada = Json.IdOperadorComparacionTasaEjecucionLlamada,
                            TasaEjecucionLlamada = Json.TasaEjecucionLlamada,

                            FechaInicioInteraccionSitioWeb = Json.FechaInicioInteraccionSitioWeb,
                            FechaFinInteraccionSitioWeb = Json.FechaFinInteraccionSitioWeb,
                            IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = Json.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                            TiempoVisualizacionTotalSitioWeb = Json.TiempoVisualizacionTotalSitioWeb,
                            IdOperadorComparacionNroClickEnlaceTodoSitioWeb = Json.IdOperadorComparacionNroClickEnlaceTodoSitioWeb,
                            NroClickEnlaceTodoSitioWeb = Json.NroClickEnlaceTodoSitioWeb,
                            IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = Json.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                            TiempoVisualizacionTotalPaginaPrograma = Json.TiempoVisualizacionTotalPaginaPrograma,
                            IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = Json.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                            TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = Json.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                            IdOperadorComparacionNroClickEnlacePaginaPrograma = Json.IdOperadorComparacionNroClickEnlacePaginaPrograma,
                            NroClickEnlacePaginaPrograma = Json.NroClickEnlacePaginaPrograma,
                            ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = Json.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma,
                            ConsiderarClickBotonMatricularmePaginaPrograma = Json.ConsiderarClickBotonMatricularmePaginaPrograma,
                            ConsiderarClickBotonVersionPruebaPaginaPrograma = Json.ConsiderarClickBotonVersionPruebaPaginaPrograma,
                            IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = Json.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,

                            TiempoVisualizacionTotalPaginaBscampus = Json.TiempoVisualizacionTotalPaginaBscampus,
                            IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = Json.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                            TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = Json.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                            IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = Json.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea,
                            NroVisitasDirectorioTagAreaSubArea = Json.NroVisitasDirectorioTagAreaSubArea,
                            IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = Json.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                            TiempoVisualizacionTotalDirectorioTagAreaSubArea = Json.TiempoVisualizacionTotalDirectorioTagAreaSubArea,
                            IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = Json.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                            NroClickEnlaceDirectorioTagAreaSubArea = Json.NroClickEnlaceDirectorioTagAreaSubArea,
                            IdOperadorComparacionNroVisitasPaginaMisCursos = Json.IdOperadorComparacionNroVisitasPaginaMisCursos,
                            NroVisitasPaginaMisCursos = Json.NroVisitasPaginaMisCursos,
                            IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = Json.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                            TiempoVisualizacionTotalPaginaMisCursos = Json.TiempoVisualizacionTotalPaginaMisCursos,
                            IdOperadorComparacionNroClickEnlacePaginaMisCursos = Json.IdOperadorComparacionNroClickEnlacePaginaMisCursos,
                            NroClickEnlacePaginaMisCursos = Json.NroClickEnlacePaginaMisCursos,
                            IdOperadorComparacionNroVisitaPaginaCursoDiplomado = Json.IdOperadorComparacionNroVisitaPaginaCursoDiplomado,
                            NroVisitaPaginaCursoDiplomado = Json.NroVisitaPaginaCursoDiplomado,
                            IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = Json.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                            TiempoVisualizacionTotalPaginaCursoDiplomado = Json.TiempoVisualizacionTotalPaginaCursoDiplomado,
                            IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = Json.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                            NroClicksEnlacePaginaCursoDiplomado = Json.NroClicksEnlacePaginaCursoDiplomado,
                            ConsiderarClickFiltroPaginaCursoDiplomado = Json.ConsiderarClickFiltroPaginaCursoDiplomado,

                            AplicaSobreCreacionOportunidad = Json.AplicaSobreCreacionOportunidad,
                            IdOperadorMedidaTiempoCreacionOportunidad = Json.IdOperadorMedidaTiempoCreacionOportunidad,
                            NroMedidaTiempoCreacionOportunidad = Json.NroMedidaTiempoCreacionOportunidad,
                            AplicaSobreUltimaActividad = Json.AplicaSobreUltimaActividad,
                            IdOperadorMedidaTiempoUltimaActividadEjecutada = Json.IdOperadorMedidaTiempoUltimaActividadEjecutada,
                            NroMedidaTiempoUltimaActividadEjecutada = Json.NroMedidaTiempoUltimaActividadEjecutada,
                            EnvioAutomaticoEstadoActividadDetalle = Json.EnvioAutomaticoEstadoActividadDetalle,
                            ConsiderarYaEnviados = Json.ConsiderarYaEnviados,

                            ConsiderarOportunidadHistorica = Json.ConsiderarOportunidadHistorica,
                            ConsiderarCategoriaDato = Json.ConsiderarCategoriaDato,
                            ConsiderarInteraccionOfflineOnline = Json.ConsiderarInteraccionOfflineOnline,
                            ConsiderarInteraccionSitioWeb = Json.ConsiderarInteraccionSitioWeb,
                            ConsiderarInteraccionFormularios = Json.ConsiderarInteraccionFormularios,
                            ConsiderarInteraccionChatPw = Json.ConsiderarInteraccionChatPw,
                            ConsiderarInteraccionCorreo = Json.ConsiderarInteraccionCorreo,
                            ConsiderarHistorialFinanciero = Json.ConsiderarHistorialFinanciero,
                            ConsiderarInteraccionWhatsApp = Json.ConsiderarInteraccionWhatsApp,
                            ConsiderarInteraccionChatMessenger = Json.ConsiderarInteraccionChatMessenger,
                            ConsiderarEnvioAutomatico = Json.ConsiderarEnvioAutomatico,

                            ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = Json.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                            FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = Json.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                            FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = Json.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,

                            IdTiempoFrecuenciaMatriculaAlumno = Json.IdTiempoFrecuenciaMatriculaAlumno,
                            CantidadTiempoMatriculaAlumno = Json.CantidadTiempoMatriculaAlumno,

                            ConsiderarConMessengerValido = Json.ConsiderarConMessengerValido,
                            ConsiderarConWhatsAppValido = Json.ConsiderarConWhatsAppValido,
                            ConsiderarConEmailValido = Json.ConsiderarConEmailValido,

                            IdTiempoFrecuenciaCumpleaniosContactoDentroDe = Json.IdTiempoFrecuenciaCumpleaniosContactoDentroDe,
                            CantidadTiempoCumpleaniosContactoDentroDe = Json.CantidadTiempoCumpleaniosContactoDentroDe,

                            FechaInicioMatriculaAlumno = Json.FechaInicioMatriculaAlumno,
                            FechaFinMatriculaAlumno = Json.FechaFinMatriculaAlumno,
                            ConsiderarAlumnosAsignacionAutomaticaOperaciones = Json.ConsiderarAlumnosAsignacionAutomaticaOperaciones,
                            ExcluirMatriculados = Json.ExcluirMatriculados,

                            UsuarioModificacion = Json.NombreUsuario,
                            FechaModificacion = DateTime.Now
                        };

                        filtroSegmento.LlenarHijoParaActualizar(Json);
                        _repFiltroSegmento.Update(filtroSegmento);
                        scope.Complete();
                        return Ok(filtroSegmento);
                    }
                }
                else {
                    return BadRequest("El registro no existe!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroSegmentoRepositorio _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
                FiltroSegmentoValorTipoRepositorio _repFiltroSegmentoTipo = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);
                FiltroSegmentoDetalleRepositorio _repFiltroSegmentoDetalle = new FiltroSegmentoDetalleRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repFiltroSegmento.Exist(Json.Id))
                    {
                        _repFiltroSegmento.Delete(Json.Id, Json.NombreUsuario);
                        _repFiltroSegmentoTipo.Delete(_repFiltroSegmentoTipo.GetBy(x => x.IdFiltroSegmento == Json.Id).Select(x => x.Id), Json.NombreUsuario);
                        _repFiltroSegmentoDetalle.Delete(_repFiltroSegmentoDetalle.GetBy(x => x.IdFiltroSegmento == Json.Id).Select(x => x.Id), Json.NombreUsuario);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerResultadosFiltro(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!_repFiltroSegmento.Exist(Id))
                {
                    return BadRequest("Registro no existente!");
                }

                var filtroSegmento = _repFiltroSegmento.FirstById(Id);
                return Ok(new { listadoDatosFiltrados = filtroSegmento.ObtenerResultado() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}")]
        [HttpGet]
        public ActionResult EjecutarFiltro(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var filtroSegmento = new FiltroSegmentoBO(_integraDBContext)
                {
                    Id = Id,
                    UsuarioCreacion = "SYSTEM"
                };
                return Ok(new { listadoDatosFiltrados = filtroSegmento.EjecutarFiltro() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult EjecutarFiltro(int Id, string NombreUsuario)
        {
            DateTime horaInicio = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repFiltroSegmento.Exist(Id))
                {
                    return BadRequest("Filtro segmento no existente!");
                }
                var filtroSegmento = new FiltroSegmentoBO(_integraDBContext)
                {
                    Id = Id,
                    UsuarioCreacion = NombreUsuario
                };
                filtroSegmento.EjecutarFiltro();

                var _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                var correosPersonalizados = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com",
                    "gmiranda@bsginstitute.com"
                };

                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(NombreUsuario))
                {
                    try
                    {
                        correosPersonalizados.Add(_repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(NombreUsuario));
                    }
                    catch (Exception)
                    {
                    }
                }

                var MailservicePersonalizado = new TMK_MailServiceImpl();
                var mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correosPersonalizados),
                    Subject = string.Concat("Procesar filtro segmento - ", _repFiltroSegmento.FirstById(Id).Nombre),
                    Message = $@"
                    <p style='color: red;'><strong>----Servicio de confirmación de filtro segmento----</strong></p>
                    <p>El filtro segmento <strong>{_repFiltroSegmento.FirstById(Id).Nombre}<span style='color: green;'> FINALIZO CORRECTAMENTE</span></strong></p>
                    <p><strong>Hora de Inicio:</strong></p>
                    <p><span style='color: orange;'>{horaInicio}</span></p>
                    <p><strong>Hora de Finalizacion:</strong></p>
                    <p><span style='color: orange;'>{DateTime.Now}</span></p> 
                    ",
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();
                return Ok(true);
            }
            catch (Exception e)
            {

                var _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

                List<string> correos = new List<string>
                {
                    "fvaldez@bsginstitute.com",
                    "jvillena@bsginstitute.com"
                };
                if (_repIntegraAspNetUsers.ExistePorNombreUsuario(NombreUsuario))
                {
                    try
                    {
                        correos.Add(_repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(NombreUsuario));
                    }
                    catch (Exception)
                    {
                    }
                }
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "wchoque@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = string.Concat("Procesar filtro segmento - Error ", _repFiltroSegmento.FirstById(Id).Nombre),
                    Message = string.Concat("Message: ", JsonConvert.SerializeObject(e),
                    $@"

                        >>>>> Servicio de confirmación de filtro segmento <<<<<
                    "
                    ),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult GetAutoComplete([FromBody] FiltroAutocompleteDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroSegmentoRepositorio _repFiltroSegmento = new FiltroSegmentoRepositorio();
                return Ok(_repFiltroSegmento.ObtenerAutoComplete(Filtros.Valor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Duplicar([FromBody] DuplicarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var filtroSegmento = new FiltroSegmentoBO(_integraDBContext)
                {
                    Id = Json.Id
                };
                var filtroDetalle = filtroSegmento.ObtenerFiltroValorPorIdFiltroSegmento();
                filtroDetalle.NombreUsuario = Json.NombreUsuario;
                filtroDetalle.Nombre = string.Concat(filtroDetalle.Nombre, " - COPIA");
                filtroDetalle.Descripcion = string.Concat(filtroDetalle.Descripcion, " - COPIA"); ;

                var nuevoFiltroSegmento = new FiltroSegmentoBO(_integraDBContext);
                return Ok(nuevoFiltroSegmento.Insertar(filtroDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
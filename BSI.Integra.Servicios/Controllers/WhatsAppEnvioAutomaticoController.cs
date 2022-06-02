using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.DTOs;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/WhatsAppEnvioAutomatico
    /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Configura las actividades automaticas de la interfaz WhatsAppEnvioAutomatico
    /// </summary>
    [Route("api/WhatsAppEnvioAutomatico")]
    public class WhatsAppEnvioAutomaticoController : Controller
    {
        public string UrlHostWhatsApp = "https://167.71.101.242:9090";
        public string IpHostWhatsApp = "167.71.101.242:9090";
        public string UserAuthToken = "eyJhbGciOiAiSFMyNTYiLCAidHlwIjogIkpXVCJ9.eyJ1c2VyIjoianJpdmVyYSIsImlhdCI6MTU2NTk4MzM2OCwiZXhwIjoxNTY2NTg4MTY4LCJ3YTpyYW5kIjoyNDIwNjg1ODM0MjM0NDQ4Njg2fQ.OD5A53Pd8FPLr6dd4Es6vxPS3hlSle1VLYZOY7TPd0k";

        private readonly integraDBContext _integraDBContext;

        private WhatsAppUsuarioCredencialRepositorio _repTokenUsuario;
        private WhatsAppConfiguracionLogEjecucionRepositorio _repWhatsAppConfiguracionLogEjecucion;
        private AlumnoRepositorio _repAlumno;
        private PersonalRepositorio _repPersonal;
        private PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private WhatsAppConfiguracionRepositorio _repCredenciales;
        private PlantillaRepositorio _repPlantilla;
        private CentroCostoRepositorio _repCentroCosto;
        private PespecificoRepositorio _repPespecifico;
        private PgeneralRepositorio _repPgeneral;
        private readonly ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing;
        private readonly ConfiguracionEnvioMailingDetalleRepositorio _repConfiguracionEnvioMailingDetalle;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        private readonly ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;
        private readonly OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private readonly CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly RegistroRecuperacionWhatsAppRepositorio _repRegistroRecuperacionWhatsApp;
        private readonly WhatsAppConfiguracionEnvioDetalleRepositorio _repWhatsAppConfiguracionEnvioDetalle;
        private readonly WhatsAppMensajePublicidadRepositorio _repWhatsAppMensajePublicidad;
        private readonly SeguimientoPreProcesoListaWhatsAppRepositorio _repSeguimientoPreProcesoListaWhatsApp;
        private readonly WhatsAppConfiguracionPreEnvioRepositorio _repWhatsAppConfiguracionPreEnvio;
        private readonly CampaniaGeneralDetalleResponsableRepositorio _repCampaniaGeneralDetalleResponsable;
        private readonly CampaniaGeneralRepositorio _repCampaniaGeneral;
        private readonly RecuperacionAutomaticoModuloSistemaRepositorio _repRecuperacionAutomaticoModuloSistema;

        private PrioridadMailChimpListaCorreoRepositorio _repPrioridadMailChimpListaCorreo;
        private WhatsAppConfiguracionEnvioRepositorio _repWhatsAppConfiguracionEnvio;
        private CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle;
        private LogRepositorio _repLog;

        private const int NRO_MAXIMO_INTENTOS = 5;

        public WhatsAppEnvioAutomaticoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;

            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(_integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(_integraDBContext);
            _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
            _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionEnvioDetalle = new WhatsAppConfiguracionEnvioDetalleRepositorio(_integraDBContext);
            _repWhatsAppMensajePublicidad = new WhatsAppMensajePublicidadRepositorio(_integraDBContext);
            _repRegistroRecuperacionWhatsApp = new RegistroRecuperacionWhatsAppRepositorio(_integraDBContext);
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repRecuperacionAutomaticoModuloSistema = new RecuperacionAutomaticoModuloSistemaRepositorio(_integraDBContext);

            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(integraDBContext);
            _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(integraDBContext);
            _repConfiguracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext);
            _repCampaniaGeneral = new CampaniaGeneralRepositorio(_integraDBContext);

            _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionRepositorio(_integraDBContext);

            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);

            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
            _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            _repSeguimientoPreProcesoListaWhatsApp = new SeguimientoPreProcesoListaWhatsAppRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionPreEnvio = new WhatsAppConfiguracionPreEnvioRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalleResponsable = new CampaniaGeneralDetalleResponsableRepositorio(_integraDBContext);
            _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
            _repLog = new LogRepositorio(_integraDBContext);

            _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarListasWhatsAppEnvioAutomatico([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                {
                    WhatsAppConfiguracionLogEjecucionBO logEjecion = new WhatsAppConfiguracionLogEjecucionBO();
                    try
                    {
                        logEjecion.FechaInicio = DateTime.Now;
                        logEjecion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                        logEjecion.Estado = true;
                        logEjecion.FechaCreacion = DateTime.Now;
                        logEjecion.FechaModificacion = DateTime.Now;
                        logEjecion.UsuarioCreacion = "ProcesoAutomatico";
                        logEjecion.UsuarioModificacion = "ProcesoAutomatico";
                        _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);


                        var Respuesta = _repConjuntoListaResultado.ObtenerConjuntoListaResultado(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                        this.ValidarNumeroConjuntoLista(ref Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int));
                        Respuesta = Respuesta.Where(w => w.Validado == true).ToList();

                        this.RemplazarEtiquetas(ref Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.ProgramaPrincipal, WhatsAppConfiguracionEnvio.ProgramaSecundario);
                        Respuesta = Respuesta.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();

                        this.EnvioAutomaticoPlantilla(Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), logEjecion.Id);

                        var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecion.Id);
                        logEjecucionFinal.FechaFin = DateTime.Now;
                        _repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);


                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (logEjecion.Id == 0 || logEjecion.Id == null)
                            {
                                logEjecion.FechaFin = DateTime.Now;
                                _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);
                            }
                        }
                        catch (Exception e)
                        {

                        }


                    }

                }
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: La funcion permite registrar un seguimiento de cuando se inicia el pre proceso y registra pre-validacion de los numeros previos al envio de mensajes
        /// </summary>
        /// <param name="IdCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla pla.T_CampaniaGeneralDetalle)</param>
        /// <returns>Retorna booleano dependiendo de si se realizo correctamente la ejecucion</returns>
        public bool ProcesarListaWhatsAppCampaniaGeneral(int IdCampaniaGeneralDetalle)
        {
            var listaAptaWhatsApp = new List<PreWhatsAppResultadoConjuntoListaDTO>();
            var seguimientoPreProcesoBo = new SeguimientoPreProcesoListaWhatsAppBO();

            try
            {
                seguimientoPreProcesoBo.IdEstadoSeguimientoPreProcesoListaWhatsApp = 2;
                seguimientoPreProcesoBo.IdConjuntoLista = 1;
                seguimientoPreProcesoBo.IdCampaniaGeneralDetalle = IdCampaniaGeneralDetalle;
                seguimientoPreProcesoBo.UsuarioCreacion = "Pre-ProcesoCampaniaGeneral";
                seguimientoPreProcesoBo.UsuarioModificacion = "Pre-ProcesoCampaniaGeneral";

                // Eliminar procesos anteriores
                _repSeguimientoPreProcesoListaWhatsApp.EliminarSeguimientoPreProcesoCampaniaGeneral(IdCampaniaGeneralDetalle);
                _repWhatsAppConfiguracionEnvio.EliminarWhatsAppConfiguracionMailingGeneral(IdCampaniaGeneralDetalle);

                seguimientoPreProcesoBo.Id = _repSeguimientoPreProcesoListaWhatsApp.InsertarSeguimientoPreProcesoCampaniaGeneral(seguimientoPreProcesoBo);

                // Obtener lista apta e insertar los ya prevalidados en el plazo de los ultimos 30 dias
                listaAptaWhatsApp = _repPrioridadMailChimpListaCorreo.ObtenerListaAptaPreprocesamientoWhatsApp(IdCampaniaGeneralDetalle);

                // Verificar que no falten validar otros registros
                if (listaAptaWhatsApp.Where(w => !w.Prevalidado.Value).ToList().Any())
                {
                    var resultado = ValidarNumeroMailingGeneralMasivo(listaAptaWhatsApp.Where(w => !w.Prevalidado.Value).ToList(), ValorEstatico.IdPersonalWhatsappNuevasOportunidades, listaAptaWhatsApp.First().IdWhatsAppConfiguracionEnvio.Value, listaAptaWhatsApp.First().IdPlantilla.Value);
                }
            }
            catch (Exception ex)
            {

            }

            _repSeguimientoPreProcesoListaWhatsApp.ActualizarSeguimientoPreProcesoCampaniaGeneral(seguimientoPreProcesoBo.Id, listaAptaWhatsApp.Any() ? 3 : ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos);

            return true;
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: La funcion permite registrar un seguimiento de cuando se inicia el pre proceso y registra pre-validacion de los numeros previos al envio de mensajes
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns>Retornamos verdadero si la lista se proceso correctamente caso contrario falso</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarListasWhatsAppEnvioAutomaticoMasivo([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var seguimientoPreProcesoListaWhatsApp = new SeguimientoPreProcesoListaWhatsAppBO();
            int seguimientoLista = 0, contadorRegistros = 0;

            try
            {
                if (ListasWhatsApp != null && ListasWhatsApp.Any())
                {
                    var datoSeguimientoLista = ListasWhatsApp.Select(x => x.IdConjuntoLista).FirstOrDefault();

                    seguimientoLista = datoSeguimientoLista != null ? datoSeguimientoLista.Value : 0;

                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = 2;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _repSeguimientoPreProcesoListaWhatsApp.Insert(seguimientoPreProcesoListaWhatsApp);

                    foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                    {
                        try
                        {
                            var respuesta = _repConjuntoListaResultado.ObtenerListaPreparadaProcesamiento(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);

                            if (respuesta != null && respuesta.Any())
                            {
                                contadorRegistros++;

                                var resultadoValidacion = this.ValidarNumeroConjuntoListaMasivo(respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.IdConjuntoLista ?? default(int));

                                this.RemplazarEtiquetaMasivo(ref resultadoValidacion, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.ProgramaPrincipal, WhatsAppConfiguracionEnvio.ProgramaSecundario);

                                var resultadoInsercion = RegistraPreValidacion(resultadoValidacion, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int), WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                            }
                        }
                        catch (Exception ex)
                        {
                            var LogSeguimientoBO = _repSeguimientoPreProcesoListaWhatsApp.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                            LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                            LogSeguimientoBO.FechaModificacion = DateTime.Now;
                            _repSeguimientoPreProcesoListaWhatsApp.Update(LogSeguimientoBO);
                        }
                    }

                    if (contadorRegistros > 0)
                    {
                        var LogSeguimientoBO = _repSeguimientoPreProcesoListaWhatsApp.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                        LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = 3;
                        LogSeguimientoBO.FechaModificacion = DateTime.Now;
                        _repSeguimientoPreProcesoListaWhatsApp.Update(LogSeguimientoBO);

                    }
                    else
                    {
                        var LogSeguimientoBO = _repSeguimientoPreProcesoListaWhatsApp.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                        LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                        LogSeguimientoBO.FechaModificacion = DateTime.Now;
                        _repSeguimientoPreProcesoListaWhatsApp.Update(LogSeguimientoBO);
                    }
                }
                else
                {
                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = ValorEstatico.IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _repSeguimientoPreProcesoListaWhatsApp.Insert(seguimientoPreProcesoListaWhatsApp);
                }

                return Ok(true);
            }
            catch (Exception Ex)
            {
                var DatoSeguimientoLista = ListasWhatsApp.Select(x => x.IdConjuntoLista).FirstOrDefault();
                if (DatoSeguimientoLista != null)
                {
                    seguimientoLista = DatoSeguimientoLista.Value;
                }
                else
                {
                    seguimientoLista = 0;
                }

                if (seguimientoPreProcesoListaWhatsApp != null && seguimientoPreProcesoListaWhatsApp.Id != 0)
                {
                    var LogSeguimientoBO = _repSeguimientoPreProcesoListaWhatsApp.FirstById(seguimientoPreProcesoListaWhatsApp.Id);
                    LogSeguimientoBO.IdEstadoSeguimientoPreProcesoListaWhatsApp = 5;
                    LogSeguimientoBO.FechaModificacion = DateTime.Now;
                    _repSeguimientoPreProcesoListaWhatsApp.Update(LogSeguimientoBO);
                }
                else
                {
                    seguimientoPreProcesoListaWhatsApp.IdEstadoSeguimientoPreProcesoListaWhatsApp = 5;
                    seguimientoPreProcesoListaWhatsApp.IdConjuntoLista = seguimientoLista;
                    seguimientoPreProcesoListaWhatsApp.Estado = true;
                    seguimientoPreProcesoListaWhatsApp.FechaCreacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.FechaModificacion = DateTime.Now;
                    seguimientoPreProcesoListaWhatsApp.UsuarioCreacion = "Pre-ProcesoAutomatico";
                    seguimientoPreProcesoListaWhatsApp.UsuarioModificacion = "Pre-ProcesoAutomatico";

                    _repSeguimientoPreProcesoListaWhatsApp.Insert(seguimientoPreProcesoListaWhatsApp);
                }


                return BadRequest(Ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: La funcion se envia el conjunto de lista detalle para su proceso de los datos pre procesados y se realice el envio respectivo ya alos datos validados 
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarListasWhatsAppEnvioAutomaticoMasivo([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> Resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                {
                    WhatsAppConfiguracionLogEjecucionBO LogEjecion = new WhatsAppConfiguracionLogEjecucionBO();
                    try
                    {
                        var Respuesta = _repWhatsAppConfiguracionPreEnvio.ListasWhatsAppEnvioAutomaticoMasivoPreProcesada(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                        if (Respuesta != null && Respuesta.Count > 0)
                        {
                            LogEjecion.FechaInicio = DateTime.Now;
                            LogEjecion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                            LogEjecion.Estado = true;
                            LogEjecion.FechaCreacion = DateTime.Now;
                            LogEjecion.FechaModificacion = DateTime.Now;
                            LogEjecion.UsuarioCreacion = "Pre-ProcesoAutomatico";
                            LogEjecion.UsuarioModificacion = "Pre-ProcesoAutomatico";
                            _repWhatsAppConfiguracionLogEjecucion.Insert(LogEjecion);

                            foreach (var item in Respuesta)
                            {
                                WhatsAppResultadoConjuntoListaDTO Objeto = new WhatsAppResultadoConjuntoListaDTO();

                                Objeto.IdPre = item.Id;
                                Objeto.IdConjuntoListaResultado = item.IdConjuntoListaResultado;
                                Objeto.IdAlumno = item.IdAlumno;
                                Objeto.Celular = item.Celular;
                                Objeto.IdCodigoPais = item.IdCodigoPais;
                                Objeto.NroEjecucion = item.NroEjecucion;
                                Objeto.Validado = item.Validado;
                                Objeto.Plantilla = item.Plantilla;
                                Objeto.IdPersonal = item.IdPersonal;
                                Objeto.IdPgeneral = item.IdPgeneral;
                                Objeto.IdPlantilla = item.IdPlantilla;

                                if (!string.IsNullOrEmpty(item.objetoplantilla))
                                {
                                    Objeto.objetoplantilla = JsonConvert.DeserializeObject<List<datoPlantillaWhatsApp>>(item.objetoplantilla);
                                    Resultado.Add(Objeto);
                                }

                            }

                            var EnvioMensajes = EnvioAutomaticoPlantillaMasivo(ref Resultado, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), LogEjecion.Id);

                            var LogEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(LogEjecion.Id);
                            LogEjecucionFinal.FechaFin = DateTime.Now;
                            _repWhatsAppConfiguracionLogEjecucion.Update(LogEjecucionFinal);

                        }

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (LogEjecion.Id == 0 || LogEjecion.Id == null)
                            {
                                LogEjecion.FechaFin = DateTime.Now;
                                _repWhatsAppConfiguracionLogEjecucion.Insert(LogEjecion);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }

                }
                return Ok(Resultado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: Obtiene todos los envios fallidos por caida de servicios 2 para reenviar de los diferentes tipos
        /// </summary>
        /// <returns>Response 200, Caso contrario response 400 con el error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarRecuperacionCaidaEnvioWhatsApp()
        {
            try
            {
                int cantidadSolicitudRechazada = _repRegistroRecuperacionWhatsApp.ObtenerCantidadCaidaRecuperacionWhatsApp();

                if (cantidadSolicitudRechazada >= 5)
                {
                    EjecutarRecuperacionFallidoEnvioWhatsApp(2/*2: Fallido por baja de servicios 2*/);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: Obtiene todos los envios fallidos para reenviar de los diferentes tipos
        /// </summary>
        /// <returns>Response 200, Caso contrario response 400 con el error</returns>
        [Route("[Action]/{IdTipoError}")]
        [HttpGet]
        public ActionResult EjecutarRecuperacionFallidoEnvioWhatsApp(int IdTipoError)
        {
            /* 1: Fallido por saturacion
             * 2: Fallido por baja de servicios 2
             */
            try
            {
                var resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                var listaRegistroFallido = new List<RegistroRecuperacionWhatsAppBO>();

                if (IdTipoError == 1)
                {
                    listaRegistroFallido = _repRegistroRecuperacionWhatsApp.GetBy(
                    x => !x.Completado
                        && x.Fallido.HasValue
                        && x.Fallido.Value
                        &&
                        (
                        (x.RecuperacionEnProceso.HasValue && !x.RecuperacionEnProceso.Value) || (!x.RecuperacionEnProceso.HasValue)
                        )
                        && x.FechaCreacion >= DateTime.Now.Date
                        && x.FechaCreacion <= DateTime.Now.Date.AddDays(1)
                    ).ToList();
                }
                else if (IdTipoError == 2)
                {
                    listaRegistroFallido = _repRegistroRecuperacionWhatsApp.GetBy(
                    x => !x.Completado
                        && x.Fallido.HasValue
                        && !x.Fallido.Value
                        &&
                        (
                        (x.RecuperacionEnProceso.HasValue && !x.RecuperacionEnProceso.Value) || (!x.RecuperacionEnProceso.HasValue)
                        )
                        && x.FechaCreacion >= DateTime.Now.Date
                        && x.FechaCreacion <= DateTime.Now.Date.AddDays(1)
                    ).ToList();
                }
                else
                {
                    return BadRequest("No es un tipo de error permitido");
                }

                var listaRegistroEvaluado = listaRegistroFallido.Select(s => new DiaFallidoEvaluadoDTO
                {
                    Id = s.Id,
                    IdCampaniaGeneral = s.IdCampaniaGeneral,
                    IdCampaniaGeneralDetalle = s.IdCampaniaGeneralDetalle,
                    IdPlantilla = s.IdPlantilla,
                    IdPersonal = s.IdPersonal,
                    Dia = s.Dia,
                    FechaEvaluada = s.FechaInicioEnvioWhatsapp.AddDays(s.Dia - 1),
                    IdWhatsAppConfiguracionEnvio = s.IdWhatsAppConfiguracionEnvio,
                    ListaDiaConfigurado = new List<int>() { s.Dia1, s.Dia2, s.Dia3, s.Dia4, s.Dia5 },
                    Cantidad = 0
                }).ToList();

                foreach (var registro in listaRegistroEvaluado)
                {
                    try
                    {
                        #region Recuperacion Activa
                        var recuperacionActiva = _repRecuperacionAutomaticoModuloSistema.FirstBy(x => x.IdModuloSistema == ValorEstatico.IdModuloSistemaWhatsAppMailing && x.Tipo == "WhatsApp");

                        if (recuperacionActiva == null || (recuperacionActiva != null && !recuperacionActiva.Habilitado))
                        {
                            return Ok("No hay configuracion activa");
                        }
                        #endregion

                        bool obtencionPreResultadoExitosa = true;
                        bool insercionLogEjecucionExitosa = true;
                        bool actualizacionLogEjecucionExitosa = true;
                        bool actualizacionEstadoEnvioWhatsApp = true;
                        int nroActualIntentosObtencionListaPreProcesada = 0;
                        int nroActualIntentosInsercionLogEjecucion = 0;
                        int nroActualIntentosActualizacionLog = 0;
                        int nroActualIntentosActualizacionEstadoEnvio = 0;

                        #region Actualizacion registro En Proceso
                        var registroActualizable = _repRegistroRecuperacionWhatsApp.FirstBy(x => x.Id == registro.Id);

                        registroActualizable.RecuperacionEnProceso = true;
                        registroActualizable.FechaModificacion = DateTime.Now;
                        registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                        _repRegistroRecuperacionWhatsApp.Update(registroActualizable);
                        #endregion

                        registro.Cantidad = registro.ListaDiaConfigurado[registro.Dia - 1];

                        var cantidadResultante = _repRegistroRecuperacionWhatsApp.ObtenerCantidadWhatsAppPreprocesadoRealizado(registro.IdCampaniaGeneralDetalle, registro.IdPersonal, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));

                        /* Realizar envio restante */
                        if (Math.Abs(registro.Cantidad - cantidadResultante) > 0)
                        {
                            var respuesta = new List<WhatsAppConfiguracionPreEnvioDTO>();

                            #region Obtencion Prerresultado
                            do
                            {
                                try
                                {
                                    respuesta = _repWhatsAppConfiguracionPreEnvio.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(Math.Abs(registro.Cantidad - cantidadResultante), registro.IdCampaniaGeneralDetalle, registro.IdPersonal).Where(w => w.Celular != "1").ToList();

                                    obtencionPreResultadoExitosa = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _repLog.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ObtencionPreEnvioWhatsApp-Rec-Fallida",
                                            Parametros = $"Cantidad={Math.Abs(registro.Cantidad - cantidadResultante)}/IdCampaniaGeneralDetalle={registro.IdCampaniaGeneralDetalle}/IdPersonal={registro.IdPersonal}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "GET",
                                            IdPadre = 0,
                                            UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                            UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    obtencionPreResultadoExitosa = false;
                                    nroActualIntentosObtencionListaPreProcesada++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosObtencionListaPreProcesada >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Supero el limite de obtencionPreEnvioWhatsApp-Rec-Fallida");
                                    }
                                }
                            } while (!obtencionPreResultadoExitosa && nroActualIntentosObtencionListaPreProcesada < NRO_MAXIMO_INTENTOS);
                            #endregion

                            /* Inicio ejecucion envio */
                            if (respuesta.Any())
                            {
                                var logEjecucion = new WhatsAppConfiguracionLogEjecucionBO(_integraDBContext);

                                #region Guardado de log de ejecucion
                                do
                                {
                                    try
                                    {
                                        logEjecucion.FechaInicio = DateTime.Now;
                                        logEjecucion.IdWhatsAppConfiguracionEnvio = registro.IdWhatsAppConfiguracionEnvio;
                                        logEjecucion.Estado = true;
                                        logEjecucion.FechaCreacion = DateTime.Now;
                                        logEjecucion.FechaModificacion = DateTime.Now;
                                        logEjecucion.UsuarioCreacion = "Pre-RecuperacionF-CampaniaGeneral";
                                        logEjecucion.UsuarioModificacion = "Pre-RecuperacionF-CampaniaGeneral";

                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                                            scope.Complete();
                                        };

                                        insercionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "InsercionLogEjecucionWhatsApp",
                                                Parametros = $"FechaInicio={logEjecucion.FechaInicio}/IdWhatsAppConfiguracionEnvio={logEjecucion.IdWhatsAppConfiguracionEnvio}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "INSERT",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        insercionLogEjecucionExitosa = false;
                                        nroActualIntentosInsercionLogEjecucion++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosInsercionLogEjecucion >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de guardadoLogEjecucion");
                                        }
                                    }
                                } while (!insercionLogEjecucionExitosa && nroActualIntentosInsercionLogEjecucion < NRO_MAXIMO_INTENTOS);
                                #endregion

                                foreach (var item in respuesta)
                                {
                                    var objetoWhatsApp = new WhatsAppResultadoConjuntoListaDTO();

                                    try
                                    {
                                        objetoWhatsApp.IdPre = item.Id;
                                        objetoWhatsApp.IdConjuntoListaResultado = 1;
                                        objetoWhatsApp.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                                        objetoWhatsApp.IdAlumno = item.IdAlumno;
                                        objetoWhatsApp.Celular = item.Celular;
                                        objetoWhatsApp.IdCodigoPais = item.IdCodigoPais;
                                        objetoWhatsApp.NroEjecucion = item.NroEjecucion;
                                        objetoWhatsApp.Validado = item.Validado;
                                        objetoWhatsApp.Plantilla = item.Plantilla;
                                        objetoWhatsApp.IdPersonal = item.IdPersonal;
                                        objetoWhatsApp.IdPgeneral = item.IdPgeneral;
                                        objetoWhatsApp.IdPlantilla = item.IdPlantilla;

                                        if (!string.IsNullOrEmpty(item.objetoplantilla))
                                        {
                                            objetoWhatsApp.objetoplantilla = JsonConvert.DeserializeObject<List<datoPlantillaWhatsApp>>(item.objetoplantilla);
                                            resultado.Add(objetoWhatsApp);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "MapeoObjetoWhatsApp",
                                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={objetoWhatsApp.IdPre}/IdPrioridadMailChimpListaCorreo={objetoWhatsApp.IdPrioridadMailChimpListaCorreo}/IdPersonal={objetoWhatsApp.IdPersonal}/IdPGeneral={objetoWhatsApp.IdPgeneral}/IdPlantilla={objetoWhatsApp.IdPlantilla}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "MAP",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }

                                var EnvioMensajes = EnvioAutomaticoPlantillaMasivo(ref resultado, registro.IdPersonal, registro.IdPlantilla, logEjecucion.Id, true);

                                #region Actualizacion de logEjecucion Final
                                do
                                {
                                    try
                                    {
                                        var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecucion.Id);

                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            logEjecucionFinal.FechaFin = DateTime.Now;
                                            _repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);

                                            scope.Complete();
                                        };

                                        actualizacionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionLogEjecucionWhatsApp",
                                                Parametros = string.Empty,
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionLogEjecucionExitosa = false;
                                        nroActualIntentosActualizacionLog++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionLog >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionLogEjecucionWhatsApp");
                                        }
                                    }
                                } while (!actualizacionLogEjecucionExitosa && nroActualIntentosActualizacionLog < NRO_MAXIMO_INTENTOS);
                                #endregion

                                #region Actualizar estado envio WhatsApp
                                do
                                {
                                    try
                                    {
                                        _repCampaniaGeneral.ActualizarEstadoEnvioWhatsApp(registro.IdCampaniaGeneral);

                                        actualizacionEstadoEnvioWhatsApp = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionEstadoEnvioWhatsApp",
                                                Parametros = $"IdCampaniaGeneral={registro.IdCampaniaGeneral}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = "RecuperacionFallida",
                                                UsuarioModificacion = "RecuperacionFallida",
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionEstadoEnvioWhatsApp = false;
                                        nroActualIntentosActualizacionEstadoEnvio++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionEstadoEnvio >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionEstadoEnvioWhatsApp");
                                        }
                                    }
                                } while (!actualizacionEstadoEnvioWhatsApp && nroActualIntentosActualizacionEstadoEnvio < NRO_MAXIMO_INTENTOS);
                                #endregion
                            }
                            /* Fin ejecucion envio */
                        }

                        #region Actualizacion registro Finalizado
                        registroActualizable = _repRegistroRecuperacionWhatsApp.FirstBy(x => x.Id == registro.Id);

                        registroActualizable.RecuperacionEnProceso = false;
                        registroActualizable.Fallido = false;
                        registroActualizable.Completado = true;
                        registroActualizable.FechaModificacion = DateTime.Now;
                        registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                        _repRegistroRecuperacionWhatsApp.Update(registroActualizable);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";
                            var cantidadResultante = _repRegistroRecuperacionWhatsApp.ObtenerCantidadWhatsAppPreprocesadoRealizado(registro.IdCampaniaGeneralDetalle, registro.IdPersonal, DateTime.Now.Date, DateTime.Now.Date.AddDays(1));

                            _repLog.Insert(new TLog
                            {
                                Ip = "-",
                                Usuario = "-",
                                Maquina = "-",
                                Ruta = "ObtencionPreEnvioWhatsApp-Rec-Fallida",
                                Parametros = $"Cantidad={(Math.Abs(registro.Cantidad - cantidadResultante))}/IdCampaniaGeneralDetalle={registro.IdCampaniaGeneralDetalle}/IdPersonal={registro.IdPersonal}",
                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                Tipo = "GET",
                                IdPadre = 0,
                                UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            });

                            #region Actualizacion registro Finalizado
                            var registroActualizable = _repRegistroRecuperacionWhatsApp.FirstBy(x => x.Id == registro.Id);

                            registroActualizable.RecuperacionEnProceso = false;
                            registroActualizable.Fallido = true;
                            registroActualizable.Completado = false;
                            registroActualizable.FechaModificacion = DateTime.Now;
                            registroActualizable.UsuarioModificacion = "RecuperacionFallida";

                            _repRegistroRecuperacionWhatsApp.Update(registroActualizable);
                            #endregion
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Autor: Carlos Crispin Riquelme
        /// Descripción: La funcion  realice el envio respectivo ya a los datos validados  segun campania general
        /// </summary>
        /// <returns>Response 200, Caso contrario response 400 con el error</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult EjecutarCampaniaGeneralEnvioWhatsApp()
        {
            try
            {
                var listaWhatsApp = _repCampaniaGeneral.ObtenerActividadCampaniaGeneralParaEjecutar();

                if (listaWhatsApp.Any())
                {
                    bool insercionRegistroExitosa = true;
                    bool obtencionPreResultadoExitosa = true;
                    bool insercionLogEjecucionExitosa = true;
                    bool actualizacionLogEjecucionExitosa = true;
                    bool actualizacionRecuperacionWhatsApp = true;
                    bool actualizacionEstadoEnvioWhatsApp = true;
                    bool actualizacionIntentoFallidoWhatsApp = true;
                    int nroActualIntentosRegistroSeguimiento = 0;
                    int nroActualIntentosObtencionListaPreProcesada = 0;
                    int nroActualIntentosInsercionLogEjecucion = 0;
                    int nroActualIntentosActualizacionLog = 0;
                    int nroActualIntentosActualizacionRecuperacion = 0;
                    int nroActualIntentosActualizacionEstadoEnvio = 0;
                    int nroActualIntentosFallidosActualizacion = 0;

                    #region InsercionRegistroSeguimientoRecuperacion
                    do
                    {
                        try
                        {
                            var registroSeguimientoRecuperacion = listaWhatsApp.Select(s => new RegistroRecuperacionWhatsAppBO()
                            {
                                IdCampaniaGeneral = s.IdCampaniaGeneral,
                                IdCampaniaGeneralDetalle = s.IdCampaniaGeneralDetalle,
                                IdCampaniaGeneralDetalleResponsable = s.IdCampaniaGeneralDetalleResponsable,
                                IdPersonal = s.IdPersonal,
                                IdPlantilla = s.IdPlantilla,
                                IdWhatsAppConfiguracionEnvio = s.IdWhatsAppConfiguracionEnvio,
                                Dia = s.Dia,
                                Dia1 = s.Dia1,
                                Dia2 = s.Dia2,
                                Dia3 = s.Dia3,
                                Dia4 = s.Dia4,
                                Dia5 = s.Dia5,
                                FechaInicioEnvioWhatsapp = s.FechaInicioEnvioWhatsapp,
                                FechaFinEnvioWhatsapp = s.FechaFinEnvioWhatsapp,
                                HoraEnvio = s.HoraEnvio,
                                Completado = false,
                                Fallido = false,
                                Estado = true,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }).ToList();

                            using (TransactionScope scope = new TransactionScope())
                            {
                                _repRegistroRecuperacionWhatsApp.Insert(registroSeguimientoRecuperacion);
                                scope.Complete();
                            }

                            insercionRegistroExitosa = true;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                _repLog.Insert(new TLog
                                {
                                    Ip = "-",
                                    Usuario = "-",
                                    Maquina = "-",
                                    Ruta = "InsercionRegistroRecuperacionWhatsApp",
                                    Parametros = $"IdCampaniaGeneral={listaWhatsApp.Select(s => s.IdCampaniaGeneral).Distinct().ToList()}/IdCampaniaGeneralDetalle={listaWhatsApp.Select(s => s.IdCampaniaGeneralDetalle).Distinct().ToList()}/IdCampaniaGeneralDetalleResponsable={listaWhatsApp.Select(s => s.IdCampaniaGeneralDetalleResponsable).Distinct().ToList()}/IdWhatsAppConfiguracionEnvio={listaWhatsApp.Select(s => s.IdWhatsAppConfiguracionEnvio).Distinct().ToList()}",
                                    Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                    Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                    Tipo = "INSERT",
                                    IdPadre = 0,
                                    UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                    UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                });
                            }
                            catch (Exception)
                            {
                            }

                            insercionRegistroExitosa = false;
                            nroActualIntentosRegistroSeguimiento++;
                            Thread.Sleep(2000);

                            if (nroActualIntentosRegistroSeguimiento >= NRO_MAXIMO_INTENTOS)
                            {
                                throw new Exception("Supero el limite de registroSeguimientoRecuperacion");
                            }
                        }
                    } while (!insercionRegistroExitosa && nroActualIntentosRegistroSeguimiento < NRO_MAXIMO_INTENTOS);
                    #endregion

                    foreach (var campaniaGeneralWhatsApp in listaWhatsApp)
                    {
                        try
                        {
                            var resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                            int cantidad;

                            switch (campaniaGeneralWhatsApp.Dia)
                            {
                                case 1:
                                    cantidad = campaniaGeneralWhatsApp.Dia1;
                                    break;
                                case 2:
                                    cantidad = campaniaGeneralWhatsApp.Dia2;
                                    break;
                                case 3:
                                    cantidad = campaniaGeneralWhatsApp.Dia3;
                                    break;
                                case 4:
                                    cantidad = campaniaGeneralWhatsApp.Dia4;
                                    break;
                                case 5:
                                    cantidad = campaniaGeneralWhatsApp.Dia5;
                                    break;
                                default:
                                    cantidad = 0;
                                    break;
                            }

                            var respuesta = new List<WhatsAppConfiguracionPreEnvioDTO>();

                            #region Obtencion Prerresultado
                            do
                            {
                                try
                                {
                                    respuesta = _repWhatsAppConfiguracionPreEnvio.ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(cantidad, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdPersonal).Where(w => w.Celular != "1").ToList();

                                    obtencionPreResultadoExitosa = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _repLog.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ObtencionPreEnvioWhatsApp",
                                            Parametros = $"Cantidad={cantidad}/IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdPersonal={campaniaGeneralWhatsApp.IdPersonal}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "GET",
                                            IdPadre = 0,
                                            UsuarioCreacion = "WhatsAppEnvioCampaniaGeneral",
                                            UsuarioModificacion = "WhatsAppEnvioCampaniaGeneral",
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    obtencionPreResultadoExitosa = false;
                                    nroActualIntentosObtencionListaPreProcesada++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosObtencionListaPreProcesada >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Supero el limite de obtencionPreEnvioWhatsApp");
                                    }
                                }
                            } while (!obtencionPreResultadoExitosa && nroActualIntentosObtencionListaPreProcesada < NRO_MAXIMO_INTENTOS);
                            #endregion

                            /* Inicio ejecucion envio */
                            if (respuesta.Any())
                            {
                                var logEjecucion = new WhatsAppConfiguracionLogEjecucionBO(_integraDBContext);

                                #region Guardado de log de ejecucion
                                do
                                {
                                    try
                                    {
                                        logEjecucion.FechaInicio = DateTime.Now;
                                        logEjecucion.IdWhatsAppConfiguracionEnvio = campaniaGeneralWhatsApp.IdWhatsAppConfiguracionEnvio;
                                        logEjecucion.Estado = true;
                                        logEjecucion.FechaCreacion = DateTime.Now;
                                        logEjecucion.FechaModificacion = DateTime.Now;
                                        logEjecucion.UsuarioCreacion = "Pre-ProcesoAutomatico-CampaniaGeneral";
                                        logEjecucion.UsuarioModificacion = "Pre-ProcesoAutomatico-CampaniaGeneral";

                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                                            scope.Complete();
                                        };

                                        insercionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "InsercionLogEjecucionWhatsApp",
                                                Parametros = $"FechaInicio={logEjecucion.FechaInicio}/IdWhatsAppConfiguracionEnvio={logEjecucion.IdWhatsAppConfiguracionEnvio}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "INSERT",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        insercionLogEjecucionExitosa = false;
                                        nroActualIntentosInsercionLogEjecucion++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosInsercionLogEjecucion >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de guardadoLogEjecucion");
                                        }
                                    }
                                } while (!insercionLogEjecucionExitosa && nroActualIntentosInsercionLogEjecucion < NRO_MAXIMO_INTENTOS);
                                #endregion

                                foreach (var item in respuesta)
                                {
                                    var objetoWhatsApp = new WhatsAppResultadoConjuntoListaDTO();

                                    try
                                    {
                                        objetoWhatsApp.IdPre = item.Id;
                                        objetoWhatsApp.IdConjuntoListaResultado = 1;
                                        objetoWhatsApp.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                                        objetoWhatsApp.IdAlumno = item.IdAlumno;
                                        objetoWhatsApp.Celular = item.Celular;
                                        objetoWhatsApp.IdCodigoPais = item.IdCodigoPais;
                                        objetoWhatsApp.NroEjecucion = item.NroEjecucion;
                                        objetoWhatsApp.Validado = item.Validado;
                                        objetoWhatsApp.Plantilla = item.Plantilla;
                                        objetoWhatsApp.IdPersonal = item.IdPersonal;
                                        objetoWhatsApp.IdPgeneral = item.IdPgeneral;
                                        objetoWhatsApp.IdPlantilla = item.IdPlantilla;

                                        if (!string.IsNullOrEmpty(item.objetoplantilla))
                                        {
                                            objetoWhatsApp.objetoplantilla = JsonConvert.DeserializeObject<List<datoPlantillaWhatsApp>>(item.objetoplantilla);
                                            resultado.Add(objetoWhatsApp);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "MapeoObjetoWhatsApp",
                                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={objetoWhatsApp.IdPre}/IdPrioridadMailChimpListaCorreo={objetoWhatsApp.IdPrioridadMailChimpListaCorreo}/IdPersonal={objetoWhatsApp.IdPersonal}/IdPGeneral={objetoWhatsApp.IdPgeneral}/IdPlantilla={objetoWhatsApp.IdPlantilla}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "MAP",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }

                                var EnvioMensajes = EnvioAutomaticoPlantillaMasivo(ref resultado, campaniaGeneralWhatsApp.IdPersonal, campaniaGeneralWhatsApp.IdPlantilla, logEjecucion.Id, true);

                                #region Actualizacion de logEjecucion Final
                                do
                                {
                                    try
                                    {
                                        var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecucion.Id);

                                        using (TransactionScope scope = new TransactionScope())
                                        {
                                            logEjecucionFinal.FechaFin = DateTime.Now;
                                            _repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);

                                            scope.Complete();
                                        };

                                        actualizacionLogEjecucionExitosa = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionLogEjecucionWhatsApp",
                                                Parametros = string.Empty,
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionLogEjecucionExitosa = false;
                                        nroActualIntentosActualizacionLog++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionLog >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionLogEjecucionWhatsApp");
                                        }
                                    }
                                } while (!actualizacionLogEjecucionExitosa && nroActualIntentosActualizacionLog < NRO_MAXIMO_INTENTOS);
                                #endregion

                                #region Actualizar estado envio WhatsApp
                                do
                                {
                                    try
                                    {
                                        _repCampaniaGeneral.ActualizarEstadoEnvioWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneral);

                                        actualizacionEstadoEnvioWhatsApp = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                            _repLog.Insert(new TLog
                                            {
                                                Ip = "-",
                                                Usuario = "-",
                                                Maquina = "-",
                                                Ruta = "ActualizacionEstadoEnvioWhatsApp",
                                                Parametros = $"IdCampaniaGeneral={campaniaGeneralWhatsApp.IdCampaniaGeneral}",
                                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                                Tipo = "UPDATE",
                                                IdPadre = 0,
                                                UsuarioCreacion = string.Empty,
                                                UsuarioModificacion = string.Empty,
                                                FechaCreacion = DateTime.Now,
                                                FechaModificacion = DateTime.Now,
                                                Estado = true
                                            });
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        actualizacionEstadoEnvioWhatsApp = false;
                                        nroActualIntentosActualizacionEstadoEnvio++;
                                        Thread.Sleep(2000);

                                        if (nroActualIntentosActualizacionEstadoEnvio >= NRO_MAXIMO_INTENTOS)
                                        {
                                            throw new Exception("Supero el limite de ActualizacionEstadoEnvioWhatsApp");
                                        }
                                    }
                                } while (!actualizacionEstadoEnvioWhatsApp && nroActualIntentosActualizacionEstadoEnvio < NRO_MAXIMO_INTENTOS);
                                #endregion
                            }
                            /* Fin ejecucion envio */

                            #region Actualizacion de registro WhatsApp recuperacion
                            do
                            {
                                try
                                {
                                    _repRegistroRecuperacionWhatsApp.ActualizarCompletadoRegistroWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable);

                                    actualizacionRecuperacionWhatsApp = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _repLog.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ActualizacionRecuperacionWhatsApp",
                                            Parametros = $"IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdCampaniaGeneralDetalleResponsable={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "UPDATE",
                                            IdPadre = 0,
                                            UsuarioCreacion = string.Empty,
                                            UsuarioModificacion = string.Empty,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    actualizacionRecuperacionWhatsApp = false;
                                    nroActualIntentosActualizacionRecuperacion++;
                                    Thread.Sleep(2000);

                                    if (nroActualIntentosActualizacionRecuperacion >= NRO_MAXIMO_INTENTOS)
                                    {
                                        throw new Exception("Error Envio WhatsApp");
                                    }
                                }
                            } while (!actualizacionRecuperacionWhatsApp && nroActualIntentosActualizacionRecuperacion < NRO_MAXIMO_INTENTOS);
                            #endregion
                        }
                        catch (Exception exx)
                        {
                            do
                            {
                                try
                                {
                                    _repRegistroRecuperacionWhatsApp.ActualizarFalloRegistroWhatsApp(campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle, campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable);

                                    actualizacionIntentoFallidoWhatsApp = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        string mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                                        _repLog.Insert(new TLog
                                        {
                                            Ip = "-",
                                            Usuario = "-",
                                            Maquina = "-",
                                            Ruta = "ActualizacionFalloRecuperacionWhatsApp",
                                            Parametros = $"IdCampaniaGeneralDetalle={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalle}/IdCampaniaGeneralDetalleResponsable={campaniaGeneralWhatsApp.IdCampaniaGeneralDetalleResponsable}",
                                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                            Tipo = "UPDATE",
                                            IdPadre = 0,
                                            UsuarioCreacion = string.Empty,
                                            UsuarioModificacion = string.Empty,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                            Estado = true
                                        });
                                    }
                                    catch (Exception)
                                    {
                                    }

                                    nroActualIntentosFallidosActualizacion++;
                                    actualizacionIntentoFallidoWhatsApp = false;
                                    Thread.Sleep(2000);
                                }
                            } while (!actualizacionIntentoFallidoWhatsApp && nroActualIntentosFallidosActualizacion < NRO_MAXIMO_INTENTOS);
                        }
                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                var _repLog = new LogRepositorio(_integraDBContext);

                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "EnviarWhatsApp", Parametros = $"", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "SEND", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion permite visualizar las listas pre procesadas por la vista del sistema
        /// </summary>
        /// <param name="ListasWhatsApp"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult VisualizacionListasWhatsAppEnvioAutomaticoMasivo([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListasWhatsApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RegistroWhatsAppConfiguracionPreEnvioDTO Registro = new RegistroWhatsAppConfiguracionPreEnvioDTO();
                Registro.ListaPreConfigurados = new List<VistaWhatsAppConfiguracionPreEnvioDTO>();

                try
                {
                    foreach (var item in ListasWhatsApp)
                    {
                        var Respuesta = _repWhatsAppConfiguracionPreEnvio.ListasVisualizarWhatsAppEnvioAutomaticoMasivoPreProcesada(item.IdConjuntoListaDetalle);

                        if (Respuesta != null && Respuesta.Count > 0)
                        {
                            Registro.NumeroValidos = Registro.NumeroValidos + Respuesta.Where(x => x.Validado == true).Count();
                            Registro.NumerosNoValidos = Registro.NumerosNoValidos + Respuesta.Where(x => x.Validado == false).Count();
                            Registro.ListaPreConfigurados.AddRange(Respuesta);
                        }
                    }

                    return Ok(Registro);

                }
                catch (Exception ex)
                {
                    return Ok(ex.Message);
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Esta funcion nos permite ver el estado en el que se encuentra el pre-procesamiento
        /// </summary>
        /// <param name="IdConjuntoLista"></param>
        /// <returns></returns>
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpPost]
        public ActionResult EstadoSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Respuesta = _repWhatsAppConfiguracionPreEnvio.RegistroSeguimientoPreProcesoListaWhatsApp(IdConjuntoLista);

                if (Respuesta != null)
                {
                    return Ok(Respuesta);
                }
                else
                {
                    RegistroSeguimientoPreProcesoListaWhatsAppDTO SinDato = new RegistroSeguimientoPreProcesoListaWhatsAppDTO();
                    SinDato.IdEstadoSeguimientoPreProcesoListaWhatsApp = 1;
                    return Ok(SinDato);
                }



            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnvioMasivoReasignacionesOperaciones()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                WhatsAppConfiguracionLogEjecucionBO logEjecion = new WhatsAppConfiguracionLogEjecucionBO();
                try
                {
                    logEjecion.FechaInicio = DateTime.Now;
                    logEjecion.IdWhatsAppConfiguracionEnvio = 1;
                    logEjecion.Estado = true;
                    logEjecion.FechaCreacion = DateTime.Now;
                    logEjecion.FechaModificacion = DateTime.Now;
                    logEjecion.UsuarioCreacion = "EnvioOperaciones";
                    logEjecion.UsuarioModificacion = "EnvioOperaciones";
                    _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);


                    var Respuesta = _repConjuntoListaResultado.ObtenerOportunidadesReasignadasOperaciones();
                    this.ValidarNumeroConjuntoOperaciones(ref Respuesta, 1);
                    Respuesta = Respuesta.Where(w => w.Validado == true).ToList();

                    this.RemplazarEtiquetasOperaciones(ref Respuesta);
                    Respuesta = Respuesta.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();

                    this.EnvioAutomaticoPlantillaOperaciones(Respuesta, logEjecion.Id);

                    var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecion.Id);
                    logEjecucionFinal.FechaFin = DateTime.Now;
                    _repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);


                }
                catch (Exception ex)
                {
                    try
                    {
                        if (logEjecion.Id == 0 || logEjecion.Id == null)
                        {
                            logEjecion.FechaFin = DateTime.Now;
                            _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);
                        }
                    }
                    catch (Exception e)
                    {

                    }


                }
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        private void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdPersonal, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var Alumno in NumerosValidados)
            {
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + Alumno.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };


                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(Alumno.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);


                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                Alumno.Validado = false;
                            }
                            else
                            {
                                Alumno.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        _repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        Alumno.Validado = false;

                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        _repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + Alumno.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }
        }
        public static List<List<int>> Split(List<int> source, int maxSubItems)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / maxSubItems)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Los registros de mailing general con los numeros para consolidarlos en un array json para whatsapp y los valide, dando una respuesta en grupo de todos los numeros validos o no validos, con una sola conexion
        /// </summary>
        /// <param name="NumerosValidados">Lista de objetos de tipo PreWhatsAppResultadoConjuntoListaDTO</param>
        /// <param name="IdPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="IdWhatsAppConfiguracionEnvio">Id de la configuracion de envio de WhatsApp (PK de la tabla mkt.T_WhatsAppConfiguracionEnvio)</param>
        /// <param name="IdPlantilla">Id de la plantilla que se enviara (PK de la tabla mkt.T_Plantilla)</param>
        /// <returns>Lista de objetos de tipo PreWhatsAppResultadoConjuntoListaDTO</returns>
        private List<PreWhatsAppResultadoConjuntoListaDTO> ValidarNumeroMailingGeneralMasivo(List<PreWhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdPersonal, int IdWhatsAppConfiguracionEnvio, int IdPlantilla)
        {
            string urlToPost = string.Empty;
            bool banderaLogin = false;
            string tokenComunicacion = string.Empty;
            string resultado = string.Empty;

            var listaPreWhatsAppMensajePublicidad = new List<PreWhatsAppResultadoConjuntoListaDTO>();
            try
            {
                var _repWhatsAppEstadoValidacion = new WhatsAppEstadoValidacionRepositorio(_integraDBContext);

                var listaPaises = NumerosValidados.Where(x => x.IdCodigoPais != -1).GroupBy(x => x.IdCodigoPais).Select(x => x.Key).ToList();

                if (listaPaises != null && listaPaises.Any())
                {
                    var listaEstados = _repWhatsAppEstadoValidacion.ObtenerListaEstadosValidacionNumeroWhatsApp();

                    foreach (var item in listaPaises)
                    {
                        var listaNumeros = NumerosValidados.Where(x => x.IdCodigoPais == item).ToList();

                        if (listaNumeros != null && listaNumeros.Any())
                        {
                            resultado = string.Empty;

                            var subListasBloque =
                                 listaNumeros.Select(s => string.Concat("+", s.Celular)).Select((x, i) => new { Index = i, Value = x })
                                .GroupBy(x => x.Index / 2500)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();

                            ServicePointManager.ServerCertificateValidationCallback =
                                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                                    {
                                        return true;
                                    };

                            var credencialesHost = _repCredenciales.ObtenerCredencialHost(item);
                            var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(ValorEstatico.IdPersonalWhatsappNuevasOportunidades, item);

                            if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                            {
                                string UrlToPostUsuario = credencialesHost.UrlWhatsApp + "/v1/users/login";

                                var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                                var cliente = new RestClient(UrlToPostUsuario);
                                var peticion = new RestSharp.RestRequest(Method.POST);
                                peticion.AddHeader("cache-control", "no-cache");
                                peticion.AddHeader("Content-Length", "");
                                peticion.AddHeader("Accept-Encoding", "gzip, deflate");
                                peticion.AddHeader("Host", credencialesHost.IpHost);
                                peticion.AddHeader("Cache-Control", "no-cache");
                                peticion.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                                peticion.AddHeader("Content-Type", "application/json");
                                IRestResponse Response = cliente.Execute(peticion);

                                if (Response.StatusCode == HttpStatusCode.OK)
                                {
                                    var Datos = JsonConvert.DeserializeObject<userLogeo>(Response.Content);

                                    foreach (var itemCredencial in Datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                        modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                        modelCredencial.IdWhatsAppConfiguracion = credencialesHost.Id;
                                        modelCredencial.UserAuthToken = itemCredencial.token;
                                        modelCredencial.ExpiresAfter = Convert.ToDateTime(itemCredencial.expires_after);
                                        modelCredencial.EsMigracion = true;
                                        modelCredencial.Estado = true;
                                        modelCredencial.FechaCreacion = DateTime.Now;
                                        modelCredencial.FechaModificacion = DateTime.Now;
                                        modelCredencial.UsuarioCreacion = "whatsapp";
                                        modelCredencial.UsuarioModificacion = "whatsapp";

                                        modelCredencial.Id = _repTokenUsuario.InsertarWhatsAppUsuarioCredencial(modelCredencial);

                                        tokenComunicacion = itemCredencial.token;
                                    }

                                    banderaLogin = true;
                                }
                                else
                                    banderaLogin = false;
                            }
                            else
                            {
                                tokenComunicacion = tokenValida.UserAuthToken;
                                banderaLogin = true;
                            }

                            foreach (var contactos in subListasBloque)
                            {
                                try
                                {
                                    var nuevoBloque = new ValidarNumerosWhatsAppDTO() { blocking = "wait", contacts = contactos };

                                    var mensajeJson = JsonConvert.SerializeObject(nuevoBloque);

                                    urlToPost = credencialesHost.UrlWhatsApp + "/v1/contacts";

                                    if (banderaLogin)
                                    {
                                        using (WebClient Client = new WebClient())
                                        {
                                            Client.Encoding = Encoding.UTF8;

                                            var Serializer = new JavaScriptSerializer();

                                            var SerializedResult = Serializer.Serialize(nuevoBloque);
                                            string MyParameters = SerializedResult;
                                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + tokenComunicacion;
                                            Client.Headers[HttpRequestHeader.ContentLength] = mensajeJson.Length.ToString();
                                            Client.Headers[HttpRequestHeader.Host] = credencialesHost.IpHost;
                                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";

                                            resultado = Client.UploadString(urlToPost, MyParameters);
                                        }

                                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                                        string numeroValidadoRespuesta = string.Empty;

                                        // Preparacion de BO
                                        List<WhatsAppMensajePublicidadBO> listaWhatsAppMensajePublicidad = new List<WhatsAppMensajePublicidadBO>();

                                        foreach (var itemRespuesta in datoRespuesta.contacts)
                                        {
                                            numeroValidadoRespuesta = itemRespuesta.status == "valid" ? itemRespuesta.wa_id : itemRespuesta.input.Replace("+", string.Empty);

                                            var usuarioEstado = listaNumeros.FirstOrDefault(x => x.Celular == numeroValidadoRespuesta);

                                            if (usuarioEstado != null)
                                            {
                                                usuarioEstado.Validado = itemRespuesta.status == "valid";

                                                var respuestaEstado = listaEstados.FirstOrDefault(x => x.Nombre == itemRespuesta.status);

                                                usuarioEstado.IdWhatsAppEstadoValidacion = respuestaEstado != null ? respuestaEstado.Id : ValorEstatico.IdWhatsAppEstadoValidacionFallido;

                                                listaWhatsAppMensajePublicidad.Add(new WhatsAppMensajePublicidadBO()
                                                {
                                                    IdAlumno = usuarioEstado.IdAlumno,
                                                    IdPersonal = IdPersonal,
                                                    IdPrioridadMailChimpListaCorreo = usuarioEstado.IdPrioridadMailChimpListaCorreo,
                                                    IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio,
                                                    IdPais = usuarioEstado.IdCodigoPais,
                                                    Celular = usuarioEstado.Celular,
                                                    EsValido = usuarioEstado.Validado,
                                                    Estado = true,
                                                    IdWhatsAppEstadoValidacion = usuarioEstado.IdWhatsAppEstadoValidacion,
                                                    FechaCreacion = DateTime.Now,
                                                    FechaModificacion = DateTime.Now,
                                                    UsuarioCreacion = "Pre-ValidacionAutomatica",
                                                    UsuarioModificacion = "Pre-ValidacionAutomatica"
                                                });
                                            }
                                        }

                                        bool respuestaInsercion = _repWhatsAppMensajePublicidad.InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(listaWhatsAppMensajePublicidad);
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    return listaPreWhatsAppMensajePublicidad;
                }
                else
                    return new List<PreWhatsAppResultadoConjuntoListaDTO>();
            }
            catch (Exception)
            {
                return listaPreWhatsAppMensajePublicidad;
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: los registros de conjunto de lista con los numeros es envia para consolidarlos en un array json para whatsapp y los valide, dando una respuesta en grupo de todos los nuemros validos o no validos, con una sola conexion
        /// </summary>
        /// <param name="NumerosValidados">Lista de numeros validar</param>
        /// <param name="IdPersonal">Identifiacor del personla</param>
        /// <param name="IdWhatsAppConfiguracionEnvio">Identificador de la configuracion</param>
        /// <param name="IdPlantilla">Identificador de la plantilla de whatsapp</param>
        /// <returns></returns>
        private List<PreWhatsAppResultadoConjuntoListaDTO> ValidarNumeroConjuntoListaMasivo(List<PreWhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdPersonal, int IdWhatsAppConfiguracionEnvio, int IdPlantilla, int IdConjuntoLista)
        {
            string UrlToPost;
            bool BanderaLogin = false;
            string TokenComunicacion = string.Empty;
            string Resultado = string.Empty;
            int EstadoValiacion = 0;

            WhatsAppEstadoValidacionRepositorio WhatsAppEstadoValidacionRepositorio = new WhatsAppEstadoValidacionRepositorio();

            ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
            DTO.contacts = new List<string>();
            DTO.blocking = "wait";

            List<PreWhatsAppResultadoConjuntoListaDTO> ListaPreWhatsAppMensajePublicidad = new List<PreWhatsAppResultadoConjuntoListaDTO>();

            var ListaPaises = NumerosValidados.Where(x => x.IdCodigoPais != -1).GroupBy(x => x.IdCodigoPais).Select(x => x.Key).ToList();

            if (ListaPaises != null && ListaPaises.Count > 0)
            {
                var ListaEstados = WhatsAppEstadoValidacionRepositorio.ObtenerListaEstadosValidacionNumeroWhatsApp();
                foreach (var item in ListaPaises)
                {
                    var ListaNumeros = NumerosValidados.Where(x => x.IdCodigoPais == item).ToList();
                    if (ListaNumeros != null && ListaNumeros.Count > 0)
                    {
                        foreach (var Alumno in ListaNumeros)
                        {
                            DTO.contacts.Add("+" + Alumno.Celular);
                        }

                        var subListasBloque = DTO.contacts.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / 2500)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();
                        try
                        {
                            ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };

                            var CredencialesHost = _repCredenciales.ObtenerCredencialHost(item);
                            var TokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, item);

                            var MensajeJson = JsonConvert.SerializeObject(DTO);
                            Resultado = string.Empty;

                            if (TokenValida == null || DateTime.Now >= TokenValida.ExpiresAfter)
                            {
                                string UrlToPostUsuario = CredencialesHost.UrlWhatsApp + "/v1/users/login";

                                var UserLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                                var Client = new RestClient(UrlToPostUsuario);
                                var Request = new RestSharp.RestRequest(Method.POST);
                                Request.AddHeader("cache-control", "no-cache");
                                Request.AddHeader("Content-Length", "");
                                Request.AddHeader("Accept-Encoding", "gzip, deflate");
                                Request.AddHeader("Host", CredencialesHost.IpHost);
                                Request.AddHeader("Cache-Control", "no-cache");
                                Request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(UserLogin.UserUsername + ":" + UserLogin.UserPassword)));
                                Request.AddHeader("Content-Type", "application/json");
                                IRestResponse Response = Client.Execute(Request);

                                if (Response.StatusCode == HttpStatusCode.OK)
                                {
                                    var Datos = JsonConvert.DeserializeObject<userLogeo>(Response.Content);

                                    foreach (var itemCredencial in Datos.users)
                                    {
                                        TWhatsAppUsuarioCredencial ModelCredencial = new TWhatsAppUsuarioCredencial();

                                        ModelCredencial.IdWhatsAppUsuario = UserLogin.IdWhatsAppUsuario;
                                        ModelCredencial.IdWhatsAppConfiguracion = CredencialesHost.Id;
                                        ModelCredencial.UserAuthToken = itemCredencial.token;
                                        ModelCredencial.ExpiresAfter = Convert.ToDateTime(itemCredencial.expires_after);
                                        ModelCredencial.EsMigracion = true;
                                        ModelCredencial.Estado = true;
                                        ModelCredencial.FechaCreacion = DateTime.Now;
                                        ModelCredencial.FechaModificacion = DateTime.Now;
                                        ModelCredencial.UsuarioCreacion = "whatsapp";
                                        ModelCredencial.UsuarioModificacion = "whatsapp";

                                        var Rpta = _repTokenUsuario.Insert(ModelCredencial);

                                        TokenComunicacion = itemCredencial.token;
                                    }

                                    BanderaLogin = true;

                                }
                                else
                                {
                                    BanderaLogin = false;
                                }

                            }
                            else
                            {
                                TokenComunicacion = TokenValida.UserAuthToken;
                                BanderaLogin = true;
                            }

                            UrlToPost = CredencialesHost.UrlWhatsApp + "/v1/contacts";

                            foreach (var contactos in subListasBloque)
                            {
                                var nuevoBloque = new ValidarNumerosWhatsAppDTO() { blocking = DTO.blocking, contacts = contactos };
                                var mensajeJson = JsonConvert.SerializeObject(nuevoBloque);
                                if (BanderaLogin)
                                {
                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;

                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(nuevoBloque);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = mensajeJson.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(UrlToPost, MyParameters);


                                    }

                                    var DatoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(Resultado);
                                    string NumeroValidadoRespuesta = string.Empty;

                                    foreach (var itemRespuesta in DatoRespuesta.contacts)
                                    {
                                        if (itemRespuesta.status == "valid")
                                        {
                                            NumeroValidadoRespuesta = itemRespuesta.wa_id;
                                        }
                                        else
                                        {
                                            NumeroValidadoRespuesta = itemRespuesta.input.Replace("+", "");
                                        }

                                        var UsuarioEstado = ListaNumeros.Where(x => x.Celular == NumeroValidadoRespuesta).FirstOrDefault();
                                        if (UsuarioEstado != null)
                                        {
                                            if (itemRespuesta.status == "valid")
                                            {
                                                UsuarioEstado.Validado = true;
                                            }
                                            else
                                            {
                                                UsuarioEstado.Validado = false;
                                            }

                                            var RespuestaEstado = ListaEstados.Where(x => x.Nombre == itemRespuesta.status).FirstOrDefault();

                                            if (RespuestaEstado != null)
                                            {
                                                EstadoValiacion = RespuestaEstado.Id;
                                            }
                                            else
                                            {
                                                EstadoValiacion = 4;
                                            }
                                            UsuarioEstado.IdWhatsAppEstadoValidacion = EstadoValiacion;


                                            WhatsAppMensajePublicidadBO WhatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                                            WhatsAppMensajePublicidad.IdAlumno = UsuarioEstado.IdAlumno;
                                            WhatsAppMensajePublicidad.IdPersonal = IdPersonal;
                                            WhatsAppMensajePublicidad.IdConjuntoListaResultado = UsuarioEstado.IdConjuntoListaResultado;
                                            WhatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                                            WhatsAppMensajePublicidad.IdPais = UsuarioEstado.IdCodigoPais;
                                            WhatsAppMensajePublicidad.Celular = UsuarioEstado.Celular;
                                            WhatsAppMensajePublicidad.EsValido = UsuarioEstado.Validado;
                                            WhatsAppMensajePublicidad.Estado = true;
                                            WhatsAppMensajePublicidad.IdWhatsAppEstadoValidacion = EstadoValiacion;
                                            WhatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                                            WhatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                                            WhatsAppMensajePublicidad.UsuarioCreacion = "Pre-ValidacionAutomatica";
                                            WhatsAppMensajePublicidad.UsuarioModificacion = "Pre-ValidacionAutomatica";
                                            _repWhatsAppMensajePublicidad.Insert(WhatsAppMensajePublicidad);
                                            UsuarioEstado.IdWhatsappMensajePublicidad = WhatsAppMensajePublicidad.Id;

                                            UsuarioEstado.IdPersonal = IdPersonal;
                                            UsuarioEstado.IdPlantilla = IdPlantilla;

                                            ListaPreWhatsAppMensajePublicidad.Add(UsuarioEstado);

                                        }

                                    }
                                }


                            }


                        }
                        catch (Exception e)
                        {
                            //Enviar correo a sistemas
                            try
                            {
                                List<string> correos = new List<string>
                                {
                                    "ccrispin@bsginstitute.com",
                                    "jvillena@bsginstitute.com"
                                };

                                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                TMKMailDataDTO mailData = new TMKMailDataDTO
                                {
                                    Sender = "jvillena@bsginstitute.com",
                                    Recipient = string.Join(",", correos),
                                    Subject = "Error Pre-Procesamiento Listas - Subrupo",
                                    Message = "IdConjuntoLista: " + IdConjuntoLista.ToString() + " <br/>" + e.Message + (e.InnerException != null ? (" - " + e.InnerException.Message) : "") + " <br/> Mensaje toString <br/> " + e.ToString(),
                                    Cc = "",
                                    Bcc = "",
                                    AttachedFiles = null
                                };

                                Mailservice.SetData(mailData);
                                Mailservice.SendMessageTask();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    DTO.contacts.Clear();

                    //break;
                }

                return ListaPreWhatsAppMensajePublicidad;
            }
            else
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> ListaPreWhatsAppMensajePublicidadError = new List<PreWhatsAppResultadoConjuntoListaDTO>();
                return ListaPreWhatsAppMensajePublicidadError;
            }


        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion con los resultados donde ya se registra si los numeros osn validos, plantilla, varibles en la plantilla y su estado del numero, permitiendo guardar la lista pre procesada para el envio por el servicio
        /// </summary>
        /// <param name="RegistrosProcesados">Lista de registros procesado</param>
        /// <param name="IdPersonal">Identificador del personal</param>
        /// <param name="IdWhatsAppConfiguracionEnvio">Identificador de la configuracion</param>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista detalle</param>
        /// <returns>Retorna true si el proceso cuncluye de forma exitosa, caso contrario false</returns>
        public bool RegistraPreValidacion(List<PreWhatsAppResultadoConjuntoListaDTO> RegistrosProcesados, int IdPersonal, int IdWhatsAppConfiguracionEnvio, int IdConjuntoListaDetalle)
        {
            try
            {
                foreach (var item in RegistrosProcesados)
                {
                    try
                    {
                        WhatsAppConfiguracionPreEnvioBO WhatsAppConfiguracionPreEnvio = new WhatsAppConfiguracionPreEnvioBO();

                        WhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad = item.IdWhatsappMensajePublicidad;
                        WhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado = item.IdConjuntoListaResultado;
                        WhatsAppConfiguracionPreEnvio.IdAlumno = item.IdAlumno;
                        WhatsAppConfiguracionPreEnvio.Celular = item.Celular;
                        WhatsAppConfiguracionPreEnvio.IdPais = item.IdCodigoPais;
                        WhatsAppConfiguracionPreEnvio.NroEjecucion = item.NroEjecucion;
                        WhatsAppConfiguracionPreEnvio.Validado = item.Validado;
                        WhatsAppConfiguracionPreEnvio.Plantilla = item.Plantilla;
                        WhatsAppConfiguracionPreEnvio.IdPersonal = item.IdPersonal;
                        WhatsAppConfiguracionPreEnvio.IdPGeneral = item.IdPgeneral;
                        WhatsAppConfiguracionPreEnvio.IdPlantilla = item.IdPlantilla;
                        WhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion = item.IdWhatsAppEstadoValidacion;

                        if (item.objetoplantilla != null && item.objetoplantilla.Count > 0)
                        {
                            WhatsAppConfiguracionPreEnvio.objetoplantilla = JsonConvert.SerializeObject(item.objetoplantilla);
                        }
                        else
                        {
                            WhatsAppConfiguracionPreEnvio.objetoplantilla = "";
                        }

                        WhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle = IdConjuntoListaDetalle;
                        WhatsAppConfiguracionPreEnvio.Procesado = false;
                        WhatsAppConfiguracionPreEnvio.MensajeProceso = "No Porcesado";

                        WhatsAppConfiguracionPreEnvio.Estado = true;
                        WhatsAppConfiguracionPreEnvio.FechaCreacion = DateTime.Now;
                        WhatsAppConfiguracionPreEnvio.FechaModificacion = DateTime.Now;
                        WhatsAppConfiguracionPreEnvio.UsuarioCreacion = "PreDatosAutomatica";
                        WhatsAppConfiguracionPreEnvio.UsuarioModificacion = "PreDatosAutomatica";

                        _repWhatsAppConfiguracionPreEnvio.Insert(WhatsAppConfiguracionPreEnvio);
                    }
                    catch (Exception ex)
                    {

                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Descripción: Funcion con los resultados donde ya se registra si los numeros son validos, plantilla, varibles en la plantilla y su estado del numero, permitiendo guardar la lista pre procesada para el envio por el servicio
        /// </summary>
        /// <param name="RegistrosProcesados">Lista de registros procesados</param>
        /// <returns>Retorna true si el proceso cuncluye de forma exitosa, caso contrario false</returns>
        public bool RegistraPreValidacionCampaniaGeneral(List<WhatsAppResultadoCampaniaGeneralDTO> RegistrosProcesados, int IdPGeneral, int IdPlantilla)
        {
            try
            {
                List<WhatsAppConfiguracionPreEnvioBO> listaWhatsAppConfiguracionPreEnvio = new List<WhatsAppConfiguracionPreEnvioBO>();

                foreach (var item in RegistrosProcesados)
                {
                    try
                    {
                        WhatsAppConfiguracionPreEnvioBO whatsAppConfiguracionPreEnvio = new WhatsAppConfiguracionPreEnvioBO();

                        whatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad = item.IdWhatsappMensajePublicidad;
                        whatsAppConfiguracionPreEnvio.IdConjuntoListaResultado = 1;
                        whatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo = item.IdPrioridadMailChimpListaCorreo;
                        whatsAppConfiguracionPreEnvio.IdAlumno = item.IdAlumno;
                        whatsAppConfiguracionPreEnvio.Celular = item.Celular.TrimStart('0');
                        whatsAppConfiguracionPreEnvio.IdPais = item.IdCodigoPais;
                        whatsAppConfiguracionPreEnvio.NroEjecucion = 1;
                        whatsAppConfiguracionPreEnvio.Validado = item.Validado;
                        whatsAppConfiguracionPreEnvio.Plantilla = item.Plantilla;
                        whatsAppConfiguracionPreEnvio.IdPersonal = item.IdPersonal;
                        whatsAppConfiguracionPreEnvio.IdPGeneral = IdPGeneral;
                        whatsAppConfiguracionPreEnvio.IdPlantilla = IdPlantilla;
                        whatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion = item.Validado ? 1 : 4;

                        if (item.ListaObjetoPlantilla != null && item.ListaObjetoPlantilla.Any())
                            whatsAppConfiguracionPreEnvio.objetoplantilla = JsonConvert.SerializeObject(item.ListaObjetoPlantilla);
                        else
                            whatsAppConfiguracionPreEnvio.objetoplantilla = string.Empty;

                        whatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle = 1;
                        whatsAppConfiguracionPreEnvio.Procesado = false;
                        whatsAppConfiguracionPreEnvio.MensajeProceso = "No Procesado";
                        whatsAppConfiguracionPreEnvio.Estado = true;
                        whatsAppConfiguracionPreEnvio.FechaCreacion = DateTime.Now;
                        whatsAppConfiguracionPreEnvio.FechaModificacion = DateTime.Now;
                        whatsAppConfiguracionPreEnvio.UsuarioCreacion = "PreDatosAutomatica";
                        whatsAppConfiguracionPreEnvio.UsuarioModificacion = "PreDatosAutomatica";

                        listaWhatsAppConfiguracionPreEnvio.Add(whatsAppConfiguracionPreEnvio);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                bool resultado = _repWhatsAppConfiguracionPreEnvio.Insert(listaWhatsAppConfiguracionPreEnvio);

                return resultado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void ValidarNumeroConjuntoOperaciones(ref List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var Alumno in NumerosValidados)
            {
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + Alumno.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };


                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(Alumno.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(Alumno.IdPersonal ?? default(int), Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(Alumno.IdPersonal ?? default(int));

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);


                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                Alumno.Validado = false;
                            }
                            else
                            {
                                Alumno.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        _repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        Alumno.Validado = false;

                        whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        _repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + Alumno.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }

        private void RemplazarEtiquetas(ref List<WhatsAppResultadoConjuntoListaDTO> NumeroAlumno, int IdPersonal, int IdPlantilla, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario)
        {
            string plantilla = string.Empty;
            string valor = string.Empty;
            string Numero = "";
            //PlantillaPwBO plantillaPw = new PlantillaPwBO();

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                try
                {
                    AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

                    Numero = AlumnoEtiqueta.Celular;
                    if (Numero.StartsWith("51"))
                    {
                        Numero = Numero.Substring(2, 9);
                    }
                    else if (Numero.StartsWith("57"))
                    {
                        Numero = "00" + Numero;
                    }
                    else if (Numero.StartsWith("591"))
                    {
                        Numero = "00" + Numero;
                    }
                    else
                    {

                    }
                    var Alumno = _repAlumno.FirstBy(w => w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                    //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                    var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);



                    plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                    PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();
                    foreach (var item in ProgramaPrincipal)
                    {
                        rpta = _repCentroCosto.ObtenerRemplazoPlantilla(item.IdPgeneral);
                        if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                        {
                            fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(item.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);

                            if (fecha.Count > 0)
                            {
                                FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                if (FechaInicioPrograma == null)
                                {
                                    FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                }
                            }
                        }
                        //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                    }


                    foreach (string word in plantilla.Split('{'))
                    {
                        datoPlantillaWhatsApp plantillaEtiqueValor = new datoPlantillaWhatsApp();
                        if (word.Contains('}'))
                        {
                            string etiqueta = word.Split('}')[0];
                            //Separamos solo los Id´s

                            if (etiqueta.Contains("tPartner.nombre"))
                            {

                                valor = rpta.NombrePartner;
                            }
                            else if (etiqueta.Contains("tPEspecifico.nombre"))
                            {
                                valor = rpta.NombrePEspecifico;
                            }
                            else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                            {
                                valor = rpta.NombrePgeneral;
                            }

                            if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    CultureInfo ci = new CultureInfo("es-ES");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.Day.ToString();
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    //CultureInfo ci = new CultureInfo("es-Es");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            if (etiqueta.Contains("Template"))
                            {

                                valor = "";
                            }
                            else
                            {

                                if ((etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && IdPersonal > 0)
                                {
                                    switch (etiqueta)
                                    {
                                        case "tPersonal.PrimerNombreApellidoPaterno":
                                            valor = Asesor.PrimerNombreApellidoPaterno; break;
                                        case "tPersonal.email":
                                            valor = Asesor.Email; break;
                                        case "tPersonal.nombres":
                                            valor = Asesor.Nombres; break;
                                        case "tPersonal.apellidos":
                                            valor = Asesor.Apellidos; break;
                                        case "tPersonal.Telefono":
                                            {
                                                if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                {
                                                    valor = Asesor.MovilReferencia;
                                                }
                                                else
                                                {
                                                    if (Asesor.Central == "192.168.0.20")
                                                    {
                                                        //aqp
                                                        valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        if (Asesor.Central == "192.168.2.20")
                                                        {
                                                            //lima
                                                            valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else
                                                        {
                                                            valor = "(51) 54 258787";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apepaterno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoPaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apematerno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoMaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre1":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre1;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre2":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre2;
                                                }
                                            }
                                            break;
                                        default:
                                            valor = ""; break;
                                    }

                                }
                            }
                            if (valor != null)
                            {
                                valor = valor.Replace("#$%", "<br>");
                                plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.texto = valor;

                            }
                            else
                            {
                                plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.texto = "";
                            }
                            AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                        }
                    }
                    AlumnoEtiqueta.Plantilla = plantilla;
                    //return Ok(new { plantilla, objetoplantilla });
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error Proceso Plantillas";
                    mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion donde se realiza el remplazo de etiquetas del mensaje y sus respectivos parametos en la lista enviada como referencia
        /// </summary>
        /// <param name="NumeroAlumno">Lista de numeros de los alumnos</param>
        /// <param name="IdPersonal">Identificador del personal</param>
        /// <param name="IdPlantilla">Identificador de la plantilla</param>
        /// <param name="ProgramaPrincipal">Lista de datos del prograam principal</param>
        /// <param name="ProgramaSecundario">Lista de datos del prograam secundarios</param>
        private void RemplazarEtiquetaMasivo(ref List<PreWhatsAppResultadoConjuntoListaDTO> NumeroAlumno, int IdPersonal, int IdPlantilla, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario)
        {
            string Plantilla = string.Empty;
            string Valor = string.Empty;
            string Numero = "";

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                if (AlumnoEtiqueta.Validado)
                {
                    try
                    {
                        AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

                        Numero = AlumnoEtiqueta.Celular;
                        if (Numero.StartsWith("51"))
                        {
                            Numero = Numero.Substring(2, 9);
                        }
                        else if (Numero.StartsWith("57"))
                        {
                            Numero = "00" + Numero;
                        }
                        else if (Numero.StartsWith("591"))
                        {
                            Numero = "00" + Numero;
                        }
                        else
                        {

                        }
                        var Alumno = _repAlumno.FirstBy(w => w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                        //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                        var Asesor = _repPersonal.ObtenerDatoPersonal(IdPersonal);



                        Plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                        PlantillaCentroCostoDTO Rpta = new PlantillaCentroCostoDTO();
                        ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                        List<ModalidadProgramaDTO> Fecha = new List<ModalidadProgramaDTO>();
                        foreach (var item in ProgramaPrincipal)
                        {
                            Rpta = _repCentroCosto.ObtenerRemplazoPlantilla(item.IdPgeneral);
                            if (Plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || Plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || Plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                            {
                                Fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(item.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);

                                if (Fecha.Count > 0)
                                {
                                    FechaInicioPrograma = Fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    if (FechaInicioPrograma == null)
                                    {
                                        FechaInicioPrograma = Fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    }
                                }
                            }
                            //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                        }


                        foreach (string word in Plantilla.Split('{'))
                        {
                            datoPlantillaWhatsApp PlantillaEtiqueValor = new datoPlantillaWhatsApp();
                            if (word.Contains('}'))
                            {
                                string Etiqueta = word.Split('}')[0];
                                //Separamos solo los Id´s

                                if (Etiqueta.Contains("tPartner.nombre"))
                                {

                                    Valor = Rpta.NombrePartner;
                                }
                                else if (Etiqueta.Contains("tPEspecifico.nombre"))
                                {
                                    Valor = Rpta.NombrePEspecifico;
                                }
                                else if (Etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                {
                                    Valor = Rpta.NombrePgeneral;
                                }

                                if (Etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        CultureInfo ci = new CultureInfo("es-ES");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                        Valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Valor);
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                else if (Etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = FechaInicioetiqueta.Day.ToString();
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                else if (Etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                {
                                    if (Fecha.Count != 0)
                                    {
                                        //CultureInfo ci = new CultureInfo("es-Es");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        Valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                        Valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Valor);
                                    }
                                    else
                                    {
                                        Valor = "";
                                    }
                                }
                                if (Etiqueta.Contains("Template"))
                                {
                                    Valor = "";
                                }
                                else
                                {

                                    if ((Etiqueta == "tPersonal.email" || Etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || Etiqueta == "tPersonal.nombres" || Etiqueta == "tPersonal.apellidos" || Etiqueta == "tPersonal.UrlFirmaCorreos" || Etiqueta == "tPersonal.Telefono" || Etiqueta == "tAlumnos.apepaterno" || Etiqueta == "tAlumnos.apematerno" || Etiqueta == "tAlumnos.nombre1" || Etiqueta == "tAlumnos.nombre2") && IdPersonal > 0)
                                    {
                                        switch (Etiqueta)
                                        {
                                            case "tPersonal.PrimerNombreApellidoPaterno":
                                                Valor = Asesor.PrimerNombreApellidoPaterno; break;
                                            case "tPersonal.email":
                                                Valor = Asesor.Email; break;
                                            case "tPersonal.nombres":
                                                Valor = Asesor.Nombres; break;
                                            case "tPersonal.apellidos":
                                                Valor = Asesor.Apellidos; break;
                                            case "tPersonal.Telefono":
                                                {
                                                    if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                    {
                                                        Valor = Asesor.MovilReferencia;
                                                    }
                                                    else
                                                    {
                                                        if (Asesor.Central == "192.168.0.20")
                                                        {
                                                            //aqp
                                                            Valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else
                                                        {
                                                            if (Asesor.Central == "192.168.2.20")
                                                            {
                                                                //lima
                                                                Valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else
                                                            {
                                                                Valor = "(51) 54 258787";
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apepaterno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.ApellidoPaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apematerno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.ApellidoMaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre1":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.Nombre1;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre2":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        Valor = Alumno.Nombre2;
                                                    }
                                                }
                                                break;
                                            default:
                                                Valor = ""; break;
                                        }

                                    }
                                }
                                if (Valor != null)
                                {
                                    Valor = Valor.Replace("#$%", "<br>");
                                    Plantilla = Plantilla.Replace("{" + Etiqueta + "}", Valor);

                                    PlantillaEtiqueValor.codigo = "{ " + Etiqueta + "}";
                                    PlantillaEtiqueValor.texto = Valor;

                                }
                                else
                                {
                                    Plantilla = Plantilla.Replace("{" + Etiqueta + "}", "");

                                    PlantillaEtiqueValor.codigo = "{ " + Etiqueta + "}";
                                    PlantillaEtiqueValor.texto = "";
                                }
                                AlumnoEtiqueta.objetoplantilla.Add(PlantillaEtiqueValor);
                            }
                        }
                        AlumnoEtiqueta.Plantilla = Plantilla;
                        //return Ok(new { plantilla, objetoplantilla });
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("fvaldez@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO();
                        mailData.Sender = "fvaldez@bsginstitute.com";
                        mailData.Recipient = string.Join(",", correos);
                        mailData.Subject = "Error Proceso Plantillas";
                        mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                        mailData.Cc = "";
                        mailData.Bcc = "";
                        mailData.AttachedFiles = null;

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    AlumnoEtiqueta.Plantilla = Plantilla;
                }
            }
        }

        /// <summary>
        ///Funcion donde se realiza el reemplazo de etiquetas del mensaje y sus respectivos parametos en la lista enviada como referencia
        /// </summary>
        /// <param name="NumeroAlumno">Lista de numeros de los alumnos</param>
        /// <param name="IdPlantilla">Identificador de la plantilla</param>
        /// <param name="IdPGeneral">Id del programa general</param>
        /// <returns>Objeto de clase WhatsAppResultadoCampaniaGeneralDTO</returns>
        public List<WhatsAppResultadoCampaniaGeneralDTO> ReemplazarEtiquetaCampaniaGeneral(List<WhatsAppResultadoCampaniaGeneralDTO> NumeroAlumno, int IdPlantilla, int IdPGeneral, int IdCampaniaGeneralDetalle)
        {
            string valor = string.Empty;
            string plantillaBaseGeneral = string.Empty;

            try
            {
                var rpta = _repCentroCosto.ObtenerRemplazoPlantilla(IdPGeneral);
                plantillaBaseGeneral = _repPlantillaClaveValor.GetBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", x => new { x.Valor }).FirstOrDefault().Valor;
                var listaPersonal = _repPersonal.GetBy(x => NumeroAlumno.Select(s => s.IdPersonal).Contains(x.Id)).ToList();
                var alumnos = _repCampaniaGeneralDetalle.ObtenerAlumnosPorCampaniaGeneralDetalle(IdCampaniaGeneralDetalle);

                foreach (var alumnoEtiqueta in NumeroAlumno)
                {
                    string plantillaBase = plantillaBaseGeneral;

                    if (alumnoEtiqueta.Validado)
                    {
                        try
                        {
                            var personal = listaPersonal.FirstOrDefault(x => x.Id == alumnoEtiqueta.IdPersonal);
                            alumnoEtiqueta.ListaObjetoPlantilla = new List<datoPlantillaWhatsApp>();

                            if (alumnoEtiqueta.Celular.StartsWith("51"))
                                alumnoEtiqueta.Celular = alumnoEtiqueta.Celular.Substring(2, 9);
                            else if (alumnoEtiqueta.Celular.StartsWith("57"))
                                alumnoEtiqueta.Celular = "00" + alumnoEtiqueta.Celular;
                            else if (alumnoEtiqueta.Celular.StartsWith("591"))
                                alumnoEtiqueta.Celular = "00" + alumnoEtiqueta.Celular;

                            var alumno = alumnos.FirstOrDefault(x => x.Id == alumnoEtiqueta.IdAlumno);

                            if (alumno != null)
                            {
                                var fechaInicioPrograma = new ModalidadProgramaDTO();
                                var fecha = new List<ModalidadProgramaDTO>();

                                if (plantillaBase.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantillaBase.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantillaBase.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                                {
                                    fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(IdPGeneral, alumnoEtiqueta.IdCodigoPais);

                                    if (fecha.Any())
                                    {
                                        fechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();

                                        if (fechaInicioPrograma == null)
                                            fechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    }
                                }

                                foreach (string word in plantillaBase.Split('{'))
                                {
                                    datoPlantillaWhatsApp PlantillaEtiqueValor = new datoPlantillaWhatsApp();
                                    if (word.Contains('}'))
                                    {
                                        string Etiqueta = word.Split('}')[0];
                                        //Separamos solo los Id's
                                        if (Etiqueta.Contains("tPartner.nombre"))
                                            valor = rpta.NombrePartner;
                                        else if (Etiqueta.Contains("tPEspecifico.nombre"))
                                            valor = rpta.NombrePEspecifico;
                                        else if (Etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                            valor = rpta.NombrePgeneral;

                                        if (Etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                CultureInfo ci = new CultureInfo("es-ES");
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                                valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        else if (Etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = FechaInicioetiqueta.Day.ToString();
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        else if (Etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                        {
                                            if (fecha.Any())
                                            {
                                                DateTime FechaInicioetiqueta = new DateTime();
                                                FechaInicioetiqueta = fechaInicioPrograma.FechaReal.Value;

                                                valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                                valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                            }
                                            else
                                                valor = string.Empty;
                                        }
                                        if (Etiqueta.Contains("Template"))
                                            valor = string.Empty;
                                        else
                                        {
                                            if ((Etiqueta == "tPersonal.email" || Etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || Etiqueta == "tPersonal.nombres" || Etiqueta == "tPersonal.apellidos" || Etiqueta == "tPersonal.UrlFirmaCorreos" || Etiqueta == "tPersonal.Telefono" || Etiqueta == "tAlumnos.apepaterno" || Etiqueta == "tAlumnos.apematerno" || Etiqueta == "tAlumnos.nombre1" || Etiqueta == "tAlumnos.nombre2") && alumnoEtiqueta.IdPersonal > 0)
                                            {
                                                switch (Etiqueta)
                                                {
                                                    case "tPersonal.PrimerNombreApellidoPaterno":
                                                        valor = personal.PrimerNombreApellidoPaterno; break;
                                                    case "tPersonal.email":
                                                        valor = personal.Email; break;
                                                    case "tPersonal.nombres":
                                                        valor = personal.Nombres; break;
                                                    case "tPersonal.apellidos":
                                                        valor = personal.Apellidos; break;
                                                    case "tPersonal.Telefono":
                                                        {
                                                            if (!string.IsNullOrEmpty(personal.MovilReferencia))
                                                                valor = personal.MovilReferencia;
                                                            else
                                                            {
                                                                if (personal.Central == "192.168.0.20") // Arequipa
                                                                    valor = "(51) 54 258787 - Anexo " + personal.Anexo3Cx;
                                                                else
                                                                {
                                                                    if (personal.Central == "192.168.2.20") // Lima
                                                                        valor = "(51) 1 207 2770 - Anexo " + personal.Anexo3Cx;
                                                                    else
                                                                        valor = "(51) 54 258787";
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case "tAlumnos.apepaterno":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.ApellidoPaterno;
                                                        }
                                                        break;
                                                    case "tAlumnos.apematerno":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.ApellidoMaterno;
                                                        }
                                                        break;
                                                    case "tAlumnos.nombre1":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.Nombre1;
                                                        }
                                                        break;
                                                    case "tAlumnos.nombre2":
                                                        {
                                                            if (alumno != null)
                                                                valor = alumno.Nombre2;
                                                        }
                                                        break;
                                                    default:
                                                        valor = string.Empty; break;
                                                }

                                            }
                                        }
                                        if (valor != null)
                                        {
                                            valor = valor.Replace("#$%", "<br>");
                                            plantillaBase = plantillaBase.Replace("{" + Etiqueta + "}", valor);

                                            PlantillaEtiqueValor.codigo = "{ " + Etiqueta + "}";
                                            PlantillaEtiqueValor.texto = valor;

                                        }
                                        else
                                        {
                                            plantillaBase = plantillaBase.Replace("{" + Etiqueta + "}", "");

                                            PlantillaEtiqueValor.codigo = "{ " + Etiqueta + "}";
                                            PlantillaEtiqueValor.texto = "";
                                        }
                                        alumnoEtiqueta.ListaObjetoPlantilla.Add(PlantillaEtiqueValor);
                                    }
                                }
                                alumnoEtiqueta.Plantilla = plantillaBase;
                            }
                        }
                        catch (Exception ex)
                        {
                            List<string> correos = new List<string>();
                            correos.Add("gmiranda@bsginstitute.com");

                            TMK_MailServiceImpl mailService = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "fvaldez@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error Proceso Plantillas Campania General";
                            mailData.Message = "Alumno: " + alumnoEtiqueta.IdAlumno.ToString() + "<br/>" + ex.Message + " <br/>Mensaje:<br/> " + ex.ToString();
                            mailData.Cc = string.Empty;
                            mailData.Bcc = string.Empty;
                            mailData.AttachedFiles = null;

                            mailService.SetData(mailData);
                            mailService.SendMessageTask();
                        }
                    }
                    else
                        alumnoEtiqueta.Plantilla = plantillaBase;
                }

                return NumeroAlumno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void RemplazarEtiquetasOperaciones(ref List<WhatsAppResultadoConjuntoListaDTO> NumeroAlumno)
        {
            string plantilla = string.Empty;
            string valor = string.Empty;
            string Numero = "";
            //PlantillaPwBO plantillaPw = new PlantillaPwBO();

            foreach (var AlumnoEtiqueta in NumeroAlumno)
            {
                try
                {
                    AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

                    Numero = AlumnoEtiqueta.Celular;
                    if (Numero.StartsWith("51"))
                    {
                        Numero = Numero.Substring(2, 9);
                    }
                    else if (Numero.StartsWith("57"))
                    {
                        Numero = "00" + Numero;
                    }
                    else if (Numero.StartsWith("591"))
                    {
                        Numero = "00" + Numero;
                    }
                    else
                    {

                    }
                    var Alumno = _repAlumno.FirstBy(w => w.Celular.Contains(Numero) && w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });
                    //var Asesor = _repPersonal.FirstBy(w => w.Id == IdPersonal, y => new { y.Nombres, y.Apellidos, y.Anexo3Cx, y.Central, y.MovilReferencia });
                    var Asesor = _repPersonal.ObtenerDatoPersonal(AlumnoEtiqueta.IdPersonal ?? default(int));



                    plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == AlumnoEtiqueta.IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                    PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();

                    rpta = _repCentroCosto.ObtenerRemplazoPlantilla(AlumnoEtiqueta.IdPgeneral ?? default(int));
                    if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                    {
                        fecha = _repPgeneral.ObtenerFechaInicioProgramaGeneral(AlumnoEtiqueta.IdPgeneral ?? default(int), AlumnoEtiqueta.IdCodigoPais);

                        if (fecha.Count > 0)
                        {
                            FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL")).OrderBy(w => w.FechaReal).FirstOrDefault();
                            if (FechaInicioPrograma == null)
                            {
                                FechaInicioPrograma = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                            }
                        }
                    }
                    //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);



                    foreach (string word in plantilla.Split('{'))
                    {
                        datoPlantillaWhatsApp plantillaEtiqueValor = new datoPlantillaWhatsApp();
                        if (word.Contains('}'))
                        {
                            string etiqueta = word.Split('}')[0];
                            //Separamos solo los Id´s

                            if (etiqueta.Contains("tPartner.nombre"))
                            {

                                valor = rpta.NombrePartner;
                            }
                            else if (etiqueta.Contains("tPEspecifico.nombre"))
                            {
                                valor = rpta.NombrePEspecifico;
                            }
                            else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                            {
                                valor = rpta.NombrePgeneral;
                            }

                            if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    CultureInfo ci = new CultureInfo("es-ES");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.Day.ToString();
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                            {
                                if (fecha.Count != 0)
                                {
                                    //CultureInfo ci = new CultureInfo("es-Es");
                                    DateTime FechaInicioetiqueta = new DateTime();
                                    FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                    valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                    valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                }
                                else
                                {
                                    valor = "";
                                }
                            }
                            if (etiqueta.Contains("Template"))
                            {

                                valor = "";
                            }
                            else
                            {

                                if ((etiqueta == "tPersonal.Nombre1" || etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && AlumnoEtiqueta.IdPersonal > 0)
                                {
                                    switch (etiqueta)
                                    {
                                        case "tPersonal.PrimerNombreApellidoPaterno":
                                            valor = Asesor.PrimerNombreApellidoPaterno; break;
                                        case "tPersonal.email":
                                            valor = Asesor.Email; break;
                                        case "tPersonal.Nombre1":
                                            valor = Asesor.Nombre1; break;
                                        case "tPersonal.nombres":
                                            valor = Asesor.Nombres; break;
                                        case "tPersonal.apellidos":
                                            valor = Asesor.Apellidos; break;
                                        case "tPersonal.Telefono":
                                            {
                                                if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                {
                                                    valor = Asesor.MovilReferencia;
                                                }
                                                else
                                                {
                                                    if (Asesor.Central == "192.168.0.20")
                                                    {
                                                        //aqp
                                                        valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
                                                    }
                                                    else
                                                    {
                                                        if (Asesor.Central == "192.168.2.20")
                                                        {
                                                            //lima
                                                            valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else
                                                        {
                                                            valor = "(51) 54 258787";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apepaterno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoPaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.apematerno":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.ApellidoMaterno;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre1":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre1;
                                                }
                                            }
                                            break;
                                        case "tAlumnos.nombre2":
                                            {
                                                if (Alumno != null)
                                                {
                                                    valor = Alumno.Nombre2;
                                                }
                                            }
                                            break;
                                        default:
                                            valor = ""; break;
                                    }

                                }
                            }
                            if (valor != null)
                            {
                                valor = valor.Replace("#$%", "<br>");
                                plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.texto = valor;

                            }
                            else
                            {
                                plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                plantillaEtiqueValor.texto = "";
                            }
                            AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                        }
                    }
                    AlumnoEtiqueta.Plantilla = plantilla;
                    //return Ok(new { plantilla, objetoplantilla });
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("fvaldez@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Error Proceso Plantillas";
                    mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }

        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdPersonal, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _repPlantilla.ObtenerPlantillaPorId(IdPlantilla);
            foreach (var AlumnoMensaje in MensajeAlumno)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = AlumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = AlumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = AlumnoMensaje.objetoplantilla
                };

                try
                {
                    var EnvioDuplicado = _repWhatsAppConfiguracionLogEjecucion.VerificadEnvioDuplicado(AlumnoMensaje.Celular);

                    if (EnvioDuplicado == false)
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                        WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                        var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                        var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, AlumnoMensaje.IdCodigoPais);

                        string urlToPost = _credencialesHost.UrlWhatsApp;

                        string resultado = string.Empty, _waType = string.Empty;

                        //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                        if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                            var client = new RestClient(urlToPostUsuario);
                            var request = new RestSharp.RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            request.AddHeader("Content-Length", "");
                            request.AddHeader("Accept-Encoding", "gzip, deflate");
                            request.AddHeader("Host", _credencialesHost.IpHost);
                            request.AddHeader("Cache-Control", "no-cache");
                            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                            request.AddHeader("Content-Type", "application/json");
                            //IRestResponse response = client.Execute(request);

                            //if (response.StatusCode == HttpStatusCode.OK)
                            //{
                            //    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            //    foreach (var item in datos.users)
                            //    {
                            //        TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                            //        modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                            //        modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                            //        modelCredencial.UserAuthToken = item.token;
                            //        modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                            //        modelCredencial.EsMigracion = true;
                            //        modelCredencial.Estado = true;
                            //        modelCredencial.FechaCreacion = DateTime.Now;
                            //        modelCredencial.FechaModificacion = DateTime.Now;
                            //        modelCredencial.UsuarioCreacion = "whatsapp";
                            //        modelCredencial.UsuarioModificacion = "whatsapp";

                            //        var rpta = _repTokenUsuario.Insert(modelCredencial);

                            //        _tokenComunicacion = item.token;
                            //    }

                            //    banderaLogin = true;

                            //}
                            //else
                            //{
                            //    banderaLogin = false;
                            //}
                        }
                        else
                        {
                            _tokenComunicacion = tokenValida.UserAuthToken;
                            banderaLogin = true;
                        }

                        if (banderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                    _waType = "text";

                                    MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                    _mensajeTexto.to = DTO.WaTo;
                                    _mensajeTexto.type = DTO.WaType;
                                    _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                    _mensajeTexto.text = new text();

                                    _mensajeTexto.text.body = DTO.WaBody;

                                    using (WebClient client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeTexto);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":

                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    _mensajePlantilla.to = DTO.WaTo;
                                    _mensajePlantilla.type = "template";
                                    _mensajePlantilla.template = new template();

                                    _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    _mensajePlantilla.template.name = DTO.WaBody;

                                    _mensajePlantilla.template.language = new language();
                                    _mensajePlantilla.template.language.policy = "deterministic";
                                    _mensajePlantilla.template.language.code = "es";

                                    _mensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";


                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    _mensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = Client.UploadString(urlToPost, MyParameters);
                                    }



                                    // original


                                    //urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    //_waType = "hsm";

                                    //MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                    //_mensajePlantilla.to = DTO.WaTo;
                                    //_mensajePlantilla.type = DTO.WaType;
                                    //_mensajePlantilla.hsm = new hsm();

                                    //_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    //_mensajePlantilla.hsm.element_name = DTO.WaBody;

                                    //_mensajePlantilla.hsm.language = new language();
                                    //_mensajePlantilla.hsm.language.policy = "deterministic";
                                    //_mensajePlantilla.hsm.language.code = "es";

                                    //if (DTO.datosPlantillaWhatsApp != null)
                                    //{
                                    //    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                    //    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    //    {
                                    //        localizable_params _dato = new localizable_params();
                                    //        _dato.@default = listaDatos.texto;

                                    //        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                    //    }
                                    //}

                                    //using (WebClient client = new WebClient())
                                    //{
                                    //    client.Encoding = Encoding.UTF8;
                                    //    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    //    var serializer = new JavaScriptSerializer();

                                    //    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    //    string myParameters = serializedResult;
                                    //    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    //    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    //    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    //    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    //    resultado = client.UploadString(urlToPost, myParameters);
                                    //}

                                    break;
                                case "image":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "image";

                                    MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                    _mensajeImagen.to = DTO.WaTo;
                                    _mensajeImagen.type = DTO.WaType;
                                    _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                    _mensajeImagen.image = new image();

                                    _mensajeImagen.image.caption = DTO.WaCaption;
                                    _mensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeImagen);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                    _waType = "document";

                                    MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                    _mensajeDocumento.to = DTO.WaTo;
                                    _mensajeDocumento.type = DTO.WaType;
                                    _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    _mensajeDocumento.document = new document();

                                    _mensajeDocumento.document.caption = DTO.WaCaption;
                                    _mensajeDocumento.document.link = DTO.WaLink;
                                    _mensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient client = new WebClient())
                                    {
                                        client.Encoding = Encoding.UTF8;
                                        var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                        var serializer = new JavaScriptSerializer();

                                        var serializedResult = serializer.Serialize(_mensajeDocumento);
                                        string myParameters = serializedResult;
                                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        resultado = client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                            WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                            mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                            mensajeEnviado.EnviadoCorrectamente = true;
                            mensajeEnviado.MensajeError = "";
                            mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                            mensajeEnviado.Mensaje = DTO.WaCaption;
                            mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                            mensajeEnviado.Estado = true;
                            mensajeEnviado.FechaCreacion = DateTime.Now;
                            mensajeEnviado.FechaModificacion = DateTime.Now;
                            mensajeEnviado.UsuarioCreacion = "Envio";
                            mensajeEnviado.UsuarioModificacion = "Envio";

                            _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);

                            //return Ok(mensajeEnviado.WaId);
                        }
                        else
                        {
                            WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                            mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                            mensajeEnviado.EnviadoCorrectamente = false;
                            mensajeEnviado.MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                            mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                            mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                            mensajeEnviado.Estado = true;
                            mensajeEnviado.FechaCreacion = DateTime.Now;
                            mensajeEnviado.FechaModificacion = DateTime.Now;
                            mensajeEnviado.UsuarioCreacion = "Envio";
                            mensajeEnviado.UsuarioModificacion = "Envio";
                            _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                            //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                        }
                    }
                    else
                    {
                        WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Envio Duplicado";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Envio";
                        mensajeEnviado.UsuarioModificacion = "Envio";
                        _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                    }
                }
                catch (Exception ex)
                {
                    WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "Envio";
                    mensajeEnviado.UsuarioModificacion = "Envio";
                    _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                }

                System.Threading.Thread.Sleep(5000);


            }

        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: La funcion permite ralizar el envio de mensajes de wahtsaap con la funcion editada en el tema de plantilla (template) en donde se valida el estado y registra en un objetos el envio que sera retornado
        /// </summary>
        /// <param name="MensajeAlumno">Lista de mensajes con los numeros a envaiar por whatsapp</param>
        /// <param name="IdPersonal">Identificador del personal</param>
        /// <param name="IdPlantilla">Identificador de la plantilla</param>
        /// <param name="IdWhatsAppConfiguracionLogEjecucion">Identificador de la configuracion de whatsapp</param>
        /// <returns>Retorna la lista de las condiguraciones del envio realizado segun el estado de cada mensaje procesado por whatsapp</returns>
        private List<WhatsAppConfiguracionEnvioDetalleBO> EnvioAutomaticoPlantillaMasivo(ref List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdPersonal, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion, bool aplicarLimiteEspera = false)
        {
            bool BanderaLogin = false;
            string TokenComunicacion = string.Empty;
            var Plantilla = _repPlantilla.ObtenerPlantillaPorId(IdPlantilla);

            int cantidadGrupoActual = 0;
            int cantidadErrorEnvioActual = 0;
            const int LIMITE_CANTIDAD_ESPERA = 5;
            const int LIMITE_CANTIDAD_ERROR_ENVIO = 100;

            var RegistroEstadoEnvio = new List<WhatsAppConfiguracionEnvioDetalleBO>();

            foreach (var alumnoMensaje in MensajeAlumno)
            {
                cantidadGrupoActual++;
                var DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = alumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = alumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = alumnoMensaje.objetoplantilla
                };

                try
                {
                    var envioDuplicado = _repWhatsAppConfiguracionLogEjecucion.VerificadEnvioDuplicado(alumnoMensaje.Celular);

                    if (!envioDuplicado)
                    {
                        ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        /*Repositorios*/
                        var _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                        var _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                        var CredencialesHost = _repCredenciales.ObtenerCredencialHost(alumnoMensaje.IdCodigoPais);
                        var TokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, alumnoMensaje.IdCodigoPais);

                        string urlToPost = CredencialesHost.UrlWhatsApp;

                        string Resultado = string.Empty, WaType = string.Empty;

                        if (TokenValida == null || DateTime.Now >= TokenValida.ExpiresAfter)
                        {
                            string urlToPostUsuario = $"{CredencialesHost.UrlWhatsApp}/v1/users/login";

                            var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                            var restClient = new RestClient(urlToPostUsuario);
                            var restRequest = new RestSharp.RestRequest(Method.POST);

                            restRequest.AddHeader("cache-control", "no-cache");
                            restRequest.AddHeader("Content-Length", "");
                            restRequest.AddHeader("Accept-Encoding", "gzip, deflate");
                            restRequest.AddHeader("Host", CredencialesHost.IpHost);
                            restRequest.AddHeader("Cache-Control", "no-cache");
                            restRequest.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                            restRequest.AddHeader("Content-Type", "application/json");

                            IRestResponse response = restClient.Execute(restRequest);

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                                foreach (var item in datos.users)
                                {
                                    var modelCredencial = new TWhatsAppUsuarioCredencial
                                    {
                                        IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                        IdWhatsAppConfiguracion = CredencialesHost.Id,
                                        UserAuthToken = item.token,
                                        ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                        EsMigracion = true,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = "whatsapp",
                                        UsuarioModificacion = "whatsapp"
                                    };

                                    bool rpta = _repTokenUsuario.Insert(modelCredencial);

                                    TokenComunicacion = item.token;
                                }

                                BanderaLogin = true;
                            }
                            else
                            {
                                BanderaLogin = false;
                            }
                        }
                        else
                        {
                            TokenComunicacion = TokenValida.UserAuthToken;
                            BanderaLogin = true;
                        }

                        if (BanderaLogin)
                        {
                            switch (DTO.WaType.ToLower())
                            {
                                case "text":
                                    urlToPost = $"{CredencialesHost.UrlWhatsApp}/v1/messages";
                                    WaType = "text";

                                    var MensajeTexto = new MensajeTextoEnvio
                                    {
                                        to = DTO.WaTo,
                                        type = DTO.WaType,
                                        recipient_type = DTO.WaRecipientType,
                                        text = new text
                                        {
                                            body = DTO.WaBody
                                        }
                                    };

                                    using (WebClient Client = new WebClient())
                                    {
                                        //client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeTexto);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeTexto);
                                        string myParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                        Resultado = Client.UploadString(urlToPost, myParameters);
                                    }

                                    break;
                                case "hsm":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "template";

                                    MensajePlantillaWhatsAppEnvioTemplate MensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                    MensajePlantilla.to = DTO.WaTo;
                                    MensajePlantilla.type = "template";
                                    MensajePlantilla.template = new template();

                                    MensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                    MensajePlantilla.template.name = DTO.WaBody;

                                    MensajePlantilla.template.language = new language();
                                    MensajePlantilla.template.language.policy = "deterministic";
                                    MensajePlantilla.template.language.code = "es";

                                    MensajePlantilla.template.components = new List<components>();
                                    components Componente = new components();
                                    Componente.type = "body";


                                    if (DTO.datosPlantillaWhatsApp != null)
                                    {
                                        Componente.parameters = new List<parameters>();
                                        foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                        {
                                            parameters Dato = new parameters();
                                            Dato.type = "text";
                                            Dato.text = listaDatos.texto;

                                            Componente.parameters.Add(Dato);
                                        }
                                    }

                                    MensajePlantilla.template.components.Add(Componente);

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajePlantilla);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajePlantilla);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "image":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "image";

                                    MensajeImagenEnvio MensajeImagen = new MensajeImagenEnvio();
                                    MensajeImagen.to = DTO.WaTo;
                                    MensajeImagen.type = DTO.WaType;
                                    MensajeImagen.recipient_type = DTO.WaRecipientType;

                                    MensajeImagen.image = new image();

                                    MensajeImagen.image.caption = DTO.WaCaption;
                                    MensajeImagen.image.link = DTO.WaLink;

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeImagen);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeImagen);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                                case "document":
                                    urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                                    WaType = "document";

                                    MensajeDocumentoEnvio MensajeDocumento = new MensajeDocumentoEnvio();
                                    MensajeDocumento.to = DTO.WaTo;
                                    MensajeDocumento.type = DTO.WaType;
                                    MensajeDocumento.recipient_type = DTO.WaRecipientType;

                                    MensajeDocumento.document = new document();

                                    MensajeDocumento.document.caption = DTO.WaCaption;
                                    MensajeDocumento.document.link = DTO.WaLink;
                                    MensajeDocumento.document.filename = DTO.WaFileName;

                                    using (WebClient Client = new WebClient())
                                    {
                                        Client.Encoding = Encoding.UTF8;
                                        var MensajeJSON = JsonConvert.SerializeObject(MensajeDocumento);
                                        var Serializer = new JavaScriptSerializer();

                                        var SerializedResult = Serializer.Serialize(MensajeDocumento);
                                        string MyParameters = SerializedResult;
                                        Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                                        Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                        Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                                        Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                        Resultado = Client.UploadString(urlToPost, MyParameters);
                                    }

                                    break;
                            }

                            var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(Resultado);

                            var MensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO
                            {
                                IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                                EnviadoCorrectamente = true,
                                MensajeError = "",
                                IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                                IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                                Mensaje = DTO.WaCaption,
                                WhatsAppId = datoRespuesta.messages[0].id,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "Envio-Pre",
                                UsuarioModificacion = "Envio-Pre"
                            };

                            alumnoMensaje.Validado = true;

                            try
                            {
                                _repWhatsAppConfiguracionEnvioDetalle.Insert(MensajeEnviado);

                                var ActualizarRegistroPre = _repWhatsAppConfiguracionPreEnvio.FirstById(alumnoMensaje.IdPre);
                                if (ActualizarRegistroPre != null)
                                {
                                    ActualizarRegistroPre.FechaModificacion = DateTime.Now;
                                    ActualizarRegistroPre.Procesado = true;
                                    ActualizarRegistroPre.MensajeProceso = "Procesado";
                                    _repWhatsAppConfiguracionPreEnvio.Update(ActualizarRegistroPre);
                                }
                            }
                            catch (Exception ex1)
                            {
                                try
                                {
                                    var mensajeCompleto = $"{ex1.Message}-{(ex1.InnerException != null ? ex1.InnerException.Message : "No contiene InnerException")}";

                                    _repLog.Insert(new TLog
                                    {
                                        Ip = "-",
                                        Usuario = "-",
                                        Maquina = "-",
                                        Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                        Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                        Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                        Excepcion = ex1.ToString().Length > 2500 ? ex1.ToString().Substring(0, 2500) : ex1.ToString(),
                                        Tipo = "SEND",
                                        IdPadre = 0,
                                        UsuarioCreacion = "gmiranda",
                                        UsuarioModificacion = "gmiranda",
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    });
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        else
                        {
                            var MensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO
                            {
                                IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                                EnviadoCorrectamente = false,
                                MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.",
                                IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                                IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                                ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "Envio-Pre",
                                UsuarioModificacion = "Envio-Pre"
                            };

                            alumnoMensaje.Validado = false;

                            try
                            {
                                var _actualizarRegistroPre = _repWhatsAppConfiguracionPreEnvio.FirstById(alumnoMensaje.IdPre);
                                if (_actualizarRegistroPre != null)
                                {
                                    _actualizarRegistroPre.FechaModificacion = DateTime.Now;
                                    _actualizarRegistroPre.Procesado = false;
                                    _actualizarRegistroPre.MensajeProceso = "Mensaje no enviado / Error en credenciales";
                                    _repWhatsAppConfiguracionPreEnvio.Update(_actualizarRegistroPre);
                                }
                                _repWhatsAppConfiguracionEnvioDetalle.Insert(MensajeEnviado);
                            }
                            catch (Exception ex2)
                            {
                                try
                                {
                                    var mensajeCompleto = $"{ex2.Message}-{(ex2.InnerException != null ? ex2.InnerException.Message : "No contiene InnerException")}";

                                    _repLog.Insert(new TLog
                                    {
                                        Ip = "-",
                                        Usuario = "-",
                                        Maquina = "-",
                                        Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                        Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                        Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                        Excepcion = ex2.ToString().Length > 2500 ? ex2.ToString().Substring(0, 2500) : ex2.ToString(),
                                        Tipo = "SEND",
                                        IdPadre = 0,
                                        UsuarioCreacion = "gmiranda",
                                        UsuarioModificacion = "gmiranda",
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    });
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        var MensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO
                        {
                            IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                            EnviadoCorrectamente = false,
                            MensajeError = "Envio Duplicado",
                            IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                            IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                            ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "Envio-Pre",
                            UsuarioModificacion = "Envio-Pre"
                        };

                        alumnoMensaje.Validado = false;

                        try
                        {
                            var _actualizarRegistroPre = _repWhatsAppConfiguracionPreEnvio.FirstById(alumnoMensaje.IdPre);
                            if (_actualizarRegistroPre != null)
                            {
                                _actualizarRegistroPre.FechaModificacion = DateTime.Now;
                                _actualizarRegistroPre.Procesado = false;
                                _actualizarRegistroPre.MensajeProceso = MensajeEnviado.MensajeError;
                                _repWhatsAppConfiguracionPreEnvio.Update(_actualizarRegistroPre);
                            }
                            _repWhatsAppConfiguracionEnvioDetalle.Insert(MensajeEnviado);
                        }
                        catch (Exception ex3)
                        {
                            try
                            {
                                var mensajeCompleto = $"{ex3.Message}-{(ex3.InnerException != null ? ex3.InnerException.Message : "No contiene InnerException")}";

                                _repLog.Insert(new TLog
                                {
                                    Ip = "-",
                                    Usuario = "-",
                                    Maquina = "-",
                                    Ruta = "InsertarWhatsappEnvioDetalle,ActualizarWhatsapPreEnvio3",
                                    Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                    Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                    Excepcion = ex3.ToString().Length > 2500 ? ex3.ToString().Substring(0, 2500) : ex3.ToString(),
                                    Tipo = "SEND",
                                    IdPadre = 0,
                                    UsuarioCreacion = "gmiranda",
                                    UsuarioModificacion = "gmiranda",
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                });
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    cantidadErrorEnvioActual++;

                    try
                    {
                        var mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO
                        {
                            IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion,
                            EnviadoCorrectamente = false,
                            MensajeError = e.ToString(),
                            IdConjuntoListaResultado = alumnoMensaje.IdConjuntoListaResultado,
                            IdPrioridadMailChimpListaCorreo = alumnoMensaje.IdPrioridadMailChimpListaCorreo,
                            ConjuntoListaNroEjecucion = alumnoMensaje.NroEjecucion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "Envio-Pre",
                            UsuarioModificacion = "Envio-Pre"
                        };

                        alumnoMensaje.Validado = false;

                        try
                        {
                            var ActualizarRegistroPre = _repWhatsAppConfiguracionPreEnvio.FirstById(alumnoMensaje.IdPre);

                            if (ActualizarRegistroPre != null)
                            {
                                ActualizarRegistroPre.FechaModificacion = DateTime.Now;
                                ActualizarRegistroPre.Procesado = false;
                                ActualizarRegistroPre.MensajeProceso = "Error";
                                _repWhatsAppConfiguracionPreEnvio.Update(ActualizarRegistroPre);
                            }

                            _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                        }
                        catch (Exception ex)
                        {
                            var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                            _repLog.Insert(new TLog
                            {
                                Ip = "-",
                                Usuario = "WhatsAppMasivo",
                                Maquina = "-",
                                Ruta = "InsertarWhatsappEnvioDetalle/ActualizarWhatsappPreEnvio4",
                                Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                                Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                                Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                                Tipo = "SEND",
                                IdPadre = 0,
                                UsuarioCreacion = "CampaniaGeneral",
                                UsuarioModificacion = "CampaniaGeneral",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                try
                {
                    // Valor por defecto es false para seguir el flujo normal
                    if (!aplicarLimiteEspera) Thread.Sleep(1500);

                    // Espera opcional
                    if (aplicarLimiteEspera && cantidadGrupoActual >= LIMITE_CANTIDAD_ESPERA)
                    {
                        cantidadGrupoActual = 0;
                        Thread.Sleep(1500);
                    }
                }
                catch (Exception ex)
                {
                    var _repLog = new LogRepositorio(_integraDBContext);

                    try
                    {
                        var mensajeCompleto = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}";

                        _repLog.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "-",
                            Maquina = "-",
                            Ruta = "EnviarWhatsAppCatch",
                            Parametros = $"{IdWhatsAppConfiguracionLogEjecucion}/{alumnoMensaje.IdConjuntoListaResultado}/{alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                            Mensaje = mensajeCompleto.Length > 4000 ? mensajeCompleto.Substring(0, 4000) : mensajeCompleto,
                            Excepcion = ex.ToString().Length > 2500 ? ex.ToString().Substring(0, 2500) : ex.ToString(),
                            Tipo = "SEND",
                            IdPadre = 0,
                            UsuarioCreacion = string.Empty,
                            UsuarioModificacion = string.Empty,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    catch (Exception)
                    {
                    }
                }

                if (cantidadErrorEnvioActual >= LIMITE_CANTIDAD_ERROR_ENVIO)
                {
                    try
                    {
                        _repLog.Insert(new TLog
                        {
                            Ip = "-",
                            Usuario = "WhatsAppMasivo",
                            Maquina = "-",
                            Ruta = "CantidadMaximaPermitida",
                            Parametros = $"IdWhatsAppConfiguracionPreEnvio={alumnoMensaje.IdPre}/IdPrioridadMailChimpListaCorreo={alumnoMensaje.IdPrioridadMailChimpListaCorreo}",
                            Mensaje = $"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}",
                            Excepcion = $"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}",
                            Tipo = "SEND",
                            IdPadre = 0,
                            UsuarioCreacion = "CampaniaGeneral",
                            UsuarioModificacion = "CampaniaGeneral",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                    }
                    catch (Exception)
                    {
                    }

                    throw new Exception($"Se supero la cantidad permitida maxima de errores {LIMITE_CANTIDAD_ERROR_ENVIO}");
                }
            }

            return RegistroEstadoEnvio;
        }

        private void EnvioAutomaticoPlantillaOperaciones(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _repPlantilla.ObtenerPlantillaPorId(MensajeAlumno[0].IdPlantilla ?? default(int));
            foreach (var AlumnoMensaje in MensajeAlumno)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = AlumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = AlumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = AlumnoMensaje.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(AlumnoMensaje.IdPersonal ?? default(int), AlumnoMensaje.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(AlumnoMensaje.IdPersonal ?? default(int));

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        //IRestResponse response = client.Execute(request);

                        //if (response.StatusCode == HttpStatusCode.OK)
                        //{
                        //    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        //    foreach (var item in datos.users)
                        //    {
                        //        TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                        //        modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                        //        modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                        //        modelCredencial.UserAuthToken = item.token;
                        //        modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                        //        modelCredencial.EsMigracion = true;
                        //        modelCredencial.Estado = true;
                        //        modelCredencial.FechaCreacion = DateTime.Now;
                        //        modelCredencial.FechaModificacion = DateTime.Now;
                        //        modelCredencial.UsuarioCreacion = "whatsapp";
                        //        modelCredencial.UsuarioModificacion = "whatsapp";

                        //        var rpta = _repTokenUsuario.Insert(modelCredencial);

                        //        _tokenComunicacion = item.token;
                        //    }

                        //    banderaLogin = true;

                        //}
                        //else
                        //{
                        //    banderaLogin = false;
                        //}
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "hsm";

                                MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = DTO.WaType;
                                _mensajePlantilla.hsm = new hsm();

                                _mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.hsm.element_name = DTO.WaBody;

                                _mensajePlantilla.hsm.language = new language();
                                _mensajePlantilla.hsm.language.policy = "deterministic";
                                _mensajePlantilla.hsm.language.code = "es";

                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        localizable_params _dato = new localizable_params();
                                        _dato.@default = listaDatos.texto;

                                        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                    }
                                }

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                        WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = true;
                        mensajeEnviado.MensajeError = "";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.Mensaje = DTO.WaCaption;
                        mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Operaciones";
                        mensajeEnviado.UsuarioModificacion = "Operaciones";

                        _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);

                        //return Ok(mensajeEnviado.WaId);
                    }
                    else
                    {
                        WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                        mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "Operaciones";
                        mensajeEnviado.UsuarioModificacion = "Operaciones";
                        _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                }
                catch (Exception ex)
                {
                    WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "Envio";
                    mensajeEnviado.UsuarioModificacion = "Envio";
                    _repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                }

                System.Threading.Thread.Sleep(5000);
            }

        }

        [Route("[Action]/{IdWhatsAppConfiguracionEnvio}")]
        [HttpGet]
        public ActionResult RegularizarPlantilla(int IdWhatsAppConfiguracionEnvio)
        {

            if (IdWhatsAppConfiguracionEnvio != 0)
            {
            }
            else
            {
                string plantilla = string.Empty;
                string plantilla0 = string.Empty;
                string valor = string.Empty;
                string Numero = "";
                //var Alumno = new object();
                int IdPersonalProceso = 0;
                int IdPlantillaProceso = 0;
                int IdPgeneralProceso = 0;
                int IdPaisProceso = 0;
                int IdPpgeneralPlantillaProceso = 0;

                PersonalBO Asesor = new PersonalBO();
                PlantillaCentroCostoDTO rpta0 = new PlantillaCentroCostoDTO();
                ModalidadProgramaDTO FechaInicioPrograma0 = new ModalidadProgramaDTO();
                List<ModalidadProgramaDTO> fecha0 = new List<ModalidadProgramaDTO>();

                var NumeroAlumno = _repConjuntoListaResultado.ObtenerEnvioSinMensaje();
                foreach (var AlumnoEtiqueta in NumeroAlumno)
                {
                    try
                    {
                        //AlumnoEtiqueta.objetoplantilla = new List<datoPlantillaWhatsApp>();

                        Numero = AlumnoEtiqueta.Celular;
                        if (Numero.StartsWith("51"))
                        {
                            Numero = Numero.Substring(2, 9);
                        }
                        else if (Numero.StartsWith("57"))
                        {
                            Numero = "00" + Numero;
                        }
                        else if (Numero.StartsWith("591"))
                        {
                            Numero = "00" + Numero;
                        }
                        else
                        {

                        }

                        var Alumno = _repAlumno.FirstBy(w => w.Celular.Contains(Numero) && w.Id == AlumnoEtiqueta.IdAlumno, y => new { y.Nombre1, y.Nombre2, y.ApellidoMaterno, y.ApellidoPaterno });

                        if (AlumnoEtiqueta.IdPersonal != IdPersonalProceso)
                        {
                            Asesor = _repPersonal.FirstBy(w => w.Id == AlumnoEtiqueta.IdPersonal, y => new PersonalBO { Nombres = y.Nombres, Apellidos = y.Apellidos, Anexo3Cx = y.Anexo3Cx, Central = y.Central, MovilReferencia = y.MovilReferencia });

                            IdPersonalProceso = AlumnoEtiqueta.IdPersonal;
                        }

                        if (AlumnoEtiqueta.IdPlantilla != IdPlantillaProceso)
                        {
                            plantilla0 = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == AlumnoEtiqueta.IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;
                            plantilla = plantilla0;
                            IdPlantillaProceso = AlumnoEtiqueta.IdPlantilla;
                        }
                        else
                        {
                            plantilla = plantilla0;
                        }
                        PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                        ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                        List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();
                        //foreach (var item in ProgramaPrincipal)
                        //{
                        if (AlumnoEtiqueta.IdPgeneral != IdPgeneralProceso)
                        {
                            rpta0 = _repCentroCosto.ObtenerRemplazoPlantilla(AlumnoEtiqueta.IdPgeneral);
                            rpta = rpta0;
                            IdPgeneralProceso = AlumnoEtiqueta.IdPgeneral;
                        }
                        else
                        {
                            rpta = rpta0;
                        }
                        if (plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.DiaFechaInicioPrograma}") || plantilla.Contains("{T_Pespecifico.NombreMesFechaInicioPrograma}"))
                        {
                            if (AlumnoEtiqueta.IdPgeneral != IdPpgeneralPlantillaProceso || AlumnoEtiqueta.IdCodigoPais != IdPaisProceso)
                            {
                                fecha0 = _repPgeneral.ObtenerFechaInicioProgramaGeneral(AlumnoEtiqueta.IdPgeneral, AlumnoEtiqueta.IdCodigoPais);
                                IdPpgeneralPlantillaProceso = AlumnoEtiqueta.IdPgeneral;
                                IdPaisProceso = AlumnoEtiqueta.IdCodigoPais;
                                fecha = fecha0;
                            }
                            else
                            {
                                fecha = fecha0;
                            }


                            if (fecha.Count > 0)
                            {
                                FechaInicioPrograma0 = fecha.Where(w => w.Tipo.ToUpper().Contains("PRESENCIAL") && !w.NombreESP.Contains("Sesion Especial")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                if (FechaInicioPrograma0 == null)
                                {
                                    FechaInicioPrograma0 = fecha.Where(w => w.Tipo.ToUpper().Contains("ONLINE SINCRONICA")).OrderBy(w => w.FechaReal).FirstOrDefault();
                                    FechaInicioPrograma = FechaInicioPrograma0;
                                }
                                else
                                {
                                    FechaInicioPrograma = FechaInicioPrograma0;
                                }
                            }
                        }
                        //plantillaPw.ObtenerFechaInicioPrograma(item.IdPgeneral, rpta.IdCentroCosto);
                        //}


                        foreach (string word in plantilla.Split('{'))
                        {
                            datoPlantillaWhatsApp plantillaEtiqueValor = new datoPlantillaWhatsApp();
                            if (word.Contains('}'))
                            {
                                string etiqueta = word.Split('}')[0];
                                //Separamos solo los Id´s

                                if (etiqueta.Contains("tPartner.nombre"))
                                {

                                    valor = rpta.NombrePartner;
                                }
                                else if (etiqueta.Contains("tPEspecifico.nombre"))
                                {
                                    valor = rpta.NombrePEspecifico;
                                }
                                else if (etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                {
                                    valor = rpta.NombrePgeneral;
                                }

                                if (etiqueta.Contains("T_Pespecifico.NombreDiaSemanaFechaInicioPrograma"))
                                {
                                    if (fecha.Count != 0)
                                    {
                                        CultureInfo ci = new CultureInfo("es-ES");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        valor = ci.DateTimeFormat.GetDayName(FechaInicioetiqueta.DayOfWeek);
                                        valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                    }
                                    else
                                    {
                                        valor = "";
                                    }
                                }
                                else if (etiqueta.Contains("T_Pespecifico.DiaFechaInicioPrograma"))
                                {
                                    if (fecha.Count != 0)
                                    {
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        valor = FechaInicioetiqueta.Day.ToString();
                                    }
                                    else
                                    {
                                        valor = "";
                                    }
                                }
                                else if (etiqueta.Contains("T_Pespecifico.NombreMesFechaInicioPrograma"))
                                {
                                    if (fecha.Count != 0)
                                    {
                                        //CultureInfo ci = new CultureInfo("es-Es");
                                        DateTime FechaInicioetiqueta = new DateTime();
                                        FechaInicioetiqueta = FechaInicioPrograma.FechaReal.Value;

                                        valor = FechaInicioetiqueta.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                                        valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor);
                                    }
                                    else
                                    {
                                        valor = "";
                                    }
                                }
                                if (etiqueta.Contains("Template"))
                                {

                                    valor = "";
                                }
                                else
                                {

                                    if ((etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2") && AlumnoEtiqueta.IdPersonal > 0)
                                    {
                                        switch (etiqueta)
                                        {

                                            case "tPersonal.nombres":
                                                valor = Asesor.Nombres; break;
                                            case "tPersonal.apellidos":
                                                valor = Asesor.Apellidos; break;
                                            case "tPersonal.Telefono":
                                                {
                                                    if (!string.IsNullOrEmpty(Asesor.MovilReferencia))
                                                    {
                                                        valor = Asesor.MovilReferencia;
                                                    }
                                                    else
                                                    {
                                                        if (Asesor.Central == "192.168.0.20")
                                                        {
                                                            //aqp
                                                            valor = "(51) 54 258787 - Anexo " + Asesor.Anexo3Cx;
                                                        }
                                                        else
                                                        {
                                                            if (Asesor.Central == "192.168.2.20")
                                                            {
                                                                //lima
                                                                valor = "(51) 1 207 2770 - Anexo " + Asesor.Anexo3Cx;
                                                            }
                                                            else
                                                            {
                                                                valor = "(51) 54 258787";
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apepaterno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        valor = Alumno.ApellidoPaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.apematerno":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        valor = Alumno.ApellidoMaterno;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre1":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        valor = Alumno.Nombre1;
                                                    }
                                                }
                                                break;
                                            case "tAlumnos.nombre2":
                                                {
                                                    if (Alumno != null)
                                                    {
                                                        valor = Alumno.Nombre2;
                                                    }
                                                }
                                                break;
                                            default:
                                                valor = ""; break;
                                        }

                                    }
                                }
                                if (valor != null)
                                {
                                    valor = valor.Replace("#$%", "<br>");
                                    plantilla = plantilla.Replace("{" + etiqueta + "}", valor);

                                    plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                    plantillaEtiqueValor.texto = valor;

                                }
                                else
                                {
                                    plantilla = plantilla.Replace("{" + etiqueta + "}", "");

                                    plantillaEtiqueValor.codigo = "{ " + etiqueta + "}";
                                    plantillaEtiqueValor.texto = "";
                                }
                                //AlumnoEtiqueta.objetoplantilla.Add(plantillaEtiqueValor);
                            }
                        }
                        //AlumnoEtiqueta.Plantilla = plantilla;
                        WhatsAppConfiguracionEnvioDetalleBO whatsAppConfiguracionEnvioDetalle = new WhatsAppConfiguracionEnvioDetalleBO();

                        whatsAppConfiguracionEnvioDetalle = _repWhatsAppConfiguracionEnvioDetalle.FirstById(AlumnoEtiqueta.Id);
                        whatsAppConfiguracionEnvioDetalle.Mensaje = plantilla;
                        _repWhatsAppConfiguracionEnvioDetalle.Update(whatsAppConfiguracionEnvioDetalle);
                        //return Ok(new { plantilla, objetoplantilla });
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>();
                        correos.Add("fvaldez@bsginstitute.com");

                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO();
                        mailData.Sender = "fvaldez@bsginstitute.com";
                        mailData.Recipient = string.Join(",", correos);
                        mailData.Subject = "Error Proceso Plantillas";
                        mailData.Message = "Alumno: " + AlumnoEtiqueta.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + AlumnoEtiqueta.IdConjuntoListaResultado.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                        mailData.Cc = "";
                        mailData.Bcc = "";
                        mailData.AttachedFiles = null;

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
            }

            return Ok(true);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarListasWhatsAppEnvioAutomaticoOperaciones([FromBody] List<ConjuntoListaDetalleWhatsAppDTO> ListaConjuntoListaDetalleWhatsApp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                foreach (var WhatsAppConfiguracionEnvio in ListaConjuntoListaDetalleWhatsApp)
                {
                    WhatsAppConfiguracionLogEjecucionBO logEjecucion = new WhatsAppConfiguracionLogEjecucionBO();
                    try
                    {
                        logEjecucion.FechaInicio = DateTime.Now;
                        logEjecucion.FechaFin = null;
                        logEjecucion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                        logEjecucion.Estado = true;
                        logEjecucion.FechaCreacion = DateTime.Now;
                        logEjecucion.FechaModificacion = DateTime.Now;
                        logEjecucion.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        logEjecucion.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                        int idResultado = _repWhatsAppConfiguracionLogEjecucion.InsertarWhatsappConfiguracionLogEjecucion(logEjecucion);
                        logEjecucion.Id = idResultado;

                        var listaConjuntoListaResultado = _repConjuntoListaResultado.ObtenerConjuntoListaResultadoWhatsAppMasivoOperaciones(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);

                        this.ValidarNumeroConjuntoLista(ref listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.Id ?? default(int));
                        listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.Validado == true).ToList();

                        this.RemplazarEtiquetas(ref listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int));

                        //listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();
                        listaConjuntoListaResultado = listaConjuntoListaResultado.Where(w => w.objetoplantilla.Count != 0).ToList();

                        this.EnvioAutomaticoPlantilla(listaConjuntoListaResultado, WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), logEjecucion.Id);

                        //var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecucion.Id);
                        WhatsAppConfiguracionLogEjecucionBO logEjecucionFinal = new WhatsAppConfiguracionLogEjecucionBO();

                        logEjecucionFinal.Id = idResultado;
                        logEjecucionFinal.FechaFin = DateTime.Now;
                        //_repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);

                        _repWhatsAppConfiguracionLogEjecucion.ActualizarWhatsappConfiguracionLogEjecucionFechaFin(logEjecucionFinal);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            if (logEjecucion.Id == 0 || logEjecucion.Id == null)
                            {
                                logEjecucion.FechaFin = DateTime.Now;
                                //_repWhatsAppConfiguracionLogEjecucion.Insert(logEjecucion);

                                _repWhatsAppConfiguracionLogEjecucion.InsertarWhatsappConfiguracionLogEjecucion(logEjecucion);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void RemplazarEtiquetas(ref List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdPlantilla)
        {

            foreach (var itemConjuntoListaResultado in listaConjuntoListaResultado)
            {
                try
                {
                    itemConjuntoListaResultado.objetoplantilla = new List<datoPlantillaWhatsApp>();


                    // Mantener por respaldo
                    //if (!_repConjuntoListaResultado.Exist(itemConjuntoListaResultado.IdConjuntoListaResultado))
                    if (!_repConjuntoListaResultado.ExisteConjuntoListaResultado(itemConjuntoListaResultado.IdConjuntoListaResultado))
                    {
                        continue;
                    }

                    // Mantener por respaldo
                    //var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(itemConjuntoListaResultado.IdConjuntoListaResultado);
                    var conjuntoListaResultado = _repConjuntoListaResultado.BuscaConjuntoListaResultado(itemConjuntoListaResultado.IdConjuntoListaResultado);

                    if (!_repOportunidad.Exist(conjuntoListaResultado.IdOportunidad.Value))
                    {
                        continue;
                    }
                    var oportunidad = _repOportunidad.FirstById(conjuntoListaResultado.IdOportunidad.Value);

                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = conjuntoListaResultado.IdOportunidad.Value,
                        IdPlantilla = IdPlantilla
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                    itemConjuntoListaResultado.objetoplantilla = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado.ListaEtiquetas;
                    itemConjuntoListaResultado.IdPersonal = oportunidad.IdPersonalAsignado;
                    itemConjuntoListaResultado.Plantilla = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado.Plantilla;
                }
                catch (Exception e)
                {
                    List<string> correos = new List<string>
                    {
                        "wchoque@bsginstitute.com",
                        "jvillena@bsginstitute.com",
                        "gmiranda@bsginstitute.com"
                    };

                    TMK_MailServiceImpl mailService = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Plantillas",
                        Message = "Alumno: " + itemConjuntoListaResultado.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + itemConjuntoListaResultado.IdConjuntoListaResultado.ToString() + " < br/>" + e.Message + " <br/> Mensaje toString <br/> " + e.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    mailService.SetData(mailData);
                    mailService.SendMessageTask();
                }
            }
        }
        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdPlantilla, int IdWhatsAppConfiguracionLogEjecucion)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var Plantilla = _repPlantilla.ObtenerPlantillaPorId(IdPlantilla);
            foreach (var conjuntoListaResultado in listaConjuntoListaResultado)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = conjuntoListaResultado.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = conjuntoListaResultado.Plantilla,
                    datosPlantillaWhatsApp = conjuntoListaResultado.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(conjuntoListaResultado.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(conjuntoListaResultado.IdPersonal.Value, conjuntoListaResultado.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(conjuntoListaResultado.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        //IRestResponse response = client.Execute(request);

                        //if (response.StatusCode == HttpStatusCode.OK)
                        //{
                        //    var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        //    foreach (var item in datos.users)
                        //    {
                        //        TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                        //        modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                        //        modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                        //        modelCredencial.UserAuthToken = item.token;
                        //        modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                        //        modelCredencial.EsMigracion = true;
                        //        modelCredencial.Estado = true;
                        //        modelCredencial.FechaCreacion = DateTime.Now;
                        //        modelCredencial.FechaModificacion = DateTime.Now;
                        //        modelCredencial.UsuarioCreacion = "whatsapp";
                        //        modelCredencial.UsuarioModificacion = "whatsapp";

                        //        var rpta = _repTokenUsuario.Insert(modelCredencial);

                        //        _tokenComunicacion = item.token;
                        //    }

                        //    banderaLogin = true;

                        //}
                        //else
                        //{
                        //    banderaLogin = false;
                        //}
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":

                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "template";

                                MensajePlantillaWhatsAppEnvioTemplate _mensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = "template";
                                _mensajePlantilla.template = new template();

                                _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.template.name = DTO.WaBody;

                                _mensajePlantilla.template.language = new language();
                                _mensajePlantilla.template.language.policy = "deterministic";
                                _mensajePlantilla.template.language.code = "es";

                                _mensajePlantilla.template.components = new List<components>();
                                components Componente = new components();
                                Componente.type = "body";


                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    Componente.parameters = new List<parameters>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        parameters Dato = new parameters();
                                        Dato.type = "text";
                                        Dato.text = listaDatos.texto;

                                        Componente.parameters.Add(Dato);
                                    }
                                }

                                _mensajePlantilla.template.components.Add(Componente);

                                using (WebClient Client = new WebClient())
                                {
                                    Client.Encoding = Encoding.UTF8;
                                    var MensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var Serializer = new JavaScriptSerializer();

                                    var SerializedResult = Serializer.Serialize(_mensajePlantilla);
                                    string MyParameters = SerializedResult;
                                    Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                                    Client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = Client.UploadString(urlToPost, MyParameters);
                                }


                                //anterior

                                //urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                //_waType = "hsm";

                                //MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                //_mensajePlantilla.to = DTO.WaTo;
                                //_mensajePlantilla.type = DTO.WaType;
                                //_mensajePlantilla.hsm = new hsm();

                                //_mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                //_mensajePlantilla.hsm.element_name = DTO.WaBody;

                                //_mensajePlantilla.hsm.language = new language();
                                //_mensajePlantilla.hsm.language.policy = "deterministic";
                                //_mensajePlantilla.hsm.language.code = "es";

                                //if (DTO.datosPlantillaWhatsApp != null)
                                //{
                                //    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                //    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                //    {
                                //        localizable_params _dato = new localizable_params();
                                //        _dato.@default = listaDatos.texto;

                                //        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                //    }
                                //}

                                //using (WebClient client = new WebClient())
                                //{
                                //    client.Encoding = Encoding.UTF8;
                                //    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                //    var serializer = new JavaScriptSerializer();

                                //    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                //    string myParameters = serializedResult;
                                //    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                //    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                //    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                //    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                //    resultado = client.UploadString(urlToPost, myParameters);
                                //}

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                        WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = true;
                        mensajeEnviado.MensajeError = "";
                        mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = 0;
                        mensajeEnviado.Mensaje = DTO.WaCaption;
                        mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                        // Mantener por respaldo
                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);

                        //return Ok(mensajeEnviado.WaId);
                    }
                    else
                    {
                        WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        mensajeEnviado.EnviadoCorrectamente = false;
                        mensajeEnviado.MensajeError = "Error en credenciales de login o revise su conexion de red para el servidor de whatsapp.";
                        mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                        mensajeEnviado.ConjuntoListaNroEjecucion = conjuntoListaResultado.NroEjecucion;
                        mensajeEnviado.Estado = true;
                        mensajeEnviado.FechaCreacion = DateTime.Now;
                        mensajeEnviado.FechaModificacion = DateTime.Now;
                        mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                        // Mantener por respaldo
                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                }
                catch (Exception ex)
                {
                    WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                    mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    mensajeEnviado.EnviadoCorrectamente = false;
                    mensajeEnviado.MensajeError = ex.ToString();
                    mensajeEnviado.IdConjuntoListaResultado = conjuntoListaResultado.IdConjuntoListaResultado;
                    mensajeEnviado.ConjuntoListaNroEjecucion = conjuntoListaResultado.NroEjecucion;
                    mensajeEnviado.Estado = true;
                    mensajeEnviado.FechaCreacion = DateTime.Now;
                    mensajeEnviado.FechaModificacion = DateTime.Now;
                    mensajeEnviado.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                    mensajeEnviado.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                    _repWhatsAppConfiguracionEnvioDetalle.InsertarWhatsAppConfiguracionEnvioDetalle(mensajeEnviado);

                    // Mantener por respaldo
                    //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                }

                System.Threading.Thread.Sleep(5000);
            }

        }
        private void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> listaConjuntoListaResultado, int IdWhatsAppConfiguracionEnvio)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var ConjuntoListaResultado in listaConjuntoListaResultado)
            {
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + ConjuntoListaResultado.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(ConjuntoListaResultado.IdCodigoPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(ConjuntoListaResultado.IdPersonal.Value, ConjuntoListaResultado.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(ConjuntoListaResultado.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                modelCredencial.Id = _repTokenUsuario.InsertarWhatsAppUsuarioCredencial(modelCredencial);
                                // Mantener por respaldo
                                //var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                ConjuntoListaResultado.Validado = false;
                            }
                            else
                            {
                                ConjuntoListaResultado.Validado = true;
                            }
                        }
                        //Alumno.Validado = true;
                        whatsAppMensajePublicidad.IdAlumno = ConjuntoListaResultado.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = ConjuntoListaResultado.IdPersonal.Value;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = ConjuntoListaResultado.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = ConjuntoListaResultado.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = ConjuntoListaResultado.Celular;
                        whatsAppMensajePublicidad.EsValido = ConjuntoListaResultado.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        whatsAppMensajePublicidad.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        whatsAppMensajePublicidad.Id = _repWhatsAppMensajePublicidad.InsertarWhatsAppMensajePublicidad(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        ConjuntoListaResultado.Validado = false;

                        whatsAppMensajePublicidad.IdAlumno = ConjuntoListaResultado.IdAlumno;
                        whatsAppMensajePublicidad.IdPersonal = ConjuntoListaResultado.IdPersonal.Value;
                        whatsAppMensajePublicidad.IdConjuntoListaResultado = ConjuntoListaResultado.IdConjuntoListaResultado;
                        whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        whatsAppMensajePublicidad.IdPais = ConjuntoListaResultado.IdCodigoPais;
                        whatsAppMensajePublicidad.Celular = ConjuntoListaResultado.Celular;
                        whatsAppMensajePublicidad.EsValido = ConjuntoListaResultado.Validado;
                        whatsAppMensajePublicidad.Estado = true;
                        whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        whatsAppMensajePublicidad.UsuarioCreacion = "wchoque_ProcesoAutomatico";
                        whatsAppMensajePublicidad.UsuarioModificacion = "wchoque_ProcesoAutomatico";

                        // Mantener para reversion
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        whatsAppMensajePublicidad.Id = _repWhatsAppMensajePublicidad.InsertarWhatsAppMensajePublicidad(whatsAppMensajePublicidad);

                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "jvillena@bsginstitute.com",
                        "fvaldez@bsginstitute.com",
                        "gmiranda@bsginstitute.com"
                    };

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Validacion Numero WhatsApp",
                        Message = "Alumno: " + ConjuntoListaResultado.IdAlumno.ToString() + ", IdConjuntoListaResultado: " + ConjuntoListaResultado.IdConjuntoListaResultado.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }
        [Route("[Action]/{IdConjuntoLista}")]
        [HttpGet]
        public ActionResult ReanudarEnvioAutomatico(int IdConjuntoLista)
        {

            try
            {
                WhatsAppConfiguracionEnvioRepositorio _repWhatsAppConfiguracionEnvio = new WhatsAppConfiguracionEnvioRepositorio(_integraDBContext);
                WhatsAppConfiguracionEnvioPorProgramaRepositorio _repWhatsAppConfiguracionEnvioPorPrograma = new WhatsAppConfiguracionEnvioPorProgramaRepositorio(_integraDBContext);

                var eliminadoCorrectamente = _repWhatsAppConfiguracionEnvio.EliminarEnviosProcesados(IdConjuntoLista);
                //var eliminadoCorrectamente = 1;

                if (eliminadoCorrectamente == 1)
                {
                    var ListasWhatsApp = _repWhatsAppConfiguracionEnvio.ObtenerConfiguracionPorIdConjuntoLista(IdConjuntoLista);
                    foreach (var item in ListasWhatsApp)
                    {
                        item.ProgramaPrincipal = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == item.Id && w.IdTipoEnvioPrograma == 1, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                        item.ProgramaSecundario = _repWhatsAppConfiguracionEnvioPorPrograma.GetBy(w => w.IdWhatsAppConfiguracionEnvio == item.Id && w.IdTipoEnvioPrograma == 2, y => new WhatsAppConfiguracionEnvioPorProgramaDTO { IdPgeneral = y.IdPgeneral }).ToList();
                    }
                    foreach (var WhatsAppConfiguracionEnvio in ListasWhatsApp)
                    {
                        WhatsAppConfiguracionLogEjecucionBO logEjecion = new WhatsAppConfiguracionLogEjecucionBO();
                        try
                        {
                            logEjecion.FechaInicio = DateTime.Now;
                            logEjecion.IdWhatsAppConfiguracionEnvio = WhatsAppConfiguracionEnvio.Id ?? default(int);
                            logEjecion.Estado = true;
                            logEjecion.FechaCreacion = DateTime.Now;
                            logEjecion.FechaModificacion = DateTime.Now;
                            logEjecion.UsuarioCreacion = "ProcesoAutomatico";
                            logEjecion.UsuarioModificacion = "ProcesoAutomatico";
                            _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);


                            var Respuesta = _repConjuntoListaResultado.ObtenerConjuntoListaResultado(WhatsAppConfiguracionEnvio.IdConjuntoListaDetalle);
                            this.ValidarNumeroConjuntoLista(ref Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.Id ?? default(int));
                            Respuesta = Respuesta.Where(w => w.Validado == true).ToList();

                            this.RemplazarEtiquetas(ref Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), WhatsAppConfiguracionEnvio.ProgramaPrincipal, WhatsAppConfiguracionEnvio.ProgramaSecundario);
                            Respuesta = Respuesta.Where(w => w.Plantilla != null && w.Plantilla != "" && w.objetoplantilla.Count != 0).ToList();

                            this.EnvioAutomaticoPlantilla(Respuesta, WhatsAppConfiguracionEnvio.IdPersonal ?? default(int), WhatsAppConfiguracionEnvio.IdPlantilla ?? default(int), logEjecion.Id);

                            var logEjecucionFinal = _repWhatsAppConfiguracionLogEjecucion.FirstById(logEjecion.Id);
                            logEjecucionFinal.FechaFin = DateTime.Now;
                            _repWhatsAppConfiguracionLogEjecucion.Update(logEjecucionFinal);


                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                if (logEjecion.Id == 0 || logEjecion.Id == null)
                                {
                                    logEjecion.FechaFin = DateTime.Now;
                                    _repWhatsAppConfiguracionLogEjecucion.Insert(logEjecion);
                                }
                            }
                            catch (Exception e)
                            {

                            }


                        }

                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        ///Funcion donde se realiza el estado del numero de whatsapp del alumno es llamado desde complementos 
        /// </summary>
        /// <returns>ok/returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarNumerosWhatsapp()
        {
            try
            {
                AlumnoBO _ValidarAlumno = new AlumnoBO();

                var alumnos = _repAlumno.ObtenerALumnosaValidarWhatsapp();
                foreach (var item in alumnos)
                {
                    ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                    contact.contacts = new List<string>();
                    var alumno = _repAlumno.FirstById(item.IdAlumno);

                    try
                    {

                        using (TransactionScope scope = new TransactionScope())
                        {
                            var alumnoNumero = _repAlumno.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                            contact.contacts.Add("+" + alumnoNumero);
                            var validacion = _ValidarAlumno.ValidarNumeroEnvioWhatsApp(4589, item.IdCodigoPais, contact);

                            //Actualizo el idestadowhatsapp del alumno
                            alumno.IdEstadoContactoWhatsApp = validacion == true ? 1 : 2; //1:valido 2:invalido
                            alumno.FechaModificacion = DateTime.Now;
                            alumno.UsuarioModificacion = "Masivo Whatsapp";
                            _repAlumno.Update(alumno);
                            scope.Complete();
                        }
                    }
                    catch
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            //Actualizo el idestadowhatsapp del alumno
                            alumno.IdEstadoContactoWhatsApp = 4; //error al validar
                            alumno.FechaModificacion = DateTime.Now;
                            alumno.UsuarioModificacion = "Masivo Whatsapp";
                            _repAlumno.Update(alumno);

                            scope.Complete();
                        }
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        ///Funcion donde se realiza el envio de whatsapp de prueba 
        /// </summary>
        /// <returns>ok/returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarenviowhatsappPruebaDesarrollo()
        {
            try
            {

                bool BanderaLogin = false;
                string TokenComunicacion = string.Empty;

                var objetoplantilla = new List<datoPlantillaWhatsApp>();
                datoPlantillaWhatsApp obj1 = new datoPlantillaWhatsApp();
                obj1.codigo = "{tAlumnos.nombre1}";
                obj1.texto = "Maria";
                datoPlantillaWhatsApp obj2 = new datoPlantillaWhatsApp();
                obj2.codigo = "{tPLA_PGeneral.Nombre}";
                obj2.texto = "Implementador Lider en Sistemas Integrados";
                objetoplantilla.Add(obj1);
                objetoplantilla.Add(obj2);


                var DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = "51980825734",//Cambiar el numero al que desea enviar
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = "saludo_bienvenida_tres",
                    WaCaption = "Hola Maria  Te saludamos desde BSG Institute para recordarte que te hemos enviado un correo electrónico con información actualizada del Implementador Líder en Sistemas Integrados de Gestión HSEQ ISO 9001, ISO 14001, ISO 45001. Quedamos a tu disposición para cualquier duda o consulta que puedas tener.",
                    datosPlantillaWhatsApp = objetoplantilla
                };

                ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };


                var _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                var _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                var CredencialesHost = _repCredenciales.ObtenerCredencialHost(51);
                var TokenValida = _repTokenUsuario.ValidarCredencialesUsuario(4659, 51);


                string urlToPost = CredencialesHost.UrlWhatsApp;

                string Resultado = string.Empty, WaType = string.Empty;

                TokenComunicacion = TokenValida.UserAuthToken;
                BanderaLogin = true;

                switch (DTO.WaType.ToLower())
                {
                    case "text":
                        urlToPost = $"{CredencialesHost.UrlWhatsApp}/v1/messages";
                        WaType = "text";

                        var MensajeTexto = new MensajeTextoEnvio
                        {
                            to = DTO.WaTo,
                            type = DTO.WaType,
                            recipient_type = DTO.WaRecipientType,
                            text = new text
                            {
                                body = DTO.WaBody
                            }
                        };

                        using (WebClient Client = new WebClient())
                        {
                            //client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeTexto);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeTexto);
                            string myParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            Resultado = Client.UploadString(urlToPost, myParameters);
                        }

                        break;
                    case "hsm":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "template";

                        MensajePlantillaWhatsAppEnvioTemplate MensajePlantilla = new MensajePlantillaWhatsAppEnvioTemplate();

                        MensajePlantilla.to = DTO.WaTo;
                        MensajePlantilla.type = "template";
                        MensajePlantilla.template = new template();

                        MensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                        MensajePlantilla.template.name = DTO.WaBody;

                        MensajePlantilla.template.language = new language();
                        MensajePlantilla.template.language.policy = "deterministic";
                        MensajePlantilla.template.language.code = "es";

                        MensajePlantilla.template.components = new List<components>();
                        components Componente = new components();
                        Componente.type = "body";


                        if (DTO.datosPlantillaWhatsApp != null)
                        {
                            Componente.parameters = new List<parameters>();
                            foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                            {
                                parameters Dato = new parameters();
                                Dato.type = "text";
                                Dato.text = listaDatos.texto;

                                Componente.parameters.Add(Dato);
                            }
                        }

                        MensajePlantilla.template.components.Add(Componente);

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajePlantilla);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajePlantilla);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                    case "image":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "image";

                        MensajeImagenEnvio MensajeImagen = new MensajeImagenEnvio();
                        MensajeImagen.to = DTO.WaTo;
                        MensajeImagen.type = DTO.WaType;
                        MensajeImagen.recipient_type = DTO.WaRecipientType;

                        MensajeImagen.image = new image();

                        MensajeImagen.image.caption = DTO.WaCaption;
                        MensajeImagen.image.link = DTO.WaLink;

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeImagen);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeImagen);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                    case "document":
                        urlToPost = CredencialesHost.UrlWhatsApp + "/v1/messages/";
                        WaType = "document";

                        MensajeDocumentoEnvio MensajeDocumento = new MensajeDocumentoEnvio();
                        MensajeDocumento.to = DTO.WaTo;
                        MensajeDocumento.type = DTO.WaType;
                        MensajeDocumento.recipient_type = DTO.WaRecipientType;

                        MensajeDocumento.document = new document();

                        MensajeDocumento.document.caption = DTO.WaCaption;
                        MensajeDocumento.document.link = DTO.WaLink;
                        MensajeDocumento.document.filename = DTO.WaFileName;

                        using (WebClient Client = new WebClient())
                        {
                            Client.Encoding = Encoding.UTF8;
                            var MensajeJSON = JsonConvert.SerializeObject(MensajeDocumento);
                            var Serializer = new JavaScriptSerializer();

                            var SerializedResult = Serializer.Serialize(MensajeDocumento);
                            string MyParameters = SerializedResult;
                            Client.Headers[HttpRequestHeader.Authorization] = "Bearer " + TokenComunicacion;
                            Client.Headers[HttpRequestHeader.ContentLength] = MensajeJSON.Length.ToString();
                            Client.Headers[HttpRequestHeader.Host] = CredencialesHost.IpHost;
                            Client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            Resultado = Client.UploadString(urlToPost, MyParameters);
                        }

                        break;
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: día/mes/año
        /// Versión: 1.0
        /// <summary>
        /// Calculo de ejecucion de estado de whatsapp de prueba
        /// </summary>
        /// <returns> 200 o 40*</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarEjecucionEstadoWhatsappMasivo()
        {
            try
            {
                AlumnoBO alumno = new AlumnoBO();
                int i = 0;
                bool bandera = true;
                while (bandera)
                {
                    var alumnosPeru = _repAlumno.ObtenerALumnosaValidarWhatsappPeru(5000, i);
                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 51, alumnosPeru);
                        _repAlumno.Update(alumnosPeru);
                        i++;
                    }
                    else
                    {
                        bandera = false;
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 07/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Calculo masivo del estado de whatsapp de los alumnos
        /// </summary>
        /// <returns>200 o 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarEjecucionEstadoWhatsappMasivoFinal()
        {
            try
            {
                AlumnoBO alumno = new AlumnoBO();
                int i = 0;
                bool bandera = true;
                bool banderaAlumnos = true;
                List<AlumnoBO> alumnosPeru = null;
                List<AlumnoBO> alumnosColombia = null;
                List<AlumnoBO> alumnosBolivia = null;
                List<AlumnoBO> alumnosInternacional = null;
                List<AlumnoBO> temporal = null;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosPeru = _repAlumno.ObtenerALumnosaValidarWhatsappPeru(3000, i);
                        temporal = alumnosPeru;
                    }
                    else
                    {
                        alumnosPeru = temporal;
                    }

                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 51, alumnosPeru);
                        if (alumnosPeru != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosPeru);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosPeru);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosColombia = _repAlumno.ObtenerALumnosaValidarWhatsappColombia(3000, i);
                        temporal = alumnosColombia;
                    }
                    else
                    {
                        alumnosColombia = temporal;
                    }

                    if (alumnosColombia.Count > 0)
                    {
                        alumnosColombia = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 57, alumnosColombia);
                        if (alumnosColombia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosColombia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosColombia);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosBolivia = _repAlumno.ObtenerALumnosaValidarWhatsappBolivia(3000, i);
                        temporal = alumnosBolivia;
                    }
                    else
                    {
                        alumnosBolivia = temporal;
                    }

                    if (alumnosBolivia.Count > 0)
                    {
                        alumnosBolivia = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 591, alumnosBolivia);
                        if (alumnosBolivia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosBolivia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosBolivia);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                bandera = true;
                i = 0;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosInternacional = _repAlumno.ObtenerALumnosaValidarWhatsappInternacional(3000, i);
                        temporal = alumnosInternacional;
                    }
                    else
                    {
                        alumnosInternacional = temporal;
                    }

                    if (alumnosInternacional.Count > 0)
                    {
                        alumnosInternacional = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 0, alumnosInternacional);
                        if (alumnosInternacional != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosInternacional);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosInternacional);
                            i++;
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                    Thread.Sleep(20 * 1000);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 07/01/2022
        /// Versión: 1.0
        /// <summary>
        /// Calculo masivo del estado de whatsapp de los alumnos
        /// </summary>
        /// <returns>200 o 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult RegularizarEstadoWhatsapp()
        {
            try
            {
                AlumnoBO alumno = new AlumnoBO();
                bool bandera = true;
                bool banderaAlumnos = true;
                List<AlumnoBO> alumnosPeru = null;
                List<AlumnoBO> alumnosColombia = null;
                List<AlumnoBO> alumnosBolivia = null;
                List<AlumnoBO> alumnosInternacional = null;
                List<AlumnoBO> temporal = null;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosPeru = _repAlumno.ObtenerALumnosaRegularizarWhatsappPeru();
                        temporal = alumnosPeru;
                    }
                    else
                    {
                        alumnosPeru = temporal;
                    }

                    if (alumnosPeru.Count > 0)
                    {
                        alumnosPeru = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 51, alumnosPeru);
                        if (alumnosPeru != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosPeru);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosPeru);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosColombia = _repAlumno.ObtenerALumnosaRegularizarWhatsappColombia();
                        temporal = alumnosColombia;
                    }
                    else
                    {
                        alumnosColombia = temporal;
                    }

                    if (alumnosColombia.Count > 0)
                    {
                        alumnosColombia = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 57, alumnosColombia);
                        if (alumnosColombia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosColombia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosColombia);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosBolivia = _repAlumno.ObtenerALumnosaRegularizarWhatsappBolivia();
                        temporal = alumnosBolivia;
                    }
                    else
                    {
                        alumnosBolivia = temporal;
                    }

                    if (alumnosBolivia.Count > 0)
                    {
                        alumnosBolivia = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 591, alumnosBolivia);
                        if (alumnosBolivia != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosBolivia);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosBolivia);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                bandera = true;
                while (bandera)
                {
                    if (banderaAlumnos)
                    {
                        alumnosInternacional = _repAlumno.ObtenerALumnosaRegularizarWhatsappInternacional();
                        temporal = alumnosInternacional;
                    }
                    else
                    {
                        alumnosInternacional = temporal;
                    }

                    if (alumnosInternacional.Count > 0)
                    {
                        alumnosInternacional = alumno.ValidarEstadoContactoWhatsAppMasivo(_integraDBContext, 0, alumnosInternacional);
                        if (alumnosInternacional != null)
                        {
                            alumno.ActualizacionMasivaEstadoWhatsApp(_integraDBContext, alumnosInternacional);
                            alumno.ActualizacionMasivaEstadoWhatsAppSecundario(_integraDBContext, alumnosInternacional);
                            banderaAlumnos = true;
                        }
                        else
                        {
                            banderaAlumnos = false;
                        }
                    }
                    else
                    {
                        bandera = false;
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
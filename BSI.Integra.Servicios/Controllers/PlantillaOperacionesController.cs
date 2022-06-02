using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Mandrill.Models;
using Nancy.Json;
using BSI.Integra.Aplicacion.DTOs;
using System.IO;
using System.Net;
using BSI.Integra.Aplicacion.Base.BO;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.Helper;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Servicios.DTOs;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;

using RestSharp.Newtonsoft.Json.NetCore;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.Marketing.BO;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlantillaOperaciones")]
    public class PlantillaOperacionesController : Controller
    {
        private integraDBContext _integraDBContext;
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
        private ReemplazoEtiquetaPlantillaBO _reemplazoEtiquetaPlantilla;
        private EnvioMasivoPlantillaBO _envioMasivoPlantilla;


        public PlantillaOperacionesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(integraDBContext);
            _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(integraDBContext);
            _repConfiguracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext);

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
            _envioMasivoPlantilla = new EnvioMasivoPlantillaBO(_integraDBContext);

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {
                var _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                var filtros = new
                {
                    listaPlantilla = _repPlantilla.ObtenerTodoFiltro()
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult PruebaPlantilla(int idMatriculaCabecera)
        {
            var pgeneralRepositorio = new PgeneralRepositorio(_integraDBContext);
            return Ok(pgeneralRepositorio.ObtenerBeneficiosVersion(idMatriculaCabecera));
        }

        [Route("[action]/{Remitente}/{CodigoAlumno}/{Destinatarios}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult Envio(string Remitente, string CodigoAlumno, string Destinatarios, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMatriculaCabecera.Exist(x => x.CodigoMatricula == CodigoAlumno))
                {
                    return BadRequest("Codigo de alumno no valido!");
                }

                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
                {
                    return BadRequest(ModelState);
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest(ModelState);
                }

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    return BadRequest(ModelState);
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = oportunidad.Id,
                    IdPlantilla = IdPlantilla
                };
                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                var destinatarios = Destinatarios.Split(";");

                if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {

                    var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (Remitente == "matriculas@bsginstitute.com" && (IdPlantilla == 1227 || IdPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.AddRange(destinatarios.ToList());

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = Remitente,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = Remitente,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                    _repGmailCorreo.Insert(gmailCorreo);
                }
                else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {//logica whatsapp
                    var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

                    foreach (var destinatario in destinatarios)
                    {
                        listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
                        {
                            IdAlumno = alumno.Id,
                            Celular = destinatario,
                            IdPersonal = personal.Id,
                            IdCodigoPais = alumno.IdCodigoPais ?? default,
                            IdConjuntoListaResultado = 0,
                            IdPgeneral = null,
                            IdPlantilla = IdPlantilla,
                            NroEjecucion = 1,
                            objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                            Plantilla = whatsAppCalculado.Plantilla,
                            Validado = false
                        });
                    }

                    this.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    this.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarEnvioCorreoSesionesWebinar([FromBody] List<DatosProgramasWebexDTO> ListaProgramasWebex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ListaProgramasWebex == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var horaactual = DateTime.Now;
                //var horaactual = new DateTime(2020, 11, 23, 20, 00, 00);


                //var cuentasWebex = _repPespecificoSesion.ObtenerCuentasWebex();

                foreach (var ProgramaEspecifico in ListaProgramasWebex)
                {
                    //if (ProgramaEspecifico.IdPEspecifico != 14292)
                    //{
                    //    continue;
                    //}
                    try
                    {

                        var listaSesiones = _repPespecifico.ObtenerSesionesNoVencidasPorPEspecificoURL(ProgramaEspecifico.IdPEspecifico).OrderBy(w => w.FechaInicio);

                        foreach (var item in listaSesiones)
                        {
                            if (ProgramaEspecifico.IdTiempoFrecuenciaCorreoConfirmacion == 6)//horas
                            {
                                var fechaComparar = item.FechaInicio.AddHours(-ProgramaEspecifico.ValorFrecuenciaCorreoConfirmacion);
                                //var fechaComparar = item.FechaInicio;
                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia correo
                                    var listaalumno = _repPespecifico.ObtenerAlumnosPorIdPEspecificoWebinar(ProgramaEspecifico.IdPEspecifico);

                                    foreach (var alumno in listaalumno)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaCorreoConfirmacion, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, item.Id, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                            else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreoConfirmacion == 2)//dias
                            {
                                var fechaComparar = item.FechaInicio.AddDays(-ProgramaEspecifico.ValorFrecuenciaCorreoConfirmacion);
                                //var fechaComparar = item.FechaInicio;
                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia correo
                                    var listaalumno = _repPespecifico.ObtenerAlumnosPorIdPEspecificoWebinar(ProgramaEspecifico.IdPEspecifico);
                                    foreach (var alumno in listaalumno)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaCorreoConfirmacion, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, item.Id, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                            else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreoConfirmacion == 3)//semanas
                            {
                                var valorDias = ProgramaEspecifico.ValorFrecuenciaCorreoConfirmacion * 7;
                                var fechaComparar = item.FechaInicio.AddDays(-valorDias);
                                //var fechaComparar = item.FechaInicio;
                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia correo
                                    var listaalumno = _repPespecifico.ObtenerAlumnosPorIdPEspecificoWebinar(ProgramaEspecifico.IdPEspecifico);
                                    foreach (var alumno in listaalumno)
                                    {
                                        //if (alumno.IdAlumno != 9899787) continue;
                                        EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaCorreoConfirmacion, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, item.Id, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarEnvioCorreoSesionesDocente([FromBody] List<DatosProgramasWebexDTO> ListaProgramasWebex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ListaProgramasWebex == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var horaactual = DateTime.Now;
                //var horaactual =new DateTime(2020, 10, 24, 19, 00, 00);

                //var cuentasWebex = _repPespecificoSesion.ObtenerCuentasWebex();

                foreach (var ProgramaEspecifico in ListaProgramasWebex)
                {
                    //if (ProgramaEspecifico.IdPEspecifico != 14259)
                    //{
                    //    continue;
                    //}
                    try
                    {

                        var listaSesiones = _repPespecifico.ObtenerSesionesNoVencidasPorPEspecificoURL(ProgramaEspecifico.IdPEspecifico).OrderBy(w => w.FechaInicio);

                        foreach (var item in listaSesiones)
                        {
                            if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 6)//horas
                            {
                                var fechaComparar = item.FechaInicio.AddHours(-ProgramaEspecifico.ValorFrecuenciaCorreo);
                                //var fechaComparar = item.FechaInicio;

                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia correo
                                    if (item.IdTipoPrograma == 3)
                                    {
                                        var listaDocente = _repPespecifico.ObtenerDocentesporIdPespecificoSesion(item.Id);

                                        foreach (var docente in listaDocente)
                                        {
                                            EnvioWebexCorreo(null, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, docente.Email, docente.ZonaHoraria == (int?)null ? 0 : docente.ZonaHoraria.Value, string.IsNullOrEmpty(docente.NombrePais) ? "Perú" : docente.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                    else
                                    {
                                        var listaDocente = _repPespecifico.ObtenerDocentesporIdPespecificoSesion(item.Id);

                                        foreach (var docente in listaDocente)
                                        {
                                            EnvioWebexCorreo(null, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, docente.Email, docente.ZonaHoraria == (int?)null ? 0 : docente.ZonaHoraria.Value, string.IsNullOrEmpty(docente.NombrePais) ? "Perú" : docente.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }


                                }
                            }
                            else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 2)//dias
                            {
                                var fechaComparar = item.FechaInicio.AddDays(-ProgramaEspecifico.ValorFrecuenciaCorreo);
                                //var fechaComparar = item.FechaInicio;

                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    if (item.IdTipoPrograma == 3)
                                    {
                                        var listaDocente = _repPespecifico.ObtenerDocentesporIdPespecificoSesion(item.Id);

                                        foreach (var docente in listaDocente)
                                        {
                                            EnvioWebexCorreo(null, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, docente.Email, docente.ZonaHoraria == (int?)null ? 0 : docente.ZonaHoraria.Value, string.IsNullOrEmpty(docente.NombrePais) ? "Perú" : docente.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                    else
                                    {
                                        var listaDocente = _repPespecifico.ObtenerDocentesporIdPespecificoSesion(item.Id);

                                        foreach (var docente in listaDocente)
                                        {
                                            EnvioWebexCorreo(null, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, docente.Email, docente.ZonaHoraria == (int?)null ? 0 : docente.ZonaHoraria.Value, string.IsNullOrEmpty(docente.NombrePais) ? "Perú" : docente.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                }
                            }
                            //else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 3)//semanas
                            //{
                            //    var valorDias = ProgramaEspecifico.ValorFrecuenciaCorreo * 7;

                            //    var fechaComparar = item.FechaInicio.AddDays(-valorDias);
                            //    if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                            //    {
                            //        //aqui llama para crear la sesiones webex
                            //        var urlsesion = InsertarSesionWebex(item, cuentasWebex, _integraDBContext);

                            //        PespecificoSesionBO PEspecificoSesion = _repPespecificoSesion.FirstById(item.Id);
                            //        PEspecificoSesion.UrlWebex = urlsesion.UrlWebex;
                            //        PEspecificoSesion.CuentaWebex = urlsesion.Cuenta;
                            //        PEspecificoSesion.FechaModificacion = DateTime.Now;
                            //        _repPespecificoSesion.Update(PEspecificoSesion);
                            //    }
                            //}

                        }
                    }
                    catch (Exception e)
                    {


                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarEnvioCorreoSesionesWebex([FromBody] List<DatosProgramasWebexDTO> ListaProgramasWebex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ListaProgramasWebex == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var horaactual = DateTime.Now;
                //var horaactual =new DateTime(2021, 06, 03, 19, 00, 00);

                //var cuentasWebex = _repPespecificoSesion.ObtenerCuentasWebex();

                foreach (var ProgramaEspecifico in ListaProgramasWebex)
                {
                    //if (ProgramaEspecifico.IdPEspecifico != 15841) continue;

                    try
                    {

                        var listaSesiones = _repPespecifico.ObtenerSesionesNoVencidasPorPEspecificoURL(ProgramaEspecifico.IdPEspecifico).OrderBy(w => w.FechaInicio);

                        foreach (var item in listaSesiones)
                        {
                            if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 6)//horas
                            {
                                var fechaComparar = item.FechaInicio.AddHours(-ProgramaEspecifico.ValorFrecuenciaCorreo);
                                //var fechaComparar = item.FechaInicio;

                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia correo
                                    if (item.IdTipoPrograma == 3)
                                    {
                                        var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesionWebinar(item.Id);

                                        foreach (var alumno in listaalumno)
                                        {
                                            //if (alumno.IdAlumno != 9759872) continue;
                                            EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                    else
                                    {

                                        var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesion(item.Id, item.Grupo);

                                        foreach (var alumno in listaalumno)
                                        {
                                            //if (alumno.IdAlumno != 9759872) continue;
                                            EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }


                                }
                            }
                            else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 2)//dias
                            {
                                var fechaComparar = item.FechaInicio.AddDays(-ProgramaEspecifico.ValorFrecuenciaCorreo);
                                //var fechaComparar = item.FechaInicio;

                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    if (item.IdTipoPrograma == 3)
                                    {
                                        var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesionWebinar(item.Id);

                                        foreach (var alumno in listaalumno)
                                        {
                                            //if (alumno.IdAlumno != 9759872) continue;
                                            EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                    else
                                    {
                                        var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesion(item.Id, item.Grupo);

                                        foreach (var alumno in listaalumno)
                                        {
                                            //if (alumno.IdAlumno != 9318615) continue;
                                            EnvioWebexCorreo(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaFrecuenciaCorreo, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, ProgramaEspecifico.IdPEspecifico, null, null);
                                        }
                                    }
                                }
                            }
                            //else if (ProgramaEspecifico.IdTiempoFrecuenciaCorreo == 3)//semanas
                            //{
                            //    var valorDias = ProgramaEspecifico.ValorFrecuenciaCorreo * 7;

                            //    var fechaComparar = item.FechaInicio.AddDays(-valorDias);
                            //    if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                            //    {
                            //        //aqui llama para crear la sesiones webex
                            //        var urlsesion = InsertarSesionWebex(item, cuentasWebex, _integraDBContext);

                            //        PespecificoSesionBO PEspecificoSesion = _repPespecificoSesion.FirstById(item.Id);
                            //        PEspecificoSesion.UrlWebex = urlsesion.UrlWebex;
                            //        PEspecificoSesion.CuentaWebex = urlsesion.Cuenta;
                            //        PEspecificoSesion.FechaModificacion = DateTime.Now;
                            //        _repPespecificoSesion.Update(PEspecificoSesion);
                            //    }
                            //}
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarEnvioWhatsappSesionesWebex([FromBody] List<DatosProgramasWebexDTO> ListaProgramasWebex)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ListaProgramasWebex == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var horaactual = DateTime.Now;
                //var horaactual = new DateTime(2020, 11, 04, 20, 00, 00);
                foreach (var ProgramaEspecifico in ListaProgramasWebex)
                {
                    //if (ProgramaEspecifico.IdPEspecifico != 14279) continue;
                    try
                    {
                        var listaSesiones = _repPespecifico.ObtenerSesionesNoVencidasPorPEspecificoURL(ProgramaEspecifico.IdPEspecifico).OrderBy(w => w.FechaInicio);

                        foreach (var item in listaSesiones)
                        {
                            if (ProgramaEspecifico.IdTiempoFrecuenciaWhatsapp == 6)//horas
                            {
                                var fechaComparar = item.FechaInicio.AddHours(-ProgramaEspecifico.ValorFrecuenciaWhatsapp);

                                //var fechaComparar = item.FechaInicio;
                                if (fechaComparar.Date == horaactual.Date && fechaComparar.Hour == horaactual.Hour && fechaComparar.Minute == horaactual.Minute)
                                {
                                    //aqui envia whatsapp
                                    //var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesionWebinar(item.Id);
                                    var listaalumno = _repPespecifico.ObtenerAlumnosporIdPespecificoSesion(item.Id, item.Grupo);
                                    foreach (var alumno in listaalumno)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, ProgramaEspecifico.IdPlantillaFrecuenciaWhatsapp, alumno.Email);
                                    }

                                }
                            }

                        }
                    }
                    catch (Exception e)
                    {


                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public bool EnvioWebexCorreo(string CodigoAlumno, int IdPlantilla, string Destinatario, int IncrementoZonaHoraria, string NombrePais, int idPEspecifico, int? idPEspecificoSesion, int? idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                if (!_repMatriculaCabecera.Exist(x => x.CodigoMatricula == CodigoAlumno))
                {
                    return false;
                }

                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
                {
                    return false;
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return false;
                }

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    return false;
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = oportunidad.Id,
                    IdPlantilla = IdPlantilla,
                    IncrementoZonaHoraria = IncrementoZonaHoraria,
                    NombrePais = NombrePais,
                    IdPEspecifico = idPEspecifico,
                    IdPEspecificoSesion = idPEspecificoSesion,
                    IdMatriculaCabecera = idMatriculaCabecera
                };
                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                //var destinatarios = "jvillena@bsginstitute.com";
                var destinatarios = alumno.Email1;

                var Remitente = string.IsNullOrEmpty(personal.Email) == true ? "matriculas@bsginstitute.com" : personal.Email;

                if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {

                    var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (Remitente == "matriculas@bsginstitute.com" && (IdPlantilla == 1227 || IdPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "jvillena@bsginstitute.com",
                        "modpru@bsginstitute.com",
                        //"ccrispin@bsginstitute.com",
                        "wruiz@bsginstitute.com"
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.Add(destinatarios);

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = Remitente,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = Remitente,
                        Cc = "",
                        Bcc = "modpru@bsginstitute.com",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                    _repGmailCorreo.Insert(gmailCorreo);
                    return true;
                }
                else
                {
                    return false;
                }
                //else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                //{//logica whatsapp
                //    var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                //    var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

                //    foreach (var destinatario in destinatarios)
                //    {
                //        listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
                //        {
                //            IdAlumno = alumno.Id,
                //            Celular = destinatario,
                //            IdPersonal = personal.Id,
                //            IdCodigoPais = alumno.IdCodigoPais ?? default,
                //            IdConjuntoListaResultado = 0,
                //            IdPgeneral = null,
                //            IdPlantilla = IdPlantilla,
                //            NroEjecucion = 1,
                //            objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                //            Plantilla = whatsAppCalculado.Plantilla,
                //            Validado = false
                //        });
                //    }

                //    this.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                //    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
                //    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                //    this.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                //}
                //return Ok(true);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool EnvioWebexWhatsapp(string CodigoAlumno, int IdPlantilla, string Destinatario)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                //IdPlantilla = 935;
                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);
                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                alumno.Celular = alumno.Celular.Replace("+", "");

                if (alumno.IdPais == 51)
                {
                    alumno.Celular = "51" + alumno.Celular;
                }
                else if (alumno.IdPais == 57)
                {
                    if (alumno.Celular.Length == 12 && alumno.Celular.StartsWith("57"))
                    {
                        alumno.Celular = alumno.Celular;
                    }
                    else
                    {
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                    }


                }
                else if (alumno.IdPais == 591)
                {
                    if (alumno.Celular.Length == 11 && alumno.Celular.StartsWith("591"))
                    {
                        alumno.Celular = alumno.Celular;
                    }
                    else
                    {
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                    }
                }
                else
                {
                    alumno.Celular = "1";
                }

                if (alumno.IdPais == 51)
                {
                    alumno.Celular = alumno.Celular + "51934129449";
                }

                var Destinatarios = alumno.Celular;
                //var Destinatarios = "51934129449";
                var destinatarios = Destinatarios.Split(";");

                if (plantilla.IdPlantillaBase == 8)
                {//logica whatsapp
                    _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPlantilla = IdPlantilla
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                    var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

                    foreach (var destinatario in destinatarios)
                    {
                        listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
                        {
                            IdAlumno = alumno.Id,
                            Celular = destinatario,
                            IdPersonal = personal.Id,
                            IdCodigoPais = alumno.IdCodigoPais ?? default,
                            IdConjuntoListaResultado = 0,
                            IdPgeneral = null,
                            IdPlantilla = IdPlantilla,
                            NroEjecucion = 1,
                            objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                            Plantilla = whatsAppCalculado.Plantilla,
                            Validado = false
                        });
                    }

                    this.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    this.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados)
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
                    //calcula el valor que tiene
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(Alumno.IdPersonal.Value, Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(Alumno.IdPersonal.Value);

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
                        //whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        //whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        //whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        //whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        //whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        //whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        //whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        //whatsAppMensajePublicidad.Estado = true;
                        //whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        //whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        //whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        //whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        Alumno.Validado = false;

                        //whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        //whatsAppMensajePublicidad.IdPersonal = IdPersonal;
                        //whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        //whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        //whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        //whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        //whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        //whatsAppMensajePublicidad.Estado = true;
                        //whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        //whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        //whatsAppMensajePublicidad.UsuarioCreacion = "ValidacionAutomatica";
                        //whatsAppMensajePublicidad.UsuarioModificacion = "ValidacionAutomatica";
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("ccrispin@bsginstitute.com");
                    correos.Add("jvillena@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", Numero: " + Alumno.Celular.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }
        private void ValidarNumeroConjuntoOperaciones(ref List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados)
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
                        //whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        //whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        //whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        //whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        //whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        //whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        //whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        //whatsAppMensajePublicidad.Estado = true;
                        //whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        //whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        //whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        //whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
                    }
                    else
                    {
                        Alumno.Validado = false;

                        //whatsAppMensajePublicidad.IdAlumno = Alumno.IdAlumno;
                        //whatsAppMensajePublicidad.IdPersonal = Alumno.IdPersonal ?? default(int);
                        //whatsAppMensajePublicidad.IdConjuntoListaResultado = Alumno.IdConjuntoListaResultado;
                        //whatsAppMensajePublicidad.IdWhatsAppConfiguracionEnvio = IdWhatsAppConfiguracionEnvio;
                        //whatsAppMensajePublicidad.IdPais = Alumno.IdCodigoPais;
                        //whatsAppMensajePublicidad.Celular = Alumno.Celular;
                        //whatsAppMensajePublicidad.EsValido = Alumno.Validado;
                        //whatsAppMensajePublicidad.Estado = true;
                        //whatsAppMensajePublicidad.FechaCreacion = DateTime.Now;
                        //whatsAppMensajePublicidad.FechaModificacion = DateTime.Now;
                        //whatsAppMensajePublicidad.UsuarioCreacion = "Operaciones";
                        //whatsAppMensajePublicidad.UsuarioModificacion = "Operaciones";
                        //_repWhatsAppMensajePublicidad.Insert(whatsAppMensajePublicidad);
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

        private void RemplazarEtiquetas(ref List<WhatsAppResultadoConjuntoListaDTO> NumeroAlumno)
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
                    var Asesor = _repPersonal.ObtenerDatoPersonal(AlumnoEtiqueta.IdPersonal.Value);



                    plantilla = _repPlantillaClaveValor.GetBy(w => w.Estado == true && w.IdPlantilla == AlumnoEtiqueta.IdPlantilla && w.Clave == "Texto", w => new { w.Valor }).FirstOrDefault().Valor;

                    PlantillaCentroCostoDTO rpta = new PlantillaCentroCostoDTO();
                    ModalidadProgramaDTO FechaInicioPrograma = new ModalidadProgramaDTO();
                    List<ModalidadProgramaDTO> fecha = new List<ModalidadProgramaDTO>();

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

                                if ((etiqueta == "tPersonal.email" || etiqueta == "tPersonal.PrimerNombreApellidoPaterno" || etiqueta == "tPersonal.nombres" || etiqueta == "tPersonal.apellidos" || etiqueta == "tPersonal.UrlFirmaCorreos" || etiqueta == "tPersonal.Telefono" || etiqueta == "tAlumnos.apepaterno" || etiqueta == "tAlumnos.apematerno" || etiqueta == "tAlumnos.nombre1" || etiqueta == "tAlumnos.nombre2"))
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
        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var IdPlantilla = MensajeAlumno.FirstOrDefault().IdPlantilla.Value;
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
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                    //personal debe tener accesos
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(AlumnoMensaje.IdPersonal.Value, AlumnoMensaje.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(AlumnoMensaje.IdPersonal.Value);

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

                        //var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);

                        //WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        //mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        //mensajeEnviado.EnviadoCorrectamente = true;
                        //mensajeEnviado.MensajeError = "";
                        //mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        //mensajeEnviado.Mensaje = DTO.WaCaption;
                        //mensajeEnviado.WhatsAppId = datoRespuesta.messages[0].id;
                        //mensajeEnviado.Estado = true;
                        //mensajeEnviado.FechaCreacion = DateTime.Now;
                        //mensajeEnviado.FechaModificacion = DateTime.Now;
                        //mensajeEnviado.UsuarioCreacion = "Envio";
                        //mensajeEnviado.UsuarioModificacion = "Envio";

                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);

                        //return Ok(mensajeEnviado.WaId);
                    }
                    else
                    {
                        //WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                        //mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                        //mensajeEnviado.EnviadoCorrectamente = false;
                        //mensajeEnviado.MensajeError = "Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.";
                        //mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                        //mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                        //mensajeEnviado.Estado = true;
                        //mensajeEnviado.FechaCreacion = DateTime.Now;
                        //mensajeEnviado.FechaModificacion = DateTime.Now;
                        //mensajeEnviado.UsuarioCreacion = "Envio";
                        //mensajeEnviado.UsuarioModificacion = "Envio";
                        //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                        //return BadRequest("Error en credenciales de login o nrevise su conexcion de red para el servidor de whatsapp.");
                    }
                }
                catch (Exception ex)
                {
                    //WhatsAppConfiguracionEnvioDetalleBO mensajeEnviado = new WhatsAppConfiguracionEnvioDetalleBO();

                    //mensajeEnviado.IdWhatsAppConfiguracionLogEjecucion = IdWhatsAppConfiguracionLogEjecucion;
                    //mensajeEnviado.EnviadoCorrectamente = false;
                    //mensajeEnviado.MensajeError = ex.ToString();
                    //mensajeEnviado.IdConjuntoListaResultado = AlumnoMensaje.IdConjuntoListaResultado;
                    //mensajeEnviado.ConjuntoListaNroEjecucion = AlumnoMensaje.NroEjecucion;
                    //mensajeEnviado.Estado = true;
                    //mensajeEnviado.FechaCreacion = DateTime.Now;
                    //mensajeEnviado.FechaModificacion = DateTime.Now;
                    //mensajeEnviado.UsuarioCreacion = "Envio";
                    //mensajeEnviado.UsuarioModificacion = "Envio";
                    //_repWhatsAppConfiguracionEnvioDetalle.Insert(mensajeEnviado);
                    throw ex;
                }

                System.Threading.Thread.Sleep(5000);
            }

        }

        /// <summary>
        /// En base a un conjuto lista resultado enviamos el correo
        /// </summary>
        /// <param name="IdConjuntoListaResultado"></param>
        /// <returns></returns>
        [Route("[action]/{IdConjuntoListaResultado}")]
        [HttpGet]
        public ActionResult EnvioAutomatico(int IdConjuntoListaResultado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repConfiguracionEnvioMailingDetalle.Exist(x => x.IdConjuntoListaResultado == IdConjuntoListaResultado))
                {
                    return BadRequest("No existe el conjunto lista resultado");
                }
                var configuracionEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.FirstBy(x => x.IdConjuntoListaResultado == IdConjuntoListaResultado);

                //envio correo
                var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(configuracionEnvioMailingDetalle.IdConjuntoListaResultado);
                var oportunidad = _repOportunidad.FirstById(conjuntoListaResultado.IdOportunidad.Value);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (listaArchivosAdjunto.Any(configuracionEnvioMailingDetalle.CuerpoHtml.Contains))
                {
                    if (configuracionEnvioMailingDetalle.CuerpoHtml.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))),
                            Name = "Manual para ingreso al Aula Virtual.pdf",
                            Type = "application/pdf"
                        });
                        configuracionEnvioMailingDetalle.CuerpoHtml = configuracionEnvioMailingDetalle.CuerpoHtml.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (configuracionEnvioMailingDetalle.CuerpoHtml.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualBSPlay}"))),
                            Name = "Manual BS Play.pdf",
                            Type = "application/pdf"
                        });
                        configuracionEnvioMailingDetalle.CuerpoHtml = configuracionEnvioMailingDetalle.CuerpoHtml.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (configuracionEnvioMailingDetalle.CuerpoHtml.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))),
                            Name = "Manual para conectarse a la sesión webinar.pdf",
                            Type = "application/pdf"
                        });
                        configuracionEnvioMailingDetalle.CuerpoHtml = configuracionEnvioMailingDetalle.CuerpoHtml.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (configuracionEnvioMailingDetalle.CuerpoHtml.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))),
                            Name = "Manual para conectarse a la sesión virtual.pdf",
                            Type = "application/pdf"
                        });
                        configuracionEnvioMailingDetalle.CuerpoHtml = configuracionEnvioMailingDetalle.CuerpoHtml.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }

                List<string> correosPersonalizados = new List<string>
                {
                    "lhuallpa@bsginstitute.com",
                    alumno.Email1
                };

                TMK_MailServiceImpl MailservicePersonalizado = new TMK_MailServiceImpl();
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    //Sender = "w.choque.itusaca@isur.edu.pe",
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = configuracionEnvioMailingDetalle.Asunto,
                    Message = configuracionEnvioMailingDetalle.CuerpoHtml,
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = listaArchivosAdjuntos
                };
                MailservicePersonalizado.SetData(mailDataPersonalizado);
                MailservicePersonalizado.SendMessageTask();
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 28/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista preparada para el envio del recordatorio a los cinco minutos para Whatsapp
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y la lista de oportunidades</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaWhatsappNuevasOportunidades()
        {
            try
            {
                var oportunidadesRecientes = _repOportunidad.ObtenerOportunidadesRecientesWhatsapp();

                return Ok(oportunidadesRecientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 28/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista preparada para el envio del ultimo recordatorio en tres dias para Whatsapp
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y la lista de oportunidades</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaWhatsappUltimoRecordatorioNuevasOportunidades()
        {
            try
            {
                var oportunidadesRecientes = _repOportunidad.ObtenerOportunidadesUltimoRecordatorioWhatsapp();

                return Ok(oportunidadesRecientes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 12/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia un numero individual de la oportunidad una plantilla y segun el flag
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="IdPlantilla">Id de la plantilla (PK de la tabla mkt.T_Plantilla)</param>
        /// <param name="FlagSeccion">Flag para determinar el reemplazo que se realizara</param>
        /// <returns>ActionResult con estado 200, 400</returns>
        public ActionResult EnvioWhatsappNumeroIndividual(int IdOportunidad, int IdPlantilla, int FlagSeccion)
        {
            try
            {
                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                var oportunidad = _repOportunidad.FirstById(IdOportunidad);

                if (oportunidad.IdPersonalAsignado == 125 || _repTokenUsuario.CredencialUsuarioLogin(oportunidad.IdPersonalAsignado) == null)
                    return BadRequest("No se enviara mensajes desde el asesor asignacion automatica");

                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);
                alumno.Celular = alumno.Celular.Replace("+", string.Empty);

                switch (alumno.IdPais)
                {
                    case 51:
                        alumno.Celular = "51" + alumno.Celular;
                        break;
                    case 57 when alumno.Celular.Length == 12 && alumno.Celular.StartsWith("57"):
                        alumno.Celular = alumno.Celular;
                        break;
                    case 57:
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                        break;
                    case 591 when alumno.Celular.Length == 11 && alumno.Celular.StartsWith("591"):
                        alumno.Celular = alumno.Celular;
                        break;
                    case 591:
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                        break;
                    default:
                        alumno.Celular = "1";
                        break;
                }

                var destinatarios = alumno.Celular.Split(";");

                if (plantilla.IdPlantillaBase == 8)
                {
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPlantilla = IdPlantilla
                    };

                    if (FlagSeccion == 1) reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                    else reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();

                    var whatsAppCalculado = reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = destinatarios.Select(destinatario => new WhatsAppResultadoConjuntoListaDTO
                    {
                        IdAlumno = alumno.Id,
                        Celular = destinatario,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdCodigoPais = alumno.IdCodigoPais ?? default,
                        IdConjuntoListaResultado = 0,
                        IdPgeneral = null,
                        IdPlantilla = IdPlantilla,
                        NroEjecucion = 1,
                        objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                        Plantilla = whatsAppCalculado.Plantilla,
                        Validado = false
                    }).ToList();

                    ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 28/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Envia los Whatsapp a nuevas oportunidades dependiendo de la lista enviada
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y booleano true o false dependiendo del resultado final</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnvioWhatsappRecordatorioNuevasOportunidades([FromBody] List<OportunidadWhatsappDTO> OportunidadesWhatsapp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var oportunidadesRecientes = _repOportunidad.ObtenerOportunidadesRecientesWhatsapp();
                var plantilla = _repPlantilla.FirstById(ValorEstatico.IdPlantillaEnvioCorreoInformacion);

                if (OportunidadesWhatsapp.Any())
                {
                    foreach (var oportunidad in OportunidadesWhatsapp)
                    {
                        /*125: Asignacion Automatica*/
                        if (oportunidad.IdPersonal == 125 || _repTokenUsuario.CredencialUsuarioLogin(oportunidad.IdPersonal) == null)
                            continue;

                        var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);
                        alumno.Celular = alumno.Celular.Replace("+", string.Empty);

                        switch (alumno.IdPais)
                        {
                            case 51:
                                alumno.Celular = "51" + alumno.Celular;
                                break;
                            case 57 when alumno.Celular.Length == 12 && alumno.Celular.StartsWith("57"):
                                alumno.Celular = alumno.Celular;
                                break;
                            case 57:
                                alumno.Celular = alumno.Celular.Remove(0, 2);
                                break;
                            case 591 when alumno.Celular.Length == 11 && alumno.Celular.StartsWith("591"):
                                alumno.Celular = alumno.Celular;
                                break;
                            case 591:
                                alumno.Celular = alumno.Celular.Remove(0, 2);
                                break;
                            default:
                                alumno.Celular = "1";
                                break;
                        }

                        if (alumno.IdPais == 51)
                            alumno.Celular += ";51935791397";

                        var destinatarios = alumno.Celular.Split(";");

                        if (plantilla.IdPlantillaBase == 8)
                        {
                            var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            {
                                IdOportunidad = oportunidad.Id,
                                IdPlantilla = ValorEstatico.IdPlantillaEnvioCorreoInformacion
                            };

                            reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();

                            var whatsAppCalculado = reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                            var listaWhatsappConjuntoListaResultado = destinatarios.Select(destinatario => new WhatsAppResultadoConjuntoListaDTO
                            {
                                IdAlumno = alumno.Id,
                                Celular = destinatario,
                                IdPersonal = oportunidad.IdPersonal/*ValorEstatico.IdPersonalWhatsappNuevasOportunidades*/,
                                IdCodigoPais = alumno.IdCodigoPais ?? default,
                                IdConjuntoListaResultado = 0,
                                IdPgeneral = null,
                                IdPlantilla = ValorEstatico.IdPlantillaEnvioCorreoInformacion,
                                NroEjecucion = 1,
                                objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                                Plantilla = whatsAppCalculado.Plantilla,
                                Validado = false
                            })
                                .ToList();

                            ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                            listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado).ToList();
                            listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                            EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
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

        [Route("[action]/{IdConjuntoListaResultado}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult CalcularEmail(int IdConjuntoListaResultado, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repConjuntoListaResultado.Exist(IdConjuntoListaResultado))
                {
                    return BadRequest("Conjunto lista resultado no existente");
                }

                var conjuntoListaResultado = _repConjuntoListaResultado.FirstById(IdConjuntoListaResultado);

                var _oportunidad = _repOportunidad.FirstById(conjuntoListaResultado.IdOportunidad.Value);//Almacenaremos el IdAlumno el idOportunidad

                var oportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == _oportunidad.Id);

                //var _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

                //if (!_repMatriculaCabecera.Exist(x => x.CodigoMatricula == CodigoAlumno))
                //{
                //    return BadRequest("Codigo de alumno no valido!");
                //}

                var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

                //var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var matriculaCabecera = _repMatriculaCabecera.FirstById(oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
                {
                    return BadRequest(ModelState);
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return BadRequest(ModelState);
                }

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    return BadRequest(ModelState);
                }
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    return BadRequest(ModelState);
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                //var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => string.Concat("{", o.Split(new string[] { "}" }, StringSplitOptions.None).First(), "}").ToList());
                //var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First().ToList());
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                if (plantillaBase.Cuerpo.Contains("{tPersonal.Nombre1}"))
                {
                    var valor = personal.PrimerNombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }
                if (plantillaBase.Cuerpo.Contains("{tPersonal.PrimerNombreApellidoPaterno}"))
                {
                    var valor = personal.PrimerNombreApellidoPaterno;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.PrimerNombreApellidoPaterno}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.PrimerNombreApellidoPaterno}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaAutoEvaluacionCompleto(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaAutoEvaluacionCompleto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidas}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidas}", _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesVencidas}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesCompletas}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesCompletas}", _repMatriculaCabecera.ObtenerAutoEvaluacionesCompletas(matriculaCabecera.Id));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesCompletas}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesCompletas(matriculaCabecera.Id) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}", _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesPendientes(matriculaCabecera.Id).ToString());
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.CantidadAutoEvaluacionesPendientes}", texto = _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesPendientes(matriculaCabecera.Id).ToString() });
                    }
                }

                //nuevos solicitadas por Pilar
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -1, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}", _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesVencidasHace3Dias}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}", _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesVencidasHace7Dias}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}", _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesVencerProximos3Dias}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}", _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.AutoEvaluacionesVencerHoy}", texto = _repMatriculaCabecera.ObtenerAutoEvaluacionesVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}", _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesVencidas(matriculaCabecera.Id).ToString());
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.CantidadAutoEvaluacionesVencidas}", texto = _repMatriculaCabecera.ObtenerCantidadAutoEvaluacionesVencidas(matriculaCabecera.Id).ToString() });
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.PeriodoDuracion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.PeriodoDuracion}", _repPEspecifico.ObtenerPeriodoDuracion(matriculaCabecera.IdPespecifico, matriculaCabecera.Id));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.PeriodoDuracion}", texto = _repPEspecifico.ObtenerPeriodoDuracion(matriculaCabecera.IdPespecifico, matriculaCabecera.Id) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.UrlAccesoSesionOnline}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.UrlAccesoSesionOnline}", _repPEspecifico.ObtenerUrlAccesoSesionOnline(matriculaCabecera.IdPespecifico));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.UrlAccesoSesionOnline}", texto = _repPEspecifico.ObtenerUrlAccesoSesionOnline(matriculaCabecera.IdPespecifico) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.BeneficiosVersion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.BeneficiosVersion}", _repPgeneral.ObtenerBeneficiosVersion(matriculaCabecera.Id));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.BeneficiosVersion}", texto = _repPgeneral.ObtenerBeneficiosVersion(matriculaCabecera.Id) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ConjuntoSesion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ConjuntoSesion}", _repPEspecifico.ObtenerConjuntoSesion(matriculaCabecera.IdPespecifico));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.ConjuntoSesion}", texto = _repPEspecifico.ObtenerConjuntoSesion(matriculaCabecera.IdPespecifico) });
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.ProximoConjuntoSesion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecifico.ProximoConjuntoSesion}", _repPEspecifico.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 0));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecifico.ProximoConjuntoSesion}", texto = _repPEspecifico.ObtenerProximoConjuntoSesion(matriculaCabecera.IdPespecifico, 0) });
                    }
                }

                ///Cronograma pago completo

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompleto}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCronogramaPagoCompleto(matriculaCabecera.Id, FormatoHTMLMostrar.Lista);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompleto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CronogramaPagoCompleto}")).FirstOrDefault().texto = valor;
                    }
                }




                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasPendientes}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadCuotasPendientes}", _repMatriculaCabecera.ObtenerCantidadCuotasPendientes(matriculaCabecera.Id).ToString());
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.CantidadCuotasPendientes}", texto = _repMatriculaCabecera.ObtenerCantidadCuotasPendientes(matriculaCabecera.Id).ToString() });
                    }
                }

                //solicitados por pilar
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace1Dia}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -1, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace1Dia}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace1Dia}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -3, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencidasHace7Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, -7, false, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencidasHace7Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencidasHace7Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerHoy}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, 0, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencerHoy}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencerHoy}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CuotasVencerProximos3Dias}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCuotasVencidas(matriculaCabecera.Id, 3, true, plantilla.IdPlantillaBase);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CuotasVencerProximos3Dias}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CuotasVencerProximos3Dias}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CantidadCuotasVencidas}"))
                {
                    var valor = _repMatriculaCabecera.ObtenerCantidadCuotasVencidas(matriculaCabecera.Id).ToString();
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CantidadCuotasVencidas}", valor);

                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_MatriculaCabecera.CantidadCuotasVencidas}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_MatriculaCabecera.CodigoMatricula}", texto = matriculaCabecera.CodigoMatricula });
                    }
                }

                //reemplazar nombre 1 alumno

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = alumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tAlumnos.nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombrePais}", alumno.NombrePaisOrigen);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tAlumnos.NombrePais}", texto = alumno.NombrePaisOrigen });
                    }
                }

                //nombre programa general
                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = detalleMatriculaCabecera.NombreProgramaGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPLA_PGeneral.Nombre}")).FirstOrDefault().texto = valor;
                    }
                }

                //nombre centro de costo
                if (plantillaBase.Cuerpo.Contains("{T_CentroCosto.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_CentroCosto.Nombre}", detalleMatriculaCabecera.NombreCentroCosto);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_CentroCosto.Nombre}", texto = detalleMatriculaCabecera.NombreCentroCosto });
                    }
                }

                //horario atencion personal
                if (plantillaBase.Cuerpo.Contains("{T_Personal.HorarioAtencion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.HorarioAtencion}", _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Personal.HorarioAtencion}", texto = _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado) });
                    }
                }

                //anexo personal
                if (plantillaBase.Cuerpo.Contains("{tPersonal.Anexo}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPersonal.Anexo}", personal.Anexo);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPersonal.Anexo}", texto = personal.Anexo });
                    }
                }

                //Telefono personal
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Telefono}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Telefono}", personal.Telefono);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Personal.Telefono}", texto = personal.Telefono });
                    }
                }

                //reemplazar nombre 1 alumno
                if (plantillaBase.Asunto.Contains("{tAlumnos.nombre1}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tAlumnos.nombre1}", alumno.Nombre1);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tAlumnos.nombre1}", texto = alumno.Nombre1 });
                    }
                }

                //nombre programa general
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", detalleMatriculaCabecera.NombreProgramaGeneral);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPLA_PGeneral.Nombre}", texto = detalleMatriculaCabecera.NombreProgramaGeneral });
                    }
                }

                //nombre centro de costo
                if (plantillaBase.Asunto.Contains("{T_CentroCosto.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_CentroCosto.Nombre}", detalleMatriculaCabecera.NombreCentroCosto);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_CentroCosto.Nombre}", texto = detalleMatriculaCabecera.NombreCentroCosto });
                    }
                }

                //numero whatsapp por pais alumno
                if (plantillaBase.Asunto.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Alumno.NroWhatsAppCoordinador}", alumno.NroWhatsAppCoordinador);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Alumno.NroWhatsAppCoordinador}", texto = alumno.NroWhatsAppCoordinador });
                    }
                }

                //horario atencion personal
                if (plantillaBase.Asunto.Contains("{T_Personal.HorarioAtencion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Personal.HorarioAtencion}", _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Personal.HorarioAtencion}", texto = _repPersonal.ObtenerHorarioTrabajo(oportunidad.IdPersonalAsignado) });
                    }
                }

                //anexo personal
                if (plantillaBase.Asunto.Contains("{tPersonal.Anexo}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPersonal.Anexo}", _repPersonal.FirstById(oportunidad.IdPersonalAsignado).Anexo);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{tPersonal.Anexo}", texto = _repPersonal.FirstById(oportunidad.IdPersonalAsignado).Anexo });
                    }
                }

                //logica documentos adjuntos
                var listaImagenes = new List<Image>();


                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoAndroid%7D", "{Link.UrlDescargarAplicativoAndroid}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlDescargarAplicativoIOS%7D", "{Link.UrlDescargarAplicativoIOS}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlGuiaAccederSesionWebinarPorVideo%7D", "{Link.UrlGuiaAccederSesionWebinarPorVideo}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlAulaVirtual%7D", "{Link.UrlAulaVirtual}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlManualBSPlay%7D", "{Link.UrlManualBSPlay}");
                plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("https://integrav4.bsginstitute.com/Marketing/Plantilla/%7BLink.UrlPortalWeb%7D", "{Link.UrlPortalWeb}");

                var listaTextoPlano = new List<string>()
                {
                    "{Link.UrlAulaVirtual}",
                    "{Link.UrlDescargarAplicativoAndroid}",
                    "{Link.UrlDescargarAplicationIOS}",
                    "{Link.UrlGuiaAccederSesionWebinarPorVideo}",
                    "{Link.UrlImagenFelizCumpleanos}",
                    "{Link.UrlManualBSPlay}",
                    "{Link.UrlPortalWeb}"
                };
                //Etiquetas texto plano
                if (listaTextoPlano.Any(plantillaBase.Cuerpo.Contains))
                {
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlPortalWeb}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlPortalWeb}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlManualBSPlay}"))
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlManualBSPlay}", ValorEstaticoUtil.Get("{Link.UrlManualBSPlay}"));
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlAulaVirtual}"))
                    {
                        var valor = ValorEstaticoUtil.Get("{Link.UrlAulaVirtual}");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{Link.UrlAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{Link.UrlDescargarAplicativoAndroid}"))
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlDescargarAplicativoAndroid}", ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoAndroid}"));
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlDescargarAplicativoIOS}"))
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlDescargarAplicativoIOS}", ValorEstaticoUtil.Get("{Link.UrlDescargarAplicativoIOS}"));
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlGuiaAccederSesionWebinarPorVideo}"))
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlGuiaAccederSesionWebinarPorVideo}", ValorEstaticoUtil.Get("{Link.UrlGuiaAccederSesionWebinarPorVideo}"));
                    }
                    if (plantillaBase.Cuerpo.Contains("{Link.UrlImagenFelizCumpleanios}"))
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{Link.UrlImagenFelizCumpleanios}", ValorEstaticoUtil.Get("{Link.UrlImagenFelizCumpleanios}"));
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.FormaPago}"))
                {
                    var detallePEspecifico = _repPEspecifico.ObtenerDetalle(matriculaCabecera.Id);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.FormaPago}", alumno.ObtenerFormaPago(detallePEspecifico.IdModalidadCurso, detallePEspecifico.IdCiudad, matriculaCabecera.CodigoMatricula, detallePEspecifico.MonedaCronograma));
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Alumno.FormaPago}", texto = alumno.ObtenerFormaPago(detallePEspecifico.IdModalidadCurso, detallePEspecifico.IdCiudad, matriculaCabecera.CodigoMatricula, detallePEspecifico.MonedaCronograma) });
                    }
                }

                var listaAccesoAulaVirtual = new List<string>()
                {
                    "{T_Alumno.UsuarioAulaVirtual}",
                    "{T_Alumno.ClaveAulaVirtual}"
                };
                if (listaAccesoAulaVirtual.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoAulaVirtual = _repMatriculaCabecera.ObtenerDetalleAccesoAulaVirtual(matriculaCabecera.Id);
                    //acceso aula virtual
                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioAulaVirtual}"))
                    {
                        var valor = accesoAulaVirtual.UsuarioMoodle;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.UsuarioAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }
                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClaveAulaVirtual}"))
                    {
                        var valor = accesoAulaVirtual.ClaveMoodle;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClaveAulaVirtual}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.ClaveAulaVirtual}")).FirstOrDefault().texto = valor;
                        }
                    }
                }

                var listaAccesoPortalWeb = new List<string>()
                {
                    "{T_Alumno.UsuarioPortalWeb}",
                    "{T_Alumno.ClavePortalWeb}"
                };
                if (listaAccesoPortalWeb.Any(plantillaBase.Cuerpo.Contains))
                {
                    var accesoPortalWeb = _repMatriculaCabecera.ObtenerDetalleAccesoPortalWeb(matriculaCabecera.Id);

                    //Acceso PW
                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.UsuarioPortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Usuario;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.UsuarioPortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.UsuarioPortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_Alumno.ClavePortalWeb}"))
                    {
                        var valor = accesoPortalWeb.Clave;
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.ClavePortalWeb}", valor);
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Alumno.ClavePortalWeb}")).FirstOrDefault().texto = valor;
                        }
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_Personal.FirmaCorreo}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.FirmaCorreo}", personal.FirmaHtml);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Personal.FirmaCorreo}", texto = personal.FirmaHtml });
                    }
                    //plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.FirmaCorreo}", personal.FirmaCorreoHTML);
                }

                //Presencial


                //calcular ultima sesion
                var listaPEspecificoSesion = new List<string>()
                {
                    "{T_PEspecificoSesion.UrlUbicacionCiudad}",
                    "{T_PEspecificoSesion.DireccionDictadoClases}",
                    "{T_PEspecificoSesion.NombreCiudadDictadoClases}"
                };
                if (listaPEspecificoSesion.Any(plantillaBase.Cuerpo.Contains))
                {
                    var idPEspecificoSesion = _repMatriculaCabecera.ObtenerProximaSesion(matriculaCabecera.Id, 0);
                    //se calcula en base a una sesion
                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.UrlUbicacionCiudad}"))
                    {
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.UrlUbicacionCiudad}", _repPEspecificoSesion.ObtenerUrlUbicacionCiudad(idPEspecificoSesion));
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.UrlUbicacionCiudad}", texto = _repPEspecificoSesion.ObtenerUrlUbicacionCiudad(idPEspecificoSesion) });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.DireccionDictadoClases}"))
                    {
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.DireccionDictadoClases}", _repPEspecificoSesion.ObtenerDireccionDictadoClases(idPEspecificoSesion));
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.DireccionDictadoClases}", texto = _repPEspecificoSesion.ObtenerDireccionDictadoClases(idPEspecificoSesion) });
                        }
                    }

                    if (plantillaBase.Cuerpo.Contains("{T_PEspecificoSesion.NombreCiudadDictadoClases}"))
                    {
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_PEspecificoSesion.NombreCiudadDictadoClases}", _repPEspecificoSesion.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion));
                        }
                        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                        {
                            listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_PEspecificoSesion.NombreCiudadDictadoClases}", texto = _repPEspecificoSesion.ObtenerNombreCiudadDictadoClases(idPEspecificoSesion) });
                        }
                    }
                }

                //reemplaza en la etiqueta de firma tambien
                //numero whatsapp por pais alumno
                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NroWhatsAppCoordinador}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NroWhatsAppCoordinador}", alumno.NroWhatsAppCoordinador);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = "{T_Alumno.NroWhatsAppCoordinador}", texto = personal.FirmaHtml });
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_PEspecifico.CalendarioSemanal}"))
                {
                    var listaPrueba = new List<PEspecificoSesionDetalleDTO>();
                    listaPrueba.Add(new PEspecificoSesionDetalleDTO()
                    {
                        Id = 123,
                        IdPEspecifico = 1234,
                        FechaHoraInicio = DateTime.Now,
                        FechaHoraFin = DateTime.Now,
                        NombrePEspecifico = "PESPECIFICO 1",
                        NombreCurso = "Nombre curso 1"
                    });
                    listaPrueba.Add(new PEspecificoSesionDetalleDTO()
                    {
                        Id = 1234,
                        IdPEspecifico = 12345,
                        FechaHoraInicio = DateTime.Now,
                        FechaHoraFin = DateTime.Now,
                        NombrePEspecifico = "PESPECIFICO 2",
                        NombreCurso = "Nombre curso 2"
                    });
                    listaPrueba.Add(new PEspecificoSesionDetalleDTO()
                    {
                        Id = 12345,
                        IdPEspecifico = 123456,
                        FechaHoraInicio = DateTime.Now,
                        FechaHoraFin = DateTime.Now,
                        NombrePEspecifico = "PESPECIFICO 3",
                        NombreCurso = "Nombre curso 3"
                    });

                }

                var email = new EmailMandrillDTO()
                {
                    Asunto = plantillaBase.Asunto,
                    CuerpoHTML = plantillaBase.Cuerpo,
                    IdConjuntoListaResultado = IdConjuntoListaResultado,
                    EtiquetaArchivoAdjunto = string.Join(",", listaEtiqueta.Select(x => x.Contains("{ArchivoAdjunto.")))
                };

                return Ok(email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{CantidadDias}/{CantidadDatos}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaCumplenCriterioAutoEvaluacionesVencidas(int CantidadDias, int CantidadDatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaOportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.GetBy(x => x.Estado == true, x => new { x.IdMatriculaCabecera }).ToList();

                var listaMatriculaCabecera = new List<int>();
                var listaCodigoMatricula = new List<string>();

                var continuarEjecutando = true;
                foreach (var item in listaOportunidadClasificacionOperaciones)
                {
                    if (!continuarEjecutando)
                    {
                        break;
                    }
                    if (listaMatriculaCabecera.Count() >= CantidadDatos && continuarEjecutando)
                    {
                        continuarEjecutando = false;
                    }

                    var cumpleCriterios = _repMatriculaCabecera.CumpleCriteriosAutoEvaluacionVencida(item.IdMatriculaCabecera, CantidadDias);

                    if (cumpleCriterios)
                    {
                        listaMatriculaCabecera.Add(item.IdMatriculaCabecera);
                    }
                }

                foreach (var item in listaMatriculaCabecera)
                {
                    var matriculaCabecera = _repMatriculaCabecera.FirstById(item);
                    listaCodigoMatricula.Add(matriculaCabecera.CodigoMatricula);
                }
                return Ok(listaCodigoMatricula);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[action]/{CantidadDias}/{CantidadDatos}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaCumplenCriterioCuotasVencidas(int CantidadDias, int CantidadDatos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaOportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.GetBy(x => x.Estado == true, x => new { x.IdMatriculaCabecera }).ToList();

                var listaMatriculaCabecera = new List<int>();
                var listaCodigoMatricula = new List<string>();

                var continuarEjecutando = true;
                foreach (var item in listaOportunidadClasificacionOperaciones)
                {
                    if (!continuarEjecutando)
                    {
                        break;
                    }
                    if (listaMatriculaCabecera.Count() >= CantidadDatos && continuarEjecutando)
                    {
                        continuarEjecutando = false;
                    }

                    var cumpleCriterios = _repMatriculaCabecera.CumpleCriteriosCuotaVencida(item.IdMatriculaCabecera, CantidadDias);

                    if (cumpleCriterios)
                    {
                        listaMatriculaCabecera.Add(item.IdMatriculaCabecera);
                    }
                }

                foreach (var item in listaMatriculaCabecera)
                {
                    var matriculaCabecera = _repMatriculaCabecera.FirstById(item);
                    listaCodigoMatricula.Add(matriculaCabecera.CodigoMatricula);
                }
                return Ok(listaCodigoMatricula);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


    }
}

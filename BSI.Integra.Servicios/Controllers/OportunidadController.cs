using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using System.Net;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Base.BO;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using System.Threading;
using BSI.Integra.Aplicacion.Transversal.Scode.DTO;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Comercial/Oportunidad
    /// Autor: Carlos Crispin
    /// Fecha: 03/02/2021
    /// <summary>
    /// Gestiona todo lo refernte a la oportunidad , tanto como su crud y sp o vista que usen la tabla com.t_oportunidad
    /// </summary>

    [Route("api/Oportunidad")]
    public class OportunidadController : Controller //BaseController<TOportunidad, ValidadorOportunidadDTO>
    {

        private readonly integraDBContext _integraDBContext;


        private OportunidadRepositorio _repOportunidad;
        //private readonly integraDBContext _integraDBContext;
        private readonly integraDBContext _integraDBContext2;

        private AsignacionAutomaticaRepositorio _repAsignacionAutomatica;
        private AsignacionAutomaticaErrorRepositorio _repAsignacionAutomaticaError;
        private BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad;
        private AlumnoRepositorio _repAlumno;
        private ExpositorRepositorio _repExpositor;
        private CategoriaOrigenRepositorio _repCategoriaOrigen;

        private PespecificoRepositorio _repPespecifico;
        private ModeloDataMiningRepositorio _repModeloDataMining;
        private ModeloPredictivoProbabilidadRepositorio _repModeloPredictivoProbabilidad;
        private ProbabilidadRegistroPwRepositorio _repProbabilidadRegistroPw;
        private OportunidadLogRepositorio _repOportunidadLog;
        private AsignacionOportunidadRepositorio _repAsignacionOportunidad;
        private AsignacionOportunidadLogRepositorio _repAsignacionOportunidadLog;
        private ActividadDetalleRepositorio _repActividadDetalle;
        private OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda;
        private ProgramaGeneralPuntoCorteRepositorio _repProgramaGeneralPuntoCorte;
        private ConfiguracionDatoRemarketingRepositorio _repConfiguracionDatoRemarketing;
        private LogRepositorio _repLog;

        public OportunidadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            //_integraDBContext = new integraDBContext();
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);

            _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
            _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio(_integraDBContext);
            _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repExpositor = new ExpositorRepositorio(_integraDBContext);
            _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repModeloDataMining = new ModeloDataMiningRepositorio(_integraDBContext);
            _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
            _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
            _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
            _repAsignacionOportunidadLog = new AsignacionOportunidadLogRepositorio(_integraDBContext);
            _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
            _repModeloPredictivoProbabilidad = new ModeloPredictivoProbabilidadRepositorio(_integraDBContext);
            _repProgramaGeneralPuntoCorte = new ProgramaGeneralPuntoCorteRepositorio(_integraDBContext);
            _repConfiguracionDatoRemarketing = new ConfiguracionDatoRemarketingRepositorio(_integraDBContext);
            _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);
            _repLog = new LogRepositorio(_integraDBContext);
        }
        //public OportunidadController(IIntegraRepository<TOportunidad> repositorio, ILogger<BaseController<TOportunidad, ValidadorOportunidadDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        //{
        //    _integraDBContext = new integraDBContext();
        //    _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
        //    _repOportunidad = new OportunidadRepositorio(_integraDBContext);

        //    _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
        //    _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio(_integraDBContext);
        //    _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio(_integraDBContext);
        //    _repAlumno = new AlumnoRepositorio(_integraDBContext);
        //    _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
        //    _repPespecifico = new PespecificoRepositorio(_integraDBContext);
        //    _repModeloDataMining = new ModeloDataMiningRepositorio(_integraDBContext);
        //    _repProbabilidadRegistroPw = new ProbabilidadRegistroPwRepositorio(_integraDBContext);
        //    _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
        //    _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
        //    _repAsignacionOportunidadLog = new AsignacionOportunidadLogRepositorio(_integraDBContext);
        //    _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
        //}


        [Route("[action]/{DNI}")]
        [HttpGet]
        public ActionResult ObtenerDatos(string DNI)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PersonalRepositorio _repPersonal = new PersonalRepositorio();

                var datosOperaciones = _repPersonal.GetDatosOperaciones(DNI);

                return Ok(datosOperaciones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra las oportunidades enviadas a OD
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Response 200</returns>
        [Route("[Action]/{idOportunidad}")]
        [HttpPost]
        public ActionResult CerrarOportunidadOD(int idOportunidad)
        {
            int[] listaOportunidadesCategorias = new int[] { idOportunidad };
            this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
            return Ok();
        }
        [Route("[Action]/{idOportunidad}")]
        [HttpPost]
        public ActionResult CerrarOportunidadOM(int idOportunidad)
        {
            int[] listaOportunidadesCategorias = new int[] { idOportunidad };
            this.CerrarOportunidadesOM(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
            return Ok();
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividadOperaciones([FromBody] FinalizarActividadDTO JsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO(JsonDTO.ActividadAntigua.IdOportunidad.Value, JsonDTO.Usuario, _integraDBContext);

                Oportunidad.IdFaseOportunidadIp = JsonDTO.Oportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = JsonDTO.Oportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = JsonDTO.Oportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = JsonDTO.Oportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = JsonDTO.Oportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = JsonDTO.Oportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = JsonDTO.Oportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = JsonDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = JsonDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = JsonDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = JsonDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = JsonDTO.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = JsonDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = JsonDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = JsonDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = JsonDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = JsonDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = JsonDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = JsonDTO.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(JsonDTO.ActividadAntigua.Id);

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                    FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                    OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                    PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                    LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);

                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividad(false, JsonDTO.Oportunidad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            _repOportunidad.Update(Oportunidad);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ////Insertar actualizar oportunidad - hijos a v3
                    //try
                    //{
                    //    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id.ToString();
                    //    using (WebClient wc = new WebClient())
                    //    {
                    //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    //        wc.DownloadString(URI);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    List<string> correos = new List<string>();
                    //    correos.Add("sistemas@bsginstitute.com");

                    //    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    //    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    //    mailData.Sender = "jcayo@bsginstitute.com";
                    //    mailData.Recipient = string.Join(",", correos);
                    //    mailData.Subject = "Error FinalizarActividad Sincronizacion";
                    //    mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    //    mailData.Cc = "";
                    //    mailData.Bcc = "";
                    //    mailData.AttachedFiles = null;

                    //    Mailservice.SetData(mailData);
                    //    Mailservice.SendMessageTask();
                    //}

                    var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(Oportunidad.ActividadNueva.Id);
                    return Ok(new { realizadas = realizadas, IdOportunidad = Oportunidad.Id });
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error FinalizarActividad General";
                mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + Ex.Message + " <br/> Mensaje toString <br/> " + Ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividad([FromBody] FinalizarActividadDTO JsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO(JsonDTO.ActividadAntigua.IdOportunidad.Value, JsonDTO.Usuario, _integraDBContext);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(JsonDTO.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == JsonDTO.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == JsonDTO.Oportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {JsonDTO.ActividadAntigua.IdOportunidad}" });

                Oportunidad.IdFaseOportunidadIp = JsonDTO.Oportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = JsonDTO.Oportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = JsonDTO.Oportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = JsonDTO.Oportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = JsonDTO.Oportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = JsonDTO.Oportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = JsonDTO.Oportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = JsonDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = JsonDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = JsonDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = JsonDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = JsonDTO.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = JsonDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = JsonDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = JsonDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = JsonDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = JsonDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = JsonDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = JsonDTO.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(JsonDTO.ActividadAntigua.Id);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (JsonDTO.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO();
                    OportunidadCompetidor.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.IdOportunidad;
                    OportunidadCompetidor.OtroBeneficio = JsonDTO.DatosCompuesto.OportunidadCompetidor.OtroBeneficio;
                    OportunidadCompetidor.Respuesta = JsonDTO.DatosCompuesto.OportunidadCompetidor.Respuesta;
                    OportunidadCompetidor.Completado = JsonDTO.DatosCompuesto.OportunidadCompetidor.Completado;
                    OportunidadCompetidor.FechaCreacion = DateTime.Now;
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    OportunidadCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    OportunidadCompetidor.Estado = true;
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(JsonDTO.DatosCompuesto.OportunidadCompetidor.Id);
                }

                CalidadProcesamientoBO CalidadBO = new CalidadProcesamientoBO();
                CalidadBO.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad;
                CalidadBO.PerfilCamposLlenos = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos;
                CalidadBO.PerfilCamposTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal;
                CalidadBO.Dni = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni;
                CalidadBO.PgeneralValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados;
                CalidadBO.PgeneralTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal;
                CalidadBO.PespecificoValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados;
                CalidadBO.PespecificoTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal;
                CalidadBO.BeneficiosValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados;
                CalidadBO.BeneficiosTotales = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales;
                CalidadBO.CompetidoresVerificacion = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion;
                CalidadBO.ProblemaSeleccionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados;
                CalidadBO.ProblemaSolucionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados;
                CalidadBO.FechaCreacion = DateTime.Now;
                CalidadBO.FechaModificacion = DateTime.Now;
                CalidadBO.UsuarioCreacion = JsonDTO.Usuario;
                CalidadBO.UsuarioModificacion = JsonDTO.Usuario;
                CalidadBO.Estado = true;
                if (!CalidadBO.HasErrors)
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                else
                    return BadRequest(CalidadBO.GetErrors(null));
                if (!OportunidadCompetidor.HasErrors)
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                else
                    return BadRequest(OportunidadCompetidor.GetErrors(null));

                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoGeneral)
                {
                    OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                    ListaPrerequisitoGeneral.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoGeneral.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;
                    ListaPrerequisitoGeneral.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioCreacion = JsonDTO.Usuario;
                    ListaPrerequisitoGeneral.UsuarioModificacion = JsonDTO.Usuario;
                    ListaPrerequisitoGeneral.Estado = true;

                    if (!ListaPrerequisitoGeneral.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    else
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));

                }
                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoEspecifico)
                {
                    OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();
                    ListaPrerequisitoEspecifico.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoEspecifico.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioCreacion = JsonDTO.Usuario;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = JsonDTO.Usuario;
                    ListaPrerequisitoEspecifico.Estado = true;

                    if (!ListaPrerequisitoEspecifico.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    else
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                }
                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaBeneficio)
                {
                    OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                    ListaBeneficio.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaBeneficio.IdBeneficio = item.IdBeneficio;
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaCreacion = DateTime.Now;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioCreacion = JsonDTO.Usuario;
                    ListaBeneficio.UsuarioModificacion = JsonDTO.Usuario;
                    ListaBeneficio.Estado = true;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                        return BadRequest(ListaBeneficio.GetErrors(null));
                }
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaCompetidor)
                {
                    DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                    ListaCompetidor.IdOportunidadCompetidor = 0;
                    ListaCompetidor.IdCompetidor = item;
                    ListaCompetidor.Estado = true;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.Estado = true;

                    if (!ListaCompetidor.HasErrors)
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    else
                        return BadRequest(ListaCompetidor.GetErrors(null));
                }
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaSoluciones)
                {
                    SolucionClienteByActividadBO ListaSoluciones = new SolucionClienteByActividadBO();
                    ListaSoluciones.IdOportunidad = item.IdOportunidad;
                    ListaSoluciones.IdActividadDetalle = item.IdActividadDetalle;
                    ListaSoluciones.IdCausa = item.IdCausa;
                    ListaSoluciones.IdPersonal = item.IdPersonal;
                    ListaSoluciones.Solucionado = item.Solucionado;
                    ListaSoluciones.IdProblemaCliente = item.IdProblemaCliente;
                    ListaSoluciones.OtroProblema = item.OtroProblema;
                    ListaSoluciones.FechaCreacion = DateTime.Now;
                    ListaSoluciones.FechaModificacion = DateTime.Now;
                    ListaSoluciones.UsuarioCreacion = JsonDTO.Usuario;
                    ListaSoluciones.UsuarioModificacion = JsonDTO.Usuario;
                    ListaSoluciones.Estado = true;

                    if (!ListaSoluciones.HasErrors)
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    else
                        return BadRequest(ListaSoluciones.GetErrors(null));
                }

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                    FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                    OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                    PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                    LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);

                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividad(false, JsonDTO.Oportunidad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }
                            _repOportunidad.Update(Oportunidad);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ////Insertar actualizar oportunidad - hijos a v3
                    //try
                    //{
                    //    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id.ToString();
                    //    using (WebClient wc = new WebClient())
                    //    {
                    //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    //        wc.DownloadString(URI);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    List<string> correos = new List<string>();
                    //    correos.Add("sistemas@bsginstitute.com");

                    //    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    //    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    //    mailData.Sender = "jcayo@bsginstitute.com";
                    //    mailData.Recipient = string.Join(",", correos);
                    //    mailData.Subject = "Error FinalizarActividad Sincronizacion";
                    //    mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    //    mailData.Cc = "";
                    //    mailData.Bcc = "";
                    //    mailData.AttachedFiles = null;

                    //    Mailservice.SetData(mailData);
                    //    Mailservice.SendMessageTask();
                    //}

                    var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(Oportunidad.ActividadNueva.Id);
                    return Ok(new { realizadas = realizadas, IdOportunidad = Oportunidad.Id });
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error FinalizarActividad General";
                mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + Ex.Message + " <br/> Mensaje toString <br/> " + Ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(Ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 25/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Finalizacion de la actividad alterno
        /// </summary>
        /// <param name="JsonDTO">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividadAlterno([FromBody] FinalizarActividadDTO JsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO(JsonDTO.ActividadAntigua.IdOportunidad.Value, JsonDTO.Usuario, _integraDBContext);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(JsonDTO.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == JsonDTO.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == JsonDTO.Oportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {JsonDTO.ActividadAntigua.IdOportunidad}" });

                Oportunidad.IdFaseOportunidadIp = JsonDTO.Oportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = JsonDTO.Oportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = JsonDTO.Oportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = JsonDTO.Oportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = JsonDTO.Oportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = JsonDTO.Oportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = JsonDTO.Oportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = JsonDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = JsonDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = JsonDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = JsonDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = JsonDTO.ActividadAntigua.DuracionReal;
                //ActividadAntigua.IdOcurrencia = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = JsonDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = JsonDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = JsonDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = JsonDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = JsonDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = JsonDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = JsonDTO.ActividadAntigua.RefLlamada;
                //ActividadAntigua.IdOcurrenciaActividad = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
                ActividadAntigua.IdOcurrenciaAlterno = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdOcurrenciaActividadAlterno = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(JsonDTO.ActividadAntigua.Id);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (JsonDTO.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO();
                    OportunidadCompetidor.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.IdOportunidad;
                    OportunidadCompetidor.OtroBeneficio = JsonDTO.DatosCompuesto.OportunidadCompetidor.OtroBeneficio;
                    OportunidadCompetidor.Respuesta = JsonDTO.DatosCompuesto.OportunidadCompetidor.Respuesta;
                    OportunidadCompetidor.Completado = JsonDTO.DatosCompuesto.OportunidadCompetidor.Completado;
                    OportunidadCompetidor.FechaCreacion = DateTime.Now;
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    OportunidadCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    OportunidadCompetidor.Estado = true;
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(JsonDTO.DatosCompuesto.OportunidadCompetidor.Id);
                }

                CalidadProcesamientoBO CalidadBO = new CalidadProcesamientoBO();
                CalidadBO.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad;
                CalidadBO.PerfilCamposLlenos = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos;
                CalidadBO.PerfilCamposTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal;
                CalidadBO.Dni = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni;
                CalidadBO.PgeneralValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados;
                CalidadBO.PgeneralTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal;
                CalidadBO.PespecificoValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados;
                CalidadBO.PespecificoTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal;
                CalidadBO.BeneficiosValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados;
                CalidadBO.BeneficiosTotales = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales;
                CalidadBO.CompetidoresVerificacion = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion;
                CalidadBO.ProblemaSeleccionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados;
                CalidadBO.ProblemaSolucionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados;
                CalidadBO.FechaCreacion = DateTime.Now;
                CalidadBO.FechaModificacion = DateTime.Now;
                CalidadBO.UsuarioCreacion = JsonDTO.Usuario;
                CalidadBO.UsuarioModificacion = JsonDTO.Usuario;
                CalidadBO.Estado = true;
                if (!CalidadBO.HasErrors)
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                else
                    return BadRequest(CalidadBO.GetErrors(null));
                if (!OportunidadCompetidor.HasErrors)
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                else
                    return BadRequest(OportunidadCompetidor.GetErrors(null));

                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoGeneral)
                {
                    OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                    ListaPrerequisitoGeneral.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoGeneral.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;
                    ListaPrerequisitoGeneral.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioCreacion = JsonDTO.Usuario;
                    ListaPrerequisitoGeneral.UsuarioModificacion = JsonDTO.Usuario;
                    ListaPrerequisitoGeneral.Estado = true;

                    if (!ListaPrerequisitoGeneral.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    else
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));

                }
                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoEspecifico)
                {
                    OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();
                    ListaPrerequisitoEspecifico.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoEspecifico.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioCreacion = JsonDTO.Usuario;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = JsonDTO.Usuario;
                    ListaPrerequisitoEspecifico.Estado = true;

                    if (!ListaPrerequisitoEspecifico.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    else
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                }
                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaBeneficio)
                {
                    OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                    ListaBeneficio.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaBeneficio.IdBeneficio = item.IdBeneficio;
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaCreacion = DateTime.Now;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioCreacion = JsonDTO.Usuario;
                    ListaBeneficio.UsuarioModificacion = JsonDTO.Usuario;
                    ListaBeneficio.Estado = true;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                        return BadRequest(ListaBeneficio.GetErrors(null));
                }

                //======================================
                ProgramaGeneralBeneficioRespuestaRepositorio _repBeneficioAlternoRespuesta = new ProgramaGeneralBeneficioRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioRespuestaBO beneficioAlterno = new ProgramaGeneralBeneficioRespuestaBO();
                var listaBeneficioAlternoAgrupado = JsonDTO.DatosCompuesto.ListaBeneficioAlterno.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAlternoAgrupado)
                {
                    beneficioAlterno = _repBeneficioAlternoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralBeneficio == item.IdBeneficio);

                    if (beneficioAlterno != null)
                    {
                        beneficioAlterno.Respuesta = item.Respuesta;
                        beneficioAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        beneficioAlterno.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Update(beneficioAlterno);
                    }
                    else
                    {
                        ProgramaGeneralBeneficioRespuestaBO beneficioAlternoV2 = new ProgramaGeneralBeneficioRespuestaBO();
                        beneficioAlternoV2.IdOportunidad = item.IdOportunidad;
                        beneficioAlternoV2.IdProgramaGeneralBeneficio = item.IdBeneficio;
                        beneficioAlternoV2.Respuesta = item.Respuesta;
                        beneficioAlternoV2.Estado = true;
                        beneficioAlternoV2.UsuarioCreacion = JsonDTO.Usuario;
                        beneficioAlternoV2.UsuarioModificacion = JsonDTO.Usuario;
                        beneficioAlternoV2.FechaCreacion = DateTime.Now;
                        beneficioAlternoV2.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Insert(beneficioAlternoV2);
                    }
                }

                ProgramaGeneralPrerequisitoRespuestaRepositorio _repPrerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaBO();
                var listaPrerequisitoRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaPrerequisitoGeneralAlterno.GroupBy(x => x.IdProgramaGeneralPrerequisito).Select(x => x.First()).ToList();
                foreach (var item in listaPrerequisitoRespuestaAgrupado)
                {
                    prerequisitoRespuesta = _repPrerequisitoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralPrerequisito);

                    if (prerequisitoRespuesta != null)
                    {
                        prerequisitoRespuesta.Respuesta = item.Respuesta;
                        prerequisitoRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Update(prerequisitoRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuestaAlterno = new ProgramaGeneralPrerequisitoRespuestaBO();
                        prerequisitoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        prerequisitoRespuestaAlterno.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito;
                        prerequisitoRespuestaAlterno.Respuesta = item.Respuesta;
                        prerequisitoRespuestaAlterno.Estado = true;
                        prerequisitoRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        prerequisitoRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        prerequisitoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        prerequisitoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Insert(prerequisitoRespuestaAlterno);
                    }
                }

                //======================================
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaCompetidor)
                {
                    DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                    ListaCompetidor.IdOportunidadCompetidor = 0;
                    ListaCompetidor.IdCompetidor = item;
                    ListaCompetidor.Estado = true;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.Estado = true;

                    if (!ListaCompetidor.HasErrors)
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    else
                        return BadRequest(ListaCompetidor.GetErrors(null));
                }
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaSoluciones)
                {
                    SolucionClienteByActividadBO ListaSoluciones = new SolucionClienteByActividadBO();
                    ListaSoluciones.IdOportunidad = item.IdOportunidad;
                    ListaSoluciones.IdActividadDetalle = item.IdActividadDetalle;
                    ListaSoluciones.IdCausa = item.IdCausa;
                    ListaSoluciones.IdPersonal = item.IdPersonal;
                    ListaSoluciones.Solucionado = item.Solucionado;
                    ListaSoluciones.IdProblemaCliente = item.IdProblemaCliente;
                    ListaSoluciones.OtroProblema = item.OtroProblema;
                    ListaSoluciones.FechaCreacion = DateTime.Now;
                    ListaSoluciones.FechaModificacion = DateTime.Now;
                    ListaSoluciones.UsuarioCreacion = JsonDTO.Usuario;
                    ListaSoluciones.UsuarioModificacion = JsonDTO.Usuario;
                    ListaSoluciones.Estado = true;

                    if (!ListaSoluciones.HasErrors)
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    else
                        return BadRequest(ListaSoluciones.GetErrors(null));
                }

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                    FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                    OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                    PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                    LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);

                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividadAlterno(false, JsonDTO.Oportunidad, JsonDTO.ActividadAntigua.IdOcurrenciaActividad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }
                            _repOportunidad.Update(Oportunidad);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ////Insertar actualizar oportunidad - hijos a v3
                    //try
                    //{
                    //    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id.ToString();
                    //    using (WebClient wc = new WebClient())
                    //    {
                    //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    //        wc.DownloadString(URI);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    List<string> correos = new List<string>();
                    //    correos.Add("sistemas@bsginstitute.com");

                    //    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    //    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    //    mailData.Sender = "jcayo@bsginstitute.com";
                    //    mailData.Recipient = string.Join(",", correos);
                    //    mailData.Subject = "Error FinalizarActividad Sincronizacion";
                    //    mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    //    mailData.Cc = "";
                    //    mailData.Bcc = "";
                    //    mailData.AttachedFiles = null;

                    //    Mailservice.SetData(mailData);
                    //    Mailservice.SendMessageTask();
                    //}

                    var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(Oportunidad.ActividadNueva.Id);
                    return Ok(new { realizadas = realizadas, IdOportunidad = Oportunidad.Id });
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error FinalizarActividad General";
                mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + Ex.Message + " <br/> Mensaje toString <br/> " + Ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{idFaseOportunidadPortal}")]
        [HttpGet]
        public ActionResult ObtenerHistoricoDetallesPorAsesorChatDetalle(string idFaseOportunidadPortal)
        {
            try
            {
                OportunidadBO oportunidad = new OportunidadBO();
                //return Ok(oportunidad.ObtenerOportunidadPorIdFaseOportunidadPortal(idFaseOportunidadPortal));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{idInteraccionChat}")]
        [HttpGet]
        public ActionResult ObtenerTodosValoresOportunidadChat(int idInteraccionChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                OportunidadDatosChatDTO oportunidadValores = null;
                DatosProgramaDTO datosprograma = null;
                var idfaseoportunidadPortal = _repFaseOportunidad.ObtenerFaseOportunidadPorInteraccionId(idInteraccionChat).IdFaseOportunidadPortal;
                int validadofinal = 1;
                if (idfaseoportunidadPortal != "00000000-0000-0000-0000-000000000000")
                {
                    oportunidadValores = _repFaseOportunidad.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(idfaseoportunidadPortal);

                    if (oportunidadValores == null)
                    {
                        oportunidadValores = _repFaseOportunidad.ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(idfaseoportunidadPortal);
                        validadofinal = 2;
                    }
                }
                datosprograma = _repFaseOportunidad.ObtenerDatosPrograma(idInteraccionChat);
                return Ok(new { Result = "OK", Records = oportunidadValores, RecordsPrograma = datosprograma, RecordsValidado = validadofinal });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Procesa el formulario enviado segun el registro del portal web y el id de la pagina
        /// </summary>
        /// <param name="IdRegistroPortalWeb">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="IdPagina">Id del nuevo asesor (PK de la tabla gp.T_Personal)</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Route("[Action]/{IdRegistroPortalWeb}/{IdPagina}")]
        [HttpGet]
        public ActionResult ProcesarFormulario(string IdRegistroPortalWeb, int IdPagina)
        {
            try
            {
                AsignacionAutomaticaTempBO AsignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();
                AsignacionAutomaticaTemp.ProcesarRegistroFormularioPortalWeb(IdRegistroPortalWeb, IdPagina);
                AsignacionAutomaticaTempRepositorio _asignacionAutomaticaTempRep = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
                _asignacionAutomaticaTempRep.Insert(AsignacionAutomaticaTemp);
                string[] listaAProcesar = new string[1];
                listaAProcesar[0] = IdRegistroPortalWeb;
                AsignacionAutomaticaTemp.MarcarComoProcesados(listaAProcesar, IdPagina);
                return Ok(AsignacionAutomaticaTemp.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <param name="IdRegistroPortalWeb">Cadena con el registro del portal web</param>
        /// <param name="IdPagina">Id de la pagina de donde proviene el dato</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Route("[Action]/{IdRegistroPortalWeb}/{IdPagina}")]
        [HttpGet]
        public ActionResult ProcesarFormularioNuevoPortal(string IdRegistroPortalWeb, int IdPagina)
        {
            try
            {
                AsignacionAutomaticaTempBO AsignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();
                AsignacionAutomaticaTemp.ProcesarRegistroFormularioNuevoPortalWeb(IdRegistroPortalWeb, IdPagina);
                AsignacionAutomaticaTempRepositorio _asignacionAutomaticaTempRep = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
                _asignacionAutomaticaTempRep.Insert(AsignacionAutomaticaTemp);
                string[] listaAProcesar = new string[1];
                listaAProcesar[0] = IdRegistroPortalWeb;

                //foreach (string Procesado in listaAProcesar)
                //{

                //    try
                //    {
                //        _asignacionAutomaticaTempRep.MarcarComoProcesadoNuevoPortal(Procesado);

                //    }
                //    catch (Exception e)
                //    {
                //        continue;
                //    }
                //}

                if (IdPagina != 1)
                    IdPagina = 1;

                AsignacionAutomaticaTemp.MarcarComoProcesados(listaAProcesar, IdPagina);
                return Ok(AsignacionAutomaticaTemp.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Validar formulario con los datos de asignacionautomaticatemp y el origen del dato
        /// </summary>
        /// <param name="IdAsignacionAutomaticaTemp">Id de la asignacion automatica temporal (PK de la tabla mkt.T_AsignacionAutomatica_Temp)</param>
        /// <param name="OrigenDato">Origen del dato (PK de la tabla mkt.T_Origen)</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        [Route("[Action]/{IdAsignacionAutomaticaTemp}/{OrigenDato}")]
        [HttpGet]
        public ActionResult ValidarFormulario(int IdAsignacionAutomaticaTemp, int OrigenDato)// 1: Scoring, 2: PortalWeb, 3: CargaMasiva
        {
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
                OrigenRepositorio _repOrigen = new OrigenRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);

                //Obtenemos la configuracion de la asignacion automatica
                AsignacionAutomaticaConfiguracionRepositorio _repAsignacionAutomaticaConfiguracion = new AsignacionAutomaticaConfiguracionRepositorio(_integraDBContext);
                var configuraciones = _repAsignacionAutomaticaConfiguracion.ObtenerConfiguracionAsignacionAutomatica();

                var inclusion = configuraciones.Where(o => o.Inclusivo == true).ToList();
                var exclusion = configuraciones.Where(o => o.Inclusivo == false).ToList();
                //Declaro el Objeto de AsignacionAutomatica que se va ha insertar
                AsignacionAutomaticaBO AsignacionAutomatica = new AsignacionAutomaticaBO();
                //Obtenemos los Paises
                var listaPaises = new Dictionary<int, string>();
                int idElSalvador = 503;
                string ElSalvadorIniciales = "SAL";
                var paises = _repPais.GetAll();
                foreach (var pais in paises)
                {
                    //EL SALVADOR
                    if (pais.CodigoPais == idElSalvador)
                    {
                        listaPaises.Add(pais.CodigoPais, ElSalvadorIniciales);
                    }
                    else
                    {
                        listaPaises.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                    }
                }
                //Obtenemos los origenes
                var listaOrigenes = new Dictionary<string, OrigenesCategoriaOrigenDTO>();
                var origenes = _repOrigen.ObtenerOrigenesCategoriasOrigen();
                foreach (var origen in origenes)
                {
                    if (!listaOrigenes.ContainsKey(origen.Nombre.Trim().ToUpper()))
                    {
                        listaOrigenes.Add(origen.Nombre.Trim().ToUpper(), new OrigenesCategoriaOrigenDTO { Id = origen.Id, NombreCategoria = origen.NombreCategoria });
                    }
                }

                AsignacionAutomatica.ValidarRegistroFormularioAsignacionAutomaticaTemp(IdAsignacionAutomaticaTemp, listaPaises, listaOrigenes);
                if (OrigenDato == 3 ? true : AsignacionAutomatica.AplicarConfiguracion(inclusion, exclusion))
                {
                    var objAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.FirstById(IdAsignacionAutomaticaTemp);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            //Actualizamos AsignacionAutomaticamTemp con True en Procesado
                            objAsignacionAutomaticaTemp.Procesado = true;
                            _repAsignacionAutomaticaTemp.Update(objAsignacionAutomaticaTemp);
                            //Insertamos en la lista de Registros para la validacion
                            AsignacionAutomatica.Id = 0;
                            AsignacionAutomatica.IdAsignacionAutomaticaTemp = objAsignacionAutomaticaTemp.Id;
                            AsignacionAutomatica.Validado = false;
                            AsignacionAutomatica.Corregido = false;
                            AsignacionAutomatica.IdAsignacionAutomaticaOrigen = OrigenDato == 3 ? AsignacionAutomaticaOrigenBO.CargaMasiva : AsignacionAutomaticaOrigenBO.PortalWeb;
                            AsignacionAutomatica.IdCategoriaOrigen = objAsignacionAutomaticaTemp.IdCategoriaDato == null && AsignacionAutomatica.IdCategoriaDato == 18 ? 18 : objAsignacionAutomaticaTemp.IdCategoriaDato;
                            _repAsignacionAutomatica.Insert(AsignacionAutomatica);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            scope.Complete();
                        }
                    }

                    try
                    {
                        string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomaticaTemp?IdAsignacionAutomaticaTemp=" + IdAsignacionAutomaticaTemp;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return Ok(AsignacionAutomatica.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 19/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Crear la nueva probabilidad
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>

        [Route("[Action]/{idOportunidad}")]
        [HttpGet]
        public ActionResult CrearProbalidadNuevoModelo(int idOportunidad)
        {
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO(_integraDBContext);

                var nuevaProbabilidad = Oportunidad.ObtenerProbabilidadModeloPredictivo(idOportunidad);
                return Ok(nuevaProbabilidad.Probabilidad);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 13/08/2021
        /// Versión: 1.0
        /// <summary>
        /// Registra en la tabla conf.T_Log
        /// </summary>
        /// <returns>Response 200, caso contrario Response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegistrarLogError([FromBody] RegistroLogDTO RegistroLog)
        {
            try
            {
                _repLog.Insert(new TLog { Ip = RegistroLog.Ip, Usuario = RegistroLog.Usuario, Maquina = RegistroLog.Maquina, Ruta = RegistroLog.Ruta, Parametros = RegistroLog.Parametros, Mensaje = RegistroLog.Mensaje, Excepcion = RegistroLog.Excepcion, Tipo = RegistroLog.Tipo, IdPadre = RegistroLog.IdPadre, UsuarioCreacion = RegistroLog.UsuarioCreacion, UsuarioModificacion = RegistroLog.UsuarioModificacion, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 01/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad segun el IdAsignacionAutomatica enviado
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>

        [Route("[Action]/{idAsignacionAutomatica}")]
        [HttpGet]
        public ActionResult CrearOportunidadPortalWeb(int IdAsignacionAutomatica)
        {
            try
            {
                var objAsignacionAutomatica = _repAsignacionAutomatica.FirstById(IdAsignacionAutomatica);
                if (objAsignacionAutomatica != null && objAsignacionAutomatica.Validado == false && objAsignacionAutomatica.Corregido == false)
                {
                    var hoy = DateTime.Now;
                    var cadena = hoy.DayOfWeek;
                    Dictionary<string, string> dias = new Dictionary<string, string>() {
                        { "Monday", "Lunes" },
                        { "Tuesday", "Martes" },
                        { "Wednesday", "Miercoles" },
                        { "Thursday", "Jueves" },
                        { "Friday", "Viernes" },
                        { "Saturday", "Sabado" },
                        { "Sunday", "Domingo" }
                    };
                    var horario = hoy.TimeOfDay;
                    var dia = dias[cadena.ToString()];
                    var diaDto = _repBloqueHorarioProcesaOportunidad.ObtenerConfiguracion(dia);
                    var horaInicioM = diaDto.HoraInicioM;
                    var horaInicioT = diaDto.HoraInicioT;
                    var horaFinM = diaDto.HoraFinM;
                    var horaFinT = diaDto.HoraFinT;
                    int idTipoCategoriaOrigen = _repCategoriaOrigen.ObtneerTipoCategoriaOrigen(objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value);

                    if ((diaDto != null && (diaDto.TurnoM && ((horario >= horaInicioM && horario <= horaFinM) || diaDto.TurnoT && (horario >= horaInicioT && horario <= horaFinT)))) || (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)) //16=tipocategoriaorigen->chat1,38=tipocategoriaorigen->chat2
                    {
                        try
                        {
                            var listaErrores = objAsignacionAutomatica.Validar(_integraDBContext);
                            if (!listaErrores.Any())
                            {
                                OportunidadBO Oportunidad = new OportunidadBO(_integraDBContext);
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    bool ventaCruzada = true;

                                    string usuario = "SYSTEM";
                                    if (objAsignacionAutomatica.IdAlumno == null)
                                        Oportunidad.Alumno = new AlumnoBO();
                                    else
                                        Oportunidad.Alumno = _repAlumno.FirstById(objAsignacionAutomatica.IdAlumno == null ? 0 : objAsignacionAutomatica.IdAlumno.Value);

                                    Oportunidad.GenerarOportunidad(objAsignacionAutomatica, ventaCruzada, usuario, idTipoCategoriaOrigen);
                                    Oportunidad.Estado = true;
                                    Oportunidad.FechaCreacion = DateTime.Now;
                                    Oportunidad.FechaModificacion = DateTime.Now;
                                    Oportunidad.UsuarioCreacion = usuario;
                                    Oportunidad.UsuarioModificacion = usuario;

                                    if (Oportunidad.Alumno.Id == 0)
                                        CrearOportunidadCrearPersona(ref Oportunidad, ventaCruzada, TipoPersona.Alumno); // Se agrego el flag-venta-cruzada
                                    else
                                        CrearOportunidadActualizarPersona(ref Oportunidad, ventaCruzada, TipoPersona.Alumno); // Se agrego el flag-venta-cruzada

                                    objAsignacionAutomatica.Validado = true;
                                    objAsignacionAutomatica.Corregido = true;
                                    objAsignacionAutomatica.IdOportunidad = Oportunidad.Id;
                                    objAsignacionAutomatica.IdAlumno = Oportunidad.IdAlumno;
                                    objAsignacionAutomatica.FechaModificacion = DateTime.Now;
                                    objAsignacionAutomatica.UsuarioModificacion = usuario;
                                    _repAsignacionAutomatica.Update(objAsignacionAutomatica);

                                    scope.Complete();
                                }

                                /// 01/02/2021
                                /// Calculo nuevo modelo predictivo
                                /// Carlos Crispin Riquelme
                                try
                                {
                                    var nuevaProbabilidad = Oportunidad.ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);
                                }
                                catch (Exception e)
                                {
                                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CalcularModeloPredictivo", Parametros = $"{Oportunidad.Id}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                }

                                return Ok(Oportunidad.Id);
                            }
                            else
                            {
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    foreach (var error in listaErrores)
                                    {
                                        error.Estado = true;
                                        error.FechaCreacion = DateTime.Now;
                                        error.FechaModificacion = DateTime.Now;
                                        error.UsuarioCreacion = "system";
                                        error.UsuarioModificacion = "system";
                                        _repAsignacionAutomaticaError.Insert(error);
                                    }
                                    objAsignacionAutomatica.Validado = true;
                                    objAsignacionAutomatica.Corregido = false;
                                    objAsignacionAutomatica.FechaModificacion = DateTime.Now;
                                    objAsignacionAutomatica.UsuarioModificacion = "SYSTEM";
                                    _repAsignacionAutomatica.Update(objAsignacionAutomatica);
                                    scope.Complete();
                                }

                                return BadRequest("Errores al Validar La Oportunidad");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Equals("Se elimino el alumno") || ex.Message.Equals("No se creo el persona casificacion"))
                            {
                                objAsignacionAutomatica.Validado = false;
                                objAsignacionAutomatica.Corregido = false;
                                objAsignacionAutomatica.FechaModificacion = DateTime.Now;
                                objAsignacionAutomatica.UsuarioModificacion = "SYSTEM";
                                _repAsignacionAutomatica.Update(objAsignacionAutomatica);
                            }
                            if (ex.Message.Equals("Se Actualizo contacto pero NO se creo la OPORTUNIDAD porque tiene Un BNC del tipo lanzamiento"))
                            {
                                objAsignacionAutomatica.Validado = true;
                                objAsignacionAutomatica.Corregido = false;
                                objAsignacionAutomatica.Estado = false;
                                objAsignacionAutomatica.FechaModificacion = DateTime.Now;
                                objAsignacionAutomatica.UsuarioModificacion = "SYSTEM";
                                _repAsignacionAutomatica.Update(objAsignacionAutomatica);
                            }

                            return BadRequest(ex);
                        }
                    }
                    else
                    {
                        return BadRequest("No se encuentra en horario para crear Oportunidades");
                    }
                }
                else
                {
                    return BadRequest("No se encuentra valor en Asignacion Automatica o ya fue Validado ");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 28/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Se crea la oportunidad y el alumno en ventas
        /// </summary>
        /// <returns>ActionResult con estado 200 con objeto anonimo (Ok en cadena y el objeto de clase OportunidadBO)</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearOportunidadCrearAlumnoVentas([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);

                AlumnoBO alumno = new AlumnoBO
                {
                    Nombre1 = Formulario.Alumno.Nombre1,
                    Nombre2 = Formulario.Alumno.Nombre2,
                    ApellidoPaterno = Formulario.Alumno.ApellidoPaterno,
                    ApellidoMaterno = Formulario.Alumno.ApellidoMaterno,
                    Direccion = Formulario.Alumno.Direccion,
                    Telefono = Formulario.Alumno.Telefono,
                    Celular = Formulario.Alumno.Celular,
                    Email1 = Formulario.Alumno.Email1,
                    Email2 = Formulario.Alumno.Email2,
                    IdCargo = Formulario.Alumno.IdCargo,
                    IdAformacion = Formulario.Alumno.IdAFormacion,
                    IdAtrabajo = Formulario.Alumno.IdATrabajo,
                    IdIndustria = Formulario.Alumno.IdIndustria,
                    IdReferido = Formulario.Alumno.IdReferido,
                    IdCodigoPais = Formulario.Alumno.IdCodigoPais,
                    IdCiudad = Formulario.Alumno.IdCodigoCiudad,
                    HoraContacto = Formulario.Alumno.HoraContacto,
                    HoraPeru = Formulario.Alumno.HoraPeru,
                    IdEmpresa = Formulario.Alumno.IdEmpresa,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = Formulario.Usuario,
                    UsuarioModificacion = Formulario.Usuario
                };


                OportunidadBO oportunidad = new OportunidadBO
                {
                    IdCentroCosto = Formulario.Oportunidad.IdCentroCosto,
                    IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado,
                    IdTipoDato = Formulario.Oportunidad.IdTipoDato,
                    IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad,
                    IdOrigen = Formulario.Oportunidad.IdOrigen,
                    UltimoComentario = Formulario.Oportunidad.UltimoComentario,
                    IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion,
                    FechaRegistroCampania = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = Formulario.Usuario,
                    UsuarioModificacion = Formulario.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Alumno = alumno
                };

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;//Programada  6
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;//No programada 2
                this.CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Alumno);

                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                ////////try
                ////////{
                ////////    string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad="+oportunidad.Id;
                ////////    using (WebClient wc = new WebClient())
                ////////    {
                ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////        wc.DownloadString(URI);
                ////////    }
                ////////    if (oportunidad.IdPersonalAsignado != 125 && (oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookCorreo || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookComentarios || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookInbox || oportunidad.IdCategoriaOrigen == 533))
                ////////    {
                ////////        var idmigracion = _repOportunidad.FirstById(oportunidad.Id).IdMigracion;
                ////////        URI = "http://localhost:63400 /crmVentas3.1/AsignacionAutomatica2/EnviarCorreoDesdeV4?Id=" + idmigracion.ToString() + "&IdAsesor=" + oportunidad.IdPersonalAsignado;
                ////////        using (WebClient wc = new WebClient())
                ////////        {
                ////////            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////            wc.DownloadString(URI);
                ////////        }
                ////////    }
                ////////}

                ////////catch (Exception e)
                ////////{
                ////////}
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                ///01/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidad.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                }

                try
                {
                    this.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // Mailing
                try
                {
                    EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}" : "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }

                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el alumno y crea la oportunidad de ventas
        /// </summary>
        /// <param name="Formulario">Objeto de clase RegistroOportunidadAlumnoDTO</param>
        /// <returns>Response 200 con el objeto de clase OportunidadBO, caso contrario Response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAlumnoCrearOportunidadVentas([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var alumno = _repAlumno.FirstById(Formulario.Alumno.Id);

                alumno.Nombre1 = Formulario.Alumno.Nombre1;
                alumno.Nombre2 = Formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = Formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = Formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = Formulario.Alumno.Direccion;
                alumno.Telefono = Formulario.Alumno.Telefono;
                alumno.Celular = Formulario.Alumno.Celular;
                //alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = Formulario.Alumno.Email2;
                alumno.IdCargo = Formulario.Alumno.IdCargo;
                alumno.IdAformacion = Formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = Formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = Formulario.Alumno.IdIndustria;
                alumno.IdReferido = Formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = Formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = Formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = Formulario.Alumno.HoraContacto;
                alumno.HoraPeru = Formulario.Alumno.HoraPeru;
                alumno.IdEmpresa = Formulario.Alumno.IdEmpresa;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioModificacion = Formulario.Usuario;

                OportunidadBO oportunidad = new OportunidadBO();
                oportunidad.IdCentroCosto = Formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = Formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = Formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = Formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.Estado = true;
                oportunidad.FechaRegistroCampania = DateTime.Now;
                oportunidad.UsuarioCreacion = Formulario.Usuario;
                oportunidad.UsuarioModificacion = Formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;
                oportunidad.IdAlumno = alumno.Id;

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;//Programada
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;//No programada

                this.CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Alumno);

                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                ////////try
                ////////{
                ////////    string URI = "http://localhost:4348/marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + oportunidad.Id;
                ////////    using (WebClient wc = new WebClient())
                ////////    {
                ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////        wc.DownloadString(URI);
                ////////    }
                ////////    if (oportunidad.IdPersonalAsignado != 125 && (oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookCorreo || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookComentarios || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookInbox || oportunidad.IdCategoriaOrigen == 533))
                ////////    {
                ////////        var idmigracion = _repOportunidad.FirstById(oportunidad.Id).IdMigracion;
                ////////        URI = "http://localhost:63400 /crmVentas3.1/AsignacionAutomatica2/EnviarCorreoDesdeV4?Id=" + idmigracion.ToString() + "&IdAsesor=" + oportunidad.IdPersonalAsignado;
                ////////        using (WebClient wc = new WebClient())
                ////////        {
                ////////            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////            wc.DownloadString(URI);
                ////////        }
                ////////    }
                ////////}
                ////////catch (Exception e)
                ////////{
                ////////    //throw;
                ////////}
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                ///01/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidad.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }

                try
                {
                    this.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                    //solo si no funciona MetodoODyOM
                }

                try
                {
                    EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}" : "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }

                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void EnviarCorreoOportunidad(int IdOportunidad)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);

                var oportunidad = _repOportunidad.FirstById(IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                if (oportunidad.IdPersonalAsignado != ValorEstatico.IdPersonalAsignacionAutomatica && oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC && oportunidad.IdTipoDato == ValorEstatico.IdTipoDatoLanzamiento && personal.AreaAbrev == ValorEstatico.AreaTrabajoVentas)
                {
                    bool enviarIdPersonalPorDefecto = false;

                    string uri = $"https://integraV4-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdPlantillaInformacionCursoVentas}/{enviarIdPersonalPorDefecto}";

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uri);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Crea la oportunidad enviada como objeto y su sincronizacion con V3, ademas de la creacion del alumno o actualizacion incluyendo la persona
        /// </summary>
        /// <param name="Oportunidad">Referencia de Objeto de clase Oportunidad BO</param>
        private void CrearOportunidadMarketing(ref OportunidadBO Oportunidad)
        {
            try
            {
                if (Oportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                if (Oportunidad.Alumno.Id == 0)
                {
                    CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno);
                }
                else
                {
                    CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno);
                }

                // 01/02/2021
                // Calculo nuevo modelo predictivo
                // Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = Oportunidad.ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }

                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                //////////Insertar actualizar alumno a v3
                ////////try
                ////////{
                ////////    //string URI = "http://localhost:4348/Marketing/SincronizarAlumnoAV3?IdAlumno=" + Oportunidad.Alumno.Id.ToString() + "&EsCrear=true";
                ////////    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id;
                ////////    using (WebClient wc = new WebClient())
                ////////    {
                ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////        wc.DownloadString(URI);
                ////////    }
                ////////}
                ////////catch (Exception e)
                ////////{
                ////////}
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                //return Oportunidad.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[Action]/{Nombres}/{Apellidos}/{Email}/{Celular}/{Pais}")]
        [HttpGet]
        public ActionResult CrearOportunidadEDX(string Nombres, string Apellidos, string Email, string Celular, string Pais)
        {
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO()
                {
                    Id = 0,
                    IdPersonalAsignado = 4220,
                    IdConjuntoAnuncio = 0,
                    IdCampaniaScoring = 0,
                    IdCategoriaOrigen = 0,
                    IdAlumno = 0,
                    IdFaseOportunidad = 2,
                    IdFaseOportunidadInicial = 0,
                    IdFaseOportunidadMaxima = 0,
                    IdCentroCosto = 12140,
                    IdOrigen = 741,
                    IdTipoDato = 8,
                    IdActividadCabeceraUltima = 0,
                    IdActividadDetalleUltima = 0,
                    UltimaFechaProgramada = null,
                    UltimoComentario = "Sin Comentario",
                    IdEstadoActividadDetalleUltimoEstado = 0,
                    IdEstadoOcurrenciaUltimo = 0,
                    IdEstadoOportunidad = 0,
                    CodMailing = null,
                    CodigoPagoIc = null,
                    IdFaseOportunidadIc = 0,
                    IdFaseOportunidadIp = 0,
                    IdFaseOportunidadPf = 0,
                    FechaEnvioFaseOportunidadPf = null,
                    FechaPagoFaseOportunidadIc = null,
                    FechaPagoFaseOportunidadPf = null,
                    FechaRegistroCampania = null,
                    IdInteraccionFormulario = 0,
                    IdSubCategoriaDato = 0,
                    IdFaseOportunidadPortal = null,
                    IdTiempoCapacitacion = 0,
                    IdTiempoCapacitacionValidacion = 0,
                    UrlOrigen = null,
                    IdPagina = 0,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "System",
                    UsuarioModificacion = "System"

                };
                AlumnoBO Alumno = new AlumnoBO()
                {
                    Id = 0,
                    FechaNacimiento = null,
                    HoraContacto = null,
                    HoraPeru = null,
                    IdAformacion = null,
                    IdAtrabajo = null,
                    IdCargo = null,
                    IdCodigoRegionCiudad = 0,
                    IdCodigoPais = Convert.ToInt32(Pais),
                    IdEmpresa = null,
                    IdIndustria = null,
                    IdReferido = null,
                    NombreCiudad = null,
                    Aformacion = null,
                    Cargo = null,
                    Ciudad = null,
                    Empresa = null,
                    Industria = null,
                    NombrePais = null,
                    ApellidoPaterno = Apellidos,
                    Asociado = false,
                    Celular = Celular,
                    Celular2 = null,
                    Direccion = null,
                    Dni = null,
                    Email1 = Email,
                    Email2 = null,
                    Nombre1 = Nombres,
                    Nombre2 = "",
                    Telefono = null,
                    Telefono2 = null,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "System",
                    UsuarioModificacion = "System"
                };
                Oportunidad.Alumno = Alumno;

                if (Oportunidad.UltimaFechaProgramada != null)
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                else
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;

                if (Oportunidad.Alumno.Id == 0)
                {
                    CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                }
                else
                {
                    CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                }

                //Insertar actualizar alumno a v3
                try
                {
                    //string URI = "http://localhost:4348/Marketing/SincronizarAlumnoAV3?IdAlumno=" + Oportunidad.Alumno.Id.ToString() + "&EsCrear=true";
                    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id;
                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(URI);
                    }
                }
                catch (Exception e)
                {
                }

                return Ok(Oportunidad.Alumno.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida los casos en los que puede convertirse en OD u OM la oportunidad
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="IdAsignacionAutomatica">Id de la asignacion automatica (PK de la tabla 6613)</param>
        /// <param name="FlagPortalWeb">Flag para determinar si el dato viene del portal web</param>
        /// <returns>Json</returns>
        [Route("[Action]/{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}")]
        [HttpGet]
        public ActionResult ValidarCasosOportunidad(int IdOportunidad, int IdAsignacionAutomatica, bool FlagPortalWeb)
        {
            try
            {
                // Valores Generales
                bool validacionCorrecta = true;
                var oportunidad = _repOportunidad.FirstById(IdOportunidad);
                var asesorAsociado = new ResultadoDTO();
                int probabilidadActual = 0;
                int idTabRedireccion = 0;
                var diaDto = new BloqueHorarioProcesaOportunidadBO();

                // Validacion punto corte por programa
                int idPGeneral = _repPespecifico.FirstBy(w => w.IdCentroCosto == oportunidad.IdCentroCosto.Value).IdProgramaGeneral == null ? 0 : _repPespecifico.FirstBy(w => w.IdCentroCosto == oportunidad.IdCentroCosto.Value).IdProgramaGeneral.Value;
                var programaGeneralPuntoCorte = _repProgramaGeneralPuntoCorte.FirstBy(w => w.IdProgramaGeneral == idPGeneral);
                // Si no tiene probabilidad el programa de la oportunidad
                if (programaGeneralPuntoCorte == null)
                {
                    #region Marcado validacion correcta
                    try
                    {
                        _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                    }
                    catch (Exception)
                    {
                    }
                    #endregion

                    return Ok();
                }

                ////////////////////////////////////////////////////////////// SI VIENE DESDE PORTAL WEB /////////////////////////////////////////////////////////////////////////
                if (FlagPortalWeb)
                {
                    var objAsignacionAutomatica = _repAsignacionAutomatica.FirstById(IdAsignacionAutomatica);

                    int idTipoCategoriaOrigen = _repCategoriaOrigen.ObtneerTipoCategoriaOrigen(objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value);
                    //var objCategoria = _repCategoriaOrigenRep.FirstById(objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value);

                    Dictionary<string, string> dias = new Dictionary<string, string>() {
                        { "Monday", "Lunes" },
                        { "Tuesday", "Martes" },
                        { "Wednesday", "Miercoles" },
                        { "Thursday", "Jueves" },
                        { "Friday", "Viernes" },
                        { "Saturday", "Sabado" },
                        { "Sunday", "Domingo" }
                    };
                    var hoy = DateTime.Now;
                    var cadena = hoy.DayOfWeek;
                    var dia = dias[cadena.ToString()];
                    diaDto = _repBloqueHorarioProcesaOportunidad.ObtenerConfiguracion(dia);

                    //probabilidadActual = _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual == null ? 0 : _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual.Value;

                    //Obtencion del valor 0-4 segun al probabilidad
                    var probabilidaNueva = _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad) == null ? 0 : _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad).Probabilidad;

                    if (probabilidaNueva < programaGeneralPuntoCorte.PuntoCorteAlta)
                    {
                        probabilidadActual = 2;//MEDIA
                    }
                    else if (probabilidaNueva >= programaGeneralPuntoCorte.PuntoCorteAlta && probabilidaNueva < programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                    {
                        probabilidadActual = 3;//ALTA
                    }
                    else if (probabilidaNueva >= programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                    {
                        probabilidadActual = 4;//MUY ALTA
                    }
                    else
                    {
                        probabilidadActual = 1;//SIN PROBABILIDAD
                    }

                    asesorAsociado.IdAsesor = oportunidad.IdPersonalAsignado;

                    int probabilidad = probabilidadActual;

                    // Verificar coincidencia configuracion remarketing
                    try
                    {
                        if (objAsignacionAutomatica.IdTipoDato.HasValue && objAsignacionAutomatica.IdSubCategoriaDato.HasValue)
                            idTabRedireccion = _repConfiguracionDatoRemarketing.ObtenerTabRedireccionRemarketing(objAsignacionAutomatica.IdTipoDato.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, probabilidad);
                    }
                    catch (Exception ex)
                    {
                        idTabRedireccion = 0;
                    }

                    // Si el contacto ya existe
                    if (objAsignacionAutomatica.IdAlumno > 0)
                    {
                        asesorAsociado.IdAsesor = asesorAsociado.IdAsesor == 125 || asesorAsociado.IdAsesor == 0 ? this.ObtenerAsesorParaCentroCosto(objAsignacionAutomatica.IdCentroCosto.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, objAsignacionAutomatica.IdPais.Value, probabilidad, idTabRedireccion) : asesorAsociado.IdAsesor;
                    }
                    else
                    {
                        asesorAsociado.IdAsesor = this.ObtenerAsesorParaCentroCosto(objAsignacionAutomatica.IdCentroCosto.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, objAsignacionAutomatica.IdPais.Value, probabilidad, idTabRedireccion);
                    }

                    if (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)//16=>TipoCategoriaOrigen = Chat1,38=>TipoCategoriaOrigen = Chat2
                    {
                        asesorAsociado.IdAsesor = oportunidad.IdPersonalAsignado;
                    }
                    else
                    {
                        // Aqui si se asigna el asesor
                        oportunidad.IdPersonalAsignado = asesorAsociado.IdAsesor == 0 ? oportunidad.IdPersonalAsignado : asesorAsociado.IdAsesor;
                    }
                }
                ////////////////////////////////////////////////////////////// FIN SI VIENE DESDE EL PORTAL WEB /////////////////////////////////////////////////////////////////////////

                bool enviarNotificacion = true; // para que no se envie notificacion a la agenda si cerramos la oportunidad en OM, OD
                                                // Valores Precalculados

                int idCentroCosto = oportunidad.IdCentroCosto.Value;
                int idCategoriaOrigen = oportunidad.IdCategoriaOrigen;

                int idProgramaGeneral = _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral == null ? 0 : _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral.Value;

                //Obtencion del valor 0-4 segun al probabilidad
                int idProbRegisPW;
                var probabilidaNuevaComparacion = _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad) == null ? 0 : _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad).Probabilidad;
                if (probabilidaNuevaComparacion < programaGeneralPuntoCorte.PuntoCorteAlta)
                {
                    idProbRegisPW = 2;// Media
                }
                else if (probabilidaNuevaComparacion >= programaGeneralPuntoCorte.PuntoCorteAlta && probabilidaNuevaComparacion < programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                {
                    idProbRegisPW = 3;// Alta
                }
                else if (probabilidaNuevaComparacion >= programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                {
                    idProbRegisPW = 4;// Muy alta
                }
                else
                {
                    idProbRegisPW = 1;// Sin probabilidad
                }

                //int idProbRegisPW = _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual == null ? 0 : _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual.Value;
                string pesoDescripcion = _repProbabilidadRegistroPw.FirstBy(w => w.Id == idProbRegisPW).Nombre;


                //Peso de la Probabilidad de la Oportunidad
                int pesoOportunidad = pesoDescripcion == "Muy Alta" ? 2 : ((pesoDescripcion == "Alta" || pesoDescripcion == "Media") ? 1 : 0);
                //Peso de la Categoria de la Oportunidad
                int pesoCategoriaOportunidad = _repCategoriaOrigen.FirstBy(w => w.Id == idCategoriaOrigen).Meta;

                var objetoAlumno = _repAlumno.FirstById(oportunidad.IdAlumno);
                /*Caracter especial para evitar registros coincidentes con celular vacio*/
                objetoAlumno.Celular = string.IsNullOrEmpty(objetoAlumno.Celular) ? "-|!x!|-" : objetoAlumno.Celular.Trim();
                ///////////////////////////////////// CASO 1:Valida si hay oportunidades en IS o M //////////////////////////////////////////////////////
                var listaISM = _repOportunidad.ValidarOportunidadesISM(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaISM.Count() > 0)//Si hay un IS o M me tengo que cerrar
                {
                    int[] listaOportunidadesISM = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesISM, "System Duplicado");//FALTA IMPLEMENTAR

                    #region Marcado validacion correcta
                    try
                    {
                        _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                    }
                    catch (Exception)
                    {
                    }
                    #endregion

                    return Ok();
                }
                //////////////////////////////////// CASO 2:Valida si hay opotunidades con mayor Probabilidad //////////////////////////////////////////
                var listaProbabilidades = _repOportunidad.ValidarOportunidadesProbabilidad(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaProbabilidades.Count() > 0)//Si hay oportunidades con el mismo alumno y del mismo programa
                {
                    if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)// Si alguno con mayor probabilidad que el actual me tengo que cerrar
                    {
                        if (listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault() != null)//mayor  probabilidad del mismo programa
                        {
                            if (listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)
                            {
                                int[] listaOportunidadesProbabilidadesOD = new int[] { oportunidad.Id };
                                this.CerrarOportunidadesOD(listaOportunidadesProbabilidadesOD, "Sys Duplicado Prob");//FALTA IMPLEMENTAR

                                #region Marcado validacion correcta
                                try
                                {
                                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                                }
                                catch (Exception)
                                {
                                }
                                #endregion

                                return Ok();
                            }

                        }
                        else if (listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault() != null)//mayor  probabilidad de diferente programa
                        {
                            if (listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)
                            {
                                int[] listaOportunidadesProbabilidadesOM = new int[] { oportunidad.Id };
                                this.CerrarOportunidadesOM(listaOportunidadesProbabilidadesOM, "Sys Duplicado Prob");

                                #region Marcado validacion correcta
                                try
                                {
                                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                                }
                                catch (Exception)
                                {
                                }
                                #endregion

                                return Ok();
                            }
                        }
                    }
                    else if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad == pesoOportunidad)//Si tienen una probabilidad igual que el actual no se hace nada
                    {
                        //nada
                    }
                    else//Significa son de menor probablidad que el actual y deben cerrarse
                    {


                        int[] listaOportunidadesProbabilidadesOD = new int[listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList().Count()];
                        int[] listaOportunidadesProbabilidadesOM = new int[listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList().Count()];
                        int contador1 = 0;

                        ///listaProbabilidades: itera en la lista de oportunidades con mayor probabilidad que la actual. 
                        foreach (var item in listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral))
                        {
                            listaOportunidadesProbabilidadesOD[contador1] = item.IdOportunidad;
                            contador1++;
                        }
                        int contador2 = 0;
                        foreach (var item in listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral))
                        {
                            listaOportunidadesProbabilidadesOM[contador2] = item.IdOportunidad;
                            contador2++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesProbabilidadesOD, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                        this.CerrarOportunidadesOM(listaOportunidadesProbabilidadesOM, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                    }
                }

                //////////////////////////////////Antes de pasar a los demas casos validamos el id del asesor si es  125:Asignacion Automatica////////
                if (FlagPortalWeb)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                    {
                        //actualizamos el asesor en la oportunidad
                        oportunidad.IdPersonalAsignado = asesorAsociado.IdAsesor;
                        oportunidad.FechaModificacion = DateTime.Now;
                        _repOportunidad.Update(oportunidad);
                        //modificacion el log inicial (para actualizar el asesor del primer log )
                        string usuario = "SystemRealTime";
                        var ultimoLog = _repOportunidadLog.GetBy(w => w.IdOportunidad == IdOportunidad).OrderByDescending(w => w.FechaLog).FirstOrDefault();
                        ultimoLog.IdPersonalAsignado = oportunidad.IdPersonalAsignado;
                        ultimoLog.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                        ultimoLog.IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad;
                        ultimoLog.IdCentroCosto = oportunidad.IdCentroCosto;
                        ultimoLog.IdOrigen = oportunidad.IdOrigen;
                        ultimoLog.IdTipoDato = oportunidad.IdTipoDato;
                        ultimoLog.UsuarioModificacion = usuario;
                        ultimoLog.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                        _repOportunidadLog.Update(ultimoLog);


                        this.ActualizarAsignacionOportunidad(oportunidad.Id, oportunidad.IdPersonalAsignado, oportunidad.IdCentroCosto.Value, oportunidad.IdAlumno, usuario);
                    }
                }
                //////////////////////////////////// CASO 3:Valida si hay opotunidades con mayor Categoria //////////////////////////////////////////
                var listaCategorias = _repOportunidad.ValidarOportunidadesCategoria(oportunidad.IdAlumno, oportunidad.Id, objetoAlumno.Celular);
                //OM-OD
                var listaCategoriasOM = listaCategorias.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList();
                var listaCategoriasOD = listaCategorias.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList();
                if (listaCategoriasOM.Count() > 0)
                {
                    if (listaCategoriasOM.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOM(listaOportunidadesCategorias, "UsuarioOM");

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                    else if (listaCategoriasOD.Count() > 0)
                    {
                        if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                        {
                            if (oportunidad.IdPersonalAsignado != 125)
                                enviarNotificacion = false;

                            int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                            this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                            #region Marcado validacion correcta
                            try
                            {
                                _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                            }
                            catch (Exception ex)
                            {
                            }
                            #endregion

                            return Ok();
                        }
                        else
                        {
                            //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                            oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                            oportunidad.FechaModificacion = DateTime.Now;
                            oportunidad.UsuarioModificacion = "System";
                            _repOportunidad.Update(oportunidad);

                            //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                            var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                            if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                            {
                                this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                                oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                            }
                            //Mandamos la lista de oportunidades anteriores a Cerrar OD
                            int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                            int contador = 0;
                            foreach (var item in listaCategoriasOD)
                            {
                                listaOportunidadesCategorias[contador] = item.IdOportunidad;
                                contador++;
                            }
                            this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                        }
                    }
                }
                else if (listaCategoriasOD.Count() > 0)
                {
                    if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                    else
                    {
                        //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        _repOportunidad.Update(oportunidad);

                        //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                        var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {

                            this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                        //Mandamos la lista de oportunidades anteriores a Cerrar OD
                        int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasOD)
                        {
                            listaOportunidadesCategorias[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                    }

                }
                //////////////////////////////////// CASO 4:Valida si hay opotunidades con mayor Categoria en Fase IP ////////////////////////////////
                var listaCategoriasIPS = _repOportunidad.ValidarOportunidadesCategoriaIPMismoPG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasIPS.Count() > 0)
                {
                    if (listaCategoriasIPS.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                    {
                        //primero me reasigno
                        this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                        oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;
                        enviarNotificacion = false;
                        int[] listaOportunidadesCategoriasIP = new int[] { oportunidad.Id };
                        //segundo cerrarme
                        this.CerrarOportunidadesOD(listaOportunidadesCategoriasIP, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                    else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                    {
                        //primero me  reasigno
                        this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                        oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;

                        //segundo cierro la otras IPS
                        int[] listaOportunidadesIPCerrar = new int[listaCategoriasIPS.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasIPS)
                        {
                            listaOportunidadesIPCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesIPCerrar, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                        //tercero me paso a IP
                        //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = 8;//IP
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        _repOportunidad.Update(oportunidad);

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                }
                //////////////////////////////////// CASO 5:Valida si hay opotunidades con mayor Categoria en Fase IP de Otros Programas///////////////
                var listaCategoriasIPSPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaIPDiferentePG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasIPSPGDiferente.Count() > 0)
                {
                    if (listaCategoriasIPSPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                    {
                        //primero me reasigno
                        this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                        oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;
                        enviarNotificacion = false;
                        int[] listaOportunidadesCategoriasIPPGDiferente = new int[] { oportunidad.Id };
                        //segundo cerrarme
                        this.CerrarOportunidadesOD(listaOportunidadesCategoriasIPPGDiferente, "UsuarioOM");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                    else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                    {
                        //primero me  reasigno
                        this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                        oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;

                        //segundo cierro la otras IPS
                        int[] listaOportunidadesIPPGDiferenteCerrar = new int[listaCategoriasIPSPGDiferente.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasIPSPGDiferente)
                        {
                            listaOportunidadesIPPGDiferenteCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOM(listaOportunidadesIPPGDiferenteCerrar, "UsuarioOM");

                        //tercero me paso a IP
                        //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = 8;//IP
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        _repOportunidad.Update(oportunidad);

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return Ok();
                    }
                }
                //////////////////////////////////// CASO 6:Valida si hay opotunidades con mayor Categoria en Fase RN,IT,BNC de Otros Programas///////////////
                var listaCategoriasBNCITRNPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaBNCITRNDiferentePG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasBNCITRNPGDiferente.Count() > 0)
                {
                    if (listaCategoriasBNCITRNPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria < pesoCategoriaOportunidad)
                    {
                        //Cerras las oportunidades que llegan
                        int[] listaOportunidadesBNCITRNPGDiferenteCerrar = new int[listaCategoriasBNCITRNPGDiferente.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasBNCITRNPGDiferente)
                        {
                            listaOportunidadesBNCITRNPGDiferenteCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        //Me actualizo a la fase de la oportunidad anterior
                        oportunidad.IdFaseOportunidad = listaCategoriasBNCITRNPGDiferente.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        _repOportunidad.Update(oportunidad);

                        var oportunidadAnterior = listaCategoriasBNCITRNPGDiferente.Where(w => w.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado)
                        {
                            if (oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                            {
                                this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                                oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                            }
                        }
                        this.CerrarOportunidadesOM(listaOportunidadesBNCITRNPGDiferenteCerrar, "UsuarioOM");//FALTA IMPLEMENTAR
                    }
                }

                //////////////////////////////////////////////////////////// EXTRAS///////////////////////////////////////////////////////////////////////////
                #region Configuracion Dato Remarketing
                try
                {
                    // Insertar tabla mkt.T_OportunidadRemarketingAgenda
                    if (idTabRedireccion > 0 && oportunidad.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadOD && oportunidad.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadOM)
                    {
                        _repOportunidadRemarketingAgenda.EliminarRedireccionRemarketingAnterior(oportunidad.Id);

                        _repOportunidadRemarketingAgenda.Insert(new OportunidadRemarketingAgendaBO
                        {
                            IdOportunidad = oportunidad.Id,
                            IdAgendaTab = idTabRedireccion,
                            AplicaRedireccion = true,
                            Estado = true,
                            UsuarioCreacion = "UsuarioRemarketing",
                            UsuarioModificacion = "UsuarioRemarketing",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                    }
                }
                catch (Exception ex)
                {
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ConfiguracionDatoRemarketing", Parametros = $"{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "REMARKETING DATA", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                    validacionCorrecta = false;
                }
                #endregion

                #region Marcado validacion correcta
                try
                {
                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                }
                catch (Exception ex)
                {
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarValidacionOportunidad", Parametros = $"{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "FINISH VALIDATION", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                }
                #endregion

                #region Envio correo, Whatsapp, SMS
                if (FlagPortalWeb)
                {
                    if (enviarNotificacion != false && oportunidad.IdPersonalAsignado != ValorEstatico.IdPersonalAsignacionAutomatica)
                    {
                        // Mailing
                        try
                        {
                            // Flag opcional
                            bool enviarIdPersonalPorDefecto = false;

                            string uri = $"https://integraV4-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdPlantillaInformacionCursoVentas}/{enviarIdPersonalPorDefecto}";

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(uri);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        // SMS
                        try
                        {
                            string uriSms = string.Empty;

                            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                            {
                                if (DateTime.Now.Hour == 18)
                                    uriSms = DateTime.Now.Minute < 30 ? $"https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}" : "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                                else if (DateTime.Now.Hour > 18)
                                    uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                                else
                                    uriSms = "https://integraV4-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                            }

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(uriSms);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (diaDto.ProbabilidadOportunidad == null)
                        diaDto.ProbabilidadOportunidad = "Muy Alta";

                    if (probabilidadActual != 0 && enviarNotificacion == true)
                    {
                        string probabilidaActualDesc = probabilidadActual == 4 ? "Muy Alta" : (probabilidadActual == 3 ? "Alta" : (probabilidadActual == 2 ? "Media" : "Sin Probabilidad"));

                        var probabilidades = diaDto.ProbabilidadOportunidad.Split(",");
                        if (probabilidades.Any(w => w == probabilidaActualDesc))
                        {
                            // LLama al Socket
                            AgendaSocket.getInstance().NuevaActividadParaEjecutar(IdOportunidad, oportunidad.IdPersonalAsignado);
                        }
                    }
                }
                #endregion

                return Ok();
            }
            catch (Exception e)
            {
                if (!e.Message.Contains("Timeout expired"))
                {
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ValidarCasosOportunidad", Parametros = $"{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                }

                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 17/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesar Oportunidades Portal Web
        /// </summary>
        /// <returns>bool true<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ProcesarOportunidadesPortalWeb()
        {
            try
            {
                AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio();
                AsignacionAutomaticaBO asignacionAutomatica = new AsignacionAutomaticaBO();

                var lista = asignacionAutomatica.ObtenerNuevosRegistros();
                string[] listaAProcesar = new string[lista.Count];
                int contador = 0;
                foreach (var item in lista)
                {
                    AsignacionAutomaticaTempBO asignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();
                    Guid auxGuid = new Guid(item.IdFaseOportunidadPortal);

                    if (_repAsignacionAutomaticaTemp.FirstBy(x => x.IdFaseOportunidadPortal == auxGuid) == null)
                    {
                        asignacionAutomaticaTemp.MapearAsignacionAutomaticaTemp(item);
                        _repAsignacionAutomaticaTemp.Insert(asignacionAutomaticaTemp);

                    }
                    listaAProcesar[contador] = item.IdFaseOportunidadPortal;
                    contador++;
                }

                AsignacionAutomaticaTempBO asignacionAutomaticaTempMarcar = new AsignacionAutomaticaTempBO();
                asignacionAutomaticaTempMarcar.MarcarComoProcesados(listaAProcesar, 1);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 04/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida las oportunidades del portal web
        /// </summary>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ValidarOportunidadesPortalWeb()
        {
            try
            {
                AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(_integraDBContext);

                // Obtenemos la configuracion de la asignacion automatica
                AsignacionAutomaticaConfiguracionRepositorio _asignacionAutomaticaConfiguracionRep = new AsignacionAutomaticaConfiguracionRepositorio();
                var configuraciones = _asignacionAutomaticaConfiguracionRep.ObtenerConfiguracionAsignacionAutomatica();

                // Se comentan las lineas ya que su uso esta comentado tambien
                //var inclusion = configuraciones.Where(o => o.Inclusivo == true).ToList();
                //var exclusion = configuraciones.Where(o => o.Inclusivo == false).ToList();

                var listaAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.GetBy(w => w.Procesado == false).ToList();

                foreach (var idAsignacionAutomaticaTemp in listaAsignacionAutomaticaTemp.Select(w => w.Id))
                {
                    // Declaro el Objeto de AsignacionAutomatica que se va ha insertar
                    AsignacionAutomaticaBO AsignacionAutomatica = new AsignacionAutomaticaBO();
                    // Obtenemos los Paises
                    var listaPaises = new Dictionary<int, string>();
                    PaisRepositorio PaisRep = new PaisRepositorio();
                    int idElSalvador = 503;
                    string ElSalvadorIniciales = "SAL";
                    var paises = PaisRep.GetAll();
                    foreach (var pais in paises)
                    {
                        // El Salvador
                        if (pais.CodigoPais == idElSalvador)
                            listaPaises.Add(pais.CodigoPais, ElSalvadorIniciales);
                        else
                            listaPaises.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                    }

                    // Obtenemos los origenes
                    var listaOrigenes = new Dictionary<string, OrigenesCategoriaOrigenDTO>();
                    OrigenRepositorio _repOrigen = new OrigenRepositorio();
                    var origenes = _repOrigen.ObtenerOrigenesCategoriasOrigen();

                    foreach (var origen in origenes)
                    {
                        if (!listaOrigenes.ContainsKey(origen.Nombre.Trim().ToUpper()))
                            listaOrigenes.Add(origen.Nombre.Trim().ToUpper(), new OrigenesCategoriaOrigenDTO { Id = origen.Id, NombreCategoria = origen.NombreCategoria });
                    }

                    try
                    {
                        AsignacionAutomatica.ValidarRegistroFormularioAsignacionAutomaticaTemp(idAsignacionAutomaticaTemp, listaPaises, listaOrigenes);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                    //if (AsignacionAutomatica.AplicarConfiguracion(inclusion, exclusion))
                    if (true)
                    {
                        var objAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.FirstById(idAsignacionAutomaticaTemp);
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                // Actualizamos AsignacionAutomaticamTemp con True en Procesado
                                objAsignacionAutomaticaTemp.Procesado = true;
                                _repAsignacionAutomaticaTemp.Update(objAsignacionAutomaticaTemp);
                                // Insertamos en la lista de Registros para la validacion
                                AsignacionAutomatica.Id = 0;
                                AsignacionAutomatica.Validado = false;
                                AsignacionAutomatica.Corregido = false;
                                AsignacionAutomatica.IdAsignacionAutomaticaOrigen = AsignacionAutomaticaOrigenBO.PortalWeb;
                                AsignacionAutomatica.IdAsignacionAutomaticaTemp = objAsignacionAutomaticaTemp.Id;
                                AsignacionAutomatica.IdCategoriaOrigen = objAsignacionAutomaticaTemp.IdCategoriaDato == null && AsignacionAutomatica.IdCategoriaDato == 18 ? 18 : objAsignacionAutomaticaTemp.IdCategoriaDato;
                                _repAsignacionAutomatica.Insert(AsignacionAutomatica);

                                scope.Complete();
                            }
                            catch (Exception e)
                            {
                                throw new Exception(e.Message);
                            }
                        }

                        try
                        {
                            string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomaticaTemp?IdAsignacionAutomaticaTemp=" + idAsignacionAutomaticaTemp;
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(URI);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        // Asignacion automatica
                        try
                        {
                            string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomatica.Id;
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(URI);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[Action]")]
        [HttpGet]

        /// <summary>
        /// Realiza la creacion de la oportunidad y validacion/actualizar por venta cruzada
        /// </summary>
        /// <param name="oportunidad"></param>
        private void CrearOportunidad(ref OportunidadBO oportunidad, bool flagVentaCruzada, TipoPersona tipoPersona)
        {
            // Llenamos valores oportunidad/hijos
            this.LlenarOportunidadHijos(oportunidad);
            if (!oportunidad.HasErrors)
            {
                var actividadNuevaTemp = oportunidad.ActividadNueva;
                oportunidad.ActividadNueva = null;
                var AsignacionOporturtunidadTemp = oportunidad.AsignacionOportunidad;
                AsignacionOporturtunidadTemp.IdClasificacionPersona = oportunidad.IdClasificacionPersona;

                AsignacionOporturtunidadTemp.AsignacionOportunidadLog = oportunidad.AsignacionOportunidad.AsignacionOportunidadLog;
                AsignacionOporturtunidadTemp.AsignacionOportunidadLog.IdClasificacionPersona = oportunidad.IdClasificacionPersona;

                oportunidad.AsignacionOportunidad = null;
                _repOportunidad.Insert(oportunidad);
                var idOportunidad = oportunidad.Id;

                AsignacionOporturtunidadTemp.IdOportunidad = idOportunidad;
                AsignacionOporturtunidadTemp.AsignacionOportunidadLog.IdOportunidad = idOportunidad;

                if (AsignacionOporturtunidadTemp.Id != 0 && AsignacionOporturtunidadTemp.Id != null)
                {
                    _repAsignacionOportunidad.Update(AsignacionOporturtunidadTemp);
                }
                else
                {
                    _repAsignacionOportunidad.Insert(AsignacionOporturtunidadTemp);
                }


                bool flagValidaActividadDetalle = false;
                int nroIntentos = 0;

                while (!flagValidaActividadDetalle && nroIntentos < 5)
                {
                    try
                    {
                        var actividadDetalleBO = _repActividadDetalle.FirstById(actividadNuevaTemp.Id);
                        actividadDetalleBO.IdOportunidad = oportunidad.Id;
                        actividadDetalleBO.IdClasificacionPersona = oportunidad.IdClasificacionPersona;

                        _repActividadDetalle.Update(actividadDetalleBO);

                        flagValidaActividadDetalle = true;
                    }
                    catch (Exception ex)
                    {
                        nroIntentos++;

                        if (nroIntentos == 4)
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarActividadDetalle", Parametros = $"idActividadDetalle={actividadNuevaTemp.Id}&idOportunidad={oportunidad.Id}&idClasificacionPersona={oportunidad.IdClasificacionPersona}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                            throw new Exception(ex.Message);
                        }

                        Thread.Sleep(2000);
                    }
                }

                var _idOportunidad = oportunidad.Id;
                var cantidad = _repModeloDataMining.GetBy(x => x.IdOportunidad == _idOportunidad, x => new { x.Id }).ToList();
                ModeloDataMiningBO modeloDataMining;

                if (tipoPersona == TipoPersona.Alumno)
                {
                    if (cantidad != null && cantidad.Count > 1)
                    {
                        modeloDataMining = _repModeloDataMining.FirstById(cantidad.FirstOrDefault().Id);
                        modeloDataMining.IdOportunidad = oportunidad.Id;
                        modeloDataMining.ObtenerProbabilidad(oportunidad.Id);

                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                        modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.Estado = true;
                        _repModeloDataMining.Update(modeloDataMining);
                    }
                    else
                    {
                        modeloDataMining = new ModeloDataMiningBO(_integraDBContext)
                        {
                            IdOportunidad = oportunidad.Id
                        };
                        modeloDataMining.ObtenerProbabilidad(oportunidad.Id);

                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                        modeloDataMining.UsuarioCreacion = oportunidad.UsuarioCreacion;
                        modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaCreacion = DateTime.Now;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.FechaCreacionContacto = DateTime.Now;
                        modeloDataMining.FechaCreacionOportunidad = oportunidad.FechaCreacion;
                        modeloDataMining.Estado = true;
                        _repModeloDataMining.Insert(modeloDataMining);
                    }

                    oportunidad.ValorProbabilidad = modeloDataMining.ProbabilidadActual < modeloDataMining.PuntoCorte ? -1 : modeloDataMining.IdProbabilidadRegistroPwActual;
                    //oportunidad.ModeloDataMining = modeloDataMining;
                }
            }
            //validamos venta cruzada
            this.OportunidadVentaCruzada(ref oportunidad, flagVentaCruzada, tipoPersona);
        }

        /// <summary>
        /// Hace la logica de validaciones por venta cruzada, reasignar las oportunidades medias y altas    
        /// </summary>
        private void OportunidadVentaCruzada(ref OportunidadBO oportunidad, bool flagVentaCruzada, Enums.TipoPersona tipoPersona)
        {
            if (tipoPersona == TipoPersona.Alumno)
            {
                var idAsesorVentaCruzada = this.ObtenerAsesorVentaCruzada(oportunidad.IdAlumno);
                if (oportunidad.IdPersonalAsignado == ValorEstatico.IdPersonalAsignacionAutomatica || oportunidad.IdPersonalAsignado == idAsesorVentaCruzada || flagVentaCruzada == true)
                {
                    if (oportunidad.ModeloDataMining != null && (_repOportunidad.ValidarProbabilidadVentaCruzada(oportunidad.ModeloDataMining.IdProbabilidadRegistroPwActual)))
                    {
                        try
                        {
                            //si encontramos almenos un programa en lanzamiento por venta cruzada, reasignamos la oportunidad a un asesor que tenga meta en ese programa
                            if (idAsesorVentaCruzada != 0 && idAsesorVentaCruzada != -1)
                            {
                                //NO ENVIA CORREO PORQUE NO HAY OTRAS CON CUAL COMPARAR
                                this.ActualizarOportunidadVentaCruzada(ref oportunidad, idAsesorVentaCruzada, "UsuarioVentaCruzada", false, false);//NO ENVIA CORREO PORQUE NO HAY OTRAS CONCUAL COMPARAR
                                _repOportunidad.Update(oportunidad);
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error al reasignar la oportunidad por por venta cruzada " + e.Message);
                        }
                    }
                }
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 04/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida los casos en los que puede convertirse en OD u OM la oportunidad
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        private void MetodoODyOM(int IdOportunidad)
        {
            OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);
            //Valores Generales
            var oportunidad = _repOportunidad.FirstById(IdOportunidad);
            var asesorAsociado = new ResultadoDTO();
            int probabilidadActual = 0;
            var diaDto = new BloqueHorarioProcesaOportunidadBO();

            try
            {
                _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(IdOportunidad);
            }
            catch (Exception ex)
            {
            }

            bool enviarNotificacion = true; //para que no se envie notificacion a la agenda si cerramos la oportunidad en OM, OD
            //Valores Precalculados

            int idCentroCosto = oportunidad.IdCentroCosto.Value;
            int idCategoriaOrigen = oportunidad.IdCategoriaOrigen;


            int idProgramaGeneral = _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral == null ? 0 : _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral.Value;

            int idProbRegisPW = _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual == null ? 0 : _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual.Value;
            string pesoDescripcion = _repProbabilidadRegistroPw.FirstBy(w => w.Id == idProbRegisPW).Nombre;
            //Peso de la Probabilidad de la Oportunidad
            int pesoOportunidad = pesoDescripcion == "Muy Alta" ? 2 : ((pesoDescripcion == "Alta" || pesoDescripcion == "Media") ? 1 : 0);
            //Peso de la Categoria de la Oportunidad
            int pesoCategoriaOportunidad = _repCategoriaOrigen.FirstBy(w => w.Id == idCategoriaOrigen).Meta;

            var objetoAlumno = _repAlumno.FirstById(oportunidad.IdAlumno);
            /*Caracter especial para evitar registros coincidentes con celular vacio*/
            objetoAlumno.Celular = string.IsNullOrEmpty(objetoAlumno.Celular) ? "-|!x!|-" : objetoAlumno.Celular.Trim();
            ///////////////////////////////////// CASO 1:Valida si hay oportunidades en IS o M //////////////////////////////////////////////////////
            var listaISM = _repOportunidad.ValidarOportunidadesISM(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaISM.Count() > 0)//Si hay un IS o M me tengo que cerrar
            {
                int[] listaOportunidadesISM = new int[] { oportunidad.Id };
                this.CerrarOportunidadesOD(listaOportunidadesISM, "System Duplicado");//FALTA IMPLEMENTAR

            }
            //////////////////////////////////// CASO 2:Valida si hay opotunidades con mayor Probabilidad //////////////////////////////////////////
            var listaProbabilidades = _repOportunidad.ValidarOportunidadesProbabilidad(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaProbabilidades.Count() > 0)//Si hay oportunidades con el mismo alumno y del mismo programa
            {
                if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)//Si alguno con mayor probabilidad que el actual me tengo que cerrar
                {
                    int[] listaOportunidadesProbabilidades = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesProbabilidades, "Sys Duplicado Prob");//FALTA IMPLEMENTAR

                }
                else if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad == pesoOportunidad)//Si tienen una probabilidad igual que el actual no se hace nada
                {
                    //nada
                }
                else//Significa son de menor probablidad que el actual y deben cerrarse
                {
                    int[] listaOportunidadesProbabilidades = new int[listaProbabilidades.Count()];
                    int contador = 0;
                    foreach (var item in listaProbabilidades)
                    {
                        listaOportunidadesProbabilidades[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesProbabilidades, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                }
            }

            var listaCategorias = _repOportunidad.ValidarOportunidadesCategoria(oportunidad.IdAlumno, oportunidad.Id, objetoAlumno.Celular);
            //OM-OD
            var listaCategoriasOM = listaCategorias.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList();
            var listaCategoriasOD = listaCategorias.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList();
            if (listaCategoriasOM.Count() > 0)
            {
                if (listaCategoriasOM.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                        enviarNotificacion = false;

                    int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOM(listaOportunidadesCategorias, "UsuarioOM");

                }
                else if (listaCategoriasOD.Count() > 0)
                {
                    if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                    }
                    else
                    {
                        //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        _repOportunidad.Update(oportunidad);

                        //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                        var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {
                            this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                        //Mandamos la lista de oportunidades anteriores a Cerrar OD
                        int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasOD)
                        {
                            listaOportunidadesCategorias[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                    }
                }
            }
            else if (listaCategoriasOD.Count() > 0)
            {
                if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                        enviarNotificacion = false;

                    int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                }
                else
                {
                    //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    _repOportunidad.Update(oportunidad);

                    //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                    var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                    if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                    {

                        this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                        oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                    }
                    //Mandamos la lista de oportunidades anteriores a Cerrar OD
                    int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasOD)
                    {
                        listaOportunidadesCategorias[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                }

            }
            //////////////////////////////////// CASO 4:Valida si hay opotunidades con mayor Categoria en Fase IP ////////////////////////////////
            var listaCategoriasIPS = _repOportunidad.ValidarOportunidadesCategoriaIPMismoPG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasIPS.Count() > 0)
            {
                if (listaCategoriasIPS.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                {
                    //primero me reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                    oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;
                    enviarNotificacion = false;
                    int[] listaOportunidadesCategoriasIP = new int[] { oportunidad.Id };
                    //segundo cerrarme
                    this.CerrarOportunidadesOD(listaOportunidadesCategoriasIP, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                }
                else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                {
                    //primero me  reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                    oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;

                    //segundo cierro la otras IPS
                    int[] listaOportunidadesIPCerrar = new int[listaCategoriasIPS.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasIPS)
                    {
                        listaOportunidadesIPCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesIPCerrar, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                    //tercero me paso a IP
                    //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = 8;//IP
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    _repOportunidad.Update(oportunidad);

                }
            }
            //////////////////////////////////// CASO 5:Valida si hay opotunidades con mayor Categoria en Fase IP de Otros Programas///////////////
            var listaCategoriasIPSPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaIPDiferentePG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasIPSPGDiferente.Count() > 0)
            {
                if (listaCategoriasIPSPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                {
                    //primero me reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                    oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;
                    enviarNotificacion = false;
                    int[] listaOportunidadesCategoriasIPPGDiferente = new int[] { oportunidad.Id };
                    //segundo cerrarme
                    this.CerrarOportunidadesOD(listaOportunidadesCategoriasIPPGDiferente, "UsuarioOM");//FALTA IMPLEMENTAR

                }
                else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                {
                    //primero me  reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                    oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;

                    //segundo cierro la otras IPS
                    int[] listaOportunidadesIPPGDiferenteCerrar = new int[listaCategoriasIPSPGDiferente.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasIPSPGDiferente)
                    {
                        listaOportunidadesIPPGDiferenteCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOM(listaOportunidadesIPPGDiferenteCerrar, "UsuarioOM");

                    //tercero me paso a IP
                    //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = 8;//IP
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    _repOportunidad.Update(oportunidad);

                }
            }
            //////////////////////////////////// CASO 6:Valida si hay opotunidades con mayor Categoria en Fase RN,IT,BNC de Otros Programas///////////////
            var listaCategoriasBNCITRNPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaBNCITRNDiferentePG(oportunidad.IdAlumno, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasBNCITRNPGDiferente.Count() > 0)
            {
                if (listaCategoriasBNCITRNPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria < pesoCategoriaOportunidad)
                {
                    //Cerras las oportunidades que llegan
                    int[] listaOportunidadesBNCITRNPGDiferenteCerrar = new int[listaCategoriasBNCITRNPGDiferente.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasBNCITRNPGDiferente)
                    {
                        listaOportunidadesBNCITRNPGDiferenteCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    //Me actualizo a la fase de la oportunidad anterior
                    oportunidad.IdFaseOportunidad = listaCategoriasBNCITRNPGDiferente.FirstOrDefault().IdFaseOportunidad;
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    _repOportunidad.Update(oportunidad);

                    var oportunidadAnterior = listaCategoriasBNCITRNPGDiferente.Where(w => w.IdPersonalAsignado != 125).FirstOrDefault();
                    if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado)
                    {
                        if (oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {
                            this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                    }
                    this.CerrarOportunidadesOM(listaOportunidadesBNCITRNPGDiferenteCerrar, "UsuarioOM");//FALTA IMPLEMENTAR
                }
            }

            #region Marcado validacion correcta
            try
            {
                _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, true);
            }
            catch (Exception ex)
            {
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarValidacionOportunidadODYM", Parametros = $"IdOportunidad={IdOportunidad}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "VALIDATE ODYM", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
            }
            #endregion


        }

        /// <summary>
        /// Reasigna las oportunidades enviadas como referencia
        /// </summary>
        /// <param name="Oportunidad">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="IdNuevoAsesor">Id del nuevo asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="EnviaCorreo">Flag para determinar si se enviara un correo o no</param>
        /// <returns>Bool</returns>
        private bool ReasignarOportunidades(ref OportunidadBO Oportunidad, int IdNuevoAsesor, bool EnviaCorreo)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    OportunidadLogBO nuevoLog = new OportunidadLogBO()
                    {
                        Id = 0,
                        IdPersonalAsignado = Oportunidad.IdPersonalAsignado,
                        IdAsesorAnt = Oportunidad.IdPersonalAsignado,
                        IdContacto = Oportunidad.IdAlumno,
                        IdFaseOportunidad = Oportunidad.IdFaseOportunidad,
                        IdFaseOportunidadAnt = Oportunidad.IdFaseOportunidad,
                        IdOportunidad = Oportunidad.Id,
                        IdCentroCosto = Oportunidad.IdCentroCosto,
                        IdCentroCostoAnt = Oportunidad.IdCentroCosto,
                        IdOrigen = Oportunidad.IdOrigen,
                        IdTipoDato = Oportunidad.IdTipoDato,
                        FechaLog = DateTime.Now,
                        FechaFinLog = DateTime.Now,
                        FechaCambioFase = DateTime.Now,
                        FechaCambioFaseAnt = DateTime.Now,
                        FechaCambioFaseIs = null,
                        CambioFase = true,
                        CambioFaseIs = false,
                        Comentario = Oportunidad.UltimoComentario,
                        IdConjuntoAnuncio = Oportunidad.IdConjuntoAnuncio,
                        FechaRegistroCampania = Oportunidad.FechaRegistroCampania,
                        CicloRn2 = 1,
                        CambioFaseAsesor = 1,
                        FechaCambioAsesor = DateTime.Now,
                        FechaCambioAsesorAnt = DateTime.Now,
                        IdCategoriaOrigen = Oportunidad.IdCategoriaOrigen,
                        IdSubCategoriaDato = Oportunidad.IdSubCategoriaDato,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    if (IdNuevoAsesor != 0 && IdNuevoAsesor != -1 && (Oportunidad.IdPersonalAsignado != IdNuevoAsesor))
                    {
                        Oportunidad.OportunidadLogAntigua = null;
                        Oportunidad.OportunidadLogNueva = null;
                        Oportunidad.OportunidadLogNueva = nuevoLog;
                        ActualizarOportunidadVentaCruzada(ref Oportunidad, IdNuevoAsesor, "UsuarioReasignacion", EnviaCorreo, true);
                    }
                    scope.Complete();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void ActualizarOportunidadVentaCruzada(ref OportunidadBO oportunidad, int idAsesorReasignacion, string usuario, bool enviaCorreo, bool permaneceEstado)
        {

            PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
            try
            {
                //obtenemos los datos de la oportunidad
                var datosOportunidadReasignacion = _repOportunidad.ObtenerDatosOportunidadReasignacion(oportunidad.Id);
                PersonalMinReasignacionDTO asesorAntiguo = new PersonalMinReasignacionDTO
                {
                    IdAsesor = datosOportunidadReasignacion.IdAsesor,
                    EmailAsesor = datosOportunidadReasignacion.EmailAsesor,
                    EmailJefe = datosOportunidadReasignacion.EmailAsesor,
                    IdJefe = datosOportunidadReasignacion.IdJefe,
                    NombreCompletoAsesor = datosOportunidadReasignacion.NombreCompletoAsesor,
                    NombreCompletoJefe = datosOportunidadReasignacion.NombreCompletoJefe
                };
                FaseOportunidadReasignacionDTO faseOportunidad = new FaseOportunidadReasignacionDTO()
                {
                    Codigo = datosOportunidadReasignacion.CodigoFaseOportunidad
                };
                var datosAsesorNuevo = _repPersonal.ObtenerPersonalReasignacion(idAsesorReasignacion);
                AlumnoReasignacionDTO alumnoReasignacion = new AlumnoReasignacionDTO()
                {
                    Nombre1 = datosOportunidadReasignacion.Nombre1,
                    Nombre2 = datosOportunidadReasignacion.Nombre1,
                    ApellidoPaterno = datosOportunidadReasignacion.ApellidoPaterno,
                    ApellidoMaterno = datosOportunidadReasignacion.ApellidoMaterno
                };
                //fin obtenemos los datos de la oportunidad 200418
                using (TransactionScope scope = new TransactionScope())
                {
                    //actualizamos la oportunidad
                    this.ActualizarOportunidadVentaCruzadaAsesor(ref oportunidad, idAsesorReasignacion, permaneceEstado, usuario);
                    //Actualizamos en nuevo log

                    oportunidad.OportunidadLogNueva.IdPersonalAsignado = idAsesorReasignacion;
                    oportunidad.OportunidadLogNueva.FechaLog = DateTime.Now;

                    _repOportunidad.Update(oportunidad);//actualizamos el log      
                    //Registramos la asignacion con los nuevos datos
                    this.ActualizarAsignacionOportunidad(oportunidad.Id, idAsesorReasignacion, oportunidad.IdCentroCosto.Value, oportunidad.IdAlumno, usuario);
                    //actualizamos la asignacion 
                    scope.Complete();
                }
                try
                {
                    //enviamos correo de la reasignacion 200418
                    if (enviaCorreo == true)
                    {
                        this.EnvioCorreoReasignacion(oportunidad, asesorAntiguo, datosAsesorNuevo, alumnoReasignacion, faseOportunidad);
                    }
                }
                catch (Exception)
                {
                    //si no se envio el correopar que igual se crre la oportunidad
                }
                //fin enviamos correo de la reasignacion 200418
            }
            catch (Exception e)
            {
                throw new Exception("Me cai al actualizar el asesor por venta cruzada con envio correo", e);
            }
        }

        /// Tipo de funcion: Interna
        /// <summary>
        /// Actualizar los campos de asignacion oportunidad 
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="IdAsesorReasignacion">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="IdCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="IdAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        private void ActualizarAsignacionOportunidad(int IdOportunidad, int IdAsesorReasignacion, int IdCentroCosto, int IdAlumno, string Usuario)
        {
            AsignacionOportunidadRepositorio _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
            AsignacionOportunidadLogRepositorio _repAsignacionOportunidadLog = new AsignacionOportunidadLogRepositorio(_integraDBContext);
            var asignacionOportunidad = _repAsignacionOportunidad.GetBy(x => x.IdOportunidad == IdOportunidad).FirstOrDefault();

            if (asignacionOportunidad != null)
            {
                //Actualizamos la asignacion
                asignacionOportunidad.IdPersonal = IdAsesorReasignacion;
                asignacionOportunidad.UsuarioModificacion = Usuario;
                _repAsignacionOportunidad.Update(asignacionOportunidad);
            }
            var logNuevo = new AsignacionOportunidadLogBO
            {
                FechaLog = DateTime.Now,
                Id = 0,
                IdPersonalAnterior = asignacionOportunidad.IdPersonal,
                IdAsignacionOportunidad = asignacionOportunidad.Id,
                IdCentroCostoAnt = asignacionOportunidad.IdCentroCosto,
                IdOportunidad = asignacionOportunidad.IdOportunidad,
                IdTipoDato = asignacionOportunidad.IdTipoDato,
                IdFaseOportunidad = asignacionOportunidad.IdFaseOportunidad,

                IdAlumno = IdAlumno == 0 ? asignacionOportunidad.IdAlumno : IdAlumno,
                IdPersonal = IdAsesorReasignacion == 0 ? asignacionOportunidad.IdPersonal : IdAsesorReasignacion,
                IdCentroCosto = IdCentroCosto == 0 ? asignacionOportunidad.IdCentroCosto : IdCentroCosto,
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = Usuario,
                UsuarioModificacion = Usuario,
                IdClasificacionPersona = asignacionOportunidad.IdClasificacionPersona
            };
            _repAsignacionOportunidadLog.Insert(logNuevo);
        }

        /// <summary>
        /// Actualiza el flag de venta cruzada y actualizar el asesor al que pertenece la oportunidad
        /// </summary>
        /// <param name="Oportunidad">Referencia a objeto de clase OportunidadBO </param>
        /// <param name="IdAsesorReasignacion">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="PermaneceEstado">Flag para validar si permanece el estado anterior</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        private void ActualizarOportunidadVentaCruzadaAsesor(ref OportunidadBO Oportunidad, int IdAsesorReasignacion, bool PermaneceEstado, string Usuario)
        {
            if (PermaneceEstado == true)//viene de reasignacion OD
            {
                Oportunidad.IdPersonalAsignado = IdAsesorReasignacion == 0 ? Oportunidad.IdPersonalAsignado : IdAsesorReasignacion;
                Oportunidad.FlagVentaCruzada = null; //marcamos esta oportunidad para poder darles seguimiento y mostrarlas en el reporte de metas
                Oportunidad.FechaModificacion = DateTime.Now;
                Oportunidad.UsuarioModificacion = Usuario;
            }
            else//logica normal
            {
                Oportunidad.IdPersonalAsignado = IdAsesorReasignacion == 0 ? Oportunidad.IdPersonalAsignado : IdAsesorReasignacion;
                Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                Oportunidad.FlagVentaCruzada = 1; //marcamos esta oportunidad para poder darles seguimiento y mostrarlas en el reporte de metas
                Oportunidad.FechaModificacion = DateTime.Now;
                Oportunidad.UsuarioModificacion = Usuario;
            }
        }

        /// <summary>
        /// Envia un correo, cuando se reasigna la oportunidad
        /// </summary>
        /// <param name="Oportunidad">Objeto de clase OportunidadBO</param>
        /// <param name="PersonalAntiguo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="PersonalNuevo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="Alumno">Objeto de clase AlumnoReasignacionDTO</param>
        /// <param name="FaseOportunidad">Objeto de clase FaseOportunidadReasignacionDTO</param>
        private void EnvioCorreoReasignacion(OportunidadBO Oportunidad, PersonalMinReasignacionDTO PersonalAntiguo, PersonalMinReasignacionDTO PersonalNuevo, AlumnoReasignacionDTO Alumno, FaseOportunidadReasignacionDTO FaseOportunidad)
        {
            string MessageBody, AsuntoMensaje;
            try
            {
                MessageBody = "<p>" +
                    "Estimado " + PersonalNuevo.NombreCompletoAsesor + "<br/><br/>" +
                    "Se te ha reasignado el contacto " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno) + " en fase <b>" + FaseOportunidad.Codigo + "</b> que estaba asignado al asesor " + PersonalAntiguo.NombreCompletoAsesor + "<br/>" +
                    "Verifica las llamadas y la informacion previa registradas en el sistema para que puedas continuar de manera adecuada el proceso de venta.<br/><br/>" +
                    "Saludos <br/>" +
                    "Integra Reasignacion" +
                    "</p>";
                AsuntoMensaje = "Reasignacion de Oportunidad " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno).ToUpper(CultureInfo.InvariantCulture);
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO data = new TMKMailDataDTO
                {
                    Sender = "reasignacion@bsginstitute.com",
                    Recipient = PersonalNuevo.EmailAsesor,
                    Cc = PersonalNuevo.EmailJefe,
                    Subject = AsuntoMensaje,
                    Message = MessageBody,
                    RemitenteC = "Integra - Reasignacion Automatica "
                };

                Mailservice.SetData(data);
                var resultado = Mailservice.VerifyData();
                Mailservice.SendMessageTask();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Llena los datos necesarios de la oportunidad y generar sus hijos
        /// </summary>
        /// <param name="oportunidad">Objeto de clase OportunidadBO</param>
        private void LlenarOportunidadHijos(OportunidadBO oportunidad)
        {
            OrigenRepositorio _repOrigen = new OrigenRepositorio(_integraDBContext);
            CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio(_integraDBContext);
            AsignacionOportunidadRepositorio _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
            CampaniaMailingDetalleRepositorio _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio(_integraDBContext);

            oportunidad.IdPagina = 1;
            if (oportunidad.IdOrigen == 0)
                throw new Exception("La Oportunidad no tiene Origen.");

            var CategoriaOrigen = _repOrigen.ObtenerIdCategoriaOrigenPorOrigen(oportunidad.IdOrigen);

            if (CategoriaOrigen.IdCategoriaOrigen == 0)
                throw new Exception("No se encontro origen no se puede crear categoria.");

            oportunidad.IdCategoriaOrigen = oportunidad.IdCategoriaOrigen != 0 ? oportunidad.IdCategoriaOrigen : CategoriaOrigen.IdCategoriaOrigen;
            if (oportunidad.IdSubCategoriaDato == 0)
            {
                var categoriadato = _repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato(oportunidad.IdCategoriaOrigen, oportunidad.IdTipoInteraccion);
                if (categoriadato != null)
                    oportunidad.IdSubCategoriaDato = categoriadato.IdSubCategoriaDato != 0 ? categoriadato.IdSubCategoriaDato : 0;
            }
            if (!string.IsNullOrEmpty(oportunidad.CodMailing))
            {
                var campaniaMailing = _repCampaniaMailingDetalle.ObtenerIdCampaniaMailing(oportunidad.CodMailing);
                if (campaniaMailing != null)
                {
                    oportunidad.IdConjuntoAnuncio = campaniaMailing.IdCampaniaMailing;
                }
            }
            oportunidad.IdFaseOportunidadInicial = oportunidad.IdFaseOportunidad;
            oportunidad.IdFaseOportunidadMaxima = oportunidad.IdFaseOportunidad;
            oportunidad.IdEstadoOportunidad = oportunidad.UltimaFechaProgramada == null || oportunidad.UltimaFechaProgramada.Equals("              ") || oportunidad.UltimaFechaProgramada.Equals("00000000000000") ? ValorEstatico.IdEstadoOportunidadNoProgramada : ValorEstatico.IdEstadoOportunidadProgramada;

            OportunidadLogBO oportunidadLog = new OportunidadLogBO()
            {

                IdPersonalAsignado = oportunidad.IdPersonalAsignado,
                IdAsesorAnt = oportunidad.IdPersonalAsignado,
                IdContacto = oportunidad.IdAlumno,
                IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad,
                IdOportunidad = oportunidad.Id,
                IdCentroCosto = oportunidad.IdCentroCosto,
                IdCentroCostoAnt = oportunidad.IdCentroCosto,
                IdOrigen = oportunidad.IdOrigen,
                IdTipoDato = oportunidad.IdTipoDato,
                FechaLog = DateTime.Now,
                FechaFinLog = DateTime.Now,
                FechaCambioFase = DateTime.Now,
                FechaCambioFaseAnt = DateTime.Now,
                CambioFase = true,
                CambioFaseIs = false,
                Comentario = oportunidad.UltimoComentario,
                IdConjuntoAnuncio = oportunidad.IdConjuntoAnuncio,
                FechaRegistroCampania = oportunidad.FechaRegistroCampania,
                CicloRn2 = 1,
                CambioFaseAsesor = 1,
                FechaCambioAsesor = DateTime.Now,
                FechaCambioAsesorAnt = DateTime.Now,
                IdCategoriaOrigen = oportunidad.IdCategoriaOrigen,
                IdSubCategoriaDato = oportunidad.IdSubCategoriaDato,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                IdPersonalAreaTrabajo = oportunidad.IdPersonalAreaTrabajo

            };
            oportunidad.OportunidadLogAntigua = oportunidadLog;

            ActividadDetalleBO actividadDetalle = new ActividadDetalleBO()
            {
                FechaProgramada = oportunidad.UltimaFechaProgramada,
                Actor = "A",
                Comentario = "Sin Comentario",
                IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(oportunidad.IdFaseOportunidad, oportunidad.IdTipoDato, oportunidad.IdPersonalAreaTrabajo, oportunidad.IdActividadCabeceraUltima),
                IdAlumno = oportunidad.IdAlumno,
                IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleNoEjecutado,
                IdOportunidad = 1,//oportunidad.Id
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona
            };
            if (oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookPreLanC2FormularioPropio)//Facebook PreLan C2 Formulario Propio 360
            {
                actividadDetalle.IdActividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProgPrelan;//15;
            }

            _repActividadDetalle.Insert(actividadDetalle);


            oportunidad.ActividadNueva = actividadDetalle;
            oportunidad.ActividadAntigua = null;

            oportunidad.UltimoComentario = oportunidad.UltimoComentario == null ? actividadDetalle.Comentario : oportunidad.UltimoComentario;
            oportunidad.IdActividadCabeceraUltima = actividadDetalle.IdActividadCabecera == null ? 0 : actividadDetalle.IdActividadCabecera.Value;
            oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
            //int divisor = 0;
            //var resultado = 1 / divisor;
            oportunidad.UltimaFechaProgramada = actividadDetalle.FechaProgramada;
            oportunidad.IdEstadoActividadDetalleUltimoEstado = actividadDetalle.IdEstadoActividadDetalle;
            //Registramos la asignacion de oportunidad
            AsignacionOportunidadBO asignacionOportunidad = null;
            if (_repAsignacionOportunidad.Exist(oportunidad.Id))
            {
                asignacionOportunidad = _repAsignacionOportunidad.FirstById(oportunidad.Id);
            }
            if (asignacionOportunidad == null)
            {
                asignacionOportunidad = new AsignacionOportunidadBO
                {
                    FechaAsignacion = DateTime.Now,
                    IdAlumno = oportunidad.IdAlumno,
                    IdPersonal = oportunidad.IdPersonalAsignado,
                    //IdCentroCosto = oportunidad.IdCentroCosto.Value,
                    IdCentroCosto = oportunidad.IdCentroCosto is null ? default : oportunidad.IdCentroCosto.Value,
                    IdOportunidad = oportunidad.Id,
                    IdTipoDato = oportunidad.IdTipoDato,
                    IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                    UsuarioCreacion = oportunidad.UsuarioCreacion,
                    UsuarioModificacion = oportunidad.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                };
            }
            else
            {
                asignacionOportunidad.IdPersonal = oportunidad.IdPersonalAsignado == 0 ? asignacionOportunidad.IdPersonal : oportunidad.IdPersonalAsignado;
                asignacionOportunidad.IdCentroCosto = oportunidad.IdCentroCosto == 0 ? asignacionOportunidad.IdCentroCosto : oportunidad.IdCentroCosto.Value;
                asignacionOportunidad.IdAlumno = oportunidad.IdAlumno == 0 ? asignacionOportunidad.IdAlumno : oportunidad.IdAlumno;
                asignacionOportunidad.FechaAsignacion = DateTime.Now;
                asignacionOportunidad.FechaModificacion = DateTime.Now;
                asignacionOportunidad.UsuarioModificacion = oportunidad.UsuarioCreacion;
                asignacionOportunidad.UsuarioCreacion = oportunidad.UsuarioCreacion;
                asignacionOportunidad.Estado = true;
                asignacionOportunidad.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
            }

            AsignacionOportunidadLogBO asignacionOportunidadLog = new AsignacionOportunidadLogBO
            {
                FechaLog = DateTime.Now,
                IdPersonalAnterior = asignacionOportunidad.IdPersonal,
                IdAsignacionOportunidad = asignacionOportunidad.Id,
                IdCentroCostoAnt = asignacionOportunidad.IdCentroCosto,
                IdOportunidad = asignacionOportunidad.Id,
                IdTipoDato = asignacionOportunidad.IdTipoDato,
                IdFaseOportunidad = asignacionOportunidad.IdFaseOportunidad,
                IdAlumno = asignacionOportunidad.IdAlumno,
                IdPersonal = asignacionOportunidad.IdPersonal,
                IdCentroCosto = asignacionOportunidad.IdCentroCosto,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona
            };
            asignacionOportunidad.AsignacionOportunidadLog = asignacionOportunidadLog;
            oportunidad.AsignacionOportunidad = asignacionOportunidad;
        }

        [Route("[Action]/{idOportunidad}/{idPersonalAsignado}")]
        [HttpGet]
        public ActionResult NuevaActividad(int idOportunidad, int idPersonalAsignado)//Pruebas
        {
            AgendaSocket.getInstance().NuevaActividadParaEjecutar(idOportunidad, idPersonalAsignado);
            return Ok();
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearAlumnoOportunidadMessengerChat([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoBO alumno = new AlumnoBO();
                alumno.Nombre1 = Formulario.Alumno.Nombre1;
                alumno.Nombre2 = Formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = Formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = Formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = Formulario.Alumno.Direccion;
                alumno.Telefono = Formulario.Alumno.Telefono;
                alumno.Celular = Formulario.Alumno.Celular;
                alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = Formulario.Alumno.Email2;
                alumno.IdCargo = Formulario.Alumno.IdCargo;
                alumno.IdAformacion = Formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = Formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = Formulario.Alumno.IdIndustria;
                alumno.IdReferido = Formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = Formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = Formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = Formulario.Alumno.HoraContacto;
                alumno.HoraPeru = Formulario.Alumno.HoraPeru;
                alumno.IdEmpresa = Formulario.Alumno.IdEmpresa;
                alumno.Estado = true;
                alumno.FechaCreacion = DateTime.Now;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioCreacion = Formulario.Usuario;
                alumno.UsuarioModificacion = Formulario.Usuario;


                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                OportunidadBO oportunidad = new OportunidadBO();
                oportunidad.IdCentroCosto = Formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = Formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = Formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = Formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.FechaRegistroCampania = DateTime.Now;
                oportunidad.Estado = true;
                oportunidad.UsuarioCreacion = Formulario.Usuario;
                oportunidad.UsuarioModificacion = Formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;

                if (oportunidad.UltimaFechaProgramada != null)

                    oportunidad.IdEstadoOportunidad = 6;    //Programada
                else
                    oportunidad.IdEstadoOportunidad = 2;    //No programada

                this.CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Alumno);

                //Insertar actualizar alumno a v3
                try
                {
                    //string URI = "http://localhost:4348/Marketing/SincronizarAlumnoAV3?IdAlumno=" + oportunidad.Alumno.Id.ToString() + "&EsCrear=true";
                    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + oportunidad.Id;
                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(URI);
                    }
                    if (oportunidad.IdPersonalAsignado != 125 && (oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookCorreo || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookComentarios || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookInbox || oportunidad.IdCategoriaOrigen == 533))
                    {
                        var idmigracion = _repOportunidad.FirstById(oportunidad.Id).IdMigracion;
                        URI = "http://localhost:63400 /crmVentas3.1/AsignacionAutomatica2/EnviarCorreoDesdeV4?Id=" + idmigracion.ToString() + "&IdAsesor=" + oportunidad.IdPersonalAsignado;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                }
                catch (Exception e)
                {
                    //throw;
                }

                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFacebookUsuarioOportunidad([FromBody] FacebookUsuarioOportunidadDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookUsuarioOportunidadRepositorio _repFacebookUsuarioOportunidad = new FacebookUsuarioOportunidadRepositorio();
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio();
                FacebookUsuarioOportunidadBO facebookUsuarioOportunidad = new FacebookUsuarioOportunidadBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    facebookUsuarioOportunidad.PSID = Formulario.PSID;
                    facebookUsuarioOportunidad.IdOportunidad = Formulario.IdOportunidad;
                    facebookUsuarioOportunidad.IdPersonal = Formulario.IdPersonal;
                    facebookUsuarioOportunidad.IdMessengerChat = Formulario.Asociar ? Formulario.IdMessengerChatMasivo : null;
                    facebookUsuarioOportunidad.Estado = true;
                    facebookUsuarioOportunidad.UsuarioCreacion = Formulario.Usuario;
                    facebookUsuarioOportunidad.UsuarioModificacion = Formulario.Usuario;
                    facebookUsuarioOportunidad.FechaCreacion = DateTime.Now;
                    facebookUsuarioOportunidad.FechaModificacion = DateTime.Now;
                    _repFacebookUsuarioOportunidad.Insert(facebookUsuarioOportunidad);
                    scope.Complete();
                }

                MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstById(Formulario.IdMessengerUsuario);
                if (messengerUsuarioBO.Id != 0)
                {
                    messengerUsuarioBO.Email = Formulario.Email;
                    messengerUsuarioBO.UsuarioModificacion = Formulario.Usuario;
                    messengerUsuarioBO.FechaModificacion = DateTime.Now;
                    messengerUsuarioRepositorio.Update(messengerUsuarioBO);
                }

                return Ok(new { Rpta = "Ok", Records = facebookUsuarioOportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult CrearOportunidadActualizarAlumnoMessengerChat([FromBody] RegistroOportunidadAlumnoDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var alumno = _repAlumno.FirstById(Formulario.Alumno.Id);
                alumno.Nombre1 = Formulario.Alumno.Nombre1;
                alumno.Nombre2 = Formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = Formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = Formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = Formulario.Alumno.Direccion;
                alumno.Telefono = Formulario.Alumno.Telefono;
                alumno.Celular = Formulario.Alumno.Celular;
                alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = Formulario.Alumno.Email2;
                alumno.IdCargo = Formulario.Alumno.IdCargo;
                alumno.IdAformacion = Formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = Formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = Formulario.Alumno.IdIndustria;
                alumno.IdReferido = Formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = Formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = Formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = Formulario.Alumno.HoraContacto;
                alumno.HoraPeru = Formulario.Alumno.HoraPeru;
                alumno.IdEmpresa = Formulario.Alumno.IdEmpresa;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioModificacion = Formulario.Usuario;


                OportunidadBO oportunidad = new OportunidadBO();
                oportunidad.IdCentroCosto = Formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = Formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = Formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = Formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = Formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = Formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = Formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.Estado = true;
                oportunidad.FechaRegistroCampania = DateTime.Now;
                oportunidad.UsuarioCreacion = Formulario.Usuario;
                oportunidad.UsuarioModificacion = Formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;
                oportunidad.IdAlumno = alumno.Id;

                if (oportunidad.UltimaFechaProgramada != null)

                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;    //Programada
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;    //No programada

                this.CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Alumno);

                this.MetodoODyOM(oportunidad.Id);

                //Insertar actualizar alumno a v3
                try
                {
                    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + oportunidad.Id;
                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(URI);
                    }
                    if (oportunidad.IdPersonalAsignado != 125 && (oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookCorreo || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookComentarios || oportunidad.IdCategoriaOrigen == ValorEstatico.IdCategoriaOrigenFacebookInbox || oportunidad.IdCategoriaOrigen == 533))
                    {
                        var idmigracion = _repOportunidad.FirstById(oportunidad.Id).IdMigracion;
                        URI = "http://localhost:63400 /crmVentas3.1/AsignacionAutomatica2/EnviarCorreoDesdeV4?Id=" + idmigracion.ToString() + "&IdAsesor=" + oportunidad.IdPersonalAsignado;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                }
                catch (Exception e)
                {
                }
                ///DESCOMENTAR PARA PRODUCCION
                //try
                //{
                //    string URI = "http://localhost:4348/Comercial/SincronizarCrearActualizarOportunidadHijos?IdOportunidad=" + oportunidad.Id.ToString();
                //    using (WebClient wc = new WebClient())
                //    {
                //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                //        wc.DownloadString(URI);
                //    }
                //}
                //catch (Exception e)
                //{
                //    //throw;
                //}
                return Ok(new { Rpta = "Ok", Records = oportunidad });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST 
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Finaliza la opprtunidad actual en fase RN1 y crea una nueva en fase IT
        /// </summary>
        /// <returns>Json</returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividadCrearOportunidad([FromBody] ParametroFinalizarActividadDTO OportunidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                HoraBloqueadaRepositorio _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraPersonalRepositorio _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                ComprobantePagoOportunidadRepositorio _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                ProcedenciaVentaCruzadaRepositorio _repProcedenciaVentaCruzada = new ProcedenciaVentaCruzadaRepositorio(_integraDBContext);

                OportunidadBO OportunidadNueva = new OportunidadBO(_integraDBContext);

                OportunidadBO Oportunidad = new OportunidadBO(OportunidadDTO.ActividadAntigua.IdOportunidad.Value, OportunidadDTO.Usuario, _integraDBContext);

                #region Desactivado de redireccion anterior
                try
                {
                    _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(Oportunidad.Id);
                }
                catch (Exception ex)
                {
                }
                #endregion

                //Oportunidad.IdCentroCosto = dto.datosOportunidad.IdCentroCosto;
                Oportunidad.IdPersonalAsignado = OportunidadDTO.datosOportunidad.IdPersonalAsignado;
                //Oportunidad.IdTipoDato = dto.datosOportunidad.IdTipoDato;
                Oportunidad.IdFaseOportunidad = OportunidadDTO.datosOportunidad.IdFaseOportunidad;
                //Oportunidad.IdOrigen = dto.datosOportunidad.IdOrigen;
                Oportunidad.IdAlumno = OportunidadDTO.datosOportunidad.IdAlumno;
                if (OportunidadDTO.datosOportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.UltimaFechaProgramada = DateTime.Parse(OportunidadDTO.datosOportunidad.UltimaFechaProgramada);
                }

                Oportunidad.UltimoComentario = OportunidadDTO.datosOportunidad.UltimoComentario;
                Oportunidad.IdSubCategoriaDato = OportunidadDTO.datosOportunidad.IdSubCategoriaDato;

                if (Oportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }


                var faseNueva = OportunidadDTO.datosOportunidad.IdFaseOportunidad;
                Oportunidad.IdFaseOportunidad = OportunidadDTO.IdFaseOportunidad;
                // Finalizar Actividad
                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = OportunidadDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = OportunidadDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = OportunidadDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = OportunidadDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = OportunidadDTO.ActividadAntigua.DuracionReal;
                ActividadAntigua.IdOcurrencia = OportunidadDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = OportunidadDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = OportunidadDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = OportunidadDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = OportunidadDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = OportunidadDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = OportunidadDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = OportunidadDTO.ActividadAntigua.RefLlamada;
                ActividadAntigua.IdOcurrenciaActividad = OportunidadDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(OportunidadDTO.ActividadAntigua.Id);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO();
                    OportunidadCompetidor.IdOportunidad = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.IdOportunidad;
                    OportunidadCompetidor.OtroBeneficio = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.OtroBeneficio;
                    OportunidadCompetidor.Respuesta = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Respuesta;
                    OportunidadCompetidor.Completado = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Completado;
                    OportunidadCompetidor.FechaCreacion = DateTime.Now;
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    OportunidadCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    OportunidadCompetidor.Estado = true;
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Id);
                }

                CalidadProcesamientoBO CalidadBO = new CalidadProcesamientoBO();
                CalidadBO.IdOportunidad = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad;
                CalidadBO.PerfilCamposLlenos = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos;
                CalidadBO.PerfilCamposTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal;
                CalidadBO.Dni = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni;
                CalidadBO.PgeneralValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados;
                CalidadBO.PgeneralTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal;
                CalidadBO.PespecificoValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados;
                CalidadBO.PespecificoTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal;
                CalidadBO.BeneficiosValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados;
                CalidadBO.BeneficiosTotales = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales;
                CalidadBO.CompetidoresVerificacion = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion;
                CalidadBO.ProblemaSeleccionados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados;
                CalidadBO.ProblemaSolucionados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados;
                CalidadBO.FechaCreacion = DateTime.Now;
                CalidadBO.FechaModificacion = DateTime.Now;
                CalidadBO.UsuarioCreacion = OportunidadDTO.Usuario;
                CalidadBO.UsuarioModificacion = OportunidadDTO.Usuario;
                CalidadBO.Estado = true;
                if (!CalidadBO.HasErrors)
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                else
                    return BadRequest(CalidadBO.GetErrors(null));
                if (!OportunidadCompetidor.HasErrors)
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                else
                    return BadRequest(OportunidadCompetidor.GetErrors(null));

                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaPrerequisitoGeneral)
                {
                    OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                    ListaPrerequisitoGeneral.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoGeneral.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;

                    ListaPrerequisitoGeneral.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoGeneral.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoGeneral.Estado = true;

                    if (!ListaPrerequisitoGeneral.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    else
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));

                }

                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaPrerequisitoEspecifico)
                {
                    OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();
                    ListaPrerequisitoEspecifico.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoEspecifico.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoEspecifico.Estado = true;

                    if (!ListaPrerequisitoEspecifico.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    else
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                }
                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaBeneficio)
                {
                    OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                    ListaBeneficio.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaBeneficio.IdBeneficio = item.IdBeneficio;
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaCreacion = DateTime.Now;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaBeneficio.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaBeneficio.Estado = true;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                        return BadRequest(ListaBeneficio.GetErrors(null));
                }
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaCompetidor)
                {
                    DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                    ListaCompetidor.IdOportunidadCompetidor = 0;
                    ListaCompetidor.IdCompetidor = item;
                    ListaCompetidor.Estado = true;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaCompetidor.Estado = true;

                    if (!ListaCompetidor.HasErrors)
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    else
                        return BadRequest(ListaCompetidor.GetErrors(null));
                }
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaSoluciones)
                {
                    SolucionClienteByActividadBO ListaSoluciones = new SolucionClienteByActividadBO();
                    ListaSoluciones.IdOportunidad = item.IdOportunidad;
                    ListaSoluciones.IdActividadDetalle = item.IdActividadDetalle;
                    ListaSoluciones.IdCausa = item.IdCausa;
                    ListaSoluciones.IdPersonal = item.IdPersonal;
                    ListaSoluciones.Solucionado = item.Solucionado;
                    ListaSoluciones.IdProblemaCliente = item.IdProblemaCliente;
                    ListaSoluciones.OtroProblema = item.OtroProblema;
                    ListaSoluciones.FechaCreacion = DateTime.Now;
                    ListaSoluciones.FechaModificacion = DateTime.Now;
                    ListaSoluciones.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaSoluciones.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaSoluciones.Estado = true;

                    if (!ListaSoluciones.HasErrors)
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    else
                        return BadRequest(ListaSoluciones.GetErrors(null));
                }

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividad(false, OportunidadDTO.datosOportunidad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }
                            _repOportunidad.Update(Oportunidad);


                            OportunidadNueva.IdFaseOportunidad = faseNueva;
                            OportunidadNueva.IdPersonalAsignado = OportunidadDTO.datosOportunidad.IdPersonalAsignado;
                            OportunidadNueva.IdTipoDato = OportunidadDTO.datosOportunidad.IdTipoDato;
                            OportunidadNueva.IdOrigen = OportunidadDTO.datosOportunidad.IdOrigen;
                            OportunidadNueva.IdAlumno = OportunidadDTO.datosOportunidad.IdAlumno;
                            if (OportunidadDTO.datosOportunidad.UltimaFechaProgramada != null)
                            {
                                OportunidadNueva.UltimaFechaProgramada = DateTime.Parse(OportunidadDTO.datosOportunidad.UltimaFechaProgramada);

                            }
                            OportunidadNueva.UltimoComentario = OportunidadDTO.datosOportunidad.UltimoComentario;
                            OportunidadNueva.IdTipoInteraccion = OportunidadDTO.datosOportunidad.IdTipoInteraccion.Value;
                            OportunidadNueva.IdSubCategoriaDato = OportunidadDTO.datosOportunidad.IdSubCategoriaDato;
                            OportunidadNueva.IdCentroCosto = OportunidadDTO.datosOportunidad.IdCentroCosto;
                            OportunidadNueva.FechaRegistroCampania = Oportunidad.FechaRegistroCampania;
                            OportunidadNueva.Estado = true;
                            OportunidadNueva.FechaCreacion = DateTime.Now;
                            OportunidadNueva.FechaModificacion = DateTime.Now;
                            OportunidadNueva.UsuarioCreacion = OportunidadDTO.Usuario;
                            OportunidadNueva.UsuarioModificacion = OportunidadDTO.Usuario;
                            OportunidadNueva.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
                            OportunidadNueva.IdPersonalAreaTrabajo = Oportunidad.IdPersonalAreaTrabajo;

                            this.CrearOportunidad(ref OportunidadNueva, false, TipoPersona.Alumno);

                            _repProcedenciaVentaCruzada.InsertarProcedenciaVentaCruzada(Oportunidad.Id, OportunidadNueva.Id);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error VentaCruzada Transaction";
                            mailData.Message = "IdOportunidad: " + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + OportunidadDTO.Usuario == null ? "" : OportunidadDTO.Usuario + "<br/>" + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ///01/02/2021
                    ///Calculo nuevo modelo predictivo
                    ///Carlos Crispin Riquelme
                    try
                    {
                        var nuevaProbabilidad = OportunidadNueva.ObtenerProbabilidadModeloPredictivo(OportunidadNueva.Id);
                    }
                    catch (Exception e)
                    {

                    }

                    /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                    /////////////DESCOMENTAR PARA PRODUCCION
                    //////////////////Replicar oportunidad-ALUMNO a integra v3
                    ////////try
                    ////////{
                    ////////    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + OportunidadNueva.Id;
                    ////////    using (WebClient wc = new WebClient())
                    ////////    {
                    ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    ////////        wc.DownloadString(URI);
                    ////////    }
                    ////////}
                    ////////catch (Exception ex)
                    ////////{
                    ////////    List<string> correos = new List<string>();
                    ////////    correos.Add("sistemas@bsginstitute.com");

                    ////////    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    ////////    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    ////////    mailData.Sender = "jcayo@bsginstitute.com";
                    ////////    mailData.Recipient = string.Join(",", correos);
                    ////////    mailData.Subject = "Error VentaCruzada Sincronizacion";
                    ////////    mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.Usuario == null ? "" : dto.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    ////////    mailData.Cc = "";
                    ////////    mailData.Bcc = "";
                    ////////    mailData.AttachedFiles = null;

                    ////////    Mailservice.SetData(mailData);
                    ////////    Mailservice.SendMessageTask();
                    ////////}
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(OportunidadDTO.ActividadAntigua.Id);
                return Ok(new { ActividadEjecutada = realizadas, IdOportunidad = OportunidadDTO.ActividadAntigua.IdOportunidad.Value });
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error VentaCruzada General";
                mailData.Message = "IdOportunidad: " + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + OportunidadDTO.Usuario == null ? "" : OportunidadDTO.Usuario + "<br/>" + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST 
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Finaliza la opprtunidad actual en fase RN1 y crea una nueva en fase IT
        /// </summary>
        /// <returns>Json</returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividadCrearOportunidadAlterno([FromBody] ParametroFinalizarActividadAlternoDTO OportunidadDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                HoraBloqueadaRepositorio _repHoraBloqueada = new HoraBloqueadaRepositorio(_integraDBContext);
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamadaActividad = new LlamadaActividadRepositorio(_integraDBContext);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);
                ReprogramacionCabeceraRepositorio _repReprogramacionCabecera = new ReprogramacionCabeceraRepositorio(_integraDBContext);
                ReprogramacionCabeceraPersonalRepositorio _repReprogramacionCabeceraPersonal = new ReprogramacionCabeceraPersonalRepositorio(_integraDBContext);
                ComprobantePagoOportunidadRepositorio _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);
                ProcedenciaVentaCruzadaRepositorio _repProcedenciaVentaCruzada = new ProcedenciaVentaCruzadaRepositorio(_integraDBContext);

                OportunidadBO OportunidadNueva = new OportunidadBO(_integraDBContext);

                OportunidadBO Oportunidad = new OportunidadBO(OportunidadDTO.ActividadAntigua.IdOportunidad.Value, OportunidadDTO.Usuario, _integraDBContext);

                #region Desactivado de redireccion anterior
                try
                {
                    _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(Oportunidad.Id);
                }
                catch (Exception ex)
                {
                }
                #endregion

                //Oportunidad.IdCentroCosto = dto.datosOportunidad.IdCentroCosto;
                Oportunidad.IdPersonalAsignado = OportunidadDTO.datosOportunidad.IdPersonalAsignado;
                //Oportunidad.IdTipoDato = dto.datosOportunidad.IdTipoDato;
                Oportunidad.IdFaseOportunidad = OportunidadDTO.datosOportunidad.IdFaseOportunidad;
                //Oportunidad.IdOrigen = dto.datosOportunidad.IdOrigen;
                Oportunidad.IdAlumno = OportunidadDTO.datosOportunidad.IdAlumno;
                if (OportunidadDTO.datosOportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.UltimaFechaProgramada = DateTime.Parse(OportunidadDTO.datosOportunidad.UltimaFechaProgramada);
                }

                Oportunidad.UltimoComentario = OportunidadDTO.datosOportunidad.UltimoComentario;
                Oportunidad.IdSubCategoriaDato = OportunidadDTO.datosOportunidad.IdSubCategoriaDato;

                if (Oportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }


                var faseNueva = OportunidadDTO.datosOportunidad.IdFaseOportunidad;
                Oportunidad.IdFaseOportunidad = OportunidadDTO.IdFaseOportunidad;
                // Finalizar Actividad
                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = OportunidadDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = OportunidadDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = OportunidadDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = OportunidadDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = OportunidadDTO.ActividadAntigua.DuracionReal;
                //ActividadAntigua.IdOcurrencia = OportunidadDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = OportunidadDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = OportunidadDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = OportunidadDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = OportunidadDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = OportunidadDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = OportunidadDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = OportunidadDTO.ActividadAntigua.RefLlamada;
                //ActividadAntigua.IdOcurrenciaActividad = OportunidadDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
                ActividadAntigua.IdOcurrenciaAlterno = OportunidadDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdOcurrenciaActividadAlterno = OportunidadDTO.ActividadAntigua.IdOcurrenciaActividad;


                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(OportunidadDTO.ActividadAntigua.Id);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO();
                    OportunidadCompetidor.IdOportunidad = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.IdOportunidad;
                    OportunidadCompetidor.OtroBeneficio = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.OtroBeneficio;
                    OportunidadCompetidor.Respuesta = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Respuesta;
                    OportunidadCompetidor.Completado = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Completado;
                    OportunidadCompetidor.FechaCreacion = DateTime.Now;
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    OportunidadCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    OportunidadCompetidor.Estado = true;
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(OportunidadDTO.DatosCompuesto.OportunidadCompetidor.Id);
                }

                CalidadProcesamientoBO CalidadBO = new CalidadProcesamientoBO();
                CalidadBO.IdOportunidad = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad;
                CalidadBO.PerfilCamposLlenos = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos;
                CalidadBO.PerfilCamposTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal;
                CalidadBO.Dni = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni;
                CalidadBO.PgeneralValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados;
                CalidadBO.PgeneralTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal;
                CalidadBO.PespecificoValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados;
                CalidadBO.PespecificoTotal = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal;
                CalidadBO.BeneficiosValidados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados;
                CalidadBO.BeneficiosTotales = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales;
                CalidadBO.CompetidoresVerificacion = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion;
                CalidadBO.ProblemaSeleccionados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados;
                CalidadBO.ProblemaSolucionados = OportunidadDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados;
                CalidadBO.FechaCreacion = DateTime.Now;
                CalidadBO.FechaModificacion = DateTime.Now;
                CalidadBO.UsuarioCreacion = OportunidadDTO.Usuario;
                CalidadBO.UsuarioModificacion = OportunidadDTO.Usuario;
                CalidadBO.Estado = true;
                if (!CalidadBO.HasErrors)
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                else
                    return BadRequest(CalidadBO.GetErrors(null));
                if (!OportunidadCompetidor.HasErrors)
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                else
                    return BadRequest(OportunidadCompetidor.GetErrors(null));
                /*
                OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaPrerequisitoGeneral)
                {
                    OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                    ListaPrerequisitoGeneral.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoGeneral.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                    ListaPrerequisitoGeneral.Completado = item.Completado;

                    ListaPrerequisitoGeneral.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoGeneral.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoGeneral.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoGeneral.Estado = true;

                    if (!ListaPrerequisitoGeneral.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                    else
                        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));

                }

                OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaPrerequisitoEspecifico)
                {
                    OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();
                    ListaPrerequisitoEspecifico.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaPrerequisitoEspecifico.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                    ListaPrerequisitoEspecifico.Completado = item.Completado;
                    ListaPrerequisitoEspecifico.FechaCreacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                    ListaPrerequisitoEspecifico.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoEspecifico.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaPrerequisitoEspecifico.Estado = true;

                    if (!ListaPrerequisitoEspecifico.HasErrors)
                        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                    else
                        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                }
                OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaBeneficio)
                {
                    OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                    ListaBeneficio.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                    ListaBeneficio.IdBeneficio = item.IdBeneficio;
                    ListaBeneficio.Respuesta = item.Respuesta;
                    ListaBeneficio.Completado = item.Completado;
                    ListaBeneficio.FechaCreacion = DateTime.Now;
                    ListaBeneficio.FechaModificacion = DateTime.Now;
                    ListaBeneficio.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaBeneficio.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaBeneficio.Estado = true;
                    if (!ListaBeneficio.HasErrors)
                    {
                        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                    }
                    else
                        return BadRequest(ListaBeneficio.GetErrors(null));
                }*/
                //======================================
                ProgramaGeneralBeneficioRespuestaRepositorio _repBeneficioAlternoRespuesta = new ProgramaGeneralBeneficioRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioRespuestaBO beneficioAlterno = new ProgramaGeneralBeneficioRespuestaBO();
                var listaBeneficioAlternoAgrupado = OportunidadDTO.DatosCompuesto.ListaBeneficioAlterno.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAlternoAgrupado)
                {
                    beneficioAlterno = _repBeneficioAlternoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralBeneficio == item.IdBeneficio);

                    if (beneficioAlterno != null)
                    {
                        beneficioAlterno.Respuesta = item.Respuesta;
                        beneficioAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        beneficioAlterno.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Update(beneficioAlterno);
                    }
                    else
                    {
                        ProgramaGeneralBeneficioRespuestaBO beneficioAlternoV2 = new ProgramaGeneralBeneficioRespuestaBO();
                        beneficioAlternoV2.IdOportunidad = item.IdOportunidad;
                        beneficioAlternoV2.IdProgramaGeneralBeneficio = item.IdBeneficio;
                        beneficioAlternoV2.Respuesta = item.Respuesta;
                        beneficioAlternoV2.Estado = true;
                        beneficioAlternoV2.UsuarioCreacion = OportunidadDTO.Usuario;
                        beneficioAlternoV2.UsuarioModificacion = OportunidadDTO.Usuario;
                        beneficioAlternoV2.FechaCreacion = DateTime.Now;
                        beneficioAlternoV2.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Insert(beneficioAlternoV2);
                    }
                }

                ProgramaGeneralMotivacionRespuestaRepositorio _repMotiviacionRespuesta = new ProgramaGeneralMotivacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionRespuestaBO motivacionRespuesta = new ProgramaGeneralMotivacionRespuestaBO();
                var listaMotivacionRespuestaAgrupado = OportunidadDTO.DatosCompuesto.ListaMotivacion.GroupBy(x => x.IdMotivacion).Select(x => x.First()).ToList();
                foreach (var item in listaMotivacionRespuestaAgrupado)
                {
                    motivacionRespuesta = _repMotiviacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralMotivacion == item.IdMotivacion);

                    if (motivacionRespuesta != null)
                    {
                        motivacionRespuesta.Respuesta = item.Respuesta;
                        motivacionRespuesta.UsuarioModificacion = OportunidadDTO.Usuario;
                        motivacionRespuesta.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Update(motivacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralMotivacionRespuestaBO motivacionRespuestaAlterno = new ProgramaGeneralMotivacionRespuestaBO();
                        motivacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        motivacionRespuestaAlterno.IdProgramaGeneralMotivacion = item.IdMotivacion;
                        motivacionRespuestaAlterno.Respuesta = item.Respuesta;
                        motivacionRespuestaAlterno.Estado = true;
                        motivacionRespuestaAlterno.UsuarioCreacion = OportunidadDTO.Usuario;
                        motivacionRespuestaAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        motivacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        motivacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Insert(motivacionRespuestaAlterno);
                    }
                }

                PublicoObjetivoRespuestaRepositorio _repPublicoObjetivoRespuesta = new PublicoObjetivoRespuestaRepositorio(_integraDBContext);
                PublicoObjetivoRespuestaBO publicoObjetivoRespuesta = new PublicoObjetivoRespuestaBO();
                var listaPublicoObjetivoRespuestaAgrupado = OportunidadDTO.DatosCompuesto.ListaPublicoObjetivo.GroupBy(x => x.IdPublicoObjetivo).Select(x => x.First()).ToList();
                foreach (var item in listaPublicoObjetivoRespuestaAgrupado)
                {
                    publicoObjetivoRespuesta = _repPublicoObjetivoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdDocumentoSeccionPw == item.IdPublicoObjetivo);

                    if (publicoObjetivoRespuesta != null)
                    {
                        publicoObjetivoRespuesta.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuesta.UsuarioModificacion = OportunidadDTO.Usuario;
                        publicoObjetivoRespuesta.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Update(publicoObjetivoRespuesta);
                    }
                    else
                    {
                        PublicoObjetivoRespuestaBO publicoObjetivoRespuestaAlterno = new PublicoObjetivoRespuestaBO();
                        publicoObjetivoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        publicoObjetivoRespuestaAlterno.IdDocumentoSeccionPw = item.IdPublicoObjetivo;
                        publicoObjetivoRespuestaAlterno.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuestaAlterno.Estado = true;
                        publicoObjetivoRespuestaAlterno.UsuarioCreacion = OportunidadDTO.Usuario;
                        publicoObjetivoRespuestaAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        publicoObjetivoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        publicoObjetivoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Insert(publicoObjetivoRespuestaAlterno);
                    }
                }

                ProgramaGeneralCertificacionRespuestaRepositorio _repCertificacionRespuesta = new ProgramaGeneralCertificacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionRespuestaBO certificacionRespuesta = new ProgramaGeneralCertificacionRespuestaBO();
                var listaCertificacionRespuestaAgrupado = OportunidadDTO.DatosCompuesto.ListaCertificacion.GroupBy(x => x.IdCertificacion).Select(x => x.First()).ToList();
                foreach (var item in listaCertificacionRespuestaAgrupado)
                {
                    certificacionRespuesta = _repCertificacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralCertificacion == item.IdCertificacion);

                    if (certificacionRespuesta != null)
                    {
                        certificacionRespuesta.Respuesta = item.Respuesta;
                        certificacionRespuesta.UsuarioModificacion = OportunidadDTO.Usuario;
                        certificacionRespuesta.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Update(certificacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralCertificacionRespuestaBO certificacionRespuestaAlterno = new ProgramaGeneralCertificacionRespuestaBO();
                        certificacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        certificacionRespuestaAlterno.IdProgramaGeneralCertificacion = item.IdCertificacion;
                        certificacionRespuestaAlterno.Respuesta = item.Respuesta;
                        certificacionRespuestaAlterno.Estado = true;
                        certificacionRespuestaAlterno.UsuarioCreacion = OportunidadDTO.Usuario;
                        certificacionRespuestaAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        certificacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        certificacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Insert(certificacionRespuestaAlterno);
                    }
                }

                ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio _repProblemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                var listaProblemaRespuestaAgrupado = OportunidadDTO.DatosCompuesto.ListaSolucionesAlterno.GroupBy(x => x.IdProblema).Select(x => x.First()).ToList();
                foreach (var item in listaProblemaRespuestaAgrupado)
                {
                    problemaRespuesta = _repProblemaRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralProblemaDetalleSolucion == item.IdProblema);

                    if (problemaRespuesta != null)
                    {
                        problemaRespuesta.EsSeleccionado = item.Seleccionado;
                        problemaRespuesta.EsSolucionado = item.Solucionado;
                        problemaRespuesta.UsuarioModificacion = OportunidadDTO.Usuario;
                        problemaRespuesta.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Update(problemaRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuestaAlterno = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                        problemaRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        problemaRespuestaAlterno.IdProgramaGeneralProblemaDetalleSolucion = item.IdProblema;
                        problemaRespuestaAlterno.EsSeleccionado = item.Seleccionado;
                        problemaRespuestaAlterno.EsSolucionado = item.Solucionado;
                        problemaRespuestaAlterno.Estado = true;
                        problemaRespuestaAlterno.UsuarioCreacion = OportunidadDTO.Usuario;
                        problemaRespuestaAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        problemaRespuestaAlterno.FechaCreacion = DateTime.Now;
                        problemaRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Insert(problemaRespuestaAlterno);
                    }
                }

                ProgramaGeneralPrerequisitoRespuestaRepositorio _repPrerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaBO();
                var listaPrerequisitoRespuestaAgrupado = OportunidadDTO.DatosCompuesto.ListaPrerequisitoGeneralAlterno.GroupBy(x => x.IdProgramaGeneralPrerequisito).Select(x => x.First()).ToList();
                foreach (var item in listaPrerequisitoRespuestaAgrupado)
                {
                    prerequisitoRespuesta = _repPrerequisitoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralPrerequisito);

                    if (prerequisitoRespuesta != null)
                    {
                        prerequisitoRespuesta.Respuesta = item.Respuesta;
                        prerequisitoRespuesta.UsuarioModificacion = OportunidadDTO.Usuario;
                        prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Update(prerequisitoRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuestaAlterno = new ProgramaGeneralPrerequisitoRespuestaBO();
                        prerequisitoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        prerequisitoRespuestaAlterno.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito;
                        prerequisitoRespuestaAlterno.Respuesta = item.Respuesta;
                        prerequisitoRespuestaAlterno.Estado = true;
                        prerequisitoRespuestaAlterno.UsuarioCreacion = OportunidadDTO.Usuario;
                        prerequisitoRespuestaAlterno.UsuarioModificacion = OportunidadDTO.Usuario;
                        prerequisitoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        prerequisitoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Insert(prerequisitoRespuestaAlterno);
                    }
                }

                //======================================
                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaCompetidor)
                {
                    DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                    ListaCompetidor.IdOportunidadCompetidor = 0;
                    ListaCompetidor.IdCompetidor = item;
                    ListaCompetidor.Estado = true;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaCompetidor.Estado = true;

                    if (!ListaCompetidor.HasErrors)
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    else
                        return BadRequest(ListaCompetidor.GetErrors(null));
                }
                /*
                Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                foreach (var item in OportunidadDTO.DatosCompuesto.ListaSoluciones)
                {
                    SolucionClienteByActividadBO ListaSoluciones = new SolucionClienteByActividadBO();
                    ListaSoluciones.IdOportunidad = item.IdOportunidad;
                    ListaSoluciones.IdActividadDetalle = item.IdActividadDetalle;
                    ListaSoluciones.IdCausa = item.IdCausa;
                    ListaSoluciones.IdPersonal = item.IdPersonal;
                    ListaSoluciones.Solucionado = item.Solucionado;
                    ListaSoluciones.IdProblemaCliente = item.IdProblemaCliente;
                    ListaSoluciones.OtroProblema = item.OtroProblema;
                    ListaSoluciones.FechaCreacion = DateTime.Now;
                    ListaSoluciones.FechaModificacion = DateTime.Now;
                    ListaSoluciones.UsuarioCreacion = OportunidadDTO.Usuario;
                    ListaSoluciones.UsuarioModificacion = OportunidadDTO.Usuario;
                    ListaSoluciones.Estado = true;

                    if (!ListaSoluciones.HasErrors)
                        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                    else
                        return BadRequest(ListaSoluciones.GetErrors(null));
                }*/

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividadAlterno(false, OportunidadDTO.datosOportunidad, OportunidadDTO.ActividadAntigua.IdOcurrenciaActividad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }
                            _repOportunidad.Update(Oportunidad);


                            OportunidadNueva.IdFaseOportunidad = faseNueva;
                            OportunidadNueva.IdPersonalAsignado = OportunidadDTO.datosOportunidad.IdPersonalAsignado;
                            OportunidadNueva.IdTipoDato = OportunidadDTO.datosOportunidad.IdTipoDato;
                            OportunidadNueva.IdOrigen = OportunidadDTO.datosOportunidad.IdOrigen;
                            OportunidadNueva.IdAlumno = OportunidadDTO.datosOportunidad.IdAlumno;
                            if (OportunidadDTO.datosOportunidad.UltimaFechaProgramada != null)
                            {
                                OportunidadNueva.UltimaFechaProgramada = DateTime.Parse(OportunidadDTO.datosOportunidad.UltimaFechaProgramada);

                            }
                            OportunidadNueva.UltimoComentario = OportunidadDTO.datosOportunidad.UltimoComentario;
                            OportunidadNueva.IdTipoInteraccion = OportunidadDTO.datosOportunidad.IdTipoInteraccion.Value;
                            OportunidadNueva.IdSubCategoriaDato = OportunidadDTO.datosOportunidad.IdSubCategoriaDato;
                            OportunidadNueva.IdCentroCosto = OportunidadDTO.datosOportunidad.IdCentroCosto;
                            OportunidadNueva.FechaRegistroCampania = Oportunidad.FechaRegistroCampania;
                            OportunidadNueva.Estado = true;
                            OportunidadNueva.FechaCreacion = DateTime.Now;
                            OportunidadNueva.FechaModificacion = DateTime.Now;
                            OportunidadNueva.UsuarioCreacion = OportunidadDTO.Usuario;
                            OportunidadNueva.UsuarioModificacion = OportunidadDTO.Usuario;
                            OportunidadNueva.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
                            OportunidadNueva.IdPersonalAreaTrabajo = Oportunidad.IdPersonalAreaTrabajo;

                            this.CrearOportunidad(ref OportunidadNueva, false, TipoPersona.Alumno);

                            _repProcedenciaVentaCruzada.InsertarProcedenciaVentaCruzada(Oportunidad.Id, OportunidadNueva.Id);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error VentaCruzada Transaction";
                            mailData.Message = "IdOportunidad: " + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + OportunidadDTO.Usuario == null ? "" : OportunidadDTO.Usuario + "<br/>" + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ///01/02/2021
                    ///Calculo nuevo modelo predictivo
                    ///Carlos Crispin Riquelme
                    try
                    {
                        var nuevaProbabilidad = OportunidadNueva.ObtenerProbabilidadModeloPredictivo(OportunidadNueva.Id);
                    }
                    catch (Exception e)
                    {

                    }

                    /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                    /////////////DESCOMENTAR PARA PRODUCCION
                    //////////////////Replicar oportunidad-ALUMNO a integra v3
                    ////////try
                    ////////{
                    ////////    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + OportunidadNueva.Id;
                    ////////    using (WebClient wc = new WebClient())
                    ////////    {
                    ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    ////////        wc.DownloadString(URI);
                    ////////    }
                    ////////}
                    ////////catch (Exception ex)
                    ////////{
                    ////////    List<string> correos = new List<string>();
                    ////////    correos.Add("sistemas@bsginstitute.com");

                    ////////    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    ////////    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    ////////    mailData.Sender = "jcayo@bsginstitute.com";
                    ////////    mailData.Recipient = string.Join(",", correos);
                    ////////    mailData.Subject = "Error VentaCruzada Sincronizacion";
                    ////////    mailData.Message = "IdOportunidad: " + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + dto.Usuario == null ? "" : dto.Usuario + "<br/>" + dto.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    ////////    mailData.Cc = "";
                    ////////    mailData.Bcc = "";
                    ////////    mailData.AttachedFiles = null;

                    ////////    Mailservice.SetData(mailData);
                    ////////    Mailservice.SendMessageTask();
                    ////////}
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(OportunidadDTO.ActividadAntigua.Id);
                return Ok(new { ActividadEjecutada = realizadas, IdOportunidad = OportunidadDTO.ActividadAntigua.IdOportunidad.Value });
            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error VentaCruzada General";
                mailData.Message = "IdOportunidad: " + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + OportunidadDTO.Usuario == null ? "" : OportunidadDTO.Usuario + "<br/>" + OportunidadDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inserta un alumno y crea una oportunidad
        /// </summary>
        /// <param name="oportunidad"></param>
        /// <param name="alumno"></param>
        /// <param name="flagVentaCruzada"></param>
        private void CrearOportunidadCrearPersona(ref OportunidadBO oportunidad, bool flagVentaCruzada, Enums.TipoPersona tipoPersona)
        {
            var _repAlumno = new AlumnoRepositorio(_integraDBContext);
            var _repExpositor = new ExpositorRepositorio(_integraDBContext);
            var persona = new PersonaBO(_integraDBContext);

            if (tipoPersona == TipoPersona.Alumno)
            {
                if (!_repAlumno.ExisteContacto(oportunidad.Alumno.Email1, oportunidad.Alumno.Email2))
                {
                    oportunidad.Alumno.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);
                    _repAlumno.Insert(oportunidad.Alumno);

                    int? creacionCorrecta = persona.InsertarPersona(oportunidad.Alumno.Id, TipoPersona.Alumno, oportunidad.UsuarioCreacion);//funcion("Alumno",alumnoNuevo.Id);
                                                                                                                                            //Si boto error en al funcion 
                    if (creacionCorrecta == null)
                    {
                        var nombreTablaV3 = "talumnos";
                        var nombreTablaV4 = "mkt.T_Alumno";
                        var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, oportunidad.Alumno.Id, null, 0);
                        if (resultado == true)
                        {
                            throw new Exception("Se elimino el alumno");
                        }
                        else
                        {
                            throw new Exception("No se elimino alumno");
                        }
                    }
                    oportunidad.IdAlumno = oportunidad.Alumno.Id;
                    oportunidad.IdClasificacionPersona = creacionCorrecta;
                    oportunidad.IdPersonalAreaTrabajo = 8;
                }
                else
                {
                    throw new Exception("Alumno ya Existe!");
                }
            }
            else if (tipoPersona == TipoPersona.Docente)
            {
                _repExpositor.Insert(oportunidad.Expositor);
                int? idClasificacionPersona = persona.InsertarPersona(oportunidad.Expositor.Id, TipoPersona.Docente, oportunidad.UsuarioCreacion);
                if (idClasificacionPersona == null)
                {
                    var nombreTablaV3 = "texpositor";
                    var nombreTablaV4 = "pla.T_Expositor";
                    var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, oportunidad.Expositor.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("Se elimino el expositor");
                    }
                    else
                    {
                        throw new Exception("No se elimino expositor");
                    }
                }

                oportunidad.IdClasificacionPersona = idClasificacionPersona;
            }
            this.CrearOportunidad(ref oportunidad, flagVentaCruzada, tipoPersona);
        }

        /// <summary>
        /// Actualiza el alumno y crea una oportunidad
        /// </summary>
        /// <param name="Oportunidad">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="FlagVentaCruzada">Flag para determinar si se considerara como venta cruzada</param>
        /// <param name="TipoPersona">Objeto de clase enum TipoPersona</param>
        private void CrearOportunidadActualizarPersona(ref OportunidadBO Oportunidad, bool FlagVentaCruzada, TipoPersona TipoPersona)
        {
            PersonaBO _persona = new PersonaBO(_integraDBContext);
            if (TipoPersona == TipoPersona.Alumno)
            {
                if (_repAlumno.ExisteContacto(Oportunidad.Alumno.Email1, Oportunidad.Alumno.Email2))
                {
                    Oportunidad.Alumno.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);
                    _repAlumno.Update(Oportunidad.Alumno);
                    if (Oportunidad.IdClasificacionPersona == null)
                    {
                        var creacionCorrecta = _persona.InsertarPersona(Oportunidad.Alumno.Id, TipoPersona.Alumno, Oportunidad.UsuarioCreacion);//funcion("Alumno",alumnoNuevo.Id);
                                                                                                                                                //Si boto error en al funcion 
                        if (creacionCorrecta == null)
                        {
                            var nombreTablaV3 = "talumnos";
                            var nombreTablaV4 = "mkt.T_Alumno";
                            var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, Oportunidad.Alumno.Id, null, 0);
                            if (resultado == true)
                            {
                                throw new Exception("Se elimino el alumno");
                            }
                            else
                            {
                                throw new Exception("No se elimino alumno");
                            }
                        }
                        Oportunidad.IdClasificacionPersona = creacionCorrecta;
                    }
                    if (!Oportunidad.IdPersonalAreaTrabajo.HasValue)
                    {
                        Oportunidad.IdPersonalAreaTrabajo = 8;
                    }
                }
                else
                {
                    throw new Exception("Alumno no Existe!");
                }
            }
            else if (TipoPersona == TipoPersona.Docente)
            {
                if (_repExpositor.ExisteContacto(Oportunidad.Expositor.Email1))
                {
                    _repExpositor.Update(Oportunidad.Expositor);
                    if (Oportunidad.IdClasificacionPersona == null)
                    {
                        var idClasificacionPersona = _persona.InsertarPersona(Oportunidad.Expositor.Id, TipoPersona.Docente, Oportunidad.UsuarioCreacion);

                        if (idClasificacionPersona == null)
                        {
                            var nombreTablaV3 = "tPLA_Expositor";
                            var nombreTablaV4 = "pla.T_Expositor";
                            var resultado = _repAlumno.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, Oportunidad.Expositor.Id, null, 0);
                            if (resultado == true)
                            {
                                throw new Exception("Se elimino el expositor");
                            }
                            else
                            {
                                throw new Exception("No se elimino expositor");
                            }
                        }
                        Oportunidad.IdClasificacionPersona = idClasificacionPersona;
                    }
                    //personal area trabajo - operaciones
                    if (!Oportunidad.IdPersonalAreaTrabajo.HasValue)
                    {
                        Oportunidad.IdPersonalAreaTrabajo = 3;
                    }
                }
                else
                {
                    throw new Exception("Expositor no Existe!");
                }
            }
            this.CrearOportunidad(ref Oportunidad, FlagVentaCruzada, TipoPersona);

        }

        /// <summary>
        /// Valida todos los casos en los que una oportunidad puede pasar a OD o OM
        /// </summary>
        /// <param name="oportunidad"></param>
        /// <param name="objAsignacionAutomatica"></param>
        /// <param name="idTipoCategoriaOrigen"></param>

        private void RegistrarDatosEnvioCorreo(ref OportunidadBO oportunidad, int tipoAasignacion)
        {
            //Repositorios
            EnvioCorreoMasivoRepositorio _repEnvioCorreoMasivoRep = new EnvioCorreoMasivoRepositorio(_integraDBContext);
            EnvioCorreoMandrilRepositorio _repEnvioCorreoMandrilRep = new EnvioCorreoMandrilRepositorio(_integraDBContext);
            PersonalRepositorio _repPersonalRepositorio = new PersonalRepositorio(_integraDBContext);

            //BO
            EnvioCorreoMasivoBO Enviocorreomasivo = new EnvioCorreoMasivoBO();
            Enviocorreomasivo.Id = 0;
            Enviocorreomasivo.IdOportunidad = oportunidad.Id;
            Enviocorreomasivo.IdActividadDetalleInicial = oportunidad.IdActividadDetalleUltima;
            Enviocorreomasivo.IdPersonal = oportunidad.IdPersonalAsignado;
            Enviocorreomasivo.IdAlumno = oportunidad.IdAlumno;
            Enviocorreomasivo.IdCentroCostoOportunidad = oportunidad.IdCentroCosto;
            Enviocorreomasivo.IdTipoEnvioCorreo = tipoAasignacion;
            Enviocorreomasivo.IdMandrilTipoEnvio = 0;
            Enviocorreomasivo.Estado = true;
            Enviocorreomasivo.FechaCreacion = DateTime.Now;
            Enviocorreomasivo.FechaModificacion = DateTime.Now;
            Enviocorreomasivo.UsuarioCreacion = "SYSTEM";
            Enviocorreomasivo.UsuarioModificacion = "SYSTEM";
            _repEnvioCorreoMasivoRep.Insert(Enviocorreomasivo);

            EnvioCorreoMandrilBO Enviocorreomandril = new EnvioCorreoMandrilBO();
            Enviocorreomandril.Id = 0;
            Enviocorreomandril.IdOportunidad = oportunidad.Id;
            Enviocorreomandril.IdPersonal = oportunidad.IdPersonalAsignado;
            Enviocorreomandril.IdAlumno = oportunidad.IdAlumno;
            Enviocorreomandril.IdOportunidadCc = oportunidad.IdCentroCosto.Value;
            Enviocorreomandril.TipoAsignacion = tipoAasignacion;
            Enviocorreomandril.EstadoEnvio = false;
            Enviocorreomandril.FechaEnvio = null;
            Enviocorreomandril.Estado = true;
            Enviocorreomandril.FechaCreacion = DateTime.Now;
            Enviocorreomandril.FechaModificacion = DateTime.Now;
            Enviocorreomandril.UsuarioCreacion = "SYSTEM";
            Enviocorreomandril.UsuarioModificacion = "SYSTEM";
            _repEnvioCorreoMandrilRep.Insert(Enviocorreomandril);


            //////////////////////////Envio Correo en Tiempo Real///////////////
            TMK_MailServiceImpl ServicioMailMandril = new TMK_MailServiceImpl();
            string CuerpodelMensaje, AsuntoMensaje, EmailAlumno;
            int contador = 0;
            try
            {
                var valoresPlantilla = _repOportunidad.ObtenerDatosEnvioCorreoOportunidad(tipoAasignacion, oportunidad.Id);
                if (valoresPlantilla != null)
                {
                    try
                    {
                        EtiquetaParametroDTO parametros = new EtiquetaParametroDTO();
                        parametros.oportunidadId = valoresPlantilla.IdOportunidad;
                        parametros.centrocostoId = valoresPlantilla.CentrocostoId;
                        parametros.actividadDetalleId = valoresPlantilla.ActividadDetalleId;
                        parametros.programaGeneralId = valoresPlantilla.ProgramaGeneralId;
                        parametros.programaEspecificoId = valoresPlantilla.ProgramaEspecificoId;

                        if (valoresPlantilla.IdAsesorActual != valoresPlantilla.IdAsesorOriginal)
                        {
                            //////CuerpodelMensaje = oportunidad.ConvertirEtiquetas(null, valoresPlantilla.Plantilla, parametros, valoresPlantilla.IdAsesorActual, "");
                        }
                        else
                        {
                            //////CuerpodelMensaje = oportunidad.ConvertirEtiquetas(null, valoresPlantilla.Plantilla, parametros, 0, "");
                        }
                        //////AsuntoMensaje = oportunidad.ConvertirEtiquetas(null, valoresPlantilla.Asunto, parametros, 0, "");

                        //////if (AsuntoMensaje.Contains("tPEspecifico.nombre"))
                        //////{
                        //////    AsuntoMensaje = AsuntoMensaje.Replace("tPEspecifico.nombre", valoresPlantilla.NombreProgEspecifico);
                        //////}

                        if (string.IsNullOrEmpty(valoresPlantilla.Email1Contacto))
                        {
                            EmailAlumno = valoresPlantilla.Email2Contacto;
                        }
                        else
                            EmailAlumno = valoresPlantilla.Email1Contacto;

                        if (string.IsNullOrEmpty(EmailAlumno))
                        {
                            _repEnvioCorreoMasivoRep.Delete(valoresPlantilla.Id, "System");
                        }
                        else
                        {
                            TMK_MailServiceImpl servicioMail = new TMK_MailServiceImpl();
                            TMKMailDataDTO datos = new TMKMailDataDTO()
                            {
                                Sender = valoresPlantilla.EmailAsesor,
                                Recipient = EmailAlumno,
                                //////Subject = AsuntoMensaje,
                                //////Message = CuerpodelMensaje,
                                RemitenteC = valoresPlantilla.Remitente
                            };
                            servicioMail.SetData(datos);
                            var resultado = servicioMail.VerifyData();
                            var respuesta = servicioMail.SendMessageTask();


                            var envioCorreoMasivo = _repEnvioCorreoMasivoRep.FirstById(valoresPlantilla.Id);
                            //envioCorreoMasivo.EstadoEnvio = 1;
                            envioCorreoMasivo.FechaModificacion = DateTime.Now;
                            _repEnvioCorreoMasivoRep.Update(envioCorreoMasivo);
                            if (_repEnvioCorreoMasivoRep.Delete(valoresPlantilla.Id, "System"))
                            {
                                contador++;
                                var correoMandril = _repEnvioCorreoMandrilRep.GetBy(w => w.IdOportunidad == valoresPlantilla.IdOportunidad && w.TipoManualAutomatico == tipoAasignacion && w.IdMandrilTipoEnvio == 0).FirstOrDefault();
                                if (correoMandril != null)
                                {
                                    correoMandril.FechaEnvio = DateTime.Now;
                                    correoMandril.Asunto = valoresPlantilla.Asunto;
                                    correoMandril.IdMandril = respuesta[0].MensajeId;
                                    //correoMandril.EstadoEnvio = 1;
                                    _repEnvioCorreoMandrilRep.Update(correoMandril);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var envioCorreoMasivo = _repEnvioCorreoMasivoRep.FirstById(valoresPlantilla.Id);
                        //envioCorreoMasivo.EstadoEnvio = 3;
                        envioCorreoMasivo.FechaModificacion = DateTime.Now;
                        _repEnvioCorreoMasivoRep.Update(envioCorreoMasivo);

                        _repEnvioCorreoMasivoRep.Delete(valoresPlantilla.Id, "System");
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        /// Tipo Función: Interna
        /// Autor: Carlos Crispin R.
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor configurado para el centro de costo enviado
        /// </summary>
        /// <param name="IdCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="IdPais">Id del pais (PK de la tabla conf.T_Pais)</param>
        /// <param name="IdSubCategoriaDato">Id de la subcategoria dato(PK de la tabla mkt.T_SubCategoriaDato)</param>
        /// <param name="ProbabilidadActual">Probabilidad actual del dato</param>
        /// <returns>Entero con el asesor que corresponde</returns>
        private int ObtenerAsesorParaCentroCosto(int IdCentroCosto, int IdSubCategoriaDato, int IdPais, int ProbabilidadActual, int IdAgendaTab = 0)
        {
            int prioridad = 1;
            int idAsesor = 0;

            ProbabilidadActual = IdAgendaTab > 0 ? 4 /*4: Muy Alta*/: ProbabilidadActual;

            if (ProbabilidadActual < 4) // 4: Muy Alta
            {
                return 0;
            }

            while (prioridad < 4)
            {
                var posibilidades = _repOportunidad.ObtenerAsesorParaCentroCosto(IdCentroCosto, IdSubCategoriaDato, IdPais, ProbabilidadActual, prioridad).ToList();

                if (posibilidades.Count == 0)
                {
                    prioridad++;
                }
                else
                {
                    int minimocc = posibilidades.Min(s => s.AsignadosTotales);
                    var posibles = posibilidades.Where(s => s.AsignadosTotales == minimocc).ToList();
                    if (posibles.Count == 1)
                    {
                        idAsesor = posibles[0].IdAsesor;
                    }
                    else
                    {
                        minimocc = posibles.Min(s => s.AsignadosTotalesBNC);
                        posibles = posibles.Where(s => s.AsignadosTotalesBNC == minimocc).ToList();
                        idAsesor = posibles[0].IdAsesor;
                    }
                    prioridad = 4;
                }
            }

            return idAsesor;
        }

        private int ObtenerAsesorVentaCruzada(int idAlumno)
        {
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                AsignacionOportunidadRepositorio _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                var fechaActual = DateTime.Now;
                int _idPEspecifico;
                string _modalidad;
                int _idAsesor;
                int? idCiudad;
                int idPais;
                int idPEspecifico = 0;
                int asesorActual = 0;//obtiene el valor de asesor
                int cantidadOportunidades = 0;
                int maxOportunidades = 0;

                var alumnoCiudadPais = _repAlumno.ObtenerCiudadPais(idAlumno);
                idCiudad = alumnoCiudadPais.IdCiudad;
                idPais = alumnoCiudadPais.IdPais;
                var ventaCruzada = _repOportunidad.ObtenerCentroCostoProbable(idAlumno, fechaActual).OrderByDescending(x => x.Precio).ToList();
                foreach (var item in ventaCruzada)
                {
                    _idPEspecifico = item.IdPEspecifico;
                    _modalidad = item.Tipo;
                    _idAsesor = item.IdPersonal;
                    if (_modalidad == "Presencial")
                    {
                        if (idCiudad == _repPEspecifico.ObtenerCiudad(_idPEspecifico).IdCiudad)
                        {
                            asesorActual = _idAsesor;
                            cantidadOportunidades = _repAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Cantidad;
                            maxOportunidades = _repAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).AsignacionMax;
                            if (cantidadOportunidades <= maxOportunidades)
                            {
                                idPEspecifico = _idPEspecifico;
                                break;
                            }
                        }
                    }
                    else
                    {
                        asesorActual = _idAsesor;
                        cantidadOportunidades = _repAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Cantidad;
                        maxOportunidades = _repAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).AsignacionMax;
                        if (cantidadOportunidades <= maxOportunidades)
                        {
                            idPEspecifico = _idPEspecifico;
                            break;
                        }
                    }
                    if (idPEspecifico != 0)
                    {
                        break;
                    }
                }
                if (idPEspecifico != 0)
                {
                    return asesorActual;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Corrige dato erroneo que no se pudo procesar por validacion erronea
        /// </summary>
        /// <param name="obj">Objeto de clase AsignaionAutomaticaCompuestoDTO</param>
        /// <param name="Usuario">Usuario que realiza la correccion</param>
        /// <returns>Response 200 con objeto nulo</returns>
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult CorregirDatoErroneo([FromBody] AsignacionAutomaticaCompuestoDTO obj, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                AsignacionAutomaticaErrorRepositorio _repAsignacionAutomaticaError = new AsignacionAutomaticaErrorRepositorio(_integraDBContext);
                BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio(_integraDBContext);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                //AsignacionAutomaticaBO AsignacionAutomaticaNueva = new AsignacionAutomaticaBO();
                AsignacionAutomaticaBO AsignacionAutomaticaAntigua = new AsignacionAutomaticaBO();

                AsignacionAutomaticaAntigua = _repAsignacionAutomatica.FirstById(obj.Id);

                AsignacionAutomaticaAntigua.IdAlumno = obj.IdContacto;
                AsignacionAutomaticaAntigua.Nombre1 = obj.Nombre1;
                AsignacionAutomaticaAntigua.Nombre2 = obj.Nombre2;
                AsignacionAutomaticaAntigua.ApellidoPaterno = obj.ApellidoPaterno;
                AsignacionAutomaticaAntigua.ApellidoMaterno = obj.ApellidoMaterno;
                AsignacionAutomaticaAntigua.Telefono = obj.Telefono == "" ? "0" : obj.Telefono;
                AsignacionAutomaticaAntigua.Celular = obj.Celular;
                AsignacionAutomaticaAntigua.Email = obj.Email;
                AsignacionAutomaticaAntigua.NombrePrograma = obj.NombrePrograma;
                AsignacionAutomaticaAntigua.IdCentroCosto = obj.IdCentroCosto;
                AsignacionAutomaticaAntigua.IdTipoDato = obj.IdTipoDato;
                AsignacionAutomaticaAntigua.IdOrigen = obj.IdOrigen;
                AsignacionAutomaticaAntigua.IdCategoriaDato = obj.IdCategoriaDato;
                AsignacionAutomaticaAntigua.IdFaseOportunidad = obj.IdFaseOportunidad;
                AsignacionAutomaticaAntigua.IdAreaFormacion = obj.IdAreaFormacion;
                AsignacionAutomaticaAntigua.IdAreaTrabajo = obj.IdAreaTrabajo;
                AsignacionAutomaticaAntigua.IdIndustria = obj.IdIndustria;
                AsignacionAutomaticaAntigua.IdCargo = obj.IdCargo;
                AsignacionAutomaticaAntigua.IdPais = obj.IdPais;
                AsignacionAutomaticaAntigua.IdCiudad = obj.IdCiudad;
                AsignacionAutomaticaAntigua.Validado = obj.Validado;
                AsignacionAutomaticaAntigua.Corregido = obj.Corregido;
                AsignacionAutomaticaAntigua.OrigenCampania = obj.OrigenCampania;
                AsignacionAutomaticaAntigua.IdCategoriaOrigen = obj.IdCategoriaOrigen;
                AsignacionAutomaticaAntigua.IdAsignacionAutomaticaOrigen = obj.IdAsignacionAutomaticaOrigen;
                AsignacionAutomaticaAntigua.IdCampaniaScoring = obj.IdCampaniaScoring;
                AsignacionAutomaticaAntigua.FechaRegistroCampania = obj.FechaRegistroCampania;
                AsignacionAutomaticaAntigua.IdFaseOportunidadPortal = obj.IdFaseOportunidadPortal;
                AsignacionAutomaticaAntigua.IdOportunidad = obj.IdOportunidad;
                AsignacionAutomaticaAntigua.IdPersonal = obj.IdPersonal;
                AsignacionAutomaticaAntigua.IdTiempoCapacitacion = obj.IdTiempoCapacitacion;
                AsignacionAutomaticaAntigua.IdSubCategoriaDato = obj.IdSubCategoriaDato;
                AsignacionAutomaticaAntigua.IdInteraccionFormulario = obj.IdInteraccionFormulario;
                AsignacionAutomaticaAntigua.UrlOrigen = obj.UrlOrigen;
                AsignacionAutomaticaAntigua.ProbabilidadActual = obj.ProbabilidadActual;
                AsignacionAutomaticaAntigua.IdPagina = obj.IdPagina;
                AsignacionAutomaticaAntigua.IdPagina = obj.IdPagina;
                AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;

                AsignacionAutomaticaAntigua.CorregirErroneo(obj);

                var lista_errores = AsignacionAutomaticaAntigua.Validar(_integraDBContext);

                if (lista_errores.Count == 0)
                {
                    //_repAsignacionAutomatica.Update(AsignacionAutomaticaAntigua); 
                    try
                    {
                        OportunidadBO Oportunidad = new OportunidadBO(_integraDBContext);
                        using (TransactionScope scope = new TransactionScope())
                        {
                            var hoy = DateTime.Now;
                            var cadena = hoy.DayOfWeek;
                            DateTime.Now.ToString("hh:mm:ss");
                            Dictionary<string, string> Dias = new Dictionary<string, string>() {
                                    { "Monday","Lunes"},
                                    { "Tuesday","Martes"},
                                    { "Wednesday","Miercoles"},
                                    { "Thursday","Jueves"},
                                    { "Friday","Viernes"},
                                    { "Saturday","Sabado"},
                                    { "Sunday","Domingo"}
                                };
                            var Horacio = hoy.TimeOfDay;
                            var dia = Dias[cadena.ToString()];
                            var diaDto = _repBloqueHorarioProcesaOportunidad.ObtenerConfiguracion(dia);

                            int idTipoCategoriaOrigen = _repCategoriaOrigen.ObtneerTipoCategoriaOrigen(AsignacionAutomaticaAntigua.IdCategoriaOrigen == null ? 0 : AsignacionAutomaticaAntigua.IdCategoriaOrigen.Value);

                            _repAsignacionAutomaticaError.Delete((_repAsignacionAutomaticaError.GetBy(w => w.IdAsignacionAutomatica == AsignacionAutomaticaAntigua.Id, w => new { w.Id }).Select(x => x.Id)), Usuario);

                            if (AsignacionAutomaticaAntigua.IdAlumno == null)
                            {
                                Oportunidad.Alumno = new AlumnoBO();
                            }
                            else
                            {
                                Oportunidad.Alumno = _repAlumno.FirstById(AsignacionAutomaticaAntigua.IdAlumno == null ? 0 : AsignacionAutomaticaAntigua.IdAlumno.Value);
                            }

                            //se agrego el flag-venta-cruzada; 1:proceza; 0:No proceza
                            Oportunidad.GenerarOportunidad(AsignacionAutomaticaAntigua, false, "", idTipoCategoriaOrigen);
                            Oportunidad.Estado = true;
                            Oportunidad.FechaCreacion = DateTime.Now;
                            Oportunidad.FechaModificacion = DateTime.Now;
                            Oportunidad.UsuarioCreacion = Usuario;
                            Oportunidad.UsuarioModificacion = Usuario;
                            if (Oportunidad.Alumno.Id == 0)
                            {
                                CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }
                            else
                            {
                                CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno); //se agrego el flag-venta-cruzada
                            }


                            AsignacionAutomaticaAntigua.Validado = true;
                            AsignacionAutomaticaAntigua.Corregido = true;
                            AsignacionAutomaticaAntigua.IdOportunidad = Oportunidad.Id;
                            AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                            AsignacionAutomaticaAntigua.UsuarioModificacion = Usuario;

                            _repAsignacionAutomatica.Update(AsignacionAutomaticaAntigua);
                            scope.Complete();
                        }

                        ///15/03/2021
                        ///Calculo nuevo modelo predictivo
                        ///Carlos Crispin Riquelme
                        try
                        {
                            var nuevaProbabilidad = Oportunidad.ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);
                        }
                        catch (Exception e)
                        {
                            //throw;
                        }

                        //asignacion automatica
                        try
                        {
                            string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(URI);
                            }
                        }
                        catch (Exception)
                        {
                        }
                        this.MetodoODyOM(Oportunidad.Id);
                        return Ok(Oportunidad.Id);

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals("Se Actualizo contacto pero NO se creo la OPORTUNIDAD porque tiene Un BNC del tipo lanzamiento"))
                        {
                            AsignacionAutomaticaAntigua.Validado = true;
                            AsignacionAutomaticaAntigua.Corregido = false;
                            AsignacionAutomaticaAntigua.Estado = false;
                            AsignacionAutomaticaAntigua.FechaModificacion = DateTime.Now;
                            AsignacionAutomaticaAntigua.UsuarioModificacion = Usuario;
                            _repAsignacionAutomatica.Update(AsignacionAutomaticaAntigua);

                            try
                            {
                                string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    wc.DownloadString(URI);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        throw ex;

                    }
                }
                else
                {
                    //Elimianmos errores anteriores e insertamos nuevos ... ademas retornamos errores a la vista
                    using (TransactionScope scope = new TransactionScope())
                    {
                        _repAsignacionAutomatica.Update(AsignacionAutomaticaAntigua);

                        _repAsignacionAutomaticaError.Delete(_repAsignacionAutomaticaError.FirstBy(w => w.IdAsignacionAutomatica == AsignacionAutomaticaAntigua.Id, w => new { w.Id }).Id, Usuario);
                        foreach (var error in lista_errores)
                        {
                            _repAsignacionAutomaticaError.Insert(error);
                        }
                        scope.Complete();
                    }
                    try
                    {
                        string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomaticaAntigua.Id;
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(URI);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    var RegistroImportados = _repAsignacionAutomatica.ObtenerTodoRegistrosDuplicadosPorIdAsignacionAutomatica(AsignacionAutomaticaAntigua.Id);
                }

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("[action]/{IdAsesor}")]
        [HttpGet]
        public ActionResult ProcesarOportunidadesAsesoresInasistentes(int IdAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var usuario = "system";
                this.ProcesarOportunidadesAsesorInasistente(usuario, IdAsesor, false, true);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void ProcesarOportunidadesAsesorInasistente(string usuario, int idAsesor, bool enviarEmail, bool mantenerEstadoOportunidad)
        {
            var listOportunidadesAsesoresInasistentes = _repOportunidad.ObtenerOportunidadesAsesoresInasistentes(idAsesor);
            ReasignarAsesoresInasistentes(listOportunidadesAsesoresInasistentes, usuario, enviarEmail, mantenerEstadoOportunidad);
        }

        private void ReasignarAsesoresInasistentes(List<AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO> Oportunidades, string usuario, bool enviarEmail, bool mantenerEstadoOportunidad)
        {
            try
            {
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(_integraDBContext);

                foreach (var oportunidad in Oportunidades)
                {

                    using (TransactionScope scope = new TransactionScope())
                    {
                        //int asesorAnteriorId = _tcrm_OportunidadNewService.getAsesor(oportunidad);
                        //var oporTMP = _tcrm_OportunidadNewService.getOportunidad(new Guid(oportunidad));
                        int? asesorAnteriorId = oportunidad.IdPersonalAsignado;
                        int idAsesorAutomatica = 125;
                        var fecha = oportunidad.UltimaFechaProgramada;
                        //Actualizar Oportunidad con centro costo y/o asesor

                        if (_repOportunidad.Exist(oportunidad.IdOportunidad))
                        {
                            var oportunidadTemp = _repOportunidad.FirstBy(x => x.Id == oportunidad.IdOportunidad);
                            //TCRM_OportunidadNewDTO opo = new TCRM_OportunidadNewDTO();
                            //opo.Id = oportunidad.IdOportunidad;
                            oportunidadTemp.IdCentroCosto = oportunidad.IdCentroCosto;
                            //int idasesorseleccionado = _tcrmHojaOportunidadService.GetAsesorParaReasignacionAutomatica(oportunidad.IdOportunidadCC, oportunidad.idCategoriaOrigen, oportunidad.idcodigopais, oportunidad.probabilidadActualDesc, oportunidad.IdAsignadoA);
                            int idAsesorSeleccionado = 643;//TODO
                            oportunidadTemp.IdPersonalAsignado = idAsesorSeleccionado == 0 ? idAsesorAutomatica : idAsesorSeleccionado;

                            //oportunidadTemp = this.updateOportunidadAsesorCentroCosto(opo);
                            this.ActualizarOportunidadAsesorCentroCosto(ref oportunidadTemp);
                            //Registramos la asignacion con los nuevos datos
                            //_tcrm_asignacionOportunidadService.RegistrarAsignacion(oportunidad, asesor, centroCosto, 0, usuario);
                            //_tcrm_asignacionOportunidadService.RegistrarAsignacion(opo.Id, opo.IdAsignadoA, opo.IdOportunidadCC, 0, usuario);
                            this.RegistrarAsignacion(oportunidadTemp.Id, oportunidadTemp.IdPersonalAsignado, oportunidadTemp.IdCentroCosto.Value, oportunidad.IdAlumno, usuario);
                            ActividadDetalleBO actividadDetalleTemp = null;
                            //Finalizar Actividad
                            if (_repActividadDetalle.Exist(oportunidadTemp.IdActividadDetalleUltima))
                            {
                                actividadDetalleTemp = _repActividadDetalle.FirstBy(x => x.Id == oportunidadTemp.IdActividadDetalleUltima);
                                actividadDetalleTemp.Comentario = "Reasignacion Automatica";
                                actividadDetalleTemp.IdOcurrencia = 0;//TODO OCURRENCIA_ASIGNACION_MANUAL;
                                actividadDetalleTemp.IdOcurrenciaActividad = null;
                                actividadDetalleTemp.IdAlumno = oportunidadTemp.IdAlumno;
                                actividadDetalleTemp.IdOportunidad = oportunidadTemp.Id;
                                actividadDetalleTemp.IdEstadoActividadDetalle = 0; //FK_IDACTIVIDAD 
                                actividadDetalleTemp.IdActividadCabecera = oportunidadTemp.IdActividadCabeceraUltima;

                            }
                            //TCRM_ActividadesDetalleNewDTO det = new TCRM_ActividadesDetalleNewDTO();
                            oportunidadTemp.ActividadAntigua = actividadDetalleTemp;
                            OportunidadDTO oportunidadDTO = null;
                            oportunidadTemp.FinalizarActividad(mantenerEstadoOportunidad, oportunidadDTO);
                            //var registroOportunidad = finalizarActividad(det, opo, usuario, null, mantenerestadooportunidad);
                            // registrar los datos si se debe realizar envio de correo
                            if (oportunidadTemp != null && enviarEmail)
                            {
                                //TCRM_OportunidadNewDTO oportunidadGenerada = new TCRM_OportunidadNewDTO();
                                //oportunidadGenerada.Id = registroOportunidad.Id;
                                //oportunidadGenerada.UltimaActividadDetalle = registroOportunidad.UltimaActividadDetalle;
                                //oportunidadGenerada.IdAsignadoA = registroOportunidad.IdAsignadoA;
                                //oportunidadGenerada.IdContacto = registroOportunidad.IdContacto;
                                //oportunidadGenerada.IdOportunidadCC = registroOportunidad.IdOportunidadCC;

                                // TODO -> registrar_datos_envio_correo(oportunidadTemp, 2);
                            }

                            //Programar Actividad
                            //DateTime? fechaParam;
                            //if (fecha.Equals("00000000000000") || fecha.Equals("              "))
                            //    fechaParam = null;
                            //else
                            //    fechaParam = DateTime.ParseExact(fecha, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);

                            oportunidadTemp.ProgramaActividad();// programarActividad(det, fechaParam, opo.IdFaseOportunidad, opo.IdTipoDato, usuario, asesorAnteriorId, mantenerestadooportunidad);
                            /// guardamos las oportunidades que se reasignan
                        }
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un error!, se procesaron " + ex);
            }
        }

        private void RegistrarAsignacion(int idOportunidad, int idAsesor, int idCentroCosto, int idContacto, string usuario)
        {
            AsignacionOportunidadRepositorio _repAsignacionOportunidad = new AsignacionOportunidadRepositorio(_integraDBContext);
            //var asignacion = _tcrm_asignacionoportunidadRepository.GetAsignacionOportunidad(new Guid(oportunidad));
            var asignacion = _repAsignacionOportunidad.GetBy(x => x.IdOportunidad == idOportunidad).FirstOrDefault();
            if (asignacion == null)
            {
                //Traemos la oportunidad
                var oportunidad_entity = _repOportunidad.GetBy(x => x.Id == idOportunidad).FirstOrDefault();
                //string probabilidad = _tcrm_BloqueHorarioProcesaOportunidadRepository.ProbabilidadByOportunidad(new Guid(oportunidad)).FirstOrDefault();
                //Creamos un nuevo registro en asignacionOportunidad
                asignacion = new AsignacionOportunidadBO
                {
                    FechaAsignacion = DateTime.Now,
                    IdAlumno = oportunidad_entity.IdAlumno,
                    IdPersonal = oportunidad_entity.IdPersonalAsignado,
                    IdCentroCosto = oportunidad_entity.IdCentroCosto.Value,
                    IdOportunidad = idOportunidad,
                    IdTipoDato = oportunidad_entity.IdTipoDato,
                    IdFaseOportunidad = oportunidad_entity.IdFaseOportunidad,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "",
                    UsuarioModificacion = ""
                }; //TCRM_AsignacionOportunidadDTO();

                //asignacion = InsertConUsuario(asig, usuario);
                _repAsignacionOportunidad.Insert(asignacion);
                //Creamos el log
                //var _log = new TCRM_AsignacionOportunidadLogDTO();
                //_log.Id = Guid.Empty.ToString();
                //_log.FECHA_LOG = Fecha.getFechaActual();
                //_log.FK_AsesorAnt = asignacion.FK_Asesor;
                //_log.FK_AsignacionOportunidad = asignacion.Id.ToString();
                //_log.FK_CentroCostoAnt = asignacion.FK_CentroCosto;
                //_log.FK_Oportunidad = asignacion.FK_Oportunidad.ToString();
                //_log.FK_Alumno = asignacion.FK_Alumno;
                //_log.FK_Asesor = asignacion.FK_Asesor;
                //_log.FK_CentroCosto = asignacion.FK_CentroCosto;

                //_tcrm_asignacionoportunidadLogService.InsertConUsuario(_log, usuario);
            }

            //Creamos el log
            var log = new AsignacionOportunidadLogBO()
            {// TCRM_AsignacionOportunidadLogDTO();
                FechaLog = DateTime.Now,
                IdPersonalAnterior = asignacion.IdPersonal,
                IdAsignacionOportunidad = asignacion.Id,
                IdCentroCostoAnt = asignacion.IdCentroCosto,
                IdOportunidad = asignacion.IdOportunidad,
                IdTipoDato = asignacion.IdTipoDato,
                IdFaseOportunidad = asignacion.IdFaseOportunidad
            };

            ////Actualizamos con los nuevos datos si son diferentes de null
            //asignacion.FK_Asesor = idAsesor == 0 ? asignacion.FK_Asesor : idAsesor;
            //asignacion.FK_CentroCosto = idCentroCosto == 0 ? asignacion.FK_CentroCosto : idCentroCosto;
            //asignacion.FK_Alumno = idContacto == 0 ? asignacion.FK_Alumno : idContacto;
            //asignacion.FECHA_ASIGNACION = Fecha.getFechaActual();
            //asignacion.FECHA_MODIFICACION = Fecha.getFechaActual();
            //asignacion.USER_MODIFICACION = usuario;

            ////_tcrm_asignacionoportunidadRepository.Merge(asignacion, asignacion);
            //_tcrm_asignacionoportunidadRepository.Modify(asignacion);
            //_tcrm_asignacionoportunidadRepository.UnitOfWork.Commit();

            //log.FK_Alumno = asignacion.FK_Alumno;
            //log.FK_Asesor = asignacion.FK_Asesor;
            //log.FK_CentroCosto = asignacion.FK_CentroCosto;

            //_tcrm_asignacionoportunidadLogService.InsertConUsuario(log, usuario);

            //throw new Exception("Cacelado");
        }
        private void ActualizarOportunidadAsesorCentroCosto(ref OportunidadBO oportunidad)
        {
            if (oportunidad.IdPersonalAsignado == 0 && oportunidad.IdCentroCosto == 0)
            {
                throw new Exception("Se tiene que seleccionar o un centro de costo o un asesor");
            }
            if (_repOportunidad.Exist(oportunidad.Id))
            {
                //Guid id = Guid.Parse(oportunidad.Id);
                //var entity = _tcrm_oportunidadnewRepository.GetFiltered(s => s.Id == id).FirstOrDefault();
                //var entity = _tcrm_oportunidadnewRepository.Get(id);
                var entity = _repOportunidad.FirstById(oportunidad.Id);
                entity.IdPersonalAsignado = oportunidad.IdPersonalAsignado == 0 ? entity.IdPersonalAsignado : oportunidad.IdPersonalAsignado;
                entity.IdCentroCosto = oportunidad.IdCentroCosto == 0 ? entity.IdCentroCosto : oportunidad.IdCentroCosto;
                entity.FechaModificacion = DateTime.Now;
                entity.UsuarioModificacion = "";
                _repOportunidad.Update(entity);
                //_tcrm_oportunidadnewRepository.Modify2(entity);
                //_tcrm_oportunidadnewRepository.UnitOfWork.Commit();
                //var resp = BuilderTCRM_OportunidadNewDTO.builderEntityDTO(entity);
            }
        }

        /// Tipo Función: interna
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra las oportunidades enviadas a OD
        /// </summary>
        /// <param name="Oportunidades">Lista de enteros con las opoortunidades (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        /// <returns>Bool</returns>
        private bool CerrarOportunidadesOD(int[] Oportunidades, string Usuario)
        {
            OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            OportunidadCompetidorRepositorio _repOportunidadCompetidor = new OportunidadCompetidorRepositorio(_integraDBContext);
            OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

            foreach (int idOportunidad in Oportunidades)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                        if (oportunidadBD == null)
                            throw new Exception("No existe oportunidad!");

                        try
                        {
                            _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                        }
                        catch (Exception ex)
                        {
                        }

                        //Finalizar Actividad
                        ActividadDetalleBO actividadDetalle = new ActividadDetalleBO();
                        actividadDetalle.Id = oportunidadBD.IdActividadDetalleUltima;
                        actividadDetalle.Comentario = "Cerrado OD";
                        actividadDetalle.IdOcurrencia = 232; //Cerrado Fase OD
                        actividadDetalle.IdOcurrenciaActividad = null;
                        actividadDetalle.IdAlumno = oportunidadBD.IdAlumno;
                        actividadDetalle.IdOportunidad = oportunidadBD.Id;
                        actividadDetalle.IdCentralLlamada = 0;
                        actividadDetalle.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                        oportunidadBD.ActividadAntigua = actividadDetalle;
                        oportunidadBD.ActividadNueva = new ActividadDetalleBO();

                        OportunidadDTO oportunidad = new OportunidadDTO()
                        {
                            Id = oportunidadBD.Id,
                            IdCentroCosto = oportunidadBD.IdCentroCosto.Value,
                            IdPersonalAsignado = oportunidadBD.IdPersonalAsignado,
                            IdTipoDato = oportunidadBD.IdTipoDato,
                            IdFaseOportunidad = oportunidadBD.IdFaseOportunidad,
                            IdOrigen = oportunidadBD.IdOrigen,
                            IdAlumno = oportunidadBD.IdAlumno,
                            UltimoComentario = oportunidadBD.UltimoComentario,
                            IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima,
                            IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima,
                            IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado,
                            UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString(),
                            IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad,
                            IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo,
                            IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima,
                            IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial,
                            IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen,
                            IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio,
                            IdCampaniaScoring = oportunidadBD.IdCampaniaScoring,
                            IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp,
                            IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc,
                            FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf,
                            FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf,
                            FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc,
                            FechaRegistroCampania = oportunidadBD.FechaRegistroCampania,
                            IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal,
                            IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf,
                            CodigoPagoIc = oportunidadBD.CodigoPagoIc,
                            FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion,
                            IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion,
                            IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion,
                            IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato,
                            IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario,
                            UrlOrigen = oportunidadBD.UrlOrigen,
                            FechaPaso2 = oportunidadBD.FechaPaso2,
                            Paso2 = oportunidadBD.Paso2,
                            CodMailing = oportunidadBD.CodMailing,
                            IdPagina = oportunidadBD.IdPagina
                        };

                        oportunidadBD.FinalizarActividad(false, oportunidad);
                        _repOportunidad.Update(oportunidadBD);
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;
        }

        /// Tipo Función: interna
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra las oportunidades enviadas a OM
        /// </summary>
        /// <param name="Oportunidades">Lista de enteros con las opoortunidades (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        /// <returns>Bool</returns>
        private bool CerrarOportunidadesOM(int[] Oportunidades, string Usuario)
        {
            OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

            foreach (int idOportunidad in Oportunidades)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        OportunidadBO oportunidadBD = new OportunidadBO(idOportunidad, Usuario, _integraDBContext);
                        if (oportunidadBD == null)
                            throw new Exception("No existe oportunidad!");

                        try
                        {
                            _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                        }
                        catch (Exception ex)
                        {
                        }

                        //Finalizar Actividad
                        ActividadDetalleBO actividadDetalle = new ActividadDetalleBO();
                        actividadDetalle.Id = oportunidadBD.IdActividadDetalleUltima;
                        actividadDetalle.Comentario = "Cerrado Fase OM";
                        actividadDetalle.IdOcurrencia = 233; //Cerrado Fase OM
                        actividadDetalle.IdOcurrenciaActividad = null;
                        actividadDetalle.IdAlumno = oportunidadBD.IdAlumno;
                        actividadDetalle.IdOportunidad = oportunidadBD.Id;
                        actividadDetalle.IdCentralLlamada = 0;
                        actividadDetalle.IdActividadCabecera = oportunidadBD.IdActividadCabeceraUltima;
                        oportunidadBD.ActividadAntigua = actividadDetalle;
                        oportunidadBD.ActividadNueva = new ActividadDetalleBO();


                        OportunidadDTO oportunidad = new OportunidadDTO()
                        {
                            Id = oportunidadBD.Id,
                            IdCentroCosto = oportunidadBD.IdCentroCosto.Value,
                            IdPersonalAsignado = oportunidadBD.IdPersonalAsignado,
                            IdTipoDato = oportunidadBD.IdTipoDato,
                            IdFaseOportunidad = oportunidadBD.IdFaseOportunidad,
                            IdOrigen = oportunidadBD.IdOrigen,
                            IdAlumno = oportunidadBD.IdAlumno,
                            UltimoComentario = oportunidadBD.UltimoComentario,
                            IdActividadDetalleUltima = oportunidadBD.IdActividadDetalleUltima,
                            IdActividadCabeceraUltima = oportunidadBD.IdActividadCabeceraUltima,
                            IdEstadoActividadDetalleUltimoEstado = oportunidadBD.IdEstadoActividadDetalleUltimoEstado,
                            UltimaFechaProgramada = oportunidadBD.UltimaFechaProgramada.ToString(),
                            IdEstadoOportunidad = oportunidadBD.IdEstadoOportunidad,
                            IdEstadoOcurrenciaUltimo = oportunidadBD.IdEstadoOcurrenciaUltimo,
                            IdFaseOportunidadMaxima = oportunidadBD.IdFaseOportunidadMaxima,
                            IdFaseOportunidadInicial = oportunidadBD.IdFaseOportunidadInicial,
                            IdCategoriaOrigen = oportunidadBD.IdCategoriaOrigen,
                            IdConjuntoAnuncio = oportunidadBD.IdConjuntoAnuncio,
                            IdCampaniaScoring = oportunidadBD.IdCampaniaScoring,
                            IdFaseOportunidadIp = oportunidadBD.IdFaseOportunidadIp,
                            IdFaseOportunidadIc = oportunidadBD.IdFaseOportunidadIc,
                            FechaEnvioFaseOportunidadPf = oportunidadBD.FechaEnvioFaseOportunidadPf,
                            FechaPagoFaseOportunidadPf = oportunidadBD.FechaPagoFaseOportunidadPf,
                            FechaPagoFaseOportunidadIc = oportunidadBD.FechaPagoFaseOportunidadIc,
                            FechaRegistroCampania = oportunidadBD.FechaRegistroCampania,
                            IdFaseOportunidadPortal = oportunidadBD.IdFaseOportunidadPortal,
                            IdFaseOportunidadPf = oportunidadBD.IdFaseOportunidadPf,
                            CodigoPagoIc = oportunidadBD.CodigoPagoIc,
                            FlagVentaCruzada = oportunidadBD.IdTiempoCapacitacion,
                            IdTiempoCapacitacion = oportunidadBD.IdTiempoCapacitacion,
                            IdTiempoCapacitacionValidacion = oportunidadBD.IdTiempoCapacitacionValidacion,
                            IdSubCategoriaDato = oportunidadBD.IdSubCategoriaDato,
                            IdInteraccionFormulario = oportunidadBD.IdInteraccionFormulario,
                            UrlOrigen = oportunidadBD.UrlOrigen,
                            FechaPaso2 = oportunidadBD.FechaPaso2,
                            Paso2 = oportunidadBD.Paso2,
                            CodMailing = oportunidadBD.CodMailing,
                            IdPagina = oportunidadBD.IdPagina
                        };


                        oportunidadBD.FinalizarActividad(false, oportunidad);
                        _repOportunidad.Update(oportunidadBD);
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;
        }

        [Route("[action]/{IdAsignacionAutomatica}")]
        [HttpGet]
        public ActionResult CrearOportunidadWebHookFacebook(int IdAsignacionAutomatica)
        {
            try
            {
                integraDBContext _integraDBContext2 = new integraDBContext();
                AsignacionAutomaticaRepositorio asignacionAutomaticaRepositorio = new AsignacionAutomaticaRepositorio(_integraDBContext);

                _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                var rpta = CrearOportunidadPortalWeb(IdAsignacionAutomatica);
                var IdOportunidad = asignacionAutomaticaRepositorio.FirstById(IdAsignacionAutomatica).IdOportunidad;

                if (IdOportunidad != null)
                {
                    _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext2);

                    int nroIntentos = 0;
                    bool flagValidado = false;

                    while (!flagValidado && nroIntentos < 10)
                    {
                        try
                        {
                            var respuesta = ValidarCasosOportunidad(IdOportunidad ?? 0, IdAsignacionAutomatica, true);

                            flagValidado = true;
                        }
                        catch (Exception ex)
                        {
                            nroIntentos++;

                            Thread.Sleep(3000);
                        }
                    }

                    if (nroIntentos == 10)
                    {
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadWebHookFacebook", Parametros = $"IdAsignacionAutomatica={IdAsignacionAutomatica}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                    }

                    return Ok(IdOportunidad);
                }
                return Ok(IdOportunidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 01/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad segun el IdAsignacionAutomatica enviado
        /// </summary>
        /// <returns>Response 200</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult CrearOportunidadesPortalWeb()
        {
            try
            {
                integraDBContext _integraDBContext2 = new integraDBContext();
                AsignacionAutomaticaRepositorio _repAsignacionAutomatica = new AsignacionAutomaticaRepositorio(_integraDBContext);
                var asignacionAutomatica = _repAsignacionAutomatica.GetBy(x => x.Validado == false && x.Corregido == false && x.FechaCreacion > DateTime.Now.AddDays(-7)).OrderByDescending(x => x.FechaCreacion).ToList();
                asignacionAutomatica = asignacionAutomatica.Where(x => x.IdCategoriaOrigen != null && x.IdOrigen != null).ToList();
                foreach (var item in asignacionAutomatica)
                {
                    if (item.IdFaseOportunidadPortal != null)
                    {
                        if (_repAsignacionAutomatica.Exist(x => x.IdFaseOportunidadPortal == item.IdFaseOportunidadPortal && x.Validado == true)) continue;
                    }

                    _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                    var rpta = CrearOportunidadPortalWeb(item.Id);
                    var IdOportunidad = _repAsignacionAutomatica.FirstById(item.Id).IdOportunidad;

                    if (IdOportunidad != null)
                    {
                        _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext2);

                        int nroIntentos = 0;
                        bool flagValidado = false;

                        while (!flagValidado && nroIntentos < 10)
                        {
                            try
                            {
                                var respuesta = ValidarCasosOportunidad(IdOportunidad ?? 0, item.Id, true);

                                flagValidado = true;
                            }
                            catch (Exception ex)
                            {
                                nroIntentos++;

                                Thread.Sleep(3000);
                            }
                        }

                        if (nroIntentos == 10)
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadesPortalWeb", Parametros = $"IdOportunidad={IdOportunidad}&IdAsignacionAutomatica={item.Id}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
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

        [Route("[action]")]
        [HttpGet]
        public ActionResult CrearOportunidadesPortalWeb2()
        {
            try
            {
                integraDBContext _integraDBContext2 = new integraDBContext();
                AsignacionAutomaticaRepositorio asignacionAutomaticaRepositorio = new AsignacionAutomaticaRepositorio(_integraDBContext);
                var asignacionAutomatica = asignacionAutomaticaRepositorio.GetBy(x => x.Validado == false && x.Corregido == false).OrderByDescending(x => x.FechaCreacion).ToList();
                //var asignacionAutomatica = asignacionAutomaticaRepositorio.GetBy(x => x.Id == 520323).OrderByDescending(x => x.FechaCreacion).ToList();
                //asignacionAutomatica = asignacionAutomatica.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-28)).OrderByDescending(x => x.FechaCreacion).Take(15).ToList();
                //asignacionAutomatica = asignacionAutomatica.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-28)).OrderByDescending(x => x.FechaCreacion).ToList();
                asignacionAutomatica = asignacionAutomatica.Where(x => x.IdCategoriaOrigen != null && x.IdOrigen != null).ToList();
                foreach (var item in asignacionAutomatica)
                {
                    _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                    var rpta = CrearOportunidadPortalWeb(item.Id);
                    var IdOportunidad = asignacionAutomaticaRepositorio.FirstById(item.Id).IdOportunidad;

                    if (IdOportunidad != null)
                    {
                        _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext2);

                        int nroIntentos = 0;
                        bool flagValidado = false;

                        while (!flagValidado && nroIntentos < 10)
                        {
                            try
                            {
                                var respuesta = ValidarCasosOportunidad(IdOportunidad ?? 0, item.Id, true);

                                flagValidado = true;
                            }
                            catch (Exception ex)
                            {
                                nroIntentos++;

                                Thread.Sleep(3000);
                            }
                        }

                        if (nroIntentos == 10)
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadesPortalWeb2", Parametros = $"IdOportunidad={IdOportunidad}&IdAsignacionAutomatica={item.Id}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
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

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad de Leads de Facebook
        /// </summary>
        /// <returns>Response 200</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult CrearOportunidadesFacebookLeads()
        {
            try
            {
                integraDBContext _integraDBContext2 = new integraDBContext();
                AsignacionAutomaticaRepositorio asignacionAutomaticaRepositorio = new AsignacionAutomaticaRepositorio(_integraDBContext);
                var asignacionAutomatica = asignacionAutomaticaRepositorio.GetBy(x => x.Validado == false && x.Corregido == false &&
                (x.IdCategoriaOrigen == 329 || x.IdCategoriaOrigen == 352)).OrderByDescending(x => x.FechaCreacion).ToList();
                foreach (var item in asignacionAutomatica)
                {
                    _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                    var rpta = CrearOportunidadPortalWeb(item.Id);
                    var IdOportunidad = asignacionAutomaticaRepositorio.FirstById(item.Id).IdOportunidad;

                    if (IdOportunidad != null)
                    {
                        _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext2);

                        int nroIntentos = 0;
                        bool flagValidado = false;

                        while (!flagValidado && nroIntentos < 10)
                        {
                            try
                            {
                                var respuesta = ValidarCasosOportunidad(IdOportunidad ?? 0, item.Id, true);

                                flagValidado = true;
                            }
                            catch (Exception ex)
                            {
                                nroIntentos++;

                                Thread.Sleep(3000);
                            }
                        }

                        if (nroIntentos == 10)
                        {
                            _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadesFacebookLeads", Parametros = $"IdOportunidad={IdOportunidad}&IdAsignacionAutomatica={item.Id}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
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

        [Route("[action]/{IdWephone}")]
        [HttpGet]
        public ActionResult ObtenerGrabacionLlamada(string IdWephone)
        {
            try
            {
                //string PathLlamadas = "C:\\Users\\sistemas\\Desktop\\llamadas\\";
                //string PathLlamadas = "E:\\BackupLlamadas\\";
                string PathLlamadas = "F:\\BackupLlamadas\\";

                string anio = IdWephone.Substring(0, 4);
                string mes = IdWephone.Substring(4, 2);
                string dia = IdWephone.Substring(6, 2);
                string archivoSinFecha = IdWephone.Substring(16);
                string anexo = archivoSinFecha.Substring(0, archivoSinFecha.IndexOf("_"));

                string folder = anio + "\\" + mes + "\\" + dia + "\\" + anexo;
                //var bytes = System.IO.File.ReadAllBytes("C:\\Users\\sistemas\\Desktop\\llamadas\\record_" + IdWephone + ".wav");
                //var bytes = System.IO.File.ReadAllBytes("C:\\Users\\Administrador\\Desktop\\llamadas\\record_" + IdWephone + ".wav");
                var bytes = System.IO.File.ReadAllBytes(PathLlamadas + folder + "\\" + IdWephone + ".wav");
                return new FileContentResult(bytes, "audio/wav");
            }
            catch (Exception Ex)
            {
                return BadRequest("NO SE PUDO ENCONTRAR LA GRABACIÓN.");
            }
        }
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult CerrarBICManualmente(string IdOportunidad)
        {
            try
            {
                integraDBContext context = new integraDBContext(); ;
                int idOportunidad = Int32.Parse(IdOportunidad);
                OportunidadBO Oportunidad = new OportunidadBO(idOportunidad, "CerradoBIC", context);

                ConfiguracionBicRepositorio configuracionBicRepositorio = new ConfiguracionBicRepositorio(context);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(context);
                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(context);

                //var oportunidadBO = oportunidadRepositorio.FirstById(oportunidad.OporId);

                try
                {
                    _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                }
                catch (Exception ex)
                {
                }

                ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                actividadDetalleDTO.Comentario = "Cerrado Reporte BIC";
                actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                actividadDetalleDTO.IdOcurrenciaActividad = null;
                actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                actividadDetalleDTO.IdOportunidad = idOportunidad;
                actividadDetalleDTO.IdEstadoActividadDetalle = 0;//
                actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                Oportunidad.ActividadAntigua = actividadDetalleDTO;


                OportunidadDTO oportunidadDTO = new OportunidadDTO();
                oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                //oportunidadDTO.FasesActivas = Oportunidad.fasesac;
                oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                Oportunidad.FinalizarActividad(false, oportunidadDTO);

                using (TransactionScope scope = new TransactionScope())
                {
                    oportunidadRepositorio.Update(Oportunidad);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult CerrarNSManualmente(string IdOportunidad)
        {
            try
            {
                integraDBContext context = new integraDBContext(); ;
                int idOportunidad = Int32.Parse(IdOportunidad);
                OportunidadBO Oportunidad = new OportunidadBO(idOportunidad, "CerradoNS", context);

                ConfiguracionBicRepositorio configuracionBicRepositorio = new ConfiguracionBicRepositorio(context);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(context);
                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(context);

                //var oportunidadBO = oportunidadRepositorio.FirstById(oportunidad.OporId);
                try
                {
                    _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                }
                catch (Exception ex)
                {
                }

                ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                actividadDetalleDTO.Comentario = "Cerrado Reporte NS";
                actividadDetalleDTO.IdOcurrencia = 371;// 242. No solicitó información (NS)//ValorEstatico.IdOcurrenciaCerradoReporteBic;
                actividadDetalleDTO.IdOcurrenciaActividad = null;
                actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                actividadDetalleDTO.IdOportunidad = idOportunidad;
                actividadDetalleDTO.IdEstadoActividadDetalle = 0;//
                actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                Oportunidad.ActividadAntigua = actividadDetalleDTO;


                OportunidadDTO oportunidadDTO = new OportunidadDTO();
                oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                //oportunidadDTO.FasesActivas = Oportunidad.fasesac;
                oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                Oportunidad.FinalizarActividad(false, oportunidadDTO);

                using (TransactionScope scope = new TransactionScope())
                {
                    oportunidadRepositorio.Update(Oportunidad);
                    scope.Complete();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult EjecutarBIC()
        {
            try
            {
                integraDBContext context = new integraDBContext();
                integraDBContext context2 = new integraDBContext();
                ConfiguracionBicRepositorio _repConfiguracionBic = new ConfiguracionBicRepositorio(context);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(context);
                OportunidadRemarketingAgendaRepositorio _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);
                BloqueHorarioRepositorio _repBloqueHorario = new BloqueHorarioRepositorio(context);
                OcurrenciaRepositorio _repOcurrencia = new OcurrenciaRepositorio(context);
                ActividadDetalleRepositorio _repActividadDetalle = new ActividadDetalleRepositorio(context);

                List<OportunidadesNoEjecutadasDTO> listaOportunidades = _repOportunidad.ObtenerOportunidadesNoEjecutadas();
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => new { o.Id, o.IdCodigoPais })
                            .Select(grp => new { OporId = grp.Key.Id, ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList(), IdPais = grp.Key.IdCodigoPais })
                            .ToList();

                var listaConfiguracionBIC = _repConfiguracionBic.GetBy(x => x.Aplica == true).ToList();
                List<int> IdsOportunidadesACerrar = new List<int>();

                foreach (var configuracion in listaConfiguracionBIC)
                {
                    int dias = configuracion.Dias;
                    int diasMexico = 3;/*Contador Mexico*/

                    var bloques = _repBloqueHorario.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    foreach (var bloque in bloques)
                    {
                        bloque.Contador = 0;
                    }

                    foreach (var oportunidad in oportunidadesAgrupadas.ToList())
                    {
                        List<DateTime> fechas = oportunidad.ListaFechas;

                        /* Comentado lógica anterior de validación de la cantidad de llamadas no ejecutadas por AMBOS turno*/
                        //if (fechas.Count < dias * bloques.Count) continue;

                        /* Nueva lógica de validación de la cantidad de llamadas no ejecutadas por UN SOLO turno*/
                        if (fechas.Count < dias) continue;

                        var nombreTurnoUltimo = "";
                        DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                        foreach (var fecha in fechas)
                        {
                            TimeSpan horaFecha = fecha.TimeOfDay;

                            foreach (var bloque in bloques)
                            {
                                if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                {
                                    if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                                    else
                                    {
                                        nombreTurnoUltimo = bloque.Nombre;
                                        fechaUltima = fecha.Date;
                                        bloque.Contador++;
                                        break;
                                    }
                                }
                            }
                        }

                        /* Comentado lógica de validación de cantidad de no ejecutadas en turno mañana Y turno tarde */
                        //bool ConvertirABic = true;
                        //foreach (var bloque in bloques)
                        //{
                        //    if (bloque.Contador < dias) ConvertirABic = false;
                        //    bloque.Contador = 0;
                        //}

                        /* Inicio Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                        bool ConvertirABic = false;
                        foreach (var bloque in bloques)
                        {
                            if ((!oportunidad.IdPais.HasValue && bloque.Contador >= dias)
                                || (oportunidad.IdPais.HasValue && oportunidad.IdPais.Value == ValorEstatico.IdPaisMexico && bloque.Contador >= diasMexico)
                                || oportunidad.IdPais.HasValue && oportunidad.IdPais != ValorEstatico.IdPaisMexico && bloque.Contador >= dias)
                            {
                                ConvertirABic = true;
                            }
                            bloque.Contador = 0;
                        }
                        /* Fin Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */

                        if (ConvertirABic)
                        {
                            try
                            {
                                //context2 = new integraDBContext();
                                int idOportunidad = Int32.Parse(oportunidad.OporId);
                                OportunidadBO Oportunidad = new OportunidadBO(idOportunidad, "CerradoBIC", context);

                                //var oportunidadBO = oportunidadRepositorio.FirstById(oportunidad.OporId);

                                ActividadDetalleBO actividadDetalleDTO = _repActividadDetalle.FirstById(Oportunidad.IdActividadDetalleUltima);
                                actividadDetalleDTO.Comentario = "Cerrado Reporte BIC";
                                actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                                actividadDetalleDTO.IdOcurrenciaActividad = null;
                                actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                                actividadDetalleDTO.IdOportunidad = idOportunidad;
                                actividadDetalleDTO.IdEstadoActividadDetalle = 0;//
                                actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                                Oportunidad.ActividadAntigua = actividadDetalleDTO;


                                OportunidadDTO oportunidadDTO = new OportunidadDTO();
                                oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                                oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                                oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                                oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                                oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                                oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                                oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                                oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                                //oportunidadDTO.FasesActivas = Oportunidad.fasesac;
                                oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                                Oportunidad.FinalizarActividad(false, oportunidadDTO);

                                try
                                {
                                    _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                                }
                                catch (Exception ex)
                                {
                                }

                                using (TransactionScope scope = new TransactionScope())
                                {
                                    _repOportunidad.Update(Oportunidad);
                                    scope.Complete();
                                }
                                IdsOportunidadesACerrar.Add(idOportunidad);
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                    }
                }
                return Ok(IdsOportunidadesACerrar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularDiasParaBIC()
        {
            try
            {
                integraDBContext context = new integraDBContext();
                integraDBContext context2 = new integraDBContext();
                ConfiguracionBicRepositorio configuracionBicRepositorio = new ConfiguracionBicRepositorio(context);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(context);
                BloqueHorarioRepositorio bloqueHorarioRepositorio = new BloqueHorarioRepositorio(context);
                OcurrenciaRepositorio ocurrenciaRepositorio = new OcurrenciaRepositorio(context);
                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(context);
                ContadorBicRepositorio contadorBicRepositorio = new ContadorBicRepositorio(context);
                ContadorBicBO contadorBicBO;

                List<OportunidadesNoEjecutadasDTO> listaOportunidades = oportunidadRepositorio.ObtenerOportunidadesNoEjecutadas();
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => o.Id)
                            .Select(grp => new { OporId = grp.Key, ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList() })
                            .ToList();

                var listaConfiguracionBIC = configuracionBicRepositorio.GetBy(x => x.Aplica == true).ToList();
                List<int> IdsOportunidadesACerrar = new List<int>();

                foreach (var configuracion in listaConfiguracionBIC)
                {
                    int dias = configuracion.Dias;

                    var bloques = bloqueHorarioRepositorio.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    foreach (var bloque in bloques)
                    {
                        bloque.Contador = 0;
                    }

                    foreach (var oportunidad in oportunidadesAgrupadas.ToList())
                    {
                        List<DateTime> fechas = oportunidad.ListaFechas;

                        //if (fechas.Count < dias * bloques.Count) continue;

                        var nombreTurnoUltimo = "";
                        DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                        foreach (var fecha in fechas)
                        {
                            TimeSpan horaFecha = fecha.TimeOfDay;

                            foreach (var bloque in bloques)
                            {
                                if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                {
                                    if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                                    else
                                    {
                                        nombreTurnoUltimo = bloque.Nombre;
                                        fechaUltima = fecha.Date;
                                        bloque.Contador++;
                                        break;
                                    }
                                }
                            }
                        }

                        contadorBicBO = new ContadorBicBO();
                        foreach (var bloque in bloques)
                        {
                            if (bloque.Nombre == "Mañana") contadorBicBO.DiasSinContactoManhana = bloque.Contador ?? 0;
                            else contadorBicBO.DiasSinContactoTarde = bloque.Contador ?? 0;
                            bloque.Contador = 0;
                        }

                        try
                        {
                            int idOportunidad = Int32.Parse(oportunidad.OporId);
                            contadorBicBO.IdOportunidad = idOportunidad;

                            contadorBicBO.Estado = true;
                            contadorBicBO.FechaCreacion = DateTime.Now;
                            contadorBicBO.FechaModificacion = DateTime.Now;
                            contadorBicBO.UsuarioCreacion = "EjecutarBic";
                            contadorBicBO.UsuarioModificacion = "EjecutarBic";
                            contadorBicRepositorio.Insert(contadorBicBO);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                return Ok(IdsOportunidadesACerrar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult CerrarOportunidadesABic(int IdOportunidad)
        {
            try
            {
                integraDBContext context = new integraDBContext();
                ConfiguracionBicRepositorio configuracionBicRepositorio = new ConfiguracionBicRepositorio(context);
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio(context);
                OcurrenciaRepositorio ocurrenciaRepositorio = new OcurrenciaRepositorio(context);
                ActividadDetalleRepositorio actividadDetalleRepositorio = new ActividadDetalleRepositorio(context);


                OportunidadBO Oportunidad = new OportunidadBO(IdOportunidad, "CerradoBIC", context);

                ActividadDetalleBO actividadDetalleDTO = actividadDetalleRepositorio.FirstById(Oportunidad.IdActividadDetalleUltima);
                actividadDetalleDTO.Comentario = "Cerrado Reporte BIC";
                actividadDetalleDTO.IdOcurrencia = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                actividadDetalleDTO.IdOcurrenciaActividad = null;
                actividadDetalleDTO.IdAlumno = Oportunidad.IdAlumno;
                actividadDetalleDTO.IdOportunidad = IdOportunidad;
                actividadDetalleDTO.IdEstadoActividadDetalle = 0;//
                actividadDetalleDTO.IdActividadCabecera = Oportunidad.IdActividadCabeceraUltima;
                Oportunidad.ActividadAntigua = actividadDetalleDTO;


                OportunidadDTO oportunidadDTO = new OportunidadDTO();
                oportunidadDTO.IdEstadoOportunidad = Oportunidad.IdEstadoOportunidad;
                oportunidadDTO.IdFaseOportunidad = Oportunidad.IdFaseOportunidad;
                oportunidadDTO.IdFaseOportunidadIc = Oportunidad.IdFaseOportunidadIc;
                oportunidadDTO.IdFaseOportunidadIp = Oportunidad.IdFaseOportunidadIp;
                oportunidadDTO.IdFaseOportunidadPf = Oportunidad.IdFaseOportunidadPf;
                oportunidadDTO.FechaEnvioFaseOportunidadPf = Oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadDTO.FechaPagoFaseOportunidadIc = Oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadDTO.FechaPagoFaseOportunidadPf = Oportunidad.FechaPagoFaseOportunidadPf;
                //oportunidadDTO.FasesActivas = Oportunidad.fasesac;
                oportunidadDTO.CodigoPagoIc = Oportunidad.CodigoPagoIc;

                Oportunidad.FinalizarActividad(false, oportunidadDTO);

                using (TransactionScope scope = new TransactionScope())
                {
                    oportunidadRepositorio.Update(Oportunidad);
                    scope.Complete();
                }

                return Ok(IdOportunidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CrearOportunidadPorFiltroSegmento([FromBody] OportunidadFiltroSegmentoDTO OportunidadFiltroSegmento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var listadoErrores = new ListError();

                int cantidadOportunidadesCreadas = 0;

                foreach (var id in OportunidadFiltroSegmento.ListadoIdsAlumnos)
                {
                    if (_repAlumno.Exist(id))
                    {
                        try
                        {
                            var oportunidad = new OportunidadBO()
                            {
                                IdCentroCosto = OportunidadFiltroSegmento.IdCentroCosto,
                                IdTipoDato = OportunidadFiltroSegmento.IdTipoDato,
                                IdOrigen = OportunidadFiltroSegmento.IdOrigen,
                                IdFaseOportunidad = OportunidadFiltroSegmento.IdFaseOportunidad,
                                IdPersonalAsignado = ValorEstatico.IdPersonalAsesorAsignacionHistorico,
                                UltimoComentario = "Segmento Historico",
                                UltimaFechaProgramada = null,
                                IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                                FechaRegistroCampania = DateTime.Now,
                                IdAlumno = id,
                                Estado = true,
                                UsuarioCreacion = OportunidadFiltroSegmento.NombreUsuario,
                                UsuarioModificacion = OportunidadFiltroSegmento.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };
                            oportunidad.Alumno = _repAlumno.FirstById(id);
                            this.CrearOportunidadMarketing(ref oportunidad);

                            int nroIntentos = 0;
                            bool flagValidado = false;

                            while (!flagValidado && nroIntentos < 10)
                            {
                                try
                                {
                                    this.ValidarCasosOportunidad(oportunidad.Id, 0, false);

                                    flagValidado = true;
                                }
                                catch (Exception ex)
                                {
                                    nroIntentos++;

                                    Thread.Sleep(3000);
                                }
                            }

                            if (nroIntentos == 10)
                            {
                                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadPorFiltroSegmento", Parametros = $"IdOportunidad={oportunidad.Id}&IdAsignacionAutomatica=0", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                            }

                            if (oportunidad.Id != null && oportunidad.Id != 0)
                            {
                                cantidadOportunidadesCreadas++;
                            }
                        }
                        catch (Exception e)
                        {
                            listadoErrores.AgregarError(new Error(id, "ERROR", $"Ocurrio un error con el alumno con id: {id} - {e.Message}"));
                        }
                    }
                    else
                    {
                        listadoErrores.AgregarError(new Error(id, "ERROR", $"El alumno con id: {id} no existe"));
                    }
                }

                //INSERTAMOS EN LOG FILTRO SEGMENTO

                //var oportunidad con id erroneos

                var listadoErroresAlumno = listadoErrores.ObtenerErrores().DistinctBy(x => x.Id).Select(x => x.Id);

                var _repLogFiltroSegmentoEjecutado = new LogFiltroSegmentoEjecutadoRepositorio(_integraDBContext);
                var logFiltroSegmentoEjecutado = new LogFiltroSegmentoEjecutadoBO()
                {
                    IdCentroCosto = OportunidadFiltroSegmento.IdCentroCosto,
                    IdTipoDato = OportunidadFiltroSegmento.IdTipoDato,
                    IdOrigen = OportunidadFiltroSegmento.IdOrigen,
                    IdFaseOportunidad = OportunidadFiltroSegmento.IdFaseOportunidad,
                    IdFiltroSegmento = OportunidadFiltroSegmento.IdFiltroSegmento,
                    //TotalOportunidadesCreadas = OportunidadFiltroSegmento.ListadoIdsAlumnos.Count(),
                    TotalOportunidadesCreadas = OportunidadFiltroSegmento.ListadoIdsAlumnos.Where(w => !listadoErroresAlumno.Any(x => w == x)).Count(),
                    Estado = true,
                    UsuarioCreacion = OportunidadFiltroSegmento.NombreUsuario,
                    UsuarioModificacion = OportunidadFiltroSegmento.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                if (!logFiltroSegmentoEjecutado.HasErrors)
                {
                    _repLogFiltroSegmentoEjecutado.Insert(logFiltroSegmentoEjecutado);
                }
                else
                {
                    listadoErrores.AgregarError(new Error(200, "ERROR", $"Ocurrion un error con el log con id FiltroSegmento {OportunidadFiltroSegmento.IdFiltroSegmento } "));
                }

                if (listadoErrores.TieneErrores)
                {
                    return BadRequest(listadoErrores.ObtenerErrores());
                }
                return Ok(new { Message = "Cantidad real de oportunidades creadas ", Count = cantidadOportunidadesCreadas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularNumeroOportunidadesPorContacto(bool CalcularTodo, bool CalcularPorCentroCostoModificado, bool CalcularOportunidadesNuevasSinCalculo)
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                _repOportunidad.CalcularSolicitudes(CalcularTodo, CalcularPorCentroCostoModificado, CalcularOportunidadesNuevasSinCalculo);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularModelo()
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);


                var lista = _repOportunidad.ObtenerOportunidades();

                foreach (var item in lista)
                {
                    this.RegularizarModeloDataMining(item);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult RegularizarModeloDataMining(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!_repOportunidad.Exist(x => x.Id == IdOportunidad))
                {
                    return BadRequest("Oportunidad no existente!");
                }
                var oportunidad = _repOportunidad.FirstById(IdOportunidad);

                ModeloDataMiningBO modeloDataMining;
                if (!_repModeloDataMining.Exist(x => x.IdOportunidad == IdOportunidad))
                {
                    //creamos el modelo datamining
                    modeloDataMining = new ModeloDataMiningBO(_integraDBContext)
                    {
                        IdOportunidad = IdOportunidad
                    };
                    modeloDataMining.ObtenerProbabilidad(IdOportunidad);

                    if (modeloDataMining.ProbabilidadInicial == null)
                    {
                        new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + IdOportunidad);
                        modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                    }
                    modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                    modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                    modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                    modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                    modeloDataMining.UsuarioCreacion = oportunidad.UsuarioCreacion;
                    modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                    modeloDataMining.FechaCreacion = DateTime.Now;
                    modeloDataMining.FechaModificacion = DateTime.Now;
                    modeloDataMining.FechaCreacionContacto = DateTime.Now;
                    modeloDataMining.FechaCreacionOportunidad = oportunidad.FechaCreacion;
                    modeloDataMining.Estado = true;
                    _repModeloDataMining.Insert(modeloDataMining);
                }
                else
                {
                    //actualizamos la probabilidad
                    modeloDataMining = _repModeloDataMining.FirstBy(x => x.IdOportunidad == IdOportunidad);
                    modeloDataMining.IdOportunidad = oportunidad.Id;
                    modeloDataMining.ObtenerProbabilidad(oportunidad.Id);

                    if (modeloDataMining.ProbabilidadInicial == null)
                    {
                        new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + oportunidad.Id);
                        modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                    }
                    modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                    modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                    modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                    modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                    modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                    modeloDataMining.FechaModificacion = DateTime.Now;
                    modeloDataMining.Estado = true;
                    _repModeloDataMining.Update(modeloDataMining);
                }
                return Ok(modeloDataMining);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdTablarOriginal}/{IdTipoPersona}")]
        [HttpGet]
        public ActionResult RegularizarAlumno(int IdTablarOriginal, int IdTipoPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var persona = new PersonaBO(_integraDBContext);

                if (IdTipoPersona == 1)
                {
                    if (!_repAlumno.Exist(x => x.Id == IdTablarOriginal))
                    {
                        return BadRequest("Registro no existente!");
                    }

                    //var alumno = _repAlumno.FirstById(IdTablarOriginal);
                    var idClasificacionPersona = persona.InsertarPersona(IdTablarOriginal, TipoPersona.Alumno, "wchoque");

                    if (idClasificacionPersona is null)
                    {
                        return BadRequest("No se calculo persona");
                    }
                    //var oportunidades = _repOportunidad.GetBy(x => x.IdAlumno == IdTablarOriginal).ToList();

                    //var oportunidadesLog = _repOportunidadLog.GetBy(x => oportunidades.Any(y => y.Id ==  x.IdOportunidad)).ToList();
                    //var actividadesDetalle = _repActividadDetalle.GetBy(x => oportunidades.Any(y => y.Id == x.IdOportunidad)).ToList();
                    //var asignacionesOportunidad = _repAsignacionOportunidad.GetBy(x => oportunidades.Any(y => y.Id == x.IdOportunidad)).ToList();
                    //var asignacionesOportunidadLog = _repAsignacionOportunidadLog.GetBy(x => oportunidades.Any(y => y.Id == x.IdOportunidad)).ToList();

                    //foreach (var item in oportunidades)
                    //{
                    //    item.IdClasificacionPersona = idClasificacionPersona;
                    //    _repOportunidad.Update(item);
                    //}
                    //foreach (var item in oportunidadesLog)
                    //{
                    //    item.IdClasificacionPersona = idClasificacionPersona;
                    //    _repOportunidadLog.Update(item);
                    //}
                    //foreach (var item in actividadesDetalle)
                    //{
                    //    item.IdClasificacionPersona = idClasificacionPersona;
                    //    _repActividadDetalle.Update(item);
                    //}
                    //foreach (var item in asignacionesOportunidad)
                    //{
                    //    item.IdClasificacionPersona = idClasificacionPersona;
                    //    _repAsignacionOportunidad.Update(item);
                    //}
                    //foreach (var item in asignacionesOportunidadLog)
                    //{
                    //    item.IdClasificacionPersona = idClasificacionPersona;
                    //    _repAsignacionOportunidadLog.Update(item);
                    //}

                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Metodo que obtiene todas los correos marcados 
        /// </summary>
        /// <param name="NombreUsuario">Cadena con el nombre del usuario que ha mandado a crear las oportunidad</param>
        /// <returns>Response 200 con la cantidad de registros creados, caso contrario response 400 con el mensaje de error</returns>
        [Route("[action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult CrearOportunidadMailing(string NombreUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var _repCorreoGmail = new CorreoGmailRepositorio(_integraDBContext);
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);
                var _repPrioridadMailChimpLista = new PrioridadMailChimpListaRepositorio(_integraDBContext);
                var _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio(_integraDBContext);
                var _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
                var correoGmailCumplenCriterios = _repCorreoGmail.ObtenerCumplenCriterioAplicaCrearOportunidad();

                var listadoErrores = new ListError();

                int cantidadOportunidadesCreadas = 0;

                foreach (var correo in correoGmailCumplenCriterios)
                {
                    try
                    {
                        var IdPrioridadMailChimpListaCorreo = correo.IdPrioridadMailChimpListaCorreo;

                        var prioridadMailChimpListaCorreo = _repPrioridadMailChimpListaCorreo.FirstBy(x => x.Id == IdPrioridadMailChimpListaCorreo.Value);

                        if (prioridadMailChimpListaCorreo != null)
                        {
                            continue;// Sale porque el id de prioridad mailchimp lista no existe
                        }
                        
                        var prioridadMailChimpLista = _repPrioridadMailChimpLista.FirstBy(x => x.Id == prioridadMailChimpListaCorreo.IdPrioridadMailChimpLista);

                        CampaniaDetalleOportunidadDTO campaniaDetalle = null;

                        /*Obtencion de la campania procedente*/
                        if (prioridadMailChimpLista.IdCampaniaMailingDetalle != null)
                        {
                            var campaniaMailingDetalle = _repCampaniaMailingDetalle.FirstBy(x => x.Id == prioridadMailChimpLista.IdCampaniaMailingDetalle);

                            if (campaniaMailingDetalle != null)
                            {
                                campaniaDetalle = new CampaniaDetalleOportunidadDTO()
                                {
                                    IdCentroCosto = campaniaMailingDetalle.IdCentroCosto,
                                    IdPersonal = campaniaMailingDetalle.IdPersonal,
                                    CodMailing = campaniaMailingDetalle.CodMailing
                                };
                            }
                        }
                        else
                        {
                            var campaniaGeneralDetalle = _repCampaniaGeneralDetalle.FirstBy(x => x.Id == prioridadMailChimpLista.IdCampaniaGeneralDetalle);

                            if (campaniaGeneralDetalle != null)
                            {
                                campaniaDetalle = new CampaniaDetalleOportunidadDTO()
                                {
                                    IdCentroCosto = campaniaGeneralDetalle.IdCentroCosto,
                                    IdPersonal = campaniaGeneralDetalle.IdPersonal,
                                    CodMailing = campaniaGeneralDetalle.Nombre
                                };
                            }
                        }

                        if (campaniaDetalle == null)
                        {
                            continue;
                        }

                        if (_repAlumno.Exist(prioridadMailChimpListaCorreo.IdAlumno))
                        {
                            try
                            {
                                var oportunidad = new OportunidadBO()
                                {
                                    IdCentroCosto = campaniaDetalle.IdCentroCosto.Value,
                                    IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento,
                                    IdOrigen = ValorEstatico.IdOrigenCorreoElectronico,
                                    IdFaseOportunidad = ValorEstatico.IdFaseOportunidadBNC,
                                    IdPersonalAsignado = campaniaDetalle.IdPersonal,
                                    UltimoComentario = "Creado por mailing",
                                    UltimaFechaProgramada = null,
                                    IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                                    FechaRegistroCampania = DateTime.Now,
                                    IdAlumno = prioridadMailChimpListaCorreo.IdAlumno,
                                    Estado = true,
                                    UsuarioCreacion = NombreUsuario,
                                    UsuarioModificacion = NombreUsuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    CodMailing = campaniaDetalle.CodMailing
                                };
                                oportunidad.Alumno = _repAlumno.FirstById(prioridadMailChimpListaCorreo.IdAlumno);
                                this.CrearOportunidadMarketing(ref oportunidad);

                                int nroIntentos = 0;
                                bool flagValidado = false;

                                while (!flagValidado && nroIntentos < 10)
                                {
                                    try
                                    {
                                        this.ValidarCasosOportunidad(oportunidad.Id, 0, false);

                                        flagValidado = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        nroIntentos++;

                                        Thread.Sleep(3000);
                                    }
                                }

                                if (nroIntentos == 10)
                                {
                                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadMailing", Parametros = $"IdOportunidad={oportunidad.Id}&IdAsignacionAutomatica=0", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                }


                                if (oportunidad.Id != null && oportunidad.Id != 0)
                                {
                                    cantidadOportunidadesCreadas++;

                                    // Crear oportunidad
                                    correo.SeCreoOportunidad = true;
                                }
                            }
                            catch (Exception e)
                            {
                                listadoErrores.AgregarError(new Error(prioridadMailChimpListaCorreo.IdAlumno, "ERROR", $"Ocurrio un error con el alumno con id: {prioridadMailChimpListaCorreo.IdAlumno} - {e.Message}"));
                            }

                            _repCorreoGmail.Update(correo);
                        }
                        else
                        {
                            listadoErrores.AgregarError(new Error(prioridadMailChimpListaCorreo.IdAlumno, "ERROR", $"El alumno con id: {prioridadMailChimpListaCorreo.IdAlumno} no existe"));
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                //INSERTAMOS EN LOG FILTRO SEGMENTO

                //var oportunidad con id erroneos

                var listadoErroresAlumno = listadoErrores.ObtenerErrores().DistinctBy(x => x.Id).Select(x => x.Id);

                if (listadoErrores.TieneErrores)
                {
                    return BadRequest(listadoErrores.ObtenerErrores());
                }
                return Ok(new { Message = "Cantidad real de oportunidades creadas ", Count = cantidadOportunidadesCreadas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Metodo que obtiene todas los WhatsApp marcados 
        /// </summary>
        /// <param name="NombreUsuario">Cadena con el nombre del usuario que ha mandado a crear las oportunidad</param>
        /// <returns>Response 200 con la cantidad de registros creados, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult CrearOportunidadWhatsApp(string NombreUsuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var _repWhatsAppConfiguracionEnvioDetalleOportunidad = new WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio(_integraDBContext);
                var _repWhatsAppConfiguracionEnvioDetalle = new WhatsAppConfiguracionEnvioDetalleRepositorio(_integraDBContext);
                var _repWhatsAppMensajePublicidad = new WhatsAppMensajePublicidadRepositorio(_integraDBContext);
                var _repOrigen = new OrigenRepositorio(_integraDBContext);

                // Mantener por respaldo
                //var listaOportunidades = _repWhatsAppConfiguracionEnvioDetalleOportunidad.GetBy(x => x.IdOportunidad == null && x.UsuarioCreacion == NombreUsuario);
                var listaOportunidades = _repWhatsAppConfiguracionEnvioDetalleOportunidad.ObtenerDatosWhatsAppPais(NombreUsuario);

                // Valores por defecto
                var idTipoDato_Lanzamiento = 8;
                var idOrigen_WhatsAppChatBasesPropias = _repOrigen.FirstBy(x => x.IdMigracion == new Guid("F1F7B94A-B33E-C0EA-5A91-08D79858254F"));
                var idFaseOportunidad_Bnc = 2;

                var listadoErrores = new ListError();

                int cantidadOportunidadesCreadas = 0;

                foreach (var oportunidadPreparada in listaOportunidades)
                {
                    if (oportunidadPreparada.IdPersonal == ValorEstatico.IdPersonalAsignacionAutomatica)
                    {
                        int idPersonalTemp = this.ObtenerAsesorParaCentroCosto(oportunidadPreparada.IdCentroCosto, ValorEstatico.IdWhatsAppMultipleSubCategoriaDato, oportunidadPreparada.IdCodigoPais.GetValueOrDefault(), 4/*Muy alta*/);

                        oportunidadPreparada.IdPersonal = idPersonalTemp == 0 ? ValorEstatico.IdPersonalAsignacionAutomatica : oportunidadPreparada.IdPersonal;
                    }

                    var whatsAppConfiguracionEnvioDetalle = _repWhatsAppConfiguracionEnvioDetalle.FirstById(oportunidadPreparada.IdWhatsAppConfiguracionEnvioDetalle);

                    WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                    if (whatsAppConfiguracionEnvioDetalle.IdPrioridadMailChimpListaCorreo == null)
                        whatsAppMensajePublicidad = _repWhatsAppMensajePublicidad.FirstBy(x => x.IdConjuntoListaResultado == whatsAppConfiguracionEnvioDetalle.IdConjuntoListaResultado);
                    else
                        whatsAppMensajePublicidad = _repWhatsAppMensajePublicidad.FirstBy(x => x.IdPrioridadMailChimpListaCorreo == whatsAppConfiguracionEnvioDetalle.IdPrioridadMailChimpListaCorreo);

                    if (_repAlumno.Exist(whatsAppMensajePublicidad.IdAlumno))
                    {
                        try
                        {
                            var oportunidad = new OportunidadBO
                            {
                                IdCentroCosto = oportunidadPreparada.IdCentroCosto,
                                IdTipoDato = idTipoDato_Lanzamiento,
                                IdOrigen = idOrigen_WhatsAppChatBasesPropias.Id,
                                IdFaseOportunidad = idFaseOportunidad_Bnc,
                                IdPersonalAsignado = oportunidadPreparada.IdPersonal,
                                UltimoComentario = "Creado por WhatsApp",
                                UltimaFechaProgramada = null,
                                IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                                FechaRegistroCampania = DateTime.Now,
                                IdAlumno = whatsAppMensajePublicidad.IdAlumno,
                                Estado = true,
                                UsuarioCreacion = NombreUsuario,
                                UsuarioModificacion = NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Alumno = _repAlumno.FirstById(whatsAppMensajePublicidad.IdAlumno)
                            };
                            CrearOportunidadMarketing(ref oportunidad);

                            int nroIntentos = 0;
                            bool flagValidado = false;

                            while (!flagValidado && nroIntentos < 10)
                            {
                                try
                                {
                                    this.ValidarCasosOportunidad(oportunidad.Id, 0, false);

                                    flagValidado = true;
                                }
                                catch (Exception e)
                                {
                                    nroIntentos++;

                                    Thread.Sleep(3000);
                                }
                            }

                            if (nroIntentos == 10)
                            {
                                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadWhatsApp", Parametros = $"IdOportunidad={oportunidad.Id}&IdAsignacionAutomatica=0", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                            }

                            if (oportunidad.Id != 0)
                            {
                                cantidadOportunidadesCreadas++;

                                // Crear oportunidad
                                oportunidadPreparada.IdOportunidad = oportunidad.Id;
                                oportunidadPreparada.FechaModificacion = DateTime.Now;
                                oportunidadPreparada.UsuarioModificacion = NombreUsuario;
                            }
                        }
                        catch (Exception e)
                        {
                            listadoErrores.AgregarError(new Error(whatsAppMensajePublicidad.IdAlumno, "ERROR", $"Ocurrio un error con el alumno con id: {whatsAppMensajePublicidad.IdAlumno} - {e.Message}"));
                        }

                        _repWhatsAppConfiguracionEnvioDetalleOportunidad.Update(oportunidadPreparada);
                    }
                    else
                    {
                        listadoErrores.AgregarError(new Error(whatsAppMensajePublicidad.IdAlumno, "ERROR", $"El alumno con id: {whatsAppMensajePublicidad.IdAlumno} no existe"));
                    }
                }

                if (listadoErrores.TieneErrores)
                {
                    return BadRequest(listadoErrores.ObtenerErrores());
                }

                return Ok("Cantidad real de oportunidades creadas = " + cantidadOportunidadesCreadas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{NombreUsuario}/{IdOportunidad}/{UsuarioCoordinadorAcademico}/{Estados}/{IdActividadCabecera}/{IdAsesorAutomaticoOperaciones}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperacionesMasivo(string NombreUsuario, int? IdOportunidad, string UsuarioCoordinadorAcademico, string Estados, int IdActividadCabecera, int IdAsesorAutomaticoOperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCorrecto = new List<IdDTOV2>();
                var listaIncorrecto = new List<IdDTOV2>();
                var listaIdOportunidad = new List<IdDTOV2>();
                if (IdOportunidad != 0)
                {
                    //listaIdOportunidad = _repOportunidad.ObtenerOportunidadesOperacionesSinCrear(IdOportunidad.Value);
                }
                else
                {
                    listaIdOportunidad = _repOportunidad.ObtenerOportunidadesOperacionesSinCrear(0, UsuarioCoordinadorAcademico, Estados);
                }
                foreach (var item in listaIdOportunidad)
                {
                    try
                    {
                        this.GenerarOportunidadOperaciones(item.Id, NombreUsuario, item.IdCentroCosto, IdActividadCabecera, IdAsesorAutomaticoOperaciones, 0);
                        listaCorrecto.Add(item);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(item);
                    }
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera la oportunidad de operaciones basandoe en la oportundiades padre de ventas
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>

        [Route("[action]/{IdOportunidad}/{NombreUsuario}/{IdCentroCosto}/{IdActividadCabecera}/{IdAsesorAutomaticoOperaciones}/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperaciones(int IdOportunidad, string NombreUsuario, int IdCentroCosto, int IdActividadCabecera, int IdAsesorAutomaticoOperaciones, int IdMatriculaCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("IdOportunidad no existe");
                }
                var oportunidadVentas = _repOportunidad.FirstById(IdOportunidad);
                oportunidadVentas.GenerarOportunidadOperaciones(_integraDBContext);
                var idPadre = oportunidadVentas.Id;
                if (IdAsesorAutomaticoOperaciones == 0)
                {
                    if (!_repOportunidad.TienePersonalOperaciones(oportunidadVentas.Id))
                    {
                        return BadRequest("No se pudo calcular un personal operaciones");
                    }
                }

                var oportunidadOperaciones = oportunidadVentas;
                oportunidadOperaciones.Id = 0;
                if (IdAsesorAutomaticoOperaciones != 0)
                {
                    oportunidadOperaciones.IdPersonalAsignado = IdAsesorAutomaticoOperaciones;//_repOportunidad.ObtenerIdPersonalOperaciones(idPadre).Id;
                }
                else
                {
                    oportunidadOperaciones.IdPersonalAsignado = _repOportunidad.ObtenerIdPersonalOperaciones(idPadre).Id;
                }

                oportunidadOperaciones.IdPersonalAreaTrabajo = 3;
                oportunidadOperaciones.IdCentroCosto = IdCentroCosto;
                oportunidadOperaciones.UltimaFechaProgramada = null;
                oportunidadOperaciones.IdActividadCabeceraUltima = IdActividadCabecera;

                oportunidadOperaciones.FechaCreacion = DateTime.Now;
                oportunidadOperaciones.UsuarioCreacion = NombreUsuario;
                oportunidadOperaciones.FechaModificacion = DateTime.Now;
                oportunidadOperaciones.UsuarioModificacion = NombreUsuario;
                oportunidadOperaciones.IdMigracion = null;
                oportunidadOperaciones.RowVersion = null;
                oportunidadOperaciones.Estado = true;

                oportunidadOperaciones.IdPadre = idPadre;

                this.CrearOportunidadActualizarPersona(ref oportunidadOperaciones, false, TipoPersona.Alumno);

                ///03/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidadOperaciones.ObtenerProbabilidadModeloPredictivo(oportunidadOperaciones.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }

                if (IdMatriculaCabecera > 0)
                {
                    MoodleCronogramaEvaluacionBO moodleCronogramaEvaluacion = new MoodleCronogramaEvaluacionBO();
                    moodleCronogramaEvaluacion.ObtenerCronogramaAutoEvaluacionUltimaVersion(IdMatriculaCabecera);
                }

                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                //string URI = "http://localhost:4348//Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + oportunidadOperaciones.Id;
                //using (WebClient wc = new WebClient())
                //{
                //	wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                //	wc.DownloadString(URI);
                //}
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                return Ok(oportunidadOperaciones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{NombreUsuario}/{IdCentroCosto}/{IdPersonalAsignado}/{IdAlumno}/{IdActividadCabecera}")]
        [HttpGet]
        public ActionResult CrearOportunidadOperacionesOportunidadNueva(string NombreUsuario, int IdCentroCosto, int IdPersonalAsignado, int IdAlumno, int IdActividadCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var idTipoDato_referido = 6;
                var idOrigen_LANPERORG2 = 74;
                var idFaseOportunidad_M = 23;

                var oportunidad = new OportunidadBO()
                {
                    IdCentroCosto = IdCentroCosto,
                    IdTipoDato = idTipoDato_referido,
                    IdOrigen = idOrigen_LANPERORG2,
                    IdFaseOportunidad = idFaseOportunidad_M,
                    IdPersonalAsignado = IdPersonalAsignado,
                    IdActividadCabeceraUltima = IdActividadCabecera,
                    IdPersonalAreaTrabajo = ValorEstatico.IdPersonalAreaTrabajoOperaciones,
                    UltimoComentario = "creacion oportunidad operaciones",
                    UltimaFechaProgramada = null,
                    IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                    FechaRegistroCampania = DateTime.Now,
                    IdAlumno = IdAlumno,
                    Estado = true,
                    UsuarioCreacion = NombreUsuario,
                    UsuarioModificacion = NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    CodMailing = ""
                };
                oportunidad.Alumno = _repAlumno.FirstById(IdAlumno);
                this.CrearOportunidadMarketing(ref oportunidad);
                return Ok(oportunidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera la oportunidad de operaciones basandoe en la oportundiades padre de ventas con algunos parameteos ya definidos
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>
        [Route("[action]/{IdOportunidad}/{NombreUsuario}/{IdPersonal}/{IdFaseOportunidad}/{IdActividadCabecera}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperacionesConParametros(int IdOportunidad, string NombreUsuario, int IdPersonal, int IdFaseOportunidad, int IdActividadCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    return BadRequest("IdOportunidad no existe");
                }
                var oportunidadVentas = _repOportunidad.FirstById(IdOportunidad);
                oportunidadVentas.GenerarOportunidadOperaciones(_integraDBContext);
                var idPadre = oportunidadVentas.Id;

                //if (!_repOportunidad.TienePersonalOperaciones(oportunidadVentas.Id))
                //{
                //    return BadRequest("No se pudo calcular un personal operaciones");
                //}
                var oportunidadOperaciones = oportunidadVentas;
                oportunidadOperaciones.Id = 0;
                oportunidadOperaciones.IdPersonalAsignado = IdPersonal;//_repOportunidad.ObtenerIdPersonalOperaciones(idPadre).Id;
                oportunidadOperaciones.IdPersonalAreaTrabajo = 3;
                oportunidadOperaciones.IdFaseOportunidad = IdFaseOportunidad;
                oportunidadOperaciones.IdActividadCabeceraUltima = IdActividadCabecera;


                oportunidadOperaciones.FechaCreacion = DateTime.Now;
                oportunidadOperaciones.UsuarioCreacion = NombreUsuario;
                oportunidadOperaciones.FechaModificacion = DateTime.Now;
                oportunidadOperaciones.UsuarioModificacion = NombreUsuario;
                oportunidadOperaciones.IdMigracion = null;
                oportunidadOperaciones.RowVersion = null;
                oportunidadOperaciones.Estado = true;

                oportunidadOperaciones.IdPadre = idPadre;

                this.CrearOportunidadActualizarPersona(ref oportunidadOperaciones, false, TipoPersona.Alumno);

                ///03/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = oportunidadOperaciones.ObtenerProbabilidadModeloPredictivo(oportunidadOperaciones.Id);
                }
                catch (Exception e)
                {
                    //throw;
                }


                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                ////////string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + oportunidadOperaciones.Id;
                ////////using (WebClient wc = new WebClient())
                ////////{
                ////////    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////    wc.DownloadString(URI);
                ////////}
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                return Ok("ok");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{NombreUsuario}/{IdFaseOportunidad}/{UsuarioCoordinadorAcademico}/{Estados}/{IdActividadCabecera}/{IdAsesorAutomaticoOperaciones}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperacionesMasivo_logica2(string NombreUsuario, int IdFaseOportunidad, string UsuarioCoordinadorAcademico, string Estados, int IdActividadCabecera, int IdAsesorAutomaticoOperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCorrecto = new List<IdDTOV3>();
                var listaIncorrecto = new List<IdDTOV3>();
                var listaIdOportunidad = new List<IdDTOV3>();

                listaIdOportunidad = _repOportunidad.ObtenerOportunidadesOperacionesSinCrear_logica2(UsuarioCoordinadorAcademico, Estados);

                foreach (var item in listaIdOportunidad)
                {
                    try
                    {
                        int idPersonal;
                        if (IdAsesorAutomaticoOperaciones != 0)
                        {
                            idPersonal = IdAsesorAutomaticoOperaciones;//_repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoMatricula).Id;

                        }
                        else
                        {
                            idPersonal = _repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoMatricula).Id;
                        }


                        this.GenerarOportunidadOperacionesConParametros(item.Id, NombreUsuario, idPersonal, IdFaseOportunidad, IdActividadCabecera);

                        //string URI = "https://integrav4-servicios.bsginstitute.com/api/Oportunidad/GenerarOportunidadOperacionesConParametros/" + item.Id + "/" + NombreUsuario + "/" + idPersonal + "/" + IdFaseOportunidad + "/" + IdActividadCabecera;
                        //using (WebClient wc = new WebClient())
                        //{
                        //	wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //	wc.DownloadString(URI);
                        //}

                        listaCorrecto.Add(item);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(item);
                    }
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }



        [Route("[action]/{NombreUsuario}/{UsuarioCoordinadorAcademico}/{Estados}/{IdActividadCabecera}/{IdAsesorAutomaticoOperaciones}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperacionesMasivo_logica3(string NombreUsuario, string UsuarioCoordinadorAcademico, string Estados, int IdActividadCabecera, int IdAsesorAutomaticoOperaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCorrecto = new List<IdOportunidadDTO>();
                var listaIncorrecto = new List<IdOportunidadDTO>();

                var listaIdOportunidad = _repOportunidad.ObtenerOportunidadesOperacionesSinCrear_logica3(UsuarioCoordinadorAcademico, Estados);

                foreach (var item in listaIdOportunidad)
                {
                    try
                    {
                        int idPersonal;
                        if (IdAsesorAutomaticoOperaciones != 0)
                        {
                            idPersonal = IdAsesorAutomaticoOperaciones;//_repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoMatricula).Id;

                        }
                        else
                        {
                            idPersonal = _repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoAlumno).Id;
                        }
                        this.CrearOportunidadOperacionesOportunidadNueva(NombreUsuario, item.IdCentroCosto, idPersonal, item.IdAlumno, IdActividadCabecera);

                        //string URI = "https://integrav4-servicios.bsginstitute.com/api/Oportunidad/CrearOportunidadOperacionesOportunidadNueva/" + NombreUsuario + "/" + item.IdCentroCosto + "/" + idPersonal + "/" + item.IdAlumno + "/" + IdActividadCabecera;
                        //using (WebClient wc = new WebClient())
                        //{
                        //	wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //	wc.DownloadString(URI);
                        //}

                        listaCorrecto.Add(item);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(item);
                    }
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]/{NombreUsuario}/{IdActividadCabecera}/{IdAsesor}/{CodigoMatricula}/{IdCentroCosto}/{IdAlumno}")]
        [HttpGet]
        public ActionResult GenerarOportunidadOperacionesURL(string NombreUsuario, int IdActividadCabecera, int IdAsesor, string CodigoMatricula, int IdCentroCosto, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaCorrecto = new List<IdOportunidadDTO>();
                var listaIncorrecto = new List<IdOportunidadDTO>();

                List<IdOportunidadDTO> listaIdOportunidad = new List<IdOportunidadDTO>();

                IdOportunidadDTO opo = new IdOportunidadDTO
                {
                    CodigoAlumno = CodigoMatricula,
                    IdAlumno = IdAlumno,
                    IdCentroCosto = IdCentroCosto
                };

                listaIdOportunidad.Add(opo);

                foreach (var item in listaIdOportunidad)
                {
                    try
                    {
                        int idPersonal;
                        if (IdAsesor != 0)
                        {
                            idPersonal = IdAsesor;//_repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoMatricula).Id;

                        }
                        else
                        {
                            idPersonal = _repOportunidad.ObtenerIdPersonalOperacionesv2(item.CodigoAlumno).Id;
                        }
                        this.CrearOportunidadOperacionesOportunidadNueva(NombreUsuario, item.IdCentroCosto, idPersonal, item.IdAlumno, IdActividadCabecera);

                        //string URI = "https://integrav4-servicios.bsginstitute.com/api/Oportunidad/CrearOportunidadOperacionesOportunidadNueva/" + NombreUsuario + "/" + item.IdCentroCosto + "/" + idPersonal + "/" + item.IdAlumno + "/" + IdActividadCabecera;
                        //using (WebClient wc = new WebClient())
                        //{
                        //	wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //	wc.DownloadString(URI);
                        //}

                        listaCorrecto.Add(item);
                    }
                    catch (Exception e)
                    {
                        listaIncorrecto.Add(item);
                    }
                }
                return Ok(new { listaCorrecto, listaIncorrecto });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularModelo_MKT()
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);

                var lista = _repOportunidad.ObtenerOportunidades();

                foreach (var item in lista)
                {
                    try
                    {
                        //this.RegularizarModeloDataMining(item);
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            string HtmlResult = wc.DownloadString("http://integrav4-servicios.bsginstitute.com/api/Oportunidad/RegularizarModeloDataMining/" + item);
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularModelo_OportunidadDocenteMasivo()
        {
            try
            {
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio(_integraDBContext);

                var lista = _repExpositor.ObtenerExpositoresParaOportunidades();

                foreach (var item in lista)
                {
                    //this.RegularizarModeloDataMining(item);
                    if (!_repExpositor.Exist(item))
                    {
                        continue;
                    }
                    CalcularOportunidadDocentev2(_repExpositor.FirstById(item));
                    //}
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //carlos comentado
        //[Route("[action]")]
        //[HttpGet]
        public void CalcularOportunidadDocentev2(ExpositorBO Expositor)
        {
            try
            {
                var oportunidad = new OportunidadBO()
                {
                    IdCentroCosto = null,
                    IdTipoDato = 8,
                    IdOrigen = 114,
                    IdFaseOportunidad = 5,
                    IdPersonalAsignado = Expositor.IdPersonalAsignado.Value,
                    UltimoComentario = "Creacion oportunidad docente padre",
                    UltimaFechaProgramada = null,
                    IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                    FechaRegistroCampania = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = Expositor.UsuarioCreacion,
                    UsuarioModificacion = Expositor.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };
                //oportunidad.Expositor = new ExpositorBO()
                //{
                //    ApellidoMaterno = "",
                //    AsistenteCelular = "",
                //    Email1 = ""
                //};

                if (oportunidad.UltimaFechaProgramada != null)
                {
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }
                oportunidad.Expositor = Expositor;
                if (oportunidad.Expositor.Id == 0)
                {
                    CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Docente);
                }
                else
                {
                    CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Docente);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("[action]/{IdExpositor}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult CalcularOportunidadDocente(int IdExpositor, string NombreUsuario, int? IdAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Asesor = 125;
                if (IdAsesor != null)
                {
                    Asesor = IdAsesor.Value;
                }

                if (_repExpositor.Exist(IdExpositor))
                {
                    var oportunidad = new OportunidadBO()
                    {
                        IdCentroCosto = null,
                        IdTipoDato = 8,
                        IdOrigen = 114,
                        IdFaseOportunidad = 5,
                        IdPersonalAsignado = Asesor,
                        UltimoComentario = "Creacion oportunidad docente padre",
                        UltimaFechaProgramada = null,
                        IdTipoInteraccion = ValorEstatico.IdTipoInteraccionFormularioEnviadoCompleto,
                        FechaRegistroCampania = DateTime.Now,
                        Estado = true,
                        UsuarioCreacion = NombreUsuario,
                        UsuarioModificacion = NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    oportunidad.Expositor = _repExpositor.FirstById(IdExpositor);

                    if (oportunidad.UltimaFechaProgramada != null)
                    {
                        oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                    }
                    else
                    {
                        oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                    }

                    if (oportunidad.Expositor.Id == 0)
                    {
                        CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Docente);
                    }
                    else
                    {
                        CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Docente);
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 27/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe valor codificado de información de correo y realiza el envío según configuración
        /// </summary>
        /// <param name="InformacionCorreoCodificado"> Recibe información de correo para envíar desde Integra </param>
        /// <returns> Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarCorreoDesdeIntegra([FromBody] EnviarCorreoIntegraCodificadoDTO InformacionCorreoCodificado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EnviarCorreoIntegraDTO informacionCorreo = JsonConvert.DeserializeObject<EnviarCorreoIntegraDTO>(Encoding.UTF8.GetString(Convert.FromBase64String(InformacionCorreoCodificado.DatosCodificados)));
                //Validación de campos obligatorios
                if (informacionCorreo != null)
                {
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO();
                    if (informacionCorreo.Emisor != null && informacionCorreo.Asunto != null && informacionCorreo.Mensaje != null && informacionCorreo.Receptores != null && informacionCorreo.Receptores.Count > 0)
                    {
                        mailDataPersonalizado.Sender = informacionCorreo.Emisor;
                        mailDataPersonalizado.RemitenteC = informacionCorreo.EmisorNombrePersonalizado;
                        mailDataPersonalizado.Subject = informacionCorreo.Asunto;
                        mailDataPersonalizado.Message = informacionCorreo.Mensaje;
                        mailDataPersonalizado.Recipient = string.Join(",", informacionCorreo.Receptores.Distinct());

                        if (informacionCorreo.ReceptoresCopia != null && informacionCorreo.ReceptoresCopia.Count > 0)
                        {
                            mailDataPersonalizado.Cc = string.Join(",", informacionCorreo.ReceptoresCopia.Distinct());
                        }
                        if (informacionCorreo.ReceptoresCopiaOculta != null && informacionCorreo.ReceptoresCopiaOculta.Count > 0)
                        {
                            mailDataPersonalizado.Bcc = string.Join(",", informacionCorreo.ReceptoresCopiaOculta.Distinct());
                        }
                        var mailServie = new TMK_MailServiceImpl();
                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();
                        string respuesta = "El correo se envió de forma exitosa";
                        return Ok(new { Respuesta = true, Mensaje = respuesta });
                    }
                    else
                    {
                        string respuesta = "La información no se recibió de forma correcta";
                        return Ok(new { Respuesta = false, Mensaje = respuesta });
                    }
                }
                else
                {
                    string respuesta = "La información no se recibió de forma correcta";
                    return Ok(new { Respuesta = false, Mensaje = respuesta });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { Respuesta = false, Mensaje = e.Message });
            }
        }

        /// TipoFuncion: GET
        /// Autor: Gian Miranda
        /// Fecha: 02/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Regulariza los casos de creacion de oportunidad que fueron interrumpidas por alguna razon
        /// </summary>
        /// <returns> Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación </returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegularizarCreacionOportunidadInterrumpida([FromBody] FechaARevisarDTO FechaConsulta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadBo = new OportunidadBO(_integraDBContext);

                #region Regularizar TemporalAsignacion Automatica
                _repOportunidad.RegularizarAsignacionAutomaticaTemporalFlagErroneo(FechaConsulta.FechaARevisar.Date, FechaConsulta.FechaARevisar.Date);
                ValidarOportunidadesPortalWeb();
                #endregion

                #region Regularizar ValidarCasosOportunidad
                var listaOportunidadAValidar = _repOportunidad.ObtenerOportunidadSinValidacionCompleta(FechaConsulta.FechaARevisar.Date, FechaConsulta.FechaARevisar.Date);

                foreach (var oportunidad in listaOportunidadAValidar)
                {
                    try
                    {
                        ValidarCasosOportunidad(oportunidad.IdOportunidad, oportunidad.IdAsignacionAutomatica == null ? 0 : oportunidad.IdAsignacionAutomatica.Value, oportunidad.IdAsignacionAutomatica == null ? false : true);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                #endregion

                #region Regularizar Oportunidades sin probabilidad
                var listaOportunidadesSinProbabilidad = _repOportunidad.ObtenerListaIdOportunidadSinProbabilidad(FechaConsulta.FechaARevisar.Date, FechaConsulta.FechaARevisar.Date);

                foreach (var idOportunidad in listaOportunidadesSinProbabilidad.Select(s => s.Valor).ToList())
                {
                    try
                    {
                        oportunidadBo.ObtenerProbabilidadModeloPredictivo(idOportunidad);
                    }
                    catch (Exception e)
                    {
                    }
                }
                #endregion

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 25/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Finalizacion de la actividad alterno
        /// </summary>
        /// <param name="JsonDTO">DTO de entrada para la funcion</param>
        /// <returns>Ok</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult FinalizarActividadAlternoV2([FromBody] FinalizarActividadAlternoDTO JsonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadBO Oportunidad = new OportunidadBO(JsonDTO.ActividadAntigua.IdOportunidad.Value, JsonDTO.Usuario, _integraDBContext);

                // Desactivado de redireccion
                var _repOportunidadRemarketingAgenda = new OportunidadRemarketingAgendaRepositorio(_integraDBContext);

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                {
                    try
                    {
                        _repOportunidadRemarketingAgenda.DesactivarRedireccionRemarketingAnterior(JsonDTO.ActividadAntigua.IdOportunidad.Value);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (JsonDTO.ActividadAntigua.IdOportunidad.HasValue)
                    if (!_repOportunidad.Exist(x => x.Id == JsonDTO.ActividadAntigua.IdOportunidad && x.IdFaseOportunidad == JsonDTO.Oportunidad.IdFaseOportunidad))
                        return BadRequest(new { Codigo = 409, Mensaje = $"La oportunidad fue trabajada por otra persona: IdOportunidad {JsonDTO.ActividadAntigua.IdOportunidad}" });

                Oportunidad.IdFaseOportunidadIp = JsonDTO.Oportunidad.IdFaseOportunidadIp;
                Oportunidad.IdFaseOportunidadIc = JsonDTO.Oportunidad.IdFaseOportunidadIc;
                Oportunidad.FechaEnvioFaseOportunidadPf = JsonDTO.Oportunidad.FechaEnvioFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadPf = JsonDTO.Oportunidad.FechaPagoFaseOportunidadPf;
                Oportunidad.FechaPagoFaseOportunidadIc = JsonDTO.Oportunidad.FechaPagoFaseOportunidadIc;
                Oportunidad.IdFaseOportunidadPf = JsonDTO.Oportunidad.IdFaseOportunidadPf;
                Oportunidad.CodigoPagoIc = JsonDTO.Oportunidad.CodigoPagoIc;

                ActividadDetalleBO ActividadAntigua = new ActividadDetalleBO();
                ActividadAntigua.Id = JsonDTO.ActividadAntigua.Id;
                ActividadAntigua.IdActividadCabecera = JsonDTO.ActividadAntigua.IdActividadCabecera;
                ActividadAntigua.FechaProgramada = JsonDTO.ActividadAntigua.FechaProgramada;
                ActividadAntigua.FechaReal = JsonDTO.ActividadAntigua.FechaReal;
                ActividadAntigua.DuracionReal = JsonDTO.ActividadAntigua.DuracionReal;
                //ActividadAntigua.IdOcurrencia = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdEstadoActividadDetalle = JsonDTO.ActividadAntigua.IdEstadoActividadDetalle;
                ActividadAntigua.Comentario = JsonDTO.ActividadAntigua.Comentario;
                ActividadAntigua.IdAlumno = JsonDTO.ActividadAntigua.IdAlumno;
                ActividadAntigua.Actor = JsonDTO.ActividadAntigua.Actor;
                ActividadAntigua.IdOportunidad = JsonDTO.ActividadAntigua.IdOportunidad.Value;
                ActividadAntigua.IdCentralLlamada = JsonDTO.ActividadAntigua.IdCentralLlamada;
                ActividadAntigua.RefLlamada = JsonDTO.ActividadAntigua.RefLlamada;
                //ActividadAntigua.IdOcurrenciaActividad = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;
                ActividadAntigua.IdClasificacionPersona = Oportunidad.IdClasificacionPersona;
                ActividadAntigua.IdOcurrenciaAlterno = JsonDTO.ActividadAntigua.IdOcurrencia.Value;
                ActividadAntigua.IdOcurrenciaActividadAlterno = JsonDTO.ActividadAntigua.IdOcurrenciaActividad;

                if (!ActividadAntigua.HasErrors)
                {
                    Oportunidad.ActividadAntigua = ActividadAntigua;
                }
                else
                {
                    return BadRequest(ActividadAntigua.GetErrors(null));
                }

                ActividadDetalleBO ActividadNueva = new ActividadDetalleBO(JsonDTO.ActividadAntigua.Id);

                OportunidadCompetidorBO OportunidadCompetidor;
                if (JsonDTO.DatosCompuesto.OportunidadCompetidor.Id == 0)
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO();
                    OportunidadCompetidor.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.IdOportunidad;
                    OportunidadCompetidor.OtroBeneficio = JsonDTO.DatosCompuesto.OportunidadCompetidor.OtroBeneficio;
                    OportunidadCompetidor.Respuesta = JsonDTO.DatosCompuesto.OportunidadCompetidor.Respuesta;
                    OportunidadCompetidor.Completado = JsonDTO.DatosCompuesto.OportunidadCompetidor.Completado;
                    OportunidadCompetidor.FechaCreacion = DateTime.Now;
                    OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    OportunidadCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    OportunidadCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    OportunidadCompetidor.Estado = true;
                }
                else
                {
                    OportunidadCompetidor = new OportunidadCompetidorBO(JsonDTO.DatosCompuesto.OportunidadCompetidor.Id);
                }

                CalidadProcesamientoBO CalidadBO = new CalidadProcesamientoBO();
                CalidadBO.IdOportunidad = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.IdOportunidad;
                CalidadBO.PerfilCamposLlenos = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposLlenos;
                CalidadBO.PerfilCamposTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PerfilCamposTotal;
                CalidadBO.Dni = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.Dni;
                CalidadBO.PgeneralValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralValidados;
                CalidadBO.PgeneralTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PgeneralTotal;
                CalidadBO.PespecificoValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoValidados;
                CalidadBO.PespecificoTotal = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.PespecificoTotal;
                CalidadBO.BeneficiosValidados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosValidados;
                CalidadBO.BeneficiosTotales = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.BeneficiosTotales;
                CalidadBO.CompetidoresVerificacion = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.CompetidoresVerificacion;
                CalidadBO.ProblemaSeleccionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSeleccionados;
                CalidadBO.ProblemaSolucionados = JsonDTO.DatosCompuesto.OportunidadCompetidor.CalidadBO.ProblemaSolucionados;
                CalidadBO.FechaCreacion = DateTime.Now;
                CalidadBO.FechaModificacion = DateTime.Now;
                CalidadBO.UsuarioCreacion = JsonDTO.Usuario;
                CalidadBO.UsuarioModificacion = JsonDTO.Usuario;
                CalidadBO.Estado = true;
                if (!CalidadBO.HasErrors)
                    Oportunidad.CalidadProcesamiento = CalidadBO;
                else
                    return BadRequest(CalidadBO.GetErrors(null));
                if (!OportunidadCompetidor.HasErrors)
                    Oportunidad.OportunidadCompetidor = OportunidadCompetidor;
                else
                    return BadRequest(OportunidadCompetidor.GetErrors(null));

                //OportunidadCompetidor.ListaPrerequisitoGeneral = new List<OportunidadPrerequisitoGeneralBO>();
                //foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoGeneral)
                //{
                //    OportunidadPrerequisitoGeneralBO ListaPrerequisitoGeneral = new OportunidadPrerequisitoGeneralBO();
                //    ListaPrerequisitoGeneral.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                //    ListaPrerequisitoGeneral.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                //    ListaPrerequisitoGeneral.Respuesta = item.Respuesta;
                //    ListaPrerequisitoGeneral.Completado = item.Completado;
                //    ListaPrerequisitoGeneral.FechaCreacion = DateTime.Now;
                //    ListaPrerequisitoGeneral.FechaModificacion = DateTime.Now;
                //    ListaPrerequisitoGeneral.UsuarioCreacion = JsonDTO.Usuario;
                //    ListaPrerequisitoGeneral.UsuarioModificacion = JsonDTO.Usuario;
                //    ListaPrerequisitoGeneral.Estado = true;
                //
                //    if (!ListaPrerequisitoGeneral.HasErrors)
                //        OportunidadCompetidor.ListaPrerequisitoGeneral.Add(ListaPrerequisitoGeneral);
                //    else
                //        return BadRequest(ListaPrerequisitoGeneral.GetErrors(null));
                //
                //}
                //OportunidadCompetidor.ListaPrerequisitoEspecifico = new List<OportunidadPrerequisitoEspecificoBO>();
                //foreach (var item in JsonDTO.DatosCompuesto.ListaPrerequisitoEspecifico)
                //{
                //    OportunidadPrerequisitoEspecificoBO ListaPrerequisitoEspecifico = new OportunidadPrerequisitoEspecificoBO();
                //    ListaPrerequisitoEspecifico.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                //    ListaPrerequisitoEspecifico.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralBeneficio;
                //    ListaPrerequisitoEspecifico.Respuesta = item.Respuesta;
                //    ListaPrerequisitoEspecifico.Completado = item.Completado;
                //    ListaPrerequisitoEspecifico.FechaCreacion = DateTime.Now;
                //    ListaPrerequisitoEspecifico.FechaModificacion = DateTime.Now;
                //    ListaPrerequisitoEspecifico.UsuarioCreacion = JsonDTO.Usuario;
                //    ListaPrerequisitoEspecifico.UsuarioModificacion = JsonDTO.Usuario;
                //    ListaPrerequisitoEspecifico.Estado = true;
                //
                //    if (!ListaPrerequisitoEspecifico.HasErrors)
                //        OportunidadCompetidor.ListaPrerequisitoEspecifico.Add(ListaPrerequisitoEspecifico);
                //    else
                //        return BadRequest(ListaPrerequisitoEspecifico.GetErrors(null));
                //}
                //OportunidadCompetidor.ListaBeneficio = new List<OportunidadBeneficioBO>();
                //foreach (var item in JsonDTO.DatosCompuesto.ListaBeneficio)
                //{
                //    OportunidadBeneficioBO ListaBeneficio = new OportunidadBeneficioBO();
                //    ListaBeneficio.IdOportunidadCompetidor = item.IdOportunidadCompetidor;
                //    ListaBeneficio.IdBeneficio = item.IdBeneficio;
                //    ListaBeneficio.Respuesta = item.Respuesta;
                //    ListaBeneficio.Completado = item.Completado;
                //    ListaBeneficio.FechaCreacion = DateTime.Now;
                //    ListaBeneficio.FechaModificacion = DateTime.Now;
                //    ListaBeneficio.UsuarioCreacion = JsonDTO.Usuario;
                //    ListaBeneficio.UsuarioModificacion = JsonDTO.Usuario;
                //    ListaBeneficio.Estado = true;
                //    if (!ListaBeneficio.HasErrors)
                //    {
                //        OportunidadCompetidor.ListaBeneficio.Add(ListaBeneficio);
                //    }
                //    else
                //        return BadRequest(ListaBeneficio.GetErrors(null));
                //}

                //======================================
                ProgramaGeneralBeneficioRespuestaRepositorio _repBeneficioAlternoRespuesta = new ProgramaGeneralBeneficioRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralBeneficioRespuestaBO beneficioAlterno = new ProgramaGeneralBeneficioRespuestaBO();
                var listaBeneficioAlternoAgrupado = JsonDTO.DatosCompuesto.ListaBeneficioAlterno.GroupBy(x => x.IdBeneficio).Select(x => x.First()).ToList();
                foreach (var item in listaBeneficioAlternoAgrupado)
                {
                    beneficioAlterno = _repBeneficioAlternoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralBeneficio == item.IdBeneficio);

                    if (beneficioAlterno != null)
                    {
                        beneficioAlterno.Respuesta = item.Respuesta;
                        beneficioAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        beneficioAlterno.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Update(beneficioAlterno);
                    }
                    else
                    {
                        ProgramaGeneralBeneficioRespuestaBO beneficioAlternoV2 = new ProgramaGeneralBeneficioRespuestaBO();
                        beneficioAlternoV2.IdOportunidad = item.IdOportunidad;
                        beneficioAlternoV2.IdProgramaGeneralBeneficio = item.IdBeneficio;
                        beneficioAlternoV2.Respuesta = item.Respuesta;
                        beneficioAlternoV2.Estado = true;
                        beneficioAlternoV2.UsuarioCreacion = JsonDTO.Usuario;
                        beneficioAlternoV2.UsuarioModificacion = JsonDTO.Usuario;
                        beneficioAlternoV2.FechaCreacion = DateTime.Now;
                        beneficioAlternoV2.FechaModificacion = DateTime.Now;
                        _repBeneficioAlternoRespuesta.Insert(beneficioAlternoV2);
                    }
                }

                ProgramaGeneralMotivacionRespuestaRepositorio _repMotiviacionRespuesta = new ProgramaGeneralMotivacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralMotivacionRespuestaBO motivacionRespuesta = new ProgramaGeneralMotivacionRespuestaBO();
                var listaMotivacionRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaMotivacion.GroupBy(x => x.IdMotivacion).Select(x => x.First()).ToList();
                foreach (var item in listaMotivacionRespuestaAgrupado)
                {
                    motivacionRespuesta = _repMotiviacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralMotivacion == item.IdMotivacion);

                    if (motivacionRespuesta != null)
                    {
                        motivacionRespuesta.Respuesta = item.Respuesta;
                        motivacionRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        motivacionRespuesta.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Update(motivacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralMotivacionRespuestaBO motivacionRespuestaAlterno = new ProgramaGeneralMotivacionRespuestaBO();
                        motivacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        motivacionRespuestaAlterno.IdProgramaGeneralMotivacion = item.IdMotivacion;
                        motivacionRespuestaAlterno.Respuesta = item.Respuesta;
                        motivacionRespuestaAlterno.Estado = true;
                        motivacionRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        motivacionRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        motivacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        motivacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repMotiviacionRespuesta.Insert(motivacionRespuestaAlterno);
                    }
                }

                PublicoObjetivoRespuestaRepositorio _repPublicoObjetivoRespuesta = new PublicoObjetivoRespuestaRepositorio(_integraDBContext);
                PublicoObjetivoRespuestaBO publicoObjetivoRespuesta = new PublicoObjetivoRespuestaBO();
                var listaPublicoObjetivoRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaPublicoObjetivo.GroupBy(x => x.IdPublicoObjetivo).Select(x => x.First()).ToList();
                foreach (var item in listaPublicoObjetivoRespuestaAgrupado)
                {
                    publicoObjetivoRespuesta = _repPublicoObjetivoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdDocumentoSeccionPw == item.IdPublicoObjetivo);

                    if (publicoObjetivoRespuesta != null)
                    {
                        publicoObjetivoRespuesta.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        publicoObjetivoRespuesta.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Update(publicoObjetivoRespuesta);
                    }
                    else
                    {
                        PublicoObjetivoRespuestaBO publicoObjetivoRespuestaAlterno = new PublicoObjetivoRespuestaBO();
                        publicoObjetivoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        publicoObjetivoRespuestaAlterno.IdDocumentoSeccionPw = item.IdPublicoObjetivo;
                        publicoObjetivoRespuestaAlterno.NivelCumplimiento = item.Respuesta;
                        publicoObjetivoRespuestaAlterno.Estado = true;
                        publicoObjetivoRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        publicoObjetivoRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        publicoObjetivoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        publicoObjetivoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPublicoObjetivoRespuesta.Insert(publicoObjetivoRespuestaAlterno);
                    }
                }

                ProgramaGeneralCertificacionRespuestaRepositorio _repCertificacionRespuesta = new ProgramaGeneralCertificacionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralCertificacionRespuestaBO certificacionRespuesta = new ProgramaGeneralCertificacionRespuestaBO();
                var listaCertificacionRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaCertificacion.GroupBy(x => x.IdCertificacion).Select(x => x.First()).ToList();
                foreach (var item in listaCertificacionRespuestaAgrupado)
                {
                    certificacionRespuesta = _repCertificacionRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralCertificacion == item.IdCertificacion);

                    if (certificacionRespuesta != null)
                    {
                        certificacionRespuesta.Respuesta = item.Respuesta;
                        certificacionRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        certificacionRespuesta.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Update(certificacionRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralCertificacionRespuestaBO certificacionRespuestaAlterno = new ProgramaGeneralCertificacionRespuestaBO();
                        certificacionRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        certificacionRespuestaAlterno.IdProgramaGeneralCertificacion = item.IdCertificacion;
                        certificacionRespuestaAlterno.Respuesta = item.Respuesta;
                        certificacionRespuestaAlterno.Estado = true;
                        certificacionRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        certificacionRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        certificacionRespuestaAlterno.FechaCreacion = DateTime.Now;
                        certificacionRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repCertificacionRespuesta.Insert(certificacionRespuestaAlterno);
                    }
                }

                ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio _repProblemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuesta = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                var listaProblemaRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaSolucionesAlterno.GroupBy(x => x.IdProblema).Select(x => x.First()).ToList();
                foreach (var item in listaProblemaRespuestaAgrupado)
                {
                    problemaRespuesta = _repProblemaRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralProblemaDetalleSolucion == item.IdProblema);

                    if (problemaRespuesta != null)
                    {
                        problemaRespuesta.EsSeleccionado = item.Seleccionado;
                        problemaRespuesta.EsSolucionado = item.Solucionado;
                        problemaRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        problemaRespuesta.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Update(problemaRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralProblemaDetalleSolucionRespuestaBO problemaRespuestaAlterno = new ProgramaGeneralProblemaDetalleSolucionRespuestaBO();
                        problemaRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        problemaRespuestaAlterno.IdProgramaGeneralProblemaDetalleSolucion = item.IdProblema;
                        problemaRespuestaAlterno.EsSeleccionado = item.Seleccionado;
                        problemaRespuestaAlterno.EsSolucionado = item.Solucionado;
                        problemaRespuestaAlterno.Estado = true;
                        problemaRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        problemaRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        problemaRespuestaAlterno.FechaCreacion = DateTime.Now;
                        problemaRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repProblemaRespuesta.Insert(problemaRespuestaAlterno);
                    }
                }

                ProgramaGeneralPrerequisitoRespuestaRepositorio _repPrerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaRepositorio(_integraDBContext);
                ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuesta = new ProgramaGeneralPrerequisitoRespuestaBO();
                var listaPrerequisitoRespuestaAgrupado = JsonDTO.DatosCompuesto.ListaPrerequisitoGeneralAlterno.GroupBy(x => x.IdProgramaGeneralPrerequisito).Select(x => x.First()).ToList();
                foreach (var item in listaPrerequisitoRespuestaAgrupado)
                {
                    prerequisitoRespuesta = _repPrerequisitoRespuesta.FirstBy(x => x.IdOportunidad == item.IdOportunidad && x.IdProgramaGeneralPrerequisito == item.IdProgramaGeneralPrerequisito);

                    if (prerequisitoRespuesta != null)
                    {
                        prerequisitoRespuesta.Respuesta = item.Respuesta;
                        prerequisitoRespuesta.UsuarioModificacion = JsonDTO.Usuario;
                        prerequisitoRespuesta.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Update(prerequisitoRespuesta);
                    }
                    else
                    {
                        ProgramaGeneralPrerequisitoRespuestaBO prerequisitoRespuestaAlterno = new ProgramaGeneralPrerequisitoRespuestaBO();
                        prerequisitoRespuestaAlterno.IdOportunidad = item.IdOportunidad;
                        prerequisitoRespuestaAlterno.IdProgramaGeneralPrerequisito = item.IdProgramaGeneralPrerequisito;
                        prerequisitoRespuestaAlterno.Respuesta = item.Respuesta;
                        prerequisitoRespuestaAlterno.Estado = true;
                        prerequisitoRespuestaAlterno.UsuarioCreacion = JsonDTO.Usuario;
                        prerequisitoRespuestaAlterno.UsuarioModificacion = JsonDTO.Usuario;
                        prerequisitoRespuestaAlterno.FechaCreacion = DateTime.Now;
                        prerequisitoRespuestaAlterno.FechaModificacion = DateTime.Now;
                        _repPrerequisitoRespuesta.Insert(prerequisitoRespuestaAlterno);
                    }
                }

                //======================================

                OportunidadCompetidor.ListaCompetidor = new List<DetalleOportunidadCompetidorBO>();
                foreach (var item in JsonDTO.DatosCompuesto.ListaCompetidor)
                {
                    DetalleOportunidadCompetidorBO ListaCompetidor = new DetalleOportunidadCompetidorBO();
                    ListaCompetidor.IdOportunidadCompetidor = 0;
                    ListaCompetidor.IdCompetidor = item;
                    ListaCompetidor.Estado = true;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.FechaCreacion = DateTime.Now;
                    ListaCompetidor.FechaModificacion = DateTime.Now;
                    ListaCompetidor.UsuarioCreacion = JsonDTO.Usuario;
                    ListaCompetidor.UsuarioModificacion = JsonDTO.Usuario;
                    ListaCompetidor.Estado = true;

                    if (!ListaCompetidor.HasErrors)
                        OportunidadCompetidor.ListaCompetidor.Add(ListaCompetidor);
                    else
                        return BadRequest(ListaCompetidor.GetErrors(null));
                }
                //Oportunidad.ListaSoluciones = new List<SolucionClienteByActividadBO>();
                //foreach (var item in JsonDTO.DatosCompuesto.ListaSoluciones)
                //{
                //    SolucionClienteByActividadBO ListaSoluciones = new SolucionClienteByActividadBO();
                //    ListaSoluciones.IdOportunidad = item.IdOportunidad;
                //    ListaSoluciones.IdActividadDetalle = item.IdActividadDetalle;
                //    ListaSoluciones.IdCausa = item.IdCausa;
                //    ListaSoluciones.IdPersonal = item.IdPersonal;
                //    ListaSoluciones.Solucionado = item.Solucionado;
                //    ListaSoluciones.IdProblemaCliente = item.IdProblemaCliente;
                //    ListaSoluciones.OtroProblema = item.OtroProblema;
                //    ListaSoluciones.FechaCreacion = DateTime.Now;
                //    ListaSoluciones.FechaModificacion = DateTime.Now;
                //    ListaSoluciones.UsuarioCreacion = JsonDTO.Usuario;
                //    ListaSoluciones.UsuarioModificacion = JsonDTO.Usuario;
                //    ListaSoluciones.Estado = true;
                //
                //    if (!ListaSoluciones.HasErrors)
                //        Oportunidad.ListaSoluciones.Add(ListaSoluciones);
                //    else
                //        return BadRequest(ListaSoluciones.GetErrors(null));
                //}

                if (!Oportunidad.HasErrors)
                {
                    Oportunidad.ActividadNueva = ActividadNueva;
                    OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                    FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                    OportunidadMaximaPorCategoriaRepositorio _repOportunidadMaximaPorCategoria = new OportunidadMaximaPorCategoriaRepositorio(_integraDBContext);
                    PreCalculadaCambioFaseRepositorio _repPreCalculadaCambioFase = new PreCalculadaCambioFaseRepositorio(_integraDBContext);
                    LlamadaActividadRepositorio _repLlamada = new LlamadaActividadRepositorio(_integraDBContext);

                    ActividadNueva.LlamadaActividad = null;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            Oportunidad.FinalizarActividadAlterno(false, JsonDTO.Oportunidad, JsonDTO.ActividadAntigua.IdOcurrenciaActividad);
                            if (Oportunidad.PreCalculadaCambioFase != null)
                            {
                                Oportunidad.PreCalculadaCambioFase.Contador = _repPreCalculadaCambioFase.ExistePreCalculadaCambioFase(Oportunidad.PreCalculadaCambioFase);
                                _repPreCalculadaCambioFase.Insert(Oportunidad.PreCalculadaCambioFase);
                            }

                            if (_repFaseOportunidad.ValidarFaseCierreOportunidad(Oportunidad.IdFaseOportunidad))
                            {
                                if (_repFaseOportunidad.ValidarFaseIS(Oportunidad.IdFaseOportunidad))
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 0);
                                }
                                else
                                {
                                    _repOportunidadMaximaPorCategoria.ActualizarDatosEstaticosPantalla2(Oportunidad.IdPersonalAsignado, Oportunidad.IdCategoriaOrigen, 1);

                                }
                            }
                            _repOportunidad.Update(Oportunidad);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            List<string> correos = new List<string>();
                            correos.Add("sistemas@bsginstitute.com");

                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error FinalizarActividad Transaction";
                            mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;

                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            return BadRequest(ex.Message);
                        }
                    }

                    ////Insertar actualizar oportunidad - hijos a v3
                    //try
                    //{
                    //    string URI = "http://localhost:4348/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id.ToString();
                    //    using (WebClient wc = new WebClient())
                    //    {
                    //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    //        wc.DownloadString(URI);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    List<string> correos = new List<string>();
                    //    correos.Add("sistemas@bsginstitute.com");

                    //    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    //    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    //    mailData.Sender = "jcayo@bsginstitute.com";
                    //    mailData.Recipient = string.Join(",", correos);
                    //    mailData.Subject = "Error FinalizarActividad Sincronizacion";
                    //    mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    //    mailData.Cc = "";
                    //    mailData.Bcc = "";
                    //    mailData.AttachedFiles = null;

                    //    Mailservice.SetData(mailData);
                    //    Mailservice.SendMessageTask();
                    //}

                    var realizadas = _repActividadDetalle.ObtenerAgendaRealizadaRegistroTiempoReal(Oportunidad.ActividadNueva.Id);
                    return Ok(new { realizadas = realizadas, IdOportunidad = Oportunidad.Id });
                }
                else
                {
                    return BadRequest(Oportunidad.GetErrors(null));
                }
            }
            catch (Exception Ex)
            {
                List<string> correos = new List<string>();
                correos.Add("sistemas@bsginstitute.com");

                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO();
                mailData.Sender = "jcayo@bsginstitute.com";
                mailData.Recipient = string.Join(",", correos);
                mailData.Subject = "Error FinalizarActividad General";
                mailData.Message = "IdOportunidad: " + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>Asesor:" + JsonDTO.Usuario == null ? "" : JsonDTO.Usuario + "<br/>" + JsonDTO.ActividadAntigua.IdOportunidad.ToString() + "<br/>" + Ex.Message + " <br/> Mensaje toString <br/> " + Ex.ToString();
                mailData.Cc = "";
                mailData.Bcc = "";
                mailData.AttachedFiles = null;

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();

                return BadRequest(Ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using iTextSharp.tool.xml.html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.ConditionalFormatting;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/MailingEnvioAutomatico
    /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
    /// Fecha: 29/01/2021
    /// <summary>
    /// Proporciona funciones para el envio de mailing automatico
    /// </summary>
    [Route("api/MailingEnvioAutomatico")]
    public class MailingEnvioAutomaticoController : Controller
    {

        private readonly integraDBContext _integraDBContext;
        private readonly ConfiguracionEnvioMailingRepositorio _repConfiguracionEnvioMailing;
        private readonly ActividadCabeceraBO actividadCabecera;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly PersonalRepositorio _repPersonal;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly ConfiguracionEnvioMailingDetalleRepositorio _repConfiguracionEnvioMailingDetalle;
        private readonly ConjuntoListaDetalleRepositorio _repConjuntoListaDetalle;
        private readonly ConjuntoListaResultadoRepositorio _repConjuntoListaResultado;
        private readonly MandrilEnvioCorreoRepositorio _repMandrilEnvioCorreo;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private ReemplazoEtiquetaPlantillaBO reemplazoEtiquetaPlantilla;
        private readonly EnvioMasivoPlantillaBO envioMasivoPlantilla;

        public MailingEnvioAutomaticoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _integraDBContext.ChangeTracker.AutoDetectChangesEnabled = false;
            _repConjuntoListaDetalle = new ConjuntoListaDetalleRepositorio(integraDBContext);
            _repConjuntoListaResultado = new ConjuntoListaResultadoRepositorio(integraDBContext);
            _repConfiguracionEnvioMailing = new ConfiguracionEnvioMailingRepositorio(integraDBContext);
            _repConfiguracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext);
            actividadCabecera = new ActividadCabeceraBO(integraDBContext);
            _repOportunidad = new OportunidadRepositorio(integraDBContext);
            _repPersonal = new PersonalRepositorio(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repMandrilEnvioCorreo = new MandrilEnvioCorreoRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);

            envioMasivoPlantilla = new EnvioMasivoPlantillaBO(_integraDBContext);
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 29/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Metodo para probar las plantillas de nuevas oportunidades, si en caso presentan fallas o probar nueva funcionalidad
        /// </summary>
        /// <param name="ParametroEtiqueta">Objeto de clase EtiquetaParametroProbarPlantillaDTO</param>
        /// <returns>HTML con el resultado de la plantilla y la oportunidad</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProbarPlantilla([FromBody] EtiquetaParametroProbarPlantillaDTO ParametroEtiqueta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlantillaEmailMandrillDTO emailCalculado = new PlantillaEmailMandrillDTO();

                if (ParametroEtiqueta.TipoPlantilla == 1) /*NuevasOportunidades*/
                {
                    if (!ParametroEtiqueta.IdOportunidad.HasValue)
                        return BadRequest("Falta el parametro IdOportunidad");

                    reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = ParametroEtiqueta.IdOportunidad.Value,
                        IdPlantilla = ParametroEtiqueta.IdPlantilla
                    };
                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                }
                else if (ParametroEtiqueta.TipoPlantilla == 2) /*AlumnoSinOportunidad*/
                {
                    if (!ParametroEtiqueta.IdAlumno.HasValue)
                        return BadRequest("Falta el parametro IdAlumno");

                    EtiquetaParametroAlumnoSinOportunidadDTO parametrosEtiquetas = new EtiquetaParametroAlumnoSinOportunidadDTO
                    {
                        IdAlumno = ParametroEtiqueta.IdAlumno.Value,
                        IdPGeneral = ParametroEtiqueta.IdPGeneral
                    };

                    reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdPlantilla = ParametroEtiqueta.IdPlantilla
                    };
                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasAlumnoSinOportunidad(parametrosEtiquetas);
                }

                emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;

                return Ok(emailCalculado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque - Fischer Valdez - Gian Miranda
        /// Fecha: 08/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Procesa la lista antes del envio automatico de operaciones
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ProcesarLista([FromBody] List<ConjuntoListaDetalleMailingMasivoDTO> ListaConfiguracionEnvioMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaConfiguracionEnvioDetalleMailing = new List<ConfiguracionEnvioMailingDetalleBO>();
                foreach (var configuracionEnvioMailing in ListaConfiguracionEnvioMailing)
                {
                    //Si no existe la configuracion
                    if (!_repConfiguracionEnvioMailing.Exist(configuracionEnvioMailing.Id.Value))
                    {
                        continue;
                    }

                    //No existe un conjunto lista detalle
                    if (!_repConjuntoListaDetalle.Exist(configuracionEnvioMailing.IdConjuntoListaDetalle))
                    {
                        continue;
                    }

                    // Nos traemos los registros de ese conjunto lista detalle
                    //var listaConjuntoListaResultado = _repConjuntoListaResultado.ObtenerPorConjuntoListaDetalle(configuracionEnvioMailing.IdConjuntoListaDetalle);
                    var listaConjuntoListaResultado = _repConjuntoListaResultado.ObtenerPorConjuntoListaDetalleRedireccion(configuracionEnvioMailing.IdConjuntoListaDetalle);

                    foreach (var conjuntoListaResultado in listaConjuntoListaResultado)
                    {

                        try
                        {
                            reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            {
                                IdOportunidad = conjuntoListaResultado.IdOportunidad.Value,
                                IdPlantilla = configuracionEnvioMailing.IdPlantilla.Value
                            };
                            reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                            var emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;

                            var configuracionEnvioDetalleMailing = new ConfiguracionEnvioMailingDetalleBO()
                            {
                                Asunto = emailCalculado.Asunto,
                                CuerpoHtml = emailCalculado.CuerpoHTML,
                                EnviadoCorrectamente = false,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdConfiguracionEnvioMailing = configuracionEnvioMailing.Id.Value,
                                IdMandrilEnvioCorreo = 0,
                                IdConjuntoListaResultado = conjuntoListaResultado.Id,
                                UsuarioCreacion = "ProcesoAutomatico",
                                UsuarioModificacion = "ProcesoAutomatico",
                                MensajeError = ""
                            };
                            listaConfiguracionEnvioDetalleMailing.Add(configuracionEnvioDetalleMailing);
                        }
                        catch (Exception e)
                        {
                            //throw;
                            continue;
                        }

                    }
                }

                // Insertamos en la tabla los datos calculados
                // Mantener por respaldo
                //_repConfiguracionEnvioMailingDetalle.Insert(listaConfiguracionEnvioDetalleMailing);

                // Nueva version
                _repConfiguracionEnvioMailingDetalle.InsertarConfiguracionEnvioMailingDetalle(listaConfiguracionEnvioDetalleMailing);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarLista([FromBody] List<ConjuntoListaDetalleMailingMasivoDTO> ListaConfiguracionEnvioMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaConfiguracionEnvioDetalleMailing = new List<ConfiguracionEnvioMailingDetalleBO>();
                foreach (var configuracionEnvioMailing in ListaConfiguracionEnvioMailing)
                {
                    // Si no existe la configuracion
                    if (!_repConfiguracionEnvioMailing.Exist(configuracionEnvioMailing.Id.Value))
                    {
                        continue;
                    }

                    // No existe un conjunto lista detalle
                    if (!_repConjuntoListaDetalle.Exist(configuracionEnvioMailing.IdConjuntoListaDetalle))
                    {
                        continue;
                    }

                    // Todos los correos que se enviaran, que no se se hayan enviado correctamente y no tengan error
                    // Mantener por respaldo
                    //var listaConfiguracioEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.GetBy(x => x.IdConfiguracionEnvioMailing == configuracionEnvioMailing.Id && x.EnviadoCorrectamente  == false && string.IsNullOrEmpty(x.MensajeError));

                    var listaConfiguracioEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(configuracionEnvioMailing.Id.Value, false);

                    foreach (var configuracionEnvioMailingDetalle in listaConfiguracioEnvioMailingDetalle)
                    {
                        /*if (!_repConfiguracionEnvioMailingDetalle.Exist(configuracionEnvioMailingDetalle.Id))*/
                        if (!_repConfiguracionEnvioMailingDetalle.ExisteConfiguracionEnvioMailingDetalle(configuracionEnvioMailingDetalle.Id))
                        {
                            continue;
                        }

                        // Mantener por respaldo
                        // var configuracionEnvioDetalleMailing = _repConfiguracionEnvioMailingDetalle.FirstById(configuracionEnvioMailingDetalle.Id);
                        var configuracionEnvioDetalleMailing = _repConfiguracionEnvioMailingDetalle.BuscaConfiguracionEnvioMailingDetallePorId(configuracionEnvioMailingDetalle.Id);

                        try
                        {
                            var listaMandrillEnvioCorreo = envioMasivoPlantilla.EnvioAutomaticoPorConfiguracionEnvioMailingDetalle(configuracionEnvioDetalleMailing.Id);

                            configuracionEnvioDetalleMailing.EnviadoCorrectamente = true;
                            configuracionEnvioDetalleMailing.MensajeError = "";
                            configuracionEnvioDetalleMailing.IdMandrilEnvioCorreo = listaMandrillEnvioCorreo.FirstOrDefault().Id;
                            // Tomamos el 1ro, no se debe enviar con copia porque la api de mandrill retorna un elemento enviado por cada email,
                            // Tambien cuenta cuando es copia adjunto
                        }
                        catch (Exception e)
                        {
                            configuracionEnvioDetalleMailing.EnviadoCorrectamente = false;
                            configuracionEnvioDetalleMailing.MensajeError = JsonConvert.SerializeObject(e);
                            configuracionEnvioDetalleMailing.IdMandrilEnvioCorreo = 0;
                        }
                        configuracionEnvioDetalleMailing.UsuarioModificacion = "EnvioAutomatico";
                        configuracionEnvioDetalleMailing.FechaModificacion = DateTime.Now;

                        // Respaldo
                        //_repConfiguracionEnvioMailingDetalle.Update(configuracionEnvioDetalleMailing);

                        // Nuevo
                        _repConfiguracionEnvioMailingDetalle.ActualizarConfiguracionEnvioMailingDetalle(configuracionEnvioDetalleMailing);
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
        public ActionResult EnviarListaDinamica([FromBody] List<ConjuntoListaDetalleMailingDinamicoDTO> ListaConfiguracionEnvioMailing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaConfiguracionEnvioDetalleMailing = new List<ConfiguracionEnvioMailingDetalleBO>();
                foreach (var configuracionEnvioMailing in ListaConfiguracionEnvioMailing)
                {
                    //Si no existe la configuracion
                    if (!_repConfiguracionEnvioMailing.Exist(configuracionEnvioMailing.Id.Value))
                    {
                        continue;
                    }

                    //No existe un conjunto lista detalle
                    if (!_repConjuntoListaDetalle.Exist(configuracionEnvioMailing.IdConjuntoListaDetalle))
                    {
                        continue;
                    }

                    //todos los correos que se enviaran, que no se hayan enviado correctamente y no tengan error
                    var listaConfiguracioEnvioMailingDetalle = _repConfiguracionEnvioMailingDetalle.GetBy(x => x.IdConfiguracionEnvioMailing == configuracionEnvioMailing.Id && x.EnviadoCorrectamente == false && string.IsNullOrEmpty(x.MensajeError));

                    foreach (var configuracionEnvioMailingDetalle in listaConfiguracioEnvioMailingDetalle)
                    {
                        if (!_repConfiguracionEnvioMailingDetalle.Exist(configuracionEnvioMailingDetalle.Id))
                        {
                            continue;
                        }

                        var configuracionEnvioDetalleMailing = _repConfiguracionEnvioMailingDetalle.FirstById(configuracionEnvioMailingDetalle.Id);

                        try
                        {
                            var listaMandrillEnvioCorreo = envioMasivoPlantilla.EnvioAutomaticoPorConfiguracionEnvioMailingDetalleDinamico(configuracionEnvioDetalleMailing.Id);

                            configuracionEnvioDetalleMailing.EnviadoCorrectamente = true;
                            configuracionEnvioDetalleMailing.IdMandrilEnvioCorreo = listaMandrillEnvioCorreo.FirstOrDefault().Id;
                            //Tomamos el 1ro, no se debe enviar con copia porque la api de mandrill retorna un elemento enviado por cada email,
                            //tambien cuenta cuando es copia adjunto
                        }
                        catch (Exception e)
                        {
                            configuracionEnvioDetalleMailing.MensajeError = JsonConvert.SerializeObject(e);
                            configuracionEnvioDetalleMailing.EnviadoCorrectamente = false;
                        }
                        configuracionEnvioDetalleMailing.UsuarioModificacion = "EnvioAutomatico";
                        configuracionEnvioDetalleMailing.FechaModificacion = DateTime.Now;
                        _repConfiguracionEnvioMailingDetalle.Update(configuracionEnvioDetalleMailing);
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
        public ActionResult ProcesarListaNuevasOportunidades([FromBody] List<ConjuntoListaDetalleMailingDinamicoDTO> ListaConfiguracionEnvioMailing, DateTime HoraEjecucion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaConfiguracionEnvioDetalleMailing = new List<ConfiguracionEnvioMailingDetalleBO>();

                foreach (var configuracionEnvioMailing in ListaConfiguracionEnvioMailing)
                {
                    // Si no existe la configuracion
                    if (!_repConfiguracionEnvioMailing.Exist(configuracionEnvioMailing.Id.Value))
                    {
                        continue;
                    }

                    // No existe un conjunto lista detalle
                    if (!_repConjuntoListaDetalle.Exist(configuracionEnvioMailing.IdConjuntoListaDetalle))
                    {
                        continue;
                    }

                    FiltroFasesOportunidadAlumnoDTO obtenerHoraMinimaFasesCadena = _repConjuntoListaResultado.ObtenerHoraMinimaFasesCadena(new FiltroHoraMinimaFasesCadenaDTO { IdConjuntoListaDetalle = configuracionEnvioMailing.IdConjuntoListaDetalle, HoraEjecucion = HoraEjecucion });

                    // Crear una lista de alumnos con oportunidades
                    List<AlumnoOportunidadFiltroDTO> listaResultadoAlumnoOportunidad = _repConjuntoListaResultado.ObtenerAlumnoOportunidadEnvioAutomatico(new AlumnoOportunidadEnvioAutomaticoDTO { IdConjuntoListaDetalle = configuracionEnvioMailing.IdConjuntoListaDetalle, HoraMinima = obtenerHoraMinimaFasesCadena.HoraMinima, FaseOportunidad = obtenerHoraMinimaFasesCadena.FasesOportunidad, ConsiderarEnviados = obtenerHoraMinimaFasesCadena.ConsiderarEnviados, IdPlantilla = configuracionEnvioMailing.IdPlantilla.Value });

                    foreach (var conjuntoListaResultado in listaResultadoAlumnoOportunidad)
                    {
                        // Filtrar por alumno
                        obtenerHoraMinimaFasesCadena.IdAlumno = conjuntoListaResultado.IdAlumno;

                        /*Consultar por db las oportunidades de los alumnos que coinciden con el tiempo mencionado*/
                        try
                        {   /*Consultamos por DB las oportunidades de los alumnos devueltos por conjuntolista que cumplen con la condicion*/
                            foreach (AlumnoOportunidadFiltroDTO elemento in listaResultadoAlumnoOportunidad.Where(w => w.IdAlumno == obtenerHoraMinimaFasesCadena.IdAlumno))
                            {
                                reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                                {
                                    IdOportunidad = elemento.IdOportunidad,
                                    IdPlantilla = configuracionEnvioMailing.IdPlantilla.Value
                                };
                                reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                                var emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;

                                var configuracionEnvioDetalleMailing = new ConfiguracionEnvioMailingDetalleBO()
                                {
                                    Asunto = emailCalculado.Asunto,
                                    CuerpoHtml = emailCalculado.CuerpoHTML,
                                    EnviadoCorrectamente = false,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    IdConfiguracionEnvioMailing = configuracionEnvioMailing.Id.Value,
                                    IdConjuntoListaResultado = conjuntoListaResultado.Id,
                                    UsuarioCreacion = "ProcesoAutomatico",
                                    UsuarioModificacion = "ProcesoAutomatico",
                                    MensajeError = "",
                                    IdPlantilla = reemplazoEtiquetaPlantilla.IdPlantilla,
                                    IdOportunidad = reemplazoEtiquetaPlantilla.IdOportunidad
                                };
                                listaConfiguracionEnvioDetalleMailing.Add(configuracionEnvioDetalleMailing);
                            }
                        }
                        catch (Exception e)
                        {
                            //throw;
                            continue;
                        }
                    }
                }

                //Insertamos en la tabla los datos calculados
                _repConfiguracionEnvioMailingDetalle.Insert(listaConfiguracionEnvioDetalleMailing);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/IdMatriculaCabecera/IdPlantilla")]
        [HttpGet]
        public ActionResult EnvioPersonalizado(int IdMatriculaCabecera, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ListaAlumnos = _repConfiguracionEnvioMailingDetalle.ObtenerRegistrosParaEnvioPersonalizado(IdMatriculaCabecera);

                foreach (var configuracionEnvioMailing in ListaAlumnos)
                {
                    List<string> correosPersonalizados = new List<string>
                    {
                        configuracionEnvioMailing.Email1
                    };

                    var plantilla = _repConfiguracionEnvioMailingDetalle.ObtenerContenidoPlantilla(IdPlantilla);

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = configuracionEnvioMailing.EmailCoordinador,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = "Notificacion Coordinadora",
                        Message = plantilla,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    var listaIdsMailChimp = mailServie.SendMessageTask();
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: _ _ _ _ , Edgar S.
        /// Fecha: 30/04/2021
        /// <summary>
        /// En base a un IdOportunidad, IdPlantilla y el flag de usar el personal por defecto o se genera y envía el correo especificado
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad que enviara el correo</param>
        /// <param name="IdPlantilla">Id de la plantilla que se enviara</param>
        /// <param name="PersonalPorDefecto">Flag para determinar si se usara el personal por defecto que se encuentra en la tabla conf.T_ConfiguracionFija</param>
        /// <returns>Response 200 si ha sido exitoso el envio o 400 si ha fallado</returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}/{PersonalPorDefecto?}")]
        [HttpGet]
        public ActionResult EnvioCorreoOportunidadPlantilla(int IdOportunidad, int IdPlantilla, bool PersonalPorDefecto = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    throw new Exception("Oportunidad no valida");
                }

                OportunidadBO oportunidad = _repOportunidad.FirstById(IdOportunidad);

                int idPersonalFinal = PersonalPorDefecto ? ValorEstatico.IdPersonalCorreoPorDefecto : oportunidad.IdPersonalAsignado;

                PersonalBO personal = _repPersonal.FirstById(idPersonalFinal);

                if (personal.Id == 125)
                {
                    throw new Exception("Asesor automatico no es valido para envio de correos");
                }

                if (_repCentroCosto.FirstById(oportunidad.IdCentroCosto.GetValueOrDefault()).Nombre.Contains("INSTITUTO"))
                {
                    IdPlantilla = ValorEstatico.IdPlantillaInformacionCarrera;
                }

                AlumnoBO alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                List<string> correosPersonalizados = new List<string>
                {
                    alumno.Email1
                };

                reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };
                reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades(PersonalPorDefecto);

                PlantillaEmailMandrillDTO emailFinalEnvio = reemplazoEtiquetaPlantilla.EmailReemplazado;

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailFinalEnvio.Asunto,
                    Message = emailFinalEnvio.CuerpoHTML,
                    Cc = string.Empty,
                    Bcc = string.Empty,
                    AttachedFiles = null
                };

                TMK_MailServiceImpl mailService = new TMK_MailServiceImpl();

                mailService.SetData(mailDataPersonalizado);
                List<TMKMensajeIdDTO> listaIdsMailChimp = mailService.SendMessageTask();

                List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                foreach (var mensaje in listaIdsMailChimp)
                {
                    var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 7, //Envio masivo automatico nuevas oportunidades
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 1, // Correo enviado automaticamente
                        FechaEnvio = DateTime.Now,
                        Asunto = emailFinalEnvio.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "EnvioAutomatico",
                        UsuarioModificacion = "EnvioAutomatico",
                        EsEnvioMasivo = false
                    };

                    if (!mandrilEnvioCorreoBO.HasErrors)
                    {
                        listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                    }
                    else
                    {
                        continue;
                    }
                }

                _repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 16/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Envio de Correo Automatico
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult EnvioCorreoOportunidadPlantillaAutomatico(int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repOportunidad.Exist(IdOportunidad))
                {
                    throw new Exception("Oportunidad no valida");
                }

                OportunidadBO oportunidad = _repOportunidad.FirstById(IdOportunidad);
                PersonalBO personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                if (_repCentroCosto.FirstById(oportunidad.IdCentroCosto.GetValueOrDefault()).Nombre.Contains("INSTITUTO"))
                {
                    IdPlantilla = ValorEstatico.IdPlantillaInformacionCarrera;
                }

                AlumnoBO alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                List<string> correosPersonalizados = new List<string>
                {
                    alumno.Email1
                    //"jvillena@bsginstitute.com"
                };

                reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = IdOportunidad,
                    IdPlantilla = IdPlantilla
                };

                reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                PlantillaEmailMandrillDTO emailFinalEnvio = reemplazoEtiquetaPlantilla.EmailReemplazado;

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailFinalEnvio.Asunto,
                    Message = emailFinalEnvio.CuerpoHTML,
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };

                TMK_MailServiceImpl mailService = new TMK_MailServiceImpl();

                mailService.SetData(mailDataPersonalizado);
                List<TMKMensajeIdDTO> listaIdsMailChimp = mailService.SendMessageTask();

                List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();

                foreach (var mensaje in listaIdsMailChimp)
                {
                    var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPersonal = oportunidad.IdPersonalAsignado,
                        IdAlumno = oportunidad.IdAlumno,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdMandrilTipoAsignacion = 7, //Envio masivo automatico nuevas oportunidades
                        EstadoEnvio = 1,
                        IdMandrilTipoEnvio = 1, // Correo enviado automaticamente
                        FechaEnvio = DateTime.Now,
                        Asunto = emailFinalEnvio.Asunto,
                        FkMandril = mensaje.MensajeId,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "EnvioAutomatico",
                        UsuarioModificacion = "EnvioAutomatico",
                        EsEnvioMasivo = false
                    };

                    if (!mandrilEnvioCorreoBO.HasErrors)
                    {
                        listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);
                    }
                    else
                    {
                        continue;
                    }
                }

                _repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}

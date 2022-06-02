using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: OportunidadWhatsApp
    /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión del guardado y la creación de oportunidades WhatsApp
    /// </summary>
    [Route("api/OportunidadWhatsApp")]
    public class OportunidadWhatsAppController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public OportunidadWhatsAppController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la plantilla de WhatsApp
        /// </summary>
        /// <returns>Objeto anonimo (IEnumerable con PlantillaWhatsAppDTO, IEnumerable con PlantillaWhatsAppDTO)</returns>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ObtenterPlantillaWhatsApp()
        {
            try
            {
                var _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
                var plantilla = _repPlantillaClaveValor.ObtenterPlantillaWhatsApp_PorIdModuloSistema(ValorEstatico.IdModuloCrearOportunidadesWhatsApp).OrderBy(w => w.Nombre);

                var plantillaWhatsApp = plantilla.Where(x => x.TipoPlantilla == ValorEstatico.IdPlantillaBaseWhatsAppFacebook);
                var plantillaPropio = plantilla.Where(x => x.TipoPlantilla == ValorEstatico.IdPlantillaBaseWhatsAppPropio);

                return Ok(new { PlantillaWhatsApp = plantillaWhatsApp, PlantillaPropio = plantillaPropio });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// TipoFuncion: POST
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes de WhatsApp respondidos segun el filtro enviado
        /// </summary>
        /// <param name="Filtro">Objeto de clase FiltroMensajesWhatsAppRespondidosDTO</param>
        /// <returns>Objeto de clase MensajesWhatsAppRespondidosDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerMensajesWhatsAppRespondidos([FromBody] FiltroMensajesWhatsAppRespondidosDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppConfiguracionEnvioDetalleRepositorio whatsAppConfiguracionEnvioDetalleRepositorio = new WhatsAppConfiguracionEnvioDetalleRepositorio(_integraDBContext);

                return Ok(whatsAppConfiguracionEnvioDetalleRepositorio.ObtenerMensajesWhatsAppRespondidos(Filtro));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Prepara suscripcion
        /// </summary>
        /// <param name="PrepararDesuscribir">Flag para indicar si se preparara la desuscripcion</param>
        /// <param name="Celular">Celular al cual se realizara la desuscripcion</param>
        /// <param name="NombreUsuario">Usuario que realiza la desuscripcion</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{PrepararDesuscribir}/{Celular}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult PrepararDesuscripcion(bool PrepararDesuscribir, string Celular, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsAppDesuscritoRepositorio = new WhatsAppDesuscritoRepositorio(_integraDBContext);
                WhatsAppDesuscritoBO whatsAppDesuscrito;

                whatsAppDesuscrito = whatsAppDesuscritoRepositorio.FirstBy(x => x.NumeroTelefono == Celular);

                if (PrepararDesuscribir == true)
                {
                    whatsAppDesuscrito = new WhatsAppDesuscritoBO();
                    whatsAppDesuscrito.NumeroTelefono = Celular;
                    whatsAppDesuscrito.Descripcion = "Desuscrito";
                    whatsAppDesuscrito.EsActivo = false;
                    whatsAppDesuscrito.EsMigracion = true;
                    whatsAppDesuscrito.Estado = true;
                    whatsAppDesuscrito.FechaCreacion = DateTime.Now;
                    whatsAppDesuscrito.FechaModificacion = DateTime.Now;
                    whatsAppDesuscrito.UsuarioCreacion = NombreUsuario;
                    whatsAppDesuscrito.UsuarioModificacion = NombreUsuario;

                    whatsAppDesuscritoRepositorio.Insert(whatsAppDesuscrito);
                }

                else
                {
                    whatsAppDesuscrito = whatsAppDesuscritoRepositorio.FirstBy(x => x.NumeroTelefono == Celular);
                    whatsAppDesuscritoRepositorio.Delete(whatsAppDesuscrito.Id, NombreUsuario);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Desuscribe el numero de WhatsApp
        /// </summary>
        /// <param name="NombreUsuario">Usuario que realiza la desuscripcion</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{NombreUsuario}")]
        [HttpGet]
        public ActionResult DesuscribirNumerosWhatsApp(string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsAppDesuscritoRepositorio = new WhatsAppDesuscritoRepositorio(_integraDBContext);

                var listaParaDesuscribir = whatsAppDesuscritoRepositorio.GetBy(x => x.EsActivo == false);

                foreach (var elementoParaDesuscribir in listaParaDesuscribir)
                {
                    elementoParaDesuscribir.EsActivo = true;
                    elementoParaDesuscribir.FechaModificacion = DateTime.Now;
                    elementoParaDesuscribir.UsuarioModificacion = NombreUsuario;

                    whatsAppDesuscritoRepositorio.Update(elementoParaDesuscribir);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Descarta el mensaje de WhatsApp
        /// </summary>
        /// <param name="IdWhatsAppConfiguracionEnvioDetalle">Id del detalle de configuracion de envio de WhatsApp (PK de la tabla mkt.T_WhatsAppConfiguracionEnvioDetalle)</param>
        /// <param name="NombreUsuario">Nombre de usuario que realiza la modificacion</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de la excepcion</returns>
        [Route("[Action]/{IdWhatsAppConfiguracionEnvioDetalle}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult DescartarMensaje(int IdWhatsAppConfiguracionEnvioDetalle, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var whatsAppConfiguracionEnvioDetalleRepositorio = new WhatsAppConfiguracionEnvioDetalleRepositorio(_integraDBContext);
                var whatsAppConfiguracionEnvioDetalle = whatsAppConfiguracionEnvioDetalleRepositorio.FirstById(IdWhatsAppConfiguracionEnvioDetalle);

                if (whatsAppConfiguracionEnvioDetalle != null && whatsAppConfiguracionEnvioDetalle.Id != 0)
                {
                    whatsAppConfiguracionEnvioDetalle.DescartarCrearOportunidad = true;
                    whatsAppConfiguracionEnvioDetalle.UsuarioModificacion = NombreUsuario;
                    whatsAppConfiguracionEnvioDetalle.FechaModificacion = DateTime.Now;

                    whatsAppConfiguracionEnvioDetalleRepositorio.Update(whatsAppConfiguracionEnvioDetalle);
                }
                else
                {
                    return BadRequest("Ocurrió un problema al descartar el mensaje.");
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener los combos para la creacion de oportunidad
        /// </summary>
        /// <returns>Objeto anonimo (Lista de objetos de clase PersonalAutocompleteDTO, lista de objetos de clase CentroCostoFiltroAutocompleteDTO)</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombosCrearOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repPersonal = new PersonalRepositorio(_integraDBContext);
                var _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                var _repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                var _repCargo = new CargoRepositorio(_integraDBContext);
                var _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                var _repIndustria = new IndustriaRepositorio(_integraDBContext);

                var listaPersonal = _repPersonal.CargarPersonalParaFiltro();
                var listaCentroCosto = _repCentroCosto.ObtenerTodoFiltroAntiguedadUnAño();
                var listaAreaFormacion = _repAreaFormacion.GetAll().Select(s => new { s.Id, s.Nombre });
                var listaCargo = _repCargo.GetBy(x => x.Id <= 24).Select(s => new { s.Id, s.Nombre });
                var listaAreaTrabajo = _repAreaTrabajo.GetAll().Select(s => new { s.Id, s.Nombre });
                var listaIndustria = _repIndustria.GetAll().Select(s => new { s.Id, s.Nombre });

                return Ok(new { listaPersonal, listaCentroCosto, listaAreaFormacion, listaCargo, listaAreaTrabajo, listaIndustria });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda la oportunidad en una tabla intermedia (mkt.T_WhatsAppConfiguracionEnvioDetalleOportunidad)
        /// </summary>
        /// <param name="GuardarOportunidadWhatsAppDTO">Objeto de clase GuardarOportunidadWhatsAppDTO</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GuardarOportunidad([FromBody] GuardarOportunidadWhatsAppDTO GuardarOportunidadWhatsAppDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repWhatsAppConfiguracionEnvioDetalleOportunidad = new WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio(_integraDBContext);

                var whatsAppConfiguracionEnvioDetalleOportunidad = new WhatsAppConfiguracionEnvioDetalleOportunidadBO();
                whatsAppConfiguracionEnvioDetalleOportunidad.IdWhatsAppConfiguracionEnvioDetalle = GuardarOportunidadWhatsAppDTO.IdWhatsAppConfiguracionEnvioDetalle;
                whatsAppConfiguracionEnvioDetalleOportunidad.IdCentroCosto = GuardarOportunidadWhatsAppDTO.IdCentroCosto;
                whatsAppConfiguracionEnvioDetalleOportunidad.IdPersonal = GuardarOportunidadWhatsAppDTO.IdPersonal;
                whatsAppConfiguracionEnvioDetalleOportunidad.Estado = true;
                whatsAppConfiguracionEnvioDetalleOportunidad.FechaCreacion = DateTime.Now;
                whatsAppConfiguracionEnvioDetalleOportunidad.FechaModificacion = DateTime.Now;
                whatsAppConfiguracionEnvioDetalleOportunidad.UsuarioCreacion = GuardarOportunidadWhatsAppDTO.NombreUsuario;
                whatsAppConfiguracionEnvioDetalleOportunidad.UsuarioModificacion = GuardarOportunidadWhatsAppDTO.NombreUsuario;

                _repWhatsAppConfiguracionEnvioDetalleOportunidad.Insert(whatsAppConfiguracionEnvioDetalleOportunidad);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Guarda la oportunidad en una tabla intermedia (mkt.T_WhatsAppConfiguracionEnvioDetalleOportunidad)
        /// </summary>
        /// <param name="IdWhatsAppConfiguracionEnvioDetalleOportunidad">Id del detalle de la configuracion de oportunidad (PK de tabla mkt.T_WhatsAppConfiguracionEnvioDetalleOportunidad)</param>
        /// <param name="NombreUsuario">Nombre del usuario que realiza el eliminado de la oportunidad guardada</param>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdWhatsAppConfiguracionEnvioDetalleOportunidad}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult EliminarOportunidadGuardada(int IdWhatsAppConfiguracionEnvioDetalleOportunidad, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repWhatsAppConfiguracionEnvioDetalleOportunidad = new WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio(_integraDBContext);

                _repWhatsAppConfiguracionEnvioDetalleOportunidad.Delete(IdWhatsAppConfiguracionEnvioDetalleOportunidad, NombreUsuario);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial del cliente por medio del Id de alumno
        /// </summary>
        /// <param name="IdAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Response 200 con el objeto anonimo (Lista de objetos de clase CorreoInteraccionesAlumnoDTO, lista de objetos de clase OportunidadHistorialDTO), caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerHistorialCliente(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var _repMandril = new MandrilRepositorio(_integraDBContext);
                var _repClasificacionPersona = new ClasificacionPersonaRepositorio(_integraDBContext);
                var _repPrioridadMailChimpListaCorreo = new PrioridadMailChimpListaCorreoRepositorio(_integraDBContext);

                var clasificacionPersona = _repClasificacionPersona.FirstBy(x => x.IdTablaOriginal == IdAlumno && x.IdTipoPersona == 1);
                var idClasificacionPersona = 0;
                if (clasificacionPersona != null && clasificacionPersona.Id != 0) idClasificacionPersona = clasificacionPersona.Id;

                var oportunidadInformacion = new OportunidadInformacionBO(IdAlumno, idClasificacionPersona);

                var mandril = _repMandril.ListaInteraccionCorreoAlumnoPorOportunidad(IdAlumno);
                var mailchimp = _repPrioridadMailChimpListaCorreo.ListaInteraccionCorreo(IdAlumno);

                mandril.AddRange(mailchimp);
                mandril = mandril.OrderByDescending(x => x.FechaCreacion).ToList();
                return Ok(new
                {
                    listaMandril = mandril,
                    listaHistorialOportunidades = oportunidadInformacion.ListaHistorial
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los correos de un alumno en especifico
        /// </summary>
        /// <param name="IdAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <returns>Response 200 con el objeto de clase OportunidadWhatsAppAlumnoDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerCorreosCliente(int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var alumnoRepositorio = new AlumnoRepositorio(_integraDBContext);

                OportunidadWhatsAppAlumnoDTO oportunidadWhatsAppAlumnoDTO;
                oportunidadWhatsAppAlumnoDTO = alumnoRepositorio.FirstBy(x => x.Id == IdAlumno, y => new OportunidadWhatsAppAlumnoDTO()
                {
                    Id = y.Id,
                    Nombre1 = y.Nombre1,
                    Nombre2 = y.Nombre2,
                    ApellidoPaterno = y.ApellidoPaterno,
                    ApellidoMaterno = y.ApellidoMaterno,
                    Email1 = y.Email1,
                    Email2 = y.Email2,
                    IdAFormacion = y.IdAformacion,
                    IdCargo = y.IdCargo,
                    IdATrabajo = y.IdAtrabajo,
                    IdIndustria = y.IdIndustria
                });

                return Ok(oportunidadWhatsAppAlumnoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Joao Benavente - Carlos Crispin - Gian Miranda
        /// Fecha: 09/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un alumno en especifico con los datos del objeto enviado
        /// </summary>
        /// <param name="OportunidadWhatsAppAlumnoDTO">Objeto de clase OportunidadWhatsAppAlumnoDTO</param>
        /// <returns>Response 200 con el objeto de clase OportunidadWhatsAppAlumnoDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAlumno([FromBody] OportunidadWhatsAppAlumnoActualizableDTO OportunidadWhatsAppAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var _repAlumnoLog = new AlumnoLogRepositorio(_integraDBContext);

                // Obtener datos del alumno
                var alumno = _repAlumno.FirstById(OportunidadWhatsAppAlumnoDTO.Id);

                var listaAlumnoLog = alumno.ActualizarDatosAlumno(OportunidadWhatsAppAlumnoDTO);

                if (!alumno.HasErrors)
                {
                    alumno.ValidarEstadoContactoWhatsAppTemporal(_integraDBContext);
                    _repAlumno.Update(alumno);
                    _repAlumnoLog.Insert(listaAlumnoLog);
                }
                else
                {
                    return BadRequest(alumno.GetErrors(null));
                }

                return Ok(new { Nombres = string.Concat(alumno.Nombre1.Trim(), " ", alumno.Nombre2.Trim()).Trim(), Apellidos = string.Concat(alumno.ApellidoPaterno, " ", alumno.ApellidoMaterno), Email1 = alumno.Email1, Email2 = alumno.Email2, IdAFormacion = alumno.IdAformacion, IdCargo = alumno.IdCargo, IdATrabajo = alumno.IdAtrabajo, IdIndustria = alumno.IdIndustria });
            }
            catch (Exception ex)
            {
                var _repAlumno = new AlumnoRepositorio(_integraDBContext);
                
                // Obtener datos del alumno
                var alumno = _repAlumno.FirstById(OportunidadWhatsAppAlumnoDTO.Id);

                return BadRequest(new { ex.Message, Nombres = string.Concat(alumno.Nombre1.Trim(), " ", alumno.Nombre2.Trim()).Trim(), Apellidos = string.Concat(alumno.ApellidoPaterno, " ", alumno.ApellidoMaterno), Email1 = alumno.Email1, Email2 = alumno.Email2, IdAFormacion = alumno.IdAformacion, IdCargo = alumno.IdCargo, IdATrabajo = alumno.IdAtrabajo, IdIndustria = alumno.IdIndustria });
            }
        }
    }
}

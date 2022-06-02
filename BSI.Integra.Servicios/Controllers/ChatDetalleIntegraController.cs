using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Comercial.SCode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ChatDetalleIntegraController
    /// Autor: Edgar S.
    /// Fecha: 10/03/2021
    /// <summary>
    /// Gestión Chat Detalle
    /// </summary>
    [Route("api/ChatDetalleIntegra")]// Hace referecia a tabla principal MessengerUsuario
    public class ChatDetalleIntegraController : BaseController<TChatDetalleIntegra, ValidadorChatDetalleIntegraDTO>
    {
        public ChatDetalleIntegraController(IIntegraRepository<TChatDetalleIntegra> repositorio, ILogger<BaseController<TChatDetalleIntegra, ValidadorChatDetalleIntegraDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccion(int idInteraccion)
        {
            try
            {
                ChatDetalleIntegraBO ChatDetalleIntegra = new ChatDetalleIntegraBO();
                return Ok(ChatDetalleIntegra.ObtenerDetalleChatPorIdInteraccion(idInteraccion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        
        [Route("[action]/{IdPersonal}/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerHistorialChatPortal(int IdPersonal , int IdAlumno)
        {
            try
            {
                ChatDetalleIntegraBO ChatDetalleIntegra = new ChatDetalleIntegraBO();
                return Ok(ChatDetalleIntegra.ObtenerHisotrialChatRecibidos(IdPersonal, IdAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [Route("[action]/{idsInteraccionChatIntegra}")]
        [HttpGet]
        public ActionResult ObtenerChatDetalleIntegraPorIdsInteraccionChatIntegra(string idsInteraccionChatIntegra)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            try
            {
                //ChatDetalleIntegraBO ChatDetalleIntegra = new ChatDetalleIntegraBO();
				ChatDetalleIntegraRepositorio chatDetalleIntegra = new ChatDetalleIntegraRepositorio();
                return Ok(chatDetalleIntegra.ObtenerChatDetalleIntegraPorIdsInteraccionChatIntegra(idsInteraccionChatIntegra));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 10/03/2021
        /// Versión: 1.1
        /// <summary>
        /// Inserta información de Chat Detalle y retorna Id de Chat Detalle
        /// </summary>
        /// <returns> int </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ChatDetalleIntegraBO DTO)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            try
            {
				ChatDetalleIntegraRepositorio _repChatDetalleIntegra = new ChatDetalleIntegraRepositorio();
				InteraccionChatIntegraRepositorio _repInteraccionChatIntegraRepositorio = new InteraccionChatIntegraRepositorio();
				var chatdetalleIntegraAnterior = _repChatDetalleIntegra.GetBy(x => x.IdInteraccionChatIntegra == DTO.IdInteraccionChatIntegra).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
				var interaccionChatIntegra = _repInteraccionChatIntegraRepositorio.FirstById(DTO.IdInteraccionChatIntegra.Value);
				DateTime actual = DateTime.Now;
				if(chatdetalleIntegraAnterior != null)
				{
					if(chatdetalleIntegraAnterior.IdRemitente.Equals("visitante") && DTO.IdRemitente.Equals("asesor"))
					{
						TimeSpan tiempo = actual.Subtract(chatdetalleIntegraAnterior.Fecha);
						int contadorUsuarioPromedioRespuesta = interaccionChatIntegra.ContadorUsuarioPromedioRespuesta == null ? 1 : interaccionChatIntegra.ContadorUsuarioPromedioRespuesta.Value;
						
						interaccionChatIntegra.TiempoRespuestaTotal = (interaccionChatIntegra.TiempoRespuestaTotal == null ? 0 : interaccionChatIntegra.TiempoRespuestaTotal.Value) + Convert.ToDecimal(tiempo.TotalSeconds);

						if (interaccionChatIntegra.UsuarioTiempoRespuestaMaximo == 0 || interaccionChatIntegra.UsuarioTiempoRespuestaMaximo < Convert.ToInt32(tiempo.TotalSeconds))
						{
							interaccionChatIntegra.UsuarioTiempoRespuestaMaximo = Convert.ToInt32(tiempo.TotalSeconds);
						}
						interaccionChatIntegra.UsuarioTiempoRespuestaPromedio = Convert.ToInt32(interaccionChatIntegra.TiempoRespuestaTotal / interaccionChatIntegra.ContadorUsuarioPromedioRespuesta);
						interaccionChatIntegra.ContadorUsuarioPromedioRespuesta = (interaccionChatIntegra.ContadorUsuarioPromedioRespuesta == null ? 0 : interaccionChatIntegra.ContadorUsuarioPromedioRespuesta.Value) + 1;
					}
					_repInteraccionChatIntegraRepositorio.Update(interaccionChatIntegra);
				}
                ChatDetalleIntegraBO chatDetalleIntegra = new ChatDetalleIntegraBO
                {
                    IdInteraccionChatIntegra = DTO.IdInteraccionChatIntegra,
                    NombreRemitente = DTO.NombreRemitente,
                    IdRemitente = DTO.IdRemitente,
                    Mensaje = DTO.Mensaje,
                    MensajeOfensivo = DTO.MensajeOfensivo,
                    Fecha = actual,
                    Estado = true,
                    UsuarioCreacion = DTO.UsuarioCreacion,
                    UsuarioModificacion = DTO.UsuarioModificacion,
                    FechaCreacion = actual,	
                    FechaModificacion = actual
				};
				_repChatDetalleIntegra.Insert(chatDetalleIntegra);
                return Ok(chatDetalleIntegra.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 16/07/2021
        /// Versión: 1.1
        /// <summary>
        /// Inserta información de Chat Detalle y retorna Id de Chat Detalle
        /// </summary>
        /// <param name="DTO">Información de ChatDetalleIntegraBO</param>
        /// <returns> int </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarArchivoSoporte([FromBody] ChatDetalleIntegraBO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChatDetalleIntegraRepositorio _repChatDetalleIntegra = new ChatDetalleIntegraRepositorio();
                InteraccionChatIntegraRepositorio _repInteraccionChatIntegraRepositorio = new InteraccionChatIntegraRepositorio();
                var chatdetalleIntegraAnterior = _repChatDetalleIntegra.GetBy(x => x.IdInteraccionChatIntegra == DTO.IdInteraccionChatIntegra).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
                var interaccionChatIntegra = _repInteraccionChatIntegraRepositorio.FirstById(DTO.IdInteraccionChatIntegra.Value);
                DateTime actual = DateTime.Now;
                if (chatdetalleIntegraAnterior != null)
                {
                    if (chatdetalleIntegraAnterior.IdRemitente.Equals("visitante") && DTO.IdRemitente.Equals("asesor"))
                    {
                        TimeSpan tiempo = actual.Subtract(chatdetalleIntegraAnterior.Fecha);
                        int contadorUsuarioPromedioRespuesta = interaccionChatIntegra.ContadorUsuarioPromedioRespuesta == null ? 1 : interaccionChatIntegra.ContadorUsuarioPromedioRespuesta.Value;

                        interaccionChatIntegra.TiempoRespuestaTotal = (interaccionChatIntegra.TiempoRespuestaTotal == null ? 0 : interaccionChatIntegra.TiempoRespuestaTotal.Value) + Convert.ToDecimal(tiempo.TotalSeconds);

                        if (interaccionChatIntegra.UsuarioTiempoRespuestaMaximo == 0 || interaccionChatIntegra.UsuarioTiempoRespuestaMaximo < Convert.ToInt32(tiempo.TotalSeconds))
                        {
                            interaccionChatIntegra.UsuarioTiempoRespuestaMaximo = Convert.ToInt32(tiempo.TotalSeconds);
                        }
                        interaccionChatIntegra.UsuarioTiempoRespuestaPromedio = Convert.ToInt32(interaccionChatIntegra.TiempoRespuestaTotal / interaccionChatIntegra.ContadorUsuarioPromedioRespuesta);
                        interaccionChatIntegra.ContadorUsuarioPromedioRespuesta = (interaccionChatIntegra.ContadorUsuarioPromedioRespuesta == null ? 0 : interaccionChatIntegra.ContadorUsuarioPromedioRespuesta.Value) + 1;
                    }
                    _repInteraccionChatIntegraRepositorio.Update(interaccionChatIntegra);
                }

                ChatDetalleIntegraBO chatDetalleIntegra = new ChatDetalleIntegraBO
                {
                    IdInteraccionChatIntegra = DTO.IdInteraccionChatIntegra,
                    NombreRemitente = DTO.NombreRemitente,
                    IdRemitente = DTO.IdRemitente,
                    Mensaje = DTO.Mensaje,
                    MensajeOfensivo = DTO.MensajeOfensivo,
                    Fecha = actual,
                    Estado = true,
                    UsuarioCreacion = DTO.UsuarioCreacion,
                    UsuarioModificacion = DTO.UsuarioModificacion,
                    FechaCreacion = actual,
                    FechaModificacion = actual,
                    IdChatDetalleIntegraArchivo = DTO.IdChatDetalleIntegraArchivo
                };
                _repChatDetalleIntegra.Insert(chatDetalleIntegra);
                return Ok(chatDetalleIntegra.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar S.
        /// Fecha: 17/04/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene información de chat por IdInteracción Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccionControlMensaje(int idInteraccion)
        {
            try
            {
                ChatDetalleIntegraBO ChatDetalleIntegra = new ChatDetalleIntegraBO();
                return Ok(ChatDetalleIntegra.ObtenerDetalleChatPorIdInteraccionControlMensajes(idInteraccion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 25/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información de chat por IdAlumno para personal Soporte - Validando Mensaje Ofensivo
        /// </summary>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{idAlumno}/{idInteraccion}")]
        [HttpGet]
        public ActionResult ObtenerDetalleChatPorIdInteraccionControlMensajeSoporte(int idAlumno,int idInteraccion)
        {
            try
            {
                ChatDetalleIntegraBO ChatDetalleIntegra = new ChatDetalleIntegraBO();

                //ACTUALIZAMOS LEIDO A 1
                InteraccionChatIntegraRepositorio _repInteraccionChatIntegraRepositorio = new InteraccionChatIntegraRepositorio();
                var objInteraccion = _repInteraccionChatIntegraRepositorio.GetBy(w => w.Id == idInteraccion).FirstOrDefault();
                if(objInteraccion!=null)
                {
                    objInteraccion.Leido = true;
                    objInteraccion.NroMensajesSinLeer = 0;
                    objInteraccion.FechaModificacion = DateTime.Now;
                    _repInteraccionChatIntegraRepositorio.Update(objInteraccion);
                }

                return Ok(ChatDetalleIntegra.ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(idAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar Serruto
        /// Fecha: 15/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat
        /// </summary>
        /// <param name="IdPlantilla">Id de Plantilla</param>
        /// <returns> Lista de Objeto de tipo (Int, String)</returns>
        [Route("[action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult ObtenerPlantillaChatIntegraSoporte(int IdPlantilla)
        {
            try
            {
                PlantillaRepositorio _repPlantilla= new PlantillaRepositorio();
                var lista = _repPlantilla.GetBy(x => x.IdPlantillaBase == IdPlantilla).Select(x => new { x.Id, x.Nombre }).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 16/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar información de archivo para chat de portal
        /// </summary>
        /// <param name="ArchivoUsuario">DTO con información de Formato y Usuario de Interfaz</param>
        /// <returns>Objeto Agrupado</returns>
        [Route("[action]")]
        [HttpPost]
        [RequestSizeLimit(200000000)]
        public ActionResult AdjuntarArchivoChatSoporte([FromForm] ChatDetalleIntegraArchivoDTO ArchivoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChatDetalleIntegraArchivoRepositorio _repChatDetalleIntegraArchivo = new ChatDetalleIntegraArchivoRepositorio();
                string NombreArchivo = "";
                string NombreArchivotemp = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";

                if (ArchivoUsuario.File != null)
                {
                    UtilBO utilBO = new UtilBO();
                    ContentType = ArchivoUsuario.File.ContentType;
                    NombreArchivo = ArchivoUsuario.File.FileName;
                    NombreArchivotemp = ArchivoUsuario.File.FileName;
                    NombreArchivotemp = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + utilBO.SlugNombreArchivo(NombreArchivotemp);
                    urlArchivoRepositorio = _repChatDetalleIntegraArchivo.SubirDocumentosChatSoporte(ArchivoUsuario.File.ConvertToByte(), ArchivoUsuario.File.ContentType, NombreArchivotemp);
                }
                else
                {
                    return BadRequest("No se subió ningún archivo.");
                }

                if (string.IsNullOrEmpty(urlArchivoRepositorio))
                {
                    return BadRequest("Ocurrió un problema al subir el archivo.");
                }
                bool esImagen = false;
                if (ArchivoUsuario.File.ContentType.Contains("image"))
                {
                    esImagen = true;
                }
                var agregarArchivo = new ChatDetalleIntegraArchivoBO
                {
                    NombreArchivo = NombreArchivo,
                    RutaArchivo = urlArchivoRepositorio,
                    MimeType = ArchivoUsuario.File.ContentType,
                    EsImagen = esImagen,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = ArchivoUsuario.Usuario,
                    UsuarioModificacion = ArchivoUsuario.Usuario
                };
                var resultado = _repChatDetalleIntegraArchivo.Insert(agregarArchivo);
                if (resultado)
                {
                    return Ok(new { Respuesta = true, Url = urlArchivoRepositorio, IdArchivo = agregarArchivo.Id, Tipo = ArchivoUsuario.File.ContentType });
                }
                else
                {
                    return Ok(new { Respuesta = false });
                }
                //return Ok(new { Respuesta = true, Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/DocumentosChatAulaVirtual/20210716-194928-Canvas.jpg", IdArchivo = 1, Tipo = "image/jpeg" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorChatDetalleIntegraDTO : AbstractValidator<TChatDetalleIntegra>
    {
        public static ValidadorChatDetalleIntegraDTO Current = new ValidadorChatDetalleIntegraDTO();
        public ValidadorChatDetalleIntegraDTO()
        {
            RuleFor(objeto => objeto.IdInteraccionChatIntegra).NotNull().WithMessage("Nombre remitente es Obligatorio, no puede ser nulo");
            RuleFor(objeto => objeto.NombreRemitente).NotEmpty().WithMessage("Nombre remitente es Obligatorio");
            RuleFor(objeto => objeto.IdRemitente).NotNull().WithMessage("Id remitente es Obligatorio, no puede ser nulo");
            RuleFor(objeto => objeto.Mensaje).NotNull().WithMessage("Mensaje es Obligatorio, no puede ser nulo");

        }
    }
}
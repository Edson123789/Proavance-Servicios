using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Marketing/ConfiguracionChat
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Configura las acciones para la interfaz de configuracion chat
    /// </summary>
    [Route("api/ConfiguracionChat")]
    public class ConfiguracionChatController : BaseController<TConfiguracionChat, ValidadorConfiguracionChatDTO>
    {
        public ConfiguracionChatController(IIntegraRepository<TConfiguracionChat> repositorio, ILogger<BaseController<TConfiguracionChat, ValidadorConfiguracionChatDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las configuraciones de los chats
        /// </summary>
        /// <returns>Response 200 con el objeto de clase ConfiguracionChatBO, caso contrario response 400</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                ConfiguracionChatRepositorio _repConfiguracionChat = new ConfiguracionChatRepositorio();
                return Ok(_repConfiguracionChat.ObtenerTodo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta una nuevo configuracion de chat en base a un objeto de clase configuracionChatDTO
        /// </summary>
        /// <returns>Response 200 con el objeto de clase ConfiguracionChatBO, caso contrario response 400</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConfiguracionChatDTO ConfiguracionChat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionChatRepositorio _repConfiguracionChat = new ConfiguracionChatRepositorio();
                ConfiguracionChatBO configuracionChatBO = new ConfiguracionChatBO()
                {
                    NombreConfiguracion = ConfiguracionChat.NombreConfiguracion,
                    VisualizarTiempo = ConfiguracionChat.VisualizarTiempo,
                    TextoHeader = ConfiguracionChat.TextoHeader,
                    TextoHeaderNotificacion = ConfiguracionChat.TextoHeaderNotificacion,
                    ColorFondoHeader = ConfiguracionChat.ColorFondoHeader,
                    ColorTextoHeader = ConfiguracionChat.ColorTextoHeader,
                    TextoHeaderFuente = ConfiguracionChat.TextoHeaderFuente,
                    IconoAsesor = ConfiguracionChat.IconoAsesor,
                    ColorFondoAsesor = ConfiguracionChat.ColorFondoAsesor,
                    ColorTextoAsesor = ConfiguracionChat.ColorTextoAsesor,
                    IconoInteresado = ConfiguracionChat.IconoInteresado,
                    ColorFondoInteresado = ConfiguracionChat.ColorFondoInteresado,
                    ColorTextoInteresado = ConfiguracionChat.ColorTextoInteresado,
                    TextoChatFuente = ConfiguracionChat.TextoChatFuente,
                    TextoOffline = ConfiguracionChat.TextoOffline,
                    TextoSatisfaccionOffline = ConfiguracionChat.TextoSatisfaccionOffline,
                    TextoInicial = ConfiguracionChat.TextoInicial,
                    ColorTextoEmpezarChat = ConfiguracionChat.ColorTextoEmpezarChat,
                    ColorFondoEmpezarChat = ConfiguracionChat.ColorFondoEmpezarChat,
                    TextoFormularioFuente = ConfiguracionChat.TextoFormularioFuente,
                    IconoChat = ConfiguracionChat.IconoChat,
                    MuestraTextoInicial = ConfiguracionChat.MuestraTextoInicial,
                    Estado = true,
                    UsuarioCreacion = ConfiguracionChat.NombreUsuario,
                    UsuarioModificacion = ConfiguracionChat.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!configuracionChatBO.HasErrors)
                {
                    _repConfiguracionChat.Insert(configuracionChatBO);
                }
                else {
                    return BadRequest(configuracionChatBO.GetErrors(null));
                }
                return Ok(configuracionChatBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza segun los datos enviados en el objeto de clase ConfiguracionChatDTO
        /// </summary>
        /// <returns>Response 200 con el objeto de clase ConfiguracionChatBO, caso contrario response 400</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ConfiguracionChatDTO ConfiguracionChat)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionChatRepositorio _repConfiguracionChat = new ConfiguracionChatRepositorio();
                if (_repConfiguracionChat.Exist(ConfiguracionChat.Id))
                {
                    var configuracionChatBO = _repConfiguracionChat.FirstById(ConfiguracionChat.Id);
                    configuracionChatBO.NombreConfiguracion = ConfiguracionChat.NombreConfiguracion;
                    configuracionChatBO.VisualizarTiempo = ConfiguracionChat.VisualizarTiempo;
                    configuracionChatBO.TextoHeader = ConfiguracionChat.TextoHeader;
                    configuracionChatBO.TextoHeaderNotificacion = ConfiguracionChat.TextoHeaderNotificacion;
                    configuracionChatBO.ColorFondoHeader = ConfiguracionChat.ColorFondoHeader;
                    configuracionChatBO.ColorTextoHeader = ConfiguracionChat.ColorTextoHeader;
                    configuracionChatBO.TextoHeaderFuente = ConfiguracionChat.TextoHeaderFuente;
                    configuracionChatBO.IconoAsesor = ConfiguracionChat.IconoAsesor;
                    configuracionChatBO.ColorFondoAsesor = ConfiguracionChat.ColorFondoAsesor;
                    configuracionChatBO.ColorTextoAsesor = ConfiguracionChat.ColorTextoAsesor;
                    configuracionChatBO.IconoInteresado = ConfiguracionChat.IconoInteresado;
                    configuracionChatBO.ColorFondoInteresado = ConfiguracionChat.ColorFondoInteresado;
                    configuracionChatBO.ColorTextoInteresado = ConfiguracionChat.ColorTextoInteresado;
                    configuracionChatBO.TextoChatFuente = ConfiguracionChat.TextoChatFuente;
                    configuracionChatBO.TextoOffline = ConfiguracionChat.TextoOffline;
                    configuracionChatBO.TextoSatisfaccionOffline = ConfiguracionChat.TextoSatisfaccionOffline;
                    configuracionChatBO.TextoInicial = ConfiguracionChat.TextoInicial;
                    configuracionChatBO.ColorTextoEmpezarChat = ConfiguracionChat.ColorTextoEmpezarChat;
                    configuracionChatBO.ColorFondoEmpezarChat = ConfiguracionChat.ColorFondoEmpezarChat;
                    configuracionChatBO.TextoFormularioFuente = ConfiguracionChat.TextoFormularioFuente;
                    configuracionChatBO.IconoChat = ConfiguracionChat.IconoChat;
                    configuracionChatBO.MuestraTextoInicial = ConfiguracionChat.MuestraTextoInicial;
                    configuracionChatBO.Estado = true;
                    configuracionChatBO.UsuarioCreacion = ConfiguracionChat.NombreUsuario;
                    configuracionChatBO.UsuarioModificacion = ConfiguracionChat.NombreUsuario;
                    configuracionChatBO.FechaCreacion = DateTime.Now;
                    configuracionChatBO.FechaModificacion = DateTime.Now;

                    if (!configuracionChatBO.HasErrors)
                    {
                        _repConfiguracionChat.Update(configuracionChatBO);
                    }
                    else
                    {
                        return BadRequest(configuracionChatBO.GetErrors(null));
                    }
                    return Ok(configuracionChatBO);
                }
                else {
                    return BadRequest("Configuracion chat no existente!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 15/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza segun los datos enviados en el objeto de clase EliminarDTO
        /// </summary>
        /// <returns>Response 200 con la variable booleana con valor true, caso contrario response 400</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ConfiguracionChatRepositorio _repConfiguracionChat = new ConfiguracionChatRepositorio();
                if (_repConfiguracionChat.Exist(DTO.Id))
                {
                    _repConfiguracionChat.Delete(DTO.Id, DTO.NombreUsuario);
                    return Ok(true);
                }
                else {
                    return BadRequest("No existe la configuracion chat!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorConfiguracionChatDTO : AbstractValidator<TConfiguracionChat>
    {
        public static ValidadorConfiguracionChatDTO Current = new ValidadorConfiguracionChatDTO();
        public ValidadorConfiguracionChatDTO()
        {
            RuleFor(objeto => objeto.NombreConfiguracion).NotEmpty().WithMessage("Nombre de configuracion es Obligatorio");
        }
    }
}

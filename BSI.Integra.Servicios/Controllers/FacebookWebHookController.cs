using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using System.Web;
using System.Text;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FacebookWebHook")]
    public class FacebookWebHookController : Controller
    {
        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult InsertaWebHookRequestLog([FromBody]WebhookRequestLogDTO Dto, string Usuario)
        {
            WebhookRequestLogRepositorio _repWebhookRequestLog = new WebhookRequestLogRepositorio();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var obj = new WebHookRequestLogBO();
                obj.Verb = Dto.Verb;
                obj.Content =Dto.Content;
                obj.Fecha = Dto.Fecha.Value;
                obj.Estado = true;
                obj.FechaCreacion = DateTime.Now;
                obj.FechaModificacion = DateTime.Now;
                obj.UsuarioCreacion = Usuario;
                obj.UsuarioModificacion = Usuario;

                bool rpta = _repWebhookRequestLog.Insert(obj);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult InsertarPageFbValue([FromBody]PageFbValueDTO Dto, string Usuario)
        {
            PageFbValueRepositorio _repPageFbValue = new PageFbValueRepositorio();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var obj = new PageFbValueBO();
                obj.Verb = Dto.Verb;
                obj.Content =Dto.Content;
                obj.Fecha = Dto.Fecha.Value;
                obj.Estado = true;
                obj.FechaCreacion = DateTime.Now;
                obj.FechaModificacion = DateTime.Now;
                obj.UsuarioCreacion = Usuario;
                obj.UsuarioModificacion = Usuario;

                bool rpta = _repPageFbValue.Insert(obj);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [Route("[Action]/{Usuario}")]
        [HttpPost]
        public ActionResult InsertarMessengerValues([FromBody]MessengerValueDTO Dto, string Usuario)
        {
            MessengerValueRepositorio _repMessengerValue = new MessengerValueRepositorio();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var obj2 = new MessengerValueBO();
                obj2.Verb =Dto.Verb;
                obj2.Content =Dto.Content;
                obj2.Fecha = Dto.Fecha.Value;
                obj2.Estado = true;
                obj2.FechaCreacion = DateTime.Now;
                obj2.FechaModificacion = DateTime.Now;
                obj2.UsuarioCreacion = Usuario;
                obj2.UsuarioModificacion = Usuario;

                bool rpta=_repMessengerValue.Insert(obj2);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [Route("[Action]/{PSID}/{RecipientId}")]
        [HttpPost]
        public ActionResult ObtenerMessengerUsuario(string PSID, string RecipientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();

                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);

                FacebookPaginaBO facebookPaginaBO = facebookPaginaRepositorio.FirstBy(x => x.FacebookId == RecipientId);

                var usuarioExistente = _repMessengerUsuario.FirstBy(x => x.Psid == PSID && x.IdFacebookPagina == facebookPaginaBO.Id, y => new MessengerUsuarioDTO()
                {
                    Id = y.Id,
                    PSID = y.Psid,
                    Nombres = y.Nombres,
                    Apellidos = y.Apellidos,
                    UrlFoto = y.UrlFoto,
                    IdPersonal = y.IdPersonal,
                    SeRespondio = y.SeRespondio,
                    IdAreaCapacitacion = y.IdAreaCapacitacion,
                    Estado = y.Estado
                });
                //var usuarioExistente = _repMessengerUsuario.ObtenerMessengerUsuarioPorIdUsuario(PSID);
                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[Action]/{Id}")]
        [HttpPost]
        public ActionResult ObtenerMessengerUsuarioPorId(int Id)
        {
            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioExistente = _repMessengerUsuario.ObtenerMessengerUsuarioPorId(Id);
                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[Action]/{PSID}")]
        [HttpPost]
        public ActionResult ObtenerIdComentarioFacebook(string PSID)
        {
            PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioExistente = _repPostComentarioUsuario.ObtenerIdComentarioFacebook(PSID);
                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
        [Route("[Action]/{PSID}")]
        [HttpPost]
        public ActionResult ObtenerPostComentarioFacebook(string PSID)
        {
            PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioExistente = _repPostComentarioUsuario.ObtenerComentarioFacebookPorIdUsuario(PSID);
                return Ok(usuarioExistente);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        [Route("[Action]/{PSID}/{AreaPostback}/{Usuario}/{IdAsesorOnline}/{RecipientId}")]
        [HttpPost]
        public ActionResult ObtenerMensajeFacebook(string PSID ,int AreaPostback,string Usuario, int IdAsesorOnline, string RecipientId)
        {
            integraDBContext contexto = new integraDBContext();
            APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
            AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(contexto);
            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
            PersonalRepositorio _repPersonal = new PersonalRepositorio(contexto);
            AreaCapacitacionFacebookRepositorio _repAreaCapacitacionFacebook = new AreaCapacitacionFacebookRepositorio(contexto);
            MessengerUsuarioLogRepositorio _repMessengerUsuarioLog = new MessengerUsuarioLogRepositorio(contexto);
            MessengerAsesorRepositorio _repMessengerAsesor = new MessengerAsesorRepositorio(contexto);
            MessengerAsesorDetalleRepositorio messengerAsesorDetalleRepositorio = new MessengerAsesorDetalleRepositorio(contexto);
            FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
            MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);

            FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(RecipientId, 1);

            string Mensaje = "SIN ASESOR";
            var existeArea = _repAreaCapacitacion.Exist(a => a.IdAreaCapacitacionFacebook == AreaPostback);
            var horaActual = DateTime.Now;

            if (IdAsesorOnline != 0)
            {
                try
                {
                    //MessengerUsuarioBO usuario = new MessengerUsuarioBO(PSID, contexto);
                    MessengerUsuarioBO usuario = _repMessengerUsuario.FirstBy(a => a.Psid == PSID);
                    if (usuario != null)
                    {
                        usuario.IdAreaCapacitacionFacebook = AreaPostback;
                        usuario.IdPersonal = IdAsesorOnline;
                        usuario.FechaModificacion = DateTime.Now;
                        usuario.UsuarioModificacion = Usuario;

                        _repMessengerUsuario.Update(usuario);

                        MessengerUsuarioLogBO usuarioLog = new MessengerUsuarioLogBO();
                        usuarioLog.IdPersonal = IdAsesorOnline;
                        usuarioLog.IdMessengerUsuario = usuario.Id;
                        usuarioLog.Estado = true;
                        usuarioLog.UsuarioCreacion = Usuario;
                        usuarioLog.UsuarioModificacion = Usuario;
                        usuarioLog.IdAreaCapacitacionFacebook = AreaPostback;
                        usuarioLog.FechaCreacion = DateTime.Now;
                        usuarioLog.FechaModificacion = DateTime.Now;
                        _repMessengerUsuarioLog.Insert(usuarioLog);

                        var NombreAsesor = "";
                        var asesorasignado = _repPersonal.Exist(a => a.Id == IdAsesorOnline);
                        if (asesorasignado)
                        {
                            NombreAsesor = _repPersonal.FirstById(IdAsesorOnline).Nombres;
                        }
                        var areaFacebookDescripcion = _repAreaCapacitacionFacebook.ObtenerDescripcion(AreaPostback).Descripcion;

                        if (horaActual.Hour < 13) NombreAsesor = "Daniela";
                        else NombreAsesor = "Sharon";

                        //Mensaje = "<strong>(Asesor asignado: " + asesorMSG + " </strong>).  Area que seleccionó el usuario: <strong>" + areaFacebookDescripcion + "</strong>";
                        Mensaje = ", mi nombre es " + NombreAsesor + ", estoy aquí para brindarte información acerca de " + areaFacebookDescripcion + ".";
                    }
                }
                catch (Exception ex)
                {
                }
                return Ok(Mensaje);
            }

            if (existeArea)
            {
                try
                {

                    var asesorArea = _repMessengerAsesor.ObtenerAsesorPorAreaFacebook(AreaPostback);

                    //MessengerUsuarioBO usuario = new MessengerUsuarioBO(PSID, contexto);
                    MessengerUsuarioBO usuario = _repMessengerUsuario.FirstBy(a => a.Psid == PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);

                    if (usuario != null && asesorArea!= null)
                    {            
                        usuario.IdAreaCapacitacionFacebook = AreaPostback;

                        MessengerUsuarioLogBO usuarioLog = new MessengerUsuarioLogBO();
                        usuarioLog.IdPersonal = asesorArea.IdPersonal;
                        usuarioLog.IdMessengerUsuario = usuario.Id;
                        usuarioLog.Estado = true;
                        usuarioLog.UsuarioCreacion = Usuario;
                        usuarioLog.UsuarioModificacion = Usuario;
                        usuarioLog.IdAreaCapacitacionFacebook = AreaPostback;
                        usuarioLog.FechaCreacion = DateTime.Now;
                        usuarioLog.FechaModificacion = DateTime.Now;
                        _repMessengerUsuarioLog.Insert(usuarioLog);

                        MessengerAsesorBO messengerAsesorBO = _repMessengerAsesor.FirstBy(x => x.IdPersonal == asesorArea.IdPersonal);
                        messengerAsesorBO.ConteoClientesAsignados = (messengerAsesorBO.ConteoClientesAsignados ?? 0) + 1;
                        _repMessengerAsesor.Update(messengerAsesorBO);

                        var NombreAsesor = "";
                        var asesorasignado = _repPersonal.Exist(a => a.Id == asesorArea.IdPersonal);
                        if (asesorasignado)
                        {
                            NombreAsesor = _repPersonal.FirstById(asesorArea.IdPersonal).Nombres;
                        }
                        var areaFacebookDescripcion = _repAreaCapacitacionFacebook.ObtenerDescripcion(AreaPostback).Descripcion;
                        //Mensaje = "<strong>(Asesor asignado: " + asesorMSG + " </strong>).  Area que seleccionó el usuario: <strong>" + areaFacebookDescripcion + "</strong>";

                        if (horaActual.Hour < 13) NombreAsesor = "Daniela";
                        else NombreAsesor = "Sharon";

                        Mensaje = ", mi nombre es " + NombreAsesor + ", estoy aquí para brindarte información acerca de " + areaFacebookDescripcion + ".";

                        Mensaje = "Hola " + usuario.Nombres + Mensaje;

                        ApiGraphFacebookResponseMensajeInboxDTO respuestaAPI = aPIGraphFacebook.ResponderInbox(usuario.Psid, Mensaje, facebookAplicacionPaginaTokenDTO.Token);
                        MessengerChatBO messengerChatBO = new MessengerChatBO();
                        messengerChatBO.IdMeseengerUsuario = usuario.Id;
                        messengerChatBO.IdPersonal = 4419;
                        messengerChatBO.Mensaje = Mensaje;
                        messengerChatBO.Tipo = false;
                        messengerChatBO.FacebookId = respuestaAPI.message_id;
                        messengerChatBO.FechaInteraccion = DateTime.Now;
                        messengerChatBO.IdTipoMensajeMessenger = 1;
                        messengerChatBO.UrlArchivoAdjunto = null;
                        messengerChatBO.Estado = true;
                        messengerChatBO.FechaCreacion = DateTime.Now;
                        messengerChatBO.FechaModificacion = DateTime.Now;
                        messengerChatBO.UsuarioCreacion = Usuario;
                        messengerChatBO.UsuarioModificacion = Usuario;

                        messengerChatRepositorio.Insert(messengerChatBO);

                        usuario.IdPersonal = asesorArea.IdPersonal;
                        usuario.SeRespondio = true;
                        usuario.FechaModificacion = DateTime.Now;
                        usuario.UsuarioModificacion = Usuario;

                        _repMessengerUsuario.Update(usuario);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }


            return Ok(Mensaje);
        }

        [Route("[Action]/{IdPost}")]
        [HttpGet]
        public ActionResult ObtenerAreaPost(string IdPost)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FacebookPostRepositorio _repFacebookPostRepositorio = new FacebookPostRepositorio();
				var area = _repFacebookPostRepositorio.ObtenerArea(IdPost);
				return Ok(area);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
            
        }
        [Route("[Action]/{IdPersonal}")]
        [HttpPost]
        public ActionResult ObtenerAreayCorreoAsesor(int IdPersonal)
        {
            integraDBContext contexto = new integraDBContext();
            PersonalRepositorio _repPersonal = new PersonalRepositorio(contexto);
            MessengerAsesorRepositorio _repMessengerAsesor = new MessengerAsesorRepositorio(contexto);
            MessengerAsesorDetalleRepositorio messengerAsesorDetalleRepositorio = new MessengerAsesorDetalleRepositorio(contexto);

            var correo = _repPersonal.ObtenerEmailPersonal(IdPersonal);
            var areas = messengerAsesorDetalleRepositorio.GetBy(x => x.IdPersonal == IdPersonal, y => new ObtenerAreasPorAsesorDTO
            {
                IdAreaFacebook = y.IdAreaCapacitacionFacebook
            }).ToList();
            return Ok(new { Correo=correo.Email, AreaAsesor= areas });
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerChatyComentarioSinAsesor()
        {
            integraDBContext contexto = new integraDBContext();
            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
            PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio(contexto);
            var chatsinAsesor = _repMessengerUsuario.ObtenerMessengerUsuario();
            var comentarioSinAsesor = _repPostComentarioUsuario.ObtenerComentarioFacebook();
            return Ok(new { rpta =new { chatsinAsesor, comentarioSinAsesor } });

        }
                
        [Route("[Action]/{pSID}/{Mensaje}/{Usuario}")]
        [HttpPost]
        public ActionResult ActualizarTelefono(string pSID, string Mensaje,string Usuario)
        {
            integraDBContext contexto = new integraDBContext();
            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
            MessengerUsuarioLogRepositorio _repMessengerUsuarioLog = new MessengerUsuarioLogRepositorio(contexto);
            try
            {
                MessengerUsuarioBO usuario = new MessengerUsuarioBO(pSID);
                if (usuario != null)
                {
                    usuario.Telefono = Mensaje;
                    usuario.MensajeEnviarTelefono = true;
                    usuario.UsuarioModificacion = Usuario;
                    usuario.FechaModificacion = DateTime.Now;

                    _repMessengerUsuario.Update(usuario);

                    MessengerUsuarioLogBO ulog = new MessengerUsuarioLogBO();
                    ulog.IdMessengerUsuario = usuario.Id;
                    ulog.IdAreaCapacitacionFacebook = usuario.IdAreaCapacitacionFacebook;
                    //ulog.Email = usuario.Email;
                    //ulog.Telefono = mensaje;
                    ulog.UsuarioCreacion = Usuario;
                    ulog.UsuarioModificacion = Usuario;
                    ulog.FechaCreacion = DateTime.Now;
                    ulog.FechaModificacion = DateTime.Now;
                    _repMessengerUsuarioLog.Update(ulog);

                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        [Route("[Action]/{pSID}/{Mensaje}/{Usuario}")]
        [HttpPost]
        public ActionResult ActualizarEmail(string pSID, string Mensaje,string Usuario)
        {
            integraDBContext contexto = new integraDBContext();
            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
            MessengerUsuarioLogRepositorio _repMessengerUsuarioLog = new MessengerUsuarioLogRepositorio(contexto);
            try
            {
                MessengerUsuarioBO usuario = new MessengerUsuarioBO(pSID);
                if (usuario != null)
                {
                    usuario.Email = Mensaje;
                    usuario.UsuarioModificacion = Usuario;
                    usuario.FechaModificacion = DateTime.Now;

                    _repMessengerUsuario.Update(usuario);

                    MessengerUsuarioLogBO ulog = new MessengerUsuarioLogBO();
                    ulog.IdMessengerUsuario = usuario.Id;
                    ulog.IdAreaCapacitacionFacebook = usuario.IdAreaCapacitacionFacebook;
                    //ulog.Email = Mensaje;
                    //ulog.Telefono = usuario.Telefono;
                    ulog.UsuarioCreacion = Usuario;
                    ulog.UsuarioModificacion = Usuario;
                    ulog.FechaCreacion = DateTime.Now;
                    ulog.FechaModificacion = DateTime.Now;
                    _repMessengerUsuarioLog.Update(ulog);

                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerIdFacebookPersonal(int IdPersonal)
        {
            PersonalRepositorio _repPersonal = new PersonalRepositorio();
            try
            {
                var rpta =_repPersonal.ObtenerIdFacebookPersonal(IdPersonal);
                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        [Route("[Action]/{PSID}/{RecipientId}/{Usuario}")]
        [HttpPost]
        public ActionResult CrearUsuarioFacebook(string PSID, string RecipientId, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            integraDBContext contexto = new integraDBContext();
            APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();

            MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
            MessengerUsuarioLogRepositorio _repMessengerUsuarioLog = new MessengerUsuarioLogRepositorio(contexto);
            FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);

            FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(RecipientId, 1);
            ApiGraphFacebookResponseUsuarioNombreDTO apiGraphFacebookResponseUsuarioNombreDTO;

            try
            {
                var existeUsuario = _repMessengerUsuario.Exist(a => a.Psid == PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);
                if (!existeUsuario)
                {
                    apiGraphFacebookResponseUsuarioNombreDTO = aPIGraphFacebook.ObtenerInformacionUsuarioConToken(PSID, facebookAplicacionPaginaTokenDTO.Token);

                    MessengerUsuarioBO usuario = new MessengerUsuarioBO();
                    usuario.Psid = PSID;
                    usuario.Nombres = apiGraphFacebookResponseUsuarioNombreDTO.first_name;
                    usuario.Apellidos = apiGraphFacebookResponseUsuarioNombreDTO.last_name;
                    usuario.UrlFoto = apiGraphFacebookResponseUsuarioNombreDTO.profile_pic;
                    usuario.Estado = true;
                    usuario.FechaCreacion = DateTime.Now;
                    usuario.FechaModificacion = DateTime.Now;
                    usuario.UsuarioCreacion = Usuario;
                    usuario.UsuarioModificacion = Usuario;
                    usuario.IdFacebookPagina = facebookAplicacionPaginaTokenDTO.IdFacebookPagina;

                    _repMessengerUsuario.Insert(usuario);

                    MessengerUsuarioLogBO uslog = new MessengerUsuarioLogBO();
                    uslog.IdMessengerUsuario = usuario.Id;
                    uslog.FechaCreacion = DateTime.Now;
                    uslog.FechaModificacion = DateTime.Now;
                    uslog.UsuarioCreacion = Usuario;
                    uslog.UsuarioModificacion = Usuario;

                    _repMessengerUsuarioLog.Insert(uslog);
                }
                else
                {
                    apiGraphFacebookResponseUsuarioNombreDTO = _repMessengerUsuario.FirstBy(a => a.Psid == PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina, x => new ApiGraphFacebookResponseUsuarioNombreDTO()
                    {
                        first_name = x.Nombres,
                        last_name = x.Apellidos,
                        profile_pic = x.UrlFoto
                    });
                }
                return Ok(apiGraphFacebookResponseUsuarioNombreDTO);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMessengerWebHook([FromBody]InsertarWebHookDTO obj)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();

                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerAsesorRepositorio messengerAsesorRepositorio = new MessengerAsesorRepositorio(contexto);
                MessengerEnvioMasivoRepositorio messengerEnvioMasivoRepositorio = new MessengerEnvioMasivoRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(obj.RecipientId, 1);

                MessengerUsuarioBO usuario = _repMessengerUsuario.FirstBy(a => a.Psid == obj.PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);

                if (usuario != null && usuario.Id != 0)
                {
                    if (obj.EsPublicidad)
                    {
                        usuario.IdPersonal = obj.Asesor;
                    }
                    else if (usuario.IdPersonal != null)
                    {
                        var messengerAsesor = messengerAsesorRepositorio.FirstBy(x => x.IdPersonal == usuario.IdPersonal);
                        if (messengerAsesor == null)
                        {
                            if (obj.Asesor != null) usuario.IdPersonal = obj.Asesor;
                            else
                            {
                                messengerAsesor = messengerAsesorRepositorio.FirstBy(x => true);
                                if (messengerAsesor == null) usuario.IdPersonal = null;
                                else usuario.IdPersonal = messengerAsesor.IdPersonal;
                            }
                        }
                        else
                        {
                            usuario.IdPersonal = messengerAsesor.IdPersonal;
                        }
                    }
                    else
                    {
                        var messengerAsesor = messengerAsesorRepositorio.FirstBy(x => true);
                        if (messengerAsesor == null) usuario.IdPersonal = null;
                        else usuario.IdPersonal = messengerAsesor.IdPersonal;
                    }

                    usuario.SeRespondio = false;
                    usuario.FechaModificacion = DateTime.Now;
                    usuario.UsuarioModificacion = obj.Usuario;
                    _repMessengerUsuario.Update(usuario);

                    MessengerChatBO messengerChatBO = messengerChatRepositorio.FirstBy(x => x.IdMeseengerUsuario == usuario.Id && x.FacebookId == obj.FacebookId);

                    if(messengerChatBO == null || obj.FacebookId == "")
                    {
                        messengerChatBO = new MessengerChatBO();
                        messengerChatBO.IdMeseengerUsuario = usuario.Id;
                        messengerChatBO.IdPersonal = usuario.IdPersonal;
                        messengerChatBO.Mensaje = obj.Mensaje;
                        messengerChatBO.Tipo = true;
                        messengerChatBO.FacebookId = obj.FacebookId;
                        messengerChatBO.FechaInteraccion = obj.FechaInteraccion;
                        messengerChatBO.IdTipoMensajeMessenger = obj.IdTipoMensajeMessenger;
                        messengerChatBO.UrlArchivoAdjunto = obj.UrlArchivoAdjunto;
                        messengerChatBO.Estado = true;
                        messengerChatBO.FechaCreacion = DateTime.Now;
                        messengerChatBO.FechaModificacion = DateTime.Now;
                        messengerChatBO.UsuarioCreacion = obj.Usuario;
                        messengerChatBO.UsuarioModificacion = obj.Usuario;

                        messengerChatRepositorio.Insert(messengerChatBO);

                        if(usuario.Email != null)
                        {
                            var envioMasivo = messengerEnvioMasivoRepositorio.ObtenerMensajeMasivoEnviadoMessenger(usuario.Psid, messengerChatBO.FechaInteraccion?? DateTime.Now);

                            if(envioMasivo.IdFacebookAnuncio != 0)
                            {
                                messengerChatBO = new MessengerChatBO();
                                messengerChatBO.IdMeseengerUsuario = usuario.Id;
                                messengerChatBO.IdPersonal = usuario.IdPersonal;
                                messengerChatBO.Mensaje = envioMasivo.Plantilla;
                                messengerChatBO.Tipo = false;
                                messengerChatBO.FacebookId = null;
                                messengerChatBO.FechaInteraccion = obj.FechaInteraccion.AddMinutes(-1);
                                messengerChatBO.IdTipoMensajeMessenger = 1;
                                messengerChatBO.UrlArchivoAdjunto = null;
                                messengerChatBO.Estado = true;
                                messengerChatBO.FechaCreacion = DateTime.Now;
                                messengerChatBO.FechaModificacion = DateTime.Now;
                                messengerChatBO.UsuarioCreacion = obj.Usuario;
                                messengerChatBO.UsuarioModificacion = obj.Usuario;
                                messengerChatBO.IdFacebookAnuncio = envioMasivo.IdFacebookAnuncio;

                                messengerChatRepositorio.Insert(messengerChatBO);
                            }
                        }
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarPostComentarioWebHook([FromBody]InsertarComentarioFacebookDTO obj)
        {
            PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();
            FacebookPostRepositorio facebookPostRepositorio = new FacebookPostRepositorio();
            APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();

            try
            {
                if (obj.PSID.Equals("174599872598131"))
                {
                    PostComentarioDetalleRepositorio _repPostComentarioDetalle = new PostComentarioDetalleRepositorio();
                    var Usuario = _repPostComentarioDetalle.GetBy(x => x.IdCommentFacebook.Equals(obj.IdParent)).OrderByDescending(x=>x.FechaCreacion).FirstOrDefault();
                    if (Usuario == null)
                    {
                        return Ok();
                    }

                    PostComentarioDetalleBO postComentarioDetalle = new PostComentarioDetalleBO()
                    {

                        IdCommentFacebook = obj.IdComentario,
                        IdPostFacebook = obj.IdPost,
                        IdUsuarioFacebook = Usuario.IdUsuarioFacebook,
                        Message = obj.Mensaje,
                        IdPersonal = 4602,
                        Tipo = false,
                        Estado = true,
                        UsuarioCreacion = "Webhook",
                        UsuarioModificacion = "Webhook",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    var rpta2 = _repPostComentarioDetalle.Insert(postComentarioDetalle);
                    ComnentInsertarFacebookResultDTO result = new ComnentInsertarFacebookResultDTO();
                    result.FechaCreacion = postComentarioDetalle.FechaCreacion;
                    result.FechaModificacion = postComentarioDetalle.FechaModificacion;
                    result.Id = postComentarioDetalle.Id;
                    result.IdPersonal = postComentarioDetalle.IdPersonal;
                    result.IdUsuarioFacebook = postComentarioDetalle.IdUsuarioFacebook;
                    result.Nombres = "BSG Institute";
                    result.TieneRespuesta = true;
                    result.UsuarioCreacion = "Facebook";
                    result.UsuarioModificacion = "WebHookFacebook";
                    List<ComnentInsertarFacebookResultDTO> data = new List<ComnentInsertarFacebookResultDTO>();
                    data.Add(result);
                    return Ok(data);
                   
                }
                ApiGraphFacebookComentarioInformacionDTO apiGraphFacebookComentarioInformacionDTO = aPIGraphFacebook.ObtenerInformacionComentario(obj.IdComentario);
                var rpta = _repPostComentarioUsuario.PostComentarioInsertarWebHook(obj.PSID,obj.Nombres, obj.Mensaje,obj.IdComentario,obj.IdPost,obj.IdParent, obj.Usuario, obj.Asesor, apiGraphFacebookComentarioInformacionDTO.created_time);

                var PostFacebook = facebookPostRepositorio.FirstBy(x => x.IdPostFacebook == obj.IdPost);
                if(PostFacebook == null)
                {
                    ApiGraphFacebookPostDTO apiGraphFacebookPostDTO = aPIGraphFacebook.ObtenerInformacionPublicacion(obj.IdPost);
                    if (apiGraphFacebookPostDTO != null)
                    {
                        try
                        {
                            FacebookPostBO facebookPostBO = new FacebookPostBO();
                            facebookPostBO.Link = "";
                            facebookPostBO.Message = apiGraphFacebookPostDTO.message;
                            facebookPostBO.PermalinkUrl = apiGraphFacebookPostDTO.permalink_url;
                            facebookPostBO.UrlPicture = apiGraphFacebookPostDTO.full_picture;
                            facebookPostBO.IdPostFacebook = obj.IdPost;
                            facebookPostBO.ConjuntoAnuncioIdFacebook = "";
                            facebookPostBO.IdAnuncioFacebook = "";
                            facebookPostBO.Estado = true;
                            facebookPostBO.FechaCreacion = DateTime.Now;
                            facebookPostBO.FechaModificacion = DateTime.Now;
                            facebookPostBO.UsuarioCreacion = "WebHookFacebook";
                            facebookPostBO.UsuarioModificacion = "WebHookFacebook";

                            facebookPostRepositorio.Insert(facebookPostBO);

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                return Ok( rpta );
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMessengerChatFacebook([FromBody]InsertarMessengerFacebookDTO obj)
        {
            MessengerChatRepositorio _repMessengerChat = new MessengerChatRepositorio();
            try
            {
                obj.Mensaje = HttpUtility.UrlDecode(obj.Mensaje);
                var rpta = _repMessengerChat.InsertarMessengerChatFacebook(obj.PSID, obj.Mensaje, obj.Usuario, obj.Asesor);
                return Ok( rpta );
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]/{PSID}/{Postback}/{Usuario}/{RecipientId}")]
        [HttpPost]
        public ActionResult ValidarUsuario(string PSID, string Postback,string Usuario, string RecipientId)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);
                PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio(contexto);
                MessengerAsesorRepositorio _repMessengerAsesor = new MessengerAsesorRepositorio(contexto);
                MessengerUsuarioLogRepositorio _repMessengerUsuarioLog = new MessengerUsuarioLogRepositorio(contexto);
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);

                FacebookPaginaBO facebookPaginaBO = facebookPaginaRepositorio.FirstBy(x => x.FacebookId == RecipientId);

                var existe = _repMessengerUsuario.Exist(a => a.Psid == PSID && a.IdFacebookPagina == facebookPaginaBO.Id);

                if (existe)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{PSID}/{AreaCliente}/{Usuario}")]
        [HttpGet]
        public ActionResult InsertarComentarioLog(string PSID, string AreaCliente, string Usuario)
        {
            PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();
            PostComentarioUsuarioLogRepositorio _repPostComentarioUsuarioLog = new PostComentarioUsuarioLogRepositorio();

            var usuarioComentario = _repPostComentarioUsuario.ObtenerComentarioFacebookPorIdUsuario(PSID);

            var user = new PostComentarioUsuarioLogBO();
            user.IdAreaCapacitacion = Convert.ToInt32(AreaCliente);
            user.IdPersonal = usuarioComentario.IdPersonal;
            user.IdPostComentarioUsuario = usuarioComentario.Id;
            user.FechaCreacion = DateTime.Now;
            user.FechaModificacion = DateTime.Now;
            user.UsuarioCreacion = Usuario;
            user.UsuarioModificacion = Usuario;
            _repPostComentarioUsuarioLog.Insert(user);
            return Ok();
        }

		[Route("[Action]/{IdComentarioUsuario}")]
		[HttpGet]
		public ActionResult ObtenerUsuario(int IdComentarioUsuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PostComentarioUsuarioRepositorio _repPostComentarioUsuario = new PostComentarioUsuarioRepositorio();
				var postComentarioUsuario = _repPostComentarioUsuario.FirstById(IdComentarioUsuario);
				return Ok(postComentarioUsuario);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult InsertarComentarioDetalleFacebook([FromBody]InsertarComentarioDetalleFacebookDTO obj)
		{
			PostComentarioDetalleRepositorio _repPostComentarioDetalle = new PostComentarioDetalleRepositorio();
			try
			{
				PostComentarioDetalleBO postComentarioDetalle = new PostComentarioDetalleBO() {

					IdCommentFacebook = obj.IdCommentFacebook,
                    IdPostFacebook = obj.IdPostFacebook,
                    IdUsuarioFacebook = obj.IdUsuarioFacebook,
					Message = HttpUtility.UrlDecode(obj.Message),
					IdPersonal = obj.IdPersonal,
					Tipo = obj.Tipo,
					Estado = true,
					UsuarioCreacion = "Webhook",
					UsuarioModificacion = "Webhook",
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var rpta = _repPostComentarioDetalle.Insert(postComentarioDetalle);

				return Ok(rpta);
			}
			catch (Exception ex)
			{
				return Ok(ex.Message);
			}
		}

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnviarAreasCapacitacion([FromBody]RespuestaInboxDTO respuestaInboxDTO)
        {
            try
            {
                integraDBContext contexto = new integraDBContext();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                AreaCapacitacionFacebookRepositorio areaCapacitacionFacebookRepositorio = new AreaCapacitacionFacebookRepositorio(contexto);
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(respuestaInboxDTO.RecipientId, 1);

                var areas = areaCapacitacionFacebookRepositorio.GetAll().OrderBy(x => x.Orden).Take(13).ToList();
                object[] listaAreas = new object[areas.Count];
                for (int i = 0; i < areas.Count; i++)
                {
                    listaAreas[i] = new { content_type = "text", title = areas[i].Descripcion, payload = "area_" + areas[i].Id };
                }

                MessengerUsuarioBO usuario = _repMessengerUsuario.FirstBy(a => a.Psid == respuestaInboxDTO.PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);

                ApiGraphFacebookResponseMensajeInboxDTO respuestaAPI = aPIGraphFacebook.ResponderInboxQuickReplies(respuestaInboxDTO.PSID, respuestaInboxDTO.Mensaje, facebookAplicacionPaginaTokenDTO.Token, listaAreas);
                MessengerChatBO messengerChatBO = new MessengerChatBO();
                messengerChatBO.IdMeseengerUsuario = usuario.Id;
                messengerChatBO.IdPersonal = respuestaInboxDTO.IdPersonal;
                messengerChatBO.Mensaje = respuestaInboxDTO.Mensaje;
                messengerChatBO.Tipo = false;
                messengerChatBO.FacebookId = respuestaAPI.message_id;
                messengerChatBO.FechaInteraccion = DateTime.Now;
                messengerChatBO.IdTipoMensajeMessenger = 1;
                messengerChatBO.UrlArchivoAdjunto = null;
                messengerChatBO.Estado = true;
                messengerChatBO.FechaCreacion = DateTime.Now;
                messengerChatBO.FechaModificacion = DateTime.Now;
                messengerChatBO.UsuarioCreacion = respuestaInboxDTO.Usuario;
                messengerChatBO.UsuarioModificacion = respuestaInboxDTO.Usuario;

                messengerChatRepositorio.Insert(messengerChatBO);

                usuario.SeRespondio = true;
                usuario.FechaModificacion = DateTime.Now;
                usuario.UsuarioModificacion = respuestaInboxDTO.Usuario;

                _repMessengerUsuario.Update(usuario);

                return Ok(listaAreas);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]/{IdAreaCapacitacionFacebook}")]
        [HttpGet]
        public ActionResult ObtenerSubAreaCapacitacionPorAreaCapacitacionFacebook(int IdAreaCapacitacionFacebook)
        {
            try
            {
                SubAreaCapacitacionRepositorio subAreaCapacitacionRepositorio = new SubAreaCapacitacionRepositorio();
                var subAreas = subAreaCapacitacionRepositorio.ObtenerSubAreaPorAreaCapacitacionFacebook(IdAreaCapacitacionFacebook);

                object[] listaSubAreas = new object[subAreas.Count];
                for (int i = 0; i < subAreas.Count; i++)
                {
                    listaSubAreas[i] = new { content_type = "text", title = subAreas[i].AliasFacebook, payload = "subarea_" + subAreas[i].Id };
                }

                return Ok(listaSubAreas);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]/{IdCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerAsesoresPorCentroCosto(int IdCentroCosto)
        {
            try
            {
                AsesorChatDetalleRepositorio asesorChatDetalleRepositorio = new AsesorChatDetalleRepositorio();
                return Ok(asesorChatDetalleRepositorio.ObtenerAsesoresPorCentroCostoMessenger(IdCentroCosto));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ResponderInbox([FromBody]RespuestaInboxDTO respuestaInboxDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(respuestaInboxDTO.RecipientId, 1);

                MessengerUsuarioBO usuario = _repMessengerUsuario.FirstBy(a => a.Psid == respuestaInboxDTO.PSID && a.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);

                ApiGraphFacebookResponseMensajeInboxDTO respuestaAPI = aPIGraphFacebook.ResponderInbox(respuestaInboxDTO.PSID, respuestaInboxDTO.Mensaje, facebookAplicacionPaginaTokenDTO.Token);
                MessengerChatBO messengerChatBO = new MessengerChatBO();
                messengerChatBO.IdMeseengerUsuario = usuario.Id;
                messengerChatBO.IdPersonal = respuestaInboxDTO.IdPersonal;
                messengerChatBO.Mensaje = respuestaInboxDTO.Mensaje;
                messengerChatBO.Tipo = false;
                messengerChatBO.FacebookId = respuestaAPI.message_id;
                messengerChatBO.FechaInteraccion = DateTime.Now;
                messengerChatBO.IdTipoMensajeMessenger = 1;
                messengerChatBO.UrlArchivoAdjunto = null;
                messengerChatBO.Estado = true;
                messengerChatBO.FechaCreacion = DateTime.Now;
                messengerChatBO.FechaModificacion = DateTime.Now;
                messengerChatBO.UsuarioCreacion = respuestaInboxDTO.Usuario;
                messengerChatBO.UsuarioModificacion = respuestaInboxDTO.Usuario;

                messengerChatRepositorio.Insert(messengerChatBO);

                usuario.SeRespondio = true;
                usuario.FechaModificacion = DateTime.Now;
                usuario.UsuarioModificacion = respuestaInboxDTO.Usuario;

                _repMessengerUsuario.Update(usuario);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMensajeEcho([FromBody]MessengerInboxEchoDTO messengerInboxEchoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(messengerInboxEchoDTO.SenderId, 1);

                bool actualizar = false;

                MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstBy(x => x.Psid == messengerInboxEchoDTO.RecipientId && x.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);
                if(messengerUsuarioBO.Id == 0)
                {
                    messengerUsuarioBO = new MessengerUsuarioBO();
                    messengerUsuarioBO.Psid = messengerInboxEchoDTO.RecipientId;
                    messengerUsuarioBO.IdPersonal = 4602;
                    messengerUsuarioBO.SeRespondio = true;
                    messengerUsuarioBO.Estado = true;
                    messengerUsuarioBO.FechaCreacion = DateTime.Now;
                    messengerUsuarioBO.FechaModificacion = DateTime.Now;
                    messengerUsuarioBO.UsuarioCreacion = "Facebook";
                    messengerUsuarioBO.UsuarioModificacion = "Facebook";
                    messengerUsuarioBO.IdFacebookPagina = facebookAplicacionPaginaTokenDTO.IdFacebookPagina;

                    ApiGraphFacebookResponseUsuarioNombreDTO infoUsario = aPIGraphFacebook.ObtenerInformacionUsuarioConToken(messengerInboxEchoDTO.RecipientId, facebookAplicacionPaginaTokenDTO.Token);
                    if (infoUsario != null)
                    {
                        messengerUsuarioBO.Nombres = infoUsario.first_name;
                        messengerUsuarioBO.Apellidos = infoUsario.last_name;
                        messengerUsuarioBO.UrlFoto = infoUsario.profile_pic;
                    }
                    messengerUsuarioRepositorio.Insert(messengerUsuarioBO);
                }
                else
                {
                    actualizar = true;
                }

                MessengerChatBO messengerChatBO = messengerChatRepositorio.FirstBy(x => x.FacebookId == messengerInboxEchoDTO.FacebookId && x.IdMeseengerUsuario == messengerUsuarioBO.Id);
                if(messengerChatBO == null)
                {
                    messengerChatBO = new MessengerChatBO();
                    messengerChatBO.IdMeseengerUsuario = messengerUsuarioBO.Id;
                    messengerChatBO.IdPersonal = 4419;
                    messengerChatBO.Mensaje = messengerInboxEchoDTO.Mensaje;
                    messengerChatBO.Tipo = false;
                    messengerChatBO.FacebookId = messengerInboxEchoDTO.FacebookId;
                    messengerChatBO.FechaInteraccion = messengerInboxEchoDTO.FechaInteraccion;
                    messengerChatBO.IdTipoMensajeMessenger = messengerInboxEchoDTO.IdTipoMensajeMessenger;
                    messengerChatBO.UrlArchivoAdjunto = messengerInboxEchoDTO.UrlArchivoAdjunto;
                    messengerChatBO.Estado = true;
                    messengerChatBO.FechaCreacion = DateTime.Now;
                    messengerChatBO.FechaModificacion = DateTime.Now;
                    messengerChatBO.UsuarioCreacion = "Facebook";
                    messengerChatBO.UsuarioModificacion = "Facebook";

                    messengerChatRepositorio.Insert(messengerChatBO);

                    if (actualizar)
                    {
                        messengerUsuarioBO.SeRespondio = true;
                        messengerUsuarioBO.FechaModificacion = DateTime.Now;
                        messengerUsuarioBO.UsuarioModificacion = "Facebook";

                        messengerUsuarioRepositorio.Update(messengerUsuarioBO);
                    }

                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarLecturaMensaje([FromBody]MessengerInboxLecturaDTO messengerInboxLecturaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                FacebookPaginaRepositorio facebookPaginaRepositorio = new FacebookPaginaRepositorio(contexto);
                MessengerChatRepositorio messengerChatRepositorio = new MessengerChatRepositorio(contexto);
                MessengerUsuarioRepositorio messengerUsuarioRepositorio = new MessengerUsuarioRepositorio(contexto);

                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = facebookPaginaRepositorio.ObtenerTokenAcceso(messengerInboxLecturaDTO.RecipientId, 1);

                MessengerUsuarioBO messengerUsuarioBO = messengerUsuarioRepositorio.FirstBy(x => x.Psid == messengerInboxLecturaDTO.SenderId && x.IdFacebookPagina == facebookAplicacionPaginaTokenDTO.IdFacebookPagina);
                if (messengerUsuarioBO.Id != 0)
                {
                    List<MessengerChatBO> messengerChatBOs = messengerChatRepositorio.GetBy(x => x.IdMeseengerUsuario == messengerUsuarioBO.Id && x.Leido != true && x.FechaInteraccion <= messengerInboxLecturaDTO.FechaInteraccion && x.Tipo == false).ToList();
                    foreach(var messengerChatBO in messengerChatBOs)
                    {
                        messengerChatBO.Leido = true;
                        messengerChatBO.FechaLectura = messengerInboxLecturaDTO.FechaLectura;
                        messengerChatBO.FechaModificacion = DateTime.Now;
                        messengerChatBO.UsuarioModificacion = "Facebook";

                        messengerChatRepositorio.Update(messengerChatBO);
                    }

                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region ReaccionesFacebook
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarReaccion([FromBody]ReaccionPublicacionWebHookFacebookDTO reaccionPublicacionWebHookFacebookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FacebookTipoReaccionRepositorio facebookTipoReaccionRepositorio = new FacebookTipoReaccionRepositorio();
                FacebookReaccionPublicacionRepositorio facebookReaccionPublicacionRepositorio = new FacebookReaccionPublicacionRepositorio();
                FacebookPostRepositorio facebookPostRepositorio = new FacebookPostRepositorio();
                FacebookUsuarioRepositorio facebookUsuarioRepositorio = new FacebookUsuarioRepositorio();
                APIGraphFacebook aPIGraphFacebook = new APIGraphFacebook();

                FacebookTipoReaccionBO facebookTipoReaccionBO = facebookTipoReaccionRepositorio.FirstBy(x => x.Nombre == reaccionPublicacionWebHookFacebookDTO.TipoReaccion);
                if(facebookTipoReaccionBO == null)
                {
                    facebookTipoReaccionBO = new FacebookTipoReaccionBO();
                    facebookTipoReaccionBO.Nombre = reaccionPublicacionWebHookFacebookDTO.TipoReaccion;
                    facebookTipoReaccionBO.Estado = true;
                    facebookTipoReaccionBO.FechaCreacion = DateTime.Now;
                    facebookTipoReaccionBO.FechaModificacion = DateTime.Now;
                    facebookTipoReaccionBO.UsuarioCreacion = "WebHookFacebook";
                    facebookTipoReaccionBO.UsuarioModificacion = "WebHookFacebook";
                    facebookTipoReaccionRepositorio.Insert(facebookTipoReaccionBO);
                }

                FacebookUsuarioBO facebookUsuarioBO = facebookUsuarioRepositorio.FirstBy(x => x.FacebookId == reaccionPublicacionWebHookFacebookDTO.UsuarioFacebookId);
                if(facebookUsuarioBO == null)
                {
                    ApiGraphFacebookResponseUsuarioNombreDTO infoUsario = aPIGraphFacebook.ObtenerInformacionUsuario(reaccionPublicacionWebHookFacebookDTO.UsuarioFacebookId);
                    if (infoUsario != null)
                    {
                        facebookUsuarioBO = new FacebookUsuarioBO();
                        facebookUsuarioBO.FacebookId = reaccionPublicacionWebHookFacebookDTO.UsuarioFacebookId;
                        facebookUsuarioBO.Nombres = infoUsario.first_name;
                        facebookUsuarioBO.Apellidos = infoUsario.last_name;
                        facebookUsuarioBO.Estado = true;
                        facebookUsuarioBO.FechaCreacion = DateTime.Now;
                        facebookUsuarioBO.FechaModificacion = DateTime.Now;
                        facebookUsuarioBO.UsuarioCreacion = "WebHookFacebook";
                        facebookUsuarioBO.UsuarioModificacion = "WebHookFacebook";
                        facebookUsuarioRepositorio.Insert(facebookUsuarioBO);
                    }
                }

                FacebookPostBO facebookPostBO = facebookPostRepositorio.FirstBy(x => x.IdPostFacebook == reaccionPublicacionWebHookFacebookDTO.IdPostFacebook);
                if (facebookPostBO == null)
                {
                    ApiGraphFacebookPostDTO apiGraphFacebookPostDTO = aPIGraphFacebook.ObtenerInformacionPublicacion(reaccionPublicacionWebHookFacebookDTO.IdPostFacebook);
                    if (apiGraphFacebookPostDTO != null)
                    {
                            facebookPostBO = new FacebookPostBO();
                            facebookPostBO.Link = "";
                            facebookPostBO.Message = apiGraphFacebookPostDTO.message;
                            facebookPostBO.PermalinkUrl = apiGraphFacebookPostDTO.permalink_url;
                            facebookPostBO.UrlPicture = apiGraphFacebookPostDTO.full_picture;
                            facebookPostBO.IdPostFacebook = reaccionPublicacionWebHookFacebookDTO.IdPostFacebook;
                            facebookPostBO.ConjuntoAnuncioIdFacebook = "";
                            facebookPostBO.IdAnuncioFacebook = "";
                            facebookPostBO.Estado = true;
                            facebookPostBO.FechaCreacion = DateTime.Now;
                            facebookPostBO.FechaModificacion = DateTime.Now;
                            facebookPostBO.UsuarioCreacion = "WebHookFacebook";
                            facebookPostBO.UsuarioModificacion = "WebHookFacebook";
                            facebookPostRepositorio.Insert(facebookPostBO);
                    }
                }

                if(reaccionPublicacionWebHookFacebookDTO.Accion == "add")
                {
                    FacebookReaccionPublicacionBO facebookReaccionPublicacionBO = facebookReaccionPublicacionRepositorio.FirstBy(x => x.IdFacebookPost == facebookPostBO.Id && x.IdFacebookUsuario == facebookUsuarioBO.Id && x.IdFacebookTipoReaccion == facebookTipoReaccionBO.Id);
                    if(facebookReaccionPublicacionBO == null)
                    {
                        facebookReaccionPublicacionBO = new FacebookReaccionPublicacionBO();
                        facebookReaccionPublicacionBO.IdFacebookUsuario = facebookUsuarioBO.Id;
                        facebookReaccionPublicacionBO.IdFacebookPost = facebookPostBO.Id;
                        facebookReaccionPublicacionBO.IdFacebookTipoReaccion = facebookTipoReaccionBO.Id;
                        facebookReaccionPublicacionBO.Estado = true;
                        facebookReaccionPublicacionBO.FechaCreacion = DateTime.Now;
                        facebookReaccionPublicacionBO.FechaModificacion = DateTime.Now;
                        facebookReaccionPublicacionBO.UsuarioCreacion = "WebHookFacebook";
                        facebookReaccionPublicacionBO.UsuarioModificacion = "WebHookFacebook";
                        facebookReaccionPublicacionRepositorio.Insert(facebookReaccionPublicacionBO);
                    }

                }
                else if (reaccionPublicacionWebHookFacebookDTO.Accion == "edit")
                {
                    FacebookReaccionPublicacionBO facebookReaccionPublicacionBO = facebookReaccionPublicacionRepositorio.FirstBy(x => x.IdFacebookPost == facebookPostBO.Id && x.IdFacebookUsuario == facebookUsuarioBO.Id);
                    if (facebookReaccionPublicacionBO != null)
                    {
                        facebookReaccionPublicacionBO.IdFacebookTipoReaccion = facebookTipoReaccionBO.Id;
                        facebookReaccionPublicacionBO.FechaModificacion = DateTime.Now;
                        facebookReaccionPublicacionBO.UsuarioModificacion = "WebHookFacebook";
                        facebookReaccionPublicacionRepositorio.Update(facebookReaccionPublicacionBO);
                    }
                    else
                    {
                        facebookReaccionPublicacionBO = new FacebookReaccionPublicacionBO();
                        facebookReaccionPublicacionBO.IdFacebookUsuario = facebookUsuarioBO.Id;
                        facebookReaccionPublicacionBO.IdFacebookPost = facebookPostBO.Id;
                        facebookReaccionPublicacionBO.IdFacebookTipoReaccion = facebookTipoReaccionBO.Id;
                        facebookReaccionPublicacionBO.Estado = true;
                        facebookReaccionPublicacionBO.FechaCreacion = DateTime.Now;
                        facebookReaccionPublicacionBO.FechaModificacion = DateTime.Now;
                        facebookReaccionPublicacionBO.UsuarioCreacion = "WebHookFacebook";
                        facebookReaccionPublicacionBO.UsuarioModificacion = "WebHookFacebook";
                        facebookReaccionPublicacionRepositorio.Insert(facebookReaccionPublicacionBO);
                    }
                }
                else if (reaccionPublicacionWebHookFacebookDTO.Accion == "remove")
                {
                    FacebookReaccionPublicacionBO facebookReaccionPublicacionBO = facebookReaccionPublicacionRepositorio.FirstBy(x => x.IdFacebookPost == facebookPostBO.Id && x.IdFacebookUsuario == facebookUsuarioBO.Id && x.IdFacebookTipoReaccion == facebookTipoReaccionBO.Id);
                    if (facebookReaccionPublicacionBO != null)
                    {
                        facebookReaccionPublicacionBO.Estado = false;
                        facebookReaccionPublicacionBO.FechaModificacion = DateTime.Now;
                        facebookReaccionPublicacionBO.UsuarioModificacion = "WebHookFacebook";
                        facebookReaccionPublicacionRepositorio.Update(facebookReaccionPublicacionBO);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        #endregion

    }
}

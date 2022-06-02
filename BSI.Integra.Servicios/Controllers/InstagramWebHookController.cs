using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/InstagramWebHook")]
    [ApiController]
    public class InstagramWebHookController : ControllerBase
    {
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarMensajeInstagram([FromBody]WebHookValueInstagramDTO obj)
        {
            try
            {
                APIGraphFacebookInstagram aPIGraphFacebookInstagram = new APIGraphFacebookInstagram();
                InstagramComentarioRepositorio instagramComentarioRepositorio = new InstagramComentarioRepositorio();
                InstagramPublicacionRepositorio instagramPublicacionRepositorio = new InstagramPublicacionRepositorio();
                InstagramUsuarioRepositorio instagramUsuarioRepositorio = new InstagramUsuarioRepositorio();

                InstagramComentarioBO instagramComentarioBO = new InstagramComentarioBO();
                InstagramPublicacionBO instagramPublicacionBO = new InstagramPublicacionBO();
                InstagramUsuarioBO instagramUsuarioBO = new InstagramUsuarioBO();

                instagramComentarioBO = instagramComentarioRepositorio.FirstBy(x => x.InstagramId == obj.id);
                if(instagramComentarioBO == null)
                {
                    ApiGraphInstagramUsuarioPublicacionDTO apiGraphInstagramUsuarioPublicacionDTO = aPIGraphFacebookInstagram.ObtenerUsuarioPublicacion(obj.id);
                    if(apiGraphInstagramUsuarioPublicacionDTO != null)
                    {
                        instagramUsuarioBO = instagramUsuarioRepositorio.FirstBy(x => x.Usuario == apiGraphInstagramUsuarioPublicacionDTO.username);
                        if(instagramUsuarioBO == null)
                        {
                            instagramUsuarioBO = new InstagramUsuarioBO();

                            instagramUsuarioBO.Usuario = apiGraphInstagramUsuarioPublicacionDTO.username;
                            instagramUsuarioBO.Estado = true;
                            instagramUsuarioBO.FechaCreacion = DateTime.Now;
                            instagramUsuarioBO.FechaModificacion = DateTime.Now;
                            instagramUsuarioBO.UsuarioCreacion = "WebHookInstagram";
                            instagramUsuarioBO.UsuarioModificacion = "WebHookInstagram";

                            instagramUsuarioRepositorio.Insert(instagramUsuarioBO);
                        }

                        instagramPublicacionBO = instagramPublicacionRepositorio.FirstBy(x => x.InstagramId == apiGraphInstagramUsuarioPublicacionDTO.media.id);
                        if(instagramPublicacionBO == null)
                        {
                            instagramPublicacionBO = new InstagramPublicacionBO();

                            instagramPublicacionBO.InstagramId = apiGraphInstagramUsuarioPublicacionDTO.media.id;
                            instagramPublicacionBO.Subtitulo = apiGraphInstagramUsuarioPublicacionDTO.media.caption;
                            instagramPublicacionBO.TipoMedia = apiGraphInstagramUsuarioPublicacionDTO.media.media_type;
                            instagramPublicacionBO.UrlMedia = apiGraphInstagramUsuarioPublicacionDTO.media.media_url;
                            instagramPublicacionBO.Estado = true;
                            instagramPublicacionBO.FechaCreacion = DateTime.Now;
                            instagramPublicacionBO.FechaModificacion = DateTime.Now;
                            instagramPublicacionBO.UsuarioCreacion = "WebHookInstagram";
                            instagramPublicacionBO.UsuarioModificacion = "WebHookInstagram";

                            instagramPublicacionRepositorio.Insert(instagramPublicacionBO);
                        }

                        instagramComentarioBO = new InstagramComentarioBO();

                        instagramComentarioBO.InstagramId = obj.id;
                        instagramComentarioBO.Texto = obj.text;
                        instagramComentarioBO.FechaInteraccion = apiGraphInstagramUsuarioPublicacionDTO.timestamp;
                        instagramComentarioBO.IdInstagramUsuario = instagramUsuarioBO.Id;
                        instagramComentarioBO.IdInstagramPublicacion = instagramPublicacionBO.Id;
                        instagramComentarioBO.IdPersonalAsociado = 4602;  //Mauricio Rodriguez
                        instagramComentarioBO.EsUsuarioInstagram = apiGraphInstagramUsuarioPublicacionDTO.username=="bsg_institute" ? false : true;
                        instagramComentarioBO.Estado = true;
                        instagramComentarioBO.FechaCreacion = DateTime.Now;
                        instagramComentarioBO.FechaModificacion = DateTime.Now;
                        instagramComentarioBO.UsuarioCreacion = "WebHookInstagram";
                        instagramComentarioBO.UsuarioModificacion = "WebHookInstagram";

                        InstagramComentarioBO comprobarMensajeExistente = instagramComentarioRepositorio.FirstBy(x => x.InstagramId == obj.id);
                        if (comprobarMensajeExistente == null)
                        {
                            instagramComentarioRepositorio.Insert(instagramComentarioBO);
                        }
                    }
                }

                return Ok(new { instagramComentarioBO.IdInstagramUsuario, instagramComentarioBO.IdInstagramPublicacion, instagramUsuarioBO.Usuario, instagramComentarioBO.Texto, instagramComentarioBO.EsUsuarioInstagram, instagramComentarioBO.IdPersonalAsociado});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Instagram")]
    [ApiController]
    public class InstagramController : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenterPlantillas()
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                var plantillasInstagram = _repPlantillaClaveValor.ObtenerPlantillaPorPlantillaBase("Instagram - Comentarios").OrderBy(w => w.Nombre);

                return Ok(plantillasInstagram);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPlantilla}")]
        [HttpGet]
        public ActionResult GenerarPlantillaComentarios(int IdPlantilla)
        {
            try
            {
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();

                var plantilla = _repPlantillaClaveValor.FirstBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", y => y.Valor);
                return Ok(plantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerComentariosPorIdPersonal(int IdPersonal)
        {
            try
            {
                InstagramComentarioRepositorio instagramComentarioRepositorio = new InstagramComentarioRepositorio();

                return Ok(instagramComentarioRepositorio.ObtenerComentariosPorIdPersonal(IdPersonal));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdInstagramUsuario}/{IdInstagramPublicacion}")]
        [HttpGet]
        public ActionResult ObtenerComentarioDetalle(int IdInstagramUsuario, int IdInstagramPublicacion)
        {
            try
            {
                InstagramComentarioRepositorio instagramComentarioRepositorio = new InstagramComentarioRepositorio();

                return Ok(instagramComentarioRepositorio.ObtenerComentariosPorUsuarioPublicacion(IdInstagramUsuario, IdInstagramPublicacion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ResponderComentario([FromBody] RespuestaComentarioInstagramDTO respuestaComentarioInstagramDTO)
        {
            try
            {
                APIGraphFacebookInstagram aPIGraphFacebookInstagram = new APIGraphFacebookInstagram();

                InstagramComentarioRepositorio instagramComentarioRepositorio = new InstagramComentarioRepositorio();

                ApiGraphInstagramResponseComentarioDTO apiGraphInstagramResponseComentarioDTO = aPIGraphFacebookInstagram.ResponderComentarioInbox(respuestaComentarioInstagramDTO);

                InstagramComentarioBO instagramComentarioBO = instagramComentarioRepositorio.FirstBy(x => x.InstagramId == apiGraphInstagramResponseComentarioDTO.id);
                if (instagramComentarioBO == null)
                {
                    instagramComentarioBO = new InstagramComentarioBO();
                    instagramComentarioBO.InstagramId = apiGraphInstagramResponseComentarioDTO.id;
                    instagramComentarioBO.Texto = respuestaComentarioInstagramDTO.Mensaje;
                    instagramComentarioBO.FechaInteraccion = DateTime.Now;
                    instagramComentarioBO.IdInstagramUsuario = respuestaComentarioInstagramDTO.IdInstagramUsuario;
                    instagramComentarioBO.IdInstagramPublicacion = respuestaComentarioInstagramDTO.IdInstagramPublicacion;
                    instagramComentarioBO.IdPersonalAsociado = respuestaComentarioInstagramDTO.IdPersonal;
                    instagramComentarioBO.EsUsuarioInstagram = false;
                    instagramComentarioBO.Estado = true;
                    instagramComentarioBO.FechaCreacion = DateTime.Now;
                    instagramComentarioBO.FechaModificacion = DateTime.Now;
                    instagramComentarioBO.UsuarioCreacion = respuestaComentarioInstagramDTO.Usuario;
                    instagramComentarioBO.UsuarioModificacion = respuestaComentarioInstagramDTO.Usuario;

                    instagramComentarioRepositorio.Insert(instagramComentarioBO);
                }
                else
                {
                    instagramComentarioBO.IdInstagramUsuario = respuestaComentarioInstagramDTO.IdInstagramUsuario;
                    instagramComentarioBO.IdPersonalAsociado = respuestaComentarioInstagramDTO.IdPersonal; 
                    instagramComentarioBO.FechaModificacion = DateTime.Now;
                    instagramComentarioBO.UsuarioModificacion = respuestaComentarioInstagramDTO.Usuario;

                    instagramComentarioRepositorio.Update(instagramComentarioBO);
                }
                return Ok(new { instagramComentarioBO.IdInstagramUsuario, instagramComentarioBO.IdInstagramPublicacion, respuestaComentarioInstagramDTO.Usuario, instagramComentarioBO.Texto });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
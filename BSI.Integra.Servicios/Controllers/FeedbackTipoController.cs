using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FeedbackTipo")]
    public class FeedbackTipoController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public FeedbackTipoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFeedbackTipo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackTipoRepositorio _repFeedbackTipo = new FeedbackTipoRepositorio(_integraDBContext);
                return Ok(_repFeedbackTipo.ObtenerTodoFeedbackTipoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarFeedbackTipo([FromBody] FeedbackTipoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackTipoRepositorio _repFeedbackTipo = new FeedbackTipoRepositorio(_integraDBContext);
                FeedbackTipoBO NuevaFeedbackTipo = new FeedbackTipoBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repFeedbackTipo.Insert(NuevaFeedbackTipo);
                return Ok(NuevaFeedbackTipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarFeedbackTipo([FromBody] FeedbackTipoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackTipoRepositorio _repFeedbackTipo = new FeedbackTipoRepositorio(_integraDBContext);
                FeedbackTipoBO FeedbackTipo = _repFeedbackTipo.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                FeedbackTipo.Nombre = ObjetoDTO.Nombre;
                FeedbackTipo.Estado = true;
                FeedbackTipo.UsuarioModificacion = ObjetoDTO.Usuario;
                FeedbackTipo.FechaModificacion = DateTime.Now;
                _repFeedbackTipo.Update(FeedbackTipo);
                return Ok(FeedbackTipo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFeedbackTipo([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FeedbackTipoRepositorio _repFeedbackTipo = new FeedbackTipoRepositorio(_integraDBContext);
                FeedbackTipoBO FeedbackTipo = _repFeedbackTipo.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repFeedbackTipo.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Transversal.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TiempoLibre")]
    public class TiempoLibreController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public TiempoLibreController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult vizualizarTiempoLibres()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoLibreRepositorio _repTiempoLibre = new TiempoLibreRepositorio(_integraDBContext);
                var TiempoLibreRepositorio = _repTiempoLibre.ObtenerAllTiempoLibre();
                return Json(new { Result = "OK", Records = TiempoLibreRepositorio });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarTiempoLibre([FromBody] TiempoLibreDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoLibreRepositorio _repTiempoLibre = new TiempoLibreRepositorio(_integraDBContext);
                TiempoLibreBO NuevaTiempoLibre = new TiempoLibreBO();

                NuevaTiempoLibre.TiempoMin = ObjetoDTO.TiempoMin;
                NuevaTiempoLibre.Tipo = ObjetoDTO.Tipo;
                NuevaTiempoLibre.Estado = true;
                NuevaTiempoLibre.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevaTiempoLibre.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevaTiempoLibre.FechaCreacion = DateTime.Now;
                NuevaTiempoLibre.FechaModificacion = DateTime.Now;

                _repTiempoLibre.Insert(NuevaTiempoLibre);

                return Ok(NuevaTiempoLibre);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPut]
        public ActionResult ActualizarTiempoLibre([FromBody] TiempoLibreDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoLibreRepositorio _repTiempoLibre = new TiempoLibreRepositorio(_integraDBContext);
                TiempoLibreBO TiempoLibre = _repTiempoLibre.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                TiempoLibre.Tipo = ObjetoDTO.Tipo;
                TiempoLibre.TiempoMin = ObjetoDTO.TiempoMin;
                TiempoLibre.Estado = true;
                TiempoLibre.UsuarioModificacion = ObjetoDTO.Usuario;
                TiempoLibre.FechaModificacion = DateTime.Now;

                _repTiempoLibre.Update(TiempoLibre);

                return Ok(TiempoLibre);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpDelete]
        public ActionResult EliminarTiempoLibre(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TiempoLibreRepositorio _repTiempoLibre = new TiempoLibreRepositorio(_integraDBContext);
                TiempoLibreBO TiempoLibre = _repTiempoLibre.GetBy(x => x.Id == Id).FirstOrDefault();

                _repTiempoLibre.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}

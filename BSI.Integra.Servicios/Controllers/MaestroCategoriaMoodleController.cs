using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroCategoriaMoodle")]
    [ApiController]
    public class MaestroCategoriaMoodleController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MoodleCategoriaRepositorio _repMoodleCategoria;
        private readonly MoodleCategoriaTipoRepositorio _repMoodleCategoriaTipo;

        public MaestroCategoriaMoodleController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repMoodleCategoria = new MoodleCategoriaRepositorio(_integraDBContext);
            _repMoodleCategoriaTipo = new MoodleCategoriaTipoRepositorio(_integraDBContext);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCategoriasRegistradas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaCategoriaMoodle = _repMoodleCategoria.ObtenerCategoriasMoodleRegistradas();
                return Ok(listaCategoriaMoodle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarCategoriaMoodle([FromBody] CategoriaMoodleDTO CategoriaMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoodleCategoriaBO maestroCategoriaMoodle = new MoodleCategoriaBO()
                {
                    IdCategoriaMoodle = CategoriaMoodle.IdCategoriaMoodle,
                	NombreCategoria = CategoriaMoodle.NombreCategoria,
                	IdMoodleCategoriaTipo = CategoriaMoodle.IdMoodleCategoriaTipo,
                	AplicaProyecto = true,
                    Estado = true,
                    UsuarioCreacion = CategoriaMoodle.Usuario,
                    UsuarioModificacion = CategoriaMoodle.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var res = _repMoodleCategoria.Insert(maestroCategoriaMoodle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCategoriaMoodle([FromBody] CategoriaMoodleDTO CategoriaMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                var categoriaMoodle = _repMoodleCategoria.FirstById(CategoriaMoodle.Id);

                categoriaMoodle.IdCategoriaMoodle = CategoriaMoodle.IdCategoriaMoodle;
                categoriaMoodle.NombreCategoria = CategoriaMoodle.NombreCategoria;
                categoriaMoodle.IdMoodleCategoriaTipo = CategoriaMoodle.IdMoodleCategoriaTipo;
                categoriaMoodle.UsuarioModificacion = CategoriaMoodle.Usuario;
                categoriaMoodle.FechaModificacion = DateTime.Now;

                var res = _repMoodleCategoria.Update(categoriaMoodle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCategoriaMoodle([FromBody] EliminarDTO CategoriaMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repMoodleCategoria.Exist(CategoriaMoodle.Id))
                {
                    var res = _repMoodleCategoria.Delete(CategoriaMoodle.Id, CategoriaMoodle.NombreUsuario);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var ListaPorNombre = _repMoodleCategoriaTipo.ObtenerCategoriasPorNombre();                
                return Ok(ListaPorNombre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

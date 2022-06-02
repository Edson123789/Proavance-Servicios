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
    [Route("api/MaestroCursoMoodle")]
    [ApiController]
    public class MaestroCursoMoodleController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MoodleCursoRepositorio _repMoodleCurso;
        private readonly MoodleCategoriaRepositorio _repMoodleCategoria;

        public MaestroCursoMoodleController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repMoodleCurso = new MoodleCursoRepositorio(_integraDBContext);
            _repMoodleCategoria = new MoodleCategoriaRepositorio(_integraDBContext);
        }

        [HttpPost]
        [Route("[action]")]        
        public ActionResult ObtenerCursosRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaCursoMoodle = _repMoodleCurso.ObtenerCursosMoodleRegistrado();
                return Ok(listaCursoMoodle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarCursoMoodle([FromBody]MaestroCursoMoodleDTO CursoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoodleCursoBO cursoMaestroMoodle = new MoodleCursoBO()
                {                    
                    IdCursoMoodle = CursoMoodle.IdCursoMoodle,
                    IdCategoriaMoodle = CursoMoodle.IdCategoria,
                    Nombre = CursoMoodle.NombreCursoMoodle,
                    Estado = true,
                    UsuarioCreacion = CursoMoodle.Usuario,
                    UsuarioModificacion = CursoMoodle.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now                    
                };
                var res = _repMoodleCurso.Insert(cursoMaestroMoodle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCursoMoodle([FromBody] MaestroCursoMoodleDTO CursoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var cursoMoodle = _repMoodleCurso.FirstById(CursoMoodle.Id);
                cursoMoodle.IdCategoriaMoodle = CursoMoodle.IdCategoria;
                cursoMoodle.IdCursoMoodle = CursoMoodle.IdCursoMoodle;
                cursoMoodle.Nombre = CursoMoodle.NombreCursoMoodle;
                cursoMoodle.UsuarioModificacion = CursoMoodle.Usuario;
                cursoMoodle.FechaModificacion = DateTime.Now;
                var res = _repMoodleCurso.Update(cursoMoodle);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCursoMoodle([FromBody] EliminarDTO CursoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repMoodleCurso.Exist(CursoMoodle.Id))
                {
                    var res = _repMoodleCurso.Delete(CursoMoodle.Id, CursoMoodle.NombreUsuario);
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
                var ListaPorNombre = _repMoodleCategoria.ObtenerCategoriasPorNombre();
                return Ok(ListaPorNombre);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CriterioEvaluacionCategoria")]
    public class CriterioEvaluacionCategoriaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CriterioEvaluacionCategoriaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            CriterioEvaluacionCategoriaRepositorio _repCriterioEvaluacionCategoria = new CriterioEvaluacionCategoriaRepositorio(_integraDBContext);

            var rpta = _repCriterioEvaluacionCategoria.ObtenerTodo();
            return Ok(new { Data = rpta, Total = rpta.Count });
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]CriterioEvaluacionCategoriaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionCategoriaRepositorio _repCriterioEvaluacionCategoria = new CriterioEvaluacionCategoriaRepositorio(_integraDBContext);

                CriterioEvaluacionCategoriaBO CriterioEvaluacionCategoriaBO = new CriterioEvaluacionCategoriaBO();
                CriterioEvaluacionCategoriaBO.Nombre = Obj.Nombre;
                CriterioEvaluacionCategoriaBO.Estado = true;
                CriterioEvaluacionCategoriaBO.UsuarioCreacion = Obj.Usuario;
                CriterioEvaluacionCategoriaBO.UsuarioModificacion = Obj.Usuario;
                CriterioEvaluacionCategoriaBO.FechaCreacion = DateTime.Now;
                CriterioEvaluacionCategoriaBO.FechaModificacion = DateTime.Now;

                _repCriterioEvaluacionCategoria.Insert(CriterioEvaluacionCategoriaBO);

                return Ok(CriterioEvaluacionCategoriaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]CriterioEvaluacionCategoriaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionCategoriaRepositorio _repCriterioEvaluacionCategoria = new CriterioEvaluacionCategoriaRepositorio(_integraDBContext);

                CriterioEvaluacionCategoriaBO CriterioEvaluacionCategoriaBO = _repCriterioEvaluacionCategoria.FirstById(Obj.Id);
                CriterioEvaluacionCategoriaBO.Nombre = Obj.Nombre;
                CriterioEvaluacionCategoriaBO.UsuarioModificacion = Obj.Usuario;
                CriterioEvaluacionCategoriaBO.FechaModificacion = DateTime.Now;

                _repCriterioEvaluacionCategoria.Update(CriterioEvaluacionCategoriaBO);

                return Ok(CriterioEvaluacionCategoriaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult Eliminar(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionCategoriaRepositorio _repCriterioEvaluacionCategoria = new CriterioEvaluacionCategoriaRepositorio(_integraDBContext);

                _repCriterioEvaluacionCategoria.Delete(Id, Usuario);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

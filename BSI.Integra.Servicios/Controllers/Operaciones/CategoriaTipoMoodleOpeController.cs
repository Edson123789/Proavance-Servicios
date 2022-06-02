using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/CategoriaTipoMoodle")]
    [ApiController]
    public class CategoriaTipoMoodleOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                CategoriaTipoMoodleRepositorio _repCategoriaTipoMoodleRepositorio = new CategoriaTipoMoodleRepositorio();
                return Ok(_repCategoriaTipoMoodleRepositorio.GetBy(x => x.Estado == true, x => new { x.Id, x.IdCategoriaMoodle, x.NombreCategoria,  x.IdTipoCapacitacionMoodle, x.AplicaProyecto }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CategoriaTipoMoodleDTO CategoriaTipoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaTipoMoodleRepositorio _repCategoriaTipoMoodle = new CategoriaTipoMoodleRepositorio();
                //validamos no exista otro registro con el mismo IdCategoria
                if (_repCategoriaTipoMoodle.GetBy(x => x.IdCategoriaMoodle == CategoriaTipoMoodle.IdCategoriaMoodle).Count() == 0)
                {
                    CategoriaTipoMoodleBO categoriaTipoMoodleBO = new CategoriaTipoMoodleBO()
                    {
                        IdCategoriaMoodle = CategoriaTipoMoodle.IdCategoriaMoodle,
                        NombreCategoria = CategoriaTipoMoodle.NombreCategoria,
                        IdTipoCapacitacionMoodle = CategoriaTipoMoodle.IdTipoCapacitacionMoodle,
                        AplicaProyecto = true,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = CategoriaTipoMoodle.NombreUsuario,
                        UsuarioModificacion = CategoriaTipoMoodle.NombreUsuario
                    };
                    if (!categoriaTipoMoodleBO.HasErrors)
                    {
                        _repCategoriaTipoMoodle.Insert(categoriaTipoMoodleBO);
                    }
                    else
                    {
                        return BadRequest(categoriaTipoMoodleBO.ActualesErrores);
                    }
                    return Ok(new { categoriaTipoMoodleBO.Id, categoriaTipoMoodleBO.IdCategoriaMoodle, categoriaTipoMoodleBO.NombreCategoria, categoriaTipoMoodleBO.IdTipoCapacitacionMoodle, categoriaTipoMoodleBO.AplicaProyecto });
                }
                else {
                    return BadRequest("La Categoría seleccionada ya existe.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CategoriaTipoMoodleDTO CategoriaTipoMoodle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CategoriaTipoMoodleRepositorio _repCategoriaTipoMoodle = new CategoriaTipoMoodleRepositorio();
                if (_repCategoriaTipoMoodle.Exist(CategoriaTipoMoodle.Id))
                {
                    var categoriaTipoMoodleBO = _repCategoriaTipoMoodle.FirstById(CategoriaTipoMoodle.Id);
                    categoriaTipoMoodleBO.IdCategoriaMoodle = CategoriaTipoMoodle.IdCategoriaMoodle;
                    categoriaTipoMoodleBO.IdTipoCapacitacionMoodle = CategoriaTipoMoodle.IdTipoCapacitacionMoodle;
                    categoriaTipoMoodleBO.NombreCategoria = CategoriaTipoMoodle.NombreCategoria;
                    categoriaTipoMoodleBO.IdTipoCapacitacionMoodle = CategoriaTipoMoodle.IdTipoCapacitacionMoodle;
                    categoriaTipoMoodleBO.AplicaProyecto = CategoriaTipoMoodle.AplicaProyecto == null ? false: CategoriaTipoMoodle.AplicaProyecto.Value;
                    categoriaTipoMoodleBO.FechaModificacion = DateTime.Now;
                    categoriaTipoMoodleBO.UsuarioModificacion = CategoriaTipoMoodle.NombreUsuario;
                    if (!categoriaTipoMoodleBO.HasErrors)
                    {
                        _repCategoriaTipoMoodle.Update(categoriaTipoMoodleBO);
                    }
                    else
                    {
                        return BadRequest(categoriaTipoMoodleBO.ActualesErrores);
                    }
                    return Ok(new { categoriaTipoMoodleBO.Id, categoriaTipoMoodleBO.IdCategoriaMoodle, categoriaTipoMoodleBO.NombreCategoria, categoriaTipoMoodleBO.IdTipoCapacitacionMoodle, categoriaTipoMoodleBO.AplicaProyecto });
                }
                else
                {
                    return BadRequest("La Categoría seleccionada no existe.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

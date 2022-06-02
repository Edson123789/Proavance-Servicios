using System;
using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialCriterioVerificacion")]
    public class MaterialCriterioVerificacionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialCriterioVerificacionRepositorio _repMaterialCriterioVerificacion;
        public MaterialCriterioVerificacionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialCriterioVerificacion = new MaterialCriterioVerificacionRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialCriterioVerificacion.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repMaterialCriterioVerificacion.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialCriterioVerificacion.Exist(Id))
                {
                    return BadRequest("Criterio de verificacion de material no existente");
                }
                return Ok(Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MaterialCriterioVerificacionDTO MaterialCriterioVerificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialCriterioVerificacion = new MaterialCriterioVerificacionBO()
                {
                    Nombre = MaterialCriterioVerificacion.Nombre,
                    Descripcion = MaterialCriterioVerificacion.Descripcion,
                    UsuarioCreacion = MaterialCriterioVerificacion.NombreUsuario,
                    UsuarioModificacion = MaterialCriterioVerificacion.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!materialCriterioVerificacion.HasErrors)
                {
                    _repMaterialCriterioVerificacion.Insert(materialCriterioVerificacion);
                }
                else
                {
                    return BadRequest(materialCriterioVerificacion.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MaterialCriterioVerificacionDTO MaterialCriterioVerificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialCriterioVerificacion.Exist(MaterialCriterioVerificacion.Id))
                {
                    return BadRequest("Criterio de verificacion de material no existente!");
                }
                var materialCriterioVerificacion = _repMaterialCriterioVerificacion.FirstById(MaterialCriterioVerificacion.Id);
                materialCriterioVerificacion.Nombre = MaterialCriterioVerificacion.Nombre;
                materialCriterioVerificacion.Descripcion = MaterialCriterioVerificacion.Descripcion;
                materialCriterioVerificacion.UsuarioModificacion = MaterialCriterioVerificacion.NombreUsuario;
                materialCriterioVerificacion.FechaModificacion = DateTime.Now;
                if (!materialCriterioVerificacion.HasErrors)
                {
                    _repMaterialCriterioVerificacion.Update(materialCriterioVerificacion);
                }
                else
                {
                    return BadRequest(materialCriterioVerificacion.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialCriterioVerificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialCriterioVerificacion.Exist(MaterialCriterioVerificacion.Id))
                {
                    return BadRequest("Criterio de verificacion de material no existente");
                }
                _repMaterialCriterioVerificacion.Delete(MaterialCriterioVerificacion.Id, MaterialCriterioVerificacion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

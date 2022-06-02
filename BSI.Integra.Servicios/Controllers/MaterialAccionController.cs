using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialAccion")]
    public class MaterialAccionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialAccionRepositorio _repMaterialAccion;
        public MaterialAccionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialAccion = new MaterialAccionRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialAccion.ObtenerTodoFiltro());
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
                return Ok(_repMaterialAccion.Obtener());
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
                if (!_repMaterialAccion.Exist(Id))
                {
                    return BadRequest("Accion de material no existente");
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
        public ActionResult Insertar([FromBody] MaterialAccionDTO MaterialAccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialAccion = new MaterialAccionBO()
                {
                    Nombre = MaterialAccion.Nombre,
                    Descripcion = MaterialAccion.Descripcion,
                    UsuarioCreacion = MaterialAccion.NombreUsuario,
                    UsuarioModificacion = MaterialAccion.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!materialAccion.HasErrors)
                {
                    _repMaterialAccion.Insert(materialAccion);
                }
                else
                {
                    return BadRequest(materialAccion.GetErrors());
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
        public ActionResult Actualizar([FromBody] MaterialAccionDTO MaterialAccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialAccion.Exist(MaterialAccion.Id))
                {
                    return BadRequest("Accion de material no existente!");
                }
                var materialAccion = _repMaterialAccion.FirstById(MaterialAccion.Id);
                materialAccion.Nombre = MaterialAccion.Nombre;
                materialAccion.Descripcion = MaterialAccion.Descripcion;
                materialAccion.UsuarioModificacion = MaterialAccion.NombreUsuario;
                materialAccion.FechaModificacion = DateTime.Now;
                if (!materialAccion.HasErrors)
                {
                    _repMaterialAccion.Update(materialAccion);
                }
                else
                {
                    return BadRequest(materialAccion.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialAccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialAccion.Exist(MaterialAccion.Id))
                {
                    return BadRequest("Accion de material no existente");
                }
                _repMaterialAccion.Delete(MaterialAccion.Id, MaterialAccion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

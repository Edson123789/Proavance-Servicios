using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialEstado")]
    public class MaterialEstadoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialEstadoRepositorio _repMaterialEstado;
        public MaterialEstadoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialEstado = new MaterialEstadoRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialEstado.ObtenerTodoFiltro());
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
                return Ok(_repMaterialEstado.Obtener());
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
                if (!_repMaterialEstado.Exist(Id))
                {
                    return BadRequest("Estado de material no existente");
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
        public ActionResult Insertar([FromBody] MaterialEstadoDTO MaterialEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialEstado = new MaterialEstadoBO()
                {
                    Nombre = MaterialEstado.Nombre,
                    Descripcion = MaterialEstado.Descripcion,
                    UsuarioCreacion = MaterialEstado.NombreUsuario,
                    UsuarioModificacion = MaterialEstado.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!materialEstado.HasErrors)
                {
                    _repMaterialEstado.Insert(materialEstado);
                }
                else
                {
                    return BadRequest(materialEstado.GetErrors());
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
        public ActionResult Actualizar([FromBody] MaterialEstadoDTO MaterialEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialEstado.Exist(MaterialEstado.Id))
                {
                    return BadRequest("Estado de material no existente!");
                }
                var materialTipo = _repMaterialEstado.FirstById(MaterialEstado.Id);
                materialTipo.Nombre = MaterialEstado.Nombre;
                materialTipo.Descripcion = MaterialEstado.Descripcion;
                materialTipo.UsuarioModificacion = MaterialEstado.NombreUsuario;
                materialTipo.FechaModificacion = DateTime.Now;
                if (!materialTipo.HasErrors)
                {
                    _repMaterialEstado.Update(materialTipo);
                }
                else
                {
                    return BadRequest(materialTipo.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialEstado.Exist(MaterialEstado.Id))
                {
                    return BadRequest("Estado de material no existente");
                }
                _repMaterialEstado.Delete(MaterialEstado.Id, MaterialEstado.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

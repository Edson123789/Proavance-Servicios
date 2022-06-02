using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialVersion")]
    public class MaterialVersionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        public MaterialVersionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repMaterialVersion.ObtenerTodoFiltro());
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
                return Ok(_repMaterialVersion.Obtener());
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
                if (!_repMaterialVersion.Exist(Id))
                {
                    return BadRequest("Version de material no existente");
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
        public ActionResult Insertar([FromBody] MaterialVersionDTO MaterialVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var materialVersion = new MaterialVersionBO()
                {
                    Nombre = MaterialVersion.Nombre,
                    Descripcion = MaterialVersion.Descripcion,
                    UsuarioCreacion = MaterialVersion.NombreUsuario,
                    UsuarioModificacion = MaterialVersion.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!materialVersion.HasErrors)
                {
                    _repMaterialVersion.Insert(materialVersion);
                }
                else
                {
                    return BadRequest(materialVersion.GetErrors());
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
        public ActionResult Actualizar([FromBody] MaterialVersionDTO MaterialVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialVersion.Exist(MaterialVersion.Id))
                {
                    return BadRequest("Version de material no existente!");
                }
                var materialVersion = _repMaterialVersion.FirstById(MaterialVersion.Id);
                materialVersion.Nombre = MaterialVersion.Nombre;
                materialVersion.Descripcion = MaterialVersion.Descripcion;
                materialVersion.UsuarioModificacion = MaterialVersion.NombreUsuario;
                materialVersion.FechaModificacion = DateTime.Now;
                if (!materialVersion.HasErrors)
                {
                    _repMaterialVersion.Update(materialVersion);
                }
                else
                {
                    return BadRequest(materialVersion.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialVersion.Exist(MaterialVersion.Id))
                {
                    return BadRequest("Version de material no existente");
                }
                _repMaterialVersion.Delete(MaterialVersion.Id, MaterialVersion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoServicio")]
    public class TipoServicioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TipoServicioRepositorio _repTipoServicio;
        public TipoServicioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repTipoServicio = new TipoServicioRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                return Ok(_repTipoServicio.ObtenerTodoFiltro());
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
                return Ok(_repTipoServicio.Obtener());
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
                if (!_repTipoServicio.Exist(Id))
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
        public ActionResult Insertar([FromBody] TipoServicioDTO TipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var tipoServicio = new TipoServicioBO()
                {
                    Nombre = TipoServicio.Nombre,
                    Descripcion = TipoServicio.Descripcion,
                    UsuarioCreacion = TipoServicio.NombreUsuario,
                    UsuarioModificacion = TipoServicio.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                if (!tipoServicio.HasErrors)
                {
                    _repTipoServicio.Insert(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult Actualizar([FromBody] TipoServicioDTO TipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repTipoServicio.Exist(TipoServicio.Id))
                {
                    return BadRequest("Tipo servicio no existente!");
                }
                var tipoServicio = _repTipoServicio.FirstById(TipoServicio.Id);
                tipoServicio.Nombre = TipoServicio.Nombre;
                tipoServicio.Descripcion = TipoServicio.Descripcion;
                tipoServicio.UsuarioModificacion = TipoServicio.NombreUsuario;
                tipoServicio.FechaModificacion = DateTime.Now;
                if (!tipoServicio.HasErrors)
                {
                    _repTipoServicio.Update(tipoServicio);
                }
                else
                {
                    return BadRequest(tipoServicio.GetErrors());
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
        public ActionResult Eliminar([FromBody] EliminarDTO TipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repTipoServicio.Exist(TipoServicio.Id))
                {
                    return BadRequest("Tipo servicio no existente");
                }
                _repTipoServicio.Delete(TipoServicio.Id, TipoServicio.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

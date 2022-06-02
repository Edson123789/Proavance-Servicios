using System;
using System.Linq;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: AreaTrabajoController
    /// Autor: ----------------
    /// Fecha: 04/03/2021
    /// <summary>
    /// Area Trabajo
    /// </summary>
    [Route("api/AreaTrabajo")]
    public class AreaTrabajoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public AreaTrabajoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Area Trabajo Filtro
        /// </summary>
        /// <returns>Objeto<returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerAreaTrabajo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaTrabajoRepositorio repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                return Ok(repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarAreaTrabajo([FromBody] AreaTrabajoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                AreaTrabajoBO NuevaAreaTrabajo = new AreaTrabajoBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _repAreaTrabajo.Insert(NuevaAreaTrabajo);
                return Ok(NuevaAreaTrabajo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAreaTrabajo([FromBody] AreaTrabajoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                AreaTrabajoBO AreaTrabajo = _repAreaTrabajo.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                AreaTrabajo.Nombre = ObjetoDTO.Nombre;
                AreaTrabajo.Estado = true;
                AreaTrabajo.UsuarioModificacion = ObjetoDTO.Usuario;
                AreaTrabajo.FechaModificacion = DateTime.Now;
                _repAreaTrabajo.Update(AreaTrabajo);
                return Ok(AreaTrabajo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAreaTrabajo([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaTrabajoRepositorio _repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                AreaTrabajoBO AreaTrabajo = _repAreaTrabajo.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repAreaTrabajo.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

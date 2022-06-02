using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroTiempoInactivo")]
    [ApiController]
    public class MaestroTiempoInactivoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MensajeTiempoInactivoRepositorio _repMensajeTiempoInactivo;

        public MaestroTiempoInactivoController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repMensajeTiempoInactivo = new MensajeTiempoInactivoRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerMensajeTiempoInactivo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaMensajeTiempoInactivo = _repMensajeTiempoInactivo.GetBy(x => x.Estado == true, x => new { x.Id, Mensaje = x.Mensaje, MinutoInactivo = x.MinutoInactivo, Usuario = x.UsuarioCreacion }).ToList();
                  
                return Ok(listaMensajeTiempoInactivo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarMensajeTiempoInactivo([FromBody]MensajeTiempoInactivoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MensajeTiempoInactivoBO tiempoInactivo = new MensajeTiempoInactivoBO()
                {
                    Mensaje = obj.Mensaje,
                    MinutoInactivo= obj.MinutoInactivo,
                    Estado = true,
                    UsuarioCreacion = obj.Usuario,
                    UsuarioModificacion = obj.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var res = _repMensajeTiempoInactivo.Insert(tiempoInactivo);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarMensajeTiempoInactivo([FromBody]MensajeTiempoInactivoDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var MensajeTiempo = _repMensajeTiempoInactivo.FirstById(obj.Id);
                MensajeTiempo.Mensaje = obj.Mensaje;
                MensajeTiempo.MinutoInactivo = obj.MinutoInactivo;
                MensajeTiempo.UsuarioModificacion = obj.Usuario;
                MensajeTiempo.FechaModificacion = DateTime.Now;
                var res = _repMensajeTiempoInactivo.Update(MensajeTiempo);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarMensajeTiempoInactivo([FromBody]EliminarDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repMensajeTiempoInactivo.Exist(obj.Id))
                {
                    var res = _repMensajeTiempoInactivo.Delete(obj.Id, obj.NombreUsuario);
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

    }
}
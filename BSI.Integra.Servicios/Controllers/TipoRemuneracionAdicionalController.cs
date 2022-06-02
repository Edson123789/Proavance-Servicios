using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoRemuneracionAdicional")]
    public class TipoRemuneracionAdicionalController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public TipoRemuneracionAdicionalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTipoRemuneracionAdicional()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRemuneracionAdicionalRepositorio _repTipoRemuneracionAdicionalRepositorio = new TipoRemuneracionAdicionalRepositorio(_integraDBContext);
                return Ok(new { Data = _repTipoRemuneracionAdicionalRepositorio.ObtenerTipoRemuneracionAdicional() });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult FiltroTipoRemuneracionAdicional()
        {
            try
            {
                TipoRemuneracionAdicionalRepositorio _repFiltroTipoRemuneracionAdicional = new TipoRemuneracionAdicionalRepositorio(_integraDBContext);
                return Ok(_repFiltroTipoRemuneracionAdicional.FiltroTipoRemuneracionAdicional());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarTipoRemuneracionAdicional([FromBody] TipoRemuneracionAdicionalDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRemuneracionAdicionalRepositorio _repTipoRemuneracionAdicional = new TipoRemuneracionAdicionalRepositorio(_integraDBContext);

                TipoRemuneracionAdicionalBO NuevaTipoRemuneracionAdicional = new TipoRemuneracionAdicionalBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Visualizar = ObjetoDTO.Visualizar,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                _repTipoRemuneracionAdicional.Insert(NuevaTipoRemuneracionAdicional);
                
                return Ok(NuevaTipoRemuneracionAdicional);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarTipoRemuneracionAdicional([FromBody] TipoRemuneracionAdicionalDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRemuneracionAdicionalRepositorio _repTipoRemuneracionAdicional = new TipoRemuneracionAdicionalRepositorio(_integraDBContext);

                TipoRemuneracionAdicionalBO TipoRemuneracionAdicional = _repTipoRemuneracionAdicional.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                TipoRemuneracionAdicional.Nombre = ObjetoDTO.Nombre;
                TipoRemuneracionAdicional.Visualizar = ObjetoDTO.Visualizar;
                TipoRemuneracionAdicional.Estado = true;
                TipoRemuneracionAdicional.UsuarioModificacion = ObjetoDTO.Usuario;
                TipoRemuneracionAdicional.FechaModificacion = DateTime.Now;

                _repTipoRemuneracionAdicional.Update(TipoRemuneracionAdicional);

                return Ok(TipoRemuneracionAdicional);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{UserName}/{Id}")]
        [HttpPost]
        public ActionResult EliminarTipoRemuneracionAdicional(int Id, string UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRemuneracionAdicionalRepositorio _repTipoRemuneracionAdicional = new TipoRemuneracionAdicionalRepositorio(_integraDBContext);
                TipoRemuneracionAdicionalBO TipoRemuneracionAdicional = _repTipoRemuneracionAdicional.GetBy(x => x.Id == Id).FirstOrDefault();
                _repTipoRemuneracionAdicional.Delete(Id, UserName);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

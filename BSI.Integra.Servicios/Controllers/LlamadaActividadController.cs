using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/LlamadaActividad")]
    [ApiController]
    public class LlamadaActividadController : ControllerBase
    {
        // GET: api/LlamadaActividad
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LlamadaActividad/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LlamadaActividad
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistroLlamadaActividad([FromBody] LlamadaActividadDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LlamadaActividadRepositorio _repoLlamadaActividad = new LlamadaActividadRepositorio();
                if (_repoLlamadaActividad.ValidarExisteLlamadaPorActividad(Obj.IdAsesor, Obj.IdActividadDetalle))
                {
                    LlamadaActividadBO LlamadaActividad = new LlamadaActividadBO();

                    LlamadaActividad.IdActividadDetalle = Obj.IdActividadDetalle;
                    LlamadaActividad.EstadoProgramado = false;
                    LlamadaActividad.Tag = Obj.Tag;
                    LlamadaActividad.IdAsesor = Obj.IdAsesor;
                    LlamadaActividad.FechaInicioLlamada = DateTime.Now;
                    LlamadaActividad.Estado = true;
                    LlamadaActividad.FechaCreacion = DateTime.Now;
                    LlamadaActividad.FechaModificacion = DateTime.Now;
                    LlamadaActividad.UsuarioCreacion = Obj.UsuarioCreacion;
                    LlamadaActividad.UsuarioModificacion = Obj.UsuarioCreacion;
                    LlamadaActividad.IdAgendaTab = Obj.IdAgendaTab;
                    if (LlamadaActividad.HasErrors)
                    {
                        return BadRequest(LlamadaActividad.GetErrors(null));
                    }
                    _repoLlamadaActividad.Insert(LlamadaActividad);
                }
                return Ok();
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        // PUT: api/LlamadaActividad/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

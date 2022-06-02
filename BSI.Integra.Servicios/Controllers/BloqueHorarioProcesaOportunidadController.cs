using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/BloqueHorarioProcesaOportunidad")]
    public class BloqueHorarioProcesaOportunidadController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public BloqueHorarioProcesaOportunidadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDiaSemana()
        {
            try
            {
                DiaSemanaRepositorio _repDiaSemanaRep = new DiaSemanaRepositorio();
                return Ok(_repDiaSemanaRep.ObtenerDiaSemana());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoBloqueHorarioProcesaOportunidad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio();
                return Ok(_repBloqueHorarioProcesaOportunidad.ObtenerTodoParaGrilla());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarBloqueHorarioProcesaOportunidad([FromBody] BloqueHorarioProcesaOportunidadDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio();
                BloqueHorarioProcesaOportunidadBO BloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadBO();

                BloqueHorarioProcesaOportunidad.Activo = false;
                BloqueHorarioProcesaOportunidad.Descripcion = "ninguna";
                BloqueHorarioProcesaOportunidad.Sede = "Arequipa";
                BloqueHorarioProcesaOportunidad.Dia = RequestDTO.NombreDiaSemana;
                BloqueHorarioProcesaOportunidad.IdDiaSemana = RequestDTO.IdDiaSemana;
                BloqueHorarioProcesaOportunidad.TurnoM = RequestDTO.TurnoM;
                BloqueHorarioProcesaOportunidad.TurnoT = RequestDTO.TurnoT;
                BloqueHorarioProcesaOportunidad.HoraInicioM = RequestDTO.HoraInicioM;
                BloqueHorarioProcesaOportunidad.HoraFinM = RequestDTO.HoraFinM;
                BloqueHorarioProcesaOportunidad.HoraInicioT = RequestDTO.HoraInicioT;
                BloqueHorarioProcesaOportunidad.HoraFinT = RequestDTO.HoraFinT;
                BloqueHorarioProcesaOportunidad.ProbabilidadOportunidad = "Muy Alta,Alta,Media,Sin Probabilidad"; //RequestDTO.ProbabilidadOportunidad;
                BloqueHorarioProcesaOportunidad.Prelanzamiento = false;
                BloqueHorarioProcesaOportunidad.Estado = true;
                BloqueHorarioProcesaOportunidad.UsuarioCreacion = RequestDTO.NombreUsuario;
                BloqueHorarioProcesaOportunidad.UsuarioModificacion = RequestDTO.NombreUsuario;
                BloqueHorarioProcesaOportunidad.FechaCreacion = DateTime.Now;
                BloqueHorarioProcesaOportunidad.FechaModificacion = DateTime.Now;


                _repBloqueHorarioProcesaOportunidad.Insert(BloqueHorarioProcesaOportunidad);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarBloqueHorarioProcesaOportunidad([FromBody] BloqueHorarioProcesaOportunidadDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad = new BloqueHorarioProcesaOportunidadRepositorio();
                BloqueHorarioProcesaOportunidadBO BloqueHorarioProcesaOportunidad = _repBloqueHorarioProcesaOportunidad.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();

                BloqueHorarioProcesaOportunidad.Dia = RequestDTO.NombreDiaSemana;
                BloqueHorarioProcesaOportunidad.IdDiaSemana = RequestDTO.IdDiaSemana;
                BloqueHorarioProcesaOportunidad.TurnoM = RequestDTO.TurnoM;
                BloqueHorarioProcesaOportunidad.TurnoT = RequestDTO.TurnoT;
                BloqueHorarioProcesaOportunidad.HoraInicioM = RequestDTO.HoraInicioM;
                BloqueHorarioProcesaOportunidad.HoraFinM = RequestDTO.HoraFinM;
                BloqueHorarioProcesaOportunidad.HoraInicioT = RequestDTO.HoraInicioT;
                BloqueHorarioProcesaOportunidad.HoraFinT = RequestDTO.HoraFinT;
                //BloqueHorarioProcesaOportunidad.ProbabilidadOportunidad = RequestDTO.ProbabilidadOportunidad; // Marketing informa que no tiene sentido elejir probabilidades ya que siempre se deberia procesar todo sin distincion
                BloqueHorarioProcesaOportunidad.UsuarioModificacion = RequestDTO.NombreUsuario;
                BloqueHorarioProcesaOportunidad.FechaModificacion = DateTime.Now;


                _repBloqueHorarioProcesaOportunidad.Update(BloqueHorarioProcesaOportunidad);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarBloqueHorarioProcesaOportunidad([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioProcesaOportunidadRepositorio _repBloqueHorarioProcesaOportunidad  = new BloqueHorarioProcesaOportunidadRepositorio();
                return Ok(_repBloqueHorarioProcesaOportunidad.Delete(Eliminar.Id, Eliminar.NombreUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

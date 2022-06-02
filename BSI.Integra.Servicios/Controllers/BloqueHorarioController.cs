using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/BloqueHorario")]
    public class BloqueHorarioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public BloqueHorarioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoBloqueHorario()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioRepositorio _repBloqueHorario= new BloqueHorarioRepositorio();
                return Ok(_repBloqueHorario.GetAll().ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarBloqueHorario([FromBody] BloqueHorarioDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioRepositorio _repBloqueHorario = new BloqueHorarioRepositorio();
                BloqueHorarioBO BloqueHorario = new BloqueHorarioBO() {
                    Nombre = RequestDTO.Nombre,
                    Descripcion = RequestDTO.Descripcion,
                    HoraInicio = RequestDTO.HoraInicio,
                    HoraFin = RequestDTO.HoraFin,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = RequestDTO.NombreUsuario,
                    UsuarioModificacion = RequestDTO.NombreUsuario
                };

                _repBloqueHorario.Insert(BloqueHorario);
                return Ok(BloqueHorario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarBloqueHorario([FromBody] BloqueHorarioDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioRepositorio _repBloqueHorario = new BloqueHorarioRepositorio();
                BloqueHorarioBO BoqueHorario = _repBloqueHorario.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();
                BoqueHorario.Nombre = RequestDTO.Nombre;
                BoqueHorario.Descripcion = RequestDTO.Descripcion;
                BoqueHorario.HoraInicio = RequestDTO.HoraInicio;
                BoqueHorario.HoraFin = RequestDTO.HoraFin;
                BoqueHorario.FechaModificacion = DateTime.Now;
                BoqueHorario.UsuarioModificacion = RequestDTO.NombreUsuario;

                _repBloqueHorario.Update(BoqueHorario);
                return Ok(BoqueHorario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarBloqueHorario([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                BloqueHorarioRepositorio _repBloqueHorario = new BloqueHorarioRepositorio();
                return Ok(_repBloqueHorario.Delete(Eliminar.Id, Eliminar.NombreUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}

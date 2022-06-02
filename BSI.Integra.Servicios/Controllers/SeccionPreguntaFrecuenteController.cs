using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SeccionPreguntaFrecuente")]
    public class SeccionPreguntaFrecuenteController : BaseController<TSeccionPreguntaFrecuente, ValidadoSeccionPreguntaFrecuenteDTO>
    {
        public SeccionPreguntaFrecuenteController(IIntegraRepository<TSeccionPreguntaFrecuente> repositorio, ILogger<BaseController<TSeccionPreguntaFrecuente, ValidadoSeccionPreguntaFrecuenteDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPreguntaFrecuenteRepositorio _repSeccionPreguntaFrecuente = new SeccionPreguntaFrecuenteRepositorio();
                return Ok(_repSeccionPreguntaFrecuente.ObtenerSeccionPreguntaFrecuenteFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] SeccionPreguntaFrecuenteDTO seccionPreguntaFrecuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPreguntaFrecuenteRepositorio _repSeccionPreguntaFrecuente = new SeccionPreguntaFrecuenteRepositorio();

                SeccionPreguntaFrecuenteBO seccionPreguntaFrecuenteBO = new SeccionPreguntaFrecuenteBO();
                seccionPreguntaFrecuenteBO.Nombre = seccionPreguntaFrecuente.Nombre;
                seccionPreguntaFrecuenteBO.Estado = true;
                seccionPreguntaFrecuenteBO.UsuarioCreacion = seccionPreguntaFrecuente.Usuario;
                seccionPreguntaFrecuenteBO.UsuarioModificacion = seccionPreguntaFrecuente.Usuario;
                seccionPreguntaFrecuenteBO.FechaCreacion = DateTime.Now;
                seccionPreguntaFrecuenteBO.FechaModificacion = DateTime.Now;

                return Ok(_repSeccionPreguntaFrecuente.Insert(seccionPreguntaFrecuenteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] SeccionPreguntaFrecuenteDTO seccionPreguntaFrecuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SeccionPreguntaFrecuenteRepositorio _repSeccionPreguntaFrecuente = new SeccionPreguntaFrecuenteRepositorio();

                SeccionPreguntaFrecuenteBO seccionPreguntaFrecuenteBO = _repSeccionPreguntaFrecuente.FirstById(seccionPreguntaFrecuente.Id);
                seccionPreguntaFrecuenteBO.Nombre = seccionPreguntaFrecuente.Nombre;
                seccionPreguntaFrecuenteBO.UsuarioModificacion = seccionPreguntaFrecuente.Usuario;
                seccionPreguntaFrecuenteBO.FechaModificacion = DateTime.Now;

                return Ok(_repSeccionPreguntaFrecuente.Update(seccionPreguntaFrecuenteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] SeccionPreguntaFrecuenteDTO seccionPreguntaFrecuente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SeccionPreguntaFrecuenteRepositorio _repSeccionPreguntaFrecuente = new SeccionPreguntaFrecuenteRepositorio();

                    if (_repSeccionPreguntaFrecuente.Exist(seccionPreguntaFrecuente.Id))
                    {
                        _repSeccionPreguntaFrecuente.Delete(seccionPreguntaFrecuente.Id, seccionPreguntaFrecuente.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class ValidadoSeccionPreguntaFrecuenteDTO : AbstractValidator<TSeccionPreguntaFrecuente>
    {
        public static ValidadoSeccionPreguntaFrecuenteDTO Current = new ValidadoSeccionPreguntaFrecuenteDTO();
        public ValidadoSeccionPreguntaFrecuenteDTO()
        {
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: /Periodo
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Contiene todos los controladores de periodos
    /// </summary>
    [Route("api/Periodo")]
    public class PeriodoController : BaseController<TPeriodo, ValidadorPeriodoDTO>
    {
        public PeriodoController(IIntegraRepository<TPeriodo> repositorio, ILogger<BaseController<TPeriodo, ValidadorPeriodoDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los periodos activos
        /// </summary>
        /// <returns>periodoRepositorio</returns>

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPeriodo()
        {
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                return Ok(_repPeriodo.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.FechaCreacion }).OrderByDescending(x => x.FechaCreacion).Select( x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                return Ok(_repPeriodo.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.FechaInicial,x.FechaFin, x.FechaInicialFinanzas, x.FechaFinFinanzas, x.FechaInicialRepIngresos, x.FechaFinRepIngresos }).OrderByDescending( x=>x.FechaInicial ));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] PeriodoDTO periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                PeriodoBO periodoBO = new PeriodoBO()
                {
                    Nombre = periodo.Nombre,
                    FechaInicial = periodo.FechaInicial,
                    FechaFin = periodo.FechaFin,
                    FechaInicialFinanzas = periodo.FechaInicialFinanzas,
                    FechaFinFinanzas = periodo.FechaFinFinanzas,
                    FechaInicialRepIngresos= periodo.FechaInicialRepIngresos,
                    FechaFinRepIngresos = periodo.FechaFinRepIngresos,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = periodo.NombreUsuario,
                    UsuarioModificacion = periodo.NombreUsuario
                };
                if (!periodoBO.HasErrors)
                {
                    _repPeriodo.Insert(periodoBO);
                }
                else {
                    return BadRequest(periodoBO.ActualesErrores);
                }
                return Ok(periodoBO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] PeriodoDTO periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                if (_repPeriodo.Exist(periodo.Id))
                {
                    var periodoBO = _repPeriodo.FirstById(periodo.Id);
                    periodoBO.Nombre = periodo.Nombre;
                    periodoBO.FechaInicial = periodo.FechaInicial;
                    periodoBO.FechaFin = periodo.FechaFin;
                    periodoBO.FechaInicialFinanzas = periodo.FechaInicialFinanzas;
                    periodoBO.FechaFinFinanzas = periodo.FechaFinFinanzas;
                    periodoBO.FechaInicialRepIngresos = periodo.FechaInicialRepIngresos;
                    periodoBO.FechaFinRepIngresos = periodo.FechaFinRepIngresos;
                    periodoBO.Estado = true;
                    periodoBO.FechaModificacion = DateTime.Now;
                    periodoBO.UsuarioModificacion = periodo.NombreUsuario;
                    if (!periodoBO.HasErrors)
                    {
                        _repPeriodo.Update(periodoBO);
                    }
                    else
                    {
                        return BadRequest(periodoBO.ActualesErrores);
                    }
                    return Ok(periodoBO);
                }
                else {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                if (_repPeriodo.Exist(periodo.Id))
                {
                    _repPeriodo.Delete(periodo.Id, periodo.NombreUsuario);
                    return Ok(true);
                }
                else {
                    return BadRequest("Registro no existente");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorPeriodoDTO : AbstractValidator<TPeriodo>
    {
        public static ValidadorPeriodoDTO Current = new ValidadorPeriodoDTO();
        public ValidadorPeriodoDTO()
        {
            RuleFor(x => x.Nombre).NotNull().NotEmpty().WithMessage("Nombre es Obligatorio");
        }
    }
}
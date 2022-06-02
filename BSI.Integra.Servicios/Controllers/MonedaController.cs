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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: Finanzas/Moneda
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Obtiene y valida las monedas
    /// </summary>

    [Route("api/Moneda")]
    public class MonedaController : BaseController<TMoneda, ValidadorMonedaDTO>
    {
        public MonedaController(IIntegraRepository<TMoneda> repositorio, ILogger<BaseController<TMoneda, ValidadorMonedaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
        /// Tipo Función: GET 
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el id y nombre plural de las monedas
        /// </summary>
        /// <returns>Retorna la moneda y el id
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroMoneda()
        {
            try
            {
                MonedaRepositorio _repMoneda = new MonedaRepositorio();
                return Ok(_repMoneda.GetBy( x => x.Estado == true, x => new { x.Id, x.NombrePlural }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

    public class ValidadorMonedaDTO : AbstractValidator<TMoneda>
    {
        public static ValidadorMonedaDTO Current = new ValidadorMonedaDTO();
        public ValidadorMonedaDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 30).WithMessage("Nombre debe tener 1 caracter minimo y 30 maximo");

            RuleFor(objeto => objeto.NombreCorto).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 15).WithMessage("Descripcion debe tener 1 caracter minimo y 15 maximo");

            RuleFor(objeto => objeto.NombrePlural).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 20).WithMessage("Descripcion debe tener 1 caracter minimo y 20 maximo");

            RuleFor(objeto => objeto.Simbolo).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 10).WithMessage("Descripcion debe tener 1 caracter minimo y 10 maximo");

            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 10).WithMessage("Descripcion debe tener 1 caracter minimo y 10 maximo");

            RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("Descripcion es Obligatorio");

        }
    }
}

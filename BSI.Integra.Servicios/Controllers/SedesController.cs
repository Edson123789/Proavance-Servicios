using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Sedes")]
    public class SedesController : BaseController<TSede, ValidadorSedesDTO>
    {
        public SedesController(IIntegraRepository<TSede> repositorio, ILogger<BaseController<TSede, ValidadorSedesDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorSedesDTO : AbstractValidator<TSede>
    {
        public static ValidadorSedesDTO Current = new ValidadorSedesDTO();
        public ValidadorSedesDTO()
        {
            //RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
            //                                .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdPais).NotEmpty().WithMessage("IdPais es Obligatorio");

            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("Codigo es Obligatorio");

            RuleFor(objeto => objeto.IdCiudad).NotEmpty().WithMessage("IdCiudad es Obligatorio");

            //RuleFor(objeto => objeto.UserId).Length(1, 25).WithMessage("UserId debe ser mayor a 1 y menor a 25");



        }

    }
}

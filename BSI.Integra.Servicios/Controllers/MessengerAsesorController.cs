using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using BSI.Integra.Persistencia.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MessengerAsesor")]// Hace referecia a tabla principal MessengerAsesor
    public class MessengerAsesorController : BaseController<TMessengerAsesor, ValidadorMessengerAsesorDTO>
    {
        public MessengerAsesorController(IIntegraRepository<TMessengerAsesor> repositorio, ILogger<BaseController<TMessengerAsesor, ValidadorMessengerAsesorDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorMessengerAsesorDTO : AbstractValidator<TMessengerAsesor>
    {
        public static ValidadorMessengerAsesorDTO Current = new ValidadorMessengerAsesorDTO();
        public ValidadorMessengerAsesorDTO()
        {

            RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");

            RuleFor(objeto => objeto.ConteoClientesAsignados).NotEmpty().WithMessage("ConteoClientesAsignados es Obligatorio");

        }
    }
}

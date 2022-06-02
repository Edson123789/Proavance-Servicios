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
    [Route("api/MessengerHistorialAsesor")]
    public class MessengerHistorialAsesorController : BaseController<TMessengerHistorialAsesor, ValidadorMessengerHistorialAsesorDTO>
    {
        public MessengerHistorialAsesorController(IIntegraRepository<TMessengerHistorialAsesor> repositorio, ILogger<BaseController<TMessengerHistorialAsesor, ValidadorMessengerHistorialAsesorDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorMessengerHistorialAsesorDTO : AbstractValidator<TMessengerHistorialAsesor>
    {
        public static ValidadorMessengerHistorialAsesorDTO Current = new ValidadorMessengerHistorialAsesorDTO();
        public ValidadorMessengerHistorialAsesorDTO()
        {

            RuleFor(objeto => objeto.IdMessengerAsesorDetalle).NotEmpty().WithMessage("IdMessengerAsesorDetalle es Obligatorio");

            RuleFor(objeto => objeto.IdMessengerAsesor).NotEmpty().WithMessage("IdMessengerAsesor es Obligatorio");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MessengerUsuarioLog")]
    public class MessengerUsuarioLogController : BaseController<TMessengerUsuarioLog, ValidadorMessengerUsuarioLogDTO>
    {
        public MessengerUsuarioLogController(IIntegraRepository<TMessengerUsuarioLog> repositorio, ILogger<BaseController<TMessengerUsuarioLog, ValidadorMessengerUsuarioLogDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorMessengerUsuarioLogDTO : AbstractValidator<TMessengerUsuarioLog>
    {
        public static ValidadorMessengerUsuarioLogDTO Current = new ValidadorMessengerUsuarioLogDTO();
        public ValidadorMessengerUsuarioLogDTO()
        {

            RuleFor(objeto => objeto.IdMessengerUsuario).NotEmpty().WithMessage("IdMeseengerUsuario es Obligatorio");

            //RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");

            //RuleFor(objeto => objeto.Mensaje).NotEmpty().WithMessage("Mensaje es Obligatorio")
            //                                .Length(1, 100).WithMessage("Mensaje debe tener 1 caracter minimo y 100 maximo");

        }
    }
}

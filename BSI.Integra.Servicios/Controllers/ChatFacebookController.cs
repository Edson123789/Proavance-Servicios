using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;
using BSI.Integra.Aplicacion.Comercial;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Aplicacion.Transversal.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ChatFacebook")]// Hace referecia a tabla principal MessengerUsuario
    public class ChatFacebookController : BaseController<MessengerUsuarioBO, ValidadorMessengerUsuarioDTO>
    {
        public ChatFacebookController(IIntegraRepository<MessengerUsuarioBO> repositorio, ILogger<BaseController<MessengerUsuarioBO, ValidadorMessengerUsuarioDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorMessengerUsuarioDTO : AbstractValidator<MessengerUsuarioBO>
    {
        public static ValidadorMessengerUsuarioDTO Current = new ValidadorMessengerUsuarioDTO();
        public ValidadorMessengerUsuarioDTO()
        {

            RuleFor(objeto => objeto.Psid).NotEmpty().WithMessage("Psid es Obligatorio");
            RuleFor(objeto => objeto.Nombres).NotEmpty().WithMessage("Nombres es Obligatorio");
            RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");
            RuleFor(objeto => objeto.SeRespondio).NotEmpty().WithMessage("SeRespondio es Obligatorio");                                         

            //RuleFor(objeto => objeto.CodigoPais).NotEmpty().WithMessage("CodigoPais es Obligatorio");

        }

    }
}

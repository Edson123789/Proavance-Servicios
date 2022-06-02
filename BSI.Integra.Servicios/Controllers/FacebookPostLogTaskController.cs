using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FacebookPostLogTask")]
    public class FacebookPostLogTaskController : BaseController<TFacebookPostLogTask, ValidadorFacebookPostLogTaskDTO>
    {
        public FacebookPostLogTaskController(IIntegraRepository<TFacebookPostLogTask> repositorio, ILogger<BaseController<TFacebookPostLogTask, ValidadorFacebookPostLogTaskDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorFacebookPostLogTaskDTO : AbstractValidator<TFacebookPostLogTask>
    {
        public static ValidadorFacebookPostLogTaskDTO Current = new ValidadorFacebookPostLogTaskDTO();
        public ValidadorFacebookPostLogTaskDTO()
        {
            RuleFor(objeto => objeto.Message).NotEmpty().WithMessage("Message es Obligatorio");

            RuleFor(objeto => objeto.ResponseJson).NotEmpty().WithMessage("ResponseJson es Obligatorio");

        }
    }
}

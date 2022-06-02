using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ConfiguracionBIC")]
    public class ConfiguracionBICController : BaseController<TConfiguracionBic, ValidadorConfiguracionBICDTO>
    {
        public ConfiguracionBICController(IIntegraRepository<TConfiguracionBic> repositorio, ILogger<BaseController<TConfiguracionBic, ValidadorConfiguracionBICDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }
    }

    public class ValidadorConfiguracionBICDTO : AbstractValidator<TConfiguracionBic>
    {
        public static ValidadorConfiguracionBICDTO Current = new ValidadorConfiguracionBICDTO();
        public ValidadorConfiguracionBICDTO()
        {
            RuleFor(objeto => objeto.Dias).NotEmpty().WithMessage("Dias es Obligatorio")
                                            .NotNull().WithMessage("Dias no permite datos nulos");
            RuleFor(objeto => objeto.Llamadas).NotEmpty().WithMessage("Llamadas es Obligatorio")
                                            .NotNull().WithMessage("Llamadas no permite datos nulos");
            RuleFor(objeto => objeto.Aplica).NotEmpty().WithMessage("Aplica es Obligatorio")
                                            .NotNull().WithMessage("Aplica no permite datos nulos");

        }
    }
}

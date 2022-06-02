using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Log")]
    public class LogsController : BaseController<TLog, LogsValidatorDTO>
    {
        public LogsController(IIntegraRepository<TLog> repositorio, ILogger<BaseController<TLog, LogsValidatorDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

    }

    public class LogsValidatorDTO : AbstractValidator<TLog>
    {
        public static LogsValidatorDTO Current = new LogsValidatorDTO();
        public LogsValidatorDTO()
        {
            RuleFor(x => x.Usuario).NotNull().WithMessage("El Usuario no puede ser nulo");
            RuleFor(x => x.Maquina).NotNull().WithMessage("La Maquina no puede ser nulo");
            RuleFor(x => x.Ruta).NotNull().WithMessage("La Ruta no puede ser nulo");
            RuleFor(x => x.Parametros).NotNull().WithMessage("El Id no puede ser nulo");
            RuleFor(x => x.Mensaje).NotNull().Length(0, 100).WithMessage("El mensaje es requerido");
            RuleFor(x => x.Tipo).NotNull();
        }
    }
}

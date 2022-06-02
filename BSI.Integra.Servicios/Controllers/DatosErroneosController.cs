using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/DatosErrores")] //Este controlador hace referencia al BO principal AsignacionAutomaticaError
    public class DatosErroneosController : BaseController<TAsignacionAutomaticaError, ValidadorDatosErroneosDTO>
    {
        public DatosErroneosController(IIntegraRepository<TAsignacionAutomaticaError> repositorio, ILogger<BaseController<TAsignacionAutomaticaError, ValidadorDatosErroneosDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorDatosErroneosDTO : AbstractValidator<TAsignacionAutomaticaError>
    {
        public static ValidadorDatosErroneosDTO Current = new ValidadorDatosErroneosDTO();
        public ValidadorDatosErroneosDTO()
        {

            RuleFor(objeto => objeto.Campo).NotEmpty().WithMessage("Campo es Obligatorio")
                                            .NotNull().WithMessage("Campo es Obligatorio");
            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .NotNull().WithMessage("Descripcion es Obligatorio");
            RuleFor(objeto => objeto.IdContacto).NotEmpty().WithMessage("IdContacto es Obligatorio")
                                            .NotNull().WithMessage("IdContacto es Obligatorio");
            RuleFor(objeto => objeto.IdAsignacionAutomatica).NotEmpty().WithMessage("IdAsignacionAutomatica es Obligatorio")
                                            .NotNull().WithMessage("IdAsignacionAutomatica es Obligatorio");
            RuleFor(objeto => objeto.IdAsignacionAutomaticaTipoError).NotEmpty().WithMessage("IdTipoError es Obligatorio")
                                            .NotNull().WithMessage("IdTipoError es Obligatorio");

        }

    }
}

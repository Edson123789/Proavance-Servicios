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
    [Route("api/PageFbValue")]
    public class PageFbValueController : BaseController<TPageFbValue, ValidadorPageFbValueDTO>
    {
        public PageFbValueController(IIntegraRepository<TPageFbValue> repositorio, ILogger<BaseController<TPageFbValue, ValidadorPageFbValueDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorPageFbValueDTO : AbstractValidator<TPageFbValue>
    {
        public static ValidadorPageFbValueDTO Current = new ValidadorPageFbValueDTO();
        public ValidadorPageFbValueDTO()
        {
            
            RuleFor(objeto => objeto.Verb).NotEmpty().WithMessage("Verb es Obligatorio")
                                            .Length(1, 40).WithMessage("Verb debe tener 1 caracter minimo y 40 maximo");

            RuleFor(objeto => objeto.Content).NotEmpty().WithMessage("Content es Obligatorio");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TasaConversionHistoricaBeta")]
    public class TasaConversionHistoricaBetaController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TasaConversionHistoricaBetaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }

    public class ValidadorTasaConversionHistoricaBetaDTO : AbstractValidator<TTasaConversionHistoricaBeta>
    {
        public static ValidadorTasaConversionHistoricaBetaDTO Current = new ValidadorTasaConversionHistoricaBetaDTO();
        public ValidadorTasaConversionHistoricaBetaDTO()
        {
            RuleFor(x => x.IdAformacion).NotNull().WithMessage("Id formación es Obligatorio");
        }
    }
}
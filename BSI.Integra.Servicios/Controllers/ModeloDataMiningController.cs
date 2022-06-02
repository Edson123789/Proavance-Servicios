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
    [Route("api/ModeloDataMining")]
    public class ModeloDataMiningController : BaseController<TModeloDataMining, ValidadorModeloDataMiningDTO>
    {
        public ModeloDataMiningController(IIntegraRepository<TModeloDataMining> repositorio, ILogger<BaseController<TModeloDataMining, ValidadorModeloDataMiningDTO>> logger, IIntegraRepository<TLog> logrepositorio) : base(repositorio, logger, logrepositorio)
        {
        }
    }

    public class ValidadorModeloDataMiningDTO : AbstractValidator<TModeloDataMining>
    {
        public static ValidadorModeloDataMiningDTO Current = new ValidadorModeloDataMiningDTO();
        public ValidadorModeloDataMiningDTO()
        {
            RuleFor(x => x.Nombres).NotNull().NotEmpty().WithMessage("Nombres es Obligatorio");
        }
    }
}
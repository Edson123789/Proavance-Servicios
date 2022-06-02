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
    [Route("api/PreCalculadaCambioFase")] // Hace referecia a tabla principal PreCalculadaCambioFase
    public class PreCalculadaCambioFaseController : BaseController<TPreCalculadaCambioFase, ValidadorPreCalculadaCambioFaseDTO>
    {
        public PreCalculadaCambioFaseController(IIntegraRepository<TPreCalculadaCambioFase> repositorio, ILogger<BaseController<TPreCalculadaCambioFase, ValidadorPreCalculadaCambioFaseDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorPreCalculadaCambioFaseDTO : AbstractValidator<TPreCalculadaCambioFase>
    {
        public static ValidadorPreCalculadaCambioFaseDTO Current = new ValidadorPreCalculadaCambioFaseDTO();
        public ValidadorPreCalculadaCambioFaseDTO()
        {

            RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");
            RuleFor(objeto => objeto.IdCentroCosto).NotEmpty().WithMessage("IdCentroCosto es Obligatorio");
            RuleFor(objeto => objeto.IdFaseOportunidadOrigen).NotEmpty().WithMessage("IdFaseOportunidadOrigen es Obligatorio");
            RuleFor(objeto => objeto.IdFaseOportunidadDestino).NotEmpty().WithMessage("IdFaseOportunidadDestino es Obligatorio");
            RuleFor(objeto => objeto.IdTipoDato).NotEmpty().WithMessage("IdTipoDato es Obligatorio");
            RuleFor(objeto => objeto.IdOrigen).NotEmpty().WithMessage("IdOrigen es Obligatorio");
            RuleFor(objeto => objeto.IdCategoriaOrigen).NotEmpty().WithMessage("IdCategoriaOrigen es Obligatorio");

        }

    }
}

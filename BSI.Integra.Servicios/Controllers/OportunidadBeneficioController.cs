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
    [Route("api/OportunidadBeneficio")]
    public class OportunidadBeneficioController : BaseController<TOportunidadBeneficio, ValidadorOportunidadBeneficioDTO>
    {
        public OportunidadBeneficioController(IIntegraRepository<TOportunidadBeneficio> repositorio, ILogger<BaseController<TOportunidadBeneficio, ValidadorOportunidadBeneficioDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorOportunidadBeneficioDTO : AbstractValidator<TOportunidadBeneficio>
    {
        public static ValidadorOportunidadBeneficioDTO Current = new ValidadorOportunidadBeneficioDTO();
        public ValidadorOportunidadBeneficioDTO()
        {
            RuleFor(objeto => objeto.IdOportunidadCompetidor).NotEmpty().WithMessage("IdOportunidadCompetidor es Obligatorio")
                                                    .NotNull().WithMessage("IdOportunidadCompetidor no permite datos nulos");

            RuleFor(objeto => objeto.IdBeneficio).NotEmpty().WithMessage("IdBeneficio es Obligatorio")
                                                    .NotNull().WithMessage("IdBeneficio no permite datos nulos");
        }
    }
}

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
    [Route("api/OportunidadCompetidor")]
    public class OportunidadCompetidorController : BaseController<TOportunidadCompetidor, ValidadorOportunidadCompetidorDTO>
    {
        public OportunidadCompetidorController(IIntegraRepository<TOportunidadCompetidor> repositorio, ILogger<BaseController<TOportunidadCompetidor, ValidadorOportunidadCompetidorDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorOportunidadCompetidorDTO : AbstractValidator<TOportunidadCompetidor>
    {
        public static ValidadorOportunidadCompetidorDTO Current = new ValidadorOportunidadCompetidorDTO();
        public ValidadorOportunidadCompetidorDTO()
        {
            RuleFor(objeto => objeto.IdOportunidad).NotEmpty().WithMessage("IdOportunidad es Obligatorio")
                                                    .NotNull().WithMessage("IdOportunidad no permite datos nulos");

            RuleFor(objeto => objeto.OtroBeneficio).NotEmpty().WithMessage("OtroBeneficio es Obligatorio")
                                                    .NotNull().WithMessage("OtroBeneficio no permite datos nulos");
        }
    }
}

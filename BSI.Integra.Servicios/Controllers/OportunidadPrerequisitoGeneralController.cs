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
    [Route("api/OportunidadPrerequisitoGeneral")]
    public class OportunidadPrerequisitoGeneralController : BaseController<TOportunidadPrerequisitoGeneral, ValidadorOportunidadPrerequisitoGeneralDTO>
    {
        public OportunidadPrerequisitoGeneralController(IIntegraRepository<TOportunidadPrerequisitoGeneral> repositorio, ILogger<BaseController<TOportunidadPrerequisitoGeneral, ValidadorOportunidadPrerequisitoGeneralDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorOportunidadPrerequisitoGeneralDTO : AbstractValidator<TOportunidadPrerequisitoGeneral>
    {
        public static ValidadorOportunidadPrerequisitoGeneralDTO Current = new ValidadorOportunidadPrerequisitoGeneralDTO();
        public ValidadorOportunidadPrerequisitoGeneralDTO()
        {
            RuleFor(objeto => objeto.IdOportunidadCompetidor).NotEmpty().WithMessage("IdOportunidadCompetidor es Obligatorio")
                                                    .NotNull().WithMessage("IdOportunidadCompetidor no permite datos nulos");

            RuleFor(objeto => objeto.IdProgramaGeneralPrerequisito).NotEmpty().WithMessage("IdProgramaGeneralPrerequisito es Obligatorio")
                                                    .NotNull().WithMessage("IdProgramaGeneralPrerequisito no permite datos nulos");
        }
    }
}

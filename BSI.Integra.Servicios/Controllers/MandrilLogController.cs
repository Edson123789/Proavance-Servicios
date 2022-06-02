using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MandrilLog")] 
    public class MandrilLogController : BaseController<TMandrilLog, ValidarMandrilLogDTO>
    {
        public MandrilLogController(IIntegraRepository<TMandrilLog> repositorio, ILogger<BaseController<TMandrilLog, ValidarMandrilLogDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidarMandrilLogDTO : AbstractValidator<TMandrilLog>
    {
        public static ValidarMandrilLogDTO Current = new ValidarMandrilLogDTO();
        public ValidarMandrilLogDTO()
        {

        }
    }
}
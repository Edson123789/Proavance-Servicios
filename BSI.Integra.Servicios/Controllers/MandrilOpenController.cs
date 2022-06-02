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
    [Route("api/MandrilOpen")] 
    public class MandrilOpenController : BaseController<TMandrilOpen, ValidarMandrilOpenDTO>
    {
        public MandrilOpenController(IIntegraRepository<TMandrilOpen> repositorio, ILogger<BaseController<TMandrilOpen, ValidarMandrilOpenDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidarMandrilOpenDTO : AbstractValidator<TMandrilOpen>
    {
        public static ValidarMandrilOpenDTO Current = new ValidarMandrilOpenDTO();
        public ValidarMandrilOpenDTO()
        {

        }
    }
}
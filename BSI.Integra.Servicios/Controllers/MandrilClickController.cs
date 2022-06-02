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
    [Route("api/MandrilClick")] 
    public class MandrilClickController : BaseController<TMandrilClick, ValidarMandrilClickDTO>
    {
        public MandrilClickController(IIntegraRepository<TMandrilClick> repositorio, ILogger<BaseController<TMandrilClick, ValidarMandrilClickDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidarMandrilClickDTO : AbstractValidator<TMandrilClick>
    {
        public static ValidarMandrilClickDTO Current = new ValidarMandrilClickDTO();
        public ValidarMandrilClickDTO()
        {

        }
    }
}
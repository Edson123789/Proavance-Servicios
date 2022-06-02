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
using BSI.Integra.Aplicacion.Marketing.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MandrilEnvioCorreo")] 
    public class MandrilEnvioCorreoController : BaseController<TMandrilEnvioCorreo, ValidarMandrilEnvioCorreoDTO>
    {
        public MandrilEnvioCorreoController(IIntegraRepository<TMandrilEnvioCorreo> repositorio, ILogger<BaseController<TMandrilEnvioCorreo, ValidarMandrilEnvioCorreoDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

    }

    public class ValidarMandrilEnvioCorreoDTO : AbstractValidator<TMandrilEnvioCorreo>
    {
        public static ValidarMandrilEnvioCorreoDTO Current = new ValidarMandrilEnvioCorreoDTO();
        public ValidarMandrilEnvioCorreoDTO()
        {

        }
    }
}
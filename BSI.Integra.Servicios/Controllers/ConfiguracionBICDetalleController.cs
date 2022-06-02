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
    [Route("api/ConfiguracionBICDetalle")]// Hace referecia a tabla principal ConfiguracionBICDetalle
    public class ConfiguracionBICDetalleController : BaseController<TConfiguracionBicdetalle, ValidadorConfiguracionBICDetalleDTO>
    {
        public ConfiguracionBICDetalleController(IIntegraRepository<TConfiguracionBicdetalle> repositorio, ILogger<BaseController<TConfiguracionBicdetalle, ValidadorConfiguracionBICDetalleDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorConfiguracionBICDetalleDTO : AbstractValidator<TConfiguracionBicdetalle>
    {
        public static ValidadorConfiguracionBICDetalleDTO Current = new ValidadorConfiguracionBICDetalleDTO();
        public ValidadorConfiguracionBICDetalleDTO()
        {

            RuleFor(objeto => objeto.IdConfiguracionBic).NotEmpty().WithMessage("IdConfiguracionBic es Obligatorio");
            RuleFor(objeto => objeto.IdBloqueHorario).NotEmpty().WithMessage("IdBloqueHorario es Obligatorio");


        }

    }
}

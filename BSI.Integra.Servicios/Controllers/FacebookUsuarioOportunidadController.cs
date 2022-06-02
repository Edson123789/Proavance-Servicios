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
    [Route("api/FacebookUsuarioOportunidad")]
    public class FacebookUsuarioOportunidadController : BaseController<TFacebookUsuarioOportunidad, ValidadorFacebookUsuarioOportunidadDTO>
    {
        public FacebookUsuarioOportunidadController(IIntegraRepository<TFacebookUsuarioOportunidad> repositorio, ILogger<BaseController<TFacebookUsuarioOportunidad, ValidadorFacebookUsuarioOportunidadDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorFacebookUsuarioOportunidadDTO : AbstractValidator<TFacebookUsuarioOportunidad>
    {
        public static ValidadorFacebookUsuarioOportunidadDTO Current = new ValidadorFacebookUsuarioOportunidadDTO();
        public ValidadorFacebookUsuarioOportunidadDTO()
        {
            RuleFor(objeto => objeto.Psid).NotEmpty().WithMessage("Psid es Obligatorio")
                                            .Length(1, 40).WithMessage("Psid debe tener 1 caracter minimo y 40 maximo");

            RuleFor(objeto => objeto.IdOportunidad).NotEmpty().WithMessage("IdOportunidad es Obligatorio");

            RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");

        }
    }
}

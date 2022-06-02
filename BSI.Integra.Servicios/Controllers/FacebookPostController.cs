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
    [Route("api/FacebookPost")]
    public class FacebookPostController : BaseController<TFacebookPost, ValidadorFacebookPostDTO>
    {
        public FacebookPostController(IIntegraRepository<TFacebookPost> repositorio, ILogger<BaseController<TFacebookPost, ValidadorFacebookPostDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorFacebookPostDTO : AbstractValidator<TFacebookPost>
    {
        public static ValidadorFacebookPostDTO Current = new ValidadorFacebookPostDTO();
        public ValidadorFacebookPostDTO()
        {
            RuleFor(objeto => objeto.Link).NotEmpty().WithMessage("Link es Obligatorio")
                                            .Length(1, 500).WithMessage("Link debe tener 1 caracter minimo y 500 maximo");

            RuleFor(objeto => objeto.PermalinkUrl).NotEmpty().WithMessage("PermalinkUrl es Obligatorio")
                                            .Length(1, 500).WithMessage("PermalinkUrl debe tener 1 caracter minimo y 500 maximo");

            RuleFor(objeto => objeto.IdPostFacebook).NotEmpty().WithMessage("IdPostFacebook es Obligatorio");

            RuleFor(objeto => objeto.IdPgeneral).NotEmpty().WithMessage("IdPgeneral es Obligatorio");

            RuleFor(objeto => objeto.ConjuntoAnuncioIdFacebook).NotEmpty().WithMessage("ConjuntoAnuncioIdFacebook es Obligatorio");

            RuleFor(objeto => objeto.IdAnuncioFacebook).NotEmpty().WithMessage("IdAnuncioFacebook es Obligatorio");

        }
    }
}

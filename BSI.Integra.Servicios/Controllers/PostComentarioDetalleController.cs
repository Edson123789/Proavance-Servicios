using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.SCode.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    public class PostComentarioDetalleController : BaseController<TPostComentarioDetalle, ValidadorPostComentarioDetalleDTO>
    {
        public PostComentarioDetalleController(IIntegraRepository<TPostComentarioDetalle> repositorio, ILogger<BaseController<TPostComentarioDetalle, ValidadorPostComentarioDetalleDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorPostComentarioDetalleDTO : AbstractValidator<TPostComentarioDetalle>
    {
        public static ValidadorPostComentarioDetalleDTO Current = new ValidadorPostComentarioDetalleDTO();
        public ValidadorPostComentarioDetalleDTO()
        {

            RuleFor(objeto => objeto.IdCommentFacebook).NotEmpty().WithMessage("IdUsuario es Obligatorio");
            RuleFor(objeto => objeto.IdPostFacebook).NotEmpty().WithMessage("Nombres es Obligatorio");
            RuleFor(objeto => objeto.IdParent).NotEmpty().WithMessage("IdAsesor es Obligatorio");
            RuleFor(objeto => objeto.IdUsuarioFacebook).NotEmpty().WithMessage("Respuesta es Obligatorio");

            //RuleFor(objeto => objeto.CodigoPais).NotEmpty().WithMessage("CodigoPais es Obligatorio");

        }

    }
}

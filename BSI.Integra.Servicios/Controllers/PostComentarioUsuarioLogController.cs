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
    [Route("api/PostComentarioUsuarioLog")]
    public class PostComentarioUsuarioLogController : BaseController<TPostComentarioUsuarioLog, ValidadorPostComentarioUsuarioLogDTO>
    {
        public PostComentarioUsuarioLogController(IIntegraRepository<TPostComentarioUsuarioLog> repositorio, ILogger<BaseController<TPostComentarioUsuarioLog, ValidadorPostComentarioUsuarioLogDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorPostComentarioUsuarioLogDTO : AbstractValidator<TPostComentarioUsuarioLog>
    {
        public static ValidadorPostComentarioUsuarioLogDTO Current = new ValidadorPostComentarioUsuarioLogDTO();
        public ValidadorPostComentarioUsuarioLogDTO()
        {

            RuleFor(objeto => objeto.IdAreaCapacitacion).NotEmpty().WithMessage("IdAreaCapacitacion es Obligatorio");

            //RuleFor(objeto => objeto.CodigoPais).NotEmpty().WithMessage("CodigoPais es Obligatorio");

        }

    }
}

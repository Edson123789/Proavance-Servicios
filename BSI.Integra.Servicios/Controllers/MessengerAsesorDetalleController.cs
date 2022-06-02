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
    [Route("api/MessengerAsesorDetalle")]
    public class MessengerAsesorDetalleController : BaseController<TMessengerAsesorDetalle, ValidadorMessengerAsesorDetalleDTO>
    {
        public MessengerAsesorDetalleController(IIntegraRepository<TMessengerAsesorDetalle> repositorio, ILogger<BaseController<TMessengerAsesorDetalle, ValidadorMessengerAsesorDetalleDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorMessengerAsesorDetalleDTO : AbstractValidator<TMessengerAsesorDetalle>
    {
        public static ValidadorMessengerAsesorDetalleDTO Current = new ValidadorMessengerAsesorDetalleDTO();
        public ValidadorMessengerAsesorDetalleDTO()
        {
            RuleFor(objeto => objeto.IdMessengerAsesor).NotEmpty().WithMessage("IdMessengerAsesor es Obligatorio");
        }
    }
}

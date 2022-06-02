using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ChatAsesor")]
    public class ChatAsesorController : BaseController<TInteraccionChatIntegra, ValidadorChatAsesorDTO>
    {
        public ChatAsesorController(IIntegraRepository<TInteraccionChatIntegra> repositorio, ILogger<BaseController<TInteraccionChatIntegra, ValidadorChatAsesorDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }


    }

    //Validador del objeto
    public class ValidadorChatAsesorDTO : AbstractValidator<TInteraccionChatIntegra>
    {
        public static ValidadorChatAsesorDTO Current = new ValidadorChatAsesorDTO();
        public ValidadorChatAsesorDTO()
        {
            RuleFor(objeto => objeto.IdAlumno).NotNull().WithMessage("El Id de alumno no puede ser nulo")
                                              .NotEmpty().WithMessage("El Id de alumno debe ser vacio");

            //RuleFor(objeto => objeto.IdContactoPortalSegmento).GreaterThan(0).WithMessage("El Id contacto portal segmento debe ser mayor a 0");
            RuleFor(objeto => objeto.IdContactoPortalSegmento).NotNull().WithMessage("El Id contacto portal segmento no puede ser nulo");
            RuleFor(objeto => objeto.IdTipoInteraccion).GreaterThan(0).WithMessage("El Id tipo de interaccion debe ser mayor a 0");
            RuleFor(objeto => objeto.Ip).NotNull().NotEmpty().WithMessage("La ip no puede ser nula");
            RuleFor(objeto => objeto.Pais).NotNull().NotEmpty().WithMessage("El pais es obligatorio, no puede ser nulo");
        }
    }
}

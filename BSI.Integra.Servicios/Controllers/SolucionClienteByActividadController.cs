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
    [Route("api/SolucionClienteByActividad")]// Hace referecia a tabla principal SolucionClienteByActividad
    public class SolucionClienteByActividadController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public SolucionClienteByActividadController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }

    public class ValidadorSolucionClienteByActividadDTO : AbstractValidator<TSolucionClienteByActividad>
    {
        public static ValidadorSolucionClienteByActividadDTO Current = new ValidadorSolucionClienteByActividadDTO();
        public ValidadorSolucionClienteByActividadDTO()
        {

            RuleFor(objeto => objeto.IdOportunidad).NotEmpty().WithMessage("IdOportunidad es Obligatorio");
            RuleFor(objeto => objeto.IdActividadDetalle).NotEmpty().WithMessage("IdActividadDetalle es Obligatorio");
            RuleFor(objeto => objeto.IdCausa).NotEmpty().WithMessage("IdCausa es Obligatorio");
            RuleFor(objeto => objeto.IdPersonal).NotEmpty().WithMessage("IdPersonal es Obligatorio");
            RuleFor(objeto => objeto.IdProblemaCliente).NotEmpty().WithMessage("IdProblemaCliente es Obligatorio");

        }
    }

}

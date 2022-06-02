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
using BSI.Integra.Aplicacion.Transversal.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OportunidadLog")]
    public class OportunidadLogController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public OportunidadLogController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult RegularizarLogDuplicados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadLogRepositorio _repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext);
                var cantidad = _repOportunidadLog.CantidadMaximaLogDuplicados();
                int contador = 0;
                if (cantidad != 0)
                {
                    while (cantidad > contador)
                    {
                        _repOportunidadLog.EliminarLogDuplicados();
                        contador++;
                    }

                    _repOportunidadLog.ActualizarLogDuplicados();

                }     
                return Ok(true);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }

    public class ValidadorOportunidadLogDTO : AbstractValidator<TOportunidadLog>
    {
        public static ValidadorOportunidadLogDTO Current = new ValidadorOportunidadLogDTO();
        public ValidadorOportunidadLogDTO()
        {
            RuleFor(objeto => objeto.IdCentroCosto).NotEmpty().WithMessage("IdCentroCosto es Obligatorio")
                                                    .NotNull().WithMessage("IdCentroCosto no permite datos nulos");

            RuleFor(objeto => objeto.IdPersonalAsignado).NotEmpty().WithMessage("IdPersonalAsignado es Obligatorio")
                                                    .NotNull().WithMessage("IdPersonalAsignado no permite datos nulos");

            RuleFor(objeto => objeto.IdFaseOportunidad).NotEmpty().WithMessage("IdFaseOportunidad es Obligatorio")
                                                    .NotNull().WithMessage("IdFaseOportunidad no permite datos nulos");

            RuleFor(objeto => objeto.IdContacto).NotEmpty().WithMessage("IdContacto es Obligatorio")
                                                    .NotNull().WithMessage("IdContacto no permite datos nulos");

            RuleFor(objeto => objeto.IdTipoDato).NotEmpty().WithMessage("IdTipoDato es Obligatorio")
                                                    .NotNull().WithMessage("IdTipoDato no permite datos nulos");

            RuleFor(objeto => objeto.IdOrigen).NotEmpty().WithMessage("IdOrigen es Obligatorio")
                                                    .NotNull().WithMessage("IdOrigen no permite datos nulos");
        }
    }
}

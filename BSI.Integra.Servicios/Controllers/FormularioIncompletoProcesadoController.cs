using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FormularioIncompletoProcesadoController")]
    public class FormularioIncompletoProcesadoController : BaseController<TFormularioIncompletoProcesado, ValidadorFormularioIncompletoProcesadoDTO>
    {
        public FormularioIncompletoProcesadoController(IIntegraRepository<TFormularioIncompletoProcesado> repositorio, ILogger<BaseController<TFormularioIncompletoProcesado, ValidadorFormularioIncompletoProcesadoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }       
    }
    public class ValidadorFormularioIncompletoProcesadoDTO : AbstractValidator<TFormularioIncompletoProcesado>
    {
        public static ValidadorFormularioIncompletoProcesadoDTO Current = new ValidadorFormularioIncompletoProcesadoDTO();
        public ValidadorFormularioIncompletoProcesadoDTO()
        {
            RuleFor(objeto => objeto.Nombre1).NotNull().WithMessage("Nombre 1 no puede ser nulo");
            RuleFor(objeto => objeto.Nombre2).NotNull().WithMessage("Nombre 2 no puede ser nulo");
            RuleFor(objeto => objeto.ApellidoPaterno).NotNull().WithMessage("Apellido paterno no puede ser nulo");
            RuleFor(objeto => objeto.ApellidoMaterno).NotNull().WithMessage("Apellido materno no puede ser nulo");
        }
    }
}
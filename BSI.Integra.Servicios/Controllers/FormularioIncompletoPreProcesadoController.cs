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
    [Route("api/FormularioIncompletoPreProcesadoController")]
    public class FormularioIncompletoPreProcesadoController : BaseController<TFormularioIncompletoPreprocesado, ValidadorFormularioIncompletoPreProcesadoDTO>
    {
        public FormularioIncompletoPreProcesadoController(IIntegraRepository<TFormularioIncompletoPreprocesado> repositorio, ILogger<BaseController<TFormularioIncompletoPreprocesado, ValidadorFormularioIncompletoPreProcesadoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }
    public class ValidadorFormularioIncompletoPreProcesadoDTO : AbstractValidator<TFormularioIncompletoPreprocesado>
    {
        public static ValidadorFormularioIncompletoPreProcesadoDTO Current = new ValidadorFormularioIncompletoPreProcesadoDTO();
        public ValidadorFormularioIncompletoPreProcesadoDTO()
        {
            RuleFor(objeto => objeto.Nombres).NotNull().WithMessage("Nombres no puede ser nulo");
            RuleFor(objeto => objeto.Apellidos).NotNull().WithMessage("Apellidos no puede ser nulo");
        }
    }
}
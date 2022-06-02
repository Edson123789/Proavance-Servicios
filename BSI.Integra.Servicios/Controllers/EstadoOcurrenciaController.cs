using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/EstadoOcurrencia")]
    public class EstadoOcurrenciaController : BaseController<TEstadoOcurrencia, ValidadorEstadoOcurrenciaDTO>
    {
        public EstadoOcurrenciaController(IIntegraRepository<TEstadoOcurrencia> repositorio, ILogger<BaseController<TEstadoOcurrencia, ValidadorEstadoOcurrenciaDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorEstadoOcurrenciaDTO : AbstractValidator<TEstadoOcurrencia>
    {
        public static ValidadorEstadoOcurrenciaDTO Current = new ValidadorEstadoOcurrenciaDTO();
        public ValidadorEstadoOcurrenciaDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 100).WithMessage("Descripcion debe tener 1 caracter minimo y 100 maximo");

        }
    }
}

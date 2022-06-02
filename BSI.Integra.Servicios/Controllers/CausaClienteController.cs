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
    [Route("api/CausaCliente")] // Hace referecia a tabla principal CausaCliente
    public class CausaClienteController : BaseController<TCausaCliente, ValidadorCausaClienteDTO>
    {
        public CausaClienteController(IIntegraRepository<TCausaCliente> repositorio, ILogger<BaseController<TCausaCliente, ValidadorCausaClienteDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorCausaClienteDTO : AbstractValidator<TCausaCliente>
    {
        public static ValidadorCausaClienteDTO Current = new ValidadorCausaClienteDTO();
        public ValidadorCausaClienteDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 2000).WithMessage("Nombre debe tener 1 caracter minimo y 2000 maximo");
            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 2000).WithMessage("Descripcion debe tener 1 caracter minimo y 2000 maximo");

            RuleFor(objeto => objeto.IdProblemaCliente).NotEmpty().WithMessage("IdProblemaCliente es Obligatorio");

        }
    }

}

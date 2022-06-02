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
    [Route("api/ProblemaCliente")]
    public class ProblemaClienteController : BaseController<TProblemaCliente, ValidadorProblemaClienteDTO>
    {
        public ProblemaClienteController(IIntegraRepository<TProblemaCliente> repositorio, ILogger<BaseController<TProblemaCliente, ValidadorProblemaClienteDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorProblemaClienteDTO : AbstractValidator<TProblemaCliente>
    {
        public static ValidadorProblemaClienteDTO Current = new ValidadorProblemaClienteDTO();
        public ValidadorProblemaClienteDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .NotNull().WithMessage("Nombre no permite datos nulos")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");
            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .NotNull().WithMessage("Descripcion no permite datos nulos")
                                            .Length(1, 500).WithMessage("Descripcion debe tener 1 caracter minimo y 500 maximo");
        }
    }
}

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
    [Route("api/FeriadoEspecial")]// Hace referecia a tabla principal FeriadoEspecial
    public class FeriadoEspecial : BaseController<TFeriadoEspecial, ValidadorFeriadoEspecialDTO>
    {
        public FeriadoEspecial(IIntegraRepository<TFeriadoEspecial> repositorio, ILogger<BaseController<TFeriadoEspecial, ValidadorFeriadoEspecialDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorFeriadoEspecialDTO : AbstractValidator<TFeriadoEspecial>
    {
        public static ValidadorFeriadoEspecialDTO Current = new ValidadorFeriadoEspecialDTO();
        public ValidadorFeriadoEspecialDTO()
        {
            RuleFor(objeto => objeto.Motivo).NotEmpty().WithMessage("Motivo es Obligatorio")
                                            .Length(1, 500).WithMessage("Motivo debe tener 1 caracter minimo y 500 maximo");
            RuleFor(objeto => objeto.IdCiudad).NotEmpty().WithMessage("IdCiudad es Obligatorio")
                                            .NotNull().WithMessage("IdCiudad es Obligatorio");
        }
    }
}

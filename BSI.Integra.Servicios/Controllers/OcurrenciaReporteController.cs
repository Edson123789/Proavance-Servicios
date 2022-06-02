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
    [Route("api/OcurrenciaReporte")]
    public class OcurrenciaReporteController : BaseController<TOcurrenciaReporte, ValidadorOcurrenciaReporteDTO>
    {
        public OcurrenciaReporteController(IIntegraRepository<TOcurrenciaReporte> repositorio, ILogger<BaseController<TOcurrenciaReporte, ValidadorOcurrenciaReporteDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidadorOcurrenciaReporteDTO : AbstractValidator<TOcurrenciaReporte>
    {
        public static ValidadorOcurrenciaReporteDTO Current = new ValidadorOcurrenciaReporteDTO();
        public ValidadorOcurrenciaReporteDTO()
        {

            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.IdFaseOportunidad).NotEmpty().WithMessage("IdFaseOportunidad es Obligatorio");

            RuleFor(objeto => objeto.IdActividadCabecera).NotEmpty().WithMessage("IdActividadCabecera es Obligatorio");

            RuleFor(objeto => objeto.IdPlantillaSpeech).NotEmpty().WithMessage("IdPlantillaSpeech es Obligatorio");

            RuleFor(objeto => objeto.IdEstadoOcurrencia).NotEmpty().WithMessage("IdEstadoOcurrencia es Obligatorio");

            RuleFor(objeto => objeto.RequiereLlamada).NotEmpty().WithMessage("RequiereLlamada es Obligatorio")
                                                        .Length(1, 20).WithMessage("Nombre debe tener 1 caracter minimo y 20 maximo");

            RuleFor(objeto => objeto.Roles).NotEmpty().WithMessage("Roles es Obligatorio")
                                                      .Length(1, 50).WithMessage("Roles debe tener 1 caracter minimo y 50 maximo");

            RuleFor(objeto => objeto.Color).NotEmpty().WithMessage("Color es Obligatorio")
                                            .Length(1, 20).WithMessage("Color debe tener 1 caracter minimo y 20 maximo");


        }
    }
}

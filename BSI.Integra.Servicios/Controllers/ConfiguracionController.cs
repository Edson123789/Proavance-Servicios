using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.DTOs;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Configuraciones")]
    public class ConfiguracionController : BaseController<TConfiguracion, ValidadorConfiguracionDTO>
    {
        public ConfiguracionController( IIntegraRepository<TConfiguracion> repositorio, ILogger<BaseController<TConfiguracion, ValidadorConfiguracionDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

    }

    public class ValidadorConfiguracionDTO : AbstractValidator<TConfiguracion>
    {
        public static ValidadorConfiguracionDTO Current = new ValidadorConfiguracionDTO();
        public ValidadorConfiguracionDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 150).WithMessage("Nombre debe tener 1 caracter minimo y 150 maximo");
            RuleFor(objeto => objeto.Codigo).NotEmpty().WithMessage("El Codigo es Obligatorio")
                                            .Length(1, 200).WithMessage("Codigo debe tener 1 caracter minimo y 150 maximo");
            RuleFor(objeto => objeto.Valor).NotEmpty().WithMessage("El Valor es Obligatorio");

        }
    }
}

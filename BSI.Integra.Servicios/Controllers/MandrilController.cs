using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Marketing.BO;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Mandril")]
    public class MandrilController : BaseController<TMandril, ValidarMandrilDTO> 
    {
        public MandrilController(IIntegraRepository<TMandril> repositorio, ILogger<BaseController<TMandril, ValidarMandrilDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }
    }

    public class ValidarMandrilDTO : AbstractValidator<TMandril>
    {
        public static ValidarMandrilDTO Current = new ValidarMandrilDTO();
        public ValidarMandrilDTO()
        {

        }
    }
}
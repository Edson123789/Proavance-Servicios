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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PersonalHorario")]
    public class PersonalHorarioController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public PersonalHorarioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }

}

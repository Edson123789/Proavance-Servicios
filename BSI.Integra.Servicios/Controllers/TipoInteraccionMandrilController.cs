using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoInteraccionMandril")] 
    public class TipoInteraccionMandrilController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoInteraccionMandrilController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }

}
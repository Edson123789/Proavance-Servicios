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
    [Route("api/CalidadProcesamiento")]
    public class CalidadProcesamientoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public CalidadProcesamientoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}

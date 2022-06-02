using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ActividadDetalle")]
    public class ActividadDetalleController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ActividadDetalleController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }

}

using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RegistroArchivoStorage")]
    [ApiController]
    public class RegistroArchivoStorageController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly RegistroArchivoStorageRepositorio _repoRegistroArchivo;

        public RegistroArchivoStorageController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoRegistroArchivo = new RegistroArchivoStorageRepositorio(_integraDBContext);
        }
    }
}

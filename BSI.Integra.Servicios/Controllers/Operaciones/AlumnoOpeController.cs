using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Alumno")]
    [ApiController]
    public class AlumnoOpeController : Controller
    {

        [Route("[Action]/{CodigoAlumno}")]
        [HttpGet]
        public ActionResult ObtenerDatosAlumno(string CodigoAlumno)
        {
            try
            {
                RaAlumnoRepositorio _repAlumno = new RaAlumnoRepositorio();
                return Ok(_repAlumno.ObtenerAlumnoPorCodigoMatricula(CodigoAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

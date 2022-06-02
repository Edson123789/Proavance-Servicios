using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Coordinadora")]
    public class CoordinadoraOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                //return Ok (_repCoordinadora.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.NombreResumido, x.Usuario }));
                return Ok(null);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroCoordinadoraPresencial()
        {
            try
            {
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                return Ok(_repCoordinadora.ObtenerListadoCoordinadoraPresencial());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
  
}
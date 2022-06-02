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
    [Route("api/Operaciones/TipoMovilidad")]
    public class TipoMovilidadOpeController : Controller
    {

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltro()
        {
            try
            {
                RaTipoMovilidadRepositorio _repMovilidad= new RaTipoMovilidadRepositorio();
                return Ok(_repMovilidad.GetBy(x => x.Estado, x => new { x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FiltroSegmentoTipoContacto")]
    public class FiltroSegmentoTipoContactoController : Controller
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FiltroSegmentoTipoContactoRepositorio _repFiltroSegmentoTipoContactoRepositorio = new FiltroSegmentoTipoContactoRepositorio();
                return Ok(_repFiltroSegmentoTipoContactoRepositorio.ObtenerTodoFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}

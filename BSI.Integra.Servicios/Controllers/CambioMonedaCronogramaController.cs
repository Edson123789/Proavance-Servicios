using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CambioMonedaCronograma")]
    public class CambioMonedaCronogramaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CambioMonedaCronogramaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 18/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Retorna los de una matricula por el id alumno
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaPEspecificoPorAlumno([FromBody] FiltroCodigoMatriculaPEspecificoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                MatriculaCabeceraRepositorio _repCabeceraRepositorio = new MatriculaCabeceraRepositorio(_integraDBContext);
                return Ok(_repCabeceraRepositorio.ObtenerCodigoMatriculaPEspecificoPorAlumno(Filtro.IdAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

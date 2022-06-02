using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    public class GestionComunicacionesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PersonalAreaTrabajoRepositorio _repAreaTrabajo;
        private readonly PersonalRepositorio _repPersonal;


        public GestionComunicacionesController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var obj = new
                {
                    ListaAreaTrabajo = _repAreaTrabajo.ObtenerTodoFiltro(),

                };
                return Ok(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("[Action]")]
        public ActionResult GetPersonalAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    return Ok(_repPersonal.CargarPersonalAutoCompleteContrato(Filtros["valor"].ToString()));
                }
                else
                {
                    List<FiltroDTO> lista = new List<FiltroDTO>();
                    return Ok(lista);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }












    }
}

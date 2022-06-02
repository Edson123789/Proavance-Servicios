using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Comercial.BO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AgendaTab")]
    public class AgendaTabController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AgendaTabController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

       

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoAgendaTab()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                return Ok(_repAgendaTab.GetBy(x => x.Estado==true).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarAgendaTab([FromBody] AgendaTabDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                


                AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                AgendaTabBO AgendaTab = new AgendaTabBO();

                AgendaTab.Nombre = RequestDTO.Nombre;
                AgendaTab.CodigoAreaTrabajo = RequestDTO.CodigoAreaTrabajo;
                AgendaTab.VisualizarActividad = true;
                AgendaTab.CargarInformacionInicial = true;
                AgendaTab.Numeracion = 1;
                AgendaTab.ValidarFecha = true;
                AgendaTab.Estado = true;
                AgendaTab.UsuarioCreacion = RequestDTO.NombreUsuario;
                AgendaTab.UsuarioModificacion = RequestDTO.NombreUsuario;
                AgendaTab.FechaCreacion = DateTime.Now;
                AgendaTab.FechaModificacion = DateTime.Now;


                _repAgendaTab.Insert(AgendaTab);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAgendaTab([FromBody] AgendaTabDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                AgendaTabBO AgendaTab = _repAgendaTab.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();

                AgendaTab.Nombre = RequestDTO.Nombre;
                AgendaTab.CodigoAreaTrabajo = RequestDTO.CodigoAreaTrabajo;
                AgendaTab.UsuarioModificacion = RequestDTO.NombreUsuario;
                AgendaTab.FechaModificacion = DateTime.Now;

                _repAgendaTab.Update(AgendaTab);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAgendaTab([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                return Ok(_repAgendaTab.Delete(Eliminar.Id, Eliminar.NombreUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

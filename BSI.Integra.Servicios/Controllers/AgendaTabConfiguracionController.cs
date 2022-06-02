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
    [Route("api/AgendaTabConfiguracion")]
    public class AgendaTabConfiguracionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AgendaTabConfiguracionController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaTabsAgenda()
        {
            try
            {
                AgendaTabConfiguracionRepositorio _repAgendaTabConfiguracion = new AgendaTabConfiguracionRepositorio();
                return Ok(_repAgendaTabConfiguracion.ObtenerTabsSinConfigurar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoCategoriaOrigen()
        {
            try
            {
                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio();
                return Ok(_repTipoCategoriaOrigen.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaCategoriaOrigen()
        {
            try
            {
                CategoriaOrigenRepositorio _repCategoriaOrigen = new CategoriaOrigenRepositorio();
                return Ok(_repCategoriaOrigen.ObtenerTodoParaFitro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaTipoDato()
        {
            try
            {
                TipoDatoRepositorio _repTipoDato = new TipoDatoRepositorio();
                return Ok(_repTipoDato.ObtenerFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaFaseOportunidad()
        {
            try
            {
                FaseOportunidadRepositorio _repFaseOportunidad = new FaseOportunidadRepositorio();
                return Ok(_repFaseOportunidad.ObtenerFasesOportunidadFiltroCodigo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaEstadoOportunidad()
        {
            try
            {
                EstadoOportunidadRepositorio _repEstadoOportunidad = new EstadoOportunidadRepositorio();
                return Ok(_repEstadoOportunidad.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoAgendaTabConfiguracion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabConfiguracionRepositorio _repAgendaTabConfiguracion = new AgendaTabConfiguracionRepositorio();
                return Ok(_repAgendaTabConfiguracion.ObtenerTodoParaGrilla());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarAgendaTabConfiguracion([FromBody] AgendaTabConfiguracionDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                //AgendaTabBO AgendaTab = _repAgendaTab.GetBy(x => x.Id == RequestDTO.IdAgendaTab).FirstOrDefault();
                
                //if (AgendaTab == null)
                //{
                //    AgendaTab = new AgendaTabBO()
                //    {
                //        Nombre = RequestDTO.NombreAgendaTab,
                //        VisualizarActividad = true,
                //        CargarInformacionInicial = true,
                //        ValidarFecha =false,
                //        Numeracion = 1,
                //        Estado = true,
                //        UsuarioCreacion = RequestDTO.NombreUsuario,
                //        UsuarioModificacion = RequestDTO.NombreUsuario,
                //        FechaCreacion = DateTime.Now,
                //        FechaModificacion = DateTime.Now
                //    };
                //    _repAgendaTab.Insert(AgendaTab) ;
                //} else
                //{
                //    AgendaTab.Nombre = RequestDTO.NombreAgendaTab;
                //    AgendaTab.UsuarioModificacion = RequestDTO.NombreUsuario;
                //    AgendaTab.FechaModificacion = DateTime.Now;
                //    _repAgendaTab.Update(AgendaTab);
                //}
                //RequestDTO.IdAgendaTab = AgendaTab.Id;



                AgendaTabConfiguracionRepositorio _repAgendaTabConfiguracion = new AgendaTabConfiguracionRepositorio();
                AgendaTabConfiguracionBO AgendaTabConfiguracion = new AgendaTabConfiguracionBO();

                AgendaTabConfiguracion.IdAgendaTab = RequestDTO.IdAgendaTab;
                AgendaTabConfiguracion.IdTipoCategoriaOrigen = RequestDTO.ListaTipoCategoriaOrigen;
                AgendaTabConfiguracion.IdCategoriaOrigen = RequestDTO.ListaCategoriaOrigen;
                AgendaTabConfiguracion.IdTipoDato = RequestDTO.ListaTipoDato;
                AgendaTabConfiguracion.IdFaseOportunidad = RequestDTO.ListaFaseOportunidad;
                AgendaTabConfiguracion.IdEstadoOportunidad = RequestDTO.ListaEstadoOportunidad;
                AgendaTabConfiguracion.Probabilidad = RequestDTO.ListaProbabilidad;
                AgendaTabConfiguracion.VistaBaseDatos = "";
                AgendaTabConfiguracion.CamposVista = "";
                AgendaTabConfiguracion.Estado = true;
                AgendaTabConfiguracion.UsuarioCreacion = RequestDTO.NombreUsuario;
                AgendaTabConfiguracion.UsuarioModificacion = RequestDTO.NombreUsuario;
                AgendaTabConfiguracion.FechaCreacion = DateTime.Now;
                AgendaTabConfiguracion.FechaModificacion = DateTime.Now;


                _repAgendaTabConfiguracion.Insert(AgendaTabConfiguracion);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarAgendaTabConfiguracion([FromBody] AgendaTabConfiguracionDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabConfiguracionRepositorio _repAgendaTabConfiguracion = new AgendaTabConfiguracionRepositorio();
                AgendaTabConfiguracionBO AgendaTabConfiguracion = _repAgendaTabConfiguracion.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();

                //AgendaTabRepositorio _repAgendaTab = new AgendaTabRepositorio();
                //AgendaTabBO AgendaTab = _repAgendaTab.GetBy(x => x.Id == RequestDTO.IdAgendaTab).FirstOrDefault();

                //if (AgendaTab == null)
                //{
                //    throw new Exception("No se encontro el Tab en T_AgendaTab con Id=" + RequestDTO.Id);
                //}
                //else
                //{
                //    AgendaTab.Nombre = RequestDTO.NombreAgendaTab;
                //    AgendaTab.UsuarioModificacion = RequestDTO.NombreUsuario;
                //    AgendaTab.FechaModificacion = DateTime.Now;
                //    _repAgendaTab.Update(AgendaTab);
                //}

                AgendaTabConfiguracion.IdTipoCategoriaOrigen = RequestDTO.ListaTipoCategoriaOrigen;
                AgendaTabConfiguracion.IdCategoriaOrigen = RequestDTO.ListaCategoriaOrigen;
                AgendaTabConfiguracion.IdTipoDato = RequestDTO.ListaTipoDato;
                AgendaTabConfiguracion.IdFaseOportunidad = RequestDTO.ListaFaseOportunidad;
                AgendaTabConfiguracion.IdEstadoOportunidad = RequestDTO.ListaEstadoOportunidad;
                AgendaTabConfiguracion.Probabilidad = RequestDTO.ListaProbabilidad;
                AgendaTabConfiguracion.UsuarioModificacion = RequestDTO.NombreUsuario;
                AgendaTabConfiguracion.FechaModificacion = DateTime.Now;

                _repAgendaTabConfiguracion.Update(AgendaTabConfiguracion);
                return Ok(RequestDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAgendaTabConfiguracion([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AgendaTabConfiguracionRepositorio _repAgendaTabConfiguracion = new AgendaTabConfiguracionRepositorio();
                return Ok(_repAgendaTabConfiguracion.Delete(Eliminar.Id, Eliminar.NombreUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

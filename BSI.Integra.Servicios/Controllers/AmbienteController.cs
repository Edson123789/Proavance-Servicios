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
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Ambiente")]
    public class AmbienteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AmbienteController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                AmbienteRepositorio ambienteRepositorio = new AmbienteRepositorio(_integraDBContext);
                return Ok(ambienteRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                LocacionRepositorio _repLocacion = new LocacionRepositorio(_integraDBContext);
                TipoAmbienteRepositorio _repTipoAmbiente = new TipoAmbienteRepositorio(_integraDBContext);
                CombosAmbienteDTO combos = new CombosAmbienteDTO();

                combos.LocacionComboFiltro = _repLocacion.ObtenerLocacionFiltro();
                combos.TipoAmbienteFiltro = _repTipoAmbiente.ObtenerAmbienteFiltro();

                return Ok(combos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] AmbienteDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AmbienteRepositorio ambienteRepositorio = new AmbienteRepositorio(_integraDBContext);

                AmbienteBO ambienteBO = new AmbienteBO();
                ambienteBO.Nombre = Json.Nombre;
                ambienteBO.IdLocacion = Json.IdLocacion;
                ambienteBO.IdTipoAmbiente = Json.IdTipoAmbiente;
                ambienteBO.Capacidad = Json.Capacidad;
                ambienteBO.Virtual = Json.Virtual;
                ambienteBO.Estado = true;
                ambienteBO.UsuarioCreacion = Json.Usuario;
                ambienteBO.UsuarioModificacion = Json.Usuario;
                ambienteBO.FechaCreacion = DateTime.Now;
                ambienteBO.FechaModificacion = DateTime.Now;

                return Ok(ambienteRepositorio.Insert(ambienteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] AmbienteDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AmbienteRepositorio ambienteRepositorio = new AmbienteRepositorio(_integraDBContext);

                AmbienteBO ambienteBO = ambienteRepositorio.FirstById(Json.Id);
                ambienteBO.Nombre = Json.Nombre;
                ambienteBO.IdLocacion = Json.IdLocacion;
                ambienteBO.IdTipoAmbiente = Json.IdTipoAmbiente;
                ambienteBO.Capacidad = Json.Capacidad;
                ambienteBO.Virtual = Json.Virtual;
                ambienteBO.UsuarioModificacion = Json.Usuario;
                ambienteBO.FechaModificacion = DateTime.Now;

                return Ok(ambienteRepositorio.Update(ambienteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody]AmbienteDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AmbienteRepositorio ambienteRepositorio = new AmbienteRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ambienteRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = ambienteRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

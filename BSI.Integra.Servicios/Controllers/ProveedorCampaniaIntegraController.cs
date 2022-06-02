using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
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
using static BSI.Integra.Servicios.Controllers.ProveedorCampaniaIntegraController;
using static BSI.Integra.Servicios.Controllers.TerminoUsoSitioWebPwController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProveedorCampaniaIntegra")]
    [ApiController]
    public class ProveedorCampaniaIntegraController : BaseController <TProveedorCampaniaIntegra, ValidadorProveedorCampaniaIntegraDTO>
    {
        public ProveedorCampaniaIntegraController(IIntegraRepository<TProveedorCampaniaIntegra> repositorio, ILogger<BaseController<TProveedorCampaniaIntegra, ValidadorProveedorCampaniaIntegraDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                ProveedorCampaniaIntegraRepositorio proveedorCampaniaIntegraRepositorio = new ProveedorCampaniaIntegraRepositorio();
                return Ok(proveedorCampaniaIntegraRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ProveedorCampaniaIntegraDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorCampaniaIntegraRepositorio proveedorCampaniaIntegraRepositorio = new ProveedorCampaniaIntegraRepositorio();

                ProveedorCampaniaIntegraBO proveedorCampaniaIntegraBO = new ProveedorCampaniaIntegraBO();
                proveedorCampaniaIntegraBO.Nombre = Json.Nombre;
                proveedorCampaniaIntegraBO.PorDefecto = Json.PorDefecto;
                proveedorCampaniaIntegraBO.Estado = true;
                proveedorCampaniaIntegraBO.UsuarioCreacion = Json.Usuario;
                proveedorCampaniaIntegraBO.UsuarioModificacion = Json.Usuario;
                proveedorCampaniaIntegraBO.FechaCreacion = DateTime.Now;
                proveedorCampaniaIntegraBO.FechaModificacion = DateTime.Now;

                return Ok(proveedorCampaniaIntegraRepositorio.Insert(proveedorCampaniaIntegraBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ProveedorCampaniaIntegraDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorCampaniaIntegraRepositorio proveedorCampaniaIntegraRepositorio = new ProveedorCampaniaIntegraRepositorio();

                ProveedorCampaniaIntegraBO proveedorCampaniaIntegraBO = proveedorCampaniaIntegraRepositorio.FirstById(Json.Id);
                proveedorCampaniaIntegraBO.Nombre = Json.Nombre;
                proveedorCampaniaIntegraBO.PorDefecto = Json.PorDefecto;
                proveedorCampaniaIntegraBO.UsuarioModificacion = Json.Usuario;
                proveedorCampaniaIntegraBO.FechaModificacion = DateTime.Now;
                
                return Ok(proveedorCampaniaIntegraRepositorio.Update(proveedorCampaniaIntegraBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] ProveedorCampaniaIntegraDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorCampaniaIntegraRepositorio proveedorCampaniaIntegraRepositorio = new ProveedorCampaniaIntegraRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (proveedorCampaniaIntegraRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = proveedorCampaniaIntegraRepositorio.Delete(Json.Id, Json.Usuario);
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

        public class ValidadorProveedorCampaniaIntegraDTO : AbstractValidator<TProveedorCampaniaIntegra>
        {
            public static ValidadorProveedorCampaniaIntegraDTO Current = new ValidadorProveedorCampaniaIntegraDTO();
            public ValidadorProveedorCampaniaIntegraDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}

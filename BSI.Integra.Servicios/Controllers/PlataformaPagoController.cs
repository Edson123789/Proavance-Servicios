using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
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
using static BSI.Integra.Servicios.Controllers.PanelControlMetaController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlataformaPago")]
    [ApiController]
    public class PlataformaPagoController : BaseController<TPlataformaPago, ValidadorPlataformaPagoDTO>
    {
        public PlataformaPagoController(IIntegraRepository<TPlataformaPago> repositorio, ILogger<BaseController<TPlataformaPago, ValidadorPlataformaPagoDTO>> logger, IIntegraRepository<TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {

        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                PlataformaPagoRepositorio plataformaPagoRepositorio = new PlataformaPagoRepositorio();
                return Ok(plataformaPagoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(PlataformaPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlataformaPagoRepositorio plataformaPagoRepositorio = new PlataformaPagoRepositorio();

                PlataformaPagoBO plataformaPagoBO = new PlataformaPagoBO();
                plataformaPagoBO.Nombre = Json.Nombre;
                plataformaPagoBO.Descripcion = Json.Descripcion;
                plataformaPagoBO.Estado = true;
                plataformaPagoBO.UsuarioCreacion = Json.Usuario;
                plataformaPagoBO.UsuarioModificacion = Json.Usuario;
                plataformaPagoBO.FechaCreacion = DateTime.Now;
                plataformaPagoBO.FechaModificacion = DateTime.Now;

                return Ok(plataformaPagoRepositorio.Insert(plataformaPagoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(PlataformaPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlataformaPagoRepositorio plataformaPagoRepositorio = new PlataformaPagoRepositorio();

                PlataformaPagoBO plataformaPagoBO = plataformaPagoRepositorio.FirstById(Json.Id);
                plataformaPagoBO.Nombre = Json.Nombre;
                plataformaPagoBO.Descripcion = Json.Descripcion;
                plataformaPagoBO.UsuarioModificacion = Json.Usuario;
                plataformaPagoBO.FechaModificacion = DateTime.Now;
                
                return Ok(plataformaPagoRepositorio.Update(plataformaPagoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(PlataformaPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlataformaPagoRepositorio plataformaPagoRepositorio = new PlataformaPagoRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (plataformaPagoRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = plataformaPagoRepositorio.Delete(Json.Id, Json.Usuario);
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
    public class ValidadorPlataformaPagoDTO : AbstractValidator<TPlataformaPago>
    {
        public static ValidadorPlataformaPagoDTO Current = new ValidadorPlataformaPagoDTO();
        public ValidadorPlataformaPagoDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");
            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio");

        }
    }
}

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
    [Route("api/TipoInteraccion")]
    public class TipoInteraccionController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoInteraccionController(integraDBContext integraDBContext) 
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                TipoInteraccionRepositorio tipoInteraccionRepositorio = new TipoInteraccionRepositorio(_integraDBContext);
                return Ok(tipoInteraccionRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoInteraccionDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoInteraccionRepositorio tipoInteraccionRepositorio = new TipoInteraccionRepositorio(_integraDBContext);

                TipoInteraccionBO tipoInteraccionBO = new TipoInteraccionBO();
                tipoInteraccionBO.Nombre = Json.Nombre;
                tipoInteraccionBO.Canal = Json.Canal;
                tipoInteraccionBO.Estado = true;
                tipoInteraccionBO.UsuarioCreacion = Json.Usuario;
                tipoInteraccionBO.UsuarioModificacion = Json.Usuario;
                tipoInteraccionBO.FechaCreacion = DateTime.Now;
                tipoInteraccionBO.FechaModificacion = DateTime.Now;

                return Ok(tipoInteraccionRepositorio.Insert(tipoInteraccionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] TipoInteraccionDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoInteraccionRepositorio tipoInteraccionRepositorio = new TipoInteraccionRepositorio(_integraDBContext);

                TipoInteraccionBO tipoInteraccionBO = tipoInteraccionRepositorio.FirstById(Json.Id);
                tipoInteraccionBO.Nombre = Json.Nombre;
                tipoInteraccionBO.Canal= Json.Canal;
                tipoInteraccionBO.UsuarioModificacion = Json.Usuario;
                tipoInteraccionBO.FechaModificacion = DateTime.Now;

                return Ok(tipoInteraccionRepositorio.Update(tipoInteraccionBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody] TipoInteraccionDatosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoInteraccionRepositorio tipoInteraccionRepositorio = new TipoInteraccionRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (tipoInteraccionRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = tipoInteraccionRepositorio.Delete(Json.Id, Json.Usuario);
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

    public class ValidadorTipoInteracccionDTO : AbstractValidator<TTipoInteracccion>
    {
        public static ValidadorTipoInteracccionDTO Current = new ValidadorTipoInteracccionDTO();
        public ValidadorTipoInteracccionDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 50 maximo");

            RuleFor(objeto => objeto.Canal).NotEmpty().WithMessage("Canal es Obligatorio")
                                            .Length(1, 100).WithMessage("Canal debe tener 1 caracter minimo y 50 maximo");


        }

    }
}

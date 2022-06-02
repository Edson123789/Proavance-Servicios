using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoAmbiente")]
    public class TipoAmbienteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoAmbienteController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                TipoAmbienteRepositorio tipoAmbienteRepositorio = new TipoAmbienteRepositorio(_integraDBContext);
                return Ok(tipoAmbienteRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] TipoAmbienteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoAmbienteRepositorio tipoAmbienteRepositorio = new TipoAmbienteRepositorio(_integraDBContext);

                TipoAmbienteBO tipoAmbienteBO = new TipoAmbienteBO();
                tipoAmbienteBO.Nombre = Json.Nombre;
                tipoAmbienteBO.Descripcion = Json.Descripcion;
                tipoAmbienteBO.Estado = true;
                tipoAmbienteBO.UsuarioCreacion = Json.Usuario;
                tipoAmbienteBO.UsuarioModificacion = Json.Usuario;
                tipoAmbienteBO.FechaCreacion = DateTime.Now;
                tipoAmbienteBO.FechaModificacion = DateTime.Now;

                return Ok(tipoAmbienteRepositorio.Insert(tipoAmbienteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] TipoAmbienteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoAmbienteRepositorio tipoAmbienteRepositorio = new TipoAmbienteRepositorio(_integraDBContext);

                TipoAmbienteBO tipoAmbienteBO = tipoAmbienteRepositorio.FirstById(Json.Id);
                tipoAmbienteBO.Nombre = Json.Nombre;
                tipoAmbienteBO.Descripcion = Json.Descripcion;
                tipoAmbienteBO.UsuarioModificacion = Json.Usuario;
                tipoAmbienteBO.FechaModificacion = DateTime.Now;

                return Ok(tipoAmbienteRepositorio.Update(tipoAmbienteBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody] TipoAmbienteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoAmbienteRepositorio tipoAmbienteRepositorio = new TipoAmbienteRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (tipoAmbienteRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = tipoAmbienteRepositorio.Delete(Json.Id, Json.Usuario);
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

    public class ValidadorTipoAmbienteDTO : AbstractValidator<TTipoAmbiente>
    {
        public static ValidadorTipoAmbienteDTO Current = new ValidadorTipoAmbienteDTO();
        public ValidadorTipoAmbienteDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 100 maximo");

            RuleFor(objeto => objeto.Descripcion).NotEmpty().WithMessage("Descripcion es Obligatorio")
                                            .Length(1, 100).WithMessage("Descripcion debe tener 1 caracter minimo y 100 maximo");

        }

    }
}

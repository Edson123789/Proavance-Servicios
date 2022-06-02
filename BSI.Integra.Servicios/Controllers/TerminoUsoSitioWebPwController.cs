using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
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
using static BSI.Integra.Servicios.Controllers.TerminoUsoSitioWebPwController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TerminoUsoSitioWebPw")]
    [ApiController]
    public class TerminoUsoSitioWebPwController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TerminoUsoSitioWebPwController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                TerminoUsoSitioWebPwRepositorio terminoUsoSitioWebPwRepositorio = new TerminoUsoSitioWebPwRepositorio(_integraDBContext);
                return Ok(terminoUsoSitioWebPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(TerminoUsoSitioWebPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TerminoUsoSitioWebPwRepositorio terminoUsoSitioWebPwRepositorio = new TerminoUsoSitioWebPwRepositorio(_integraDBContext);

                TerminoUsoSitioWebPwBO terminoUsoSitioWebPwBO = new TerminoUsoSitioWebPwBO();
                terminoUsoSitioWebPwBO.CodigoIsopais = Json.CodigoIsopais;
                terminoUsoSitioWebPwBO.NombrePais = Json.NombrePais;
                terminoUsoSitioWebPwBO.Nombre = Json.Nombre;
                terminoUsoSitioWebPwBO.Contenido = Json.Contenido;
                terminoUsoSitioWebPwBO.Estado = true;
                terminoUsoSitioWebPwBO.UsuarioCreacion = Json.Usuario;
                terminoUsoSitioWebPwBO.UsuarioModificacion = Json.Usuario;
                terminoUsoSitioWebPwBO.FechaCreacion = DateTime.Now;
                terminoUsoSitioWebPwBO.FechaModificacion = DateTime.Now;

                return Ok(terminoUsoSitioWebPwRepositorio.Insert(terminoUsoSitioWebPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(TerminoUsoSitioWebPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TerminoUsoSitioWebPwRepositorio terminoUsoSitioWebPwRepositorio = new TerminoUsoSitioWebPwRepositorio(_integraDBContext);

                TerminoUsoSitioWebPwBO terminoUsoSitioWebPwBO = terminoUsoSitioWebPwRepositorio.FirstById(Json.Id);
                terminoUsoSitioWebPwBO.CodigoIsopais = Json.CodigoIsopais;
                terminoUsoSitioWebPwBO.NombrePais = Json.NombrePais;
                terminoUsoSitioWebPwBO.Nombre = Json.Nombre;
                terminoUsoSitioWebPwBO.Contenido = Json.Contenido;
                terminoUsoSitioWebPwBO.UsuarioModificacion = Json.Usuario;
                terminoUsoSitioWebPwBO.FechaModificacion = DateTime.Now;
                
                return Ok(terminoUsoSitioWebPwRepositorio.Update(terminoUsoSitioWebPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(TerminoUsoSitioWebPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TerminoUsoSitioWebPwRepositorio terminoUsoSitioWebPwRepositorio = new TerminoUsoSitioWebPwRepositorio(_integraDBContext);
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (terminoUsoSitioWebPwRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = terminoUsoSitioWebPwRepositorio.Delete(Json.Id, Json.Usuario);
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

        public class ValidadorTerminoUsoSitioWebPwDTO : AbstractValidator<TTerminoUsoSitioWebPw>
        {
            public static ValidadorTerminoUsoSitioWebPwDTO Current = new ValidadorTerminoUsoSitioWebPwDTO();
            public ValidadorTerminoUsoSitioWebPwDTO()
            {
                RuleFor(objeto => objeto.CodigoIsopais).NotEmpty().WithMessage("CodigoIsopais es Obligatorio");
                RuleFor(objeto => objeto.NombrePais).NotEmpty().WithMessage("NombrePais es Obligatorio");
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");
                RuleFor(objeto => objeto.Contenido).NotEmpty().WithMessage("Contenido es Obligatorio");

            }
        }
    }
}

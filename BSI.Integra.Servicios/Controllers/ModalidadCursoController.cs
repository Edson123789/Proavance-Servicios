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
    [Route("api/ModalidadCurso")]
    public class ModalidadCursoController : BaseController<TModalidadCurso, ValidadorModalidadCursoDTO>
    {
        public ModalidadCursoController(IIntegraRepository<TModalidadCurso> repositorio, ILogger<BaseController<TModalidadCurso, ValidadorModalidadCursoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                ModalidadCursoRepositorio modalidadCursoRepositorio = new ModalidadCursoRepositorio();
                return Ok(modalidadCursoRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio();
                return Ok(_repModalidadCurso.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ModalidadCursoDatosFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio modalidadCursoRepositorio = new ModalidadCursoRepositorio();

                ModalidadCursoBO modalidadCursoBO = new ModalidadCursoBO();
                modalidadCursoBO.Nombre = Json.Nombre;
                modalidadCursoBO.Codigo = Json.Codigo;
                modalidadCursoBO.Estado = true;
                modalidadCursoBO.UsuarioCreacion = Json.Usuario;
                modalidadCursoBO.UsuarioModificacion = Json.Usuario;
                modalidadCursoBO.FechaCreacion = DateTime.Now;
                modalidadCursoBO.FechaModificacion = DateTime.Now;

                return Ok(modalidadCursoRepositorio.Insert(modalidadCursoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] ModalidadCursoDatosFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio modalidadCursoRepositorio = new ModalidadCursoRepositorio();

                ModalidadCursoBO modalidadCursoBO = modalidadCursoRepositorio.FirstById(Json.Id);
                modalidadCursoBO.Nombre = Json.Nombre;
                modalidadCursoBO.Codigo = Json.Codigo;
                modalidadCursoBO.UsuarioModificacion = Json.Usuario;
                modalidadCursoBO.FechaModificacion = DateTime.Now;

                return Ok(modalidadCursoRepositorio.Update(modalidadCursoBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar([FromBody] ModalidadCursoDatosFiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ModalidadCursoRepositorio modalidadCursoRepositorio = new ModalidadCursoRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (modalidadCursoRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = modalidadCursoRepositorio.Delete(Json.Id, Json.Usuario);
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

    public class ValidadorModalidadCursoDTO : AbstractValidator<TModalidadCurso>
    {
        public static ValidadorModalidadCursoDTO Current = new ValidadorModalidadCursoDTO();
        public ValidadorModalidadCursoDTO()
        {
            RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio")
                                            .Length(1, 100).WithMessage("Nombre debe tener 1 caracter minimo y 50 maximo");


        }

    }
}

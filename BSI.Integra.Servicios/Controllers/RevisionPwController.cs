using BSI.Integra.Aplicacion.DTOs;
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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/RevisionPw")]
    public class RevisionPwController : BaseController<TRevisionPw, ValidadoRevisionPwDTO>
    {
        public RevisionPwController(IIntegraRepository<TRevisionPw> repositorio, ILogger<BaseController<TRevisionPw, ValidadoRevisionPwDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RevisionPwRepositorio _repRevisionPw = new RevisionPwRepositorio();
                return Ok(_repRevisionPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdRevision}")]
        [HttpGet]
        public ActionResult ObtenerTodoRevisionNivelPorId(int IdRevision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RevisionNivelPwRepositorio _repRevisionNivelPw = new RevisionNivelPwRepositorio();
                return Ok(_repRevisionNivelPw.ObtenerRevisionNivelPorIdRevision(IdRevision));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarRevision([FromBody] CompuestoRevisionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RevisionPwRepositorio _repRevisionPw = new RevisionPwRepositorio();
                RevisionPwBO revisionBO = new RevisionPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    revisionBO.Nombre = Json.ObjetoRevision.Nombre;
                    revisionBO.Descripcion = Json.ObjetoRevision.Descripcion;
                    revisionBO.Estado = true;
                    revisionBO.UsuarioCreacion = Json.Usuario;
                    revisionBO.UsuarioModificacion = Json.Usuario;
                    revisionBO.FechaCreacion = DateTime.Now;
                    revisionBO.FechaModificacion = DateTime.Now;

                    revisionBO.RevisionNivel = new List<RevisionNivelPwBO>();

                    foreach (var item in Json.ListaRevision)
                    {
                        RevisionNivelPwBO revisionNivelBO = new RevisionNivelPwBO();
                        revisionNivelBO.Nombre = item.Nombre;
                        revisionNivelBO.Prioridad = item.Prioridad;
                        revisionNivelBO.IdTipoRevisionPw = item.IdTipoRevisionPw;
                        revisionNivelBO.Estado = true;
                        revisionNivelBO.UsuarioCreacion = Json.Usuario;
                        revisionNivelBO.UsuarioModificacion = Json.Usuario;
                        revisionNivelBO.FechaCreacion = DateTime.Now;
                        revisionNivelBO.FechaModificacion = DateTime.Now;

                        revisionBO.RevisionNivel.Add(revisionNivelBO);
                    }
                    _repRevisionPw.Insert(revisionBO);
                    scope.Complete();
                }
                return Ok(revisionBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRevision([FromBody] CompuestoRevisionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                RevisionPwRepositorio _repRevisionPw = new RevisionPwRepositorio(contexto);
                RevisionNivelPwRepositorio _repRevisionNivelPw = new RevisionNivelPwRepositorio(contexto);

                RevisionPwBO revisionBO = new RevisionPwBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    _repRevisionNivelPw.EliminacionLogicoPorIdRevision(Json.ObjetoRevision.Id, Json.Usuario, Json.ListaRevision);

                    revisionBO = _repRevisionPw.FirstById(Json.ObjetoRevision.Id);

                    revisionBO.Nombre = Json.ObjetoRevision.Nombre;
                    revisionBO.Descripcion = Json.ObjetoRevision.Descripcion;
                    revisionBO.UsuarioModificacion = Json.Usuario;
                    revisionBO.FechaModificacion = DateTime.Now;

                    revisionBO.RevisionNivel = new List<RevisionNivelPwBO>();

                    foreach (var item in Json.ListaRevision)
                    {
                        RevisionNivelPwBO revisionNivelBO;
                        if (_repRevisionNivelPw.Exist(x => x.Id == item.Id && x.IdRevisionPw == Json.ObjetoRevision.Id))
                        {
                            revisionNivelBO = _repRevisionNivelPw.FirstBy(x => x.Id == item.Id && x.IdRevisionPw == Json.ObjetoRevision.Id);
                            revisionNivelBO.Nombre = item.Nombre;
                            revisionNivelBO.Prioridad = item.Prioridad;
                            revisionNivelBO.IdTipoRevisionPw = item.IdTipoRevisionPw;
                            revisionNivelBO.UsuarioModificacion = Json.Usuario;
                            revisionNivelBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            revisionNivelBO = new RevisionNivelPwBO();
                            revisionNivelBO.Nombre = item.Nombre;
                            revisionNivelBO.Prioridad = item.Prioridad;
                            revisionNivelBO.IdTipoRevisionPw = item.IdTipoRevisionPw;

                            revisionNivelBO.UsuarioCreacion = Json.Usuario;
                            revisionNivelBO.UsuarioModificacion = Json.Usuario;
                            revisionNivelBO.FechaCreacion = DateTime.Now;
                            revisionNivelBO.FechaModificacion = DateTime.Now;
                            revisionNivelBO.Estado = true;
                        }
                        revisionBO.RevisionNivel.Add(revisionNivelBO);
                    }
                    _repRevisionPw.Update(revisionBO);
                    scope.Complete();
                }
                return Ok(revisionBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarRevision(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext contexto = new integraDBContext();
                RevisionPwRepositorio _repRevisionPw = new RevisionPwRepositorio(contexto);
                RevisionNivelPwRepositorio _repRevisionNivelPw = new RevisionNivelPwRepositorio(contexto);

                RevisionPwBO revisionBO = new RevisionPwBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repRevisionPw.Exist(Id))
                    {
                        _repRevisionPw.Delete(Id, Usuario);
                        var hijosSeccionRevisionNivel = _repRevisionNivelPw.GetBy(x => x.IdRevisionPw == Id);
                        foreach (var hijo in hijosSeccionRevisionNivel)
                        {
                            _repRevisionNivelPw.Delete(hijo.Id, Usuario);
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoRevision()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRevisionPwRepositorio _repTipoRevisionPw = new TipoRevisionPwRepositorio();
                return Ok(_repTipoRevisionPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoRevision()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRevisionPwRepositorio _repTipoRevisionPw = new TipoRevisionPwRepositorio();
                return Ok(_repTipoRevisionPw.ObtenerListaTipoRevision());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
    public class ValidadoRevisionPwDTO : AbstractValidator<TRevisionPw>
    {
        public static ValidadoRevisionPwDTO Current = new ValidadoRevisionPwDTO();
        public ValidadoRevisionPwDTO()
        {
        }
    }
}

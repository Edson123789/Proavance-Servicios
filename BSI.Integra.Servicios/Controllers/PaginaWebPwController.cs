using BSI.Integra.Aplicacion.DTOs;
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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PaginaWebPw")]
    public class PaginaWebPwController : BaseController<TPaginaWebPw, ValidadoPaginaWebPwDTO>
    {
        public PaginaWebPwController(IIntegraRepository<TPaginaWebPw> repositorio, ILogger<BaseController<TPaginaWebPw, ValidadoPaginaWebPwDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
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
                PaginaWebPwRepositorio _repPaginaWebPw = new PaginaWebPwRepositorio();
                return Ok(_repPaginaWebPw.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] PaginaWebPwDTO paginaWebPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaginaWebPwRepositorio _repPaginaWebRepositorio = new PaginaWebPwRepositorio();

                PaginaWebPwBO paginaWebPwBO = new PaginaWebPwBO();
                paginaWebPwBO.Nombre = paginaWebPw.Nombre;
                paginaWebPwBO.ServidorVinculado = paginaWebPw.ServidorVinculado;
                paginaWebPwBO.Estado = true;
                paginaWebPwBO.UsuarioCreacion = paginaWebPw.Usuario;
                paginaWebPwBO.UsuarioModificacion = paginaWebPw.Usuario;
                paginaWebPwBO.FechaCreacion = DateTime.Now;
                paginaWebPwBO.FechaModificacion = DateTime.Now;

                return Ok(_repPaginaWebRepositorio.Insert(paginaWebPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] PaginaWebPwDTO paginaWebPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaginaWebPwRepositorio _repPaginaWebPw = new PaginaWebPwRepositorio();

                PaginaWebPwBO paginaWebPwBO = _repPaginaWebPw.FirstById(paginaWebPw.Id);
                paginaWebPwBO.Nombre = paginaWebPw.Nombre;
                paginaWebPwBO.ServidorVinculado = paginaWebPw.ServidorVinculado;
                paginaWebPwBO.UsuarioModificacion = paginaWebPw.Usuario;
                paginaWebPwBO.FechaModificacion = DateTime.Now;

                return Ok(_repPaginaWebPw.Update(paginaWebPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] PaginaWebPwDTO paginaWebPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PaginaWebPwRepositorio _repPaginaWebPw = new PaginaWebPwRepositorio();

                    if (_repPaginaWebPw.Exist(paginaWebPw.Id))
                    {
                        _repPaginaWebPw.Delete(paginaWebPw.Id, paginaWebPw.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }

    public class ValidadoPaginaWebPwDTO : AbstractValidator<TPaginaWebPw>
    {
        public static ValidadoPaginaWebPwDTO Current = new ValidadoPaginaWebPwDTO();
        public ValidadoPaginaWebPwDTO()
        {
        }
    }
}

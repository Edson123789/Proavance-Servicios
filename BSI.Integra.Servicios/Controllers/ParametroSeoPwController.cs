using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ParametroSeoPw")]
    public class ParametroSeoPwController : BaseController<TParametroSeoPw, ValidadoParametroSeoPwDTO>
    {
        public ParametroSeoPwController(IIntegraRepository<TParametroSeoPw> repositorio, ILogger<BaseController<TParametroSeoPw, ValidadoParametroSeoPwDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
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
                ParametroSeoPwRepositorio _parametroSeoPwRepositorio = new ParametroSeoPwRepositorio();
                return Ok(_parametroSeoPwRepositorio.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ParametroSeoPwDTO parametroSeoPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ParametroSeoPwRepositorio _parametroSeoPwRepositorio = new ParametroSeoPwRepositorio();

                ParametroSeoPwBO parametroSeoPwBO = new ParametroSeoPwBO();
                parametroSeoPwBO.Nombre = parametroSeoPw.Nombre;
                parametroSeoPwBO.NumeroCaracteres = parametroSeoPw.NumeroCaracteres;
                parametroSeoPwBO.Estado = true;
                parametroSeoPwBO.UsuarioCreacion = parametroSeoPw.Usuario;
                parametroSeoPwBO.UsuarioModificacion = parametroSeoPw.Usuario;
                parametroSeoPwBO.FechaCreacion = DateTime.Now;
                parametroSeoPwBO.FechaModificacion = DateTime.Now;

                return Ok(_parametroSeoPwRepositorio.Insert(parametroSeoPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ParametroSeoPwDTO parametroSeoPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ParametroSeoPwRepositorio _parametroSeoPwRepositorio = new ParametroSeoPwRepositorio();

                ParametroSeoPwBO parametroSeoPwBO = _parametroSeoPwRepositorio.FirstById(parametroSeoPw.Id);
                parametroSeoPwBO.Nombre = parametroSeoPw.Nombre;
                parametroSeoPwBO.NumeroCaracteres = parametroSeoPw.NumeroCaracteres;
                parametroSeoPwBO.UsuarioModificacion = parametroSeoPw.Usuario;
                parametroSeoPwBO.FechaModificacion = DateTime.Now;

                return Ok(_parametroSeoPwRepositorio.Update(parametroSeoPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] ParametroSeoPwDTO parametroSeoPw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ParametroSeoPwRepositorio _parametroSeoPwRepositorio = new ParametroSeoPwRepositorio();

                    if (_parametroSeoPwRepositorio.Exist(parametroSeoPw.Id))
                    {
                        _parametroSeoPwRepositorio.Delete(parametroSeoPw.Id, parametroSeoPw.Usuario);
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

    public class ValidadoParametroSeoPwDTO : AbstractValidator<TParametroSeoPw>
    {
        public static ValidadoParametroSeoPwDTO Current = new ValidadoParametroSeoPwDTO();
        public ValidadoParametroSeoPwDTO()
        {
        }
    }
}

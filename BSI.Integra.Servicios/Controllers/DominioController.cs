using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Dominio")]
    [ApiController]
    public class DominioController : Controller
    {
        // GET: api/<DominioController>
        private readonly integraDBContext _integraDBContext;

        public DominioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro()
        {
            try
            {
                DominioRepositorio dominioRepositorio = new DominioRepositorio(_integraDBContext);
                var registros = dominioRepositorio.ObtenerFiltroDominio();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodosRegistros()
        {
            try
            {
                DominioRepositorio dominioRepositorio = new DominioRepositorio(_integraDBContext);
                var registros = dominioRepositorio.ObtenerListaDominio();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarDominio([FromBody] DominioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DominioRepositorio dominioRepositorio = new DominioRepositorio(_integraDBContext);
                DominioBO dominioBO = new DominioBO();


                using (TransactionScope scope = new TransactionScope())
                {
                    dominioBO.Nombre = Json.Nombre;
                    dominioBO.IpPublico = Json.IpPublico;
                    dominioBO.IpPrivado = Json.IpPrivado;
                    dominioBO.Estado = true;
                    dominioBO.UsuarioCreacion = Json.Usuario;
                    dominioBO.UsuarioModificacion = Json.Usuario;
                    dominioBO.FechaCreacion = DateTime.Now;
                    dominioBO.FechaModificacion = DateTime.Now;

                    dominioRepositorio.Insert(dominioBO);
                    scope.Complete();
                }

                return Ok(dominioRepositorio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDominio([FromBody] DominioDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DominioRepositorio dominioRepositorio = new DominioRepositorio(_integraDBContext);
                DominioBO dominioBO = new DominioBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (dominioRepositorio.Exist(Json.Id))
                    {
                        dominioBO = dominioRepositorio.FirstById(Json.Id);
                        dominioBO.Nombre = Json.Nombre;
                        dominioBO.IpPublico = Json.IpPublico;
                        dominioBO.IpPrivado = Json.IpPrivado;
                        dominioBO.UsuarioModificacion = Json.Usuario;
                        dominioBO.FechaModificacion = DateTime.Now;

                        dominioRepositorio.Update(dominioBO);
                        scope.Complete();
                    }
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpDelete]
        public ActionResult EliminarArea(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DominioRepositorio dominioRepositorio = new DominioRepositorio(_integraDBContext);
                DominioBO dominioBO = new DominioBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (dominioRepositorio.Exist(Id))
                    {
                        dominioRepositorio.Delete(Id, Usuario);

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
    }
}

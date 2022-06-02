using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PaginaReclutadoraPersonal")]
    public class PaginaReclutadoraPersonalController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PaginaReclutadoraPersonalRepositorio _repPaginaReclutadoraPersonal;
        public PaginaReclutadoraPersonalController()
        {
            _integraDBContext = new integraDBContext();
            _repPaginaReclutadoraPersonal = new PaginaReclutadoraPersonalRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPaginaReclutadora()
        {
            try
            {
                var listaNivelCurso = _repPaginaReclutadoraPersonal.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre ,Url=x.Url}).ToList();

                return Ok(listaNivelCurso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarPaginaReclutadora([FromBody] EliminarDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repPaginaReclutadoraPersonal.Exist(objeto.Id))
                    {
                        _repPaginaReclutadoraPersonal.Delete(objeto.Id, objeto.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPaginaReclutadora([FromBody] GenericoUrlDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaginaReclutadoraPersonalBO paginaReclutadora = new PaginaReclutadoraPersonalBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    paginaReclutadora.Nombre = Json.Nombre;
                    paginaReclutadora.Url = Json.Url;
                    paginaReclutadora.Estado = true;
                    paginaReclutadora.UsuarioCreacion = Json.Usuario;
                    paginaReclutadora.FechaCreacion = DateTime.Now;
                    paginaReclutadora.UsuarioModificacion = Json.Usuario;
                    paginaReclutadora.FechaModificacion = DateTime.Now;

                    _repPaginaReclutadoraPersonal.Insert(paginaReclutadora);
                    scope.Complete();
                }
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPaginaReclutadora([FromBody] GenericoUrlDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaginaReclutadoraPersonalBO paginaReclutadora = new PaginaReclutadoraPersonalBO();
                paginaReclutadora = _repPaginaReclutadoraPersonal.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    paginaReclutadora.Nombre = Json.Nombre;
                    paginaReclutadora.Url = Json.Url;
                    paginaReclutadora.UsuarioModificacion = Json.Usuario;
                    paginaReclutadora.FechaModificacion = DateTime.Now;
                    _repPaginaReclutadoraPersonal.Update(paginaReclutadora);
                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
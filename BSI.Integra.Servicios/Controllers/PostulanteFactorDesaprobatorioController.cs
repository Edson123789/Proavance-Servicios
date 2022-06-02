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
    [Route("api/PostulanteFactorDesaprobatorio")]
    public class PostulanteFactorDesaprobatorioController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PostulanteFactorDesaprobatorioRepositorio _repPostulanteFactorDesaprobatorio;
        public PostulanteFactorDesaprobatorioController()
        {
            _integraDBContext = new integraDBContext();
            _repPostulanteFactorDesaprobatorio = new PostulanteFactorDesaprobatorioRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerNivelCurso()
        {
            try
            {
                var listaNivelCurso = _repPostulanteFactorDesaprobatorio.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre }).ToList();

                return Ok(listaNivelCurso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarNivelCurso([FromBody] EliminarDTO objeto)
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
                    if (_repPostulanteFactorDesaprobatorio.Exist(objeto.Id))
                    {
                        _repPostulanteFactorDesaprobatorio.Delete(objeto.Id, objeto.NombreUsuario);
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
        public ActionResult InsertarNivelCurso([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PostulanteFactorDesaprobatorioBO nivelIdioma = new PostulanteFactorDesaprobatorioBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    nivelIdioma.Nombre = Json.Nombre;
                    nivelIdioma.Estado = true;
                    nivelIdioma.UsuarioCreacion = Json.Usuario;
                    nivelIdioma.FechaCreacion = DateTime.Now;
                    nivelIdioma.UsuarioModificacion = Json.Usuario;
                    nivelIdioma.FechaModificacion = DateTime.Now;

                    _repPostulanteFactorDesaprobatorio.Insert(nivelIdioma);
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
        public ActionResult ActualizarNivelCurso([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PostulanteFactorDesaprobatorioBO nivelIdioma = new PostulanteFactorDesaprobatorioBO();
                nivelIdioma = _repPostulanteFactorDesaprobatorio.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    nivelIdioma.Nombre = Json.Nombre;
                    nivelIdioma.UsuarioModificacion = Json.Usuario;
                    nivelIdioma.FechaModificacion = DateTime.Now;
                    _repPostulanteFactorDesaprobatorio.Update(nivelIdioma);
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
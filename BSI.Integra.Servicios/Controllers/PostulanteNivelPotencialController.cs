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
    [Route("api/PostulanteNivelPotencial")]
    public class PostulanteNivelPotencialController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PostulanteNivelPotencialRepositorio _repPostulanteNivelPotencial;
        public PostulanteNivelPotencialController()
        {
            _integraDBContext = new integraDBContext();
            _repPostulanteNivelPotencial = new PostulanteNivelPotencialRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerNivelPotencialPostulante()
        {
            try
            {
                var listaNivelCurso = _repPostulanteNivelPotencial.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre }).ToList();

                return Ok(listaNivelCurso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarNivelPotencialPostulante([FromBody] EliminarDTO objeto)
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
                    if (_repPostulanteNivelPotencial.Exist(objeto.Id))
                    {
                        _repPostulanteNivelPotencial.Delete(objeto.Id, objeto.NombreUsuario);
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
        public ActionResult InsertarNivelPotencialPostulante([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PostulanteNivelPotencialBO nivelPotencial = new PostulanteNivelPotencialBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    nivelPotencial.Nombre = Json.Nombre;
                    nivelPotencial.Estado = true;
                    nivelPotencial.UsuarioCreacion = Json.Usuario;
                    nivelPotencial.FechaCreacion = DateTime.Now;
                    nivelPotencial.UsuarioModificacion = Json.Usuario;
                    nivelPotencial.FechaModificacion = DateTime.Now;

                    _repPostulanteNivelPotencial.Insert(nivelPotencial);
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
        public ActionResult ActualizarNivelPotencialPostulante([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PostulanteNivelPotencialBO nivelPotencial = new PostulanteNivelPotencialBO();
                nivelPotencial = _repPostulanteNivelPotencial.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    nivelPotencial.Nombre = Json.Nombre;
                    nivelPotencial.UsuarioModificacion = Json.Usuario;
                    nivelPotencial.FechaModificacion = DateTime.Now;
                    _repPostulanteNivelPotencial.Update(nivelPotencial);
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
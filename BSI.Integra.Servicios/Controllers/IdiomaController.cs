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
    [Route("api/Idioma")]
    public class IdiomaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly IdiomaRepositorio _repIdioma;
        public IdiomaController()
        {
            _integraDBContext = new integraDBContext();
            _repIdioma = new IdiomaRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerIdioma()
        {
            try
            {
                var listaIdioma= _repIdioma.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre }).ToList();

                return Ok(listaIdioma);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarIdioma([FromBody] EliminarDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {                    
                    if (_repIdioma.Exist(objeto.Id))
                    {
                        _repIdioma.Delete(objeto.Id, objeto.NombreUsuario);
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
        public ActionResult InsertarIdioma([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IdiomaBO idioma = new IdiomaBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    idioma.Nombre = Json.Nombre;
                    idioma.Estado = true;
                    idioma.UsuarioCreacion = Json.Usuario;
                    idioma.FechaCreacion = DateTime.Now;
                    idioma.UsuarioModificacion = Json.Usuario;
                    idioma.FechaModificacion = DateTime.Now;

                    _repIdioma.Insert(idioma);
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
        public ActionResult ActualizarIdioma([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IdiomaBO idioma = new IdiomaBO();
                idioma = _repIdioma.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    idioma.Nombre = Json.Nombre;
                    idioma.UsuarioModificacion = Json.Usuario;
                    idioma.FechaModificacion = DateTime.Now;
                    _repIdioma.Update(idioma);
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
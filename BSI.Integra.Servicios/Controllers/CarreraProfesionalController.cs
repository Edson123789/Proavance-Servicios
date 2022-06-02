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
    [Route("api/CarreraProfesional")]
    public class CarreraProfesionalController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly CarreraProfesionalRepositorio _repCarreraProfesional;
        public CarreraProfesionalController()
        {
            _integraDBContext = new integraDBContext();
            _repCarreraProfesional = new CarreraProfesionalRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCarreraProfesional()
        {
            try
            {
                var listaCarreraProfesional= _repCarreraProfesional.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre }).ToList();

                return Ok(listaCarreraProfesional);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCarreraProfesional([FromBody] EliminarDTO objeto)
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
                    if (_repCarreraProfesional.Exist(objeto.Id))
                    {
                        _repCarreraProfesional.Delete(objeto.Id, objeto.NombreUsuario);
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
        public ActionResult InsertarCarreraProfesional([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CarreraProfesionalBO carreraProfesional = new CarreraProfesionalBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    carreraProfesional.Nombre = Json.Nombre;
                    carreraProfesional.Estado = true;
                    carreraProfesional.UsuarioCreacion = Json.Usuario;
                    carreraProfesional.FechaCreacion = DateTime.Now;
                    carreraProfesional.UsuarioModificacion = Json.Usuario;
                    carreraProfesional.FechaModificacion = DateTime.Now;

                    _repCarreraProfesional.Insert(carreraProfesional);
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
        public ActionResult ActualizarCarreraProfesional([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CarreraProfesionalBO carreraProfesional = new CarreraProfesionalBO();
                carreraProfesional = _repCarreraProfesional.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    carreraProfesional.Nombre = Json.Nombre;
                    carreraProfesional.UsuarioModificacion = Json.Usuario;
                    carreraProfesional.FechaModificacion = DateTime.Now;
                    _repCarreraProfesional.Update(carreraProfesional);
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
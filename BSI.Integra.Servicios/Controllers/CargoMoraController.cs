using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
 
namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CargoMora")]
    [ApiController]
    public class CargoMoraController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public CargoMoraController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        // GET: api/<CargoMoraController>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCargos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoMoraRepositorio repCargoMoraRepositorio = new CargoMoraRepositorio(_integraDBContext);
                return Ok(repCargoMoraRepositorio.ObtenerTodoCargoMora());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<CargoMoraController>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertCargo([FromBody] CargoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoMoraRepositorio _repCargo = new CargoMoraRepositorio(_integraDBContext);
                CargoMoraBO NuevoCargo = new CargoMoraBO();
                NuevoCargo.Nombre=ObjetoDTO.Nombre;
                NuevoCargo.Descripcion = ObjetoDTO.Descripcion;
                NuevoCargo.Estado = true;
                NuevoCargo.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoCargo.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoCargo.FechaCreacion = DateTime.Now;
                NuevoCargo.FechaModificacion = DateTime.Now;

                CargoDTO UtlimoCargoInsertado = _repCargo.ObtenerTodoCargoMora()[0];
                NuevoCargo.Orden = UtlimoCargoInsertado.Orden + 1;
                _repCargo.Insert(NuevoCargo);
                return Ok(NuevoCargo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CargoMoraController>/5
        [Route("[Action]")]
        [HttpPost]
        public ActionResult UpdateCargo(int id, [FromBody] CargoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoMoraRepositorio _repCargo = new CargoMoraRepositorio(_integraDBContext);
                CargoMoraBO Cargo = _repCargo.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

                Cargo.Nombre = ObjetoDTO.Nombre;
                Cargo.Descripcion = ObjetoDTO.Descripcion;
                Cargo.Estado = true;
                Cargo.UsuarioModificacion = ObjetoDTO.Usuario;
                Cargo.FechaModificacion = DateTime.Now;

                _repCargo.Update(Cargo);

                return Ok(Cargo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CargoMoraController>/5
        [Route("[Action]")]
        [HttpPost]
        public ActionResult DeleteCargo([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoMoraRepositorio _repCargo = new CargoMoraRepositorio(_integraDBContext);
                CargoMoraBO Cargo = _repCargo.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
                _repCargo.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

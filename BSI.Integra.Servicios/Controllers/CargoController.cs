using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CargoController
    /// Autor: ----------------
    /// Fecha: 04/03/2021
    /// <summary>
    /// Cargo
    /// </summary>
    [Route("api/Cargo")]
    public class CargoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CargoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Jose Villena.
        /// Fecha: 04/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Cargos
        /// </summary>
        /// <returns>Objeto<returns>
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
                CargoRepositorio repCargoRepositorio = new CargoRepositorio(_integraDBContext);
                return Ok(repCargoRepositorio.ObtenerCargoFiltro());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult VizualizarCargos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repCargoRepositorio = new CargoRepositorio(_integraDBContext);
                return Ok(_repCargoRepositorio.ObtenerTodoCargo());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCargo([FromBody] CargoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repCargo = new CargoRepositorio(_integraDBContext);
                CargoBO NuevaCargo = new CargoBO
                {
                    Nombre = ObjetoDTO.Nombre,
                    Descripcion = ObjetoDTO.Descripcion,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                CargoDTO UtlimoCargoInsertado = _repCargo.ObtenerTodoCargo()[0];
                NuevaCargo.Orden = UtlimoCargoInsertado.Orden + 1;
                _repCargo.Insert(NuevaCargo);
                return Ok(NuevaCargo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCargo([FromBody] CargoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repCargo = new CargoRepositorio(_integraDBContext);
                CargoBO Cargo = _repCargo.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();

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

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCargo([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CargoRepositorio _repCargo = new CargoRepositorio(_integraDBContext);
                CargoBO Cargo = _repCargo.GetBy(x => x.Id == Eliminar.Id).FirstOrDefault();
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

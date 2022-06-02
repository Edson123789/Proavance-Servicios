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
    [Route("api/[controller]")]
    [ApiController]
    public class TipoReclamoAlumnoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public TipoReclamoAlumnoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarReclamo([FromBody] TipoReclamoAlumnoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				TipoReclamoAlumnoRepositorio repReclamo = new TipoReclamoAlumnoRepositorio();
				TipoReclamoAlumnoBO reclamo = new TipoReclamoAlumnoBO();
				reclamo.Nombre = Json.Nombre;
				reclamo.UsuarioCreacion = Json.Usuario;
				reclamo.UsuarioModificacion = Json.Usuario;
				reclamo.FechaCreacion = DateTime.Now;
				reclamo.FechaModificacion = DateTime.Now;
				reclamo.Estado = true;
				repReclamo.Insert(reclamo);
				return Ok(reclamo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//Actualizar Reclamos
		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarReclamo([FromBody] TipoReclamoAlumnoDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoReclamoAlumnoRepositorio repreclamoE = new TipoReclamoAlumnoRepositorio();
				TipoReclamoAlumnoBO reclamo = new TipoReclamoAlumnoBO();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repreclamoE.Exist(Json.Id))
					{
						reclamo = repreclamoE.FirstById(Json.Id);
						reclamo.Nombre = Json.Nombre;
						reclamo.UsuarioCreacion = Json.Usuario;
						reclamo.UsuarioModificacion = Json.Usuario;
						reclamo.FechaModificacion = DateTime.Now;
						repreclamoE.Update(reclamo);
					}
					scope.Complete();
					Json.Id = reclamo.Id;
				}

				return Ok(Json);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		//Eliminar Reclamos
		[Route("[action]/{Id}/{Usuario}")]
		[HttpGet]
		public ActionResult EliminarReclamo(int Id, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				TipoReclamoAlumnoRepositorio repReclamo = new TipoReclamoAlumnoRepositorio();
				TipoReclamoAlumnoBO reclamo = new TipoReclamoAlumnoBO();
				var result = repReclamo.Delete(Id, Usuario);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerListaTipoReclamoAlumno()
		{

			try
			{
				TipoReclamoAlumnoRepositorio combosOrigen = new TipoReclamoAlumnoRepositorio();
				var cmbOrigen = combosOrigen.ObtenerListaTipoReclamoAlumno();
				return Ok(cmbOrigen);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

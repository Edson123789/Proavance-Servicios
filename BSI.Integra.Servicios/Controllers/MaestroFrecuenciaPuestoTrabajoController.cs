using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/MaestroFrecuenciaPuestoTrabajo")]
	[ApiController]
	public class MaestroFrecuenciaPuestoTrabajoController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly FrecuenciaPuestoTrabajoRepositorio _repFrecuenciaPuestoTrabajo;

		public MaestroFrecuenciaPuestoTrabajoController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repFrecuenciaPuestoTrabajo = new FrecuenciaPuestoTrabajoRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerFrecuenciaPuestoTrabajo()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaFrecuenciaPuestoTrabajo = _repFrecuenciaPuestoTrabajo.ObtenerFrecuenciaPuestoTrabajo();
				return Ok(listaFrecuenciaPuestoTrabajo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarFrecuenciaPuestoTrabajo([FromBody]FrecuenciaPuestoTrabajoDTO FrecuenciaPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FrecuenciaPuestoTrabajoBO frecuenciaPuestoTrabajo = new FrecuenciaPuestoTrabajoBO()
				{
					Nombre = FrecuenciaPuestoTrabajo.Nombre,
					Estado = true,
					UsuarioCreacion = FrecuenciaPuestoTrabajo.Usuario,
					UsuarioModificacion = FrecuenciaPuestoTrabajo.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repFrecuenciaPuestoTrabajo.Insert(frecuenciaPuestoTrabajo);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarFrecuenciaPuestoTrabajo([FromBody]FrecuenciaPuestoTrabajoDTO FrecuenciaPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var frecuenciaPuestoTrabajo = _repFrecuenciaPuestoTrabajo.FirstById(FrecuenciaPuestoTrabajo.Id);
				frecuenciaPuestoTrabajo.Nombre = FrecuenciaPuestoTrabajo.Nombre;
				frecuenciaPuestoTrabajo.UsuarioModificacion = FrecuenciaPuestoTrabajo.Usuario;
				frecuenciaPuestoTrabajo.FechaModificacion = DateTime.Now;
				var res = _repFrecuenciaPuestoTrabajo.Update(frecuenciaPuestoTrabajo);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarFrecuenciaPuestoTrabajo([FromBody]EliminarDTO FrecuenciaPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repFrecuenciaPuestoTrabajo.Exist(FrecuenciaPuestoTrabajo.Id))
				{
					var res = _repFrecuenciaPuestoTrabajo.Delete(FrecuenciaPuestoTrabajo.Id, FrecuenciaPuestoTrabajo.NombreUsuario);
					return Ok(res);
				}
				else
				{
					return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


	}
}

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
	[Route("api/MaestroTipoFormacion")]
	[ApiController]
	public class MaestroTipoFormacionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly TipoFormacionRepositorio _repTipoFormacion;

		public MaestroTipoFormacionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repTipoFormacion = new TipoFormacionRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerTipoFormacion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaTipoFormacion = _repTipoFormacion.ObtenerTipoFormacion();
				return Ok(listaTipoFormacion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarTipoFormacion([FromBody]FiltroIdUsuarioNombreDTO TipoFormacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoFormacionBO tipoFormacion = new TipoFormacionBO()
				{
					Nombre = TipoFormacion.Nombre,
					Estado = true,
					UsuarioCreacion = TipoFormacion.Usuario,
					UsuarioModificacion = TipoFormacion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repTipoFormacion.Insert(tipoFormacion);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarTipoFormacion([FromBody]FiltroIdUsuarioNombreDTO TipoFormacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var tipoFormacion = _repTipoFormacion.FirstById(TipoFormacion.Id);
				tipoFormacion.Nombre = TipoFormacion.Nombre;
				tipoFormacion.UsuarioModificacion = TipoFormacion.Usuario;
				tipoFormacion.FechaModificacion = DateTime.Now;
				var res = _repTipoFormacion.Update(tipoFormacion);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarTipoFormacion([FromBody]EliminarDTO TipoFormacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repTipoFormacion.Exist(TipoFormacion.Id))
				{
					var res = _repTipoFormacion.Delete(TipoFormacion.Id, TipoFormacion.NombreUsuario);
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

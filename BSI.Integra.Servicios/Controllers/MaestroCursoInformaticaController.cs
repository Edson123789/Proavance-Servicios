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
	[Route("api/MaestroCursoInformatica")]
	[ApiController]
	public class MaestroCursoInformaticaController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly CursoInformaticaRepositorio _repCursoInformatica;

		public MaestroCursoInformaticaController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repCursoInformatica = new CursoInformaticaRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCursoInformatica()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaCursoInformatica = _repCursoInformatica.ObtenerCursoInformatica();
				return Ok(listaCursoInformatica);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCursoInformatica([FromBody]CursoInformaticaDTO CursoInformatica)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CursoInformaticaBO cursoInformatica = new CursoInformaticaBO()
				{
					Nombre = CursoInformatica.Nombre,
					Estado = true,
					UsuarioCreacion = CursoInformatica.Usuario,
					UsuarioModificacion = CursoInformatica.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repCursoInformatica.Insert(cursoInformatica);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCursoInformatica([FromBody]CursoInformaticaDTO CursoInformatica)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var cursoInformatica = _repCursoInformatica.FirstById(CursoInformatica.Id);
				cursoInformatica.Nombre = CursoInformatica.Nombre;
				cursoInformatica.UsuarioModificacion = CursoInformatica.Usuario;
				cursoInformatica.FechaModificacion = DateTime.Now;
				var res = _repCursoInformatica.Update(cursoInformatica);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCursoInformatica([FromBody]EliminarDTO CursoInformatica)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repCursoInformatica.Exist(CursoInformatica.Id))
				{
					var res = _repCursoInformatica.Delete(CursoInformatica.Id, CursoInformatica.NombreUsuario);
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

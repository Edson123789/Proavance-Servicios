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
	[Route("api/MaestroCategoriaEvaluacion")]
	[ApiController]
	public class MaestroCategoriaEvaluacionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly EvaluacionCategoriaRepositorio _repEvaluacionCategoria;

		public MaestroCategoriaEvaluacionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repEvaluacionCategoria = new EvaluacionCategoriaRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCategoriaEvaluacion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaCategoriaEvaluacion = _repEvaluacionCategoria.ObtenerCategoriasEvaluacionRegistradas();
				return Ok(listaCategoriaEvaluacion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCategoriaEvaluacion([FromBody]CategoriaEvaluacionDTO CategoriaEvaluacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				EvaluacionCategoriaBO EvaluacionCategoria = new EvaluacionCategoriaBO()
				{
					Nombre = CategoriaEvaluacion.Nombre,
					Estado = true,
					UsuarioCreacion = CategoriaEvaluacion.Usuario,
					UsuarioModificacion = CategoriaEvaluacion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repEvaluacionCategoria.Insert(EvaluacionCategoria);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCategoriaEvaluacion([FromBody]CategoriaEvaluacionDTO CategoriaEvaluacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var EvaluacionCategoria = _repEvaluacionCategoria.FirstById(CategoriaEvaluacion.Id);
				EvaluacionCategoria.Nombre = CategoriaEvaluacion.Nombre;
				EvaluacionCategoria.UsuarioModificacion = CategoriaEvaluacion.Usuario;
				EvaluacionCategoria.FechaModificacion = DateTime.Now;
				var res = _repEvaluacionCategoria.Update(EvaluacionCategoria);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCategoriaEvaluacion([FromBody]EliminarDTO CategoriaEvaluacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repEvaluacionCategoria.Exist(CategoriaEvaluacion.Id))
				{
					var res = _repEvaluacionCategoria.Delete(CategoriaEvaluacion.Id, CategoriaEvaluacion.NombreUsuario);
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

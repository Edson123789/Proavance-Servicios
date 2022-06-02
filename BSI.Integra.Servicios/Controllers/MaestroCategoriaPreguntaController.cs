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
	[Route("api/MaestroCategoriaPregunta")]
	[ApiController]
	public class MaestroCategoriaPreguntaController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PreguntaCategoriaRepositorio _repPreguntaCategoria;

		public MaestroCategoriaPreguntaController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPreguntaCategoria = new PreguntaCategoriaRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCategoriaPreguntas()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaCategoriaPregunta = _repPreguntaCategoria.ObtenerCategoriasPreguntasRegistradas();
				return Ok(listaCategoriaPregunta);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCategoriaPreguntas([FromBody]CategoriaPreguntaDTO CategoriaPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PreguntaCategoriaBO preguntaCategoria = new PreguntaCategoriaBO()
				{
					Nombre = CategoriaPregunta.Nombre,
					Estado = true,
					UsuarioCreacion = CategoriaPregunta.Usuario,
					UsuarioModificacion = CategoriaPregunta.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repPreguntaCategoria.Insert(preguntaCategoria);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCategoriaPreguntas([FromBody]CategoriaPreguntaDTO CategoriaPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var preguntaCategoria = _repPreguntaCategoria.FirstById(CategoriaPregunta.Id);
				preguntaCategoria.Nombre = CategoriaPregunta.Nombre;
				preguntaCategoria.UsuarioModificacion = CategoriaPregunta.Usuario;
				preguntaCategoria.FechaModificacion = DateTime.Now;
				var res = _repPreguntaCategoria.Update(preguntaCategoria);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCategoriaPreguntas([FromBody]EliminarDTO CategoriaPregunta)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPreguntaCategoria.Exist(CategoriaPregunta.Id))
				{
					var res = _repPreguntaCategoria.Delete(CategoriaPregunta.Id, CategoriaPregunta.NombreUsuario);
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

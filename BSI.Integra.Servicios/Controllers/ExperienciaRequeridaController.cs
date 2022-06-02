using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: ExperienciaRequeridaController
	/// Autor: Britsel Calluchi - Luis Huallpa
	/// Fecha: 07/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Experiencia Requerida
	/// </summary>
	[Route("api/ExperienciaRequerida")]
	[ApiController]
	public class ExperienciaRequeridaController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly ExperienciaRepositorio _repExperiencia;
		private readonly AreaTrabajoRepositorio _repAreaTrabajo;
		public ExperienciaRequeridaController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repExperiencia = new ExperienciaRepositorio(_integraDBContext);
			_repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Combos para Interfaz
		/// </summary>
		/// <returns>Objeto con propiedade de tipo List<AreaTrabajoDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaAreaTrabajo = _repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro();
				return Ok(new
				{
					ListaAreaTrabajo = listaAreaTrabajo
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Registros de Experiencia Requerida
		/// </summary>
		/// <returns>Objeto con propiedade de tipo List<ExperienciaDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerExperiencia()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaExperiencia = _repExperiencia.ObtenerExperiencia();
				return Ok(listaExperiencia);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Insertar Registro de Experiencia Requerida
		/// </summary>
		/// <param name="Experiencia">Información compuesta de Experiencia Requerida</param>
		/// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarExperiencia([FromBody]ExperienciaFiltroDTO Experiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ExperienciaBO experiencia = new ExperienciaBO()
				{
					Nombre = Experiencia.Nombre,
					IdAreaTrabajo = Experiencia.IdAreaTrabajo,
					Estado = true,
					UsuarioCreacion = Experiencia.Usuario,
					UsuarioModificacion = Experiencia.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repExperiencia.Insert(experiencia);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualizar Registro de Experiencia Requerida
		/// </summary>
		/// <param name="Experiencia">Información compuesta de Experiencia Requerida</param>
		/// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarExperiencia([FromBody]ExperienciaFiltroDTO Experiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var experiencia = _repExperiencia.FirstById(Experiencia.Id);
				experiencia.Nombre = Experiencia.Nombre;
				experiencia.IdAreaTrabajo = Experiencia.IdAreaTrabajo;
				experiencia.UsuarioModificacion = Experiencia.Usuario;
				experiencia.FechaModificacion = DateTime.Now;
				var resultado = _repExperiencia.Update(experiencia);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Eliminar Registro de Experiencia Requerida
		/// </summary>
		/// <param name="Experiencia">Información de Id, Usuario</param>
		/// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarExperiencia([FromBody]EliminarDTO Experiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repExperiencia.Exist(Experiencia.Id))
				{
					var resultado = _repExperiencia.Delete(Experiencia.Id, Experiencia.NombreUsuario);
					return Ok(resultado);
				}
				else
				{
					return BadRequest("La entidad a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

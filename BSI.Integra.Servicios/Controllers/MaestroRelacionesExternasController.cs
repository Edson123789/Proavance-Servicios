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
	/// Controlador: MaestroRelacionesExternasController
	/// Autor: Luis Huallpa
	/// Fecha: 10/09/2020
	/// <summary>
	/// Gestiona Maestro de Relaciones Externas
	/// </summary>
	[Route("api/MaestroRelacionesExternas")]
	[ApiController]
	public class MaestroRelacionesExternasController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PersonalRelacionExternaRepositorio _repPersonalRelacionExterna;
		private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;

		public MaestroRelacionesExternasController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPersonalRelacionExterna = new PersonalRelacionExternaRepositorio(_integraDBContext);
			_repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 10/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Combos para Interfaz
		/// </summary>
		/// <returns>List<FiltroDTO></returns>
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
				var listaPersonalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerTodoFiltro();
				return Ok(listaPersonalAreaTrabajo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 10/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de Relaciones Externas
		/// </summary>
		/// <returns>List<PersonalRelacionExternaDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerRelacionExterna()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaRelacionExterna = _repPersonalRelacionExterna.ObtenerPersonalRelacionExterna();
				return Ok(listaRelacionExterna);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 10/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Inserta Nueva Información de Relación Externa
		/// </summary>
		/// <param name="PersonalRelacionExterna">Información compuesta de Relación Externa</param>
		/// <returns>Retorna StatusCode 200 con Bool de confirmación de inserción</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarRelacionExterna([FromBody]PersonalRelacionExternaDTO PersonalRelacionExterna)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRelacionExternaBO personalRelacionExterna = new PersonalRelacionExternaBO()
				{
					Nombre = PersonalRelacionExterna.Nombre,
					IdPersonalAreaTrabajo = PersonalRelacionExterna.IdPersonalAreaTrabajo,
					Estado = true,
					UsuarioCreacion = PersonalRelacionExterna.Usuario,
					UsuarioModificacion = PersonalRelacionExterna.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repPersonalRelacionExterna.Insert(personalRelacionExterna);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 10/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Información de Relación Externa
		/// </summary>
		/// <param name="PersonalRelacionExterna">Información compuesta de Relación Externa</param>
		/// <returns>Retorna StatusCode 200 con Bool de confirmación de actualización</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarRelacionExterna([FromBody]PersonalRelacionExternaDTO PersonalRelacionExterna)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var personalRelacionExterna = _repPersonalRelacionExterna.FirstById(PersonalRelacionExterna.Id);
				personalRelacionExterna.Nombre = PersonalRelacionExterna.Nombre;
				personalRelacionExterna.IdPersonalAreaTrabajo = PersonalRelacionExterna.IdPersonalAreaTrabajo;
				personalRelacionExterna.UsuarioModificacion = PersonalRelacionExterna.Usuario;
				personalRelacionExterna.FechaModificacion = DateTime.Now;
				var resultado = _repPersonalRelacionExterna.Update(personalRelacionExterna);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 10/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Elimina Registro de Relación Externa
		/// </summary>
		/// <param name="PersonalRelacionExterna">Id de Registro y Usuario de Módulo</param>
		/// <returns>Retorna StatusCode 200 con Bool de confirmación de eliminación de registro</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarRelacionExterna([FromBody]EliminarDTO PersonalRelacionExterna)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPersonalRelacionExterna.Exist(PersonalRelacionExterna.Id))
				{
					var resultado = _repPersonalRelacionExterna.Delete(PersonalRelacionExterna.Id, PersonalRelacionExterna.NombreUsuario);
					return Ok(resultado);
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

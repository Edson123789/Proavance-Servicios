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
	/// Controlador: MaestroTipoExperiencia
	/// Autor: Luis Huallpa
	/// Fecha: 25/09/2020
	/// <summary>
	/// Gestión de Tipo de Experiencia
	/// </summary>
	[Route("api/MaestroTipoExperiencia")]
	[ApiController]
	public class MaestroTipoExperienciaController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly TipoExperienciaRepositorio _repTipoExperiencia;

		public MaestroTipoExperienciaController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repTipoExperiencia = new TipoExperienciaRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 25/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Obtiene tipos de Experiencia Registrada
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerTipoExperiencia()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaTipoExperiencia = _repTipoExperiencia.ObtenerListaParaFiltro();
				return Ok(listaTipoExperiencia);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 25/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Inserta nuevo registro de Tipo de Experiencia
		/// </summary>
		/// <param name="TipoExperiencia">Información Compuesta de Tipo de Experiencia</param>
		/// <returns>StatusCode 200 si la operación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarTipoExperiencia([FromBody]FiltroIdUsuarioNombreDTO TipoExperiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoExperienciaBO tipoExperiencia = new TipoExperienciaBO()
				{
					Nombre = TipoExperiencia.Nombre,
					Estado = true,
					UsuarioCreacion = TipoExperiencia.Usuario,
					UsuarioModificacion = TipoExperiencia.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repTipoExperiencia.Insert(tipoExperiencia);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 25/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Actualiza registro de Tipo de Experiencia
		/// </summary>
		/// <param name="TipoExperiencia">Información Compuesta de Tipo de Experiencia</param>
		/// <returns>StatusCode 200 si la operación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarTipoExperiencia([FromBody]FiltroIdUsuarioNombreDTO TipoExperiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var tipoExperiencia = _repTipoExperiencia.FirstById(TipoExperiencia.Id);
				tipoExperiencia.Nombre = TipoExperiencia.Nombre;
				tipoExperiencia.UsuarioModificacion = TipoExperiencia.Usuario;
				tipoExperiencia.FechaModificacion = DateTime.Now;
				var resultado = _repTipoExperiencia.Update(tipoExperiencia);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 25/09/2020
		/// Versión: 1.0
		/// <summary>
		/// Elimina registro de Tipo de Experiencia
		/// </summary>
		/// <param name="TipoExperiencia">Id de Registro y Nombre de Usuario de Interfaz</param>
		/// <returns>StatusCode 200 si la operación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarTipoExperiencia([FromBody]EliminarDTO TipoExperiencia)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repTipoExperiencia.Exist(TipoExperiencia.Id))
				{
					var resultado = _repTipoExperiencia.Delete(TipoExperiencia.Id, TipoExperiencia.NombreUsuario);
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

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
	/// Controlador: NivelInstruccionController
	/// Autor: Britsel Calluchi - Luis Huallpa
	/// Fecha: 08/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Nivel Formación
	/// </summary>
	[Route("api/NivelInstruccion")]
	[ApiController]
	public class NivelInstruccionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly NivelEstudioRepositorio _repNivelInstruccion;
		private readonly TipoFormacionRepositorio _repTipoFormacion;

		public NivelInstruccionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repNivelInstruccion = new NivelEstudioRepositorio(_integraDBContext);
			_repTipoFormacion = new TipoFormacionRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Britsel Calluchi - Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Combos para Interfaz
		/// </summary>
		/// <returns>Lista de Objetos(List<TipoFormacionAcademicaDTO>)</returns>
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
				var listaTipoFormacion = _repTipoFormacion.ObtenerTipoFormacion();
				return Ok(new
				{
					ListaTipoFormacion = listaTipoFormacion
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
		/// Obtiene Registros de Nivel de Instrucción
		/// </summary>
		/// <returns>List<NivelEstudioDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerNivelInstruccion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaNivelInstruccion = _repNivelInstruccion.ObtenerListaNivelEstudio();
				return Ok(listaNivelInstruccion);
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
		/// Inserta Nivel de Instrucción
		/// </summary>
		/// <param name="NivelInstruccion">Información Compuesta de Nivel de Instrucción</param>
		/// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarNivelInstruccion([FromBody]NivelEstudioFiltroDTO NivelInstruccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				NivelEstudioBO nivelInstruccion = new NivelEstudioBO()
				{
					Nombre = NivelInstruccion.Nombre,
					IdTipoFormacion = NivelInstruccion.IdTipoFormacion,
					Estado = true,
					UsuarioCreacion = NivelInstruccion.Usuario,
					UsuarioModificacion = NivelInstruccion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repNivelInstruccion.Insert(nivelInstruccion);
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
		/// Actualiza Nivel de Instrucción
		/// </summary>
		/// <param name="NivelInstruccion">Información Compuesta de Nivel de Instrucción</param>
		/// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarNivelInstruccion([FromBody]NivelEstudioFiltroDTO NivelInstruccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var nivelInstruccion = _repNivelInstruccion.FirstById(NivelInstruccion.Id);
				nivelInstruccion.Nombre = NivelInstruccion.Nombre;
				nivelInstruccion.IdTipoFormacion = NivelInstruccion.IdTipoFormacion;
				nivelInstruccion.UsuarioModificacion = NivelInstruccion.Usuario;
				nivelInstruccion.FechaModificacion = DateTime.Now;
				var resultado = _repNivelInstruccion.Update(nivelInstruccion);
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
		/// Elimina Nivel de Instrucción
		/// </summary>
		/// <param name="NivelInstruccion">Información Id, Usuario</param>
		/// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarNivelInstruccion([FromBody]EliminarDTO NivelInstruccion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repNivelInstruccion.Exist(NivelInstruccion.Id))
				{
					var resultado = _repNivelInstruccion.Delete(NivelInstruccion.Id, NivelInstruccion.NombreUsuario);
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

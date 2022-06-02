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
	/// Controlador: MaestroNivelCursoComplementarioController
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Nivel Curso Complementario
	/// </summary>
	[Route("api/MaestroNivelCursoComplementario")]
	[ApiController]
	public class MaestroNivelCursoComplementarioController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly NivelCompetenciaTecnicaRepositorio _repNivelCompetenciaTecnica;

		public MaestroNivelCursoComplementarioController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repNivelCompetenciaTecnica = new NivelCompetenciaTecnicaRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Registros de Nivel de Cursos Complementarios
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerNivelCursoComplementario()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaNivelCompetenciaTecnica = _repNivelCompetenciaTecnica.ObtenerListaParaFiltro();
				return Ok(listaNivelCompetenciaTecnica);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta Registros de Nivel de Curso Complementario
		/// </summary>
		/// <param name="NivelCursoComplementario">Información Compuesta de Nivel de Curso Complementario</param>
		/// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarNivelCursoComplementario([FromBody]FiltroIdUsuarioNombreDTO NivelCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				NivelCompetenciaTecnicaBO NivelCompetenciaTecnica = new NivelCompetenciaTecnicaBO()
				{
					Nombre = NivelCursoComplementario.Nombre,
					Estado = true,
					UsuarioCreacion = NivelCursoComplementario.Usuario,
					UsuarioModificacion = NivelCursoComplementario.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repNivelCompetenciaTecnica.Insert(NivelCompetenciaTecnica);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Registros de Nivel de Curso Complementario
		/// </summary>
		/// <param name="NivelCursoComplementario">Información Compuesta de Nivel de Curso Complementario</param>
		/// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarNivelCursoComplementario([FromBody]FiltroIdUsuarioNombreDTO NivelCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var nivelCompetenciaTecnica = _repNivelCompetenciaTecnica.FirstById(NivelCursoComplementario.Id);
				nivelCompetenciaTecnica.Nombre = NivelCursoComplementario.Nombre;
				nivelCompetenciaTecnica.UsuarioModificacion = NivelCursoComplementario.Usuario;
				nivelCompetenciaTecnica.FechaModificacion = DateTime.Now;
				var resultado = _repNivelCompetenciaTecnica.Update(nivelCompetenciaTecnica);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina Registros de Nivel de Curso Complementario
		/// </summary>
		/// <param name="NivelCursoComplementario">Información Id, Usuario de registro</param>
		/// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarNivelCursoComplementario([FromBody]EliminarDTO NivelCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repNivelCompetenciaTecnica.Exist(NivelCursoComplementario.Id))
				{
					var resultado = _repNivelCompetenciaTecnica.Delete(NivelCursoComplementario.Id, NivelCursoComplementario.NombreUsuario);
					return Ok(resultado);
				}
				else
				{
					return BadRequest("El elemnto a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

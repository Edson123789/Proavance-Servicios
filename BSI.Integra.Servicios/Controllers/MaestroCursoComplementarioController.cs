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
	/// Controlador: MaestroCursoComplementarioController
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Curso Complementario
	/// </summary>
	[Route("api/MaestroCursoComplementario")]
	[ApiController]
	public class MaestroCursoComplementarioController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly CompetenciaTecnicaRepositorio _repCompetenciaTecnica;
		private readonly TipoCompetenciaTecnicaRepositorio _repTipoCompetenciaTecnica;

		public MaestroCursoComplementarioController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repCompetenciaTecnica = new CompetenciaTecnicaRepositorio(_integraDBContext);
			_repTipoCompetenciaTecnica = new TipoCompetenciaTecnicaRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Combos para Interfaz
		/// </summary>
		/// <returns>List<TipoCursoComplementarioDTO></returns>
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
				var listaTipoCompetenciaTecnica = _repTipoCompetenciaTecnica.ObtenerListaParaFiltro();
				return Ok(new
				{
					ListaTipoCompetenciaTecnica = listaTipoCompetenciaTecnica
				});
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
		/// Obtiene Registros de Cursos Complementarios
		/// </summary>
		/// <returns>List<CursoComplementarioDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCursoComplementario()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaCompetenciaTecnica = _repCompetenciaTecnica.ObtenerListaCursoComplementario();
				return Ok(listaCompetenciaTecnica);
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
		/// Inserta Registros de Cursos Complementario
		/// </summary>
		/// <param name="CursoComplementario">Información Compuesta de Curso Complementario</param>
		/// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCursoComplementario([FromBody]CursoComplementarioFiltroDTO CursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CompetenciaTecnicaBO competenciaTecnica = new CompetenciaTecnicaBO()
				{
					Nombre = CursoComplementario.Nombre,
					IdTipoCompetenciaTecnica = CursoComplementario.IdTipoCursoComplementario,
					Estado = true,
					UsuarioCreacion = CursoComplementario.Usuario,
					UsuarioModificacion = CursoComplementario.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repCompetenciaTecnica.Insert(competenciaTecnica);
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
		/// Actualiza Registros de Cursos Complementario
		/// </summary>
		/// <param name="CursoComplementario">Información Compuesta de Curso Complementario</param>
		/// <returns>Retorna StatusCodes, 200 si la actualiza es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCursoComplementario([FromBody]CursoComplementarioFiltroDTO CursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var competenciaTecnica = _repCompetenciaTecnica.FirstById(CursoComplementario.Id);
				competenciaTecnica.Nombre = CursoComplementario.Nombre;
				competenciaTecnica.IdTipoCompetenciaTecnica = CursoComplementario.IdTipoCursoComplementario;
				competenciaTecnica.UsuarioModificacion = CursoComplementario.Usuario;
				competenciaTecnica.FechaModificacion = DateTime.Now;
				var resultado = _repCompetenciaTecnica.Update(competenciaTecnica);
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
		/// Elimina Registros de Curso Complementario
		/// </summary>
		/// <param name="CursoComplementario">Información Id, Usuario de registro</param>
		/// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCursoComplementario([FromBody]EliminarDTO CursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repCompetenciaTecnica.Exist(CursoComplementario.Id))
				{
					var res = _repCompetenciaTecnica.Delete(CursoComplementario.Id, CursoComplementario.NombreUsuario);
					return Ok(res);
				}
				else
				{
					return BadRequest("El elemento a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

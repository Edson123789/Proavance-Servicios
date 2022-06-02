using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;


namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: PostulanteInformacionController
	/// Autor: Edgar S.
	/// Fecha: 25/03/2021
	/// <summary>
	/// Gestión Información de Postulantes
	/// </summary>
	[Route("api/PostulanteInformacion")]
	public class PostulanteInformacionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PostulanteRepositorio _repPostulante;
		private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
		private readonly SedeTrabajoRepositorio _repSedeTrabajo;
		private readonly PostulanteConexionInternetRepositorio _repPostulanteConexionInternet;
		private readonly PostulanteEquipoComputoRepositorio _repPostulanteEquipoComputo;
		private readonly PostulanteExperienciaRepositorio _repPostulanteExperiencia;
		private readonly PostulanteFormacionRepositorio _repPostulanteFormacion;
		private readonly PostulanteIdiomaRepositorio _repPostulanteIdioma;

		public PostulanteInformacionController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
			_repPostulante = new PostulanteRepositorio(_integraDBContext);
			_repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
			_repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
			_repPostulanteConexionInternet = new PostulanteConexionInternetRepositorio(_integraDBContext);
			_repPostulanteEquipoComputo = new PostulanteEquipoComputoRepositorio(_integraDBContext);
			_repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
			_repPostulanteFormacion = new PostulanteFormacionRepositorio(_integraDBContext);
			_repPostulanteIdioma = new PostulanteIdiomaRepositorio(_integraDBContext);
		}

		[HttpPost]
		[Route("[action]")]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var comboPuestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre();
				var comboSedeTrabajo = _repSedeTrabajo.ObtenerTodoFiltro();

				return Ok(new { ComboPuestoTrabajo = comboPuestoTrabajo, ComboSedeTrabajo = comboSedeTrabajo });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult GetPostulanteAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					return Ok(_repPostulante.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
				}
				else
				{
					List<FiltroDTO> lista = new List<FiltroDTO>();
					return Ok(lista);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[HttpPost]
		[Route("[Action]")]
		public ActionResult GetPostulanteAutocomplete2()
		{
			try
			{
				return Ok(_repPostulante.GetFiltroIdNombre());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerPostulantesPorFiltro([FromBody] PostulanteReporteFiltroDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var postulante = _repPostulante.ObtenerPostulanteInformacion(Filtro);
				return Ok(postulante);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerPostulantesInformacion([FromBody]int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var postulanteFormacion = _repPostulanteFormacion.ObtenerPostulanteFormacion(IdPostulante);
				var postulanteIdioma = _repPostulanteIdioma.ObtenerPostulanteIdioma(IdPostulante);
				var postulanteExperiencia = _repPostulanteExperiencia.ObtenerPostulanteExperiencia(IdPostulante);
				var postulanteEquipoComputo = _repPostulanteEquipoComputo.ObtenerPostulanteEquipoComputo(IdPostulante);
				var postulanteConexionInternet = _repPostulanteConexionInternet.ObtenerPostulanteConexionInternet(IdPostulante);
				
				return Ok(new {
					PostulanteFormacion = postulanteFormacion,
					PostulanteIdioma = postulanteIdioma,
					PostulanteExperiencia = postulanteExperiencia,
					PostulanteEquipoComputo = postulanteEquipoComputo,
					PostulanteConexionInternet = postulanteConexionInternet,
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de Postulantes
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerPostulantesInformacionV2([FromBody] int IdPostulante)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var postulanteInformacion = _repPostulante.ObtenerInformacionPostulanteVisual(IdPostulante);
				var postulanteFormacion = _repPostulanteFormacion.ObtenerPostulanteFormacion(IdPostulante);
				var postulanteIdioma = _repPostulanteIdioma.ObtenerPostulanteIdioma(IdPostulante);
				var postulanteExperiencia = _repPostulanteExperiencia.ObtenerPostulanteExperiencia(IdPostulante);
				var postulanteEquipoComputo = _repPostulanteEquipoComputo.ObtenerPostulanteEquipoComputo(IdPostulante);
				var postulanteConexionInternet = _repPostulanteConexionInternet.ObtenerPostulanteConexionInternet(IdPostulante);

				return Ok(new
				{
					PostulanteInformacion = postulanteInformacion,
					PostulanteFormacion = postulanteFormacion,
					PostulanteIdioma = postulanteIdioma,
					PostulanteExperiencia = postulanteExperiencia,
					PostulanteEquipoComputo = postulanteEquipoComputo,
					PostulanteConexionInternet = postulanteConexionInternet,
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de Postulante que fueron incorporados como Personal
		/// </summary>
		/// <returns> objeto Agrupado </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult GetPersonalComoPostulanteAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					return Ok(_repPostulante.ObtenerPersonalComoPostulanteAutocomplete(Filtros["valor"].ToString()));
				}
				else
				{
					List<FiltroDTO> lista = new List<FiltroDTO>();
					return Ok(lista);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

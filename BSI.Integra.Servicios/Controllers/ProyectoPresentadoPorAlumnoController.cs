using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: ProyectoPresentadoPorAlumno
	/// Autor: Cesar Santillana
	/// Fecha: 21/06/2021
	/// <summary>
	/// Contiene los controladores necesarios para los filtros y la consulta a los proyectos presentados por alumno
	/// </summary>

	[Route("api/ProyectoPresentadoPorAlumno")]
	public class ProyectoPresentadoPorAlumnoController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;

		public ProyectoPresentadoPorAlumnoController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
		}

		/// TipoFuncion: GET
		/// Autor: Cesar Santillana
		/// Fecha: 25/06/2021
		/// Version: 1.0
		/// <summary>
		/// Función que trae data para llenar los combos Coordinadores, Docente, CentroCosto y PEspecifico
		/// </summary>
		/// <returns>Retorma una lista</returns>
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerDataCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio repCoordinadoras = new PersonalRepositorio();
				ProveedorRepositorio repProveedor = new ProveedorRepositorio();
				ProyectoPresentadoPorAlumnoRepositorio repCentroCosto = new ProyectoPresentadoPorAlumnoRepositorio();
				PespecificoRepositorio repPEspecifico = new PespecificoRepositorio();
				
				var Detalles = new
				{
					Coordinadora = repCoordinadoras.ObtenerCoordinadorasDocente(),
					Docente = repProveedor.ObtenerNombreProveedorParaHonorario(),
					CentroCosto = repCentroCosto.ObtenerCentroCosto(),
					PEspecifico = repPEspecifico.ObtenerProgramaEspecifico(),
				};
				return Ok(Detalles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		/// Tipo Función: POST
		/// Autor: Cesar Santillana
		/// Fecha: 25/06/2021
		/// Versión: 1.0
		/// <summary>
		/// Generar el reporte de los proyectos presentados por alumno
		/// </summary>
		/// <param name="filtroReporte">Filtro para el reporte de los proyectos presentados por alumno  </param>
		/// <returns>El reporte retorna una Lista List<ProyectoPresentadoPorAlumnoDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult GenerarReporteProyectoPresentadoPorAlumno([FromBody] ProyectoPresentadoPorAlumnoFiltroDTO FiltroReporte)
		{
			try
			{
				ProyectoPresentadoPorAlumnoRepositorio repoProyectoPorAlumno = new ProyectoPresentadoPorAlumnoRepositorio();
				var lista = repoProyectoPorAlumno.GenerarReporteProyectoPresentadoPorAlumno(FiltroReporte);
				return Ok(lista);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Max Mantilla
		/// Fecha: 19/01/2022
		/// Versión: 1.0
		/// <summary>
		/// Generar un reporte con los nombre de los programas especificos según el valor ingresado
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerProgramaEspecificoAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
					return Ok(_repPEspecifico.ObtenerProgramaEspecificoAutocomplete(Filtros["valor"].ToString()));
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		/// Tipo Función: POST
		/// Autor: Max Mantilla
		/// Fecha: 19/01/2022
		/// Versión: 1.0
		/// <summary>
		/// Generar un reporte con los nombre de los centros de costos según el valor ingresado
		/// </summary>
		/// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (Filtros != null)
				{
					CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
					var centroCosto = _repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"]);
					return Ok(centroCosto);
				}
				else
				{
					return Ok();
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

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
    [Route("api/ReporteExamen")]
    [ApiController]
    public class ReporteExamenController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;
		public ReporteExamenController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Información de combos para módulo
		/// </summary>
		/// <returns>Registros para combos</returns>
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
				SexoRepositorio _repSexo = new SexoRepositorio(_integraDBContext);
				PuestoTrabajoRepositorio _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
				SedeTrabajoRepositorio _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
				PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                GrupoComparacionProcesoSeleccionRepositorio _repGrupoComparacion = new GrupoComparacionProcesoSeleccionRepositorio(_integraDBContext);
                ProcesoSeleccionRepositorio _repProceso = new ProcesoSeleccionRepositorio(_integraDBContext);
                ProcesoSeleccionEtapaRepositorio _repEtapaProceso= new ProcesoSeleccionEtapaRepositorio(_integraDBContext);

                var sexo = _repSexo.GetFiltroIdNombre().OrderBy(x=>x.Nombre);
				var sedeTrabajo = _repSedeTrabajo.GetFiltroIdNombre().OrderBy(x=>x.Nombre);
				var puestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre().OrderBy(x=>x.Nombre);
				var postulante = _repPostulante.GetFiltroIdNombre().OrderBy(x=>x.Nombre);
                var grupoComparacion = _repGrupoComparacion.GetFiltroIdNombre().OrderBy(x=>x.Nombre);
                var proceso = _repProceso.ObtenerCodigoNombreProcesoSeleccion();
                var etapa= _repEtapaProceso.ObtenerProcesoSeleccionEtapa();
                var estadoPostulante = _repProceso.ObtenerEstadoProcesoSeleccion();
                var estadoEtapaProcesoSeleccion = _repProceso.ObtenerEstadoEtapaProcesoSeleccion();

                return Ok(new { Sexo = sexo, SedeTrabajo = sedeTrabajo, PuestoTrabajo = puestoTrabajo, Postulante = postulante, GrupoComparacion= grupoComparacion,ListaProcesoSeleccion= proceso,ListaEtapa= etapa,EstadoProceso= estadoPostulante,ListaEstadoEtapa= estadoEtapaProcesoSeleccion });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Genera reporte GmatPma por Filtro
		/// </summary>
		/// <returns>Información de reporte GmatPma por Filtro</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ReporteGmatPma(ReporteProcesoSeleccionFiltroDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ReporteExamenProcesoSeleccionBO reporteExamenProcesoSeleccion = new ReporteExamenProcesoSeleccionBO(_integraDBContext);

				if (Filtro.IdSexo == "_")
				{
					Filtro.IdSexo = "1,2";
				}
				if (Filtro.IdSede == "_")
				{
					Filtro.IdSede = "1,2";
				}
				if (Filtro.Psicotecnico == "_")
				{
					Filtro.Psicotecnico = "Edad,Factor GMAT,Factor E,Factor N,Factor R,Factor V,Factor CI,Factor F";
				}
				else
				{
					Filtro.Psicotecnico = Filtro.Psicotecnico + ",Edad";
				}
				if (Filtro.Psicologico == "_")
				{
					Filtro.Psicologico = "ISRA,Optimismo,NEO-PI-R";
				}

				var reporte = reporteExamenProcesoSeleccion.ObtenerReporteGmatPma(Filtro);

				var datosAgrupado = (from p in reporte
									 group p by p.Orden into grupo
									 select new { g = grupo.Key, l = grupo }).ToList();

				var postulantes = (from p in reporte
								   group p by new { p.IdPostulante, p.Postulante, p.Edad } into grupo
								   select new { IdPostulante = grupo.Key.IdPostulante, Postulante = grupo.Key.Postulante, Edad = grupo.Key.Edad }).ToList();

				return Ok(new { DatosAgrupado = datosAgrupado, Postulantes = postulantes, Estado = true });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Genera reporte Isra Optimismo Neopir por Filtro
		/// </summary>
		/// <returns>Información de reporte Isra Optimismo Neopir por Filtro</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ReporteIsraOptimismoNeopir(ReporteProcesoSeleccionFiltroDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ReporteExamenProcesoSeleccionBO reporteExamenProcesoSeleccion = new ReporteExamenProcesoSeleccionBO(_integraDBContext);

				if (Filtro.IdSexo == "_")
				{
					Filtro.IdSexo = "1,2";
				}
				if (Filtro.IdSede == "_")
				{
					Filtro.IdSede = "1,2";
				}
				if (Filtro.Psicotecnico == "_")
				{
					Filtro.Psicotecnico = "Edad,Factor GMAT,Factor E,Factor N,Factor R,Factor V,Factor CI,Factor F";
				}
				else
				{
					Filtro.Psicotecnico = Filtro.Psicotecnico + ",Edad";
				}
				if (Filtro.Psicologico == "_")
				{
					Filtro.Psicologico = "Edad,ISRA,Optimismo,NEO-PI-R";
				}
				else
				{
					Filtro.Psicologico = Filtro.Psicologico + ",Edad";
				}

				var reporte = reporteExamenProcesoSeleccion.ObtenerReporteIsraOptimismoNeopir(Filtro);

				var datosAgrupado = (from p in reporte
									 group p by p.Orden into grupo
									 select new { g = grupo.Key, l = grupo }).ToList();

				var postulantes = (from p in reporte
								   group p by new { p.IdPostulante, p.Postulante, p.Edad } into grupo
								   select new { IdPostulante = grupo.Key.IdPostulante, Postulante = grupo.Key.Postulante, Edad = grupo.Key.Edad }).ToList();

				return Ok(new { DatosAgrupado = datosAgrupado, Postulantes = postulantes, Estado = true  });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

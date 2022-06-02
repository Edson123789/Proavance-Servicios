using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/ProgramaEspecificoMaterial")]
	[ApiController]
	public class ProgramaEspecificoMaterialController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		public ProgramaEspecificoMaterialController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

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
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
				RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio(_integraDBContext);
				EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio(_integraDBContext);
				ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
				PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
				AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio(_integraDBContext);
				SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio(_integraDBContext);
				var _repMaterialTipo = new MaterialTipoRepositorio(_integraDBContext);


				var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
				var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
				var modalidad = _repModalidadCurso.ObtenerModalidadCursoFiltro();

				var area = _repArea.ObtenerAreaCapacitacionFiltro();
				var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
				var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(1);
				var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadres(1);
				var centroCosto = _repCentroCosto.ObtenerCentroCostoPadres(1);

				var listaMaterialTipo = _repMaterialTipo.ObtenerTodoFiltro();

				return Ok(new { 
					ProgramaEspecifico = programaEspecifico, 
					CentroCosto = centroCosto, 
					Ciudad = ciudadBs, 
					EstadoPEspecifico = estadoPespecifico, 
					Modalidad = modalidad, 
					ProgramaGeneral = programaGeneral, 
					Area = area, 
					SubArea = subArea,
					ListaMaterialTipo = listaMaterialTipo
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerProgramasEspecificos([FromBody]ProgramaEspecificoMaterialFiltroDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				var res = _repPespecifico.ObtenerProgramasEspecificoFiltros(Filtro);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

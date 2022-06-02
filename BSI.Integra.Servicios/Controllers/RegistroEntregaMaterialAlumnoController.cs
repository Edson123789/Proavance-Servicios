using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/RegistroEntregaMaterialAlumno")]
	public class RegistroEntregaMaterialAlumnoController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public RegistroEntregaMaterialAlumnoController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosRegistroMaterial()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio(_integraDBContext);
				ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
				PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
				AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio(_integraDBContext);
				SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio(_integraDBContext);
				PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
				EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio(_integraDBContext);
				ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio(_integraDBContext);
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);

				var res = _repPEspecifico.ObtenerPEspecificoGruposPorPEspecificoPadre();
				var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
				var area = _repArea.ObtenerAreaCapacitacionFiltro();
				var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
				var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(null);
				var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadres(null);
				var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();
				var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
				var grupos = _repPespecificoSesion.ObtenerGruposProgramaEspecificoFiltro();
				var estados = _repPEspecifico.ObtenerEstadoEntregaMaterialAlumnoFiltro();

				return Ok(new { Estados = estados,
					CiudadBS = ciudadBs,
					Area = area,
					SubArea = subArea,
					ProgramaGeneralP = programaGeneral,
					ProgramaEspecifico = programaEspecifico,
					PEspecificoCurso = res,
					Estado = estadoPespecifico,
					Grupos = grupos,
					Modalidad = modalidades
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ProgramaEspecificoPadreAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
					return Ok(_repPEspecifico.ObtenerProgramaEspecificoPadreAutoComplete(Filtros["valor"].ToString()));
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

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCriteriosMaterialesProgramaEspecifico([FromBody]FiltroMaterialDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MaterialPespecificoRepositorio _repMaterialPEspecifico = new MaterialPespecificoRepositorio(_integraDBContext);
				MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
				var listaAlumnos = _repMaterialPEspecifico.ObtenerMaterialesRegistroEntregaAlumno(Filtro);
				List<MaterialTipoColumnaDTO> columnas;
				var agrupado = listaAlumnos.GroupBy(x => new { x.IdAlumno, x.Alumno, x.IdMatriculaCabecera, x.IdPEspecifico, x.NombrePEspecifico, x.Grupo }).Select(x => new MaterialAlumnoAgrupadoDTO
				{
					IdAlumno = x.Key.IdAlumno,
					Alumno = x.Key.Alumno,
					IdMatriculaCabecera = x.Key.IdMatriculaCabecera,
					IdPEspecifico = x.Key.IdPEspecifico,
					NombrePEspecifico = x.Key.NombrePEspecifico,
					Grupo = x.Key.Grupo,
					Materiales = x.GroupBy(y => new { y.IdMaterialPEspecificoDetalle, y.IdMaterialTipo, y.NombreMaterialTipo, y.IdEstadoEntregaMaterialAlumno, y.EstadoEntregaMaterialAlumno }).Select(y => new MaterialAlumnoAgrupadoDetalleDTO
					{
						IdMaterialPEspecificoDetalle = y.Key.IdMaterialPEspecificoDetalle,
						IdMaterialTipo = y.Key.IdMaterialTipo,
						MaterialTipo = y.Key.NombreMaterialTipo,
						IdEstadoEntregaMaterialAlumno = y.Key.IdEstadoEntregaMaterialAlumno,
						EstadoEntregaMaterialAlumno = y.Key.EstadoEntregaMaterialAlumno
					}).ToList()
				});

				columnas = listaAlumnos.GroupBy(x => new { x.IdMaterialTipo, x.NombreMaterialTipo, x.IdMaterialPEspecificoDetalle }).Select(x => new MaterialTipoColumnaDTO
				{
					IdMaterialPEspecificoDetalle = x.Key.IdMaterialPEspecificoDetalle,
					IdMaterialTipo = x.Key.IdMaterialTipo,
					MaterialTipo = x.Key.NombreMaterialTipo
				}).ToList();
				return Ok(new { ListaAlumnos = agrupado, Columnas = columnas });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarEntregaAlumno([FromBody]MaterialAlumnoEntregaRegistroDTO Registro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MaterialPespecificoDetalleRepositorio _repMaterialPEspecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
				MaterialRegistroEntregaAlumnoRepositorio _repMaterialRegistroEntregaAlumno = new MaterialRegistroEntregaAlumnoRepositorio(_integraDBContext);
				MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
				MaterialCriterioVerificacionDetalleRepositorio _repMaterialCriterioVerificacionDetalle = new MaterialCriterioVerificacionDetalleRepositorio(_integraDBContext);
				//var listaCriterios = _repMaterialAsociacionCriterioVerificacion.ObtenerCriteriosVerificacionPorMaterialDetalle(Registro.IdMaterialPEspecificoDetalle);
				foreach (var item in Registro.ClaveValor)
				{
					MaterialRegistroEntregaAlumnoBO registro;
					registro = _repMaterialRegistroEntregaAlumno.FirstBy(x => x.IdMatriculaCabecera == Registro.IdMatriculaCabecera && x.IdMaterialPespecificoDetalle == item.Key);
					//registro = _repMaterialRegistroEntregaAlumno.ObtenerMaterialRegistroEntregaAlumno(Registro.IdMatriculaCabecera, item.Key);

					if(registro != null)
					{
						registro.IdEstadoEntregaMaterialAlumno = item.Value;
						registro.UsuarioAprobacion = Registro.Usuario;
						registro.UsuarioModificacion = Registro.Usuario;
						registro.FechaAprobacion = DateTime.Now;
						registro.FechaModificacion = DateTime.Now;
						_repMaterialRegistroEntregaAlumno.Update(registro);
						//_repMaterialCriterioVerificacionDetalle.InsertarActualizarRegistroEntregaMaterialAlumno(registro);
					}
					else
					{
						registro = new MaterialRegistroEntregaAlumnoBO()
						{
							IdMatriculaCabecera = Registro.IdMatriculaCabecera,
							IdMaterialPespecificoDetalle = item.Key,
							IdEstadoEntregaMaterialAlumno = item.Value,
							Estado = true,
							UsuarioCreacion = Registro.Usuario,
							UsuarioModificacion = Registro.Usuario,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							UsuarioAprobacion = Registro.Usuario,
							FechaAprobacion = DateTime.Now
						};
						_repMaterialRegistroEntregaAlumno.Insert(registro);
					}
					//_repMaterialCriterioVerificacionDetalle.InsertarActualizarRegistroEntregaMaterialAlumno(registro);
					//_repMaterialAsociacionCriterioVerificacion.RegistrarCriteriosEntregaMaterial(criterio.Id, item.Value, Registro.Usuario);
				}
				return Ok(true);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}

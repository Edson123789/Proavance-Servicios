using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/Operaciones/Curso")]
	[ApiController]
	public class CursoOpeController : Controller
	{
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerListaCursoDocentes(int IdCentroCosto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaCursoRepositorio _repCursoRepositorio = new RaCursoRepositorio();
				ExpositorRepositorio _repExpositorRepositorio = new ExpositorRepositorio();
				List<RaCursoDTO> listaCurso = _repCursoRepositorio.ObtenerCursos(IdCentroCosto);
				List<RaExpositorDTO> listaDocente = _repExpositorRepositorio.GetBy(x => x.Estado == true, x => new RaExpositorDTO { Id = x.Id, NombresCompletos = string.Concat(x.PrimerNombre, " ", x.SegundoNombre, " ", x.ApellidoPaterno, " ", x.ApellidoMaterno) }).ToList();

				foreach (var item in listaCurso)
				{
					item.Docente = listaDocente.Where(x => x.Id == item.IdExpositor).FirstOrDefault();
				}
				return Ok(listaCurso);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerCombosFiltros()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
				MonedaRepositorio _repMoneda = new MonedaRepositorio();
				RaTipoCursoRepositorio _repTipoCurso = new RaTipoCursoRepositorio();
				RaTipoContratoRepositorio _repTipoContrato = new RaTipoContratoRepositorio();

				var listaExpositor = _repExpositor.ObtenerFiltroExpositor();
				var listaMoneda = _repMoneda.ObtenerFiltroMoneda();
				var listadoTipoCurso = _repTipoCurso.ObtenerFiltroTipoCurso();
				var listadoTipoContrato = _repTipoContrato.ObtenerFiltroTipoContrato();
				return Ok(new { Expositor = listaExpositor, Moneda = listaMoneda, TipoCurso = listadoTipoCurso, TipoContrato = listadoTipoContrato });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarCurso([FromBody]CompuestoCursoDTO CursoFormulario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaCursoRepositorio _repCurso = new RaCursoRepositorio();
				RaCursoBO Curso = new RaCursoBO()
				{
					IdRaCentroCosto = CursoFormulario.Curso.IdRaCentroCosto,
					NombreCurso = CursoFormulario.Curso.NombreCurso,
					IdRaTipoCurso = CursoFormulario.Curso.IdRaTipoCurso,
					Silabo = CursoFormulario.Curso.Silabo,
					IdExpositor = CursoFormulario.Curso.IdExpositor,
					PorcentajeAsistencia = CursoFormulario.Curso.PorcentajeAsistencia,
					Orden = CursoFormulario.Curso.Orden,
					Grupo = CursoFormulario.Curso.Grupo,
					PlazoPagoDias = CursoFormulario.Curso.PlazoPagoDias,
					EsInicioAonline = CursoFormulario.Curso.EsInicioAonline,
					IdMoneda = CursoFormulario.Curso.IdMoneda,
					CostoHora = CursoFormulario.Curso.CostoHora,
					IdRaTipoContrato = CursoFormulario.Curso.IdRaTipoContrato,
					IdMonedaTipoCambio = CursoFormulario.Curso.IdMonedaTipoCambio,
					TipoCambio = CursoFormulario.Curso.TipoCambio,
					FechaTipoCambio = CursoFormulario.Curso.FechaTipoCambio,
					Finalizado = false,
					Estado = true,
					UsuarioCreacion = CursoFormulario.Usuario,
					UsuarioModificacion = CursoFormulario.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
				};
				_repCurso.Insert(Curso);
				return Ok(Curso.Id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFiltroDependienteCentroCosto()
        {
            try
            {
                RaCursoRepositorio _repCursoRepositorio = new RaCursoRepositorio();
                return Ok(_repCursoRepositorio.ObtenerFiltroDependienteCentroCosto());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

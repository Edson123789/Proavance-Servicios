using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CriterioEvaluacion")]
    [ApiController]
	public class CriterioEvaluacionController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;

		public CriterioEvaluacionController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}		

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarCriterio([FromBody] CriterioEvaluacionInsertarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
                CriterioEvaluacionTipoProgramaRepositorio RepTipoPrograma = new CriterioEvaluacionTipoProgramaRepositorio();
                CriterioEvaluacionModalidadCursoRepositorio RepModalidadCurso = new CriterioEvaluacionModalidadCursoRepositorio();
                CriterioEvaluacionTipoPersonaRepositorio ReptipoPersona = new CriterioEvaluacionTipoPersonaRepositorio();
				List<CriterioEvaluacionTipoProgramaBO> listatipoprograma = new List<CriterioEvaluacionTipoProgramaBO>();
				List<CriterioEvaluacionModalidadCursoBO> listamodalidad = new List<CriterioEvaluacionModalidadCursoBO>();
				List<CriterioEvaluacionTipoPersonaBO> listaTipoPersona = new List<CriterioEvaluacionTipoPersonaBO>();
				CriterioEvaluacionBO criterio = new CriterioEvaluacionBO();				

				criterio.Nombre = Json.Nombre;
				criterio.IdCriterioEvaluacionCategoria = Json.IdCriterioEvaluacionCategoria;

                criterio.IdFormaCalificacionEvaluacion = Json.IdFormaCalificacionEvaluacion;
                criterio.IdFormaCalculoEvaluacion = Json.IdFormaCalculoEvaluacion;
                criterio.IdFormaCalculoEvaluacionParametro = Json.IdFormaCalculoEvaluacionParametro;

				criterio.UsuarioCreacion = Json.Usuario;
				criterio.UsuarioModificacion = Json.Usuario;
				criterio.FechaCreacion = DateTime.Now;
				criterio.FechaModificacion = DateTime.Now;
				criterio.Estado = true;

                //añade la lista de parametros
                if (Json.ListadoParametro != null && Json.ListadoParametro.Count > 0)
                {
                    criterio.ListadoParametro = new List<ParametroEvaluacionBO>();
                    criterio.ListadoParametro.AddRange(Json.ListadoParametro.Select(s =>
                        new ParametroEvaluacionBO()
                        {
							IdEscalaCalificacion = s.IdEscalaCalificacion,
                            Nombre = s.Nombre,
                            Ponderacion = s.Ponderacion,

                            Estado = true,
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }));
                }

				repCriterioE.Insert(criterio);
				//CriterioEvaluacionBO criterio2 = new CriterioEvaluacionBO();

				foreach (var item in Json.IdTipoPrograma)
                {
					CriterioEvaluacionTipoProgramaBO tipoprograma = new CriterioEvaluacionTipoProgramaBO();
					tipoprograma.IdCriterioEvaluacion = criterio.Id;
					tipoprograma.IdTipoPrograma = item;
					tipoprograma.UsuarioCreacion = Json.Usuario;
					tipoprograma.UsuarioModificacion = Json.Usuario;
					tipoprograma.FechaCreacion = DateTime.Now;
					tipoprograma.FechaModificacion = DateTime.Now;
					tipoprograma.Estado = true;
					listatipoprograma.Add(tipoprograma);
					RepTipoPrograma.Insert(tipoprograma);
				}
				foreach (var modalidad in Json.IdModalidadCurso)
				{
					CriterioEvaluacionModalidadCursoBO modalidadcurso = new CriterioEvaluacionModalidadCursoBO();
					modalidadcurso.IdCriterioEvaluacion = criterio.Id;
					modalidadcurso.IdModalidadCurso = modalidad;
					modalidadcurso.UsuarioCreacion = Json.Usuario;
					modalidadcurso.UsuarioModificacion = Json.Usuario;
					modalidadcurso.FechaCreacion = DateTime.Now;
					modalidadcurso.FechaModificacion = DateTime.Now;
					modalidadcurso.Estado = true;
					listamodalidad.Add(modalidadcurso);
					RepModalidadCurso.Insert(modalidadcurso);					
				}

				foreach (var modalidad in Json.IdTipoPersona)
				{
					CriterioEvaluacionTipoPersonaBO tipoPersonaBO = new CriterioEvaluacionTipoPersonaBO();
					tipoPersonaBO.IdCriterioEvaluacion = criterio.Id;
					tipoPersonaBO.IdTipoPersona = modalidad;
					tipoPersonaBO.UsuarioCreacion = Json.Usuario;
					tipoPersonaBO.UsuarioModificacion = Json.Usuario;
					tipoPersonaBO.FechaCreacion = DateTime.Now;
					tipoPersonaBO.FechaModificacion = DateTime.Now;
                    tipoPersonaBO.Estado = true;
					listaTipoPersona.Add(tipoPersonaBO);
                    ReptipoPersona.Insert(tipoPersonaBO);					
				}

				
				
				criterio.IdTipoPrograma = listatipoprograma;
				criterio.IdModalidadCurso = listamodalidad;
				criterio.IdTipoPersona = listaTipoPersona;

				return Ok(criterio);				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
				
		}

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarCriterio([FromBody] CriterioEvaluacionActualizarDTO Json)
			{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
				CriterioEvaluacionTipoProgramaRepositorio RepTipoPrograma = new CriterioEvaluacionTipoProgramaRepositorio();
				CriterioEvaluacionModalidadCursoRepositorio RepModalidadCurso = new CriterioEvaluacionModalidadCursoRepositorio();
				CriterioEvaluacionTipoPersonaRepositorio RepTipoPersona = new CriterioEvaluacionTipoPersonaRepositorio();
                ParametroEvaluacionRepositorio repParametro = new ParametroEvaluacionRepositorio();
				CriterioEvaluacionBO criterio = new CriterioEvaluacionBO();

				List<CriterioEvaluacionTipoProgramaBO> listaTipoPrograma = new List<CriterioEvaluacionTipoProgramaBO>();
				List<CriterioEvaluacionModalidadCursoBO> listaModalidad = new List<CriterioEvaluacionModalidadCursoBO>();
				List<CriterioEvaluacionTipoPersonaBO> listaTipoPersona = new List<CriterioEvaluacionTipoPersonaBO>();

				RepTipoPrograma.DeleteLogicoPorCriterio(Json.Id, Json.Usuario, Json.IdTipoPrograma);
				RepModalidadCurso.DeleteLogicoPorCriterio(Json.Id, Json.Usuario, Json.IdModalidadCurso);
				RepTipoPersona.DeleteLogicoPorCriterio(Json.Id, Json.Usuario, Json.IdTipoPersona);

				if (repCriterioE.Exist(Json.Id))
				{
					criterio = repCriterioE.FirstById(Json.Id);
					criterio.Nombre = Json.Nombre;
					criterio.IdCriterioEvaluacionCategoria = Json.IdCriterioEvaluacionCategoria;

                    criterio.IdFormaCalificacionEvaluacion = Json.IdFormaCalificacionEvaluacion;
                    criterio.IdFormaCalculoEvaluacion = Json.IdFormaCalculoEvaluacion;
                    criterio.IdFormaCalculoEvaluacionParametro = Json.IdFormaCalculoEvaluacionParametro;

                    criterio.UsuarioModificacion = Json.Usuario;
					criterio.FechaModificacion = DateTime.Now;

					
                    var listadoParametroExistente =
                        repParametro.GetBy(w => w.IdCriterioEvaluacion == Json.Id);
					List<int> listadoIdParametroExistente = new List<int>();
                    var listadoEliminar = new List<int>();

					//añade la lista de detalles
					if (Json.ListadoParametro != null && Json.ListadoParametro.Count > 0)
					{
                        criterio.ListadoParametro = new List<ParametroEvaluacionBO>();
						listadoIdParametroExistente = listadoParametroExistente.Select(s => s.Id).ToList();

						var listadoNuevo = new List<ParametroEvaluacionBO>();
						var listadoActualizar = new List<ParametroEvaluacionBO>();

						listadoNuevo.AddRange(Json.ListadoParametro.Where(w => w.Id == null || w.Id == 0).Select(s =>
							new ParametroEvaluacionBO()
							{
								IdEscalaCalificacion = s.IdEscalaCalificacion,
								Nombre = s.Nombre,
								Ponderacion = s.Ponderacion,

								Estado = true,
								UsuarioCreacion = Json.Usuario,
								UsuarioModificacion = Json.Usuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							}));
						foreach (var detalleExistente in listadoParametroExistente.Where(w => Json.ListadoParametro.Select(s => s.Id).Contains(w.Id)))
						{
							var itemActualizado = Json.ListadoParametro.FirstOrDefault(f => f.Id == detalleExistente.Id);

                            detalleExistente.IdEscalaCalificacion = itemActualizado.IdEscalaCalificacion;
							detalleExistente.Nombre = itemActualizado.Nombre;
							detalleExistente.Ponderacion = itemActualizado.Ponderacion;

							detalleExistente.UsuarioModificacion = Json.Usuario;
							detalleExistente.FechaModificacion = DateTime.Now;

							listadoActualizar.Add(detalleExistente);
						}

                        criterio.ListadoParametro.AddRange(listadoNuevo);
                        criterio.ListadoParametro.AddRange(listadoActualizar);

					}
					if (listadoIdParametroExistente != null && listadoIdParametroExistente.Count > 0)
					{
						listadoEliminar.AddRange(listadoIdParametroExistente.Where(w =>
							!Json.ListadoParametro.Select(s => s.Id).Contains(w)));
					}

					repCriterioE.Update(criterio);
                    repParametro.Delete(listadoEliminar, Json.Usuario);
				}
				
				foreach (var tipo in Json.IdTipoPrograma)
				{
					CriterioEvaluacionTipoProgramaBO tipoprograma;
					tipoprograma = RepTipoPrograma.FirstBy(x => x.IdTipoPrograma == tipo && x.IdCriterioEvaluacion == criterio.Id);
					if (tipoprograma != null)
                    {
						tipoprograma.IdTipoPrograma = tipo;
						tipoprograma.UsuarioModificacion = Json.Usuario;
						tipoprograma.FechaModificacion = DateTime.Now;
						tipoprograma.Estado = true;
						RepTipoPrograma.Update(tipoprograma);
					}
					else
                    {
						tipoprograma = new CriterioEvaluacionTipoProgramaBO();
						tipoprograma.IdCriterioEvaluacion = criterio.Id;
						tipoprograma.IdTipoPrograma = tipo;						
						tipoprograma.UsuarioCreacion = Json.Usuario;
						tipoprograma.UsuarioModificacion = Json.Usuario;
						tipoprograma.FechaCreacion = DateTime.Now;
						tipoprograma.FechaModificacion = DateTime.Now;
						tipoprograma.Estado = true;
						
						RepTipoPrograma.Insert(tipoprograma);
					}
					listaTipoPrograma.Add(tipoprograma);
					
				}

				foreach(var mo in Json.IdModalidadCurso)
                {
					CriterioEvaluacionModalidadCursoBO modalidad;
					modalidad = RepModalidadCurso.FirstBy(x => x.IdModalidadCurso == mo && x.IdCriterioEvaluacion == criterio.Id);
					if (modalidad != null)
					{
						modalidad.IdModalidadCurso = mo;
						modalidad.UsuarioModificacion = Json.Usuario;
						modalidad.FechaModificacion = DateTime.Now;
						modalidad.Estado = true;
						RepModalidadCurso.Update(modalidad);
					}
					else
					{
						modalidad = new CriterioEvaluacionModalidadCursoBO();
						modalidad.IdCriterioEvaluacion = criterio.Id;
						modalidad.IdModalidadCurso = mo;
						modalidad.UsuarioCreacion = Json.Usuario;
						modalidad.UsuarioModificacion = Json.Usuario;
						modalidad.FechaCreacion = DateTime.Now;
						modalidad.FechaModificacion = DateTime.Now;
						modalidad.Estado = true;
						RepModalidadCurso.Insert(modalidad);
					}
					listaModalidad.Add(modalidad);
					
				}
                foreach(var mo in Json.IdTipoPersona)
                {
					CriterioEvaluacionTipoPersonaBO tipoPersona;
                    tipoPersona = RepTipoPersona.FirstBy(x => x.IdTipoPersona == mo && x.IdCriterioEvaluacion == criterio.Id);
					if (tipoPersona != null)
					{
						tipoPersona.IdTipoPersona = mo;
						tipoPersona.UsuarioModificacion = Json.Usuario;
						tipoPersona.FechaModificacion = DateTime.Now;
                        tipoPersona.Estado = true;
						RepTipoPersona.Update(tipoPersona);
					}
					else
					{
                        tipoPersona = new CriterioEvaluacionTipoPersonaBO();
						tipoPersona.IdCriterioEvaluacion = criterio.Id;
						tipoPersona.IdTipoPersona = mo;
						tipoPersona.UsuarioCreacion = Json.Usuario;
						tipoPersona.UsuarioModificacion = Json.Usuario;
						tipoPersona.FechaCreacion = DateTime.Now;
						tipoPersona.FechaModificacion = DateTime.Now;
                        tipoPersona.Estado = true;
                        RepTipoPersona.Insert(tipoPersona);
					}
					listaTipoPersona.Add(tipoPersona);
					
				}
				
				criterio.IdTipoPrograma = listaTipoPrograma;
				criterio.IdModalidadCurso = listaModalidad;
				criterio.IdTipoPersona = listaTipoPersona;

                criterio.IdFormaCalificacionEvaluacion = Json.IdFormaCalificacionEvaluacion;
                criterio.IdFormaCalculoEvaluacion = Json.IdFormaCalculoEvaluacion;
                criterio.IdFormaCalculoEvaluacionParametro = Json.IdFormaCalculoEvaluacionParametro;

				Json.Id = criterio.Id;				

				return Ok(criterio);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		
		
		[Route("[action]")]
		[HttpDelete]
		public ActionResult EliminarCriterio([FromBody] CriterioEvaluacionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				
				CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
				CriterioEvaluacionTipoProgramaRepositorio RepTipoPrograma = new CriterioEvaluacionTipoProgramaRepositorio();
				CriterioEvaluacionModalidadCursoRepositorio RepModalidadCurso = new CriterioEvaluacionModalidadCursoRepositorio();

				CriterioEvaluacionBO criterio = new CriterioEvaluacionBO();
				bool result = false;
                if (repCriterioE.Exist(Json.Id))
                {
                    result = repCriterioE.Delete(Json.Id, Json.Usuario);
                }

                //if (RepTipoPrograma.Exist(criterio.Id))
                //{
                //	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);

                //}

                //if (RepModalidadCurso.Exist(criterio.Id))
                //{
                //	result = RepTipoPrograma.Delete(criterio.Id, Json.Usuario);
                //}

                return Ok(result);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}		

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosTipoPrograma()
		{

			try
			{
				TipoProgramaRepositorio combosTipoPrograma = new TipoProgramaRepositorio();
				var cmbTP = new {TipoPrograma = combosTipoPrograma.ObtenerFiltro()};				
				return Ok(cmbTP);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosModalidad()
		{

			try
			{
				ModalidadCursoRepositorio combosModalidad = new ModalidadCursoRepositorio();
				var cmbModalidad = new { Modalidad = combosModalidad.ObtenerModalidadCursoFiltro() };
				return Ok(cmbModalidad);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        [Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosTipoPersona()
		{

			try
			{
				TipoPersonaRepositorio _repTipoPersona = new TipoPersonaRepositorio();
				var cmbTipoPersona = new { TipoPersona = _repTipoPersona.ObtenerFiltroCriterioEvaluacion() };
				return Ok(cmbTipoPersona);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosCategoria()
		{

			try
			{
				CriterioEvaluacionCategoriaRepositorio combosModalidad = new CriterioEvaluacionCategoriaRepositorio();
				var cmbCategoria = new { Categoria = combosModalidad.ObtenerFiltro() };
				return Ok(cmbCategoria);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ObtenerCriterios([FromBody] FiltroPaginadorDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
				var valor = true;
				var lista = repCriterioE.ListarCriterios(Filtro);
				var registro = lista.FirstOrDefault();

				return Ok(new { Data = lista });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerCombosCriteriosEvaluacion()
		{

			try
			{
				CriterioEvaluacionRepositorio ce = new CriterioEvaluacionRepositorio();
				var cmbCategoria = new { criterioevaluacion = ce.ListarCriteriosEvaluacion() };
				return Ok(cmbCategoria);

				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]/{IdCriterioEvaluacion}")]
		[HttpGet]
		public IActionResult ObtenerDetallesCriteriosEvaluacion(int IdCriterioEvaluacion)
		{

			try
			{
				CriterioEvaluacionRepositorio repCriterioE = new CriterioEvaluacionRepositorio();
				CriterioEvaluacionTipoProgramaRepositorio RepTipoPrograma = new CriterioEvaluacionTipoProgramaRepositorio();
				CriterioEvaluacionModalidadCursoRepositorio RepModalidadCurso = new CriterioEvaluacionModalidadCursoRepositorio();
				CriterioEvaluacionTipoPersonaRepositorio RepTipoPersona = new CriterioEvaluacionTipoPersonaRepositorio();
				var Detalles = new {
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					detalletipoprograma = RepTipoPrograma.ListarCriteriosEvaluacionTipoProgramaEspecifico(IdCriterioEvaluacion),
					detallemodalidad = RepModalidadCurso.ListarCriteriosEvaluacionModalidadCursoEspecifico(IdCriterioEvaluacion),
					detalleTipoPersona = RepTipoPersona.ListarCriteriosEvaluacionTipoPersona(IdCriterioEvaluacion)
				};
				return Ok(Detalles);


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		
        [Route("[Action]/{IdCriterioEvaluacion}")]
        [HttpGet]
        public IActionResult ObtenerListadoParametros(int IdCriterioEvaluacion)
        {
            try
            {
                ParametroEvaluacionRepositorio _repoParametros = new ParametroEvaluacionRepositorio(_integraDBContext);
                var listado = _repoParametros.GetBy(w => w.IdCriterioEvaluacion == IdCriterioEvaluacion);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;



namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ControlPantallas")]

    public class ControlPantallasController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ControlPantallasController()
        {
            _integraDBContext = new integraDBContext();
        }


        [Route("[action]")]
        [HttpGet]

        public ActionResult ObtenerObtenerCombosControlPantallas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalRepositorio repAsesores = new PersonalRepositorio(_integraDBContext);
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
                FaseOportunidadRepositorio repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
                TipoCategoriaOrigenRepositorio repGrupo = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                TipoDatoRepositorio repTipoDato = new TipoDatoRepositorio(_integraDBContext);

                var listaAsesores = repAsesores.ObtenerTodoAsesorCoordinadorVentas();
				listaAsesores.Add(new Aplicacion.DTOs.AsesorNombreFiltroDTO { Id = -1, NombreCompleto = "Todos los asesores" });
				listaAsesores = listaAsesores.OrderBy(x => x.Id).ToList();
                var listaCentroCosto = repCentroCosto.ObtenerTodoFiltro();
				listaCentroCosto.Add(new Aplicacion.DTOs.FiltroDTO { Id = -1, Nombre = "Todos los Centros de Costo" });
				listaCentroCosto = listaCentroCosto.OrderBy(x => x.Id).ToList();
				var listaFaseOportunidad = repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro();
				listaFaseOportunidad.Add(new Aplicacion.DTOs.FaseOportunidadFiltroDTO { Id= -1, Nombre="Todas las Fases de Oportunidad"});
				listaFaseOportunidad = listaFaseOportunidad.OrderBy(x => x.Id).ToList();
                var listaGrupo = repGrupo.ObtenerTodoFiltro();
				listaGrupo.Add(new Aplicacion.DTOs.FiltroDTO {Id=-1,Nombre="Todos los Grupos" });
				listaGrupo = listaGrupo.OrderBy(x => x.Id).ToList();
				var listaTipoDato = repTipoDato.ObtenerFiltro();
				listaTipoDato.Add(new Aplicacion.DTOs.FiltroDTO { Id = -1, Nombre = "Todos los Tipos de Datos" });
				listaTipoDato = listaTipoDato.OrderBy(x => x.Id).ToList();

                return Ok(new { listaAsesores, listaCentroCosto, listaFaseOportunidad, listaGrupo, listaTipoDato });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		
		/*public IActionResult InsertarControlPantallas([FromBody] ControlPantallasInsertarDTO Json)
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
				criterio.UsuarioCreacion = Json.Usuario;
				criterio.UsuarioModificacion = Json.Usuario;
				criterio.FechaCreacion = DateTime.Now;
				criterio.FechaModificacion = DateTime.Now;
				criterio.Estado = true;
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

		}*/


	}
}

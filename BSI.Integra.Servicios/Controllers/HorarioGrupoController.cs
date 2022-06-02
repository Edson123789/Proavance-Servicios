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
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioGrupoController : ControllerBase
    {
		private readonly integraDBContext _integraDBContext;

		public HorarioGrupoController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarGrupo([FromBody] HorarioGrupoInsertarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();
				HorarioGrupoPersonalRepositorio repHorarioGrupoPersonal = new HorarioGrupoPersonalRepositorio();
				List<HorarioGrupoPersonalBO> listagrupopersonal = new List<HorarioGrupoPersonalBO>();

				HorarioGrupoBO horarioGrupo = new HorarioGrupoBO();
				horarioGrupo.Nombre = Json.Nombre;
				horarioGrupo.UsuarioCreacion = Json.Usuario;
				horarioGrupo.UsuarioModificacion = Json.Usuario;
				horarioGrupo.FechaCreacion = DateTime.Now;
				horarioGrupo.FechaModificacion = DateTime.Now;
				horarioGrupo.Estado = true;

				repHorarioGrupo.Insert(horarioGrupo);

                foreach (var item in Json.IdPersonal)
                {
                    HorarioGrupoPersonalBO grupopersonal = new HorarioGrupoPersonalBO();
                    grupopersonal.IdHorarioGrupo = horarioGrupo.Id;
                    grupopersonal.IdPersonal = item;
                    grupopersonal.UsuarioCreacion = Json.Usuario;
                    grupopersonal.UsuarioModificacion = Json.Usuario;
                    grupopersonal.FechaCreacion = DateTime.Now;
                    grupopersonal.FechaModificacion = DateTime.Now;
                    grupopersonal.Estado = true;
					listagrupopersonal.Add(grupopersonal);
					repHorarioGrupoPersonal.Insert(grupopersonal);
                }
				horarioGrupo.IdPersonal = listagrupopersonal;
				return Ok(horarioGrupo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarGrupo([FromBody] HorarioGrupoInsertarDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();
				HorarioGrupoPersonalRepositorio repHorarioGrupoPersonal = new HorarioGrupoPersonalRepositorio();
				List<HorarioGrupoPersonalBO> listagrupopersonal = new List<HorarioGrupoPersonalBO>();

				HorarioGrupoBO horarioGrupo = new HorarioGrupoBO();

				repHorarioGrupoPersonal.DeleteLogicoPorGrupo(Json.Id, Json.Usuario, Json.IdPersonal);			

				if (repHorarioGrupo.Exist(Json.Id))
				{
					horarioGrupo = repHorarioGrupo.FirstById(Json.Id);
					horarioGrupo.Nombre = Json.Nombre;
					horarioGrupo.UsuarioModificacion = Json.Usuario;
					horarioGrupo.FechaModificacion = DateTime.Now;
					repHorarioGrupo.Update(horarioGrupo);
					
				}

				foreach (var tipo in Json.IdPersonal)
				{
					HorarioGrupoPersonalBO horarioGrupoPersonal;
					horarioGrupoPersonal = repHorarioGrupoPersonal.FirstBy(x => x.IdPersonal == tipo && x.IdHorarioGrupo == horarioGrupo.Id);
					if (horarioGrupoPersonal != null)
					{
						horarioGrupoPersonal.IdPersonal = tipo;
						horarioGrupoPersonal.UsuarioModificacion = Json.Usuario;
						horarioGrupoPersonal.FechaModificacion = DateTime.Now;
						horarioGrupoPersonal.Estado = true;
						listagrupopersonal.Add(horarioGrupoPersonal);
						repHorarioGrupoPersonal.Update(horarioGrupoPersonal);
					}
					else
					{
						horarioGrupoPersonal = new HorarioGrupoPersonalBO();
						horarioGrupoPersonal.IdHorarioGrupo = horarioGrupo.Id;
						horarioGrupoPersonal.IdPersonal = tipo;
						horarioGrupoPersonal.UsuarioCreacion = Json.Usuario;
						horarioGrupoPersonal.UsuarioModificacion = Json.Usuario;
						horarioGrupoPersonal.FechaCreacion = DateTime.Now;
						horarioGrupoPersonal.FechaModificacion = DateTime.Now;
						horarioGrupoPersonal.Estado = true;
						listagrupopersonal.Add(horarioGrupoPersonal);
						repHorarioGrupoPersonal.Insert(horarioGrupoPersonal);
					}			

				}
				horarioGrupo.IdPersonal = listagrupopersonal;
				Json.Id = horarioGrupo.Id;
				return Ok(horarioGrupo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[Route("[action]")]
		[HttpDelete]
		public ActionResult EliminarGrupo([FromBody] CriterioEvaluacionDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();
				HorarioGrupoPersonalRepositorio repHorarioGrupoPersonal = new HorarioGrupoPersonalRepositorio();

				HorarioGrupoBO horariogrupo = new HorarioGrupoBO();
				bool result = false;
				if (repHorarioGrupo.Exist(Json.Id))
				{
					result = repHorarioGrupo.Delete(Json.Id, Json.Usuario);
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

		[Route("[Action]")]
		[HttpGet]
		public ActionResult comboPersonal()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				var personal = new { personal = _repPersonal.getDatosPersonal() };
				return Ok(personal);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]")]
		[HttpGet]
		public ActionResult getGrupos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HorarioGrupoRepositorio repHorarioGrupo = new HorarioGrupoRepositorio();
				var grupos = new { grupos = repHorarioGrupo.getGrupos() };
				return Ok(grupos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[Action]/{IdGrupo}")]
		[HttpGet]
		public IActionResult ObtenerDetalles(int IdGrupo)
		{

			try
			{
				HorarioGrupoPersonalRepositorio _repConfiguracionMarcacionGrupo = new HorarioGrupoPersonalRepositorio();
				var Detalles = new
				{
					//detallecategoria = repCriterioE.ObtenerModalidadPorId(IdCriterioEvaluacion),
					detalletipoprograma = _repConfiguracionMarcacionGrupo.listarGrupoPersonal(IdGrupo)
				};
				return Ok(Detalles);


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


	}
}

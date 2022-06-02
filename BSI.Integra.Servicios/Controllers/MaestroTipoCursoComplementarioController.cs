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
	[Route("api/MaestroTipoCursoComplementario")]
	[ApiController]
	public class MaestroTipoCursoComplementarioController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly TipoCompetenciaTecnicaRepositorio _repTipoCompetenciaTecnica;

		public MaestroTipoCursoComplementarioController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repTipoCompetenciaTecnica = new TipoCompetenciaTecnicaRepositorio(_integraDBContext);
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerTipoCursoComplementario()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaTipoCompetenciaTecnica = _repTipoCompetenciaTecnica.ObtenerListaParaFiltro();
				return Ok(listaTipoCompetenciaTecnica);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarTipoCursoComplementario([FromBody]FiltroIdUsuarioNombreDTO TipoCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				TipoCompetenciaTecnicaBO tipoCompetenciaTecnica = new TipoCompetenciaTecnicaBO()
				{
					Nombre = TipoCursoComplementario.Nombre,
					Estado = true,
					UsuarioCreacion = TipoCursoComplementario.Usuario,
					UsuarioModificacion = TipoCursoComplementario.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var res = _repTipoCompetenciaTecnica.Insert(tipoCompetenciaTecnica);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarTipoCursoComplementario([FromBody]FiltroIdUsuarioNombreDTO TipoCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var tipoCompetenciaTecnica = _repTipoCompetenciaTecnica.FirstById(TipoCursoComplementario.Id);
				tipoCompetenciaTecnica.Nombre = TipoCursoComplementario.Nombre;
				tipoCompetenciaTecnica.UsuarioModificacion = TipoCursoComplementario.Usuario;
				tipoCompetenciaTecnica.FechaModificacion = DateTime.Now;
				var res = _repTipoCompetenciaTecnica.Update(tipoCompetenciaTecnica);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarTipoCursoComplementario([FromBody]EliminarDTO TipoCursoComplementario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repTipoCompetenciaTecnica.Exist(TipoCursoComplementario.Id))
				{
					var res = _repTipoCompetenciaTecnica.Delete(TipoCursoComplementario.Id, TipoCursoComplementario.NombreUsuario);
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

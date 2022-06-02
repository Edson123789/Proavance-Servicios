using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Skype")]
    
    public class SkypeReunionOpeController : Controller
    {
		private string UrlBase = "https://meet.lync.com/bsgrupo/bsginstitute/";
		// GET: api/WebinarOpe
		[HttpGet]
		[Route("[Action]/{IdCentroCosto}")]
		public ActionResult ObtenerListado(int IdCentroCosto)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaSkypeReunionRepositorio _repSkypeReunion = new RaSkypeReunionRepositorio();
				List<SkypeReunionDTO> skypeReunion = _repSkypeReunion.GetBy(x => x.IdRaCentroCosto == IdCentroCosto, x => new SkypeReunionDTO { Id = x.Id, IdRaCentroCosto = x.IdRaCentroCosto, ReunionId = x.ReunionId, UrlBase = x.UrlBase, Activo = x.Activo, FechaModificacion = x.FechaModificacion }).ToList();
				return Ok(skypeReunion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

		[HttpPost]
		[Route("[Action]")]
		public ActionResult DesactivarReunion([FromBody]CompuestoSkypeReunionDTO CompuestoSkypeReunion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaSkypeReunionRepositorio _repSkypeReunionRepositorio = new RaSkypeReunionRepositorio();
				if (_repSkypeReunionRepositorio.Exist(CompuestoSkypeReunion.SkypeReunion.Id))
				{
					var skypeReunion = _repSkypeReunionRepositorio.FirstById(CompuestoSkypeReunion.SkypeReunion.Id);
					skypeReunion.Activo = false;
					skypeReunion.UsuarioModificacion = CompuestoSkypeReunion.Usuario;
					skypeReunion.FechaModificacion = DateTime.Now;

					_repSkypeReunionRepositorio.Update(skypeReunion);
					return Ok(skypeReunion.Activo);
				}
				else
				{
					return BadRequest("Esta reunion no existe");
				}
				
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult RegistrarReunion([FromBody]CompuestoSkypeReunionDTO CompuestoSkypeReunion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaSkypeReunionRepositorio _repSkypeReunionRepositorio = new RaSkypeReunionRepositorio();

				foreach (var reunionAnterior in _repSkypeReunionRepositorio.GetBy(w => w.IdRaCentroCosto == CompuestoSkypeReunion.SkypeReunion.IdRaCentroCosto, w => new { w.Id }))
				{
					var reunion = _repSkypeReunionRepositorio.FirstById(reunionAnterior.Id);
					reunion.Activo = false;
					reunion.UsuarioModificacion = CompuestoSkypeReunion.Usuario;
					reunion.FechaModificacion = DateTime.Now;
					_repSkypeReunionRepositorio.Update(reunion);
				}
				RaSkypeReunionBO raSkypeReunion = new RaSkypeReunionBO();
				raSkypeReunion.IdRaCentroCosto = CompuestoSkypeReunion.SkypeReunion.IdRaCentroCosto;
				raSkypeReunion.ReunionId = CompuestoSkypeReunion.SkypeReunion.ReunionId;
				raSkypeReunion.UrlBase = UrlBase;
				raSkypeReunion.Activo = true;
				raSkypeReunion.Estado = true;
				raSkypeReunion.UsuarioCreacion = CompuestoSkypeReunion.Usuario;
				raSkypeReunion.UsuarioModificacion = CompuestoSkypeReunion.Usuario;
				raSkypeReunion.FechaCreacion = DateTime.Now;
				raSkypeReunion.FechaModificacion = DateTime.Now;

				_repSkypeReunionRepositorio.Insert(raSkypeReunion);
				return Ok(raSkypeReunion.Id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

	}
}

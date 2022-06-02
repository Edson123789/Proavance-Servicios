using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/Jerarquia")]
    [ApiController]
    public class JerarquiaOpeController : ControllerBase
    {
		// GET: api/JerarquiaOpe
		[Route("[Action]")]
		[HttpGet]
        public ActionResult ObtenerCombosJerarquia()
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				RaJerarquiaRepositorio _repJerarquia = new RaJerarquiaRepositorio();
				var listaPersonal = _repPersonal.ObtenerPersonalFiltro();
				var listaJerarquia = _repJerarquia.ObtenerJerarquias();
				return Ok(new { Personal = listaPersonal, Jerarquia = listaJerarquia });

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarJerarquia([FromBody] CompuestoJerarquiaDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaJerarquiaRepositorio _repJerarquia = new RaJerarquiaRepositorio();
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				RaJerarquiaBO jerarquia = new RaJerarquiaBO();
				using (TransactionScope scope = new TransactionScope())
				{
					if (_repJerarquia.Exist(Json.Jerarquia.Id))
					{
						jerarquia = _repJerarquia.FirstById(Json.Jerarquia.Id);
						jerarquia.IdUsuarioSubordinado = Json.Jerarquia.IdSubordinado;
						jerarquia.IdUsuarioJefe = Json.Jerarquia.IdJefe;
						jerarquia.UsuarioModificacion = Json.Usuario;
						jerarquia.FechaModificacion = DateTime.Now;
						_repJerarquia.Update(jerarquia);
					}
					scope.Complete();
					Json.Jerarquia.Id = jerarquia.Id;
				}

				var NombresJefe = _repPersonal.GetBy(x => x.Id == jerarquia.IdUsuarioJefe, x => new { NombresJefe = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();
				var NombresSubordinado = _repPersonal.GetBy(x => x.Id == jerarquia.IdUsuarioSubordinado, x => new { NombresSubordinado = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();
				return Ok(new { jerarquia.Id, IdJefe = jerarquia.IdUsuarioJefe, IdSubordinado = jerarquia.IdUsuarioSubordinado, NombresJefe.NombresJefe, NombresSubordinado.NombresSubordinado });
				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarJerarquia([FromBody] CompuestoJerarquiaDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaJerarquiaRepositorio _repJerarquia = new RaJerarquiaRepositorio();
				PersonalRepositorio _repPersonal = new PersonalRepositorio();
				RaJerarquiaBO jerarquia = new RaJerarquiaBO();
				using (TransactionScope scope = new TransactionScope())
				{

					jerarquia.IdUsuarioSubordinado = Json.Jerarquia.IdSubordinado;
					jerarquia.IdUsuarioJefe = Json.Jerarquia.IdJefe;
					jerarquia.UsuarioCreacion = Json.Usuario;
					jerarquia.UsuarioModificacion = Json.Usuario;
					jerarquia.FechaCreacion = DateTime.Now;
					jerarquia.FechaModificacion = DateTime.Now;
					jerarquia.Estado = true;
					_repJerarquia.Insert(jerarquia);
					scope.Complete();
				}
				Json.Jerarquia.Id = jerarquia.Id;

				var NombresJefe = _repPersonal.GetBy(x => x.Id == jerarquia.IdUsuarioJefe, x => new { NombresJefe = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();
				var NombresSubordinado = _repPersonal.GetBy(x => x.Id == jerarquia.IdUsuarioSubordinado, x => new { NombresSubordinado = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();
				return Ok(new { jerarquia.Id,IdJefe = jerarquia.IdUsuarioJefe, IdSubordinado = jerarquia.IdUsuarioSubordinado,NombresJefe.NombresJefe,NombresSubordinado.NombresSubordinado});
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

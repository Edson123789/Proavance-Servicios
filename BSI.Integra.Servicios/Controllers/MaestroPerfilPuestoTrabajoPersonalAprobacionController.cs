using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/MaestroPerfilPuestoTrabajoPersonalAprobacion")]
	[ApiController]
	public class MaestroPerfilPuestoTrabajoPersonalAprobacionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PerfilPuestoTrabajoPersonalAprobacionRepositorio _repPerfilPuestoTrabajoPersonalAprobacion;
		private readonly PersonalRepositorio _repPersonal;
		private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;

		public MaestroPerfilPuestoTrabajoPersonalAprobacionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPerfilPuestoTrabajoPersonalAprobacion = new PerfilPuestoTrabajoPersonalAprobacionRepositorio(_integraDBContext);
			_repPersonal = new PersonalRepositorio(_integraDBContext);
			_repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
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
				var listaPersonal = _repPersonal.getDatosPersonal();
				var listaPuestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre();
				return Ok(new
				{
					ListaPersonal = listaPersonal,
					ListaPuestoTrabajo = listaPuestoTrabajo.OrderBy(x => x.Nombre).ToList()
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPerfilPuestoTrabajoPersonalAprobacion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPerfilPuestoTrabajoPersonalAprobacion = _repPerfilPuestoTrabajoPersonalAprobacion.ObtenerPersonalConfigurado();
				var agrupado = listaPerfilPuestoTrabajoPersonalAprobacion.GroupBy(x => new { x.IdPersonal, x.Personal }).Select(x => new PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO
				{
					IdPersonal = x.Key.IdPersonal,
					Personal = x.Key.Personal,
					ListaPuestoTrabajo = x.GroupBy(y => y.IdPuestoTrabajo).Select(y => y.Key).ToList(),
					PuestoTrabajo = x.GroupBy(z => z.PuestoTrabajo).Select(z => z.Key).ToList(),
				}).ToList();
				return Ok(agrupado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPerfilPuestoTrabajoPersonalAprobacion([FromBody]PerfilPuestoTrabajoPersonalAprobacionDTO PerfilPuestoTrabajoPersonalAprobacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var res = InsertarActualizarConfiguracion(PerfilPuestoTrabajoPersonalAprobacion);

				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPerfilPuestoTrabajoPersonalAprobacion([FromBody]PerfilPuestoTrabajoPersonalAprobacionDTO PerfilPuestoTrabajoPersonalAprobacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				foreach (var personal in PerfilPuestoTrabajoPersonalAprobacion.ListaPersonal)
				{
					var configuracionPersonal = _repPerfilPuestoTrabajoPersonalAprobacion.GetBy(x => x.IdPersonal == personal).ToList();
					foreach (var conf in configuracionPersonal)
					{
						var listaConfiguracion = PerfilPuestoTrabajoPersonalAprobacion.ListaPersonal.Contains(personal) && PerfilPuestoTrabajoPersonalAprobacion.ListaPuestoTrabajo.Contains(conf.IdPuestoTrabajo);
						if (!listaConfiguracion)
						{
							_repPerfilPuestoTrabajoPersonalAprobacion.Delete(conf.Id, PerfilPuestoTrabajoPersonalAprobacion.Usuario);
						}
					}
				}

				var res = InsertarActualizarConfiguracion(PerfilPuestoTrabajoPersonalAprobacion);

				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPerfilPuestoTrabajoPersonalAprobacion([FromBody]EliminarDTO PerfilPuestoTrabajoPersonalAprobacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var configuracion = _repPerfilPuestoTrabajoPersonalAprobacion.GetBy(x => x.IdPersonal == PerfilPuestoTrabajoPersonalAprobacion.Id).ToList();
				foreach(var item in configuracion)
				{
					if (_repPerfilPuestoTrabajoPersonalAprobacion.Exist(item.Id))
					{
						var res = _repPerfilPuestoTrabajoPersonalAprobacion.Delete(item.Id, PerfilPuestoTrabajoPersonalAprobacion.NombreUsuario);
					}
					else
					{
						return BadRequest("La entidad a eliminar no existe o ya fue eliminada.");
					}
				}
				return Ok(true);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		public bool InsertarActualizarConfiguracion(PerfilPuestoTrabajoPersonalAprobacionDTO PerfilPuestoTrabajoPersonalAprobacion)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				try
				{
					foreach (var personal in PerfilPuestoTrabajoPersonalAprobacion.ListaPersonal)
					{
						foreach (var puestoTrabajo in PerfilPuestoTrabajoPersonalAprobacion.ListaPuestoTrabajo)
						{
							PerfilPuestoTrabajoPersonalAprobacionBO configuracionPersonal;
							configuracionPersonal = _repPerfilPuestoTrabajoPersonalAprobacion.FirstBy(x => x.IdPersonal == personal && x.IdPuestoTrabajo == puestoTrabajo);
							if (configuracionPersonal != null)
							{
								configuracionPersonal.UsuarioModificacion = PerfilPuestoTrabajoPersonalAprobacion.Usuario;
								configuracionPersonal.FechaModificacion = DateTime.Now;
								_repPerfilPuestoTrabajoPersonalAprobacion.Update(configuracionPersonal);
							}
							else
							{
								configuracionPersonal = new PerfilPuestoTrabajoPersonalAprobacionBO();
								configuracionPersonal.IdPersonal = personal;
								configuracionPersonal.IdPuestoTrabajo = puestoTrabajo;
								configuracionPersonal.Estado = true;
								configuracionPersonal.UsuarioCreacion = PerfilPuestoTrabajoPersonalAprobacion.Usuario;
								configuracionPersonal.UsuarioModificacion = PerfilPuestoTrabajoPersonalAprobacion.Usuario;
								configuracionPersonal.FechaCreacion = DateTime.Now;
								configuracionPersonal.FechaModificacion = DateTime.Now;
								_repPerfilPuestoTrabajoPersonalAprobacion.Insert(configuracionPersonal);
							}
						}
					}
					scope.Complete();
					return true;
				}
				catch (Exception e)
				{
					scope.Dispose();
					return false;
				}
			}
		}
	}
}

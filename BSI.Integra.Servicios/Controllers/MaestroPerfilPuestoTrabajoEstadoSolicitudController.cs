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
	/// Controlador: MaestroPerfilPuestoTrabajoEstadoSolicitud
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Estado Solicitud
	/// </summary>
	[Route("api/MaestroPerfilPuestoTrabajoEstadoSolicitud")]
	[ApiController]
	public class MaestroPerfilPuestoTrabajoEstadoSolicitudController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PerfilPuestoTrabajoEstadoSolicitudRepositorio _repPerfilPuestoTrabajoEstadoSolicitud;

		public MaestroPerfilPuestoTrabajoEstadoSolicitudController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPerfilPuestoTrabajoEstadoSolicitud = new PerfilPuestoTrabajoEstadoSolicitudRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Perfiles de  Estado de Solicitud de Puestos de Trabajos
		/// </summary>
		/// <returns>List<FiltroIdNombreDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPerfilPuestoTrabajoEstadoSolicitud()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPerfilPuestoTrabajoEstadoSolicitud = _repPerfilPuestoTrabajoEstadoSolicitud.ObtenerPerfilPuestoTrabajoEstadoSolicitud();
				return Ok(listaPerfilPuestoTrabajoEstadoSolicitud);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta Registros de Estado de Solicitud de Puestos de Trabajo
		/// </summary>
		/// <param name="PerfilPuestoTrabajoEstadoSolicitud">Información Compuesta de Estado de Solicitud de Puesto de Trabajo</param>
		/// <returns>Bool</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPerfilPuestoTrabajoEstadoSolicitud([FromBody]FiltroIdUsuarioNombreDTO PerfilPuestoTrabajoEstadoSolicitud)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PerfilPuestoTrabajoEstadoSolicitudBO perfilPuestoTrabajoEstadoSolicitud = new PerfilPuestoTrabajoEstadoSolicitudBO()
				{
					Nombre = PerfilPuestoTrabajoEstadoSolicitud.Nombre,
					Estado = true,
					UsuarioCreacion = PerfilPuestoTrabajoEstadoSolicitud.Usuario,
					UsuarioModificacion = PerfilPuestoTrabajoEstadoSolicitud.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repPerfilPuestoTrabajoEstadoSolicitud.Insert(perfilPuestoTrabajoEstadoSolicitud);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualizar Registros de Estado de Solicitud de Puestos de Trabajo
		/// </summary>
		/// <param name="PerfilPuestoTrabajoEstadoSolicitud">Información Compuesta de Estado de Solicitud de Puesto de Trabajo</param>
		/// <returns>Bool</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPerfilPuestoTrabajoEstadoSolicitud([FromBody]FiltroIdUsuarioNombreDTO PerfilPuestoTrabajoEstadoSolicitud)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var perfilPuestoTrabajoEstadoSolicitud = _repPerfilPuestoTrabajoEstadoSolicitud.FirstById(PerfilPuestoTrabajoEstadoSolicitud.Id);
				perfilPuestoTrabajoEstadoSolicitud.Nombre = PerfilPuestoTrabajoEstadoSolicitud.Nombre;
				perfilPuestoTrabajoEstadoSolicitud.UsuarioModificacion = PerfilPuestoTrabajoEstadoSolicitud.Usuario;
				perfilPuestoTrabajoEstadoSolicitud.FechaModificacion = DateTime.Now;
				var resultado = _repPerfilPuestoTrabajoEstadoSolicitud.Update(perfilPuestoTrabajoEstadoSolicitud);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 07/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina Registros de Estado de Solicitud de Puestos de Trabajo
		/// </summary>
		/// <param name="PerfilPuestoTrabajoEstadoSolicitud">Información Id, Usuario de registro</param>
		/// <returns>Bool</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPerfilPuestoTrabajoEstadoSolicitud([FromBody]EliminarDTO PerfilPuestoTrabajoEstadoSolicitud)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPerfilPuestoTrabajoEstadoSolicitud.Exist(PerfilPuestoTrabajoEstadoSolicitud.Id))
				{
					var resultado = _repPerfilPuestoTrabajoEstadoSolicitud.Delete(PerfilPuestoTrabajoEstadoSolicitud.Id, PerfilPuestoTrabajoEstadoSolicitud.NombreUsuario);
					return Ok(resultado);
				}
				else
				{
					return BadRequest("La entidad a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

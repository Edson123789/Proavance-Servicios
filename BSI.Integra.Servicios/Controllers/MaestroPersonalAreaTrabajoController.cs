using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: MaestroPersonalAreaTrabajoController
	/// Autor: Luis Huallpa
	/// Fecha: 06/07/2021
	/// <summary>
	/// Gestiona Maestro de Área de Trabajo de Personal
	/// </summary>
	[Route("api/MaestroPersonalAreaTrabajo")]
	[ApiController]
	public class MaestroPersonalAreaTrabajoController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;

		public MaestroPersonalAreaTrabajoController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor:
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Area de Trabajo de Personal
		/// </summary>
		/// <returns> List<PersonalAreaTrabajoDTO> </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPersonalAreaTrabajo()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPersonalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerPersonalAreaTrabajo();
				return Ok(listaPersonalAreaTrabajo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor:
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta información de Área de Trabajo de Personal
		/// </summary>
		/// <param name="PersonalAreaTrabajo">Información Compuesta para Inserción</param>
		/// <returns> bool: COnfirmación de Inserción </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPersonalAreaTrabajo([FromBody]PersonalAreaTrabajoRegistroDTO PersonalAreaTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				string mensaje = string.Empty;
                if (PersonalAreaTrabajo.Nombre.Trim().Length > 0 && PersonalAreaTrabajo.Codigo.Trim().Length > 0)
                {
					PersonalAreaTrabajoBO personalAreaTrabajo = new PersonalAreaTrabajoBO()
					{
						Nombre = PersonalAreaTrabajo.Nombre,
						Codigo = PersonalAreaTrabajo.Codigo,
						Descripcion = "",
						Estado = true,
						UsuarioCreacion = PersonalAreaTrabajo.Usuario,
						UsuarioModificacion = PersonalAreaTrabajo.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};					
					var respuesta = _repPersonalAreaTrabajo.Insert(personalAreaTrabajo);
					mensaje = "Los datos se guardaron correctamente";
					return Ok( new { Respuesta = true, Mensaje = mensaje });
				}
                else
                {
					mensaje = "Los campos de nombre y código no pueden ser vacíos";
					return Ok(new { Respuesta = false, Mensaje = mensaje });
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor:
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza información de Área de Trabajo de Personal
		/// </summary>
		/// <param name="PersonalAreaTrabajo">Información Compuesta para Actualización</param>
		/// <returns> bool: Confirmación de Actualización </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPersonalAreaTrabajo([FromBody]PersonalAreaTrabajoRegistroDTO PersonalAreaTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				string mensaje = string.Empty;
				if (PersonalAreaTrabajo.Nombre.Trim().Length > 0 && PersonalAreaTrabajo.Codigo.Trim().Length > 0)
				{
					var personalAreaTrabajo = _repPersonalAreaTrabajo.FirstById(PersonalAreaTrabajo.Id);
					personalAreaTrabajo.Nombre = PersonalAreaTrabajo.Nombre;
					personalAreaTrabajo.Codigo = PersonalAreaTrabajo.Codigo;
					personalAreaTrabajo.UsuarioModificacion = PersonalAreaTrabajo.Usuario;
					personalAreaTrabajo.FechaModificacion = DateTime.Now;
					var respuesta = _repPersonalAreaTrabajo.Update(personalAreaTrabajo);
					mensaje = "Los datos se guardaron correctamente";
					return Ok(new { Respuesta = true, Mensaje = mensaje });
				}
				else
				{
					mensaje = "Los campos de nombre y código no pueden ser vacíos";
					return Ok(new { Respuesta = false, Mensaje = mensaje });
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor:
		/// Fecha: 06/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina información de Área de Trabajo de Personal
		/// </summary>
		/// <param name="PersonalAreaTrabajo">Id, Usuario para eliminación de registro</param>
		/// <returns> bool: Confirmación de Eliminación </returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPersonalAreaTrabajo([FromBody]EliminarDTO PersonalAreaTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPersonalAreaTrabajo.Exist(PersonalAreaTrabajo.Id))
				{
					var respuesta = _repPersonalAreaTrabajo.Delete(PersonalAreaTrabajo.Id, PersonalAreaTrabajo.NombreUsuario);
					return Ok(respuesta);
				}
				else
				{
					return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

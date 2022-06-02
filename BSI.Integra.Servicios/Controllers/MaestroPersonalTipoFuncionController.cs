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
	/// Controlador: MaestroPersonalTipoFuncionController
	/// Autor: Luis Huallpa
	/// Fecha: 08/09/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Personal Tipo Función
	/// </summary>
	[Route("api/MaestroPersonalTipoFuncion")]
	[ApiController]
	public class MaestroPersonalTipoFuncionController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PersonalTipoFuncionRepositorio _repPersonalTipoFuncion;
		public MaestroPersonalTipoFuncionController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPersonalTipoFuncion = new PersonalTipoFuncionRepositorio(_integraDBContext);
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Tipo de Función de Personal
		/// </summary>
		/// <returns>List<PersonalTipoFuncionDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerPersonalTipoFuncion()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPersonalTipoFuncion = _repPersonalTipoFuncion.ObtenerPersonalTipoFuncion();
				return Ok(listaPersonalTipoFuncion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta Registro de Tipo de Función de Personal
		/// </summary>
		/// <param name="PersonalTipoFuncion">Información Compuesta de Tipo de Función de Personal</param>
		/// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarPersonalTipoFuncion([FromBody]PersonalTipoFuncionDTO PersonalTipoFuncion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalTipoFuncionBO personalTipoFuncion = new PersonalTipoFuncionBO()
				{
					Nombre = PersonalTipoFuncion.Nombre,
					Estado = true,
					UsuarioCreacion = PersonalTipoFuncion.Usuario,
					UsuarioModificacion = PersonalTipoFuncion.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repPersonalTipoFuncion.Insert(personalTipoFuncion);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Registro de Tipo de Función de Personal
		/// </summary>
		/// <param name="PersonalTipoFuncion">Información Compuesta de Tipo de Función de Personal</param>
		/// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarPersonalTipoFuncion([FromBody]PersonalTipoFuncionDTO PersonalTipoFuncion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var personalTipoFuncion = _repPersonalTipoFuncion.FirstById(PersonalTipoFuncion.Id);
				personalTipoFuncion.Nombre = PersonalTipoFuncion.Nombre;
				personalTipoFuncion.UsuarioModificacion = PersonalTipoFuncion.Usuario;
				personalTipoFuncion.FechaModificacion = DateTime.Now;
				var resultado = _repPersonalTipoFuncion.Update(personalTipoFuncion);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa
		/// Fecha: 08/09/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina Registro de Tipo de Función de Personal
		/// </summary>
		/// <param name="PersonalTipoFuncion">Información de Id, Usuario</param>
		/// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarPersonalTipoFuncion([FromBody]EliminarDTO PersonalTipoFuncion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPersonalTipoFuncion.Exist(PersonalTipoFuncion.Id))
				{
					var resultado = _repPersonalTipoFuncion.Delete(PersonalTipoFuncion.Id, PersonalTipoFuncion.NombreUsuario);
					return Ok(resultado);
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

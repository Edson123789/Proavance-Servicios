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
	/// Controlador: MaestroCentroEstudioController
	/// Autor: Luis Huallpa
	/// Fecha: 05/08/2021
	/// <summary>
	/// Gestiona información Interfaz (M) Instituciones Educativas
	/// </summary>
	[Route("api/MaestroCentroEstudio")]
	[ApiController]
	public class MaestroCentroEstudioController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly CentroEstudioRepositorio _repCentroEstudio;
		private readonly PaisRepositorio _repPais;
		private readonly CiudadRepositorio _repCiudad;

		public MaestroCentroEstudioController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repCentroEstudio = new CentroEstudioRepositorio(_integraDBContext);
			_repPais = new PaisRepositorio(_integraDBContext);
			_repCiudad = new CiudadRepositorio(_integraDBContext);
		}

		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 05/08/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos para Interfaz
		/// </summary>
		/// <returns>Objeto Agrupado</returns>
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
				var listaPais = _repPais.ObtenerListaPais();
				var listaCiudad = _repCiudad.ObtenerCiudadesFiltro();
				var listaTipoCentroEstudio = _repCentroEstudio.ObtenerTipoCentroEstudio();
				return Ok(new {
					ListaPais = listaPais,
					ListaCiudad = listaCiudad,
					ListaTipoCentroEstudio = listaTipoCentroEstudio
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 05/08/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Centros de Estudios Registrados
		/// </summary>
		/// <returns>List<CentroEstudioDTO></returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerCentroEstudio()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaCentroEstudio = _repCentroEstudio.ObtenerCentroEstudioRegistrado();
				return Ok(listaCentroEstudio);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 05/08/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta registro de Centro de Estudio
		/// </summary>
		/// <param name="CentroEstudio">Información de centro de Estudio</param>
		/// <returns>Bool: Confirmación de Inserción</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult InsertarCentroEstudio([FromBody]CentroEstudioFiltroDTO CentroEstudio)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentroEstudioBO centroEstudio = new CentroEstudioBO()
				{
					Nombre = CentroEstudio.Nombre,
					IdTipoCentroEstudio = CentroEstudio.IdTipoCentroEstudio,
					IdPais = CentroEstudio.IdPais,
					IdCiudad = CentroEstudio.IdCiudad,
					Estado = true,
					UsuarioCreacion = CentroEstudio.Usuario,
					UsuarioModificacion = CentroEstudio.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				var resultado = _repCentroEstudio.Insert(centroEstudio);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 05/08/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza registro de Centro de Estudio
		/// </summary>
		/// <param name="CentroEstudio">Información de centro de Estudio</param>
		/// <returns>Bool: Confirmación de Actualización</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarCentroEstudio([FromBody]CentroEstudioFiltroDTO CentroEstudio)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var centroEstudio = _repCentroEstudio.FirstById(CentroEstudio.Id);
				centroEstudio.Nombre = CentroEstudio.Nombre;
				centroEstudio.IdTipoCentroEstudio = CentroEstudio.IdTipoCentroEstudio;
				centroEstudio.IdPais = CentroEstudio.IdPais;
				centroEstudio.IdCiudad = CentroEstudio.IdCiudad;
				centroEstudio.UsuarioModificacion = CentroEstudio.Usuario;
				centroEstudio.FechaModificacion = DateTime.Now;
				var resultado = _repCentroEstudio.Update(centroEstudio);
				return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		/// TipoFuncion: POST
		/// Autor: Luis Huallpa - Edgar Serruto
		/// Fecha: 05/08/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina registro de Centro de Estudio
		/// </summary>
		/// <param name="CentroEstudio">Información de Id de registro y Usuario de Interaz</param>
		/// <returns>Bool: Confirmación de Eliminación</returns>
		[Route("[action]")]
		[HttpPost]
		public ActionResult EliminarCentroEstudio([FromBody]EliminarDTO CentroEstudio)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repCentroEstudio.Exist(CentroEstudio.Id))
				{
					var resultado = _repCentroEstudio.Delete(CentroEstudio.Id, CentroEstudio.NombreUsuario);
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

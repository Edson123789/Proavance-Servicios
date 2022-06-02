using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: ConfiguracionCoordinadores
	/// Autor: Luis H, Jose Villena
	/// Fecha: 28/05/2021        
	/// <summary>
	/// Controlador de Configuracion de Coordinadores
	/// </summary>
	[Route("api/ConfiguracionCoordinadores")]
	[ApiController]
	public class ConfiguracionCoordinadoresController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public ConfiguracionCoordinadoresController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Jose Villena
		/// Fecha: 28/05/2021        
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos de Configuracion de Coordinadores
		/// </summary>
		/// <returns> Lista de Personal : List<FiltroDTO>/returns>
		/// <returns> Lista de CentroCosto : List<CentroCostoPadreCentroCostoIndividualDTO>/returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosConfiguracionCoordinadores()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PersonalRepositorio repPersonal = new PersonalRepositorio(_integraDBContext);
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio(_integraDBContext);

				EstadoMatriculaRepositorio _repEstadoMatricula = new EstadoMatriculaRepositorio(_integraDBContext);
				MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

				var personal = repPersonal.ObtenerCoordinadoresParaFiltro();
				var centroCosto = repCentroCosto.ObtenerCentroCostoPadreCentroCostoIndividual();
				var estado = _repEstadoMatricula.ObtenerTodoFiltroConfiguracionCoordinadora();
				var subestado = _repMatriculaCabecera.ObtenerSubEstadoMatriculaConfiguracionCoordinadora();

				return Ok(new { Personal = personal, CentroCosto = centroCosto, EstadoMatricula = estado, SubEstadoMatricula = subestado });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Jose Villena
		/// Fecha: 28/05/2021        
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Informacion de Configuracion de Coordinadores
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCentroCostoCoordinadorDTO>/returns>
		/// <returns> Lista de CentroCosto sin Asignacion : List<ConfiguracionCentroCostoCoordinadorDTO>/returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerConfiguracionCoordinadores()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio repCoordinadora = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio();
				var configuracion = repCoordinadora.ObtenerConfiguracionCoordinadores();
				var agrupado = configuracion.GroupBy(x => new { x.IdPersonal, x.Personal}).Select(g => new ConfiguracionCoordinadorPorPersonal
				{
					IdPersonal = g.Key.IdPersonal.Value,
					Personal = g.Key.Personal,
					DetalleCentroCosto = g.GroupBy(y => new { y.IdCentroCosto, y.CentroCosto }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleCentroCosto
					{
						IdCentroCosto = y.Key.IdCentroCosto,
						CentroCosto = y.Key.CentroCosto
					}).ToList(),
					DetalleEstadoMatricula = g.GroupBy(y => new { y.IdEstadoMatricula, y.EstadoMatricula }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula
					{
						IdEstadoMatricula = y.Key.IdEstadoMatricula,
						EstadoMatricula = y.Key.EstadoMatricula
					}).ToList(),
					DetalleSubEstadoMatricula = g.GroupBy(y => new { y.IdSubEstadoMatricula, y.SubEstadoMatricula }).Select(y => new ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula
					{
						IdSubEstadoMatricula = y.Key.IdSubEstadoMatricula,
						SubEstadoMatricula = y.Key.SubEstadoMatricula
					}).ToList()

				}).ToList();
                var centroCostoSinAsignacion = repCoordinadora.ObtenerCentroCostoSigAsignacion();

				return Ok(new { CentroCostoAsignado = agrupado, CentroCostoNoAsignado = centroCostoSinAsignacion });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Jose Villena
		/// Fecha: 28/05/2021        
		/// Versión: 1.0
		/// <summary>
		/// Insertar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>		
		[Route("[Action]")]
		[HttpPost]
		public ActionResult InsertarConfiguracion([FromBody]List<ConfiguracionCoordinadorDTO> ConfiguracionCoordinador)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
				var estado = true;
				if (!configuracionCoordinador.InsertarActualizarConfiguracionCoordinador(ConfiguracionCoordinador))
				{
					estado = false;
				}
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Jose Villena
		/// Fecha: 28/05/2021        
		/// Versión: 1.0
		/// <summary>
		/// Actualizar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarConfiguracion([FromBody]List<ConfiguracionCoordinadorDTO> ConfiguracionCoordinador)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio repConfiguracionCoordinadora = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio();

				foreach (var configuracion in ConfiguracionCoordinador)
				{
					foreach (var personal in configuracion.ListaPersonal)
					{
						var listaEliminar= repConfiguracionCoordinadora.GetBy(x => x.IdPersonal == personal).ToList();

						foreach (var conf in listaEliminar)
						{
								repConfiguracionCoordinadora.Delete(conf.Id, configuracion.Usuario);
						}
			
						
					}
				}
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
				var estado = true;
				if (!configuracionCoordinador.InsertarActualizarConfiguracionCoordinador(ConfiguracionCoordinador))
				{
					estado = false;
				}
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Jose Villena
		/// Fecha: 28/05/2021        
		/// Versión: 1.0
		/// <summary>
		/// Eliminar Configuracion
		/// </summary>
		/// <returns> Lista de Configuracion : List<ConfiguracionCoordinadorDTO>/returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult EliminarConfiguracionCoordinadora([FromBody] EliminarDTO ConfiguracionEliminar)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio _repConfiguracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesRepositorio(_integraDBContext);
				bool estado = false;
				using (TransactionScope scope = new TransactionScope())
				{
					var configuracionDetalle = _repConfiguracionCoordinador.GetBy(x => x.IdPersonal == ConfiguracionEliminar.Id);
					if (configuracionDetalle != null)
					{
						foreach (var detalle in configuracionDetalle)
						{
							estado = _repConfiguracionCoordinador.Delete(detalle.Id, ConfiguracionEliminar.NombreUsuario);
						}
					}
					//estado = _repConfiguracionCoordinador.Delete(ConfiguracionEliminar.Id, ConfiguracionEliminar.NombreUsuario);
					scope.Complete();
					return Ok(estado);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

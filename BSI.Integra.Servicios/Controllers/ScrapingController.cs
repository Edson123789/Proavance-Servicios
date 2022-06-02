using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: Planificacion/Scraping
	/// Autor: Luis HUallpa - Ansoli Espinoza
    /// Fecha: 18-01-2021
	/// <summary>
	/// Controlador para el consumo de la data de Scraping
	/// </summary>

	[Route("api/Scraping")]
	[ApiController]
	public class ScrapingController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public ScrapingController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosScraping()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				var centroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro();
				var pEspecifico = _repPespecifico.ObtenerProgramaEspecificoSesionCentroCostoParaFiltro();

				return Ok(new { CentroCosto = centroCosto, ProgramaEspecifico = pEspecifico });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerConfiguracionScraping()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
				CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
				ScrapingAerolineaEstadoConsultaRepositorio _repScrapingAerolineaEstadoConsulta = new ScrapingAerolineaEstadoConsultaRepositorio(_integraDBContext);
				var centroCosto = _repCentroCosto.GetBy(x => x.Estado == true).Where(x => x.FechaCreacion.Year >= DateTime.Now.AddYears(-2).Year).Select(x => new { x.Id, x.Nombre });
				var scrapingEstadoConsulta = _repScrapingAerolineaEstadoConsulta.GetBy(x => x.Estado == true).Select(x => new { x.Id, x.Nombre }).ToList();
				
				var ciudades = _repCiudad.ObtenerCiudadesScrapingAerolinea();
				return Ok(new { CentroCosto = centroCosto, Ciudades = ciudades, EstadoConsulta = scrapingEstadoConsulta });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCronogramaGrupoSesion([FromBody]CronogramaGrupoSesionFiltroDTO CronogramaGrupoSesion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				var pespecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == CronogramaGrupoSesion.IdCentroCosto).FirstOrDefault();
				int idPespecifico = 0;
				if (pespecifico != null)
				{
					idPespecifico = pespecifico.Id;
				}
				var cronogramaGrupoSesion = _repPespecificoSesion.ObtenerCronogramaGrupoSesion(idPespecifico, CronogramaGrupoSesion.GrupoCronograma, CronogramaGrupoSesion.GrupoSesion);
				return Ok(cronogramaGrupoSesion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdScrapingAerolineaConfiguracion}")]
		[HttpGet]
		public ActionResult ObtenerScrapingAerolineaResultado(int IdScrapingAerolineaConfiguracion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ScrapingAerolineaResultadoRepositorio _repScrapingAerolineaResultado = new ScrapingAerolineaResultadoRepositorio();
				var scrapingAerolineaResultado = _repScrapingAerolineaResultado.ObtenerScrapingAerolineaResultado(IdScrapingAerolineaConfiguracion);
				return Ok(scrapingAerolineaResultado);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[Route("[Action]/{IdScrapingAerolineaResultado}")]
		[HttpGet]
		public ActionResult ObtenerScrapingAerolineaResultadoDetalle(int IdScrapingAerolineaResultado)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (IdScrapingAerolineaResultado != 0)
				{
					ScrapingAerolineaResultadoDetalleRepositorio _repScrapingAerolineaResultado = new ScrapingAerolineaResultadoDetalleRepositorio();
					var scrapingAerolineaResultado = _repScrapingAerolineaResultado.ObtenerScrapingAerolineaResultadoDetalle(IdScrapingAerolineaResultado);
					return Ok(scrapingAerolineaResultado);
				}
				else
				{
					return Ok();
				}
				
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

		[Route("[Action]/{IdPadre}")]
		[HttpGet]
		public ActionResult ObtenerScrapingAerolineaResultadoDetalleEscala(int IdPadre)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ScrapingAerolineaResultadoDetalleRepositorio _repScrapingAerolineaResultadoDetalle = new ScrapingAerolineaResultadoDetalleRepositorio();
				var escalas = _repScrapingAerolineaResultadoDetalle.ObtenerDetalleEscalas(IdPadre);
				return Ok(escalas);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerConsultas([FromBody]FiltroScrapingAerolineaDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				var configuracionScraping = _repPespecifico.ObtenerConfiguracionScraping(Filtro.FechaInicio, Filtro.CentroCosto, Filtro.EstadoScraping);

				return Ok(configuracionScraping);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GuardarConfiguracionScraping([FromBody]List<ScrapingConfiguracionDTO> Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ScrapingAerolineaConfiguracionRepositorio _repScrapingAerolineaConfiguracion = new ScrapingAerolineaConfiguracionRepositorio();
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				List<ScrapingAerolineaConfiguracionBO> listaConfiguracion = new List<ScrapingAerolineaConfiguracionBO>();
				List<int> ListaIds = new List<int>();
				foreach(var item in Filtro)
				{
					var fechaIda = item.FechaHoraInicio.Value.AddHours(-5);
					var fechaRegreso = item.FechaHoraFin.Value.AddHours(-5);
					var pespecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == item.IdCentroCosto).FirstOrDefault();
					int? idPespecifico = null;
					if (pespecifico != null)
					{
						idPespecifico = pespecifico.Id;
					}
					if (item.Id == null)
					{
						ScrapingAerolineaConfiguracionBO configuracion = new ScrapingAerolineaConfiguracionBO()
						{
							IdPespecifico = idPespecifico,
							IdCentroCosto = item.IdCentroCosto,
							NroGrupoSesion = item.NroGrupoSesion,
							NroGrupoCronograma = item.NroGrupoCronograma,
							IdScrapingAerolineaEstadoConsulta = 2,
							IdCiudadOrigen = item.IdCiudadOrigen,
							IdCiudadDestino = item.IdCiudadDestino,
							FechaIda = fechaIda.AddDays(-Convert.ToDouble(item.PrecisionIda == null ? 0 : item.PrecisionIda.Value)),
							FechaRetorno = fechaRegreso.AddDays(Convert.ToDouble(item.PrecisionRetorno == null ? 0 : item.PrecisionRetorno.Value)),
							PrecisionIda = Convert.ToDecimal(item.PrecisionIda),
							PrecisionRetorno = Convert.ToDecimal(item.PrecisionRetorno),
							NroFrecuencia = item.NroFrecuencia,
							TipoFrecuencia = item.TipoFrecuencia,
							TipoVuelo = item.TipoVuelo,
							FechaEjecucion = DateTime.Now.AddDays(1),
							Estado = true,
							FechaCreacion = DateTime.Now,
							FechaModificacion = DateTime.Now,
							UsuarioCreacion = "System",
							UsuarioModificacion = "System",
							TienePasajeComprado = false
						};
						ListaIds.Add(configuracion.Id);
						_repScrapingAerolineaConfiguracion.Insert(configuracion);
					}
					else
					{
						var configuracion = _repScrapingAerolineaConfiguracion.FirstById(item.Id.Value);
						configuracion.IdPespecifico = idPespecifico;
						configuracion.IdCentroCosto = item.IdCentroCosto;
						configuracion.NroGrupoSesion = item.NroGrupoSesion;
						configuracion.NroGrupoCronograma = item.NroGrupoCronograma;
						configuracion.IdScrapingAerolineaEstadoConsulta = 4;
						configuracion.IdCiudadOrigen = item.IdCiudadOrigen;
						configuracion.IdCiudadDestino = item.IdCiudadDestino;
						configuracion.FechaIda = fechaIda.AddDays(Convert.ToDouble(-item.PrecisionIda == null ? 0 : item.PrecisionIda.Value));
						configuracion.FechaRetorno = fechaRegreso.AddDays(Convert.ToDouble(item.PrecisionRetorno == null ? 0 : item.PrecisionRetorno.Value));
						configuracion.PrecisionIda = Convert.ToDecimal(item.PrecisionIda);
						configuracion.PrecisionRetorno = Convert.ToDecimal(item.PrecisionRetorno);
						configuracion.NroFrecuencia = item.NroFrecuencia;
						configuracion.TipoFrecuencia = item.TipoFrecuencia;
						configuracion.TipoVuelo = item.TipoVuelo;
						configuracion.FechaEjecucion = DateTime.Now.AddDays(1);
						configuracion.FechaModificacion = DateTime.Now;
						configuracion.UsuarioModificacion = "System";
						configuracion.TienePasajeComprado = item.TienePasajeComprado;
						ListaIds.Add(configuracion.Id);
						_repScrapingAerolineaConfiguracion.Update(configuracion);
					}	
				}
				return Ok(ListaIds);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GenerarActualizarFur([FromBody]FurScrapingAerolineaDTO FurScraping)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{

				ScrapingAerolineaConfiguracionRepositorio _repScrapingAerolineaConfiguracion = new ScrapingAerolineaConfiguracionRepositorio();
				ScrapingAerolineaResultadoRepositorio _repScrapingAerolineaResultado = new ScrapingAerolineaResultadoRepositorio();
				ScrapingAerolineaResultadoDetalleRepositorio _repScrapingAerolineaResultadoDetalle = new ScrapingAerolineaResultadoDetalleRepositorio();
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				ProductoRepositorio _repProducto = new ProductoRepositorio();
				ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
				PlanContableRepositorio _repPlanContable = new PlanContableRepositorio();
				var proveedor = _repProveedor.FirstBy(x => x.RazonSocial.Equals("SCRAPING AEROLINEAS"));
				var producto = _repProducto.FirstBy(x => x.Nombre.Equals("SCRAPING VUELOS"));
				var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(producto.Id, proveedor.Id);
				var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));
				var scrapingAerolineaConfiguracion = _repScrapingAerolineaConfiguracion.FirstById(FurScraping.IdScrapingAerolineaConfiguracion);
				string Usuario = scrapingAerolineaConfiguracion.UsuarioCreacion;
				var estado = false;

				var pespecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == FurScraping.IdCentroCosto).FirstOrDefault();
				int? idPespecifico = null;
				if (pespecifico != null)
				{
					idPespecifico = pespecifico.Id;
				}

				var scrapingAerolineaResultadoDetalle = _repScrapingAerolineaResultadoDetalle.GetBy(x => x.IdScrapingAerolineaResultado == FurScraping.IdScrapingAerolineaResultado);
				var aerolineas = scrapingAerolineaResultadoDetalle.GroupBy(x => x.NombreAerolinea);
				string descripcion = "";
				foreach (var item in aerolineas)
				{
					descripcion += item.Key + " ";
				}
				FurBO fur;
				descripcion = descripcion.Trim();
				if (FurScraping.IdFur != null)
				{
					fur = new FurBO();
				}
				else
				{
					fur = new FurBO();
					int numeroSemana = fur.obtenerNumeroSemana(FurScraping.Fecha);
					fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
					fur.IdPespecifico = idPespecifico;
					fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
					fur.IdMonedaPagoReal = detalleFur.IdMoneda;
					fur.NumeroCuenta = detalleFur.NumeroCuenta;
					fur.Descripcion = descripcion;
					fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
					fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
					fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * FurScraping.Precio);
					fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * FurScraping.Precio);
					fur.FechaLimite = FurScraping.Fecha;
					fur.IdCiudad = 4;
					fur.IdCentroCosto = FurScraping.IdCentroCosto;
					fur.IdProveedor = proveedor.Id;
					fur.Cuenta = detalleFur.CuentaDescripcion;
					fur.IdProducto = producto.Id;
					fur.NumeroSemana = numeroSemana;
					fur.Cantidad = FurScraping.Precio;
					fur.Descripcion = detalleFur.Descripcion;
					fur.UsuarioSolicitud = Usuario;
					fur.Estado = true;
					fur.UsuarioCreacion = Usuario;
					fur.UsuarioModificacion = Usuario;
					fur.FechaCreacion = DateTime.Now;
					fur.FechaModificacion = DateTime.Now;
					fur.IdMonedaProveedor = detalleFur.IdMoneda;
					fur.IdFurFaseAprobacion1 = 1;
					fur.IdPersonalAreaTrabajo = 3; //Operaciones
					fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
					fur.Monto = fur.PrecioTotalMonedaOrigen;
					fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * FurScraping.Precio;
					fur.PagoDolares = detalleFur.PrecioDolares * FurScraping.Precio;
					fur.Antiguo = 0;
					fur.OcupadoSolicitud = false;
					fur.OcupadoRendicion = false;
					fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
					estado = _repFur.Insert(fur);
				}
				scrapingAerolineaConfiguracion.FechaModificacion = DateTime.Now;
				scrapingAerolineaConfiguracion.UsuarioModificacion = "System";
				scrapingAerolineaConfiguracion.IdFur = fur.Id;
				_repScrapingAerolineaConfiguracion.Update(scrapingAerolineaConfiguracion);
				return Ok(new { fur.Id, fur.MontoProyectado });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult GuardarConfiguracionIndividual([FromBody]ScrapingAerolineaConfiguracionRegistroDTO Configuracion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ScrapingAerolineaConfiguracionRepositorio _repScrapingAerolineaConfiguracionRepositorio = new ScrapingAerolineaConfiguracionRepositorio();
				ScrapingAerolineaConfiguracionBO configuracion = new ScrapingAerolineaConfiguracionBO()
				{
					IdCentroCosto = Configuracion.IdCentroCosto,
					IdScrapingAerolineaEstadoConsulta = 2,
					IdCiudadOrigen = Configuracion.IdCiudadOrigen,
					IdCiudadDestino = Configuracion.IdCiudadDestino,
					FechaIda = Configuracion.FechaIda,
					FechaRetorno = Configuracion.FechaRetorno,
					NroFrecuencia = Configuracion.NroFrecuencia,
					NroGrupoCronograma = 0,
					NroGrupoSesion = 0,
					PrecisionIda = 0,
					PrecisionRetorno = 0,
					TipoFrecuencia = Configuracion.TipoFrecuencia,
					TipoVuelo = Configuracion.TipoVuelo,
					FechaEjecucion = DateTime.Now.AddDays(1),
					Estado = true,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now,
					UsuarioCreacion = "System",
					UsuarioModificacion = "System",
					TienePasajeComprado = false
				};

				_repScrapingAerolineaConfiguracionRepositorio.Insert(configuracion);
				var conf = _repScrapingAerolineaConfiguracionRepositorio.obtenerConfiguracionScraping(configuracion.Id);
				return Ok(conf);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{Id}/{Usuario}")]
		[HttpDelete]
		public ActionResult EliminarConfiguracion(int Id, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ScrapingAerolineaConfiguracionRepositorio _repScrapingAerolineaConfiguracion = new ScrapingAerolineaConfiguracionRepositorio();
				var estado = _repScrapingAerolineaConfiguracion.Delete(Id,Usuario);
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Tipo Función: POST
		/// Autor: Ansoli Espinoza
		/// Fecha: 18-01-2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene las Cabeceras del scraping de Portales de Empleo
		/// </summary>
		/// <param name="Filtro">Filtro del formulario</param>
		/// <returns>Listado de las Cabeceras del scraping de Portales de Empleo</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCabeceraPortalEmpleo([FromBody] FiltroScrapingPortalEmpleoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ScrapingPortalEmpleoResultadoRepositorio repo = new ScrapingPortalEmpleoResultadoRepositorio();
                var resultado = repo.ObtenerCabeceras(Filtro);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
		/// Autor: Ansoli Espinoza
		/// Fecha: 18-01-2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene el detalle del Scraping de portal de Empleo por el Id enviado
		/// </summary>
		/// <param name="Cabecera">Parametros del formulario</param>
		/// <returns>El detalle de scraping de Portal de Empleo consultado</returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDetallePortalEmpleo([FromBody] ScrapinPortalEmpleoCabeceraDTO Cabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
				ScrapingPortalEmpleoResultadoRepositorio repo = new ScrapingPortalEmpleoResultadoRepositorio();
                ScrapingEmpleoClasificacionEstudioRepositorio repoEstudio = new ScrapingEmpleoClasificacionEstudioRepositorio();
                ScrapingEmpleoClasificacionExperienciaRepositorio repoExperiencia = new ScrapingEmpleoClasificacionExperienciaRepositorio();
                ScrapingEmpleoClasificacionCertificacionRepositorio repoCertificacion = new ScrapingEmpleoClasificacionCertificacionRepositorio();
				
				var resultado = repo.ObtenerDetalle(Cabecera.Id);
                var listadoClasificacionEstudios = repoEstudio.GetBy(w => w.IdScrapingPortalEmpleoResultado == resultado.Id);
                var listadoClasificacionExperiencia = repoExperiencia.GetBy(w => w.IdScrapingPortalEmpleoResultado == resultado.Id);
                var listadoClasificacionCertificacion = repoCertificacion.GetBy(w => w.IdScrapingPortalEmpleoResultado == resultado.Id);
                var listadoClasificacion = repo.ObtenerClasificacionAgrupada(Cabecera.Id);

                return Ok(new {anuncio = resultado, listadoClasificacionEstudios, listadoClasificacionExperiencia, listadoClasificacionCertificacion, listadoClasificacion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 22-01-2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion de los combos para la interfaz del Scraping de portal de Empleo
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionScrapingPortalEmpleo()
        {
			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ScrapingPaginaRepositorio repPagina = new ScrapingPaginaRepositorio(_integraDBContext);
                AreaFormacionRepositorio repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
                TipoEstudioRepositorio repEstudio = new TipoEstudioRepositorio(_integraDBContext);

                CargoRepositorio repCargo = new CargoRepositorio(_integraDBContext);
                AreaTrabajoRepositorio repAreaTrabajo = new AreaTrabajoRepositorio(_integraDBContext);
                IndustriaRepositorio repIndustria = new IndustriaRepositorio(_integraDBContext);

                CertificacionRepositorio repCertificacion = new CertificacionRepositorio(_integraDBContext);
                EmpresaRepositorio repEmpresa = new EmpresaRepositorio(_integraDBContext);

                var portalEmpleo = repPagina.GetBy(w => w.EsPortalEmpleo == true, s => new { s.Id, s.Nombre });
                var areaFormacion = repAreaFormacion.ObtenerAreaFormacionFiltro();
                var tipoEstudio = repEstudio.ObtenerListaParaFiltro();

                var cargo = repCargo.ObtenerCargoFiltro();
                var areaTrabajo = repAreaTrabajo.ObtenerTodoAreaTrabajoFiltro();
                var industria = repIndustria.ObtenerIndustriaFiltro();

                var certificacion = repCertificacion.GetBy(w => w.Estado == true, s => new {s.Id, s.Nombre});
                var empresa = repEmpresa.ObtenerTodoEmpresasFiltro();

                return Ok(new
                {
                    Portales = portalEmpleo, AreaFormacion = areaFormacion, TipoEstudio = tipoEstudio,
                    Cargo = cargo, AreaTrabajo = areaTrabajo, Industria = industria,
                    Certificacion = certificacion, Empresa = empresa
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		/// Tipo Función: POST
		/// Autor: Ansoli Espinoza
		/// Fecha: 16-02-2021
		/// Versión: 1.0
		/// <summary>
		/// Registra la clasificacion del anuncio del Portal de Empleos
		/// </summary>
		/// <returns></returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult RegistrarClasificacionScrapingPortalEmpleo([FromBody] ScrapinPortalEmpleoRegistrarClasificacionDTO clasificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ScrapingEmpleoClasificacionEstudioRepositorio repoEstudio = new ScrapingEmpleoClasificacionEstudioRepositorio(_integraDBContext);
                ScrapingEmpleoClasificacionExperienciaRepositorio repoExperiencia = new ScrapingEmpleoClasificacionExperienciaRepositorio(_integraDBContext);
                ScrapingEmpleoClasificacionCertificacionRepositorio repoCertificacion = new ScrapingEmpleoClasificacionCertificacionRepositorio(_integraDBContext);

				var listadoEstudioExistente =
                    repoEstudio.GetBy(w => w.IdScrapingPortalEmpleoResultado == clasificacion.IdScrapingPortalEmpleoResultado);
                var listadoExperienciaExistente =
                    repoExperiencia.GetBy(w => w.IdScrapingPortalEmpleoResultado == clasificacion.IdScrapingPortalEmpleoResultado);
                var listadoCertificacionExistente =
                    repoCertificacion.GetBy(w => w.IdScrapingPortalEmpleoResultado == clasificacion.IdScrapingPortalEmpleoResultado);

                if (clasificacion.ListadoEstudio != null && clasificacion.ListadoEstudio.Count > 0)
                {
                    List<ScrapingEmpleoClasificacionEstudioBO> listado =
                        new List<ScrapingEmpleoClasificacionEstudioBO>();
                    List<int> listadoIdEnviados = new List<int>();

					foreach (var estudio in clasificacion.ListadoEstudio)
                    {
                        if (estudio.Id != 0 && repoEstudio.Exist(estudio.Id))
                        {
                            listadoIdEnviados.Add(estudio.Id);

							var bo = repoEstudio.FirstById(estudio.Id);
                            bo.IdScrapingPortalEmpleoResultado = estudio.IdScrapingPortalEmpleoResultado;
                            bo.IdTipoEstudio = estudio.IdTipoEstudio;
                            bo.IdAreaFormacion = estudio.IdAreaFormacion;
                            bo.EsValidado = true;

                            bo.UsuarioModificacion = clasificacion.NombreUsuario;
                            bo.FechaModificacion = DateTime.Now;
                            listado.Add(bo);
                        }
                        else
                        {
                            listado.Add(new ScrapingEmpleoClasificacionEstudioBO()
                            {
                                IdScrapingPortalEmpleoResultado = estudio.IdScrapingPortalEmpleoResultado,
                                IdTipoEstudio = estudio.IdTipoEstudio,
                                IdAreaFormacion = estudio.IdAreaFormacion,
                                EsAutomatico = false,
                                EsValidado = true,
                                Estado = true,
                                UsuarioCreacion = clasificacion.NombreUsuario,
                                UsuarioModificacion = clasificacion.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            });
                        }
                    }

                    repoEstudio.Update(listado);

					//eliminacion
                    var listadoEliminar =
                        listadoEstudioExistente.Where(w => !listadoIdEnviados.Contains(w.Id)).ToList();
                    if (listadoEliminar != null && listadoEliminar.Count() > 0)
                        repoEstudio.Delete(listadoEliminar.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
				}
                else
                {
                    if (listadoEstudioExistente != null && listadoEstudioExistente.Count() > 0)
                    {
                        repoEstudio.Delete(listadoEstudioExistente.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
					}
                }

				if (clasificacion.ListadoExperiencia != null && clasificacion.ListadoExperiencia.Count > 0)
                {
                    List<ScrapingEmpleoClasificacionExperienciaBO> listado =
                        new List<ScrapingEmpleoClasificacionExperienciaBO>();
                    List<int> listadoIdEnviados = new List<int>();

					foreach (var experiencia in clasificacion.ListadoExperiencia)
                    {
                        if (experiencia.Id != 0 && repoExperiencia.Exist(experiencia.Id))
                        {
                            listadoIdEnviados.Add(experiencia.Id);

							var bo = repoExperiencia.FirstById(experiencia.Id);
                            bo.IdScrapingPortalEmpleoResultado = experiencia.IdScrapingPortalEmpleoResultado;
                            bo.IdCargo = experiencia.IdCargo;
                            bo.IdAreaTrabajo = experiencia.IdAreaTrabajo;
                            bo.IdIndustria = experiencia.IdIndustria;
                            bo.NroAnhos = experiencia.NroAnhos;
                            bo.Obligatorio = experiencia.Obligatorio;
                            bo.EsValidado = true;

                            bo.UsuarioModificacion = clasificacion.NombreUsuario;
                            bo.FechaModificacion = DateTime.Now;
                            listado.Add(bo);
                        }
                        else
                        {
                            listado.Add(new ScrapingEmpleoClasificacionExperienciaBO()
                            {
                                IdScrapingPortalEmpleoResultado = experiencia.IdScrapingPortalEmpleoResultado,
                                IdCargo = experiencia.IdCargo,
                                IdAreaTrabajo = experiencia.IdAreaTrabajo,
                                IdIndustria = experiencia.IdIndustria,
                                NroAnhos = experiencia.NroAnhos,
                                Obligatorio = experiencia.Obligatorio,
                                EsAutomatico = false,
                                EsValidado = true,
                                Estado = true,
                                UsuarioCreacion = clasificacion.NombreUsuario,
                                UsuarioModificacion = clasificacion.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            });
                        }
                    }

                    repoExperiencia.Update(listado);

                    //eliminacion
                    var listadoEliminar =
                        listadoExperienciaExistente.Where(w => !listadoIdEnviados.Contains(w.Id)).ToList();
                    if (listadoEliminar != null && listadoEliminar.Count() > 0)
                        repoExperiencia.Delete(listadoEliminar.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
				}
                else
                {
                    if (listadoExperienciaExistente != null && listadoExperienciaExistente.Count() > 0)
                    {
                        repoExperiencia.Delete(listadoExperienciaExistente.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
                    }
                }

				if (clasificacion.ListadoCertificacion != null && clasificacion.ListadoCertificacion.Count > 0)
				{
					List<ScrapingEmpleoClasificacionCertificacionBO> listado =
						new List<ScrapingEmpleoClasificacionCertificacionBO>();
					List<int> listadoIdEnviados = new List<int>();

					foreach (var certificacion in clasificacion.ListadoCertificacion)
					{
						if (certificacion.Id != 0 && repoCertificacion.Exist(certificacion.Id))
						{
							listadoIdEnviados.Add(certificacion.Id);

							var bo = repoCertificacion.FirstById(certificacion.Id);
							bo.IdScrapingPortalEmpleoResultado = certificacion.IdScrapingPortalEmpleoResultado;
							bo.IdEmpresa = certificacion.IdEmpresa;
							bo.IdCertificacion = certificacion.IdCertificacion;
							bo.Obligatorio = certificacion.Obligatorio;
							bo.EsValidado = true;

							bo.UsuarioModificacion = clasificacion.NombreUsuario;
							bo.FechaModificacion = DateTime.Now;
							listado.Add(bo);
						}
						else
						{
							listado.Add(new ScrapingEmpleoClasificacionCertificacionBO()
							{
								IdScrapingPortalEmpleoResultado = certificacion.IdScrapingPortalEmpleoResultado,
                                IdEmpresa = certificacion.IdEmpresa,
                                IdCertificacion = certificacion.IdCertificacion,
								Obligatorio = certificacion.Obligatorio,
								EsAutomatico = false,
								EsValidado = true,
								Estado = true,
								UsuarioCreacion = clasificacion.NombreUsuario,
								UsuarioModificacion = clasificacion.NombreUsuario,
								FechaCreacion = DateTime.Now,
								FechaModificacion = DateTime.Now
							});
						}
					}

                    repoCertificacion.Update(listado);

					//eliminacion
					var listadoEliminar =
                        listadoCertificacionExistente.Where(w => !listadoIdEnviados.Contains(w.Id)).ToList();
					if (listadoEliminar != null && listadoEliminar.Count() > 0)
                        repoCertificacion.Delete(listadoEliminar.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
				}
				else
				{
					if (listadoCertificacionExistente != null && listadoCertificacionExistente.Count() > 0)
					{
                        repoCertificacion.Delete(listadoCertificacionExistente.Select(s => s.Id).ToList(), clasificacion.NombreUsuario);
					}
				}

				return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		/// Tipo Función: POST
		/// Autor: Ansoli Espinoza
		/// Fecha: 07-12-2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina la clasificacion segun los parametros enviados
		/// </summary>
		/// <param name="Cabecera">Parametros del formulario</param>
		/// <returns>El estado de la eliminacion</returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult EliminarClasificacionEmpleo([FromBody] ScrapinPortalEmpleoClasificacionAgrupadaDTO Cabecera)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
            {
                ScrapingEmpleoResultadoClasificacionBO bo = new ScrapingEmpleoResultadoClasificacionBO();
                var resultado = bo.EliminarClasificacionEmpleo(Cabecera);

                return Ok(resultado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 10-12-2021
        /// Versión: 1.0
		/// <summary>
		/// Obtiene los Combos para la interfaz de Patron de clasificación de empleos
		/// </summary>
		/// <returns></returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCombosPatronClasficacionEmpleo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AreaTrabajoRepositorio repoAT = new AreaTrabajoRepositorio(_integraDBContext);
                AreaFormacionRepositorio repoAF = new AreaFormacionRepositorio(_integraDBContext);
                CargoRepositorio repoCA = new CargoRepositorio(_integraDBContext);
                IndustriaRepositorio repoIN = new IndustriaRepositorio(_integraDBContext);

                var listadoAreaTrabajo = repoAT.GetAll();
                var listadoAreaFormacion = repoAF.GetAll();
                var listadoCargo = repoCA.GetAll();
                var listadoIndustria = repoIN.GetAll();


                return Ok(new
                {
                    ListadoAreaTrabajo = listadoAreaTrabajo, ListadoAreaFormacion = listadoAreaFormacion,
                    ListadoCargo = listadoCargo, ListadoIndustria = listadoIndustria
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		/// Tipo Función: POST
		/// Autor: Ansoli Espinoza
		/// Fecha: 16-02-2021
		/// Versión: 1.0
		/// <summary>
		/// Registra la clasificacion del anuncio del Portal de Empleos
		/// </summary>
		/// <returns></returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarPatronClasificacionEmpleo([FromBody] PatronClasificacionEmpleoRegistrarDTO clasificacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
            {
				//validaciones
				if(string.IsNullOrEmpty(clasificacion.Patron))
                    return BadRequest("Debe de especificar un patron de filtrado");

				if(clasificacion.IdAreaFormacion == null && clasificacion.IdAreaTrabajo == null && clasificacion.IdCargo == null && clasificacion.IdIndustria == null)
                    return BadRequest("Debe de de seleccionar un valor del combo");

				ScrapingEmpleoPatronClasificacionRepositorio repo =
                    new ScrapingEmpleoPatronClasificacionRepositorio(_integraDBContext);

                ScrapingEmpleoPatronClasificacionBO bo = new ScrapingEmpleoPatronClasificacionBO()
                {
                    IdAreaFormacion = clasificacion.IdAreaFormacion,
                    IdAreaTrabajo = clasificacion.IdAreaTrabajo,
                    IdCargo = clasificacion.IdCargo,
                    IdIndustria = clasificacion.IdIndustria,
                    Patron = clasificacion.Patron,

                    Estado = true,
                    UsuarioCreacion = clasificacion.Usuario,
                    UsuarioModificacion = clasificacion.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                repo.Insert(bo);

				return Ok(bo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 10-12-2021
        /// Versión: 1.0
        /// <summary>
        /// Realiza la busqueda de los patrones de Clasificacion de empleos
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult BuscarPatronClasificacionEmpleo([FromBody] PatronClasificacionEmpleoBuscarDTO busqueda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //validaciones
                if (busqueda.Tipo == 0)
                    return BadRequest("Seleccione un tipo");

                ScrapingEmpleoPatronClasificacionRepositorio repo =
                    new ScrapingEmpleoPatronClasificacionRepositorio(_integraDBContext);
                var listado = repo.BuscarPatron(busqueda);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 10-12-2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina un patron de Clasificacion de empleos
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EliminarPatronClasificacionEmpleo([FromBody] EliminarDTO eliminar)
        {
            try
            {
                ScrapingEmpleoPatronClasificacionRepositorio repo =
                    new ScrapingEmpleoPatronClasificacionRepositorio(_integraDBContext);
                var resultado = repo.Delete(eliminar.Id, eliminar.NombreUsuario);

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Ansoli Espinoza
        /// Fecha: 10-12-2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un patron de Clasificacion de empleos
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarPatronClasificacionEmpleo([FromBody] PatronClasificacionEmpleoRegistrarDTO registrar)
        {
            try
            {
                ScrapingEmpleoPatronClasificacionRepositorio repo =
                    new ScrapingEmpleoPatronClasificacionRepositorio(_integraDBContext);
                var bo = repo.FirstById(registrar.Id);

                bo.Patron = registrar.Patron;
                bo.UsuarioModificacion = registrar.Usuario;
                bo.FechaModificacion = DateTime.Now;

                repo.Update(bo);

                return Ok(bo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
	}
}

using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Servicios.SCode.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/RegistroEntregaMaterial")]
	public class RegistroEntregaMaterialController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public RegistroEntregaMaterialController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCombosRegistroMaterial()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio(_integraDBContext);
				ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
				PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
				AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio(_integraDBContext);
				SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio(_integraDBContext);
				PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
				EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio(_integraDBContext);
				ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio(_integraDBContext);
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);

				var res = _repPEspecifico.ObtenerPEspecificoGruposPorPEspecificoPadre();
				var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
				var area = _repArea.ObtenerAreaCapacitacionFiltro();
				var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
				var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(null);
				var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadres(null);
				var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();
				var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
				var grupos = _repPespecificoSesion.ObtenerGruposProgramaEspecificoFiltro();

				//FUR
				ProductoRepositorio _repProducto = new ProductoRepositorio(_integraDBContext);
				ProductoPresentacionRepositorio _repProductoPresentacion = new ProductoPresentacionRepositorio(_integraDBContext);
				ProveedorRepositorio _repProveedor = new ProveedorRepositorio(_integraDBContext);

				var producto = _repProducto.ObtenerListaProductoMaterialesParaCombo();
				var proveedor = _repProveedor.ObtenerProveedorParaCombo();
				var productoPresentacion = _repProductoPresentacion.ObtenerProductoPresentacionParaCombo();

				return Ok(new {

					CiudadBS = ciudadBs,
					Area = area,
					SubArea = subArea,
					ProgramaGeneralP = programaGeneral,
					ProgramaEspecifico = programaEspecifico,
					PEspecificoCurso = res,
					Estado = estadoPespecifico,
					Grupos = grupos,
					Modalidad = modalidades,

					Producto = producto,
					Proveedor = proveedor,
					ProductoPresentacion = productoPresentacion });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ProgramaEspecificoPadreAutocomplete([FromBody] Dictionary<string, string> Filtros)
		{
			try
			{
				if (Filtros != null)
				{
					PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
					return Ok(_repPEspecifico.ObtenerProgramaEspecificoPadreAutoComplete(Filtros["valor"].ToString()));
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

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerCriteriosMaterialesProgramaEspecifico([FromBody]FiltroMaterialDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MaterialPespecificoRepositorio _repMaterialPEspecifico = new MaterialPespecificoRepositorio(_integraDBContext);
				MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
				var listaMateriales = _repMaterialPEspecifico.ObtenerMaterialesRegistroEntrega(Filtro);
				List<CriterioVerificacionColumnasDTO> colCriterios = new List<CriterioVerificacionColumnasDTO>();
				foreach(var item in listaMateriales)
				{
					var listaCriterios = _repMaterialAsociacionCriterioVerificacion.ObtenerCriteriosVerificacionPorMaterialDetalle(item.IdMaterialPEspecificoDetalle);
					item.CriteriosVerificacion = listaCriterios;
					colCriterios.AddRange(listaCriterios.Select(x => new CriterioVerificacionColumnasDTO { IdMaterialCriterioVerificacion = x.IdMaterialCriterioVerificacion, MaterialCriterioVerificacion = x.MaterialCriterioVerificacion }).ToList());
				}
				var columnas = colCriterios.GroupBy(x => new { x.IdMaterialCriterioVerificacion, x.MaterialCriterioVerificacion }).Select(x => new CriterioVerificacionColumnasDTO
				{
					IdMaterialCriterioVerificacion = x.Key.IdMaterialCriterioVerificacion,
					MaterialCriterioVerificacion = x.Key.MaterialCriterioVerificacion
				}).ToList();
				return Ok(new { ListaMateriales = listaMateriales, Columnas = columnas });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult AprobarRechazarRegistroEntrega([FromBody]AprobarRechazarRegistroEntregaMaterialDTO Registro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MaterialPespecificoDetalleRepositorio _repMaterialPEspecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
				MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
				MaterialCriterioVerificacionDetalleRepositorio _repMaterialCriterioVerificacionDetalle = new MaterialCriterioVerificacionDetalleRepositorio(_integraDBContext);
				var listaCriterios = _repMaterialAsociacionCriterioVerificacion.ObtenerCriteriosVerificacionPorMaterialDetalle(Registro.IdMaterialPEspecificoDetalle);
				foreach (var item in Registro.ClaveValor)
				{
					var criterio = listaCriterios.Where(x => x.IdMaterialCriterioVerificacion == item.Key).FirstOrDefault();
					var criterioVerificacion = _repMaterialCriterioVerificacionDetalle.FirstById(criterio.Id);
					if(Registro.EstadoRegistroMaterial == 2)
					{
						criterioVerificacion.EsAprobado = false;
					}
					else
					{
						criterioVerificacion.EsAprobado = item.Value;
					}
					criterioVerificacion.UsuarioModificacion = Registro.Usuario;
					criterioVerificacion.FechaModificacion = DateTime.Now;
					_repMaterialCriterioVerificacionDetalle.Update(criterioVerificacion);
					//_repMaterialAsociacionCriterioVerificacion.RegistrarCriteriosEntregaMaterial(criterio.Id, item.Value, Registro.Usuario);
				}
				var materialPEspecificoDetalle = _repMaterialPEspecificoDetalle.FirstById(Registro.IdMaterialPEspecificoDetalle);
				materialPEspecificoDetalle.IdEstadoRegistroMaterial = Registro.EstadoRegistroMaterial;
				materialPEspecificoDetalle.UsuarioModificacion = Registro.Usuario;
				materialPEspecificoDetalle.FechaModificacion = DateTime.Now;
				_repMaterialPEspecificoDetalle.Update(materialPEspecificoDetalle);

				return Ok(true);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdMaterialPEspecificoDetalle}")]
		[HttpGet]
		public ActionResult ObtenerDetalleFurMaterialPEspecifico(int IdMaterialPEspecificoDetalle)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MaterialPespecificoDetalleRepositorio _repMaterialPEspecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
				var detalle = _repMaterialPEspecificoDetalle.ObtenerDetalleFur(IdMaterialPEspecificoDetalle);
				return Ok(detalle);
			}
			catch (Exception e )
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarFurRegistroMaterial([FromBody]FurRegistroMaterialDTO FurMaterial)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio(_integraDBContext);
				PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio(_integraDBContext);
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
				FurRepositorio _repFur = new FurRepositorio(_integraDBContext);
				ProductoRepositorio _repProducto = new ProductoRepositorio(_integraDBContext);
				PlanContableRepositorio _repPlanContable = new PlanContableRepositorio(_integraDBContext);
				MaterialPespecificoDetalleRepositorio _repMaterialPespecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);

				var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(FurMaterial.IdProducto, FurMaterial.IdProveedor);
				var estado = false;
				var semana = obtenerNumeroSemana(FurMaterial.FechaEntrega);
				var producto = _repProducto.FirstById(FurMaterial.IdProducto);
				var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));

				var fur = _repFur.FirstById(FurMaterial.Id);
				fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
				fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
				fur.IdMonedaPagoReal = detalleFur.IdMoneda;
				fur.NumeroCuenta = detalleFur.NumeroCuenta;
				fur.Descripcion = planContable.Descripcion;
				fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
				fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * FurMaterial.Cantidad);
				fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
				fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * FurMaterial.Cantidad);
				fur.IdProveedor = FurMaterial.IdProveedor;
				fur.Cuenta = detalleFur.CuentaDescripcion;
				fur.IdProducto = FurMaterial.IdProducto;
				fur.NumeroSemana = semana;
				fur.Cantidad = FurMaterial.Cantidad;
				fur.Descripcion = detalleFur.Descripcion;
				fur.UsuarioModificacion = FurMaterial.Usuario;
				fur.FechaModificacion = DateTime.Now;
				fur.IdMonedaProveedor = detalleFur.IdMoneda;
				fur.IdFurFaseAprobacion1 = 1;
				fur.Monto = fur.PrecioTotalMonedaOrigen;
				fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * FurMaterial.Cantidad;
				fur.PagoDolares = detalleFur.PrecioDolares * FurMaterial.Cantidad;
				fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
				fur.Monto = fur.PrecioTotalMonedaOrigen;
				estado = _repFur.Update(fur);

				var materialDetalle = _repMaterialPespecificoDetalle.FirstById(FurMaterial.IdMaterialPEspecificoDetalle);
				materialDetalle.FechaEntrega = FurMaterial.FechaEntrega;
				materialDetalle.DireccionEntrega = FurMaterial.DireccionEntrega;
				materialDetalle.UsuarioModificacion = FurMaterial.Usuario;
				materialDetalle.FechaModificacion = DateTime.Now;
				_repMaterialPespecificoDetalle.Update(materialDetalle);

				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		private int obtenerNumeroSemana(DateTime fecha)
		{
			var d = fecha;
			CultureInfo cul = CultureInfo.CurrentCulture;

			var firstDayWeek = cul.Calendar.GetWeekOfYear(
				d,
				CalendarWeekRule.FirstDay,
				DayOfWeek.Monday);

			int weekNum = cul.Calendar.GetWeekOfYear(
				d,
				CalendarWeekRule.FirstDay,
				DayOfWeek.Monday);

			return weekNum;
		}
	}
}

using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static BSI.Integra.Servicios.Controllers.PEspecificoConsumoController;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PEspecificoConsumo")]
    public class PEspecificoConsumoController : BaseController<TPespecificoConsumo, ValidadoPEspecificoConsumoDTO>
    {
        public PEspecificoConsumoController(IIntegraRepository<TPespecificoConsumo> repositorio, ILogger<BaseController<TPespecificoConsumo, ValidadoPEspecificoConsumoDTO>> logger, IIntegraRepository<Persistencia.Models.TLog> repositoriologg) : base(repositorio, logger, repositoriologg)
        { }

        [Route("[action]/{IdPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerTodoPEspecificoPorIdPGeneral(int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                return Ok(_repPEspecifico.ObtenerPEspecificoPorIdPGeneral(IdPGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProducto}")]
        [HttpGet]
        public ActionResult ObtenerProveedorPorProducto(int IdProducto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
                return Ok(_repProveedor.ObtenerProveedorPorProducto(IdProducto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProductoAutocomplete([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                ProductoRepositorio _repProducto = new ProductoRepositorio();
                if (Filtro.Count() > 0 && (Filtro != null && Filtro["Valor"] != null))
                {
                    var lista = _repProducto.ObtenerProductoAutocomplete(Filtro["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdProducto}/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerDetalleHistorio(int IdProducto, int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductoRepositorio _repProducto = new ProductoRepositorio();
                return Ok(_repProducto.ObtenerDetalleHistorio(IdProducto, IdProveedor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdProgramaEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerCursoIndividual(int IdProgramaEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                return Ok(_repPEspecifico.ObtenerCursoIndividual(IdProgramaEspecifico));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPespecifico}/{CursoIndividual}")]
        [HttpGet]
        public ActionResult ObtenerSesionPorPEspecifico(int IdPespecifico, int CursoIndividual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                return Ok(_repPEspecifico.ObtenerSesionPorPEspecifico(IdPespecifico, CursoIndividual));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoProgramaEspecifico([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                if (Filtro.Count() > 0 && (Filtro != null && Filtro["Valor"] != null))
                {
                    var lista = _repPEspecifico.ObtenerPEspecificoPorCentroCosto(Filtro["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ProgramaGeneralAutocomplete([FromBody] Dictionary<string, string> Filtro)
        {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                if (Filtro.Count() > 0 && (Filtro != null && Filtro["Valor"] != null))
                {
                    var lista = _repPGeneral.ObtenerProgramaGeneralAutocomplete(Filtro["Valor"].ToString());
                    return Ok(lista);
                }
                return Ok(new { });

			}
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //public ActionResult Insertar([FromBody] PEspecificoConsumoDTO pEspecificoConsumo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio();

        //        PEspecificoConsumoBO pEspecificoConsumoBO = new PEspecificoConsumoBO();
        //        pEspecificoConsumoBO.IdPespecificoSesion = pEspecificoConsumo.IdPespecificoSesion;
        //        pEspecificoConsumoBO.IdHistoricoProductoProveedor = pEspecificoConsumo.IdHistoricoProductoProveedor;
        //        pEspecificoConsumoBO.Cantidad = pEspecificoConsumo.Cantidad;
        //        pEspecificoConsumoBO.Factor = pEspecificoConsumo.Factor;
        //        pEspecificoConsumoBO.Estado = true;
        //        pEspecificoConsumoBO.UsuarioCreacion = pEspecificoConsumo.Usuario;
        //        pEspecificoConsumoBO.UsuarioModificacion = pEspecificoConsumo.Usuario;
        //        pEspecificoConsumoBO.FechaCreacion = DateTime.Now;
        //        pEspecificoConsumoBO.FechaModificacion = DateTime.Now;

        //        return Ok(_repPEspecificoConsumo.Insert(pEspecificoConsumoBO));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult InsertarFurSesiones([FromBody]List<PEspecificoConsumoDTO> ListaSesionesFur)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				ProductoRepositorio _repProducto = new ProductoRepositorio();
				PlanContableRepositorio _repPlanContable = new PlanContableRepositorio();
				var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(ListaSesionesFur.FirstOrDefault().IdProducto, ListaSesionesFur.FirstOrDefault().IdProveedor);
				var estado = false;
				//var idPersonalAreaTrabajo = _repPersonalAreaTrabajo.GetBy(x => x.Codigo == ListaSesionesFur.FirstOrDefault().AreaAbrev).FirstOrDefault().Id;
				var producto = _repProducto.FirstById(ListaSesionesFur.FirstOrDefault().IdProducto);
				var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));
				
				foreach (var item in ListaSesionesFur)
				{
					PEspecificoConsumoBO pEspecificoConsumo = new PEspecificoConsumoBO()
					{
						IdPespecificoSesion = item.IdPespecificoSesion,
						IdHistoricoProductoProveedor = item.IdHistoricoProductoProveedor,
						Cantidad = item.Cantidad,
						Factor = item.Factor,
						Estado = true,
						UsuarioCreacion = item.Usuario,
						UsuarioModificacion = item.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					_repPEspecificoConsumo.Insert(pEspecificoConsumo);
					int numeroSemana = _repPEspecificoConsumo.ObtenerNumeroSemana(item.FechaHoraInicio);
					FurBO fur = new FurBO();
					fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
					fur.IdPespecifico = item.IdPespecifico;
					fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
					fur.IdMonedaPagoReal = detalleFur.IdMoneda;
					fur.NumeroCuenta = detalleFur.NumeroCuenta;
					fur.Descripcion = planContable.Descripcion;
					fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
					fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
					fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * item.Cantidad);
					fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * item.Cantidad);
					fur.FechaLimite = item.FechaHoraInicio;
					fur.IdCiudad = item.Ciudad;

					//string anio = item.FechaHoraInicio.Year.ToString().Substring(2, 2);
					//string codigo = anio + "-" + "A" + "-" + item.AreaAbrev + "-" + (item.Semana.ToString().Length > 1 ? "" : "0") + item.Semana + "-";
					//var correlativo = _repFur.ObtenerCorrelativo(anio, ((item.Semana.ToString().Length > 1 ? "" : "0") + item.Semana.ToString()),item.AreaAbrev);
					//fur.Codigo = codigo + correlativo.Correlativo;
					//fur.NumeroFur = fur.Codigo;
					fur.IdCentroCosto = item.IdCentroCosto;
					fur.IdProveedor = item.IdProveedor;
					fur.Cuenta = detalleFur.CuentaDescripcion;
					fur.IdProducto = item.IdProducto;
					fur.NumeroSemana = numeroSemana;
					fur.Cantidad = item.Cantidad;
					fur.Descripcion = detalleFur.Descripcion;
					fur.UsuarioSolicitud = item.Usuario;
					fur.Estado = true;
					fur.UsuarioCreacion = item.Usuario;
					fur.UsuarioModificacion = item.Usuario;
					fur.FechaCreacion = DateTime.Now;
					fur.FechaModificacion = DateTime.Now;
					fur.IdMonedaProveedor = detalleFur.IdMoneda;
					fur.IdFurFaseAprobacion1 = 1;
					fur.IdPersonalAreaTrabajo = item.AreaTrabajo;
					fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
					fur.Monto = fur.PrecioTotalMonedaOrigen;
					fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * item.Cantidad;
					fur.PagoDolares = detalleFur.PrecioDolares * item.Cantidad;
					fur.Antiguo = 0;
					fur.OcupadoSolicitud = false;
					fur.OcupadoRendicion = false;
					fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
					estado = _repFur.Insert(fur);
				}
				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]/{IdFurSesion}/{Usuario}")]
		[HttpGet]
		public ActionResult EliminarSesionFur(int IdFurSesion, string Usuario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				FurRepositorio _repFur = new FurRepositorio();
				var estado = false;
				if (_repFur.Exist(IdFurSesion))
					estado = _repFur.Delete(IdFurSesion,Usuario);

				return Ok(estado);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult ActualizarSesionFur([FromBody]FurSesionFiltroDTO FurSesion)
		 {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				ProductoRepositorio _repProducto = new ProductoRepositorio();
				ProveedorRepositorio _repProveedor = new ProveedorRepositorio();
				var producto = _repProducto.FirstById(FurSesion.IdProducto).Nombre;
				var proveedor = _repProveedor.FirstById(FurSesion.IdProveedor);
				var proveedorNombre = string.Concat(proveedor.RazonSocial," ",proveedor.Nombre1," ",proveedor.Nombre2," ",proveedor.ApePaterno," ",proveedor.ApeMaterno).Trim();
				var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(FurSesion.IdProducto, FurSesion.IdProveedor);
				//var idPersonalAreaTrabajo = _repPersonalAreaTrabajo.GetBy(x => x.Codigo == FurSesion.AreaAbrev).FirstOrDefault().Id;
				var estado = false;
				var fur = _repFur.FirstById(FurSesion.Id);
				fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
				fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
				fur.IdMonedaPagoReal = detalleFur.IdMoneda;
				fur.NumeroCuenta = detalleFur.NumeroCuenta;
				fur.Descripcion = detalleFur.Descripcion;
				fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
				fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * FurSesion.Cantidad);
				fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
				fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * FurSesion.Cantidad);
				fur.IdCiudad = FurSesion.Ciudad;
				fur.IdProveedor = FurSesion.IdProveedor;
				fur.Cuenta = detalleFur.CuentaDescripcion;
				fur.IdProducto = FurSesion.IdProducto;
				fur.NumeroSemana = FurSesion.Semana;
				fur.Cantidad = FurSesion.Cantidad;
				fur.Descripcion = detalleFur.Descripcion;
				//fur.UsuarioSolicitud = FurSesion.Usuario;
				fur.UsuarioModificacion = FurSesion.Usuario;
				fur.FechaModificacion = DateTime.Now;
				fur.IdMonedaProveedor = detalleFur.IdMoneda;
				fur.IdFurFaseAprobacion1 = 1;
				fur.IdPersonalAreaTrabajo = FurSesion.AreaTrabajo;
				fur.Monto = fur.PrecioTotalMonedaOrigen;
				fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * FurSesion.Cantidad;
				fur.PagoDolares = detalleFur.PrecioDolares * FurSesion.Cantidad;
				fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
				fur.Monto = fur.PrecioTotalMonedaOrigen;
				estado = _repFur.Update(fur);

				return Ok(new { Estado = estado, Fur = fur, Producto = producto, Proveedor = proveedorNombre });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[Action]")]
		[HttpPost]
		public ActionResult InsertarFurPrograma([FromBody]FurProgramaDTO FurPrograma)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
				PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio();
				HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio();
				PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio();
				PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
				FurRepositorio _repFur = new FurRepositorio();
				ProductoRepositorio _repProducto = new ProductoRepositorio();
				PlanContableRepositorio _repPlanContable = new PlanContableRepositorio();

				var programaEspecifico = _repPespecifico.FirstById(FurPrograma.IdPespecifico);
				var pespecificoSesionInicio = _repPespecificoSesion.FirstById(programaEspecifico.IdSesionInicio.Value);
				var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(FurPrograma.IdProducto, FurPrograma.IdProveedor);
				var estado = false;
				//var idPersonalAreaTrabajo = _repPersonalAreaTrabajo.GetBy(x => x.Codigo == FurPrograma.AreaAbrev).FirstOrDefault().Id;
				var semana = obtenerNumeroSemana(pespecificoSesionInicio.FechaHoraInicio);
				var producto = _repProducto.FirstById(FurPrograma.IdProducto);
				var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));

				FurBO fur = new FurBO();
				fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
				fur.IdPespecifico = FurPrograma.IdPespecifico;
				fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
				fur.IdMonedaPagoReal = detalleFur.IdMoneda;
				fur.NumeroCuenta = detalleFur.NumeroCuenta;
				fur.Descripcion = planContable.Descripcion;
				fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
				fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
				fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * FurPrograma.Cantidad);
				fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * FurPrograma.Cantidad);
				fur.FechaLimite = pespecificoSesionInicio.FechaHoraInicio;
				fur.IdCiudad = FurPrograma.Ciudad;
				fur.IdCentroCosto = programaEspecifico.IdCentroCosto;
				fur.IdProveedor = FurPrograma.IdProveedor;
				fur.Cuenta = detalleFur.CuentaDescripcion;
				fur.IdProducto = FurPrograma.IdProducto;
				fur.NumeroSemana = semana;
				fur.Cantidad = FurPrograma.Cantidad;
				fur.Descripcion = detalleFur.Descripcion;
				fur.UsuarioSolicitud = FurPrograma.Usuario;
				fur.Estado = true;
				fur.UsuarioCreacion = FurPrograma.Usuario;
				fur.UsuarioModificacion = FurPrograma.Usuario;
				fur.FechaCreacion = DateTime.Now;
				fur.FechaModificacion = DateTime.Now;
				fur.IdMonedaProveedor = detalleFur.IdMoneda;
				fur.IdFurFaseAprobacion1 = 1;
				fur.IdPersonalAreaTrabajo = FurPrograma.AreaTrabajo;
				fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
				fur.Monto = fur.PrecioTotalMonedaOrigen;
				fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * FurPrograma.Cantidad;
				fur.PagoDolares = detalleFur.PrecioDolares * FurPrograma.Cantidad;
				fur.Antiguo = 0;
				fur.OcupadoSolicitud = false;
				fur.OcupadoRendicion = false;
				fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
				estado = _repFur.Insert(fur);

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

    public class ValidadoPEspecificoConsumoDTO : AbstractValidator<TPespecificoConsumo>
    {
        public static ValidadoPEspecificoConsumoDTO Current = new ValidadoPEspecificoConsumoDTO();
        public ValidadoPEspecificoConsumoDTO()
        {
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/BoletoAereoDetalle")]
    public class PresencialBoletoAereoDetalleController : ControllerBase
    {
        // GET: api/PresencialBoletoAereoDetalle
        [HttpGet]
		[Route("[Action]/{IdBoletoAereo}")]
        public ActionResult ObtenerBoletoAereoDetalle(int IdBoletoAereo)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoDetalleRepositorio _repBoletoAereoDetalle = new RaPresencialBoletoAereoDetalleRepositorio();
				var detalleBoletoAereo = _repBoletoAereoDetalle.ObtenerBoletoAereoDetalle(IdBoletoAereo);
				return Ok(detalleBoletoAereo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

		[HttpGet]
		[Route("[Action]")]
		public ActionResult ObtenerCombosBoletoAereoDetalle()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaAerolineaRepositorio _repBoletoAereLinea = new RaAerolineaRepositorio();
				RaTipoBoletoAereoRepositorio _repTipoBoletoAereo = new RaTipoBoletoAereoRepositorio();
				var Aerolineas = _repBoletoAereLinea.ObtenerAerolineasFiltro();
				var Tipo = _repTipoBoletoAereo.ObtenerTipoBoletoAereoFiltro();
				return Ok(new { Aerolineas, Tipo });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		
		[HttpPost]
		[Route("[Action]")]
		public ActionResult RegistrarBoletoAereoDetalle([FromBody]CompuestoBoletoAereoDetalleDTO BoletoAereoDetalleFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoDetalleRepositorio _repBoletoAereoDetalle = new RaPresencialBoletoAereoDetalleRepositorio();
				RaPresencialBoletoAereoDetalleBO boletoAereoDetalle = new RaPresencialBoletoAereoDetalleBO()
				{
					IdRaPresencialBoletoAereo = BoletoAereoDetalleFiltro.BoletoAereoDetalle.IdRaPresencialBoletoAereo,
					IdRaAerolinea = BoletoAereoDetalleFiltro.BoletoAereoDetalle.IdRaAerolinea,
					Fecha = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Fecha,
					Origen = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Origen,
					HoraSalida = BoletoAereoDetalleFiltro.BoletoAereoDetalle.HoraSalida,
					Destino = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Destino,
					HoraLlegada = BoletoAereoDetalleFiltro.BoletoAereoDetalle.HoraLlegada,
					NumeroVuelo = BoletoAereoDetalleFiltro.BoletoAereoDetalle.NumeroVuelo,
					CodigoReserva = BoletoAereoDetalleFiltro.BoletoAereoDetalle.CodigoReserva,
					IdRaTipoBoletoAereo = BoletoAereoDetalleFiltro.BoletoAereoDetalle.IdRaTipoBoletoAereo,
					Estado = true,
					UsuarioCreacion = BoletoAereoDetalleFiltro.Usuario,
					UsuarioModificacion = BoletoAereoDetalleFiltro.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				_repBoletoAereoDetalle.Insert(boletoAereoDetalle);
				return Ok(boletoAereoDetalle.Id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[Action]/{IdBoletoAereoDetalle}")]
		public ActionResult ObtenerBoletoAereoDetalleActualizar(int IdBoletoAereoDetalle)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoDetalleRepositorio _repBoletoAereoDetalle = new RaPresencialBoletoAereoDetalleRepositorio();
				if (_repBoletoAereoDetalle.Exist(IdBoletoAereoDetalle))
				{
					var boletoAereoDetalle = _repBoletoAereoDetalle.GetBy(x => x.Id ==IdBoletoAereoDetalle, x => new BoletoAereoDetalleDTO {
						Id = x.Id,
						IdRaPresencialBoletoAereo = x.IdRaPresencialBoletoAereo,
						IdRaAerolinea = x.IdRaAerolinea,
						Fecha = x.Fecha,
						Origen = x.Origen,
						HoraSalida = x.HoraSalida,
						Destino = x.Destino,
						HoraLlegada = x.HoraLlegada,
						NumeroVuelo = x.NumeroVuelo,
						IdRaTipoBoletoAereo = x.IdRaTipoBoletoAereo,
						CodigoReserva = x.CodigoReserva}).FirstOrDefault();
					return Ok(boletoAereoDetalle);
				}
				else
				{
					return BadRequest("El boleto aereo detalle no existe");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarBoletoAereoDetalle([FromBody]CompuestoBoletoAereoDetalleDTO BoletoAereoDetalleFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoDetalleRepositorio _repBoletoAereoDetalle = new RaPresencialBoletoAereoDetalleRepositorio();

				if (_repBoletoAereoDetalle.Exist(BoletoAereoDetalleFiltro.BoletoAereoDetalle.Id))
				{
					var boletoAreoDetalle = _repBoletoAereoDetalle.FirstById(BoletoAereoDetalleFiltro.BoletoAereoDetalle.Id);
					
					boletoAreoDetalle.IdRaAerolinea = BoletoAereoDetalleFiltro.BoletoAereoDetalle.IdRaAerolinea;
					boletoAreoDetalle.Fecha = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Fecha;
					boletoAreoDetalle.Origen = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Origen;
					boletoAreoDetalle.HoraSalida = BoletoAereoDetalleFiltro.BoletoAereoDetalle.HoraSalida;
					boletoAreoDetalle.Destino = BoletoAereoDetalleFiltro.BoletoAereoDetalle.Destino;
					boletoAreoDetalle.HoraLlegada = BoletoAereoDetalleFiltro.BoletoAereoDetalle.HoraLlegada;
					boletoAreoDetalle.NumeroVuelo = BoletoAereoDetalleFiltro.BoletoAereoDetalle.NumeroVuelo;
					boletoAreoDetalle.IdRaTipoBoletoAereo = BoletoAereoDetalleFiltro.BoletoAereoDetalle.IdRaTipoBoletoAereo;
					boletoAreoDetalle.CodigoReserva = BoletoAereoDetalleFiltro.BoletoAereoDetalle.CodigoReserva;
					boletoAreoDetalle.UsuarioModificacion = BoletoAereoDetalleFiltro.Usuario;
					boletoAreoDetalle.FechaModificacion = DateTime.Now;

					_repBoletoAereoDetalle.Update(boletoAreoDetalle);
					return Ok(boletoAreoDetalle.Id);
				}
				else
				{
					return BadRequest("No existe boleto aereo detalle");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

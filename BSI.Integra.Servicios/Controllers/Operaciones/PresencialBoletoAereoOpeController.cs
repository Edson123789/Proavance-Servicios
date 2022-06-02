using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Operaciones/BoletoAereo")]
    public class PresencialBoletoAereoOpeController : Controller
    {
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerCombosBoletosAereos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToString());
			}
			try
			{
				RaHotelRepositorio _repHotel= new RaHotelRepositorio();
				RaMovilidadRepositorio _repMovilidad = new RaMovilidadRepositorio();
				RaPresencialBoletoAereoRepositorio _repBoletoAereo = new RaPresencialBoletoAereoRepositorio();
				var listaHoteles = _repHotel.ObtenerHotelesParaCombo();
				var listaMovilidades = _repMovilidad.ObtenerMovilidadesParaCombo();

				return Ok(new { Hoteles = listaHoteles, Movilidades = listaMovilidades });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("[Action]")]
		[HttpPost]
		public ActionResult RegistrarBoletoAereo([FromBody]CompuestoBoletoAereoDTO BoletoAereoFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoRepositorio _repBoletoAereo = new RaPresencialBoletoAereoRepositorio();
				RaPresencialBoletoAereoBO boletoAereo = new RaPresencialBoletoAereoBO()
				{
					IdRaSesion = BoletoAereoFiltro.BoletoAereo.IdRaSesion,
					IdRaHotel = BoletoAereoFiltro.BoletoAereo.IdRaHotel,
					IdRaMovilidad = BoletoAereoFiltro.BoletoAereo.IdRaMovilidad,
					FechaCoordinacionEstadia = BoletoAereoFiltro.BoletoAereo.FechaCoordinacionEstadia,
					Estado = true,
					UsuarioCreacion = BoletoAereoFiltro.Usuario,
					UsuarioModificacion = BoletoAereoFiltro.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				_repBoletoAereo.Insert(boletoAereo);

				return Ok(boletoAereo.Id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("[Action]/{IdRaSesion}")]
		[HttpGet]
		public ActionResult ObteneBoletoAereo(int IdRaSesion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToString());
			}
			try
			{
				RaHotelRepositorio _repHotel = new RaHotelRepositorio();
				RaMovilidadRepositorio _repMovilidad = new RaMovilidadRepositorio();
				RaPresencialBoletoAereoRepositorio _repBoletoAereo = new RaPresencialBoletoAereoRepositorio();
				var BoletoAereo = _repBoletoAereo.ObtenerBoletoAereo(IdRaSesion);

				return Ok(BoletoAereo);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[Action]/{IdBoletoAereo}")]
		public ActionResult ObtenerBoletoAereoActualizar(int IdBoletoAereo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoRepositorio _repBoletoAereo = new RaPresencialBoletoAereoRepositorio();
				if (_repBoletoAereo.Exist(IdBoletoAereo))
				{
					var boletoAereo = _repBoletoAereo.GetBy(x => x.Id == IdBoletoAereo, x => new BoletoAereoFiltroDTO
					{
						Id = x.Id,
						IdRaSesion = x.IdRaSesion == null?default: x.IdRaSesion.Value,
						IdRaHotel = x.IdRaHotel == null ? default : x.IdRaHotel.Value,
						IdRaMovilidad = x.IdRaMovilidad == null ? default : x.IdRaMovilidad.Value,
						FechaCoordinacionEstadia = x.FechaCoordinacionEstadia,
				}).FirstOrDefault();
					return Ok(boletoAereo);
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
		public ActionResult ActualizarBoletoAereo([FromBody]CompuestoBoletoAereoDTO BoletoAereoFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoRepositorio _repBoletoAereo = new RaPresencialBoletoAereoRepositorio();
				if (_repBoletoAereo.Exist(BoletoAereoFiltro.BoletoAereo.Id))
				{
					var boletoAereo = _repBoletoAereo.FirstById(BoletoAereoFiltro.BoletoAereo.Id);
					boletoAereo.IdRaHotel = BoletoAereoFiltro.BoletoAereo.IdRaHotel;
					boletoAereo.IdRaMovilidad = BoletoAereoFiltro.BoletoAereo.IdRaMovilidad;
					boletoAereo.FechaCoordinacionEstadia = BoletoAereoFiltro.BoletoAereo.FechaCoordinacionEstadia;
					boletoAereo.UsuarioModificacion = BoletoAereoFiltro.Usuario;
					boletoAereo.FechaModificacion = DateTime.Now;
					_repBoletoAereo.Update(boletoAereo);
					return Ok(boletoAereo.Id);
				}
				else
				{
					return BadRequest("No existe el boleto aereo");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

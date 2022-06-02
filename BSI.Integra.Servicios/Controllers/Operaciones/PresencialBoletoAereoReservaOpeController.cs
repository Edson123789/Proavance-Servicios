using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/BoletoAereoReserva")]
    public class PresencialBoletoAereoReservaOpeController : Controller
    {
        // GET: api/PresencialBoletoAereoReservaOpe
        [HttpGet]
		[Route("[Action]/{IdRaPresencialBoletoAereo}")]
        public ActionResult ObtenerListaBoletosAereos(int IdRaPresencialBoletoAereo)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoReservaRepositorio _repBoletoAereoReserva = new RaPresencialBoletoAereoReservaRepositorio();
				var listaBoletoAereoReserva = _repBoletoAereoReserva.ObtenerListaBoletosAereosReserva(IdRaPresencialBoletoAereo);
				return Ok(listaBoletoAereoReserva);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

		[HttpGet]
		[Route("[Action]")]
		public ActionResult ObtenerComboFormulario()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaAerolineaRepositorio _repAerolinea = new RaAerolineaRepositorio();
				var listaAerolineas = _repAerolinea.ObtenerAerolineasFiltro();
				return Ok(new { Aerolineas = listaAerolineas });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult RegistrarBoletoAereoReserva([FromBody]CompuestoBoletoAereoReservaDTO BoletoAereoReservaFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoReservaRepositorio _repBoletoAereoReserva = new RaPresencialBoletoAereoReservaRepositorio();
				RaPresencialBoletoAereoReservaBO boletoAereoReserva = new RaPresencialBoletoAereoReservaBO()
				{
					IdRaPresencialBoletoAereo = BoletoAereoReservaFiltro.BoletoAereoReserva.IdRaPresencialBoletoAereo,
					IdRaAerolinea = BoletoAereoReservaFiltro.BoletoAereoReserva.IdRaAerolinea,
					LinkBoarding = BoletoAereoReservaFiltro.BoletoAereoReserva.LinkBoarding,
					CodigoReserva = BoletoAereoReservaFiltro.BoletoAereoReserva.CodigoReserva,
					Estado = true,
					UsuarioCreacion = BoletoAereoReservaFiltro.Usuario,
					UsuarioModificacion = BoletoAereoReservaFiltro.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				_repBoletoAereoReserva.Insert(boletoAereoReserva);
				return Ok(boletoAereoReserva.Id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarBoletoAereoReserva([FromBody]CompuestoBoletoAereoReservaDTO BoletoAereoReservaFiltro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoReservaRepositorio _repBoletoAereoReserva = new RaPresencialBoletoAereoReservaRepositorio();
				if (_repBoletoAereoReserva.Exist(BoletoAereoReservaFiltro.BoletoAereoReserva.Id))
				{
					var boletoAereoReserva = _repBoletoAereoReserva.FirstById(BoletoAereoReservaFiltro.BoletoAereoReserva.Id);
					boletoAereoReserva.IdRaAerolinea = BoletoAereoReservaFiltro.BoletoAereoReserva.IdRaAerolinea;
					boletoAereoReserva.LinkBoarding = BoletoAereoReservaFiltro.BoletoAereoReserva.LinkBoarding;
					boletoAereoReserva.CodigoReserva = BoletoAereoReservaFiltro.BoletoAereoReserva.CodigoReserva;
					boletoAereoReserva.UsuarioModificacion = BoletoAereoReservaFiltro.Usuario;
					boletoAereoReserva.FechaModificacion = DateTime.Now;

					_repBoletoAereoReserva.Update(boletoAereoReserva);
					return Ok(boletoAereoReserva.Id);
				}
				else
				{
					return BadRequest("El boleto aereo reserva no existe");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet]
		[Route("[Action]/{IdRaBoletoAereoReserva}")]
		public ActionResult ObtenerBoletoAereoReserva(int IdRaBoletoAereoReserva)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaPresencialBoletoAereoReservaRepositorio _repBoletoAereoReserva = new RaPresencialBoletoAereoReservaRepositorio();
				var boletoAereoReserva = _repBoletoAereoReserva.GetBy(x => x.Id == IdRaBoletoAereoReserva, x => new { x.Id, x.IdRaPresencialBoletoAereo, x.IdRaAerolinea, x.LinkBoarding, x.CodigoReserva }).FirstOrDefault();
				return Ok(boletoAereoReserva);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}

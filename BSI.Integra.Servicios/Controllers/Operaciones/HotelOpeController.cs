using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/Operaciones/Hotel")]
	public class HotelOpeController : Controller
	{
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ObtenerHoteles()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToString());
			}
			try
			{
				RaHotelRepositorio _repHotel = new RaHotelRepositorio();
				var listaHoteles = _repHotel.ObtenerListaHoteles();
				return Ok(listaHoteles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("[Action]")]
		[HttpGet]
		public ActionResult ObtenerCiudades()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaHotelRepositorio _repHotel = new RaHotelRepositorio();
				var listaCiudades = _repHotel.ObtenerCiudades();
				return Ok(listaCiudades);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		[HttpGet]
		public ActionResult ObtenerSedes()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaSedeRepositorio _repSede = new RaSedeRepositorio();
				var listaSedes = _repSede.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = string.Concat(x.Nombre, " - ", x.Ciudad) });
				return Ok(listaSedes);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}

		}
		[Route("[action]")]
		[HttpPut]
		public IActionResult ActualizarHotel([FromBody] CompuestoHotelDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaHotelRepositorio repHotel = new RaHotelRepositorio();
				RaHotelBO hotel = new RaHotelBO();
				using (TransactionScope scope = new TransactionScope())
				{
					if (repHotel.Exist(Json.Hotel.Id))
					{
						hotel = repHotel.FirstById(Json.Hotel.Id);
						hotel.Nombre = Json.Hotel.Nombre;
						hotel.Telefono = Json.Hotel.Telefono;
						hotel.IdCiudad = Json.Hotel.IdCiudad;
						hotel.Direccion = Json.Hotel.Direccion;
						hotel.CubrimosDesayuno = Json.Hotel.CubrimosDesayuno;
						hotel.IdRaSede = Json.Hotel.IdRaSede;
						hotel.UsuarioModificacion = Json.Usuario;
						hotel.FechaModificacion = DateTime.Now;
						repHotel.Update(hotel);
					}
					scope.Complete();
					Json.Hotel.Id = hotel.Id;
				}

				return Ok(Json.Hotel);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarHotel([FromBody] CompuestoHotelDTO Json)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				RaHotelRepositorio repHotel = new RaHotelRepositorio();
				RaHotelBO hotel = new RaHotelBO();
				using (TransactionScope scope = new TransactionScope())
				{

					hotel.Nombre = Json.Hotel.Nombre;
					hotel.Telefono = Json.Hotel.Telefono;
					hotel.IdCiudad = Json.Hotel.IdCiudad;
					hotel.Direccion = Json.Hotel.Direccion;
					hotel.CubrimosDesayuno = Json.Hotel.CubrimosDesayuno;
					hotel.IdRaSede = Json.Hotel.IdRaSede;
					hotel.UsuarioCreacion = Json.Usuario;
					hotel.UsuarioModificacion = Json.Usuario;
					hotel.FechaCreacion = DateTime.Now;
					hotel.FechaModificacion = DateTime.Now;
					hotel.Estado = true;
					repHotel.Insert(hotel);
					scope.Complete();
				}
				Json.Hotel.Id = hotel.Id;

				return Ok(Json.Hotel);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}



	}
}

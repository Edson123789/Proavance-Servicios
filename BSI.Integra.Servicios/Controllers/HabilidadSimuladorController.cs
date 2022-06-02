using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Servicios.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HabilidadSimuladorController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;

		public HabilidadSimuladorController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerHabilidadSimulador()
		{

			try
			{
				HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var cmbTP = _repHabilitarSimulador.ObtenerHabilidadesSimulador();
				return Ok(cmbTP);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarHabilidadesSimulador([FromBody] HabilidadSimuladorDTO json)
		{

			try
			{
				HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var respuesta = _repHabilitarSimulador.InsertarHabilidadesSimulador(json.Nombre, json.PuntajeMaximo, json.PuntajeMinimo, json.Usuario);
				return Ok(respuesta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarHabilidadesSimulador([FromBody] HabilidadSimuladorDTO json)
		{
			try
			{
				HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var existe = _repHabilitarSimulador.FirstByIdHabilidadesSimulador(json.Id);
				if(existe)
                {
					_repHabilitarSimulador.ActualizarHabilidadesSimulador(json.Id, json.Nombre, json.PuntajeMaximo, json.PuntajeMinimo, json.Usuario);
					return Ok(json);
				}
                else
                {
					return Ok(false);
                }
				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult EliminarHabilidadesSimulador([FromBody] HabilidadSimuladorDTO json)
		{
			try
			{
				HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var cmbTP = _repHabilitarSimulador.EliminarHabilidadesSimulador(json.Id);
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

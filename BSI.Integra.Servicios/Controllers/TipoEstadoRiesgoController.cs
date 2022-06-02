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
    public class TipoEstadoRiesgoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public TipoEstadoRiesgoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerTipoEstadoRiesgo()
		{
            try
			{
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();

				var cmbTP = _repHabilitarSimulador.ObtenerTipoEstadoRiesgo();
				return Ok(cmbTP);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarTipoEstadoRiesgo([FromBody] TipoEstadoRiesgoDTO json)
		{

			try
			{
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var rpta = _repHabilitarSimulador.InsertarTipoEstadoRiesgo(json.Nombre, json.Usuario);
				return Ok(rpta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarTipoEstadoRiesgo([FromBody] TipoEstadoRiesgoDTO json)
		{
			try
			{
				//HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				var existe = _repHabilitarSimulador.FirstByIdTipoEstadoRiesgo(json.Id);
				if (existe)
				{
					_repHabilitarSimulador.ActualizarTipoEstadoRiesgo(json.Id, json.Nombre,  json.Usuario);
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
		public IActionResult EliminarTipoEstadoRiesgo([FromBody] TipoEstadoRiesgoDTO json)
		{
			try
			{
				//HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				var cmbTP = _repHabilitarSimulador.EliminarTipoEstadoRiesgo(json.Id);
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

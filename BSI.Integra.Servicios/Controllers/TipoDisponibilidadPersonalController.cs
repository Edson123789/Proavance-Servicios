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
    public class TipoDisponibilidadPersonalController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public TipoDisponibilidadPersonalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }


		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerTipoDisponibilidadPersonal()
		{

			try
			{
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();

				var cmbTP = _repHabilitarSimulador.ObtenerTipoDisponibilidadPersonal();
				return Ok(cmbTP);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarTipoDisponibilidadPersonal([FromBody] TipoDisponibilidadPersonalDTO json)
		{

			try
			{
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				//MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
				var rpta= _repHabilitarSimulador.InsertarTipoDisponibilidadPersonal(json.Nombre, json.GeneraCosto, json.Usuario);
				return Ok(rpta);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarTipoDisponibilidadPersonal([FromBody] TipoDisponibilidadPersonalDTO json)
		{
			try
			{
				//HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				var existe = _repHabilitarSimulador.FirstByIdTipoDisponibilidadPersonal(json.Id);
				if(existe)
                {
					_repHabilitarSimulador.ActualizarTipoDisponibilidadPersonal(json.Id, json.Nombre, json.GeneraCosto, json.Usuario);
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
		public IActionResult EliminarTipoDisponibilidadPersonal([FromBody] TipoDisponibilidadPersonalDTO json)
		{
			try
			{
				//HabilidadSimuladorRepositorio _repHabilitarSimulador = new HabilidadSimuladorRepositorio();
				TipoDisponibilidadPersonalRepositorio _repHabilitarSimulador = new TipoDisponibilidadPersonalRepositorio();
				var cmbTP = _repHabilitarSimulador.EliminarTipoDisponibilidadPersonal(json.Id);
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}

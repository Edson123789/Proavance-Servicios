using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/NuevoAlumnoCongelado")]
    [ApiController]
    public class NuevoAlumnoCongeladoController : Controller
    {
		private readonly integraDBContext _integraDBContext;
		private NuevoAlumnoCongeladoBO NuevoAlumnoCongelado;

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Autor modificacion: Lisbeth Ortogorin
		/// Fecha: 12/06/2021
		/// Versión: 2.0
		/// <summary>
		/// Registra en la tabla fin.T_NuevoAlumnoCongelado los datos del excel
		/// Se cambio las cabeceras del excel 
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult MostrarDatosExcel([FromForm] IFormFile ArchivoExcel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var ListaReporteFlujo = new List<NuevoAlumnoCongeladoExcelDTO>();
				int index = 0;
				using (var cvs = new CsvReader(new StreamReader(ArchivoExcel.OpenReadStream())))
				{
					cvs.Configuration.Delimiter = ";";
					cvs.Configuration.MissingFieldFound = null;
					cvs.Configuration.BadDataFound = null;
					cvs.Read();
					cvs.ReadHeader();
					while (cvs.Read())
					{
						index++;

						NuevoAlumnoCongeladoExcelDTO flujoData = new NuevoAlumnoCongeladoExcelDTO();
						flujoData.CodigoMatricula = cvs.GetField<string>("Codigo Matricula");
						flujoData.NroCuota = cvs.GetField<int>("Nro Cuota");
						flujoData.NroSubCuota = cvs.GetField<int>("Nro Subcuota");
						flujoData.FechaVencimiento = cvs.GetField<string>("Fecha Vencimiento Actual") == "" ? DateTime.Today : cvs.GetField<DateTime>("Fecha Vencimiento Actual");
						flujoData.Cuota = cvs.GetField<decimal>("Total Cuota Dolar");
						flujoData.Saldo = cvs.GetField<decimal>("Saldo Pendiente");
						flujoData.Mora = cvs.GetField<decimal>("Mora");
						flujoData.MontoPagado = cvs.GetField<decimal>("Real Pago Dolar");
						flujoData.FechaPago = cvs.GetField<string>("Fecha Pago") == "" ? DateTime.Today : cvs.GetField<DateTime>("Fecha Pago");
						ListaReporteFlujo.Add(flujoData);
					}
				}
				var Nregistros = index;
				return Ok(new { ListaReporteFlujo, Nregistros });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta los datos de la tabla fin.T_NuevoAlumnoCongelado
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarNuevoAlumnoCongelado([FromBody] NuevoAlumnoCongeladoDTO json)
		{

			try
			{
				NuevoAlumnoCongeladoRepositorio _repNuevoAlumnoCongelado = new NuevoAlumnoCongeladoRepositorio();
				NuevoAlumnoCongelado = new NuevoAlumnoCongeladoBO();
				NuevoAlumnoCongelado.IdMatriculaCabecera = json.IdMatriculaCabecera;
				NuevoAlumnoCongelado.NroCuota = json.NroCuota;
				NuevoAlumnoCongelado.NroSubCuota = json.NroSubCuota;
				NuevoAlumnoCongelado.FechaVencimiento = json.FechaVencimiento;
				//NuevoAlumnoCongelado.TotalPagar = 0;
				NuevoAlumnoCongelado.Cuota = json.Cuota;
				NuevoAlumnoCongelado.Saldo = json.Saldo;
				NuevoAlumnoCongelado.Mora = json.Mora;
				NuevoAlumnoCongelado.MontoPagado = json.MontoPagado;
				NuevoAlumnoCongelado.Cancelado = json.Cancelado;
				NuevoAlumnoCongelado.TipoCuota = json.TipoCuota;
				NuevoAlumnoCongelado.Moneda = json.Moneda;
				NuevoAlumnoCongelado.FechaPago = json.FechaPago;
				NuevoAlumnoCongelado.FechaCongelamiento = DateTime.Now;
				NuevoAlumnoCongelado.UsuarioCreacion = json.Usuario;
				NuevoAlumnoCongelado.UsuarioModificacion = json.Usuario;
				NuevoAlumnoCongelado.FechaCreacion = DateTime.Now;
				NuevoAlumnoCongelado.FechaModificacion = DateTime.Now;
				NuevoAlumnoCongelado.Estado = true;

				_repNuevoAlumnoCongelado.Insert(NuevoAlumnoCongelado);
				return Ok(NuevoAlumnoCongelado);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza los datos de la tabla fin.T_NuevoAlumnoCongelado
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarNuevoAlumnoCongelado([FromBody] NuevoAlumnoCongeladoDTO json)
		{
			try
			{
				NuevoAlumnoCongelado = new NuevoAlumnoCongeladoBO();
				NuevoAlumnoCongeladoRepositorio _repNuevoAlumnoCongelado = new NuevoAlumnoCongeladoRepositorio();
				if (_repNuevoAlumnoCongelado.Exist(json.Id))
				{
					NuevoAlumnoCongelado = _repNuevoAlumnoCongelado.FirstById(json.Id);
					NuevoAlumnoCongelado.IdMatriculaCabecera = json.IdMatriculaCabecera;
					NuevoAlumnoCongelado.NroCuota = json.NroCuota;
					NuevoAlumnoCongelado.NroSubCuota = json.NroSubCuota;
					NuevoAlumnoCongelado.FechaVencimiento = json.FechaVencimiento;
					//NuevoAlumnoCongelado.TotalPagar = json.TotalPagar;
					NuevoAlumnoCongelado.Cuota = json.Cuota;
					NuevoAlumnoCongelado.Saldo = json.Saldo;
					NuevoAlumnoCongelado.Mora = json.Mora;
					NuevoAlumnoCongelado.MontoPagado = json.MontoPagado;
					NuevoAlumnoCongelado.Cancelado = json.Cancelado;
					NuevoAlumnoCongelado.TipoCuota = json.TipoCuota;
					NuevoAlumnoCongelado.Moneda = json.Moneda;
					NuevoAlumnoCongelado.FechaPago = json.FechaPago;
					NuevoAlumnoCongelado.UsuarioModificacion = json.Usuario;
					NuevoAlumnoCongelado.FechaModificacion = DateTime.Now;
					NuevoAlumnoCongelado.Estado = true;
					_repNuevoAlumnoCongelado.Update(NuevoAlumnoCongelado);

				}
				return Ok(NuevoAlumnoCongelado);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina los datos de la tabla fin.T_NuevoAlumnoCongelado
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult EliminarNuevoAlumnoCongelado([FromBody] NuevoAlumnoCongeladoDTO json)
		{
			try
			{
				NuevoAlumnoCongeladoRepositorio _repHabilitarSimulador = new NuevoAlumnoCongeladoRepositorio();
				bool result = false;
				if (_repHabilitarSimulador.Exist(json.Id))
				{
					result = _repHabilitarSimulador.Delete(json.Id, json.Usuario);
				}
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza los los datos de la tabla fin.T_NuevoAlumnoCongelado
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarExcelAlumnoCongelado([FromBody] FiltroNuevoAlumnoCongeladoExcelDTO json)
		{
			try
			{
				NuevoAlumnoCongeladoRepositorio _repNuevoAlumnoCongelado = new NuevoAlumnoCongeladoRepositorio();
				MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                _repNuevoAlumnoCongelado.InsertarExcelNuevoAlumnoCongelado(json.ListaAlumnoCongelado,json.FechaCongelamiento,json.IdPeriodo,json.Usuario);
				return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: GET
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 26/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Trae los datos de la tabla fin.T_NuevoAlumnoCongelado
		/// </summary>
		/// <returns>Lista de objetos<returns>
		[Route("[action]")]
		[HttpGet]
		public IActionResult ObtenerListaNuevoAlumnoCongelado()
		{

			try
			{
				NuevoAlumnoCongeladoRepositorio _repNuevoAlumnoCongelado = new NuevoAlumnoCongeladoRepositorio();
				var listaNuevoAlumnoCongelado = _repNuevoAlumnoCongelado.ObtenerListaNuevoAlumnoCongelado();
				return Ok(listaNuevoAlumnoCongelado);


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

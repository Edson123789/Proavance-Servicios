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
    [Route("api/PgeneralProyectoAplicacionAnexo")]
    [ApiController]
    public class PgeneralProyectoAplicacionAnexoController : Controller
    {
		private readonly integraDBContext _integraDBContext;
		private PgeneralProyectoAplicacionAnexoBO PgeneralProyectoAplicacionAnexo;

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 24/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult InsertarPgeneralProyectoAplicacionAnexo([FromBody] PgeneralProyectoAplicacionAnexoDTO json)
		{

			try
			{
				PgeneralProyectoAplicacionAnexoRepositorio _repPgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoRepositorio();
				PgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoBO();
				PgeneralProyectoAplicacionAnexo.IdPgeneral = json.IdPgeneral;
				PgeneralProyectoAplicacionAnexo.NombreArchivo = json.NombreArchivo;
				PgeneralProyectoAplicacionAnexo.RutaArchivo = json.RutaArchivo;
				PgeneralProyectoAplicacionAnexo.EsEnlace = json.EsEnlace;
				PgeneralProyectoAplicacionAnexo.SoloLectura = json.SoloLectura;
				PgeneralProyectoAplicacionAnexo.UsuarioCreacion = json.Usuario;
				PgeneralProyectoAplicacionAnexo.UsuarioModificacion = json.Usuario;
				PgeneralProyectoAplicacionAnexo.FechaCreacion = DateTime.Now;
				PgeneralProyectoAplicacionAnexo.FechaModificacion = DateTime.Now;
				PgeneralProyectoAplicacionAnexo.Estado = true;

				_repPgeneralProyectoAplicacionAnexo.Insert(PgeneralProyectoAplicacionAnexo);
				return Ok(PgeneralProyectoAplicacionAnexo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 03/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult ActualizarPgeneralProyectoAplicacionAnexo([FromBody] PgeneralProyectoAplicacionAnexoDTO json)
		{
			try
			{
				PgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoBO();
				PgeneralProyectoAplicacionAnexoRepositorio _repPgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoRepositorio();
				if (_repPgeneralProyectoAplicacionAnexo.Exist(json.Id))
				{
					PgeneralProyectoAplicacionAnexo = _repPgeneralProyectoAplicacionAnexo.FirstById(json.Id);
					PgeneralProyectoAplicacionAnexo.IdPgeneral = json.IdPgeneral;
					PgeneralProyectoAplicacionAnexo.NombreArchivo = json.NombreArchivo;
					PgeneralProyectoAplicacionAnexo.RutaArchivo = json.RutaArchivo;
					PgeneralProyectoAplicacionAnexo.EsEnlace = json.EsEnlace;
					PgeneralProyectoAplicacionAnexo.SoloLectura = json.SoloLectura;
					PgeneralProyectoAplicacionAnexo.UsuarioModificacion = json.Usuario;
					PgeneralProyectoAplicacionAnexo.FechaModificacion = DateTime.Now;
					PgeneralProyectoAplicacionAnexo.Estado = true;
					_repPgeneralProyectoAplicacionAnexo.Update(PgeneralProyectoAplicacionAnexo);

				}
				return Ok(json);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 03/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult EliminarPgeneralProyectoAplicacionAnexo([FromBody] PgeneralProyectoAplicacionAnexoDTO json)
		{
			try
			{
				PgeneralProyectoAplicacionAnexoRepositorio _repHabilitarSimulador = new PgeneralProyectoAplicacionAnexoRepositorio();
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
		/// TipoFuncion: GET
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 03/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Trae los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Lista de objetos<returns>
		[Route("[action]/{Id}")]
		[HttpGet]
		public IActionResult ObtenerListaPgeneralProyectoAplicacionAnexo(int Id)
		{

			try
			{
				PgeneralProyectoAplicacionAnexoRepositorio _repPgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoRepositorio();
				var listaPgeneralProyectoAplicacionAnexo = _repPgeneralProyectoAplicacionAnexo.ObtenerListaPgeneralProyectoAplicacionAnexo(Id);
				return Ok(listaPgeneralProyectoAplicacionAnexo);


			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 03/07/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina los datos de la tabla fin.T_PgeneralProyectoAplicacionAnexo
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[action]")]
		[HttpPost]
		public IActionResult GuardarArchivo(IFormFile file, string nombreArchivo)
		{
			try
			{
				nombreArchivo = file.FileName;
				PgeneralProyectoAplicacionAnexoRepositorio _repPgeneralProyectoAplicacionAnexo = new PgeneralProyectoAplicacionAnexoRepositorio();
				byte[] data;
				using (Stream inputStream = file.OpenReadStream())
				{
					MemoryStream memoryStream = inputStream as MemoryStream;
					if (memoryStream == null)
					{
						memoryStream = new MemoryStream();
						inputStream.CopyTo(memoryStream);
					}
					data = memoryStream.ToArray();
				}
				var url = _repPgeneralProyectoAplicacionAnexo.GuardarArchivo(data, "application/pdf", nombreArchivo);
				return Ok(url);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}

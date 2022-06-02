using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Api.Ads.Common.Util;
using CsvHelper;
using System.IO;
namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CongelamientoReporteFlujo")]
    public class CongelamientoReporteFlujoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext; 
        public CongelamientoReporteFlujoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarCongelamiento([FromBody] List<FlujoCongelamientoDTO> FlujoCongelamiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CongelamientoReporteFlujoRepositorio congelamientoReporteFlujoRepositorio = new CongelamientoReporteFlujoRepositorio();
                string resultado;
                var valor = congelamientoReporteFlujoRepositorio.GenerarCongelamientoReporte(FlujoCongelamiento);

                if (valor == false)
                {
                    resultado = "Se guardo correctamente ";
                }
                else
                {
                    resultado = "ERROR: No se pudo guardar";
                }

                return Ok(new { resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Fecha}/{Usuario}")]
        [HttpGet]
        /// Tipo Función: GET/
        /// Autor: Miguel Mora
        /// Fecha: 19/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos del reporte de flujo por dia en base a la fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDeFlujoPorDia(string Fecha, string Usuario)
        {
            try
            {

                CongelamientoReporteFlujoRepositorio congelamientoReporteFlujoRepositorio = new CongelamientoReporteFlujoRepositorio();

                var congelamientoReportePagosPorDia = congelamientoReporteFlujoRepositorio.CongelarReporteDeFlujoPorDia(Fecha, Usuario);

                return Ok(congelamientoReportePagosPorDia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Usuario}/{Periodo}")]
        [HttpGet]
        /// Tipo Función: GET/
        /// Autor: Miguel Mora
        /// Fecha: 19/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos del reporte de flujo por periodo en base a la fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDeFlujoPorPeriodo(string Usuario, int Periodo)
        {
            try
            {

                CongelamientoReporteFlujoRepositorio congelamientoReporteFlujoRepositorio = new CongelamientoReporteFlujoRepositorio();

                var congelamientoReportePagosPorPeriodo = congelamientoReporteFlujoRepositorio.CongelarReporteDeFlujoPorPeriodo(Usuario, Periodo);

                return Ok(congelamientoReportePagosPorPeriodo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET/
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 20/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de cronogramas originales  por dia
        /// </summary>
        /// <returns>Objeto</returns>
        [Route("[Action]/{Fecha}/{Usuario}")]
        [HttpGet]        
        public ActionResult CongelarReporteOriginalesPorDia(string Fecha,string Usuario)
        {
            try
            {

                CongelamientoReporteFlujoRepositorio congelamientoReporteFlujoRepositorio = new CongelamientoReporteFlujoRepositorio();

                var congelamientoReportePagosPorPeriodo = congelamientoReporteFlujoRepositorio.CongelarReporteOriginalesPorDia(Fecha,Usuario);

                return Ok(congelamientoReportePagosPorPeriodo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET/
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 20/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de cronogramas originales  por dia
        /// </summary>
        /// <returns>Objeto</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ListaCambiosPorPeriodo()
        {
            try
            {

                CongelamientoReporteFlujoRepositorio congelamientoReporteFlujoRepositorio = new CongelamientoReporteFlujoRepositorio();

                var congelamientoReportePagosPorPeriodo = congelamientoReporteFlujoRepositorio.ListaCambiosPorPeriodo();

                return Ok(congelamientoReportePagosPorPeriodo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET/
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 20/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de cronogramas originales  por dia
        /// </summary>
        /// <returns>Objeto</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult MostrarListaCambiosCSV([FromForm] IFormFile files)
        {
            var ListaReporteFlujo = new List<ListaCambiosPorPeriodoDTO>();
            CsvFile file = new CsvFile();
            try
            {
                int index = 0;
                using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream())))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Configuration.BadDataFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;

                        ListaCambiosPorPeriodoDTO flujoData = new ListaCambiosPorPeriodoDTO();
                        flujoData.CodigoMatricula = cvs.GetField<string>("CodigoMatricula");
                        flujoData.FechaVencimiento = cvs.GetField<DateTime>("FechaVencimientoCambio");
                        flujoData.MontoCambio = cvs.GetField<float>("MontoCambio");
                        flujoData.TipoModificacion = cvs.GetField<string>("TipoModificacion");
                        flujoData.Periodo = cvs.GetField<string>("PeriodoCambio");
                      
                        ListaReporteFlujo.Add(flujoData);
                    }
                }
                var Nregistros = index;
                return Ok(new { ListaReporteFlujo, Nregistros });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 20/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos de cronogramas originales  por dia
        /// </summary>
        /// <returns>Objeto</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCambiosPeriodo([FromBody] List<ListaCambiosCSVPorPeriodoDTO> Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var dataCadena = "";
                //using (TransactionScope scope = new TransactionScope())
                //{
                ImportarFlujoRepositorio flujoRepositorio = new ImportarFlujoRepositorio();
                foreach (var flujo in Json)
                {
                    dataCadena += flujo.CodigoMatricula + "|" +
                    flujo.FechaVencimiento + "|" +
                     flujo.MontoCambio + "|" +
                     flujo.TipoModificacion + "|" +
                     flujo.Periodo + "|" +
                     flujo.UsuarioCreacion + "|" +
                     flujo.UsuarioModificacion + "|" +
                     DateTime.Today + "|" +
                     DateTime.Today + "!";
                }
                //
                flujoRepositorio.InsertarCambiosPeriodo(dataCadena.Substring(0, dataCadena.Length - 1));

                //    scope.Complete();
                //}
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.IO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Transactions;
using BSI.Integra.Aplicacion.Finanzas.BO;
using System.Text;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ImportarFlujo")]
    public class ImportarFlujoController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public ActionResult MostrarFlujoReportes([FromForm] IFormFile files)
        {
            var ListaReporteFlujo = new List<ReporteFlujoSubidoDTO>();
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

                        ReporteFlujoSubidoDTO flujoData = new ReporteFlujoSubidoDTO();
                        flujoData.CodigoPrograma = cvs.GetField<string>("CodigoPrograma");
                        flujoData.EstadoPrograma = cvs.GetField<string>("EstadoPrograma");
                        flujoData.EstadoMatricula = cvs.GetField<string>("EstadoMatricula");
                        flujoData.Alumno = cvs.GetField<string>("Alumno");
                        flujoData.FechaVencimientoOriginal = cvs.GetField<string>("FechaVencimientoOriginal") == "" ? (DateTime?) null : cvs.GetField<DateTime>("FechaVencimientoOriginal");
                        flujoData.MontoCuotaOriginal = cvs.GetField<double>("MontoCuotaOriginal");
                        flujoData.MontoModificado = cvs.GetField<double>("MontoModificado");
                        flujoData.FechaVencimientoActual = cvs.GetField<string>("FechaVencimientoActual") == "" ? DateTime.Today : cvs.GetField<DateTime>("FechaVencimientoActual");
                        flujoData.MontoCuotaActual = cvs.GetField<double>("MontoCuotaActual");
                        flujoData.FechaPago = cvs.GetField<string>("FechaPago") == "" ? (DateTime?)null : cvs.GetField<DateTime>("FechaPago");
                        flujoData.MontoPagado = cvs.GetField<double>("MontoPagado");
                        flujoData.SaldoPendiente = cvs.GetField<double>("SaldoPendiente");
                        flujoData.Mora = cvs.GetField<double>("Mora");
                        flujoData.NroCuota = cvs.GetField<string>("NroCuota");
                        flujoData.Moneda = cvs.GetField<string>("Moneda");
                        flujoData.TotalCuotaDolar = cvs.GetField<double>("TotalCuotaDolar");
                        flujoData.RealPagoDolar = cvs.GetField<double>("RealPagoDolar");
                        flujoData.SaldoPendienteDolar = cvs.GetField<double>("SaldoPendienteDolar");
                        flujoData.OrigenPrograma = cvs.GetField<string>("OrigenPrograma");
                        flujoData.CCodigo = cvs.GetField<string>("CCodigo");
                        flujoData.CodigoMatricula = cvs.GetField<string>("CodigoMatricula");
                        flujoData.Email = cvs.GetField<string>("Email");
                        flujoData.TelFijo = cvs.GetField<string>("TelFijo");
                        flujoData.TelCel = cvs.GetField<string>("TelCel");
                        flujoData.Dni = cvs.GetField<string>("Dni");
                        flujoData.Direccion = cvs.GetField<string>("Direccion");
                        flujoData.Documento = cvs.GetField<string>("Documento");
                        flujoData.RazonSocial = cvs.GetField<string>("RazonSocial");
                        flujoData.CoordinadoraAcademica = cvs.GetField<string>("CoordinadoraAcademica");
                        flujoData.CoordinadoraCobranza = cvs.GetField<string>("CoordinadoraCobranza");
                        flujoData.Nuevo = cvs.GetField<string>("Nuevo");
                        flujoData.Factura = cvs.GetField<string>("Factura");
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
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarFlujoReportes([FromBody] List<ReporteFlujoSubidoDTO> Json)
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
                    dataCadena += flujo.CodigoPrograma + "|" +
                    flujo.EstadoPrograma + "|" +
                     flujo.EstadoMatricula + "|" +
                     flujo.Alumno + "|" +
                     flujo.FechaVencimientoOriginal + "|" +
                    flujo.MontoCuotaOriginal + "|" +
                    flujo.MontoModificado + "|" +
                    flujo.FechaVencimientoActual + "|" +
                    flujo.MontoCuotaActual + "|" +
                     flujo.FechaPago + "|" +
                    flujo.MontoPagado + "|" +
                    flujo.SaldoPendiente + "|" +
                    flujo.Mora + "|" +
                     flujo.NroCuota + "|" +
                     flujo.Moneda + "|" +
                    flujo.TotalCuotaDolar + "|" +
                    flujo.RealPagoDolar + "|" +
                    flujo.SaldoPendienteDolar + "|" +
                     flujo.CCodigo + "|" +
                     flujo.OrigenPrograma + "|" +
                     flujo.CodigoMatricula + "|" +
                     flujo.Email + "|" +
                     flujo.TelFijo + "|" +
                     flujo.TelCel + "|" +
                     flujo.Dni + "|" +
                     flujo.Direccion + "|" +
                     flujo.Documento + "|" +
                     flujo.RazonSocial + "|" +
                    flujo.CoordinadoraAcademica + "|" +
                     flujo.CoordinadoraCobranza + "|" +
                     flujo.Nuevo + "|" +
                     flujo.Factura + "|" +
                    1 + "|" +
                     flujo.UsuarioCreacion + "|" +
                     flujo.UsuarioModificacion + "|" +
                     DateTime.Today + "|" +
                     DateTime.Today + "!";



                        /*
                         dataCadena += "'" + flujo.CodigoPrograma + "'|" +
                    "'" + flujo.EstadoPrograma + "'|" +
                    "'" + flujo.EstadoMatricula + "'|" +
                    "'" + flujo.Alumno + "'|" +
                    "'" + flujo.FechaVencimientoOriginal + "'|" +
                    flujo.MontoCuotaOriginal + "|" +
                    flujo.MontoModificado + "|" +
                    "'" + flujo.FechaVencimientoActual + "'|" +
                    flujo.MontoCuotaActual + "|" +
                    "'" + flujo.FechaPago + "'|" +
                    flujo.MontoPagado + "|" +
                    flujo.SaldoPendiente + "|" +
                    flujo.Mora + "|" +
                    "'" + flujo.NroCuota + "'|" +
                    "'" + flujo.Moneda + "'|" +
                    flujo.TotalCuotaDolar + "|" +
                    flujo.RealPagoDolar + "|" +
                    flujo.SaldoPendienteDolar + "|" +
                    "'" + flujo.OrigenPrograma + "'|" +
                    "'" + flujo.CCodigo + "'|" +
                    "'" + flujo.CodigoMatricula + "'|" +
                    "'" + flujo.Email + "'|" +
                    "'" + flujo.TelFijo + "'|" +
                    "'" + flujo.TelCel + "'|" +
                    "'" + flujo.Dni + "'|" +
                    "'" + flujo.Direccion + "'|" +
                    "'" + flujo.Documento + "'|" +
                    "'" + flujo.RazonSocial + "'|" +
                    "'" + flujo.CoordinadoraAcademica + "'|" +
                    "'" + flujo.CoordinadoraCobranza + "'|" +
                    "'" + flujo.Nuevo + "'|" +
                    "'" + flujo.Factura + "'|" +
                    1 + "|" +
                    "'" + flujo.UsuarioCreacion + "'|" +
                    "'" + flujo.UsuarioModificacion + "'|" +
                    "'" + DateTime.Today + "'|" +
                    "'" + DateTime.Today + "'!";*/
                    }
                    //
                    flujoRepositorio.InsertarFlujoReporte(dataCadena.Substring(0, dataCadena.Length - 1));

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
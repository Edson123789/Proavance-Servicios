using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteControlDocumentos")]
    public class ReporteControlDocumentosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteControlDocumentosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoPagoMatricula()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EstadoPagoMatriculaRepositorio repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);   
                return Ok(repEstadoPagoMatricula.ObtenerEstadoPagoMatricula());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCoordinadorAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    return Ok(_repPersonal.ObtenerTipoPersonalCoordinador(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAsesorFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    return Ok(_repPersonal.ObtenerTodoPersonalAsesoresFiltroAutocomplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                    return Ok(_repCentroCosto.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerMatriculaFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    return Ok(_repMatriculaCabecera.ObtenerCodigoMatriculaAutocompleto(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 28/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Retorna los datos de los documentos por alumno
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteControlDocumentosFiltroDTO filtroControlDocumentos)
        {
            try
            {
                EstadoPagoMatriculaRepositorio repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                
                var reporteControlDocumentos = reportesRepositorio.ObtenerReporteControlDocumentos(filtroControlDocumentos);

                foreach (var item in reporteControlDocumentos)
                {
                    var documentos = item.Documentos;
                    var ArrayDocumentos = documentos.Split(';');
                    for (var x = 0; x < ArrayDocumentos.Count() - 1; x++)
                    {
                        var IdDocumento = ArrayDocumentos[x].Split(',')[1];
                        var NombreDocumento = ArrayDocumentos[x].Split(',')[2];
                        var estadoDoc = ArrayDocumentos[x].Split(',')[3];
                        if (IdDocumento.Equals("1"))
                        {
                            item.Cronograma = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("2"))
                        {
                            item.Convenio = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("3"))
                        {
                            item.Pagare = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("4"))
                        {
                            item.Carta_Autorizacion = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("5"))
                        {
                            item.Hoja_Requisitos = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("6"))
                        {
                            item.Orden_compra = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("7"))
                        {
                            item.Carta_compromiso = estadoDoc.Equals("1") ? 1 : 0;
                        }
                        if (IdDocumento.Equals("8"))
                        {
                            item.DNI = estadoDoc.Equals("1") ? 1 : 0;
                        }

                    }
                }

                return Ok(reporteControlDocumentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDocumentos([FromBody] ReporteDocumentosFiltroDTO FiltroControlDocumentos)
        {
            try
            {
                EstadoPagoMatriculaRepositorio repEstadoPagoMatricula = new EstadoPagoMatriculaRepositorio(_integraDBContext);
                InteraccionChatMessengerRepositorio _repInteraccionChatMessenger = new InteraccionChatMessengerRepositorio(_integraDBContext);
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado = null;
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado2 = null;
                IEnumerable<ReporteDocuemntosAgrupadoDTO> agrupado3 = null;

                var reporteControlDocumentos = reportesRepositorio.ObtenerReporteDocumentos(FiltroControlDocumentos).OrderByDescending(x => x.FechaCierre);

                if (FiltroControlDocumentos.Desglose == 1)
                {
                    agrupado = reporteControlDocumentos.GroupBy(x => _repInteraccionChatMessenger.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });

                    agrupado2 = reporteControlDocumentos.GroupBy(x => _repInteraccionChatMessenger.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });

                    agrupado3 = reporteControlDocumentos.GroupBy(x => _repInteraccionChatMessenger.ObtenerNumeroSemana(x.FechaCierre))
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = "Semana_" + g.Key,
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });
                }
                else if (FiltroControlDocumentos.Desglose == 2)
                {
                    agrupado = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _repInteraccionChatMessenger.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });

                    agrupado2 = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _repInteraccionChatMessenger.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            NombrePersonalAsesor = y.NombrePersonalAsesor,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });

                    agrupado3 = reporteControlDocumentos.GroupBy(x => x.FechaCierre.Month)
                    .Select(g => new ReporteDocuemntosAgrupadoDTO
                    {
                        Fecha = _repInteraccionChatMessenger.ObtenerNombreMes(g.Key),
                        DetalleFecha = g.Select(y => new ReporteDocumentosVistaDTO
                        {
                            Coordinador = y.NombrePersonalCoordinador,
                            NumeroIS = y.NumeroIS,
                            ContratoVoz = y.ContratoVoz,
                            ContratoFirmado = y.ContratoFirmado,
                            Empresa = y.Empresa,
                            SinDocumentacion = y.SinDocumentacion,
                            Convenio = y.Convenio,
                            SinDocumentacionP = y.SinDocumentacionP

                        }).ToList()
                    });
                }

                ReporteDocumentosCompuestoDTO reporte = new ReporteDocumentosCompuestoDTO();
                reporte.ReporteDocumentosEquipo = agrupado.ToList();
                reporte.ReporteDocumentosAsesor = agrupado2.ToList();
                reporte.ReporteDocumentosCoordinador = agrupado3.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
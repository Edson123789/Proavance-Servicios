using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    // Controlador: ReporteSeguimientoOportunidades
    /// Autor: Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de reportes de los distintos seguimientos de oportunidades
    /// </summary>
    [Route("api/ReporteSeguimientoOportunidades")]
    [ApiController]
    public class ReporteSeguimientoOportunidadesController : ControllerBase
    {
        /// TipoFuncion: GET
        /// Autor: Gian Miranda
        /// Fecha: 28/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos del reporte se seguimiento
        /// </summary>
        /// <param name="IdPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de clase ReporteSeguimientoOportunidadCombosDTO</returns>
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteSeguimiento(int IdPersonal)
        {
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                FaseOportunidadRepositorio repFaseOportunidad = new FaseOportunidadRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                ReporteSeguimientoOportunidadCombosDTO result = new ReporteSeguimientoOportunidadCombosDTO();
                result.CentroCostos = repCentroCosto.ObtenerCentroCostoParaFiltro();
                result.FaseOportunidades = repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro();
                result.Asesores = IdPersonal == 213 ? repPersonal.ObtenerAsesoresVentasOficialReporteSeguimiento() : repPersonal.GetPersonalAsignadoVentas(IdPersonal);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 23/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos del modulo de habilitar discador
        /// </summary>
        /// <param name="IdPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de clase DiscadorPersonalDTO</returns>
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPersonalDiscador(int IdPersonal)
        {
            try
            {
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                ReporteSeguimientoOportunidadCombosDTO result = new ReporteSeguimientoOportunidadCombosDTO();
                result.Asesores = IdPersonal == 213 ? repPersonal.ObtenerAsesoresVentasOficialReporteSeguimiento() : repPersonal.GetPersonalAsignadoVentas(IdPersonal);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosReporteSeguimientoOperaciones(int IdPersonal)
        {
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                FaseOportunidadRepositorio repFaseOportunidad = new FaseOportunidadRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio();


                ReporteSeguimientoOportunidadCombosDTO result = new ReporteSeguimientoOportunidadCombosDTO();
                result.CentroCostos = repCentroCosto.ObtenerCentroCostoParaFiltro();
                result.FaseOportunidades = repFaseOportunidad.ObtenerFaseOportunidadTodoFiltro();
                result.Asesores = repPersonal.GetPersonalAsignadoOperaciones(IdPersonal);
                result.Estados = repMatriculaCabecera.ObtenerEstadosMatricula();


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// TipoFuncion: POST
        /// Autor: Gian Miranda
        /// Fecha: 28/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los centros de costos por el personal
        /// </summary>
        /// <param name="IdsAsesor">Id de los asesores (PK de la tabla gp.T_Personal)</param>
        /// <returns>Reponse 200 con la lista de objetos de clase FiltroDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPersonal([FromBody]ListadoIdDTO IdsAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio repCentroCosto = new CentroCostoRepositorio();
                string asesores = string.Join(",", IdsAsesor.Ids);
                return Ok(repCentroCosto.ObtenerCentroCostoPorAsesores(IdsAsesor.Ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesDTO> result = new List<ReporteSeguimientoOportunidadesDTO>();
                result = reporte.ObtenerReporteSeguimientoOportunidad(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteSolicitudesVisualizacion([FromBody] ReporteSolicitudesVisualizacionFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesDTO> result = new List<ReporteSeguimientoOportunidadesDTO>();
                result = reporte.ObtenerReporteSolicitudesVisualizacion(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteSolicitudesVisualizacionOperaciones([FromBody] ReporteSolicitudesVisualizacionFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesDTO> result = new List<ReporteSeguimientoOportunidadesDTO>();
                result = reporte.ObtenerReporteSolicitudesVisualizacionOperaciones(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarSolicitudVisualizacion([FromBody] AprobacionSolicitudesVisualizacionFiltrosDTO aprobacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                var result = reporte.AprobacionSolicitudVisualizacion(aprobacion);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteOperaciones([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesOperacionesDTO> result = new List<ReporteSeguimientoOportunidadesOperacionesDTO>();
                result = reporte.ObtenerReporteSeguimientoOportunidadOperaciones(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteProbabilidad([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesModeloDTO> result = new List<ReporteSeguimientoOportunidadesModeloDTO>();
                result = reporte.ObtenerReporteSeguimientoOportunidadProbabilidad(Filtros);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de fecha de creacion de registro tanto para creacion de oportunidad o de la campania
        /// </summary>
        /// <returns>ActionResult con estado 200 con lista de objetos de clase ReporteSeguimientoOportunidadesDTO, caso contrario 400</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteFechaCreacionRegistro([FromBody] ReporteSeguimientoOportunidadesFiltrosDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Reportes reporte = new Reportes();
                List<ReporteSeguimientoOportunidadesDTO> result = new List<ReporteSeguimientoOportunidadesDTO>();
                if (Filtros.TipoFecha == 1)//fecha creacion
                {
                    result = reporte.ObtenerReporteSeguimientoOportunidadFC(Filtros);
                }
                if (Filtros.TipoFecha == 2)//fecha registro campania
                {
                    result = reporte.ObtenerReporteSeguimientoOportunidadFRC(Filtros);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista de log de la oportunidad
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerListaOportunidadLog(int IdOportunidad)
        {
            try
            {
                Reportes reporte = new Reportes();
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                BloqueHorarioRepositorio bloqueHorarioRepositorio = new BloqueHorarioRepositorio();

                var fechas = reportesRepositorio.ObtenerActividadesNoEjecutadas(IdOportunidad);

                List<BloqueHorarioProcesarBicDTO> bloques = bloqueHorarioRepositorio.GetBy(x => true, y => new BloqueHorarioProcesarBicDTO
                {
                    Nombre = y.Nombre,
                    HoraInicio = y.HoraInicio,
                    HoraFin = y.HoraFin
                }).ToList();

                foreach (var bloque in bloques)
                {
                    bloque.Contador = 0;
                }

                var nombreTurnoUltimo = string.Empty;
                DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                foreach (var fecha in fechas)
                {
                    TimeSpan horaFecha = fecha.TimeOfDay;

                    foreach (var bloque in bloques)
                    {
                        if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                        {
                            if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                            else
                            {
                                nombreTurnoUltimo = bloque.Nombre;
                                fechaUltima = fecha.Date;
                                bloque.Contador++;
                                break;
                            }
                        }
                    }
                }
                return Ok(new { log = reporte.ObtenerOportunidadesLog(IdOportunidad), bloques = bloques });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerListaOportunidadLogRichard(int IdOportunidad)
        {
            try
            {
                Reportes reporte = new Reportes();
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                BloqueHorarioRepositorio bloqueHorarioRepositorio = new BloqueHorarioRepositorio();

                var fechas = reportesRepositorio.ObtenerActividadesNoEjecutadas(IdOportunidad);

                List<BloqueHorarioProcesarBicDTO> bloques = bloqueHorarioRepositorio.GetBy(x => true, y => new BloqueHorarioProcesarBicDTO
                {
                    Nombre = y.Nombre,
                    HoraInicio = y.HoraInicio,
                    HoraFin = y.HoraFin
                }).ToList();

                foreach (var bloque in bloques)
                {
                    bloque.Contador = 0;
                }

                var nombreTurnoUltimo = "";
                DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                foreach (var fecha in fechas)
                {
                    TimeSpan horaFecha = fecha.TimeOfDay;

                    foreach (var bloque in bloques)
                    {
                        if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                        {
                            if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                            else
                            {
                                nombreTurnoUltimo = bloque.Nombre;
                                fechaUltima = fecha.Date;
                                bloque.Contador++;
                                break;
                            }
                        }
                    }
                }
                return Ok(new { log = reporte.ObtenerOportunidadesLogRichard(IdOportunidad), bloques = bloques });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdOportunidad}/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInformacionOportunidad(int IdOportunidad, int IdAlumno)
        {
            try
            {
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio();
                ProgramaGeneralBeneficioArgumentoRepositorio programaGeneralBeneficioArgumentoRepositorio = new ProgramaGeneralBeneficioArgumentoRepositorio();
                Aplicacion.Transversal.BO.OportunidadInformacionBO oportunidadInformacionBO = new Aplicacion.Transversal.BO.OportunidadInformacionBO();
                AlumnoRepositorio alumnoRepositorio = new AlumnoRepositorio();
                SentinelBO Sentinel = new SentinelBO();
                ReporteOportunidadDetalleDTO reporteOpotunidadDetalleDTO = new ReporteOportunidadDetalleDTO();

                reporteOpotunidadDetalleDTO.listaOportunidadVentaCruzada = oportunidadRepositorio.ObtenerHistorialOportunidadesReporte(IdAlumno); // jrivera
                var idClasificacionPersona = oportunidadRepositorio.FirstById(IdOportunidad).IdClasificacionPersona.Value;
                reporteOpotunidadDetalleDTO.datosAlumno = alumnoRepositorio.ObtenerDatosAlumno(idClasificacionPersona);

                //ENCRIPTAR EMIAL Y NRO
                reporteOpotunidadDetalleDTO.datosAlumno.Email1 = EncriptarStringCorreo(reporteOpotunidadDetalleDTO.datosAlumno.Email1);
                reporteOpotunidadDetalleDTO.datosAlumno.Email2 = EncriptarStringCorreo(reporteOpotunidadDetalleDTO.datosAlumno.Email2);
                reporteOpotunidadDetalleDTO.datosAlumno.Telefono = EncriptarStringNumero(reporteOpotunidadDetalleDTO.datosAlumno.Telefono);
                reporteOpotunidadDetalleDTO.datosAlumno.Telefono2 = EncriptarStringNumero(reporteOpotunidadDetalleDTO.datosAlumno.Telefono2);
                reporteOpotunidadDetalleDTO.datosAlumno.Celular = EncriptarStringNumero(reporteOpotunidadDetalleDTO.datosAlumno.Celular);
                reporteOpotunidadDetalleDTO.datosAlumno.Celular2 = EncriptarStringNumero(reporteOpotunidadDetalleDTO.datosAlumno.Celular2);
                //FIN ENCRIPTAR EMIAL Y NRO


                if (reporteOpotunidadDetalleDTO.datosAlumno.IdCargo == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdCargo = 11;
                if (reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria = 48;

                if (reporteOpotunidadDetalleDTO.datosAlumno.DNI == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.DNI = "";

                reporteOpotunidadDetalleDTO.probabilidadsueldo = Sentinel.GetPromedioSueldo(reporteOpotunidadDetalleDTO.datosAlumno.IdEmpresa, reporteOpotunidadDetalleDTO.datosAlumno.DNI, reporteOpotunidadDetalleDTO.datosAlumno.IdCargo, reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria);

                oportunidadInformacionBO.CargarPrerequisitosBeneficios(IdOportunidad);
                reporteOpotunidadDetalleDTO.ProgramaGeneralPreBen = oportunidadInformacionBO.ProgramaGeneralPreBen;

                reporteOpotunidadDetalleDTO.ListaProblemaCliente = oportunidadRepositorio.ObtenerOportunidadProblemasCliente(IdOportunidad);
                reporteOpotunidadDetalleDTO.OportunidadComplementos = oportunidadRepositorio.ObtenerInformacionComplementariaReporteSeguimiento(IdOportunidad);

                return Ok(reporteOpotunidadDetalleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]/{IdOportunidad}/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInformacionOportunidadOperaciones(int IdOportunidad, int IdAlumno)
        {
            try
            {
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio();
                ProgramaGeneralBeneficioArgumentoRepositorio programaGeneralBeneficioArgumentoRepositorio = new ProgramaGeneralBeneficioArgumentoRepositorio();
                Aplicacion.Transversal.BO.OportunidadInformacionBO oportunidadInformacionBO = new Aplicacion.Transversal.BO.OportunidadInformacionBO();
                AlumnoRepositorio alumnoRepositorio = new AlumnoRepositorio();
                SentinelBO Sentinel = new SentinelBO();
                ReporteOportunidadDetalleDTO reporteOpotunidadDetalleDTO = new ReporteOportunidadDetalleDTO();

                reporteOpotunidadDetalleDTO.listaOportunidadVentaCruzada = oportunidadRepositorio.ObtenerHistorialOportunidadesReporte(IdAlumno); // jrivera
                var idClasificacionPersona = oportunidadRepositorio.FirstById(IdOportunidad).IdClasificacionPersona.Value;
                reporteOpotunidadDetalleDTO.datosAlumno = alumnoRepositorio.ObtenerDatosAlumno(idClasificacionPersona);
                if (reporteOpotunidadDetalleDTO.datosAlumno.IdCargo == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdCargo = 11;
                if (reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria = 48;

                if (reporteOpotunidadDetalleDTO.datosAlumno.DNI == null)
                    reporteOpotunidadDetalleDTO.datosAlumno.DNI = "";

                reporteOpotunidadDetalleDTO.probabilidadsueldo = Sentinel.GetPromedioSueldo(reporteOpotunidadDetalleDTO.datosAlumno.IdEmpresa, reporteOpotunidadDetalleDTO.datosAlumno.DNI, reporteOpotunidadDetalleDTO.datosAlumno.IdCargo, reporteOpotunidadDetalleDTO.datosAlumno.IdIndustria);

                oportunidadInformacionBO.CargarPrerequisitosBeneficios(IdOportunidad);
                reporteOpotunidadDetalleDTO.ProgramaGeneralPreBen = oportunidadInformacionBO.ProgramaGeneralPreBen;

                reporteOpotunidadDetalleDTO.ListaProblemaCliente = oportunidadRepositorio.ObtenerOportunidadProblemasCliente(IdOportunidad);
                reporteOpotunidadDetalleDTO.OportunidadComplementos = oportunidadRepositorio.ObtenerInformacionComplementariaReporteSeguimiento(IdOportunidad);

                return Ok(reporteOpotunidadDetalleDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerHistorialInteraccionesOportunidad(int IdOportunidad)
        {
            try
            {
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio();
                return Ok(oportunidadRepositorio.CargarHistorialInteraccionesOportunidad(IdOportunidad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerReferidos(int IdAlumno)
        {
            try
            {
                AlumnoRepositorio alumnoRepositorio = new AlumnoRepositorio();
                return Ok(alumnoRepositorio.ObtenerReferidos(IdAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesAnteriores(int IdAlumno)
        {
            try
            {
                OportunidadRepositorio oportunidadRepositorio = new OportunidadRepositorio();
                return Ok(oportunidadRepositorio.ObtenerOportunidadesAnterioresAlumno(IdAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]/{IdAlumno}")]
        [HttpGet]
        public ActionResult ObtenerInteraccionesAlumno(int IdAlumno)
        {
            try
            {
                InteraccionPaginaRepositorio interaccionPaginaRepositorio = new InteraccionPaginaRepositorio();
                return Ok(interaccionPaginaRepositorio.ObtenerInteraccionesPorAlumno(IdAlumno));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CargarDiscadorPersonal([FromBody] DiscadorPersonalFiltroDTO Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                try
                {
                    PersonalRepositorio personalRepositorio = new PersonalRepositorio();
                    string _asesores = ListIntToString(Filtros.asesores);
                    //General
                    var RespuestaPersonalDiscador = personalRepositorio.ObtenerDiscadorPersonal(_asesores);
                    return Ok(new { Records = RespuestaPersonalDiscador });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 15/02/2022
        /// Versión: 1.0
        /// <summary>
        /// Modifica la url de la llamada
        /// </summary>
        /// <returns>ActionResult con estado 200, 400 y cantidad de contactos resultantes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult SubirArchivo([FromForm] IList<IFormFile> Archivos, [FromForm] int IdLlamada, [FromForm] string NombreArchivo, [FromForm] string NombreUsuario, [FromForm] int DuracionContesto, [FromForm] int NroBytes)
        {
            try
            {
                GestionArchivoLlamadaBO gestionArchivoBo = new GestionArchivoLlamadaBO();
                string varUrl = string.Empty;
                string RutaCompleta = "https://repositorioaudiollamada.blob.core.windows.net/asterisk/2022/Regularizacion/";
                string RutaBlob = "asterisk/2022/Regularizacion/";
                foreach (var archivo in Archivos)
                {
                    varUrl = gestionArchivoBo.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, NombreArchivo, RutaCompleta.Replace("V4/", string.Empty), RutaBlob.Replace("V4/", string.Empty));
                }
                LlamadaWebphoneAsteriskRepositorio _repLlamada = new LlamadaWebphoneAsteriskRepositorio();
                //LlamadaWebphoneModificadoDTO webphoneDTO = new LlamadaWebphoneModificadoDTO();
                //webphoneDTO.IdLlamada = IdLlamada;
                //webphoneDTO.NombreArchivo = NombreArchivo;
                //webphoneDTO.NombreUsuario = NombreUsuario;
                //webphoneDTO.DuracionContesto = DuracionContesto;
                //webphoneDTO.NroBytes = NroBytes;
                _repLlamada.ModificarLlamadaWebphone(IdLlamada, varUrl, NombreUsuario, DuracionContesto, NroBytes);

                return Ok(varUrl);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Jashin Salazar
        /// Fecha: 11/03/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el area del personal que esta ingresando al reporte
        /// </summary>
        /// <param name="IdPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto con  area de trabajo</returns>
        [Route("[Action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerAreaTrabajo(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio();
                var personal = _repPersonal.FirstById(IdPersonal);
                return Ok(new { Area = personal.AreaAbrev });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //carlos comentado
        //[Route("[action]/{parametro}")]
        //[HttpGet]
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");
                
                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }
        //carlos comentado
        //[Route("[action]/{parametro}")]
        //[HttpGet]
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    //respuesta = parametro.Remove(0, posicion) + new string('x', 4);
                    respuesta = parametro.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }
        private string ListIntToString(IList<int> datos)
        {
            if (datos == null)
                datos = new List<int>();
            int NumberElements = datos.Count;
            string rptaCadena = string.Empty;
            for (int i = 0; i < NumberElements - 1; i++)
                rptaCadena += Convert.ToString(datos[i]) + ",";
            if (NumberElements > 0)
                rptaCadena += Convert.ToString(datos[NumberElements - 1]);
            return rptaCadena;
        }
    }
}

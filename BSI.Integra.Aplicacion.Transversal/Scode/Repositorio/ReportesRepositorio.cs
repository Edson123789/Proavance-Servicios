using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class ReportesRepositorio
    {
        private DapperRepository _dapper;
        public ReportesRepositorio()
        {
            _dapper = new DapperRepository();
        }

        public List<ReportePendientePeriodoDTO> ObtenerReportePendiente(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);

                List<ReportePendientePeriodoDTO> items = new List<ReportePendientePeriodoDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoDTO>>(query);
                }

                return items;
            }
           catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientesCambiosDTO> ObtenerReportePendienteCambios(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {
                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);

                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientesCambiosDTO> items = new List<ReportePendientesCambiosDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesCambios]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesCambiosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientesDiferenciasDTO> ObtenerReportePendienteDiferencias(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientesDiferenciasDTO> items = new List<ReportePendientesDiferenciasDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesDiferencias]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesDiferenciasDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public List<ReportePendientePeriodoPorCoordinadorDTO> ObtenerReportePendientePorCoordinador(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientePeriodoPorCoordinadorDTO> items = new List<ReportePendientePeriodoPorCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoPorCoordinador]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoPorCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientesCambiosPorCoordinadorDTO> ObtenerReportePendienteCambiosPorCoordinador(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);

                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientesCambiosPorCoordinadorDTO> items = new List<ReportePendientesCambiosPorCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesCambiosPorCoordinador]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesCambiosPorCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinador(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReportePendienteDetalles> ObtenerReportePendienteDetalles(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {

                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (FiltroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in FiltroPendiente.Modalidad)
                    {
                        Modalidad += item + " ";
                    }
                    Modalidad = Modalidad.Trim();
                    Modalidad = Modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendienteDetalles> items = new List<ReportePendienteDetalles>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinadorDetalles]", new { fechainicio, fechafin, tipos = Modalidad, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendienteDetalles>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteResumenMontosPagosDTO> ObtenerReporteResumenMontos(ReporteResumenMontosFiltroGeneralDTO FiltroPendiente)
        {
            /*prueba para obtener montos general*/
            try
            {
                var FechaInicioPeriodo = FiltroPendiente.FechaInicio;
                var FechaFinalPeriodo = FiltroPendiente.FechaFin;
                var IdPeriodo = FiltroPendiente.PeriodoActual;
                DateTime fechainicio = new DateTime(FechaInicioPeriodo.Year, FechaInicioPeriodo.Month, FechaInicioPeriodo.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFinalPeriodo.Year, FechaFinalPeriodo.Month, FechaFinalPeriodo.Day, 23, 59, 59);
                Console.WriteLine(fechainicio +" "+ fechafin);
                List<ReporteResumenMontosPagosDTO> items = new List<ReporteResumenMontosPagosDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteResumenMontos]", new { fechainicio, fechafin, IdPeriodo});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosPagosDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public List<ReporteResumenMontosCierreDTO> ObtenerReporteResumenMontosCierre(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {

                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                var IdPeriodo = FiltroPendiente.PeriodoCierre;
                var FechaFinalPeriodo = repPeriodo.ObtenerFechaFinal(FiltroPendiente.PeriodoCierre.Value);
                var FechaFinal = FechaFinalPeriodo.AddDays(1);
                DateTime FechaFin = new DateTime(FechaFinal.Year, FechaFinal.Month, FechaFinal.Day, 0, 0, 0);
                List<ReporteResumenMontosCierreDTO> items = new List<ReporteResumenMontosCierreDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteResumenMontos_Cierre]", new { IdPeriodo, FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosCierreDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteResumenMontosCambiosPorPaisesDTO> ObtenerReporteResumenMontosCambiosPorPais(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var idPeriodo = FiltroPendiente.PeriodoActual;
                
                List<ReporteResumenMontosCambiosPorPaisesDTO> items = new List<ReporteResumenMontosCambiosPorPaisesDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteResumenMontosCambios]", new { IdPeriodo =idPeriodo });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosCambiosPorPaisesDTO>>(query);
                }

                return items;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteResumenMontosDiferenciasPorPaisesDTO> ObtenerReporteResumenMontosDiferencias(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                var FechaInicioPeriodo = repPeriodo.ObtenerFechaInicial(FiltroPendiente.PeriodoActual);
                var FechaFinalPeriodo = repPeriodo.ObtenerFechaFinal(FiltroPendiente.PeriodoActual);
                var IdPeriodo = FiltroPendiente.PeriodoActual;
                DateTime fechainicio = new DateTime(FechaInicioPeriodo.Year, FechaInicioPeriodo.Month, FechaInicioPeriodo.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFinalPeriodo.Year, FechaFinalPeriodo.Month, FechaFinalPeriodo.Day, 23, 59, 59);

                List<ReporteResumenMontosDiferenciasPorPaisesDTO> items = new List<ReporteResumenMontosDiferenciasPorPaisesDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteResumenMontosDiferencias]", new { fechainicio, fechafin, IdPeriodo });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosDiferenciasPorPaisesDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteResumenMontosVariacionesDTO> ObtenerReporteResumenMontosVariaciones(ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {

                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                var FechaInicioPeriodo = repPeriodo.ObtenerFechaInicial(FiltroPendiente.PeriodoActual);
                var FechaFinalPeriodo = repPeriodo.ObtenerFechaFinal(FiltroPendiente.PeriodoActual);

                DateTime fechainicio = new DateTime(FechaInicioPeriodo.Year, FechaInicioPeriodo.Month, FechaInicioPeriodo.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFinalPeriodo.Year, FechaFinalPeriodo.Month, FechaFinalPeriodo.Day, 23, 59, 59);


                List<ReporteResumenMontosVariacionesDTO> items = new List<ReporteResumenMontosVariacionesDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteResumenMontos_Variaciones]", new { fechainicio, fechafin});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteResumenMontosVariacionesDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public List<ReporteProductividadVentasHorasTrabajadasDTO> ObtenerReporteProductividadVentasHorasTrabajadas(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {               
                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReporteProductividadVentasHorasTrabajadasDTO> items = new List<ReporteProductividadVentasHorasTrabajadasDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteProductividadVentas_HorasTrabajadas]", new { fechainicio, fechafin});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProductividadVentasHorasTrabajadasDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Genera el reporte de seguimiento de comisiones [ejecuta el store procedure: [fin].[SP_EstraerRegistrosSeguimientoComisiones]
        /// </summary>
        /// <param name="IdAsesores"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisiones(string IdAsesores, string FechaInicio, string FechaFin, int IdEstado)
        {
            try
            {
                List<ReporteSeguimientoComisionesDTO> seguimiento = new List<ReporteSeguimientoComisionesDTO>();
                var _query = string.Empty;
               
                _query = "exec [fin].[SP_EstraerRegistrosSeguimientoComisiones] '" + IdAsesores +"', "  +  "'"+FechaInicio+"', "  +  "'"+FechaFin+"', "  +  IdEstado;
                var seguimientoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(seguimientoDB) && !seguimientoDB.Contains("[]"))
                {
                    seguimiento = JsonConvert.DeserializeObject<List<ReporteSeguimientoComisionesDTO>>(seguimientoDB);
                }
                return seguimiento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera el reporte de seguimiento de comisiones [ejecuta el store procedure: [fin].[SP_ExtraerVentasComisionables]
        /// </summary>
        /// <param name="IdAsesores"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="IdEstado"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisionables(string IdAsesores, string FechaInicio, string FechaFin, int IdEstado)
        {
            try
            {
                List<ReporteSeguimientoComisionesDTO> seguimiento = new List<ReporteSeguimientoComisionesDTO>();
                var _query = string.Empty;

                _query = "exec [fin].[SP_ExtraerVentasComisionables] '" + IdAsesores + "', " + "'" + FechaInicio + "', " + "'" + FechaFin + "', " + IdEstado;
                var seguimientoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(seguimientoDB) && !seguimientoDB.Contains("[]"))
                {
                    seguimiento = JsonConvert.DeserializeObject<List<ReporteSeguimientoComisionesDTO>>(seguimientoDB);
                }
                return seguimiento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera el reporte de seguimiento de comisiones [ejecuta el store procedure: [fin].[SP_GenerarReporteComisionPersonal]
        /// </summary>
        /// <param name="IdAsesores"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <returns></returns>
        public List<ReporteComisionesDTO> ObtenerReporteComisiones(string IdAsesores, string FechaInicio, string FechaFin, int IdSubEstado)
        {
            try
            {
                List<ReporteComisionesDTO> comisiones = new List<ReporteComisionesDTO>();
                var _query = string.Empty;

                _query = "exec [fin].[SP_GenerarReporteComisionPersonal] '" + IdAsesores + "', " + "'" + FechaInicio + "', " + "'" + FechaFin + "' "+", "+ IdSubEstado;
                var comisionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(comisionesDB) && !comisionesDB.Contains("[]"))
                {
                    comisiones = JsonConvert.DeserializeObject<List<ReporteComisionesDTO>>(comisionesDB);
                }
                return comisiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Actualiza el estao de las comisiones a pagado
        /// </summary>
        /// <returns>Arreglo de ReporteComisionesDTO</returns>
        public List<ReporteComisionesDTO> ActualizarReporteComisiones(string FechaInicio, string FechaFin)
        {
            try
            {
                List<ReporteComisionesDTO> comisiones = new List<ReporteComisionesDTO>();
                var _query = string.Empty;

                _query = "exec [fin].[SP_ActualizarReporteComisionPersonal]" + "'" + FechaInicio + "', " + "'" + FechaFin + "' " ;
                var comisionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(comisionesDB) && !comisionesDB.Contains("[]"))
                {
                    comisiones = JsonConvert.DeserializeObject<List<ReporteComisionesDTO>>(comisionesDB);
                }
                return comisiones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de estados validos para el reporte de seguimiento de comisiones
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaSubEstadosParaSeguimientoComisiones()
        {
            try
            {
                List<FiltroDTO> estados = new List<FiltroDTO>();
                var _query = string.Empty;

                _query = "SELECT * FROM [fin].[V_SubEstadoSeguimientoComision]";
                var estadosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(estadosDB) && !estadosDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<List<FiltroDTO>>(estadosDB);
                }
                return estados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de las comisiones disponibles
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaPorcentajesComision()
        {
            try
            {
                List<FiltroDTO> comision = new List<FiltroDTO>();
                var _query = string.Empty;

                _query = "SELECT * FROM [fin].[V_PorcentajesComision]";
                var comisionDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(comisionDB) && !comisionDB.Contains("[]"))
                {
                    comision = JsonConvert.DeserializeObject<List<FiltroDTO>>(comisionDB);
                }
                return comision;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReporteProductividadVentasIndicadoresDTO> ObtenerReporteProductividadVentasIndicadores(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {
                DateTime fechainicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReporteProductividadVentasIndicadoresDTO> items = new List<ReporteProductividadVentasIndicadoresDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteProductividadVentas_Indicadores]", new { fechainicio, fechafin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProductividadVentasIndicadoresDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista por dias del periodo seleccionado para saber los pagos realizados
        /// </summary>
        /// <returns></returns>
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosDia(ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo)
        {
            try
            {
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                string Modalidad = null, Coordinadora = null;
                if (FiltroReportePagosDiaPeriodo.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroReportePagosDiaPeriodo.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }
                var FechaInicio = repPeriodo.ObtenerFechaInicial(FiltroReportePagosDiaPeriodo.Periodo);
                var FechaFin = repPeriodo.ObtenerFechaFinal(FiltroReportePagosDiaPeriodo.Periodo);
                DateTime fechainicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePagosDiaPeriodoDTO> items = new List<ReportePagosDiaPeriodoDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePagosDiaPeriodo]", new { fechainicio, fechafin,coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDiaPeriodoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista por 12 meses antes del periodo seleccionado para saber los pagos realizados
        /// </summary>
        /// <returns></returns>
        public List<ReportePagosDiaPeriodoDTO> ObtenerReportePagosPeriodo(ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo)
        {
            try
            {
               PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                string Modalidad = null, Coordinadora = null;
                if (FiltroReportePagosDiaPeriodo.Coordinadora.Count() > 0)
                {
                    foreach (var item in FiltroReportePagosDiaPeriodo.Coordinadora)
                    {
                        Coordinadora += item + " ";
                    }
                    Coordinadora = Coordinadora.Trim();
                    Coordinadora = Coordinadora.Replace(" ", ",");
                }
                var FechaInicio = repPeriodo.ObtenerFechaInicial(FiltroReportePagosDiaPeriodo.Periodo);
                var FechaFin = repPeriodo.ObtenerFechaFinal(FiltroReportePagosDiaPeriodo.Periodo);
                FechaInicio = FechaInicio.AddMonths(-12);
                DateTime fechainicio = new DateTime(FechaInicio.Year, FechaInicio.Month, FechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(FechaFin.Year, FechaFin.Month, FechaFin.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePagosDiaPeriodoDTO> items = new List<ReportePagosDiaPeriodoDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePagosDiaPeriodo_Periodo]", new { fechainicio, fechafin, coordinadoras = Coordinadora });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDiaPeriodoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 03/02/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por periodo 
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 0, 0, 0);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora,fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por periodo 
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 0, 0, 0);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por periodo obtiene los matriculados por fecha vencimineto y fecha de pago
        /// </summary>
        /// <returns>Lista de tipo ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 0, 0, 0);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Periodo_Cierre_Matriculados]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene los cambios para el reporte de pendientes por periodo 
        /// </summary>
        /// <returns>ReportePendientesCambiosPorCoordinadorDTO</returns>
        public List<ReportePendientesCambiosPorCoordinadorDTO> ObtenerReportePendienteCambiosPorCoordinadorPorPeriodo(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {

                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 23, 59, 59);

                List<ReportePendientesCambiosPorCoordinadorDTO> items = new List<ReportePendientesCambiosPorCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesCambiosPorCoordinador_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre});

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesCambiosPorCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene las diferencias para el reporte de pendientes por periodo 
        /// </summary>
        /// <returns>ReportePendientesDiferenciasDTO</returns>
        public List<ReportePendientesDiferenciasDTO> ObtenerReportePendienteDiferenciasPorPeriodo(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 23, 59, 59);

                List<ReportePendientesDiferenciasDTO> items = new List<ReportePendientesDiferenciasDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesDiferencias_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesDiferenciasDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// REPORTE PENDIENTES PERIODO
        /// Obtiene el reporte de pendientes cierre por periodo 
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodoOriginales(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierrePrevio = new DateTime(filtroPendiente.FechaCortePrevio.Year, filtroPendiente.FechaCortePrevio.Month, filtroPendiente.FechaCortePrevio.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 23, 59, 59);
                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo_Cierre_Comparar_Originales]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre, fechaCierrePrevio });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 25/01/2021
        /// <summary>
        /// REPORTE PENDIENTES PERIODO
        /// Obtiene el reporte de pendientes cierre por periodo 
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodo(ReportePendientePeriodoFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                DateTime fechainicio = new DateTime(filtroPendiente.FechaInicial.Year, filtroPendiente.FechaInicial.Month, filtroPendiente.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroPendiente.FechaFin.Year, filtroPendiente.FechaFin.Month, filtroPendiente.FechaFin.Day, 23, 59, 59);
                DateTime fechaCierrePrevio = new DateTime(filtroPendiente.FechaCortePrevio.Year, filtroPendiente.FechaCortePrevio.Month, filtroPendiente.FechaCortePrevio.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte.Year, filtroPendiente.FechaCorte.Month, filtroPendiente.FechaCorte.Day, 23, 59, 59);
                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo_Cierre_Comparar]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre, fechaCierrePrevio });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();               
                var fechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var fechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);
                DateTime fechainicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechaFin.Year, fechaFin.Month,fechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte1.Year, filtroPendiente.FechaCorte1.Month, filtroPendiente.FechaCorte1.Day);
                DateTime fechaPagoInicial = new DateTime(filtroPendiente.FechaPagoInicial.Year, filtroPendiente.FechaPagoInicial.Month, filtroPendiente.FechaPagoInicial.Day, 0, 0, 0);
                DateTime fechaPagoFinal = new DateTime(filtroPendiente.FechaPagoFinal.Year, filtroPendiente.FechaPagoFinal.Month, filtroPendiente.FechaPagoFinal.Day, 23, 59, 59);
                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora,fechaCierre,fechaPagoInicial,fechaPagoFinal });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene los cambios para el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientesCambiosPorCoordinadorDTO</returns>
        public List<ReportePendientesCambiosPorCoordinadorDTO> ObtenerReportePendienteCambiosPorCoordinadorPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                var fechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var fechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);
                DateTime fechainicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte1.Year, filtroPendiente.FechaCorte1.Month, filtroPendiente.FechaCorte1.Day, 23, 59, 59);

                List<ReportePendientesCambiosPorCoordinadorDTO> items = new List<ReportePendientesCambiosPorCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesCambiosPorCoordinador_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora,fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesCambiosPorCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene las diferencias para el reporte de pendientes por mes coordinador 
        /// </summary>
        /// <returns>ReportePendientesDiferenciasDTO</returns>
        public List<ReportePendientesDiferenciasDTO> ObtenerReportePendienteDiferenciasPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }

                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                var fechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var fechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);
                DateTime fechainicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte1.Year, filtroPendiente.FechaCorte1.Month, filtroPendiente.FechaCorte1.Day, 23, 59, 59);
                //Modalidad = String.Join(",", FiltroPendiente.Modalidad);

                List<ReportePendientesDiferenciasDTO> items = new List<ReportePendientesDiferenciasDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesDiferencias_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesDiferenciasDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                var fechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var fechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);
                DateTime fechainicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte2.Year, filtroPendiente.FechaCorte2.Month, filtroPendiente.FechaCorte2.Day, 23, 59, 59);
                DateTime fechaPagoInicial = new DateTime(filtroPendiente.FechaPagoInicial.Year, filtroPendiente.FechaPagoInicial.Month, filtroPendiente.FechaPagoInicial.Day, 0, 0, 0);
                DateTime fechaPagoFinal = new DateTime(filtroPendiente.FechaPagoFinal.Year, filtroPendiente.FechaPagoFinal.Month, filtroPendiente.FechaPagoFinal.Day, 23, 59, 59);

                List<ReportePendientePeriodoyCoordinadorDTO> items = new List<ReportePendientePeriodoyCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodoyCoordinador_Cierre_Comparar]", new { fechainicio, fechafin, tipos = modalidad,  coordinadoras = coordinadora, fechaCierre, fechaPagoInicial, fechaPagoFinal });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientePeriodoyCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 21/01/2021
        /// <summary>
        /// Obtiene el reporte de pendientes por mes coordinador
        /// </summary>
        /// <returns>ReportePendientePeriodoyCoordinadorDTO</returns>
        public List<ReportePendientesModificacionesMesDTO> ObtenerReportePendienteModificacionesPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente)
        {
            try
            {
                string modalidad = null, coordinadora = null;
                if (filtroPendiente.Coordinadora.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Coordinadora)
                    {
                        coordinadora += item + " ";
                    }
                    coordinadora = coordinadora.Trim();
                    coordinadora = coordinadora.Replace(" ", ",");
                }

                //Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);
                if (filtroPendiente.Modalidad.Count() > 0)
                {
                    foreach (var item in filtroPendiente.Modalidad)
                    {
                        modalidad += item + " ";
                    }
                    modalidad = modalidad.Trim();
                    modalidad = modalidad.Replace(" ", ",");
                }
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                var fechaInicio = _repPeriodo.ObtenerFechaInicial(filtroPendiente.PeriodoInicial);
                var fechaFin = _repPeriodo.ObtenerFechaFinal(filtroPendiente.PeriodoFin);
                DateTime fechainicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                DateTime fechaCierre = new DateTime(filtroPendiente.FechaCorte2.Year, filtroPendiente.FechaCorte2.Month, filtroPendiente.FechaCorte2.Day, 23, 59, 59);
                DateTime fechaPagoInicial = new DateTime(filtroPendiente.FechaPagoInicial.Year, filtroPendiente.FechaPagoInicial.Month, filtroPendiente.FechaPagoInicial.Day, 0, 0, 0);
                DateTime fechaPagoFinal = new DateTime(filtroPendiente.FechaPagoFinal.Year, filtroPendiente.FechaPagoFinal.Month, filtroPendiente.FechaPagoFinal.Day, 23, 59, 59);

                List<ReportePendientesModificacionesMesDTO> items = new List<ReportePendientesModificacionesMesDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesModificaciones_Cierre]", new { fechainicio, fechafin, tipos = modalidad, coordinadoras = coordinadora, fechaCierre, fechaPagoInicial, fechaPagoFinal });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendientesModificacionesMesDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de EnlacePublicidadFacebook
        /// </summary>
        /// <returns></returns>
        public List<EnlacesPublicidadFacebookDTO> ObtenerListaEnlacesPublicidadFacebook(string FechaInicio, string FechaFin)
        {
            try
            {
                List<EnlacesPublicidadFacebookDTO> lista = new List<EnlacesPublicidadFacebookDTO>();
                var _query = string.Empty;

                _query = "SELECT Id, Nombre, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesPublicidadFacebook " +
                    "where convert(date,FechaCreacion)<=convert(date,'"+FechaFin+"') and convert(date,FechaCreacion)>=convert(date,'"+FechaInicio+"') order by fechaCreacion desc";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesPublicidadFacebookDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de EnlacesLandingPage
        /// </summary>
        /// <returns></returns>
        public List<EnlacesLandingPageDTO> ObtenerListaEnlacesLandingPage(string FechaInicio, string FechaFin)
        {
            try
            {
                List<EnlacesLandingPageDTO> lista = new List<EnlacesLandingPageDTO>();
                var _query = string.Empty;

                _query = "SELECT FlpCodigo, FstCampanha, PeCentroCosto, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesLandingPage " +
                    "where convert(date,FechaCreacion)<=convert(date,'" + FechaFin + "') and convert(date,FechaCreacion)>=convert(date,'" + FechaInicio + "') order by fechaCreacion desc";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesLandingPageDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene un enlace de un determinado conjunto de anuncio, para ello recibe el nombre del conjunto de anuncio
        /// </summary>
        /// <returns></returns>
        public List<EnlacesLandingPageDTO> ObtenerListaEnlacesLandingPage(string NombreConjuntoAnuncio)
        {
            try
            {
                List<EnlacesLandingPageDTO> lista = new List<EnlacesLandingPageDTO>();
                var _query = string.Empty;

                _query = "SELECT FlpCodigo, FstCampanha, PeCentroCosto, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesLandingPageV2 " +
                    "WHERE FstCampanha=@NombreConjuntoAnuncio order by FechaCreacion desc";
                var listaDB = _dapper.QueryDapper(_query, new { NombreConjuntoAnuncio = NombreConjuntoAnuncio });
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesLandingPageDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un enlace de un determinado conjunto de anuncio, para ello recibe el nombre del FormularioLandingPage
        /// </summary>
        /// <returns></returns>
        public List<EnlacesLandingPageDTO> ObtenerListaEnlacesLandingPagePorNombreLandingPage(string NombreFormularioLandingPage)
        {
            try
            {
                List<EnlacesLandingPageDTO> lista = new List<EnlacesLandingPageDTO>();
                var _query = string.Empty;

                _query = "SELECT FlpCodigo, FstCampanha, PeCentroCosto, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesLandingPageV2 " +
                    "WHERE FlpCodigo = @NombreFormularioLandingPage";
                var listaDB = _dapper.QueryDapper(_query, new { NombreFormularioLandingPage = NombreFormularioLandingPage });
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesLandingPageDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene la lista de ListaEnlacesLandingPageWeb
        /// </summary>
        /// <returns></returns>
        public List<EnlacesLandingPageWebDTO> ObtenerListaEnlacesLandingPageWeb(string FechaInicio, string FechaFin)
        {
            try
            {
                List<EnlacesLandingPageWebDTO> lista = new List<EnlacesLandingPageWebDTO>();
                var _query = string.Empty;

                _query = "SELECT Id, Nombre, Codigo, EsPopUp, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesLandingPageWeb " +
                    "where convert(date,FechaCreacion)<=convert(date,'" + FechaFin + "') and convert(date,FechaCreacion)>=convert(date,'" + FechaInicio + "') order by fechaCreacion desc";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesLandingPageWebDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de ListaEnlacesLandingPageWeb
        /// </summary>
        /// <returns></returns>
        public List<EnlacesLandingPageWebEstandarDTO> ObtenerListaEnlacesLandingPageWebEstandar(string FechaInicio, string FechaFin)
        {
            try
            {
                List<EnlacesLandingPageWebEstandarDTO> lista = new List<EnlacesLandingPageWebEstandarDTO>();
                var _query = string.Empty;

                _query = "SELECT Id, Nombre, Codigo, EsPopUp, DireccionUrl, FechaCreacion " +
                    "FROM mkt.V_EnlacesLandingPageWebEstandar " +
                    "where convert(date,FechaCreacion)<=convert(date,'" + FechaFin + "') and convert(date,FechaCreacion)>=convert(date,'" + FechaInicio + "') order by fechaCreacion desc";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaDB) && !listaDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesLandingPageWebEstandarDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de Urls de Publicidad de Whatsapp
        /// </summary>
        /// <returns></returns>
        public List<EnlacesPublicidadWhatsappDTO> ObtenerListaEnlacesWhatsapp(string CodigoPais)
        {
            try
            {
                List<EnlacesPublicidadWhatsappDTO> lista = new List<EnlacesPublicidadWhatsappDTO>();
               
                var query = _dapper.QuerySPDapper("[mkt].[SP_ObtenerPublicidadWhatsappPorPais]", new { CodigoPais= CodigoPais });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EnlacesPublicidadWhatsappDTO>>(query);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de monitoreo de mensajes de whatsapp para cada ConjuntoListaDetalle, dado un Id de ConjuntoLista
        /// </summary>
        /// <returns></returns>
        public List<ReporteMonitoreoMensajesWhatsappDTO> ObtenerReporteMonitoreoMensajesWhatsappPorConjuntoLista(int IdConjuntoLista, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ReporteMonitoreoMensajesWhatsappDTO> lista = new List<ReporteMonitoreoMensajesWhatsappDTO>();

                var query = _dapper.QuerySPDapper("[mkt].[SP_MonitoreoMensajesWhatsappPorConjuntoLista]", new { IdConjuntoLista= IdConjuntoLista, FechaInicio=FechaInicio,FechaFin=FechaFin }, 30);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ReporteMonitoreoMensajesWhatsappDTO>>(query);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

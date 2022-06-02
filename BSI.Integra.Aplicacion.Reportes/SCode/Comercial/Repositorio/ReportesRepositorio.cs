using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio
{
    /// Repositorio: ReportesRepositorio
    /// Autor: ,Edgar S.
    /// Fecha: 10/02/2021
    /// <summary>
    /// Gestión de Reportes
    /// </summary>
    public class ReportesRepositorio
    {
        private DapperRepository _dapper;
        public ReportesRepositorio()
        {
            _dapper = new DapperRepository();
        }

        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimiento(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesDTO> items = new List<ReporteSeguimientoOportunidadesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino
                });


                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{D
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesDTO>>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los solicitudes de visualizacion de informacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSolicitudesVisualizacion(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesDTO> items = new List<ReporteSeguimientoOportunidadesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoVisualizarInformacionNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos
                });

                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{D
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesDTO>>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los solicitudes de visualizacion de informacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSolicitudesVisualizacionOperaciones(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesDTO> items = new List<ReporteSeguimientoOportunidadesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoVisualizarInformacionOperacionesNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos
                });

                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{D
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesDTO>>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Aprueba la solicitud de informacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoFinalDTO AprobacionSolicitudVisualizacion(AprobacionSolicitudesVisualizacionFiltrosDTO aprobacion)
        {
            try
            {
                ResultadoFinalDTO items = new ResultadoFinalDTO();

                var query = _dapper.QuerySPFirstOrDefault("com.SP_AprobarSolicitudVisualizacion", new
                {
                    IdOportunidad = aprobacion.IdOportunidad,
                    IdPersonalSolicitante = aprobacion.IdPersonalSolicitante,
                    Usuario = aprobacion.Usuario,
                    IdSolicitudVisualizacion = aprobacion.IdSolicitudVisualizacion
                });

                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{D
                items = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesOperacionesDTO> ObtenerReporteSeguimientoOperaciones(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesOperacionesDTO> items = new List<ReporteSeguimientoOportunidadesOperacionesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadOperacionesNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino,
                    codigomatricula = filtro.CodigoMatricula,
                    documentoidentidad = filtro.DocumentoIdentidad,
                    estadosmatricula = filtro.EstadosMatricula,
                    controlFiltroFecha = filtro.ControlFiltroFecha
                });


                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesOperacionesDTO>>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades por fecha de creacion
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimientoFC(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesDTO> items = new List<ReporteSeguimientoOportunidadesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadFCNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades por fecha de registrocampania
        /// </summary>
        /// <param name="filtro">Objeto de clase SeguimientoFiltroFinalDTO</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadesDTO</returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimientoFRC(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesDTO> items = new List<ReporteSeguimientoOportunidadesDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadFRCNuevoModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades con las probabilidades
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesModeloDTO> ObtenerReporteSeguimientoProbabilidad(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesModeloDTO> items = new List<ReporteSeguimientoOportunidadesModeloDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadModelo", new
                {
                    asesores = filtro.Asesores,
                    faseOportunidad = filtro.FasesOportunidad,
                    centrosCosto = filtro.CentroCostos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    opcionFase = filtro.OpcionFase,
                    faseOportunidadOrigen = filtro.FasesOportunidadOrigen,
                    faseOportunidadDestino = filtro.FasesOportunidadDestino
                });


                //if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                //{D
                items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesModeloDTO>>(query);
                //}
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContacto(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<PreReporteTasaContactoDTO> datos = new List<PreReporteTasaContactoDTO>();
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                var queryEjecutadasConLlamadas = string.Empty;
                var queryTotal_TotalEjecutadas = string.Empty;

                queryTotal_TotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM com.V_ReporteTasaContacto
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND (IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                               OR IdEstadoOcurrencia = @IdEstadoOcurrenciaNoEjecutado)
                          AND IdFaseOportunidad != @IdFaseOportunidadE
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') { filtros.Filtro }
                  ";

                queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*)
                    FROM com.V_ReporteTasaContactoLlamada
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND IdFaseOportunidad NOT IN (4, 11,27)
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                          AND IdLlamada = 1
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') { filtros.Filtro }
                ";

                var respuestaDapperTotal_TotalEjecutadas = _dapper.FirstOrDefault(queryTotal_TotalEjecutadas, new
                {
                    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    ValorEstatico.IdEstadoOcurrenciaNoEjecutado,
                    ValorEstatico.IdFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                var respuestaDapperEjecutadasLlamada = _dapper.FirstOrDefault(queryEjecutadasConLlamadas, new
                {
                    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    ValorEstatico.IdFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas);
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapperEjecutadasLlamada);

                    item.TotalLlamadas = datosTotal.CantidadTotal;
                    item.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    item.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Select(w => w.Value).FirstOrDefault();
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContactoCongelado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<PreReporteTasaContactoDTO> datos = new List<PreReporteTasaContactoDTO>();
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                var queryEjecutadasConLlamadas = string.Empty;
                var queryTotal_TotalEjecutadas = string.Empty;

                queryTotal_TotalEjecutadas = $@"
                    SELECT COUNT(*) AS CantidadTotal, 
                           ISNULL(SUM(CASE
                                   WHEN IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                   THEN 1
                                   ELSE 0
                               END), 0) AS CantidadTotalEjecutada
                    FROM com.V_ReporteTasaContactoCongelado
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND (IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                               OR IdEstadoOcurrencia = @IdEstadoOcurrenciaNoEjecutado)
                          AND IdFaseOportunidad != @IdFaseOportunidadE
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC','AutomatizacionRN2') { filtros.Filtro }
                  ";

                queryEjecutadasConLlamadas = $@"
                    SELECT COUNT(*)
                    FROM com.V_ReporteTasaContactoLlamadaCongelado
                    WHERE EstadoOcurrencia = 1
                          AND EstadoOportunidad = 1
                          AND EstadoActividad = 1
                          AND IdFaseOportunidad NOT IN (4, 11,27)
                          AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                          AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)
                          AND IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                          AND IdLlamada = 1
                          AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2') { filtros.Filtro }
                ";

                var respuestaDapperTotal_TotalEjecutadas = _dapper.FirstOrDefault(queryTotal_TotalEjecutadas, new
                {
                    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    ValorEstatico.IdEstadoOcurrenciaNoEjecutado,
                    ValorEstatico.IdFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });
                var respuestaDapperEjecutadasLlamada = _dapper.FirstOrDefault(queryEjecutadasConLlamadas, new
                {
                    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    ValorEstatico.IdFaseOportunidadE,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(respuestaDapperTotal_TotalEjecutadas) && !respuestaDapperTotal_TotalEjecutadas.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<TasaContactoEjecutadoDTO>(respuestaDapperTotal_TotalEjecutadas);
                    var datosEjecutadasLlamada = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapperEjecutadasLlamada);

                    item.TotalLlamadas = datosTotal.CantidadTotal;
                    item.TotalLlamadasEjecutadas = datosTotal.CantidadTotalEjecutada;
                    item.TotalLlamadasEjecutadasConLlamada = datosEjecutadasLlamada.Select(w => w.Value).FirstOrDefault();
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContactoRn2(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                string _queryTasaContactoRn2 = _dapper.QuerySPFirstOrDefault("com.SP_ObtenertasaContactoRn2", filtros);
                if (!string.IsNullOrEmpty(_queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(_queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambios de fase Rn2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContactoRn2Congelado(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                string _queryTasaContactoRn2 = _dapper.QuerySPFirstOrDefault("com.SP_ObtenertasaContactoRn2Congelado", filtros);
                if (!string.IsNullOrEmpty(_queryTasaContactoRn2))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoDTO>(_queryTasaContactoRn2);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoConySinLlamadaDTO </returns>
        public ReporteTasaContactoConySinLlamadaDTO ObtenerReporteTasaContactoConySinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                ReporteTasaContactoConySinLlamadaDTO item = new ReporteTasaContactoConySinLlamadaDTO();
                ReporteTasaContactoConySinLlamadaDTO itemAuxiliarFC = new ReporteTasaContactoConySinLlamadaDTO();

                var queryOptimizado = string.Empty;
                queryOptimizado = @"Select Count(*) AS CambiosFaseTotal,
                    ISNULL(SUM(CASE WHEN EstadoLlamada = 1 THEN 1 ELSE 0 END), 0) AS CambiosFaseConLlamada, 
                    ISNULL(SUM(CASE WHEN EstadoLlamada = 0 THEN 1 ELSE 0 END), 0) AS CambiosFaseSinLlamada, 
                    0 AS CambiosFaseOCconLlamada, 0 AS CambiosFaseOCsinLlamada 
                    From com.V_ReporteTasaContactoConySinLlamada2 
                    Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and 
                    IdFaseOrigen != IdFaseDestino And Estado = 1 and
                    Cambio is not null and 
                    IdFaseDestinoCalculado NOT IN (4, 7, 9, 11, 27, 28, 32, 33, 34,29)";

                var respuestaDapperOptimizado = _dapper.FirstOrDefault(queryOptimizado, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                var queryOptimizadoOC = string.Empty;
                queryOptimizadoOC = @"Select 0 AS CambiosFaseTotal, 0 AS CambiosFaseConLlamada, 0 AS CambiosFaseSinLlamada, 
                    ISNULL(SUM(CASE WHEN EstadoLlamada = 1 THEN 1 ELSE 0 END), 0) AS CambiosFaseOCconLlamada, 
                    ISNULL(SUM(CASE WHEN EstadoLlamada = 0 THEN 1 ELSE 0 END), 0) AS CambiosFaseOCsinLlamada 
                    From com.V_ReporteTasaContactoConySinLlamada2 
                    Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + @" and
                    IdFaseOrigen != IdFaseDestino And Estado=1 and 
                    IdFaseDestinoCalculado IN (3, 5, 6, 10, 20, 23, 25, 26)";
                var respuestaDapperOptimizadoOC = _dapper.FirstOrDefault(queryOptimizadoOC, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(respuestaDapperOptimizado) && !respuestaDapperOptimizado.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(respuestaDapperOptimizado);
                }

                if (!string.IsNullOrEmpty(respuestaDapperOptimizadoOC) && !respuestaDapperOptimizadoOC.Contains("[]"))
                {
                    itemAuxiliarFC = JsonConvert.DeserializeObject<ReporteTasaContactoConySinLlamadaDTO>(respuestaDapperOptimizadoOC);
                }

                item.CambiosFaseOCconLlamada = itemAuxiliarFC.CambiosFaseOCconLlamada;
                item.CambiosFaseOCsinLlamada = itemAuxiliarFC.CambiosFaseOCsinLlamada;

                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de Cambio de Fase Rn2 (Joao: sin optimizacion, se deja de usar)
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Objeto DTO: ReporteTasaContactoConySinLlamadaDTO </returns>
        public ReporteTasaContactoConySinLlamadaDTO ObtenerReporteTasaContactoConySinLlamadaRn2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                //List<CambioFaseConySinLlamadaDTO> datos = new List<CambioFaseConySinLlamadaDTO>();
                ReporteTasaContactoConySinLlamadaDTO item = new ReporteTasaContactoConySinLlamadaDTO();
                var _query = string.Empty;
                _query = "Select Count(*) From com.V_ReporteTasaContactoConySinLlamada2 " +
                         "Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                         "IdFaseOrigen != IdFaseDestino And Estado=1 and " +
                         "Cambio is not null And EstadoLlamada=1 and " +
                         "IdFaseOrigen in (10) and " +
                          "IdFaseDestinoCalculado not in (4, 7, 9, 11, 27, 28, 32, 33, 34)";
                var respuestaDapper = _dapper.FirstOrDefault(_query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });


                var _query2 = string.Empty;
                _query2 = "Select Count(*) From com.V_ReporteTasaContactoConySinLlamada2 " +
                         "Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                         "IdFaseOrigen != IdFaseDestino And Estado=1 and " +
                         "IdFaseOrigen in (10) and " +
                        "IdFaseDestinoCalculado not in (4, 7, 9, 11, 27, 28, 32, 33, 34)";
                var respuestaDapper2 = _dapper.FirstOrDefault(_query2, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });


                var _query3 = string.Empty;
                _query3 = "Select Count(*) From com.V_ReporteTasaContactoConySinLlamada2 " +
                         "Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                         "IdFaseOrigen != IdFaseDestino And Estado=1 and " +
                         "Cambio is not null and EstadoLlamada =0  and  " +
                         "IdFaseOrigen in (10) and " +
                         "IdFaseDestinoCalculado not in (4, 7, 9, 11, 27, 28, 32, 33, 34)";
                var respuestaDapper3 = _dapper.FirstOrDefault(_query3, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                var _query4 = string.Empty;
                _query4 = "Select Count(*) From com.V_ReporteTasaContactoConySinLlamada2 " +
                         "Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                         "IdFaseOrigen != IdFaseDestino And Estado=1 and " +
                         "Cambio is not null and EstadoLlamada=1 and IdFaseOrigen = 10 AND " +
                         "IdFaseDestinoCalculado IN(3, 5, 6, 23, 25, 26)";
                var respuestaDapper4 = _dapper.FirstOrDefault(_query4, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });


                var _query5 = string.Empty;
                _query5 = "Select Count(*) From com.V_ReporteTasaContactoConySinLlamada2 " +
                         "Where FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                         "IdFaseOrigen != IdFaseDestino And Estado=1 and " +
                         "Cambio is not null and  EstadoLlamada =0 and IdFaseOrigen = 10 AND " +
                         "IdFaseDestinoCalculado IN(3, 5, 6, 23, 25, 26)";
                var respuestaDapper5 = _dapper.FirstOrDefault(_query5, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    var datosConLlamada = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapper);
                    item.CambiosFaseConLlamada = datosConLlamada.Select(w => w.Value).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(respuestaDapper2) && !respuestaDapper2.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapper2);
                    item.CambiosFaseTotal = datosTotal.Select(w => w.Value).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(respuestaDapper3) && !respuestaDapper3.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapper3);
                    item.CambiosFaseSinLlamada = datosTotal.Select(w => w.Value).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(respuestaDapper4) && !respuestaDapper4.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapper4);
                    item.CambiosFaseOCconLlamada = datosTotal.Select(w => w.Value).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(respuestaDapper5) && !respuestaDapper5.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapper5);
                    item.CambiosFaseOCsinLlamada = datosTotal.Select(w => w.Value).FirstOrDefault();
                }
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Autor: Carlos Crispin R.
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns>List<ReporteSeguimientoOportunidadLogDTO></returns>

        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLog(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, FechaSiguienteLlamada, IdFaseOportunidad, IdFaseOportunidadIP, IdFaseOportunidadPF, IdFaseOportunidadIC," +
                    "FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, IdOcurrencia, IdEstadoOcurrencia, TiempoDuracion, TiempoDuracionMinutos, TiempoDuracion3CX, IdCentralLLamada," +
                    "IdTresCX, IdOportunidadLog, FechaIncioLlamadaIntegra,EstadoLlamadaIntegra, EstadoLlamadaTresCX, FechaIncioLlamadaTresCX, NombreActividad, NombreOcurrencia, ComentarioActividad, " +
                    "FechaFinLlamadaIntegra, FechaFinLlamadaTresCX, SubEstadoLlamadaTresCX, SubEstadoLlamadaIntegra, IdFaseOportunidadInicial, NombreGrabacionIntegra, NombreGrabacionTresCX, Webphone " +
                    "FROM com.V_ObtenerOportunidadLogReporteSeguimientoNW WHERE IdOportunidad = @idOportunidad AND EstadoOportunidadLog=1 AND (ComentarioActividad<>'Asignacion Manual' OR ComentarioActividad IS NULL)  AND IdActividadDetalle IS NOT NULL " +
                    "ORDER BY FechaModificacion";
                var queryRespuesta = _dapper.QueryDapper(query, new { idOportunidad = idOportunidad });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta);

                    oportunidades = (from p in oportunidadesLog
                                     group p by new
                                     {
                                         p.FaseInicio,
                                         p.FaseDestino,
                                         p.FechaModificacion,
                                         p.FechaSiguienteLlamada,
                                         p.IdFaseOportunidad,
                                         p.IdFaseOportunidadIP,
                                         p.IdFaseOportunidadPF,
                                         p.IdFaseOportunidadIC,
                                         p.FechaEnvioFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadIC,
                                         p.IdOcurrencia,
                                         p.IdEstadoOcurrencia,
                                         p.IdOportunidadLog,
                                         p.NombreActividad,
                                         p.NombreOcurrencia,
                                         p.ComentarioActividad,
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
                                     {
                                         FaseInicio = g.Key.FaseInicio,
                                         FaseDestino = g.Key.FaseDestino,
                                         FechaModificacion = g.Key.FechaModificacion,
                                         FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                                         IdFaseOportunidad = g.Key.IdFaseOportunidad,
                                         IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                                         IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                                         IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                                         FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                                         FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                                         FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                                         IdOcurrencia = g.Key.IdOcurrencia,
                                         IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                                         IdOportunidadLog = g.Key.IdOportunidadLog,
                                         NombreActividad = g.Key.NombreActividad,
                                         NombreOcurrencia = g.Key.NombreOcurrencia,
                                         ComentarioActividad = g.Key.ComentarioActividad,
                                         IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                         //TiempoDuracion = string.Join("\n", g.Select(x => new { x.TiempoDuracion, x.IdCentralLLamada })
                                         //                                    .GroupBy(i => i.IdCentralLLamada).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion)),
                                         //TiempoDuracion3CX = string.Join("\n", g.Select(x => new { x.TiempoDuracion3CX, x.IdTresCX })
                                         //                                    .GroupBy(i => i.IdTresCX).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion3CX)),
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                     }).ToList();
                }

                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad
        /// </summary>
        /// <param name="IdAlumno"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLogPorIdAlumno(int IdAlumno, int IdOportunidad, int IdPadre)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, FechaSiguienteLlamada, IdFaseOportunidad, IdFaseOportunidadIP, IdFaseOportunidadPF, IdFaseOportunidadIC," +
                    "FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, IdOcurrencia, IdEstadoOcurrencia, TiempoDuracion, TiempoDuracion3CX, IdCentralLLamada," +
                    "IdTresCX, IdOportunidadLog, FechaIncioLlamadaIntegra,EstadoLlamadaIntegra, EstadoLlamadaTresCX, FechaIncioLlamadaTresCX, NombreActividad, NombreOcurrencia, ComentarioActividad, " +
                    "FechaFinLlamadaIntegra, FechaFinLlamadaTresCX, SubEstadoLlamadaTresCX, SubEstadoLlamadaIntegra, IdFaseOportunidadInicial, NombreGrabacionIntegra, NombreGrabacionTresCX,Webphone " +
                    "FROM com.V_ObtenerOportunidadLogReporteSeguimientoNW WHERE IdContacto = @IdAlumno AND IdOportunidad in (" + IdOportunidad + "," + IdPadre + ") AND EstadoOportunidadLog=1" +
                    "ORDER BY FechaModificacion";
                var queryRespuesta = _dapper.QueryDapper(query, new { IdAlumno });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta);

                    oportunidades = (from p in oportunidadesLog
                                     group p by new
                                     {
                                         p.FaseInicio,
                                         p.FaseDestino,
                                         p.FechaModificacion,
                                         p.FechaSiguienteLlamada,
                                         p.IdFaseOportunidad,
                                         p.IdFaseOportunidadIP,
                                         p.IdFaseOportunidadPF,
                                         p.IdFaseOportunidadIC,
                                         p.FechaEnvioFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadIC,
                                         p.IdOcurrencia,
                                         p.IdEstadoOcurrencia,
                                         p.IdOportunidadLog,
                                         p.NombreActividad,
                                         p.NombreOcurrencia,
                                         p.ComentarioActividad,
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
                                     {
                                         FaseInicio = g.Key.FaseInicio,
                                         FaseDestino = g.Key.FaseDestino,
                                         FechaModificacion = g.Key.FechaModificacion,
                                         FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                                         IdFaseOportunidad = g.Key.IdFaseOportunidad,
                                         IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                                         IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                                         IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                                         FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                                         FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                                         FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                                         IdOcurrencia = g.Key.IdOcurrencia,
                                         IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                                         IdOportunidadLog = g.Key.IdOportunidadLog,
                                         NombreActividad = g.Key.NombreActividad,
                                         NombreOcurrencia = g.Key.NombreOcurrencia,
                                         ComentarioActividad = g.Key.ComentarioActividad,
                                         IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                         //TiempoDuracion = string.Join("\n", g.Select(x => new { x.TiempoDuracion, x.IdCentralLLamada })
                                         //                                    .GroupBy(i => i.IdCentralLLamada).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion)),
                                         //TiempoDuracion3CX = string.Join("\n", g.Select(x => new { x.TiempoDuracion3CX, x.IdTresCX })
                                         //                                    .GroupBy(i => i.IdTresCX).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion3CX)),
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                     }).ToList();
                }

                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad Con el estado del KHOMP [Richard]
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLogRichard(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = "SELECT FaseInicio, FaseDestino, FechaModificacion, FechaSiguienteLlamada, IdFaseOportunidad, IdFaseOportunidadIP, IdFaseOportunidadPF, IdFaseOportunidadIC," +
                    "FechaEnvioFaseOportunidadPF, FechaPagoFaseOportunidadPF, FechaPagoFaseOportunidadIC, IdOcurrencia, IdEstadoOcurrencia, TiempoDuracion, TiempoDuracion3CX, IdCentralLLamada," +
                    "IdTresCX, IdOportunidadLog, FechaIncioLlamadaIntegra,EstadoLlamadaIntegra, EstadoLlamadaTresCX, FechaIncioLlamadaTresCX, NombreActividad, NombreOcurrencia, ComentarioActividad, " +
                    "FechaFinLlamadaIntegra, FechaFinLlamadaTresCX, SubEstadoLlamadaTresCX, SubEstadoLlamadaIntegra, IdFaseOportunidadInicial, NombreGrabacionIntegra, NombreGrabacionTresCX, EstadoLlamadaSegunFlow " +
                    "FROM com.V_ObtenerOportunidadLogReporteSeguimientoV2Richard WHERE IdOportunidad = @idOportunidad AND EstadoOportunidadLog=1" +
                    "ORDER BY FechaModificacion";
                var queryRespuesta = _dapper.QueryDapper(query, new { idOportunidad = idOportunidad });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta);

                    oportunidades = (from p in oportunidadesLog
                                     group p by new
                                     {
                                         p.FaseInicio,
                                         p.FaseDestino,
                                         p.FechaModificacion,
                                         p.FechaSiguienteLlamada,
                                         p.IdFaseOportunidad,
                                         p.IdFaseOportunidadIP,
                                         p.IdFaseOportunidadPF,
                                         p.IdFaseOportunidadIC,
                                         p.FechaEnvioFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadIC,
                                         p.IdOcurrencia,
                                         p.IdEstadoOcurrencia,
                                         p.IdOportunidadLog,
                                         p.NombreActividad,
                                         p.NombreOcurrencia,
                                         p.ComentarioActividad,
                                         p.IdFaseOportunidadInicial
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
                                     {
                                         FaseInicio = g.Key.FaseInicio,
                                         FaseDestino = g.Key.FaseDestino,
                                         FechaModificacion = g.Key.FechaModificacion,
                                         FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                                         IdFaseOportunidad = g.Key.IdFaseOportunidad,
                                         IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                                         IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                                         IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                                         FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                                         FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                                         FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                                         IdOcurrencia = g.Key.IdOcurrencia,
                                         IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                                         IdOportunidadLog = g.Key.IdOportunidadLog,
                                         NombreActividad = g.Key.NombreActividad,
                                         NombreOcurrencia = g.Key.NombreOcurrencia,
                                         ComentarioActividad = g.Key.ComentarioActividad,
                                         IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                         //TiempoDuracion = string.Join("\n", g.Select(x => new { x.TiempoDuracion, x.IdCentralLLamada })
                                         //                                    .GroupBy(i => i.IdCentralLLamada).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion)),
                                         //TiempoDuracion3CX = string.Join("\n", g.Select(x => new { x.TiempoDuracion3CX, x.IdTresCX })
                                         //                                    .GroupBy(i => i.IdTresCX).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion3CX)),
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             EstadoLlamadaSegunFlow = o.EstadoLlamadaSegunFlow,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                     }).ToList();
                }

                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la oportunidad con Codigo de Fase de acuerdo a un IdOportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase ReporteSeguimientoOportunidadLogGridDTO</returns>
        public ReporteSeguimientoOportunidadLogGridDTO ObtenerOportunidadCodigoFase(int idOportunidad)
        {
            try
            {
                ReporteSeguimientoOportunidadLogGridDTO item = new ReporteSeguimientoOportunidadLogGridDTO();
                var query = "SELECT FaseInicio, FechaSiguienteLlamada, IdFaseOportunidad FROM com.V_ObtenerOportunidadCodigoFase WHERE IdOportunidad = @idOportunidad";
                var queryRespuesta = _dapper.FirstOrDefault(query, new { idOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<ReporteSeguimientoOportunidadLogGridDTO>(queryRespuesta);
                }
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene la oportunidad con Codigo de Fase de acuerdo a un IdAlumno
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public ReporteSeguimientoOportunidadLogGridDTO ObtenerOportunidadCodigoFaseporIdAlumno(int IdAlumno, int IdOportunidad)
        {
            try
            {
                ReporteSeguimientoOportunidadLogGridDTO item = new ReporteSeguimientoOportunidadLogGridDTO();
                var query = "SELECT FaseInicio, FechaSiguienteLlamada, IdFaseOportunidad FROM com.V_ObtenerOportunidadCodigoFase WHERE IdAlumno = @IdAlumno AND IdOportunidad = @IdOportunidad";
                var queryRespuesta = _dapper.FirstOrDefault(query, new { IdAlumno = IdAlumno, IdOportunidad = IdOportunidad });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<ReporteSeguimientoOportunidadLogGridDTO>(queryRespuesta);
                }
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades segun el ultimo log
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Numero = 1 and Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino " +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        ///Repositorio: ReportesRepositorio
        ///Autor: Edgar S.
        ///Fecha: 15/03/2021
        /// <summary>
        /// Obtiene el reporte de tasa conversión consolidadas por Periodo Semanal
        /// </summary>
        /// <param name="coordinadores"> Id de Coordinadores </param>
        /// <param name="asesores"> Id de Asesores </param>
        /// <param name="periodoInicio"> Id de periodo Inicio </param>
        /// <param name="periodoFin"> Id de periodo Fin </param>
        /// <returns> ReporteTasaConversionConsolidadaGraficasVistaDTO </returns>
        public ReporteTasaConversionConsolidadaGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficas(string coordinadores, string asesores, string periodoInicio, string periodoFin)
        {
            try
            {
                ReporteTasaConversionConsolidadaGraficasVistaDTO rpta = new ReporteTasaConversionConsolidadaGraficasVistaDTO();
                rpta.Consolidado = new List<TCRM_ConsolidadTCAsesoresGraficas>();


                if (string.IsNullOrEmpty(coordinadores))
                {
                    coordinadores = "_";
                }
                if (string.IsNullOrEmpty(asesores))
                {
                    asesores = "_";
                }


                var query = _dapper.QuerySPDapper("com.SP_GenerarReportePorPeriodo_Graficas", new
                {
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    startPeriod = periodoInicio,
                    endPeriod = periodoFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesoresGraficas>>(query);
                    rpta.Consolidado = rpta.Consolidado.OrderBy(w => w.Ano).ThenBy(w => w.NroSemana).ToList();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ReportesRepositorio
        ///Autor: Edgar S.
        ///Fecha: 15/03/2021
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por Periodo Mensual
        /// </summary>
        /// <param name="coordinadores"> Id de Coordinadores </param>
        /// <param name="asesores"> Id de Asesores </param>
        /// <param name="periodoInicio"> Id de periodo Inicio </param>
        /// <param name="periodoFin"> Id de periodo Fin </param>
        /// <returns> ReporteTasaConversionConsolidadaMensualGraficasVistaDTO </returns>
        public ReporteTasaConversionConsolidadaMensualGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficasMensual(string coordinadores, string asesores, string periodoInicio, string periodoFin)
        {
            try
            {

                ReporteTasaConversionConsolidadaMensualGraficasVistaDTO rpta = new ReporteTasaConversionConsolidadaMensualGraficasVistaDTO();
                rpta.Consolidado = new List<TCRM_ConsolidadTCAsesoresMensualGraficas>();
                List<TCRM_ConsolidadTCAsesoresMensualGraficas> listaMensual = new List<TCRM_ConsolidadTCAsesoresMensualGraficas>();

                if (string.IsNullOrEmpty(coordinadores))
                {
                    coordinadores = "_";
                }
                if (string.IsNullOrEmpty(asesores))
                {
                    asesores = "_";
                }

                var query = _dapper.QuerySPDapper("com.SP_GenerarReportePorPeriodoMensual_Graficas", new
                {
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    startPeriod = periodoInicio,
                    endPeriod = periodoFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesoresMensualGraficas>>(query);
                    rpta.Consolidado = rpta.Consolidado.OrderBy(w => w.Ano).ThenBy(w => w.Mes).ToList();
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public ReporteTasaConversionConsolidadaAsesoresDTO ReporteTasaConversionConsolidadoAsesores(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            ReporteTasaConversionConsolidadaAsesoresDTO rpta = new ReporteTasaConversionConsolidadaAsesoresDTO();
            rpta.Consolidado = new List<TCRM_ConsolidadTCAsesores>();
            rpta.Desagregado = new List<TCRM_TasaConversionByCategoriaDatoPaisDTO>();


            if (string.IsNullOrEmpty(area))
            {
                area = "_";
            }
            if (string.IsNullOrEmpty(subarea))
            {
                subarea = "_";
            }
            if (string.IsNullOrEmpty(pgeneral))
            {
                pgeneral = "_";
            }
            if (string.IsNullOrEmpty(pespecifico))
            {
                pespecifico = "_";
            }
            if (string.IsNullOrEmpty(modalidades))
            {
                modalidades = "_";
            }
            if (string.IsNullOrEmpty(ciudades))
            {
                ciudades = "_";
            }
            if (string.IsNullOrEmpty(coordinadores))
            {
                coordinadores = "_";
            }
            if (string.IsNullOrEmpty(asesores))
            {
                asesores = "_";
            }

            if (fecha)
            {
                var query = _dapper.QuerySPDapper("com.SP_GenerarReporteTCAsesoresPais", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesores>>(query);
                }
            }
            else
            {
                var query = _dapper.QuerySPDapper("com.SP_GenerarReporteTCAsesoresPaisCierre", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta.Consolidado = JsonConvert.DeserializeObject<List<TCRM_ConsolidadTCAsesores>>(query);

                }
            }

            var result = (from p in rpta.Consolidado
                          group p by new
                          {
                              p.probabilidadDesc,
                              p.ordenp,
                              p.grupo,
                              p.nombreGrupo,
                              p.tcMeta
                          } into g
                          select new TCRM_TasaConversionByCategoriaDatoPaisDTO
                          {
                              probabilidadDesc = g.Key.probabilidadDesc,
                              orden = g.Key.ordenp,
                              grupo = g.Key.grupo.ToString(),
                              tcMeta = g.Key.tcMeta,
                              nombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo,
                              pais = Guid.Empty.ToString(),
                              listaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                              {
                                  orden = o.orden,
                                  probabilidadDesc = o.probabilidadDesc,
                                  pais = o.pais,
                                  idCoordinador = o.idCoordinador,
                                  nombreCoordinador = o.nombreCoordinador,
                                  idasesor = o.idasesor,
                                  nombre = o.nombre,
                                  idcategoriaOrigen = o.idcategoriaOrigen,
                                  ca_nombre = o.ca_nombre,
                                  inscritosMatriculados = o.inscritosMatriculados,
                                  oportunidadesCerradas = o.oportunidadesCerradas,
                                  idSub = o.idSub,
                                  nombreSub = o.nombreSub,
                                  tcMeta = o.tcMeta
                              }).ToList()

                          });
            var result2 = (from p in rpta.Consolidado
                           group p by new
                           {
                               p.probabilidadDesc,
                               p.ordenp,
                               p.grupo,
                               p.nombreGrupo,
                               p.pais,
                               p.nombrePais,
                               p.tcMeta,
                           } into g
                           select new TCRM_TasaConversionByCategoriaDatoPaisDTO
                           {
                               probabilidadDesc = g.Key.probabilidadDesc,
                               orden = g.Key.ordenp,
                               grupo = g.Key.grupo.ToString(),
                               tcMeta = g.Key.tcMeta,
                               nombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo + " " + g.Key.nombrePais,
                               pais = g.Key.pais.ToString(),
                               nombrePais = g.Key.nombrePais,
                               listaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                               {
                                   orden = o.orden,
                                   probabilidad = o.probabilidadDesc,
                                   pais = o.pais,
                                   idasesor = o.idasesor,
                                   nombre = o.nombre,
                                   idCoordinador = o.idCoordinador,
                                   nombreCoordinador = o.nombreCoordinador,
                                   idcategoriaOrigen = o.idcategoriaOrigen,
                                   ca_nombre = o.ca_nombre,
                                   inscritosMatriculados = o.inscritosMatriculados,
                                   oportunidadesCerradas = o.oportunidadesCerradas,
                                   idSub = o.idSub,
                                   nombreSub = o.nombreSub,
                                   tcMeta = o.tcMeta
                               }).ToList()

                           });

            rpta.Desagregado = result2.ToList();

            rpta.Desagregado.AddRange(result.ToList());
            rpta.Desagregado = rpta.Desagregado.OrderBy(x => x.orden).ThenByDescending(x => x.tcMeta).ThenBy(w => w.grupo).ThenBy(w => w.nombreGrupo).ToList();
            return rpta;

        }
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<TCRM_CentroCostoByAsesorDTO> ObtenerCentroCostoPorAsesor(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            List<TCRM_CentroCostoByAsesorDTO> rpta = new List<TCRM_CentroCostoByAsesorDTO>();

            if (string.IsNullOrEmpty(area))
            {
                area = "_";
            }
            if (string.IsNullOrEmpty(subarea))
            {
                subarea = "_";
            }
            if (string.IsNullOrEmpty(pgeneral))
            {
                pgeneral = "_";
            }
            if (string.IsNullOrEmpty(pespecifico))
            {
                pespecifico = "_";
            }
            if (string.IsNullOrEmpty(modalidades))
            {
                modalidades = "_";
            }
            if (string.IsNullOrEmpty(ciudades))
            {
                ciudades = "_";
            }
            if (string.IsNullOrEmpty(coordinadores))
            {
                coordinadores = "_";
            }
            if (string.IsNullOrEmpty(asesores))
            {
                asesores = "_";
            }

            if (fecha)
            {
                var query = _dapper.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPais", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDTO>>(query);
                }
            }
            else
            {
                var query = _dapper.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPaisCierre", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDTO>>(query);

                }
            }
            return rpta;
        }
        /// <summary>
        /// Obtiene el reporte de tasa conversion consolidadas por asesor
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<TCRM_CentroCostoByAsesorDetallesDTO> ObtenerCentroCostoPorAsesorDetalles(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            List<TCRM_CentroCostoByAsesorDetallesDTO> rpta = new List<TCRM_CentroCostoByAsesorDetallesDTO>();

            if (string.IsNullOrEmpty(area))
            {
                area = "_";
            }
            if (string.IsNullOrEmpty(subarea))
            {
                subarea = "_";
            }
            if (string.IsNullOrEmpty(pgeneral))
            {
                pgeneral = "_";
            }
            if (string.IsNullOrEmpty(pespecifico))
            {
                pespecifico = "_";
            }
            if (string.IsNullOrEmpty(modalidades))
            {
                modalidades = "_";
            }
            if (string.IsNullOrEmpty(ciudades))
            {
                ciudades = "_";
            }
            if (string.IsNullOrEmpty(coordinadores))
            {
                coordinadores = "_";
            }
            if (string.IsNullOrEmpty(asesores))
            {
                asesores = "_";
            }

            if (fecha)
            {
                var query = _dapper.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPaisCierreDetalles", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDetallesDTO>>(query);
                }
            }
            else
            {
                var query = _dapper.QuerySPDapper("com.SP_GetOportunidadesCentroCostoTCAsesoresPaisCierreDetalles", new
                {
                    areaTCAP = area,
                    subAreaTCAP = subarea,
                    proGeneralTCAP = pgeneral,
                    pEspecificoTCAP = pespecifico,
                    modalidadesTCAP = modalidades,
                    ciudadesTCAP = ciudades,
                    coordinadoresTCAP = coordinadores,
                    asesoresTCAP = asesores,
                    fechaInicioTCAP = fechaInicio,
                    fechaFinTCAP = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TCRM_CentroCostoByAsesorDetallesDTO>>(query);

                }
            }
            return rpta;
        }
        /// <summary>
        /// obtiene el precio en cuotas del programa y del pais enviado
        /// </summary>
        /// <param name="idCC"></param>
        /// <param name="pais"></param>
        /// <returns></returns>
        public List<TCRM_PrecioDTO> ObtenerPrecioProgramaCuotas(int idCC, int pais)
        {
            //Tipopago=2 (Credito)
            List<TCRM_PrecioDTO> result = new List<TCRM_PrecioDTO>();
            var _query = string.Empty;
            _query = "select precio from  [com].[V_MontosCentroCostoPais] Where idcentrocosto = @idCC and IdTipoPago = 2 And IdPais=@pais";
            var queryRespuesta = _dapper.QueryDapper(_query, new { idCC = idCC, pais = pais });

            if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
            {
                result = JsonConvert.DeserializeObject<List<TCRM_PrecioDTO>>(queryRespuesta);
            }
            return result;
        }
        /// <summary>
        /// obtiene el precio al contado del programa y del pais enviado
        /// </summary>
        /// <param name="idCC"></param>
        /// <param name="pais"></param>
        /// <returns></returns>
        public List<TCRM_PrecioDTO> ObtenerPrecioProgramaContado(int idCC, int pais)
        {
            //Tipopago=1 (Contado)
            List<TCRM_PrecioDTO> result = new List<TCRM_PrecioDTO>();
            var _query = string.Empty;
            _query = "select precio from  [com].[V_MontosCentroCostoPais] Where idcentrocosto = @idCC and IdTipoPago = 1 And IdPais=@pais";
            var queryRespuesta = _dapper.QueryDapper(_query, new { idCC, pais });

            if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
            {
                result = JsonConvert.DeserializeObject<List<TCRM_PrecioDTO>>(queryRespuesta);
            }
            return result;
        }
        /// <summary>
        /// obtiene el precio al contado del programa y del pais enviado
        /// </summary>
        /// <param name="idCC"></param>
        /// <param name="pais"></param>
        /// <returns></returns>
        public List<TCRM_PrecioDTO> ObtenerPrecioProgramaPorDefecto(int idCC)
        {
            //Tipopago=1 (Contado)
            List<TCRM_PrecioDTO> result = new List<TCRM_PrecioDTO>();
            var _query = string.Empty;
            _query = "select precio from  [com].[V_MontosCentroCostoPais] Where idcentrocosto = @idCC";
            var queryRespuesta = _dapper.QueryDapper(_query, new { idCC });

            if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
            {
                result = JsonConvert.DeserializeObject<List<TCRM_PrecioDTO>>(queryRespuesta);
            }
            return result;
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra de las oportunidades según el ultimo log
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "Numero, " +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "IdFaseDestino Not In (4, 7, 9, 11, 28, 32, 33)" +
                            "GROUP BY Numero,Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }

                items = items.Where(w => w.Numero == 1).ToList();
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Reporte de cambio de fase no acumulado con llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto de DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada = 1 and " +
                            "IdEstadoOcurrencia = 1 and " +
                            "FaseDestino Not In ('RN', 'RN1','BNC1','E','BIC','OD','OM','RN5')" +
                            //"IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene reporte de cambio de fase no acumulado sin llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamadaNoAcumulado2 WHERE  Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada =0 and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new { FechaInicio = filtros.FechaInicio, FechaFin = filtros.FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objetos DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino " +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene reporte acumulado de fase RN1 Y RN
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = @"SELECT 
                COUNT(*) AS NumeroRegistros, 
                FaseOrigen, 
                FaseDestino, 
                'td' as TipoDato, 
                0.0 as MetaLanzamiento, 
                0 as IndicadorLanzamiento 
                FROM com.V_ReporteCambiosDeFaseOportunidadRN1 AS t WHERE Estado = 1 AND 
                FechaLog between @FechaInicio AND @FechaFin " + filtros.Filtro + @" AND 
                IdFaseOrigen != IdFaseDestino AND 
                IdFaseDestino IN (7,9) 
                GROUP BY FaseOrigen, FaseDestino";

                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Reporte No Acumulado de Fase RN1 y RN2
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlNoAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "IdFaseDestinoCalculado In (7,9)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";

                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene todas las transiciones de una fase a otra en general de las oportunidades 
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de objetos DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidad2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 28, 32, 33, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";


                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Todo los cambios de fase realizados con Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns></returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada = 1 and " +
                            "IdFaseDestinoCalculado Not In (4, 7, 9, 11, 27, 28, 32, 33, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Todo los cambios de fase realizados sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();
                var query = "SELECT " +
                            "Count(Cambio) AS NumeroRegistros," +
                            "FaseOrigen," +
                            "FaseDestino," +
                            "0.0 MetaLanzamiento," +
                            "0 IndicadorLanzamiento, " +
                            "'' TipoDato " +
                            "FROM com.V_ReporteCambiosDeFaseOportunidadConySinLlamada2 WHERE Estado = 1 and " +
                            "FechaLog between @FechaInicio  and @FechaFin " + filtros.Filtro + " and " +
                            "IdFaseOrigen != IdFaseDestino and " +
                            "EstadoLlamada =0 and " +
                            "IdFaseDestinoCalculado Not IN(4, 7, 9, 11, 28, 34,29)" +
                            "GROUP BY Cambio,FaseOrigen, FaseDestino";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicio = filtros.FechaInicio,
                    FechaFin = filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene reporte de cambio de fase BIC y acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO: List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseControlBICYEAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> items = new List<ReporteCambiosDeFaseOportunidadDTO>();

                var listaFaseOportunidadExcluir = new List<int>(){
                    ValorEstatico.IdFaseOportunidadBIC,
                    ValorEstatico.IdFaseOportunidadE,
                    36 /* Fase ISM*/
                };
                var query1 = $@"
                        SELECT COUNT(Cambio) AS NumeroRegistros, 
                               FaseOrigen, 
                               FaseDestino, 
                               0.0 MetaLanzamiento, 
                               0 IndicadorLanzamiento, 
                               '' TipoDato
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin { filtros.Filtro } 
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        GROUP BY FaseOrigen, 
                                 FaseDestino";

                var query2 = $@"
                        SELECT Cambio, 
                               FaseOrigen
                        FROM com.V_ReporteCambiosDeFaseOportunidad3
                        WHERE Estado = 1
                              AND FechaLog BETWEEN @FechaInicio AND @FechaFin { filtros.Filtro }
                              AND IdFaseOrigen != IdFaseDestino
                              AND IdFaseOportunidadLog IN @listaFaseOportunidadExcluir
                        ";
                var queryRespuesta1 = _dapper.QueryDapper(query1, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                });
                var queryRespuesta2 = _dapper.QueryDapper(query2, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    listaFaseOportunidadExcluir
                    //FaseBic = ValorEstatico.IdFaseOportunidadBIC,
                    //FaseE = ValorEstatico.IdFaseOportunidadE
                });
                if (!string.IsNullOrEmpty(queryRespuesta1) && !queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDeFaseOportunidadDTO>>(queryRespuesta1);
                    var lista = JsonConvert.DeserializeObject<List<CambiosDeFaseControlBICYEDTO>>(queryRespuesta2);
                    foreach (var item in items)
                    {
                        var indicadorLanzamiento = lista.Where(x => x.FaseOrigen == item.FaseOrigen).Count();
                        item.MetaLanzamiento = (item.NumeroRegistros * 100) / indicadorLanzamiento;
                    }
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene el total de IS
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> int </returns>
        public int ReporteMetasObtenerTotalIS(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.Asesores) + ")";
                }
                int total = 0;
                var query = @"
                    SELECT COUNT(Id) AS Cantidad
                    FROM com.V_GenerarReporteMetasGetTotalIS
                    WHERE FechaLog BETWEEN @FechaInicio AND @FechaFin
                            AND Estado = 1
                            AND IdFaseOportunidad = @IdFaseOportunidadIS
                            AND IdFaseOportunidad <> IdFaseOportunidadAnt 
                    " + filtro;
                var queryRespuesta = _dapper.FirstOrDefault(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    ValorEstatico.IdFaseOportunidadIS
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    var result = JsonConvert.DeserializeObject<ReporteMetasGetTotalISDTO>(queryRespuesta);
                    total = result.Cantidad;
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambiodeFaseV2DTO> ReporteControlCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                List<ControlCambiodeFaseV2DTO> result = new List<ControlCambiodeFaseV2DTO>();

                string Asesores = null;
                string CentroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    CentroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividades";
                var queryRespuesta = _dapper.QuerySPDapper(query, new
                {
                    Asesores,
                    CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    result = JsonConvert.DeserializeObject<List<ControlCambiodeFaseV2DTO>>(queryRespuesta);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 22/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte de control de cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtros de Búsqueda </param>
        /// <returns> Lista de objeto DTO: List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambiodeFaseV2DTO> ReporteControlCambiodeFaseCongelado(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                List<ControlCambiodeFaseV2DTO> result = new List<ControlCambiodeFaseV2DTO>();

                string Asesores = null;
                string CentroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    CentroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ControldeActividadesCongelado";
                var queryRespuesta = _dapper.QuerySPDapper(query, new
                {
                    Asesores,
                    CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    result = JsonConvert.DeserializeObject<List<ControlCambiodeFaseV2DTO>>(queryRespuesta);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene Reporte de Llamadas ejecutadas sin cambio de fase
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de objeto DTO : List<EjecutadasSinCambiodeFaseDTO> </returns>
        public List<EjecutadasSinCambiodeFaseDTO> ReporteEjecutadasSinCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                List<EjecutadasSinCambiodeFaseDTO> result = new List<EjecutadasSinCambiodeFaseDTO>();

                string asesores = null;
                string centroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    centroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ActividadesSinCambiodeFase";
                var queryRespuesta = _dapper.QuerySPDapper(query, new
                {
                    Asesores = asesores,
                    CentroCostos = centroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    result = JsonConvert.DeserializeObject<List<EjecutadasSinCambiodeFaseDTO>>(queryRespuesta);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 08/03/2021
        /// <summary>
        /// Obtiene Reporte de Actividades vencidas
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> Lista de IEnumerable : IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> </returns>
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ReporteActividadesVencidasporTab(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                List<ActividadVencidaporTabDTO> result = new List<ActividadVencidaporTabDTO>();
                IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> agrupado = null;

                string Asesores = null;
                string CentroCostos = null;

                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    CentroCostos = String.Join(",", filtros.Asesores);
                }

                var query = "com.SP_ActividadesVencidasPorAgendaTabNuevoModelo";
                var queryRespuesta = _dapper.QuerySPDapper(query, new
                {
                    Asesores,
                    CentroCostos,
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    result = JsonConvert.DeserializeObject<List<ActividadVencidaporTabDTO>>(queryRespuesta);
                    agrupado = result.GroupBy(x => x.Dia)
                    .Select(g => new ActividadVencidaporTabPorDiaAgrupadoDTO
                    {
                        Dia = g.Key,
                        Detalle = g.Select(y => new ActividadVencidaporTabDTO
                        {
                            Dia = y.Dia,
                            Estado = y.Estado,
                            Total = y.Total
                        }).ToList()
                    });
                }
                return agrupado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene el reporte de calidad procesamiento
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                List<ReporteCalidadProcesamientoDTO> items = new List<ReporteCalidadProcesamientoDTO>();
                var query = $@"
                    SELECT NombreFase AS DatosAsesor,
                           AVG(PromedioBeneficios) AS PromedioBeneficios, 
                           AVG(PromedioCompetidores) AS PromedioCompetidores, 
                           AVG(PromedioHistorialFinanciero) AS PromedioHistorialFinanciero, 
                           AVG(PromedioPerfil) AS PromedioPerfil, 
                           AVG(PromedioPEspecifico) AS PromedioPEspecifico, 
                           AVG(PromedioPGeneral) AS PromedioPGeneral, 
                           AVG(PromedioProblemaSeleccionados) AS PromedioProblemaSeleccionados, 
                           AVG(PromedioProblemaSolucionados) AS PromedioProblemaSolucionados
                    FROM com.V_ReporteCalidadProcesamiento
                    WHERE Fecha BETWEEN @FechaInicio AND @FechaFin  { filtro } 
                    GROUP BY NombreFase,
                             DatosAsesor,
                             IdFaseOportunidad,
                             IdPersonal";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCalidadProcesamientoDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Lista de objetos de clase ReporteActividadOcurrenciaDTO</returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrencia(int idOportunidad)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query1 = "SELECT " +
                            "IdOportunidad," +
                            "IdEstadoOcurrencia," +
                            "IdFaseOportunidadAnterior," +
                            "IdFaseActual," +
                            "FechaReal " +
                            "FROM com.V_NumeroActividadesEstadoOcurrencia where IdOportunidad = @IdOportunidad";

                var queryRespuesta1 = _dapper.QueryDapper(query1, new { IdOportunidad = idOportunidad });
                if (!queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(queryRespuesta1);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaPorIdAlumno(int IdAlumno)
        {
            try
            {
                List<ReporteActividadOcurrenciaDTO> items = new List<ReporteActividadOcurrenciaDTO>();
                var query1 = "SELECT " +
                            "IdOportunidad," +
                            "IdEstadoOcurrencia," +
                            "IdFaseOportunidadAnterior," +
                            "IdFaseActual," +
                            "FechaReal " +
                            "FROM com.V_NumeroActividadesEstadoOcurrencia where IdContacto = @IdAlumno";

                var queryRespuesta1 = _dapper.QueryDapper(query1, new { IdAlumno });
                if (!queryRespuesta1.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteActividadOcurrenciaDTO>>(queryRespuesta1);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene el reporte de programas criticos (indicadores de ventas)
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteProgramasCriticosFiltroDTO pasando los parametros necesarios para generar el reporte</param>
        /// <returns>Lista de objetos de clase ReporteProgramasCriticosDTO</returns>
        public List<ReporteProgramasCriticosDTO> ObtenerReporteProgramasCriticos(ReporteProgramasCriticosFiltroDTO filtros)
        {
            try
            {
                string grupos = null, paises = null, estadoPrograma = null, areas = null, subareas = null;
                if (filtros.Grupos.Count() > 0) grupos = String.Join(",", filtros.Grupos);
                if (filtros.Pais.Count() > 0) paises = String.Join(",", filtros.Pais);
                if (filtros.EstadoPrograma.Count() > 0) estadoPrograma = String.Join(",", filtros.EstadoPrograma);
                if (filtros.Areas.Count() > 0) areas = String.Join(",", filtros.Areas);
                if (filtros.Subareas.Count() > 0) subareas = String.Join(",", filtros.Subareas);

                List<ReporteProgramasCriticosDTO> items = new List<ReporteProgramasCriticosDTO>();
                var query = _dapper.QuerySPDapper("com.SP_ReporteProgramasCriticosNuevoModelo", new { grupos, paises, estadoPrograma, areas, subareas });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProgramasCriticosDTO>>(query);
                }

                foreach (var item in items)
                {
                    if (item.IngresoPromedioIS == 0 && item.PrecioPromedio10Descuento != 0)
                    {
                        item.PuntoEquilibrio = Convert.ToInt32(item.CostoPrograma / item.PrecioPromedio10Descuento);
                    }
                    else if (item.IngresoPromedioIS > 0)
                    {
                        item.PuntoEquilibrio = Convert.ToInt32(item.CostoPrograma / item.IngresoPromedioIS);
                    }
                    else item.PuntoEquilibrio = 0;
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de programas criticos
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteProgramasCriticosFiltroDTO pasando los parametros necesarios para generar el reporte</param>
        /// <returns>Lista de objetos de clase ReporteEstructuradoAsignacionProgramasCriticosDTO</returns>
        public List<ReporteEstructuradoAsignacionProgramasCriticosDTO> ObtenerReporteProgramasCriticosAsignacion(ReporteProgramasCriticosFiltroDTO filtros)
        {
            try
            {
                string grupos = filtros.Grupos.Any() ? string.Join(",", filtros.Grupos) : null;
                string periodos = filtros.Periodo.Any() ? string.Join(",", filtros.Periodo) : null;

                var resultadoCrudo = new List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteAsignacionDiaria_TodosNuevoModelo", new
                {
                    IdPeriodo = periodos,
                    IdGrupo = grupos
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    resultadoCrudo = JsonConvert.DeserializeObject<List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO>>(query);
                }

                var resultado = resultadoCrudo.GroupBy(g => new
                {
                    g.IdGrupoFiltroProgramaCritico,
                    g.NombreGrupoFiltroProgramaCritico,
                    g.OrdenAsesorGrupo,
                    g.IdPersonal,
                    g.NombrePersonal,
                    g.NombrePaisPersonal,
                    g.AsignacionPais
                }).Select(s => new ReporteEstructuradoAsignacionProgramasCriticosDTO
                {
                    IdGrupoFiltroProgramaCritico = s.Key.IdGrupoFiltroProgramaCritico,
                    NombreGrupoFiltroProgramaCritico = s.Key.NombreGrupoFiltroProgramaCritico,
                    OrdenAsesorGrupo = s.Key.OrdenAsesorGrupo,
                    IdPersonal = s.Key.IdPersonal,
                    NombrePersonal = s.Key.NombrePersonal,
                    NombrePaisPersonal = s.Key.NombrePaisPersonal,
                    AsignacionPais = s.Key.AsignacionPais,
                    Paises = new PaisesReporteProgramasCriticosDTO
                    {
                        CantidadPeru = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisPeru).Sum(op => op.TotalDatos),
                        CantidadColombia = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisColombia).Sum(op => op.TotalDatos),
                        CantidadBolivia = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisBolivia).Sum(op => op.TotalDatos),
                        CantidadMexico = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisMexico).Sum(op => op.TotalDatos),
                        CantidadOtros = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais != ValorEstatico.IdPaisPeru && w.IdCodigoPais != ValorEstatico.IdPaisColombia && w.IdCodigoPais != ValorEstatico.IdPaisBolivia && w.IdCodigoPais != ValorEstatico.IdPaisMexico).Sum(op => op.TotalDatos)
                    },
                    BNC_MuyAlta = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_MuyAlta),
                    BNC_Historico = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_Historico),
                    BNC_AltaMediaRemarketing = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_AltaMediaRemarketing),
                    BNC_TotalDatos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_TotalDatos),
                    RN = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.RN),
                    IT = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IT),
                    IP = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IP),
                    PF = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.PF),
                    IC = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IC),
                    Seguimiento = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.Seguimiento),
                    TotalDatos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.TotalDatos),
                    IS_M = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IS_M),
                    IS_M_Acumulado = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IS_M_Acumulado),
                    CantidadGrupoActual = resultadoCrudo.Where(w => w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdGrupoFiltroProgramaCriticoExterno == s.Key.IdGrupoFiltroProgramaCritico).Sum(op => op.TotalDatos),
                    CantidadOtrosGrupos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdGrupoFiltroProgramaCriticoExterno != s.Key.IdGrupoFiltroProgramaCritico).Sum(op => op.TotalDatos)
                }).Where(w => w.IdGrupoFiltroProgramaCritico != 0).OrderBy(o => o.IdGrupoFiltroProgramaCritico).ThenBy(tb => tb.OrdenAsesorGrupo).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los subordinados( solo Nombres) activos de un Coordinador
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerSubordinadosNombreCoordinador(int coordinador)
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, Nombre AS NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where IdJefe = @Coordinador and estado = 1 and activo = 1";
                var personalDB = _dapper.QueryDapper(_query, new { Coordinador = coordinador });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los subordinados activos de un Coordinador
        /// </summary>
        /// <param name="coordinador"></param>
        /// <returns></returns>
        public List<AsesorNombreFiltroDTO> ObtenerSubordinadosCoordinador(int coordinador)
        {
            try
            {
                List<AsesorNombreFiltroDTO> coordinadores = new List<AsesorNombreFiltroDTO>();
                var _query = "SELECT Id, NombreCompleto FROM gp.V_TPersonal_ObtenerSubordinado where IdJefe = @Coordinador and estado = 1 and activo = 1";
                var personalDB = _dapper.QueryDapper(_query, new { Coordinador = coordinador });
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]") && !personalDB.Contains("null"))
                {
                    coordinadores = JsonConvert.DeserializeObject<List<AsesorNombreFiltroDTO>>(personalDB);
                }
                return coordinadores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteControlCalidadProcesamientoDTO> ReporteControlCalidadProcesamiento(ControlCalidadProcesamientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteControlCalidadProcesamientoDTO> items = new List<ReporteControlCalidadProcesamientoDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteControlCalidadProcesamientoConsolidado", new
                {
                    asesores = filtro.Asesores,
                    grupos = filtro.Grupos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlCalidadProcesamientoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Calcula tasa de conversion por categoria de dato
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteCalidadProspectoDTO> ReporteCalidadProspecto(ReporteCalidadProspectoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteCalidadProspectoDTO> items = new List<ReporteCalidadProspectoDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteControlCalidadProspectos", new
                {
                    asesores = filtro.Asesores,
                    grupos = filtro.Grupos,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCalidadProspectoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Calcula Indicadores de llamadas que se mostrara en el reporte control Operativo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteTasaContactoIndicadoresDTO> ReporteTasaContactoConsolidadoIndicadoresPrimeraParte(string asesores, DateTime fechaInicio)
        {
            try
            {
                List<ReporteTasaContactoIndicadoresDTO> items = new List<ReporteTasaContactoIndicadoresDTO>();

                var query = _dapper.QuerySPDapper("com.ReporteTasasContacto_Parte1", new
                {
                    asesores = asesores,
                    fechaInicio = fechaInicio
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteTasaContactoIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Calcula Indicadores de llamadas que se mostrara en el reporte control Operativo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteTasaContactoIndicadoresDTO> ReporteTasaContactoConsolidadoIndicadoresSegundaParte(string asesores, DateTime fechaInicio)
        {
            try
            {
                List<ReporteTasaContactoIndicadoresDTO> items = new List<ReporteTasaContactoIndicadoresDTO>();

                var query = _dapper.QuerySPDapper("com.ReporteTasasContacto_Parte2", new
                {
                    asesores = asesores,
                    fechaInicio = fechaInicio
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteTasaContactoIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Calcula Indicadores de llamadas que se mostrara en el reporte control Operativo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteTasaContactoIndicadoresDTO> ReporteTasaContactoConsolidadoIndicadoresTerceraParte(string asesores, DateTime fechaInicio)
        {
            try
            {
                List<ReporteTasaContactoIndicadoresDTO> items = new List<ReporteTasaContactoIndicadoresDTO>();

                var query = _dapper.QuerySPDapper("com.ReporteTasasContacto_Parte3", new
                {
                    asesores = asesores,
                    fechaInicio = fechaInicio
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteTasaContactoIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Calcula Indicadores de llamadas que se mostrara en el reporte control Operativo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteTasaContactoIndicadoresDTO> ReporteTasaContactoConsolidadoIndicadoresCuartaParte(string asesores, DateTime fechaInicio)
        {
            try
            {
                List<ReporteTasaContactoIndicadoresDTO> items = new List<ReporteTasaContactoIndicadoresDTO>();

                var query = _dapper.QuerySPDapper("com.ReporteTasasContacto_Parte4", new
                {
                    asesores = asesores,
                    fechaInicio = fechaInicio
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteTasaContactoIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de indicadores para el Reporte de Control Operativo Sin llamada
        /// </summary>
        /// <returns></returns>
        public List<ReporteControlOperativoIndicadorDTO> ObtenerIndicadoresReporteControlOperativoSinLlamada()
        {
            try
            {
                List<ReporteControlOperativoIndicadorDTO> items = new List<ReporteControlOperativoIndicadorDTO>();
                var _query = string.Empty;
                _query = "Select Id, Nombre, Orden  from com.V_IndicadorReporteControlOperativo where Estado = 1 and EsIndicadorLlamada = 0";
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlOperativoIndicadorDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene la lista de indicadores para el Reporte de Control Operativo Con llamada
        /// </summary>
        /// <returns></returns>
        public List<ReporteControlOperativoIndicadorDTO> ObtenerIndicadoresReporteControlOperativo()
        {
            try
            {
                List<ReporteControlOperativoIndicadorDTO> items = new List<ReporteControlOperativoIndicadorDTO>();
                var _query = string.Empty;
                _query = "Select Id, Nombre, Orden  from com.V_IndicadorReporteControlOperativo where Estado = 1 and EsIndicadorLlamada = 1";
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlOperativoIndicadorDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidad(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadDTO> items = new List<ReporteContactabilidadDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadOportunidadLog", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadV2(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadDTO> items = new List<ReporteContactabilidadDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadDetalleOportunidadLog", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// Autor: Jashin Salazar
        /// Fecha: 17/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor de la tabla congelada diaria
        /// </summary>
        /// <returns> List<ReporteContactabilidadDTO> </returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadCongelado(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadDTO> items = new List<ReporteContactabilidadDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadCongelado", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadMinutosDTO> ObtenerReporteContactabilidadMinutos(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadMinutosDTO> items = new List<ReporteContactabilidadMinutosDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteMinutosContactabilidad", new
                {
                    asesores = filtro.Asesores,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadMinutosDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad con tiempos de contesto por asesor
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadDTO> ObtenerReporteContactabilidadDesagregado(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadDTO> items = new List<ReporteContactabilidadDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadDesagregadoOportunidadLog", new
                {
                    Asesores = filtro.Asesores,
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    Tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad por Asesor
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadAsesorDTO> ObtenerReporteContactabilidadAsesorComparativo(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadAsesorDTO> items = new List<ReporteContactabilidadAsesorDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadAsesor", new
                {
                    asesores = filtro.AsesoresComparativo,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadAsesorDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Los Valores para el reporte de contactabilidad por Asesor
        /// </summary>
        /// <returns></returns>
        public List<ReporteContactabilidadAsesorIndicadoresDTO> ObtenerReporteContactabilidadAsesorComparativoV2(ReporteContactabilidadFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteContactabilidadAsesorIndicadoresDTO> items = new List<ReporteContactabilidadAsesorIndicadoresDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadAsesorV2", new
                {
                    asesores = filtro.AsesoresComparativo,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin,
                    tipo = filtro.Tipo
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadAsesorIndicadoresDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        public ReporteRN2DTO ObtenerReporteRN2(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, string categoriaOrigen, string anio, string faseMaxima, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                ReporteRN2DTO listado = new ReporteRN2DTO();
                List<ReporteRN2JsonDTO> items = new List<ReporteRN2JsonDTO>();

                if (string.IsNullOrEmpty(area))
                {
                    area = "_";
                }
                if (string.IsNullOrEmpty(subarea))
                {
                    subarea = "_";
                }
                if (string.IsNullOrEmpty(pgeneral))
                {
                    pgeneral = "_";
                }
                if (string.IsNullOrEmpty(pespecifico))
                {
                    pespecifico = "_";
                }
                if (string.IsNullOrEmpty(modalidades))
                {
                    modalidades = "_";
                }
                if (string.IsNullOrEmpty(ciudades))
                {
                    ciudades = "_";
                }
                if (string.IsNullOrEmpty(categoriaOrigen))
                {
                    categoriaOrigen = "_";
                }
                if (string.IsNullOrEmpty(faseMaxima))
                {
                    faseMaxima = "_";
                }
                var query = _dapper.QuerySPDapper("com.SP_ObtenerCantidadOportunidadRN2", new
                {
                    area = area,
                    subArea = subarea,
                    proGeneral = pgeneral,
                    pEspecifico = pespecifico,
                    modalidades = modalidades,
                    ciudades = ciudades,
                    categoriaOrigen = categoriaOrigen,
                    anio = anio,
                    faseMaxima = faseMaxima,
                    fechaInicio = fechaInicio,
                    fechaFin = fechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRN2JsonDTO>>(query);
                }
                listado.ListaGeneral = items.GroupBy(d => d.NombreAsesor)
                            .Select(
                                g => new ReporteRN2PorAsesorDTO
                                {
                                    NombreAsesor = g.Key,
                                    Enero = g.Sum(s => s.Enero),
                                    Febrero = g.Sum(s => s.Febrero),
                                    Marzo = g.Sum(s => s.Marzo),
                                    Abril = g.Sum(s => s.Abril),
                                    Mayo = g.Sum(s => s.Mayo),
                                    Junio = g.Sum(s => s.Junio),
                                    Julio = g.Sum(s => s.Julio),
                                    Agosto = g.Sum(s => s.Agosto),
                                    Setiembre = g.Sum(s => s.Setiembre),
                                    Octubre = g.Sum(s => s.Octubre),
                                    Noviembre = g.Sum(s => s.Noviembre),
                                    Diciembre = g.Sum(s => s.Diciembre)
                                }).OrderBy(x => x.NombreAsesor).ToList();

                listado.ListaPorGrupo = (from p in items
                                         group p by new
                                         {
                                             p.IdGrupo,
                                             p.Grupo,

                                         } into g
                                         select new ReporteRN2PorGrupoDTO
                                         {
                                             IdGrupo = g.Key.IdGrupo,
                                             Grupo = g.Key.Grupo,

                                             ListaAsesores = g.Select(o => new ReporteRN2PorAsesorDTO
                                             {
                                                 NombreAsesor = o.NombreAsesor,
                                                 Enero = o.Enero,
                                                 Febrero = o.Febrero,
                                                 Marzo = o.Marzo,
                                                 Abril = o.Abril,
                                                 Mayo = o.Mayo,
                                                 Junio = o.Junio,
                                                 Julio = o.Julio,
                                                 Agosto = o.Agosto,
                                                 Setiembre = o.Setiembre,
                                                 Octubre = o.Octubre,
                                                 Noviembre = o.Noviembre,
                                                 Diciembre = o.Diciembre
                                             }).ToList()
                                         }).ToList();


                return listado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteEnvioMasivoMessengerFechaDTO> ObtenerReporteMessengerMasivoGeneral(DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ReporteEnvioMasivoMessengerFechaDTO> items = new List<ReporteEnvioMasivoMessengerFechaDTO>();
                var resultado = _dapper.QuerySPDapper("mkt.SP_ObtenerReporteMessengerMasivoGeneral", new { FechaInicio, FechaFin });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteEnvioMasivoMessengerFechaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<ReporteEnvioMasivoMessengerAsesorDTO> ObtenerReporteMessengerMasivo(DateTime FechaInicio, DateTime FechaFin, string Personal)
        {
            try
            {
                List<ReporteEnvioMasivoMessengerAsesorDTO> lista = new List<ReporteEnvioMasivoMessengerAsesorDTO>();
                List<ReporteEnvioMasivoMessengerAsesorDTO> global = new List<ReporteEnvioMasivoMessengerAsesorDTO>();

                if (string.IsNullOrEmpty(Personal))
                {
                    Personal = "_";
                }

                List<ReporteEnvioMasivoMessengerJsonDTO> items = new List<ReporteEnvioMasivoMessengerJsonDTO>();
                var resultado = _dapper.QuerySPDapper("mkt.SP_ObtenerReporteMessengerMasivo", new { FechaInicio, FechaFin, Personal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteEnvioMasivoMessengerJsonDTO>>(resultado);
                }


                global = items.GroupBy(d => new { d.Asesor, d.IdPersonal })
                            .Select(

                                g => new ReporteEnvioMasivoMessengerAsesorDTO
                                {
                                    Asesor = g.Key.Asesor,
                                    IdPersonal = g.Key.IdPersonal,
                                    Detalle = g.Select(o => new ReporteEnvioMasivoMessengerFechaDTO
                                    {
                                        Fecha = o.Fecha,
                                        RespondidosAsesor = o.RespondidosAsesor,
                                        OportunidadCreada = o.OportunidadCreada,
                                        OportunidadVentas = o.OportunidadVentas,
                                        OportunidadCerradas = o.OportunidadCerradas,
                                        OportunidadIS = o.OportunidadIS,
                                        Desuscritos = o.Desuscritos
                                    }).ToList().GroupBy(x => x.Fecha).Select(
                                        q => new ReporteEnvioMasivoMessengerFechaDTO
                                        {
                                            Fecha = q.Key,
                                            RespondidosAsesor = q.Sum(s => s.RespondidosAsesor),
                                            OportunidadCreada = q.Sum(s => s.OportunidadCreada),
                                            OportunidadVentas = q.Sum(s => s.OportunidadVentas),
                                            OportunidadCerradas = q.Sum(s => s.OportunidadCerradas),
                                            OportunidadIS = q.Sum(s => s.OportunidadIS),
                                            Desuscritos = q.Sum(s => s.Desuscritos)
                                        }).ToList()
                                }).ToList();

                lista = (from p in items
                         group p by new
                         {
                             p.Asesor,
                             p.IdPersonal,
                             p.IdFacebookPagina,
                             p.Pais

                         } into g
                         select new ReporteEnvioMasivoMessengerAsesorDTO
                         {
                             Asesor = g.Key.Asesor + " - " + g.Key.Pais,
                             IdPersonal = g.Key.IdPersonal + "_" + g.Key.IdFacebookPagina,

                             Detalle = g.Select(o => new ReporteEnvioMasivoMessengerFechaDTO
                             {
                                 Fecha = o.Fecha,
                                 RespondidosAsesor = o.RespondidosAsesor,
                                 OportunidadCreada = o.OportunidadCreada,
                                 OportunidadVentas = o.OportunidadVentas,
                                 OportunidadCerradas = o.OportunidadCerradas,
                                 OportunidadIS = o.OportunidadIS,
                                 Desuscritos = o.Desuscritos

                             }).ToList()
                         }).ToList();

                global.AddRange(lista);
                global = global.OrderBy(x => x.Asesor).ToList();

                return global;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento para conjunto lista
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Lista de objetos de clase DateTime</returns>
        public List<DateTime> ObtenerActividadesNoEjecutadas(int idOportunidad)
        {
            try
            {
                List<DateTime> actividades = new List<DateTime>();
                List<OportunidadesNoEjecutadasDTO> actividades2 = new List<OportunidadesNoEjecutadasDTO>();
                var registrosBO = _dapper.QuerySPDapper("com.SP_ObtenerOportunidadNoEjecutadaPorId", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    actividades2 = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(registrosBO);
                    foreach (var item in actividades2)
                    {
                        actividades.Add(item.FechaProgramada);
                    }
                }
                return actividades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades Rn2 cerradas automaticamente
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteSeguimientoOportunidadesRN2DTO> ObtenerReporteSeguimientoRN2(SeguimientoFiltroFinalDTO filtro)
        {
            try
            {
                List<ReporteSeguimientoOportunidadesRN2DTO> items = new List<ReporteSeguimientoOportunidadesRN2DTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteSeguimientoOportunidadRN2", new
                {
                    asesores = filtro.Asesores,
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin

                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadesRN2DTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de contactabilidad por periodo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteContactabilidadPeriodoDTO> ReporteContactabilidadPeriodo(int periodo)
        {
            try
            {
                List<ReporteContactabilidadPeriodoDTO> items = new List<ReporteContactabilidadPeriodoDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadPeriodoOportunidadLog", new
                {
                    periodo = periodo,
                    tipo = 8//Idpersonalareatrabajo de Ventas

                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadPeriodoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de contactabilidad por periodo por asesor seleccionado
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<ReporteContactabilidadPeriodoDTO> ReporteContactabilidadPeriodoAsesor(int periodo, string asesores)
        {
            try
            {
                List<ReporteContactabilidadPeriodoDTO> items = new List<ReporteContactabilidadPeriodoDTO>();

                var query = _dapper.QuerySPDapper("com.SP_ReporteContactabilidadPeriodoAsesorOportunidadLog", new
                {
                    Periodo = periodo,
                    Asesores = asesores,
                    Tipo = 8//Idpersonalareatrabajo de Ventas

                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteContactabilidadPeriodoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        ///Repositorio: ReportesRepositorio
        ///Autor: Edgar S.
        ///Fecha: 01/03/2021
        /// <summary>
        /// Obtiene Reporte de Actividades Realizadas en el Área de Operaciones
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda </param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio </param>
        /// <param name="fechaFin"> Filtro de Fecha Fin </param>
        /// <returns> lista de objetos DTO: List<ReporteRealizadaDataDTO> </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadasOperaciones(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> items = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }

                var query = _dapper.QuerySPDapper("com.SP_ReporteActividadesRealizadasOperacionesNWNuevoModelo", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino,
                    //Activo = filtro.EstadoPersonal

                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }





        ///Repositorio: ReportesRepositorio
        ///Autor: ,Edgar S.
        ///Fecha: 01/03/2021
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <returns> Lista de Objeto DTO </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> items = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }

                var query = _dapper.QuerySPDapper("com.SP_ReporteActividadesRealizadasNWNuevoModelo", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        ///Autor: Jashin Salazar
        ///Fecha: 22/03/2022
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda por Id</param>
        /// <param name="fechaInicio"> Filtro de Fecha de Inicio</param>
        /// <param name="fechaFin"> Filtro de Fecha de Fin </param>
        /// <returns> Lista de Objeto DTO </returns>
        public List<ReporteRealizadaDataDTO> ObtenerReporteActividadesRealizadasCongelado(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteRealizadaDataDTO> items = new List<ReporteRealizadaDataDTO>();
                string fasesOrigen = null;
                string fasesDestino = null;

                if (filtro.IdFasesOportunidadOrigen.Count() > 0)
                {
                    fasesOrigen = String.Join(",", filtro.IdFasesOportunidadOrigen);
                }
                if (filtro.IdFasesOportunidadDestino.Count() > 0)
                {
                    fasesDestino = String.Join(",", filtro.IdFasesOportunidadDestino);
                }

                var query = _dapper.QuerySPDapper("com.SP_ReporteActividadesRealizadasNWNuevoModeloCongelado", new
                {
                    IdAsesor = filtro.IdAsesor,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdCentroCosto = filtro.IdCentroCosto,
                    IdAlumno = filtro.IdAlumno,
                    IdTipoDato = filtro.IdTipoDato,
                    IdCategoriaOrigen = filtro.IdTipoCategoriaOrigen,
                    IdProbabilidadActual = filtro.IdProbabilidadActual,
                    IdEstadoOcurrencia = filtro.IdEstadoOcurrencia,
                    FasesOrigen = fasesOrigen,
                    FasesDestino = fasesDestino
                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteRealizadaDataDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de cantidad de mensajes enviados por whatsapp
        /// </summary>
        /// <returns></returns>
        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtro)
        {
            try
            {
                List<ObtenerReporteMensajesWhatsAppPorTipoDTO> items = new List<ObtenerReporteMensajesWhatsAppPorTipoDTO>();

                var query = _dapper.QuerySPDapper("mkt.SP_ReporteMensajesWhatsAppPlantillas", new
                {
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin

                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ObtenerReporteMensajesWhatsAppPorTipoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene Reportes de Mensajes Enviados Masivamente
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivos(ReporteMensajesWhatsAppFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapper.QuerySPDapper("mkt.SP_GenerarReporteWhatsAppMasivo", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin

                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene Reportes de Mensajes Enviados Masivamente por determinada Area (Operaciones, Marketing)
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosPorArea(ReporteMensajesWhatsAppPorAreaFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapper.QuerySPDapper("mkt.SP_GenerarReporteWhatsAppMasivoPorArea", new
                {
                    FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0),
                    FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59),
                    Area = filtro.IdArea
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        public List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO> GenerarReporteDetallesAsignacionCoordinadora(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO> items = new List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAsignacionCoordinadoras", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteControlContactoTelefonicoDTO> GenerarReporteControlContactoTelefonico(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteControlContactoTelefonicoDTO> items = new List<ReporteControlContactoTelefonicoDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteControlContactoTelefonico", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlContactoTelefonicoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> GenerarReporteEstadoAlumnosPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> items = new List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAvanceAcademicoPagos", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteIndicadoresOperativosDTO> GenerarReporteIndicadoresOperativos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteIndicadoresOperativosDTO> items = new List<ReporteIndicadoresOperativosDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosAtencionClienteOportunidadLog", new
                {
                    Valor = filtro,
                    FechaInicio = filtros2.FechaInicio,
                    FechaFin = filtros2.FechaFin
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIndicadoresOperativosDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadora(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> items = new List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteIndicadoresOperativosPorCoordinadoraAtencionClienteOportunidadLog", new
                {
                    Valor = filtro,
                    FechaInicio = filtros2.FechaInicio,
                    FechaFin = filtros2.FechaFin
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> GenerarReporteEstadoAlumnos2(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteAvanceAcademicoPresencialOnlineDTO> items = new List<ReporteAvanceAcademicoPresencialOnlineDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAvanceAcademicoAonline", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoPresencialOnlineDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> items = new List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAvanceAcademicoVSPagosAonline", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> GenerarReporteEstadoAlumnosPagosAtrasados(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteAvanceAcademicoAlumnosPagosAtrasados> items = new List<ReporteAvanceAcademicoAlumnosPagosAtrasados>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAlumnosConPagosAtrasados", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteAvanceAcademicoAlumnosPagosAtrasados>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO> GenerarReporteDetallesAsignacionCoordinadoraEstados(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO> items = new List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO>();

                var query = _dapper.QuerySPDapper("ope.SP_ReporteAsignacionCoordinadorasEstados", new
                {
                    valor = filtro,
                    fechainiciomatricula = filtros2.FechaInicioMatricula,
                    fechafinmatricula = filtros2.FechaFinMatricula,
                    fechainicioasignacion = filtros2.FechaInicioAsignacion,
                    fechafinasignacion = filtros2.FechaFinAsignacion,
                    tipofiltrofecha = filtros2.CheckFecha
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapper.QuerySPDapper("mkt.SP_ObtenerWhatsAppMasivoConjuntoLista", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdPersonal = filtro.IdPersonal,
                    IdPais = filtro.IdPais
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtiene la lista de furs que no tengan asociado ningun documento de pago y esten aprobados por jefe de finanzas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        public List<FurPorPagarDTO> ObtenerFurPorPagarByFecha(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            try
            {
                var _query = "";
                var FurDB = "";
                var camposTabla = "Empresa,Sede,Area, TipoPedido,CodigoFur,ProductoServicio,Cantidad,Unidades,PrecioUnitario,MonedaProveedor,Descripcion,CentroCosto,Atipico,Rubro,NroCuenta,Cuenta,UsuarioCreacion,UsuarioModificacion,RUC,Proveedor,MonedaReal,MontoAPagar,MontoAPagarDolares,FechaProgramada,MesProgramado,Estado ";

                List<FurPorPagarDTO> listaFur = new List<FurPorPagarDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursPorPagar order by FechaProgramada desc";
                    FurDB = _dapper.QueryDapper(_query, new { });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursPorPagar WHERE Convert(Date,FechaProgramada)>=@fechaInicial and Convert(Date, FechaProgramada)  <= @fechaFinal Order By Convert(Date, FechaProgramada ) Desc";
                    FurDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    FurDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(FurDB) && !FurDB.Contains("[]"))
                {
                    listaFur = JsonConvert.DeserializeObject<List<FurPorPagarDTO>>(FurDB);
                }

                return listaFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte totalizado de acuero a las Cuentas Contables
        /// </summary>
        /// <param name="año"></param>
        /// <param name="empresa"></param>
        /// <returns></returns>
        public List<ReportePagoPorCuentaContableDTO> ObtenerReportePagoPorCuentaContable(string año, string empresa)
        {
            try
            {
                var _query = "";
                var pagoCuentaDB = "";
                var camposTabla = "Empresa,NumeroCuenta,Rubro,Descripcion,AñoPago,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Setiembre,Octubre,Noviembre,Diciembre ";

                List<ReportePagoPorCuentaContableDTO> listaPagoCuentaContable = new List<ReportePagoPorCuentaContableDTO>();
                if (año == null && empresa == null)
                {
                    _query = "SELECT " + camposTabla + " FROM Fin.V_ReportePagosPorCuenta order by AñoPago desc";
                    pagoCuentaDB = _dapper.QueryDapper(_query, new { });
                }
                else if (año != null && empresa != null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReportePagosPorCuenta WHERE AñoPago like concat('%',@año,'%') and  Empresa like concat('%',@empresa,'%') Order By AñoPago Desc";
                    pagoCuentaDB = _dapper.QueryDapper(_query, new { año = año, empresa = empresa });

                }
                else if (año == null && empresa != null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReportePagosPorCuenta WHERE Empresa like concat('%',@empresa,'%') Order By AñoPago Desc";
                    pagoCuentaDB = _dapper.QueryDapper(_query, new { empresa = empresa });
                }
                else if (año != null && empresa == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReportePagosPorCuenta WHERE AñoPago like concat('%',@año,'%') Order By AñoPago Desc";
                    pagoCuentaDB = _dapper.QueryDapper(_query, new { año = año });
                }
                if (!string.IsNullOrEmpty(pagoCuentaDB) && !pagoCuentaDB.Contains("[]"))
                {
                    listaPagoCuentaContable = JsonConvert.DeserializeObject<List<ReportePagoPorCuentaContableDTO>>(pagoCuentaDB);
                }

                return listaPagoCuentaContable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los furs Aprobados por Jefe de Finanzas que no tengan ningun odcumento de pago y que sean pagos de Gasto Inmediato , ninguna compra a credito
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="IdPersonalAreaTrabajo"></param>
        /// <returns></returns>
        public List<ReporteFurAprobadoDTO> ObtenerReporteFurAprobado(DateTime? FechaInicial, DateTime? FechaFinal, int? IdPersonalAreaTrabajo)
        {
            try
            {
                var _query = "";
                var FurDB = "";
                var camposTabla = "IdFur,Empresa,Sede,Area,TipoPedido,CodigoFur,Semana,RUC,Proveedor,ProductoServicio,Cantidad,Unidad,MonedaReal,PagoOrigen,PagoDolares,Descripcion,FechaLimite,CentroCosto,Observaciones,Rubro,NroCuenta,Cuenta,FechaCreacion,UsuarioSolicitud,FaseAprobacion,FechaAprobacionJefeFinanzas ";

                List<ReporteFurAprobadoDTO> listaFur = new List<ReporteFurAprobadoDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue && IdPersonalAreaTrabajo == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursAprobados order by FechaAprobacionJefeFinanzas desc";
                    FurDB = _dapper.QueryDapper(_query, new { });
                }
                else if (!FechaFinal.HasValue && !FechaFinal.HasValue && IdPersonalAreaTrabajo != null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursAprobados where IdAreaTrabajo=@idAreaTrabajo order by FechaAprobacionJefeFinanzas desc";
                    FurDB = _dapper.QueryDapper(_query, new { });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue && IdPersonalAreaTrabajo != null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursAprobados WHERE Convert(Date,FechaAprobacionJefeFinanzas)>=@fechaInicial and Convert(Date, FechaAprobacionJefeFinanzas)  <= @fechaFinal and  IdAreaTrabajo=@idAreaTrabajo  Order By Convert(Date, FechaAprobacionJefeFinanzas ) Desc";
                    FurDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date, idAreaTrabajo = IdPersonalAreaTrabajo });

                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue && IdPersonalAreaTrabajo == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteFursAprobados WHERE Convert(Date,FechaAprobacionJefeFinanzas)>=@fechaInicial and Convert(Date, FechaAprobacionJefeFinanzas)  <= @fechaFinal  Order By Convert(Date, FechaAprobacionJefeFinanzas ) Desc";
                    FurDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date, idAreaTrabajo = IdPersonalAreaTrabajo });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    FurDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(FurDB) && !FurDB.Contains("[]"))
                {
                    listaFur = JsonConvert.DeserializeObject<List<ReporteFurAprobadoDTO>>(FurDB);
                }

                return listaFur;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de todos los furs 
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <returns></returns>
        public List<CuentasPorPagarDTO> ObtenerCuentasPorPagarByFecha(DateTime? FechaInicial, DateTime? FechaFinal)
        {
            try
            {
                var _query = "";
                var FurDB = "";
                var camposTabla = "Id,Empresa,Sede,Area,TipoPedido,CodigoFur,ProductoServicio,Cantidad,Unidades,PrecioUnitario,MonedaFur,Descripcion,CentroCosto,Curso,Programa,Atipico,Rubro,NroCuenta,Cuenta,UsuarioCreacion,UsuarioModificacion,TipoDocumento,NroDoc,Proveedor,TipoComprobante,NumComprobante,MonedaFurComprobante,MontoporPagar,TotalDolares,FechaEmisionComprobante,MesdeEmision,FechaVencimientoComprobante,MesdeVcto,Estado,Diferido,Anterior,FechaProgramacion,MesProgramacion,IdComprobante ";

                List<CuentasPorPagarDTO> listaCuentasPorPagar = new List<CuentasPorPagarDTO>();
                if (!FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteCuentasPorPagar order by FechaProgramacion desc";
                    FurDB = _dapper.QueryDapper(_query, new { });
                }
                else if (FechaFinal.HasValue && FechaFinal.HasValue)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ReporteCuentasPorPagar WHERE Convert(Date,FechaProgramacion)>=@fechaInicial and Convert(Date, FechaProgramacion)  <= @fechaFinal Order By Convert(Date, FechaProgramacion ) Desc";
                    FurDB = _dapper.QueryDapper(_query, new { fechaInicial = FechaInicial.Value.Date, fechaFinal = FechaFinal.Value.Date });

                }
                else if (FechaFinal.HasValue && !FechaFinal.HasValue)
                {
                    FechaFinal = DateTime.Now;
                    FurDB = _dapper.QueryDapper(_query, null);
                }
                if (!string.IsNullOrEmpty(FurDB) && !FurDB.Contains("[]"))
                {
                    listaCuentasPorPagar = JsonConvert.DeserializeObject<List<CuentasPorPagarDTO>>(FurDB);
                }

                return listaCuentasPorPagar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<ReportePagosDTO> ObtenerReportePagos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReportePagosDTO> items = new List<ReportePagosDTO>();
                var _query = string.Empty;
                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                _query = "Select Id, Empresa,EmpresaFur, Sede, Area, TipoPedido, CodigoFur, ProductoServicio, Cantidad, Unidades, PrecioUnitario, Moneda, Descripcion, CentroCosto, Curso, Programa, Rubro, NroCuenta," +
                    " Cuenta, UsuarioCreacion, UsuarioModificacion, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaPago, MontoPagado, TotalDolares, " +
                    "NumCuenta, NumRecibo, TipoPago, FechaEmision, FechaVencimiento, FechaPagoBanco, MesPagoBanco, Anterior, MontoProgramado, MontoNoProgramado  from fin.V_ReporteFurPago" +
                    " where FechaBanco BETWEEN @FechaInicio AND @FechaFin AND FurCodigo <> '' AND (FurAntiguo = '0' OR FurAntiguo IS NULL) AND Estado = 1 AND EstadoPago = 1 ORDER BY FechaBanco DESC";
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio, FechaFin });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePagosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los documentos pendientes (comprobantes) de pago
        /// </summary>
        /// <returns></returns>
        public List<ReporteDocumentosPendientesPagoDTO> ObtenerReporteDocumentosPendientesPago()
        {
            try
            {
                List<ReporteDocumentosPendientesPagoDTO> items = new List<ReporteDocumentosPendientesPagoDTO>();
                var _query = string.Empty;
                _query = "Select Id, Empresa,EmpresaFur, Sede, Area, TipoPedido, CodigoFur, ProductoServicio, Cantidad, Unidades, PrecioUnitario, Moneda, Descripcion, CentroCosto, Curso, Programa, Atipico, Rubro, NroCuenta," +
                    " Cuenta, UsuarioCreacion, UsuarioModificacion, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaComprobante, MontoaPagar,TotalDolaresAsociado,MonedaPago,ACuenta," +
                    "TotalDolaresACuenta, TotalDolaresPendiente, FechaEmisionComprobante, MesdeEmision, FechaVencimientoComprobante, MesdeVcto, Estado, Anterior from fin.V_ReporteDocumentosPendientesPagos " +
                    "ORDER BY FurFechaEmision DESC";
                var respuestaDapper = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteDocumentosPendientesPagoDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene toda la data de los campos para el Reporte de Presupuesto 
        /// </summary>
        /// <returns></returns>
        public List<ReportePresupuestoDTO> ObtenerReportePresupuestoFinanzas(ReportePresupuestoFiltroDTO filtros)
        {
            try
            {
                string periodoProgramacionOriginal = null, periodoProgramacionActual = null, periodoFechaLimiteFur = null, periodoFechaCobroBanco = null;
                string sede = null, cuenta = null, usuarioCreacion = null, rubro = null, tipoPedido = null, centroCosto = null, proveedor = null, codigoFur = null, faseFur = null, subFaseFur = null, estadoComprobante = null;
                if (filtros.sede > 0) sede = String.Join(",", filtros.sede);
                if (filtros.cuenta > 0) cuenta = String.Join(",", filtros.cuenta);
                if (filtros.usuarioCreacion > 0) usuarioCreacion = String.Join(",", filtros.usuarioCreacion);
                if (filtros.rubro > 0) rubro = String.Join(",", filtros.rubro);
                if (filtros.tipoPedido > 0) tipoPedido = String.Join(",", filtros.tipoPedido);
                if (filtros.centroCosto > 0) centroCosto = String.Join(",", filtros.centroCosto);
                if (filtros.proveedor > 0) proveedor = String.Join(",", filtros.proveedor);
                if (filtros.codigoFur > 0) codigoFur = String.Join(",", filtros.codigoFur);
                if (filtros.faseFur > 0) faseFur = String.Join(",", filtros.faseFur);
                if (filtros.subFaseFur > 0) subFaseFur = String.Join(",", filtros.subFaseFur);
                if (filtros.estadoComprobante > 0) estadoComprobante = String.Join(",", filtros.estadoComprobante);
                if (filtros.periodoProgramacionOriginal.Count() > 0) periodoProgramacionOriginal = String.Join(",", filtros.periodoProgramacionOriginal);
                if (filtros.periodoProgramacionActual.Count() > 0) periodoProgramacionActual = String.Join(",", filtros.periodoProgramacionActual);
                if (filtros.periodoFechaLimiteFur.Count() > 0) periodoFechaLimiteFur = String.Join(",", filtros.periodoFechaLimiteFur);
                if (filtros.periodoFechaCobroBanco.Count() > 0) periodoFechaCobroBanco = String.Join(",", filtros.periodoFechaCobroBanco);

                List<ReportePresupuestoDTO> items = new List<ReportePresupuestoDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReportePresupuesto", new { sede, cuenta, usuarioCreacion, rubro, tipoPedido, centroCosto, proveedor, codigoFur, faseFur, subFaseFur, estadoComprobante, periodoProgramacionOriginal, periodoProgramacionActual, periodoFechaLimiteFur, periodoFechaCobroBanco });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePresupuestoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los comprobantes asociados o sin asociar a un determinado fur
        /// </summary>
        /// <returns></returns>
        public List<ReporteComprobantesDTO> ObtenerReporteComprobantes(int? idTipoAsociado)
        {
            try
            {
                if (idTipoAsociado != 0)
                {
                    List<ReporteComprobantesDTO> items = new List<ReporteComprobantesDTO>();
                    var _query = string.Empty;
                    _query = "Select Id, Empresa, Sede, Area, TipoPedido, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaComprobante, MontoTotal," +
                        "FechaEmision, MesFechaEmision, FechaVencimiento, MesFechaProgramacion, CodigoFur, MontoAsociado,MontoPendiente from fin.V_ReporteComprobantes" +
                        " where IdTipoAsociado = @idTipoAsociado AND Estado = 1 ORDER BY FechaComprobante DESC";
                    var respuestaDapper = _dapper.QueryDapper(_query, new { idTipoAsociado });
                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<ReporteComprobantesDTO>>(respuestaDapper);
                    }

                    return items;
                }
                else
                {
                    List<ReporteComprobantesDTO> items = new List<ReporteComprobantesDTO>();
                    var _query = string.Empty;
                    _query = "Select Id, Empresa, Sede, Area, TipoPedido, TipoDocumento, NroDoc, Proveedor, TipoComprobante, NumComprobante, MonedaComprobante, MontoTotal," +
                        "FechaEmision, MesFechaEmision, FechaVencimiento, MesFechaProgramacion, CodigoFur, MontoAsociado,MontoPendiente from fin.V_ReporteComprobantes" +
                        " where Estado = 1 ORDER BY FechaComprobante DESC";
                    var respuestaDapper = _dapper.QueryDapper(_query, new { idTipoAsociado });
                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<ReporteComprobantesDTO>>(respuestaDapper);
                    }

                    return items;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnterior(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 0, 0, 0);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsideraDiasHabilesLV, ConsideraDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosSinDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosterior(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsideraDiasHabilesLV, ConsideraDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosSinDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosAnteriorConDeposito(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 0, 0, 0);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro,FechaDepositaron, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosConDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosPosteriorConDeposito(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro,FechaDepositaron, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosIngresosConDepositos where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosIngresosGestionCobranza(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosGestionCobranza where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los pagos de los fur
        /// </summary>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerPagosTasasAcademicas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();

                DateTime FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReportePagosTasasAcademicas where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<PagosIngresosDTO> ObtenerReportePagosIngresos(ReportePagoFiltroDTO FiltroPagoIngresos)
        {
            try
            {
                DateTime FechaInicio = new DateTime(FiltroPagoIngresos.FechaInicio.Year, FiltroPagoIngresos.FechaInicio.Month, FiltroPagoIngresos.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(FiltroPagoIngresos.FechaFin.Year, FiltroPagoIngresos.FechaFin.Month, FiltroPagoIngresos.FechaFin.Day, 23, 59, 59);

                string IdCentroCosto = null, IdAlumno = null, IdMatriculaCabecera = null, IdFormaPago = null, IdCiudad = null, IdModalidad = null;
                if (FiltroPagoIngresos.IdCentroCosto > 0) IdCentroCosto = String.Join(",", FiltroPagoIngresos.IdCentroCosto);
                if (FiltroPagoIngresos.IdAlumno > 0) IdAlumno = String.Join(",", FiltroPagoIngresos.IdAlumno);
                if (FiltroPagoIngresos.IdMatriculaCabecera > 0) IdMatriculaCabecera = String.Join(",", FiltroPagoIngresos.IdMatriculaCabecera);
                if (FiltroPagoIngresos.IdFormaPago > 0) IdFormaPago = String.Join(",", FiltroPagoIngresos.IdFormaPago);
                if (FiltroPagoIngresos.IdCiudad > 0) IdCiudad = String.Join(",", FiltroPagoIngresos.IdCiudad);
                if (FiltroPagoIngresos.IdModalidad >= 0) IdModalidad = String.Join(",", FiltroPagoIngresos.IdModalidad);

                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReportePagosIngresos", new { FechaInicio, FechaFin, IdCentroCosto, IdAlumno, IdMatriculaCabecera, IdFormaPago, IdCiudad, IdModalidad });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Metodo para actualizar CronogramaVersionFinal del dia actual
        /// </summary>
        /// <returns></returns>
        public bool ActualizarCronogramaVersionFinal()
        {
            try
            {


                bool items = false;
                var query = _dapper.QuerySPDapper("fin.SP_InsertarTablaCronogramaVersionFinal", new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = true;
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte de flujos 
        /// </summary>
        /// <returns></returns>
        public List<ReporteFlujoDTO> ObtenerReporteFlujos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {

                DateTime _FechaIni = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day, 23, 59, 59);

                List<ReporteFlujoDTO> items = new List<ReporteFlujoDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReporteFlujo", new { _FechaIni, _FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteFlujoDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte de cronogram originales 
        /// </summary>
        /// <returns></returns>
        public List<ReporteCronogramaOriginalDTO> ObtenerReporteCronogramaOriginales(DateTime fechainicio, DateTime fechafin, string tipos, string coordinadoras)
        {
            try
            {


                List<ReporteCronogramaOriginalDTO> items = new List<ReporteCronogramaOriginalDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_CronogramasOriginales_Bryan", new { fechainicio, fechafin, tipos, coordinadoras });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCronogramaOriginalDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el reporte de pendientes
        /// </summary>
        /// <returns></returns>
        public List<ReportePendientePeriodoDTO> ObtenerReportePendiente(ReportePendienteFiltroDTO FiltroPendiente)
        {
            try
            {
                DateTime FechaInicio = new DateTime(FiltroPendiente.PeriodoInicio.Year, FiltroPendiente.PeriodoInicio.Month, FiltroPendiente.PeriodoInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(FiltroPendiente.PeriodoFin.Year, FiltroPendiente.PeriodoFin.Month, FiltroPendiente.PeriodoFin.Day, 23, 59, 59);
                string PeriodoCierre = null;
                if (FiltroPendiente.PeriodoCierre.Count() > 0) PeriodoCierre = String.Join(",", FiltroPendiente.PeriodoCierre);
                string Modalidad = null, Coordinadora = null;
                if (FiltroPendiente.Modalidad.Count() >= 0) Modalidad = String.Join(",", FiltroPendiente.Modalidad);
                if (FiltroPendiente.Coordinadora.Count() > 0) Coordinadora = String.Join(",", FiltroPendiente.Coordinadora);

                List<ReportePendientePeriodoDTO> items = new List<ReportePendientePeriodoDTO>();
                var query = _dapper.QuerySPDapper("[fin].[SP_ReportePendientesPeriodo]", new { FechaInicio, FechaFin, tipos = Modalidad, Coordinadoras = Coordinadora });

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

        /// <summary>
        /// Obtiene el reporte de pendiente de cobro por coordinador
        /// </summary>
        /// <returns></returns>
        public List<ReportePendienteCobroCoordinadorDTO> ObtenerReportePendienteCobroPorCoordinadora(ReportePendienteCobroPorCoordinadorDTO FiltroPendientePorCobro)
        {
            try
            {

                DateTime _FechaIni = new DateTime(FiltroPendientePorCobro.PeriodoInicio.Year, FiltroPendientePorCobro.PeriodoInicio.Month, FiltroPendientePorCobro.PeriodoInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroPendientePorCobro.PeriodoFin.Year, FiltroPendientePorCobro.PeriodoFin.Month, FiltroPendientePorCobro.PeriodoFin.Day, 23, 59, 59);

                string Coordinadora = null, Modalidad = null, AreaCapacitacion = null, SubAreaCapacitacion = null, ProgramaGeneral = null, CentroCosto = null, Sede = null;
                if (FiltroPendientePorCobro.Coordinadora.Count() > 0) Coordinadora = String.Join(",", FiltroPendientePorCobro.Coordinadora);
                if (FiltroPendientePorCobro.Modalidad.Count() > 0) Modalidad = String.Join(",", FiltroPendientePorCobro.Modalidad);
                if (FiltroPendientePorCobro.AreaCapacitacion.Count() > 0) AreaCapacitacion = String.Join(",", FiltroPendientePorCobro.AreaCapacitacion);
                if (FiltroPendientePorCobro.SubAreaCapacitacion.Count() > 0) SubAreaCapacitacion = String.Join(",", FiltroPendientePorCobro.SubAreaCapacitacion);
                if (FiltroPendientePorCobro.ProgramaGeneral.Count() > 0) ProgramaGeneral = String.Join(",", FiltroPendientePorCobro.ProgramaGeneral);
                if (FiltroPendientePorCobro.CentroCosto.Count() > 0) CentroCosto = String.Join(",", FiltroPendientePorCobro.CentroCosto);
                if (FiltroPendientePorCobro.Sede.Count() > 0) Sede = String.Join(",", FiltroPendientePorCobro.Sede);

                List<ReportePendienteCobroCoordinadorDTO> items = new List<ReportePendienteCobroCoordinadorDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReportePendienteCobroCoordinadora", new { _FechaIni, _FechaFin, coordinadoras = Coordinadora, Area = AreaCapacitacion, Subarea = SubAreaCapacitacion, Modalidad, ProgramaGeneral, CentroCosto, Sede });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReportePendienteCobroCoordinadorDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Repositorio: ReportesRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// <summary>
        /// Obtiene los datos para generar el reporte de seguimiento de oportunidades
        /// </summary>
        /// <param name="filtros"> Filtro de Búsqueda </param>
        /// <returns> ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ObtenerReporteTasaContacto_prueba(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<PreReporteTasaContactoDTO> datos = new List<PreReporteTasaContactoDTO>();
                ReporteTasaContactoDTO item = new ReporteTasaContactoDTO();
                var _queryTotal = string.Empty;
                var _queryEjecutadas = string.Empty;
                var _queryEjecutadasConLlamadas = string.Empty;

                var ejecutadaTotal = 1;
                var ejecutadaEjecutada = 2;
                var ejecutadaConLlamada = 3;

                _queryEjecutadasConLlamadas = @" 
                                SELECT COUNT(*) AS Total, @ejecutadaTotal AS Valor
                                FROM com.V_ReporteTasaContacto
                                WHERE EstadoOcurrencia = 1
                                      AND EstadoOportunidad = 1
                                      AND EstadoActividad = 1
                                      AND (IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                           OR IdEstadoOcurrencia = @IdEstadoOcurrenciaNoEjecutado)
                                      AND IdFaseOportunidad != @IdFaseOportunidadE
                                      AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                                      AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2')
                            " + filtros.Filtro;

                _queryEjecutadasConLlamadas += @"

                                UNION
                                SELECT COUNT(*) AS Total, @ejecutadaEjecutada AS Valor
                                FROM com.V_ReporteTasaContacto
                                WHERE EstadoOcurrencia = 1
                                      AND EstadoOportunidad = 1
                                      AND EstadoActividad = 1
                                      AND (IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                           OR IdEstadoOcurrencia = @IdEstadoOcurrenciaNoEjecutado)
                                      AND IdFaseOportunidad != @IdFaseOportunidadE
                                      AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                                      AND IdEstadoOcurrencia = @IdEstadoOcurrencia
                                      AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2')
                                " + filtros.Filtro;

                _queryEjecutadasConLlamadas += @"
                                
                                UNION
                                SELECT COUNT(*) AS Total, @ejecutadaConLlamada AS Valor
                                FROM com.V_ReporteTasaContactoLlamada
                                WHERE EstadoOcurrencia = 1
                                        AND EstadoOportunidad = 1
                                        AND EstadoActividad = 1
                                        AND (IdEstadoOcurrencia = @IdEstadoOcurrenciaEjecutado
                                            OR IdEstadoOcurrencia = @IdEstadoOcurrenciaNoEjecutado)
                                        AND IdFaseOportunidad != @IdFaseOportunidadE
                                        AND FechaReal BETWEEN @FechaInicio AND @FechaFin
                                        AND IdEstadoOcurrencia = @IdEstadoOcurrencia
                                        AND IdLlamada = 1
                                        AND UsuarioModificacion NOT IN('UsuarioBic', 'UsuarioFaseX', 'UsuarioOM', 'system duplicado', 'sys duplicadoIP', 'CerradoBIC', 'AutomatizacionRN2')
                                " + filtros.Filtro;

                //var respuestaDapperTotal = _dapper.FirstOrDefault(_queryTotal, new
                //{
                //    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                //    ValorEstatico.IdEstadoOcurrenciaNoEjecutado,
                //    ValorEstatico.IdFaseOportunidadE,
                //    filtros.FechaInicio,
                //    filtros.FechaFin
                //});

                //var respuestaDapperEjecutadas = _dapper.FirstOrDefault(_queryEjecutadas, new
                //{
                //    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                //    ValorEstatico.IdEstadoOcurrenciaNoEjecutado,
                //    ValorEstatico.IdFaseOportunidadE,
                //    IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaEjecutado,
                //    filtros.FechaInicio,
                //    filtros.FechaFin
                //});



                var respuestaDapperEjecutadasLlamada = _dapper.QueryDapper(_queryEjecutadasConLlamadas, new
                {
                    ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    ValorEstatico.IdEstadoOcurrenciaNoEjecutado,
                    ValorEstatico.IdFaseOportunidadE,
                    IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaEjecutado,
                    filtros.FechaInicio,
                    filtros.FechaFin,
                    ejecutadaTotal,
                    ejecutadaEjecutada,
                    ejecutadaConLlamada
                });

                if (!string.IsNullOrEmpty(respuestaDapperEjecutadasLlamada) && !respuestaDapperEjecutadasLlamada.Contains("[]"))
                {
                    var datosTotal = JsonConvert.DeserializeObject<List<ResultadoTasaContactoDTO>>(respuestaDapperEjecutadasLlamada);
                    //var datosEjecutadas = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapperEjecutadasLlamada);
                    //var datosEjecutadasLlamada = JsonConvert.DeserializeObject<Dictionary<string, int>>(respuestaDapperEjecutadasLlamada);

                    item.TotalLlamadas = datosTotal.Where(x => x.Valor == ejecutadaTotal).FirstOrDefault().Total;
                    item.TotalLlamadasEjecutadas = datosTotal.Where(x => x.Valor == ejecutadaEjecutada).FirstOrDefault().Total;
                    item.TotalLlamadasEjecutadasConLlamada = datosTotal.Where(x => x.Valor == ejecutadaConLlamada).FirstOrDefault().Total;
                }

                return item;
            }
            catch (Exception e)
            {
                throw e;
            }

        }










        /// <summary>
        /// Obtiene el color del semaforo en sentinel
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public string obtenerColorSemaforo(string color)
        {
            try
            {
                if (color != null)
                {
                    color = color.Trim();
                    switch (color)
                    {
                        case "1":
                            return "ROJO";
                        case "2":
                            return "AMARILLO";
                        case "3":
                            return "GRIS";
                        case "4":
                            return "VERDE";
                        default:
                            return "SIN SENTINEL";
                    }
                }
                else
                {
                    return "SIN SENTINEL";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 28/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de control documentos
        /// </summary>
        /// <returns></returns>
        public List<ReporteControlDocumentosDTO> ObtenerReporteControlDocumentos(ReporteControlDocumentosFiltroDTO filtroControlDocumentos)
        {
            try
            {
                DateTime? _FechaIni = filtroControlDocumentos.FechaInicio;
                DateTime? _FechaFin = filtroControlDocumentos.FechaFin;

                if (filtroControlDocumentos.FechaInicio != null && filtroControlDocumentos.FechaFin != null)
                {
                    _FechaIni = new DateTime(filtroControlDocumentos.FechaInicio.Value.Year, filtroControlDocumentos.FechaInicio.Value.Month, filtroControlDocumentos.FechaInicio.Value.Day, 0, 0, 0);
                    _FechaFin = new DateTime(filtroControlDocumentos.FechaFin.Value.Year, filtroControlDocumentos.FechaFin.Value.Month, filtroControlDocumentos.FechaFin.Value.Day, 23, 59, 59);
                }

                string IdEstadoPagoMatricula = null;
                if (filtroControlDocumentos.IdEstadoPagoMatricula.Count() > 0) IdEstadoPagoMatricula = String.Join(",", filtroControlDocumentos.IdEstadoPagoMatricula);

                List<ReporteControlDocumentosDTO> items = new List<ReporteControlDocumentosDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReporteControlDocumentos", new { fechainicio = _FechaIni, fechafin = _FechaFin, filtroControlDocumentos.IdCoordinador, filtroControlDocumentos.IdAsesor, filtroControlDocumentos.IdCentroCosto, filtroControlDocumentos.IdAlumno, filtroControlDocumentos.IdMatricula, IdEstado = IdEstadoPagoMatricula });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteControlDocumentosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de documentos
        /// </summary>
        /// <returns></returns>
        public List<ReporteDocumentosDTO> ObtenerReporteDocumentos(ReporteDocumentosFiltroDTO FiltroControlDocumentos)
        {
            try
            {
                DateTime _FechaIni = FiltroControlDocumentos.FechaInicio;
                DateTime _FechaFin = FiltroControlDocumentos.FechaFin;
                _FechaIni = new DateTime(FiltroControlDocumentos.FechaInicio.Year, FiltroControlDocumentos.FechaInicio.Month, FiltroControlDocumentos.FechaInicio.Day, 0, 0, 0);
                _FechaFin = new DateTime(FiltroControlDocumentos.FechaFin.Year, FiltroControlDocumentos.FechaFin.Month, FiltroControlDocumentos.FechaFin.Day, 23, 59, 59);


                List<ReporteDocumentosDTO> items = new List<ReporteDocumentosDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReporteDocumentos", new { FechaInicio = _FechaIni, FechaFin = _FechaFin });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteDocumentosDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de devoluciones
        /// </summary>
        /// <returns></returns>
        public List<ReporteDevolucionesDTO> ObtenerReporteDevoluciones(ReporteDevolucionesFiltroDTO FiltroDevoluciones)
        {
            try
            {
                DateTime _FechaIni = new DateTime(FiltroDevoluciones.FechaInicio.Year, FiltroDevoluciones.FechaInicio.Month, FiltroDevoluciones.FechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroDevoluciones.FechaFin.Year, FiltroDevoluciones.FechaFin.Month, FiltroDevoluciones.FechaFin.Day, 23, 59, 59);

                DateTime? _FechaIniCronograma = FiltroDevoluciones.FechaInicioCronograma;
                DateTime? _FechaFinCronograma = FiltroDevoluciones.FechaFinCronograma;

                if (FiltroDevoluciones.FechaInicioCronograma != null && FiltroDevoluciones.FechaFinCronograma != null)
                {
                    _FechaIniCronograma = new DateTime(FiltroDevoluciones.FechaInicioCronograma.Value.Year, FiltroDevoluciones.FechaInicioCronograma.Value.Month, FiltroDevoluciones.FechaInicioCronograma.Value.Day, 0, 0, 0);
                    _FechaFinCronograma = new DateTime(FiltroDevoluciones.FechaFinCronograma.Value.Year, FiltroDevoluciones.FechaFinCronograma.Value.Month, FiltroDevoluciones.FechaFinCronograma.Value.Day, 23, 59, 59);
                }

                string IdEstadoPagoMatricula = null;
                if (FiltroDevoluciones.IdEstadoPagoMatricula.Count() > 0) IdEstadoPagoMatricula = String.Join(",", FiltroDevoluciones.IdEstadoPagoMatricula);

                List<ReporteDevolucionesDTO> items = new List<ReporteDevolucionesDTO>();
                var query = _dapper.QuerySPDapper("fin.SP_ReporteDevoluciones", new { _FechaIni, _FechaFin, _FechaIniCronograma, _FechaFinCronograma, FiltroDevoluciones.IdAlumno, FiltroDevoluciones.IdCentroCosto, FiltroDevoluciones.IdMatricula });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteDevolucionesDTO>>(query);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de cambios
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambiosDTO> ObtenerReporteCambios(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                DateTime _FechaIni = new DateTime(FiltroCambios.FechaInicio.Year, FiltroCambios.FechaInicio.Month, FiltroCambios.FechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroCambios.FechaFin.Year, FiltroCambios.FechaFin.Month, FiltroCambios.FechaFin.Day, 23, 59, 59);

                List<ReporteCambiosDTO> items = new List<ReporteCambiosDTO>();
                var _query = string.Empty;
                _query = "Select IdCronogramaMod, IdAlumno,Alumno, Modalidad, FechaCambio, Ciudad, Programa, IdCentroCosto, CodigoAlumno, IdMatricula, Observaciones, RealizadoPor, " +
                    "MensajeSistema, SolicitadoPor, AprobadoPor, Observaciones2 from fin.V_ReporteCambios " +
                    "where CAST(FechaCambio AS date) between CAST(@FechaIni AS DATE) and CAST(@FechaFin AS DATE) and (@IdCentroCosto is null or IdCentroCosto=@IdCentroCosto)" +
                    " and (@IdAlumno is null or IdAlumno=@IdAlumno) and (@IdMatricula is null or IdMatricula=@IdMatricula) order by FechaCambio desc";
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaIni = _FechaIni, FechaFin = _FechaFin, FiltroCambios.IdCentroCosto, FiltroCambios.IdAlumno, FiltroCambios.IdMatricula });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambiosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de codigos
        /// </summary>
        /// <returns></returns>
        public List<ReporteCodigosDTO> ObtenerReporteCodigos(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                DateTime _FechaIni = new DateTime(FiltroCambios.FechaInicio.Year, FiltroCambios.FechaInicio.Month, FiltroCambios.FechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroCambios.FechaFin.Year, FiltroCambios.FechaFin.Month, FiltroCambios.FechaFin.Day, 23, 59, 59);

                List<ReporteCodigosDTO> items = new List<ReporteCodigosDTO>();
                var _query = string.Empty;
                _query = "Select IdAlumno, Modalidad, Ciudad, Programa, IdCentroCosto, Codigo, IdMatricula, Alumno, FechaCreacion from fin.V_ReporteCodigoAlumnos " +
                    "where CAST(FechaCreacion as date) between CAST(@FechaIni as date) and CAST(@FechaFin as DATE) " +
                    "and(@IdCentroCosto is null or IdCentroCosto = @IdCentroCosto) and(@IdAlumno is null or IdAlumno = @IdAlumno) and(@IdMatricula is null or IdMatricula = @IdMatricula) order by FechaCreacion desc";
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaIni = _FechaIni, FechaFin = _FechaFin, FiltroCambios.IdCentroCosto, FiltroCambios.IdAlumno, FiltroCambios.IdMatricula });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCodigosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de cuotas
        /// </summary>
        /// <returns></returns>
        public List<ReporteCuotasDTO> ObtenerReporteCuotas(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                DateTime _FechaIni = new DateTime(FiltroCambios.FechaInicio.Year, FiltroCambios.FechaInicio.Month, FiltroCambios.FechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroCambios.FechaFin.Year, FiltroCambios.FechaFin.Month, FiltroCambios.FechaFin.Day, 23, 59, 59);

                List<ReporteCuotasDTO> items = new List<ReporteCuotasDTO>();
                var _query = string.Empty;
                _query = "Select IdAlumno, IdMatricula, CodigoMatricula, Modalidad, Ciudad, CentroCosto, IdCentroCosto, Alumno, FechaCuota, Cuota, SaldoPendiente, Cuota_SubCuota, " +
                    "MonedaPago, EstadoCuota from fin.V_ReporteCuotaAlumno " +
                    "where CAST(FechaCuota as date) between CAST(@FechaIni as date) and CAST(@FechaFin as DATE) " +
                    "and(@IdCentroCosto is null or IdCentroCosto = @IdCentroCosto) and(@IdAlumno is null or IdAlumno = @IdAlumno) and(@IdMatricula is null or IdMatricula = @IdMatricula)";
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaIni = _FechaIni, FechaFin = _FechaFin, FiltroCambios.IdCentroCosto, FiltroCambios.IdAlumno, FiltroCambios.IdMatricula });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCuotasDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de traslaciones
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambioProgramaDTO> ObtenerReporteTraslados(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                DateTime _FechaIni = new DateTime(FiltroCambios.FechaInicio.Year, FiltroCambios.FechaInicio.Month, FiltroCambios.FechaInicio.Day, 0, 0, 0);
                DateTime _FechaFin = new DateTime(FiltroCambios.FechaFin.Year, FiltroCambios.FechaFin.Month, FiltroCambios.FechaFin.Day, 23, 59, 59);

                List<ReporteCambioProgramaDTO> items = new List<ReporteCambioProgramaDTO>();
                var _query = string.Empty;
                _query = "Select Fecha, IdAlumno, Alumno, IdMatriculaCabecera, CodigoMatricula, CentroCostoAnterior, CentroCostoNuevo from fin.V_ReporteCambioPrograma " +
                    "where cast(fecha as DATE) between @FechaIni  and @FechaFin and IdPEspecificoAnterior <> IdPEspecificoNuevo " +
                    "and(@IdCentroCosto is null or IdCentroCostoNuevo = @IdCentroCosto or  IdCentroCostoAnterior = @IdCentroCosto) and(@IdAlumno is null or IdAlumno = @IdAlumno) and(@IdMatriculaCabecera is null or IdMatriculaCabecera = @IdMatriculaCabecera)";
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaIni = _FechaIni, FechaFin = _FechaFin, FiltroCambios.IdCentroCosto, FiltroCambios.IdAlumno, IdMatriculaCabecera = FiltroCambios.IdMatricula });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCambioProgramaDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el tipo de cambio promedio en colombianos del mes anterior o de el rango de fechas seleccionadas
        /// </summary>
        /// <param name="FiltroCambios"></param>
        /// <returns></returns>
        public decimal ObtenerTipoCambioPromedioMesAnteriorColombianos(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;

                if (Filtro.FechaInicioFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                }

                decimal tipoCambio = new decimal(0);
                ValorOpcionalDecimalDTO items = new ValorOpcionalDecimalDTO();
                var resultado = _dapper.QuerySPFirstOrDefault("fin.SP_TipoCambioColombianosPromedioMes", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, fechainicioFiltro = FechaInicio });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ValorOpcionalDecimalDTO>(resultado);
                }
                if (items.Valor == null)
                {
                    return 0M;
                }
                else
                {
                    return items.Valor.Value;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteIngresosMatriculaNuevaDTO> ReporteIngresoMatriculasNuevas(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                List<ReporteIngresosMatriculaNuevaDTO> items = new List<ReporteIngresosMatriculaNuevaDTO>();
                var resultado = _dapper.QuerySPDapper("fin.SP_ReporteIngresos", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, FechaInicioFiltroDate = FechaInicio, FechaFinFiltroDate = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIngresosMatriculaNuevaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el ingreso Real de los Pagos realizados por Aumnos
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteIngresosMatriculaNuevaDTO> ReporteIngresoPagos(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                List<ReporteIngresosMatriculaNuevaDTO> items = new List<ReporteIngresosMatriculaNuevaDTO>();
                var resultado = _dapper.QuerySPDapper("fin.SP_ReporteIngresosPagos", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, FechaInicioFiltroDate = FechaInicio, FechaFinFiltroDate = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIngresosMatriculaNuevaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los pagos de Insregsos de datos Inhouse
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteIngresosMatriculaNuevaDTO> ReporteIngresoInHouse(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                List<ReporteIngresosMatriculaNuevaDTO> items = new List<ReporteIngresosMatriculaNuevaDTO>();
                var resultado = _dapper.QuerySPDapper("fin.SP_ReporteIngresosInHouse", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, FechaInicioFiltroDate = FechaInicio, FechaFinFiltroDate = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIngresosMatriculaNuevaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteIngresosMatriculaNuevaDTO> ReporteIngresoPorHoras(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                List<ReporteIngresosMatriculaNuevaDTO> items = new List<ReporteIngresosMatriculaNuevaDTO>();
                var resultado = _dapper.QuerySPDapper("fin.SP_ReporteIngresosHoras", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, FechaInicioFiltroDate = FechaInicio, FechaFinFiltroDate = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIngresosMatriculaNuevaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteIngresosMatriculaNuevaDTO> ReporteIngresoOtrosIngresos(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                List<ReporteIngresosMatriculaNuevaDTO> items = new List<ReporteIngresosMatriculaNuevaDTO>();
                var resultado = _dapper.QuerySPDapper("fin.SP_ReporteIngresosOtrosIngresos", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, FechaInicioFiltro = FechaInicio, FechaFinFiltro = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteIngresosMatriculaNuevaDTO>>(resultado);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// utiliza el Store Procedure para obtener los abonos de cuentas
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public decimal ReporteIngresoAbonoCuenta(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                decimal tipoCambio = new decimal(0);
                var items = new ValorOpcionalDecimalDTO();
                var resultado = _dapper.QuerySPFirstOrDefault("fin.SP_ReporteIngresosAbonosCuentas", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, fechainicioFiltro = FechaInicio, fechaFinFiltro = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ValorOpcionalDecimalDTO>(resultado);
                }
                if (items.Valor == null)
                {
                    return 0M;
                }
                else
                {
                    return items.Valor.Value;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public decimal ReporteIngresoOtrosIntereses(ReporteIngresosFiltroDTO Filtro)
        {
            try
            {

                DateTime? FechaInicio = Filtro.FechaInicioFiltro;
                DateTime? FechaFin = Filtro.FechaFinFiltro;

                if (Filtro.FechaInicioFiltro != null && Filtro.FechaFinFiltro != null)
                {
                    FechaInicio = new DateTime(Filtro.FechaInicioFiltro.Value.Year, Filtro.FechaInicioFiltro.Value.Month, Filtro.FechaInicioFiltro.Value.Day, 0, 0, 0);
                    FechaFin = new DateTime(Filtro.FechaFinFiltro.Value.Year, Filtro.FechaFinFiltro.Value.Month, Filtro.FechaFinFiltro.Value.Day, 23, 59, 59);
                }

                decimal tipoCambio = new decimal(0);
                var items = new ValorOpcionalDecimalDTO();
                var resultado = _dapper.QuerySPFirstOrDefault("fin.SP_ReporteIngresosOtrosIntereses", new { periodo = Filtro.IdPeriodo, periodoFlag = Filtro.SeleccionoPeriodo, fechainicioFiltro = FechaInicio, fechaFinFiltro = FechaFin });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<ValorOpcionalDecimalDTO>(resultado);
                }
                if (items.Valor == null)
                {
                    return 0M;
                }
                else
                {
                    return items.Valor.Value;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<PagoAlumnoDTO> ObtenerReportePagoAlumnos(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoDTO> items = new List<PagoAlumnoDTO>();
                var _query = string.Empty;
                if (Filtro.IdCambio == null)
                {
                    _query = "select IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados," +
                        "ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera," +
                        "FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota from [fin].[V_ObtenerPagos] where  CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE)";
                }
                if (Filtro.IdCambio == 1)
                {
                    _query = "select IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados," +
                        "ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera," +
                        "FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota from [fin].[V_ObtenerPagos] where CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) and FechaProcesoPagoReal is null and FechaDepositaron is null and FechaDisponible is null ";
                }
                if (Filtro.IdCambio == 2)
                {
                    _query = "select IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados," +
                        "ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera," +
                        "FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota from [fin].[V_ObtenerPagos] where CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) and FechaProcesoPagoReal is not null ";
                }
                if (Filtro.IdCambio == 3)
                {
                    _query = "select IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados," +
                        "ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera," +
                        "FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota from [fin].[V_ObtenerPagos] where CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) and FechaDepositaron is not null  ";
                }
                if (Filtro.IdCambio == 4)
                {
                    _query = "select IdCronogramaPagoDetalleFinal,Alumno,CodigoAlumno,FechaPagoOriginal,FechaPago,DiaPago,FechaPagoReal,DiasDeposito,DiasDisponible,CuentaFeriados," +
                        "ConsideraVSD,ConsiderarDiasHabilesLV,ConsiderarDiasHabilesLS,FechaDepositaron,FechaDisponible,EstadoEfectivo,Cuota_SubCuota,FechaCuota,FormaPago,IdMatriculaCabecera," +
                        "FechaProcesoPago,FechaProcesoPagoReal,FechaMatricula,IdCiudad,Cuota,MonedaCuota from [fin].[V_ObtenerPagos] where CAST(FechaProcesoPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) and FechaDisponible is not null ";
                }
                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el cuadro 1 del reporte de operaciones
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteOperacionesEstructuraDetalleDTO> ObtenerReporteCuadro1(ReporteOperacionesFiltroDTO Filtro)
        {
            try
            {
                var listaReporteCuadro3 = new List<ReporteOperacionesEstructuraDetalleDTO>();
                var listaBase = new List<BaseReporteOperacionesEstructuraDetalleV2DTO>();

                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper("ope.SP_ObtenerReporteCuadro1", new
                {
                    ListaAsesor = Filtro.ListaPersonal.ToListString(),
                    ListaPais = Filtro.ListaPais.ToListString()
                });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listaBase = JsonConvert.DeserializeObject<List<BaseReporteOperacionesEstructuraDetalleV2DTO>>(listadoFiltroSegmentoDB);
                }

                if (listaBase.Count() >= 1)
                {
                    var valorBase = listaBase.FirstOrDefault();
                    //
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con más de 2 llamadas ejecutadas",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_1,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_1,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_1,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_1,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_1
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con dos llamadas ejecutadas",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_2,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_2,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_2,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_2,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_2
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Con una llamada ejecutada",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_3,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_3,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_3,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_3,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_3
                    };
                    var nivel4 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 4,
                        NombreNivel = "Con mas de 5 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_4,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_4,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_4,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_4,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_4
                    };
                    var nivel5 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 5,
                        NombreNivel = "Con 5 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_5,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_5,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_5,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_5,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_5
                    };
                    var nivel6 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 6,
                        NombreNivel = "Con 4 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_6,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_6,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_6,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_6,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_6
                    };
                    var nivel7 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 7,
                        NombreNivel = "Con 3 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_7,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_7,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_7,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_7,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_7
                    };
                    var nivel8 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 8,
                        NombreNivel = "Con 2 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_8,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_8,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_8,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_8,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_8
                    };
                    var nivel9 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 9,
                        NombreNivel = "Con 1 intento de llamada reprogramados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_9,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_9,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_9,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_9,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_9
                    };
                    var nivel10 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 10,
                        NombreNivel = "Sin intento de llamada",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_10,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_10,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_10,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_10,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_10
                    };

                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                    listaReporteCuadro3.Add(nivel4);
                    listaReporteCuadro3.Add(nivel5);
                    listaReporteCuadro3.Add(nivel6);
                    listaReporteCuadro3.Add(nivel7);
                    listaReporteCuadro3.Add(nivel8);
                    listaReporteCuadro3.Add(nivel9);
                    listaReporteCuadro3.Add(nivel10);
                }
                else
                {
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con más de 2 llamadas ejecutadas",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con dos llamadas ejecutadas",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Con una llamada ejecutada",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel4 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 4,
                        NombreNivel = "Con mas de 5 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel5 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 5,
                        NombreNivel = "Con 5 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel6 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 6,
                        NombreNivel = "Con 4 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel7 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 7,
                        NombreNivel = "Con 3 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel8 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 8,
                        NombreNivel = "Con 2 intentos de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel9 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 9,
                        NombreNivel = "Con 1 intento de llamada reprogramados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel10 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 10,
                        NombreNivel = "Sin intento de llamada",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };

                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                    listaReporteCuadro3.Add(nivel4);
                    listaReporteCuadro3.Add(nivel5);
                    listaReporteCuadro3.Add(nivel6);
                    listaReporteCuadro3.Add(nivel7);
                    listaReporteCuadro3.Add(nivel8);
                    listaReporteCuadro3.Add(nivel9);
                    listaReporteCuadro3.Add(nivel10);
                }

                var total = new ReporteOperacionesEstructuraDetalleDTO()
                {
                    Nivel = 4,
                    NombreNivel = "Total",
                    CantidadSemanaMenos1 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos1),
                    CantidadSemanaMenos2 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos2),
                    CantidadSemanaMenos3 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos3),
                    CantidadSemanaMenos4 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos4),
                    CantidadSemanaMenos5 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos5)
                };
                listaReporteCuadro3.Add(total);

                return listaReporteCuadro3;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cuadro 2 del reporte de operaciones
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteOperacionesEstructuraDetalleDTO> ObtenerReporteCuadro2(ReporteOperacionesFiltroDTO Filtro)
        {
            try
            {
                var listaReporteCuadro3 = new List<ReporteOperacionesEstructuraDetalleDTO>();
                var listaBase = new List<BaseReporteOperacionesEstructuraDetalleDTO>();

                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper("ope.SP_ObtenerReporteCuadro2", new
                {
                    ListaAsesor = Filtro.ListaPersonal.ToListString(),
                    ListaPais = Filtro.ListaPais.ToListString()
                });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listaBase = JsonConvert.DeserializeObject<List<BaseReporteOperacionesEstructuraDetalleDTO>>(listadoFiltroSegmentoDB);
                }

                if (listaBase.Count() >= 1)
                {
                    var valorBase = listaBase.FirstOrDefault();
                    //
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con al menos un correo enviado",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_1,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_1,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_1,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_1,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_1
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con al menos un correo respondido",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_2,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_2,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_2,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_2,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_2
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Sin correos enviados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_3,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_3,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_3,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_3,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_3
                    };
                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                }
                else
                {
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con al menos un correo enviado",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con al menos un correo respondido",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Sin correos enviados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };

                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                }

                var total = new ReporteOperacionesEstructuraDetalleDTO()
                {
                    Nivel = 4,
                    NombreNivel = "Total",
                    CantidadSemanaMenos1 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos1),
                    CantidadSemanaMenos2 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos2),
                    CantidadSemanaMenos3 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos3),
                    CantidadSemanaMenos4 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos4),
                    CantidadSemanaMenos5 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos5)
                };
                listaReporteCuadro3.Add(total);

                return listaReporteCuadro3;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cuadro 3 del reporte de operaciones
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<ReporteOperacionesEstructuraDetalleDTO> ObtenerReporteCuadro3(ReporteOperacionesFiltroDTO Filtro)
        {
            try
            {
                var listaReporteCuadro3 = new List<ReporteOperacionesEstructuraDetalleDTO>();
                var listaBase = new List<BaseReporteOperacionesEstructuraDetalleDTO>();

                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper("ope.SP_ObtenerReporteCuadro3", new
                {
                    ListaAsesor = Filtro.ListaPersonal.ToListString(),
                    ListaPais = Filtro.ListaPais.ToListString()
                });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listaBase = JsonConvert.DeserializeObject<List<BaseReporteOperacionesEstructuraDetalleDTO>>(listadoFiltroSegmentoDB);
                }

                if (listaBase.Count() >= 1)
                {
                    var valorBase = listaBase.FirstOrDefault();
                    //
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con al menos un mensaje de WhatApp enviado",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_1,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_1,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_1,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_1,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_1
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con al menos un mensaje de WhatApp respondido",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_2,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_2,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_2,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_2,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_2
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Sin WhatApp enviados",
                        CantidadSemanaMenos1 = valorBase.NivelSumaSemana1_3,
                        CantidadSemanaMenos2 = valorBase.NivelSumaSemana2_3,
                        CantidadSemanaMenos3 = valorBase.NivelSumaSemana3_3,
                        CantidadSemanaMenos4 = valorBase.NivelSumaSemana4_3,
                        CantidadSemanaMenos5 = valorBase.NivelSumaSemana5_3
                    };
                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                }
                else
                {
                    var nivel1 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 1,
                        NombreNivel = "Con al menos un mensaje de WhatApp enviado",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel2 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 2,
                        NombreNivel = "Con al menos un mensaje de WhatApp respondido",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };
                    var nivel3 = new ReporteOperacionesEstructuraDetalleDTO()
                    {
                        Nivel = 3,
                        NombreNivel = "Sin WhatApp enviados",
                        CantidadSemanaMenos1 = 0,
                        CantidadSemanaMenos2 = 0,
                        CantidadSemanaMenos3 = 0,
                        CantidadSemanaMenos4 = 0,
                        CantidadSemanaMenos5 = 0
                    };

                    listaReporteCuadro3.Add(nivel1);
                    listaReporteCuadro3.Add(nivel2);
                    listaReporteCuadro3.Add(nivel3);
                }

                var total = new ReporteOperacionesEstructuraDetalleDTO()
                {
                    Nivel = 4,
                    NombreNivel = "Total",
                    CantidadSemanaMenos1 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos1),
                    CantidadSemanaMenos2 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos2),
                    CantidadSemanaMenos3 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos3),
                    CantidadSemanaMenos4 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos4),
                    CantidadSemanaMenos5 = listaReporteCuadro3.Sum(x => x.CantidadSemanaMenos5)
                };
                listaReporteCuadro3.Add(total);

                return listaReporteCuadro3;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de pagos realizados(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReportePagoAlumnosIngresos(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "CodigoMatricula,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal,FechaMatricula, PorcentajeComision, CobroComisionMontoPagado";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosPagoAlumnos] where CAST(FechaPagoOriginal as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosVentas(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "CodigoMatricula,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal,FechaMatricula, PorcentajeComision, CobroComisionMontoPagado";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosPagoAlumnosVentas] where CAST(FechaPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOperaciones(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "CodigoMatricula,IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal,FechaMatricula, PorcentajeComision, CobroComisionMontoPagado";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosPagoAlumnosOperaciones] where CAST(FechaPago as date) between CAST(@FechaInicio as date) and CAST(@FechaFin as DATE) ";

                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagosIngresosDTO> ObtenerReporteIngresosOperacionesTipoCambio(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaInicio = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagosIngresosDTO> items = new List<PagosIngresosDTO>();
                var query = "SELECT matiid,CodigoAlumno,MonedaPago,TipoCambio,Cuota,Mora,TotalPagado" +
                            ", FechaPagoOriginal, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, PorcentajeCobro, FechaDisponible, EstadoEfectivo, Cuota_SubCuota, FechaCuota" +
                            ", Observaciones, FormaIngreso, EstadoCuota, IdModalidad, IdMatriculaCabecera, IdCentroCosto, FechaProcesoPago FROM FIN.V_ReporteIngresosPagoAlumnosOperacionesTipoCambio where FechaPagoOriginal between @fechaInicio and @fechaFin";
                var respuestaDapper = _dapper.QueryDapper(query, new { fechaInicio = FechaInicio, fechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagosIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Reporte de Otros Ingresos(Ingresos)
        /// </summary>
        /// <param name="Filtro"></param>
        /// <returns></returns>
        public List<PagoAlumnoIngresosDTO> ObtenerReporteIngresosOtrosIngresos(ReportePagoFiltroDTO Filtro)
        {
            try
            {
                DateTime FechaIni = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                List<PagoAlumnoIngresosDTO> items = new List<PagoAlumnoIngresosDTO>();
                var CamposTabla = "IdCronogramaPagoDetalleFinal,IdMatriculaCabecera,nrocuota,nrosubcuota,cuotadolares,montopagado,MontoPagadoTipoCambioFechaPago" +
                    ", periodoporfechavencimiento, periodofechapago, FechaPago, DiaPago, FechaPagoReal, DiasDeposito, DiasDisponible, CuentaFeriados, ConsideraVSD, ConsiderarDiasHabilesLV, ConsiderarDiasHabilesLS, FechaIngresoEnCuenta" +
                    ", EstadoEfectivo, FechaCuota, IdCiudad, IdCategoriaOrigen, fechapagoOriginal, PorcentajeComision, CobroComisionMontoPagado, IdTipoMovimientoCaja";
                var _query = string.Empty;

                _query = "select " + CamposTabla + " from [fin].[V_ReporteIngresosOtrosIngresosEgresos] where FechaPago between @FechaInicio  and @FechaFin ";

                var respuestaDapper = _dapper.QueryDapper(_query, new { FechaInicio = FechaIni, FechaFin = FechaFin });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PagoAlumnoIngresosDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene Reclamos realizados en el aula virtual
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<ReporteProblemasAulaVirtualResultadoDTO> ObtenerReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtro, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteProblemasAulaVirtualResultadoDTO> items = new List<ReporteProblemasAulaVirtualResultadoDTO>();
                string coordinadores = null;
                string centroCosto = null;
                string tipoCategoriaError = null;
                string matriculaCabecera = null;

                if (filtro.Coordinadores.Count() > 0)
                {
                    coordinadores = String.Join(",", filtro.Coordinadores);
                }
                if (filtro.CentroCostos.Count() > 0)
                {
                    centroCosto = String.Join(",", filtro.CentroCostos);
                }
                if (filtro.TipoCategoriaError.Count() > 0)
                {
                    tipoCategoriaError = String.Join(",", filtro.TipoCategoriaError);
                }
                if (filtro.MatriculaCabecera.Count() > 0)
                {
                    matriculaCabecera = String.Join(",", filtro.MatriculaCabecera);
                }

                var query = _dapper.QuerySPDapper("pla.SP_ReporteProblemasAulaVirtual", new
                {
                    Coordinador = coordinadores,
                    CentroCosto = centroCosto,
                    MatriculaCabecera = matriculaCabecera,
                    TipoCategoriaError = tipoCategoriaError,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin
                });

                if (!string.IsNullOrEmpty(query))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteProblemasAulaVirtualResultadoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        ///Repositorio: ReporteRepositorio
        ///Autor: Edgar S.
        ///Fecha: 10/02/2021
        /// <summary>
        /// Obtiene el reporte de calidad procesamiento Version 2
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns> Lista de objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamientoV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdCategoriaDato in (" + String.Join(",", filtros.CategoriaDatos) + ")";
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdTipoDato in (" + String.Join(",", filtros.TipoDatos) + ")";
                //}

                List<ReporteCalidadProcesamientoDTO> items = new List<ReporteCalidadProcesamientoDTO>();
                var query = $@"
                    SELECT 
                            DatosAsesor AS DatosAsesor,
                            CodigoFaseOportunidad AS NombreFase,
                            COUNT(CodigoFaseOportunidad) AS Registros,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(PerfilCamposLlenos)*100 /Sum(PerfilCamposTotal))),3)/100 AS numeric(36,2)) AS PromedioPerfil,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(PGeneralValidados)*100 /Sum(PGeneralTotal))),3)/100 AS numeric(36,2)) AS PromedioPGeneral,
	                        CAST(ROUND(CONVERT(decimal(12,2),(Sum(PEspecificoValidados)*100 /Sum(PEspecificoTotal))),3)/100 AS numeric(36,2)) AS PromedioPEspecifico,
                            CAST(ROUND(CONVERT(decimal(12,2),(Sum(BeneficiosValidados)*100 /Sum(BeneficiosTotales))),3)/100 AS numeric(36,2)) AS PromedioBeneficios,
	                        CAST(ROUND(AVG(CONVERT(decimal(12,2),CompetidoresVerificacion)),2) AS numeric(36,2)) AS PromedioCompetidores, 
	                        CAST(ROUND((AVG(CONVERT(decimal(12,2),ProblemaSeleccionados))/18),2) AS numeric(36,2)) AS PromedioProblemaSeleccionados,
	                        CAST(ROUND((AVG(CONVERT(decimal(12,2),ProblemaSolucionados))/18),2) AS numeric(36,2)) AS PromedioProblemaSolucionados,
                            CAST(ROUND(CONVERT(decimal(12,2),(
	                        SUM(    CASE
			                        WHEN UltimaFechaConsultaSentinel IS NOT NULL AND UltimaFechaConsultaSentinel BETWEEN @FechaInicio and @FechaFin
				                        THEN 1
			                        ELSE 0
			                        END))),2) AS numeric(36,2))/ 
		                    COUNT(  CASE
			                        WHEN UltimaFechaConsultaSentinel IS NULL
			                            THEN 1
			                        ELSE 1 END) AS PromedioHistorialFinanciero
                    FROM com.V_ObtenerReporteCalidadDeProcesamiento
                    WHERE Fecha BETWEEN @FechaInicio AND @FechaFin  { filtro } 
                    GROUP BY DatosAsesor,
                             CodigoFaseOportunidad
                    ORDER BY DatosAsesor, CodigoFaseOportunidad";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteCalidadProcesamientoDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ReporteRepositorio
        ///Autor: Edgar S.
        ///Fecha: 17/02/2021
        /// <summary>
        /// Obtiene el reporte de Diferencia de Llamadas por Bloque
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<DiferenciaLlamadasBloqueDTO> </returns>
        public List<DiferenciaLlamadasBloqueDTO> ReporteDiferenciaLlamadasBloque(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string filtro = "";
                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdCategoriaDato in (" + String.Join(",", filtros.CategoriaDatos) + ")";
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdTipoDato in (" + String.Join(",", filtros.TipoDatos) + ")";
                //}
                List<ResultadoDiferenciaLlamadasBloqueDTO> resultado = new List<ResultadoDiferenciaLlamadasBloqueDTO>();
                var query = $@"
                    SELECT
                    SUM(CASE WHEN Diferencia = 0 THEN 1 ELSE 0 END) AS Cero,
	                SUM(CASE WHEN Diferencia = 1 THEN 1 ELSE 0 END) AS MasCero,
	                SUM(CASE WHEN Diferencia = 2 THEN 1 ELSE 0 END) AS MasUno,
	                SUM(CASE WHEN Diferencia = 3 THEN 1 ELSE 0 END) AS MasDos,
	                SUM(CASE WHEN Diferencia = 4 THEN 1 ELSE 0 END) AS MasTres,
	                SUM(CASE WHEN Diferencia = 5 THEN 1 ELSE 0 END) AS MasCuatro,
	                SUM(CASE WHEN Diferencia = 6 THEN 1 ELSE 0 END) AS MasCinco,
	                SUM(CASE WHEN Diferencia > 6 THEN 1 ELSE 0 END) AS MasSeis
                    FROM com.V_ObtenerDiferenciaOportunidadesContadorBIC
                    WHERE FechaModificacionOportunidad BETWEEN @FechaInicio AND @FechaFin  { filtro } 
                    GROUP BY Diferencia";
                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    filtros.FechaInicio,
                    filtros.FechaFin
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<ResultadoDiferenciaLlamadasBloqueDTO>>(queryRespuesta);
                }
                List<DiferenciaLlamadasBloqueDTO> resultadoReporte = new List<DiferenciaLlamadasBloqueDTO>();
                DiferenciaLlamadasBloqueDTO nuevo;
                foreach (var registro in resultado)
                {
                    nuevo = new DiferenciaLlamadasBloqueDTO();
                    if (registro.Cero != 0)
                    {
                        nuevo.Descripcion = "0 días";
                        nuevo.Cantidad = registro.Cero.GetValueOrDefault();
                    }
                    else if (registro.MasCero != 0)
                    {
                        nuevo.Descripcion = "Mas de 0 días";
                        nuevo.Cantidad = registro.MasCero.GetValueOrDefault();
                    }
                    else if (registro.MasUno != 0)
                    {
                        nuevo.Descripcion = "Mas de 1 día";
                        nuevo.Cantidad = registro.MasUno.GetValueOrDefault();
                    }
                    else if (registro.MasDos != 0)
                    {
                        nuevo.Descripcion = "Mas de 2 día";
                        nuevo.Cantidad = registro.MasDos.GetValueOrDefault();
                    }
                    else if (registro.MasTres != 0)
                    {
                        nuevo.Descripcion = "Mas de 3 día";
                        nuevo.Cantidad = registro.MasTres.GetValueOrDefault();
                    }
                    else if (registro.MasCuatro != 0)
                    {
                        nuevo.Descripcion = "Mas de 4 día";
                        nuevo.Cantidad = registro.MasCuatro.GetValueOrDefault();
                    }
                    else if (registro.MasCinco != 0)
                    {
                        nuevo.Descripcion = "Mas de 5 días";
                        nuevo.Cantidad = registro.MasCinco.GetValueOrDefault();
                    }
                    resultadoReporte.Add(nuevo);
                }
                return resultadoReporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ReporteRepositorio
        ///Autor: Edgar S.
        ///Fecha: 17/02/2021
        /// <summary>
        /// Obtiene el reporte Conteo de Datos por Fase
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de objeto DTO : List<ConteoDatosFaseDTO> </returns>
        public List<ConteoDatosFaseDTO> ReporteConteoDatosFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                List<ResultadoConteoDatosFaseDTO> resultAnterior = new List<ResultadoConteoDatosFaseDTO>();
                List<ResultadoConteoDatosFaseDTO> resultActual = new List<ResultadoConteoDatosFaseDTO>();

                bool banderaConsultaActual = false;
                DateTime fechaSeguimiento;
                fechaSeguimiento = DateTime.Now.Date;
                if (filtros.FechaFin.Date == fechaSeguimiento)
                {
                    banderaConsultaActual = true;
                }

                string filtro = "";
                string filtroConsultaActual = "";

                if (filtros.Asesores.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdPersonal in (" + String.Join(",", filtros.Asesores) + ")";

                    filtroConsultaActual += " and ";
                    filtroConsultaActual += "IdPersonal_Asignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtro += " and ";
                    filtro += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";

                    filtroConsultaActual += " and ";
                    filtroConsultaActual += "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdCategoriaOrigen in (" + String.Join(",", filtros.CategoriaDatos) + ")";
                //
                //    filtroConsultaActual += " and ";
                //    filtroConsultaActual += "IdCategoriaOrigen in (" + String.Join(",", filtros.CategoriaDatos) + ")";
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    filtro += " and ";
                //    filtro += "IdTipoDato in (" + String.Join(",", filtros.TipoDatos) + ")";
                //
                //    filtroConsultaActual += " and ";
                //    filtroConsultaActual += "IdTipoDato in (" + String.Join(",", filtros.TipoDatos) + ")";
                //}

                var query = $@"
                    select
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                    WHERE DOV.IdSesionGuardado = 1 AND DOV.FechaCreacion BETWEEN @FechaInicioDia AND @FechaInicioFinDia  { filtro } 
	                GROUP BY DOV.IdFaseOportunidad,FO.Codigo";

                var queryRespuesta = _dapper.QueryDapper(query, new
                {
                    FechaInicioDia = filtros.FechaInicio.Date,
                    FechaInicioFinDia = filtros.FechaInicio.Date.AddDays(1),
                });

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    resultAnterior = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuesta);
                }


                if (banderaConsultaActual)
                {
                    var queryActual = $@"
                    select
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM com.T_Oportunidad AS OP
	                INNER JOIN pla.T_FaseOportunidad AS FO ON OP.IdFaseOportunidad = FO.Id AND FO.Estado = 1 AND FO.Id IN (2,7,8,12,13,22)
                    WHERE OP.Estado = 1 { filtroConsultaActual } 
	                GROUP BY OP.IdFaseOportunidad,FO.Codigo";
                    var queryRespuestaActual = _dapper.QueryDapper(queryActual, null);

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuestaActual);
                    }
                }
                else
                {
                    var queryActual = $@"
                    select
	                    FO.Codigo AS FaseOportunidad,
	                    COUNT(IdFaseOportunidad) AS Total
	                FROM [com].[T_DatoOportunidadAreaVenta] AS DOV
	                INNER JOIN pla.T_FaseOportunidad AS FO ON DOV.IdFaseOportunidad = FO.Id
                    WHERE DOV.IdSesionGuardado = 2 AND DOV.FechaCreacion BETWEEN @FechaInicioTarde AND @FechaInicioFinTarde  { filtro } 
	                GROUP BY DOV.IdFaseOportunidad,FO.Codigo";
                    var queryRespuestaActual = _dapper.QueryDapper(queryActual, new
                    {
                        FechaInicioTarde = filtros.FechaFin.Date,
                        FechaInicioFinTarde = filtros.FechaFin.Date.AddDays(1)
                    });

                    if (!string.IsNullOrEmpty(queryRespuestaActual) && !queryRespuestaActual.Contains("[]"))
                    {
                        resultActual = JsonConvert.DeserializeObject<List<ResultadoConteoDatosFaseDTO>>(queryRespuestaActual);
                    }
                }

                List<ConteoDatosFaseDTO> result = new List<ConteoDatosFaseDTO>();
                ConteoDatosFaseDTO nuevoActual;
                foreach (var fase in resultActual)
                {
                    if (fase.FaseOportunidad != null)
                    {
                        nuevoActual = new ConteoDatosFaseDTO();
                        nuevoActual.Fase = fase.FaseOportunidad;
                        nuevoActual.Momento = fase.Total;
                        result.Add(nuevoActual);
                    }
                }

                foreach (var fase in resultAnterior)
                {
                    if (fase.FaseOportunidad != null)
                    {
                        var faseAnterior = result.Where(x => x.Fase == fase.FaseOportunidad).FirstOrDefault();
                        if (faseAnterior == null)
                        {
                            nuevoActual = new ConteoDatosFaseDTO();
                            nuevoActual.Fase = fase.FaseOportunidad;
                            nuevoActual.Momento = 0;
                            nuevoActual.Inicio = fase.Total;
                            result.Add(nuevoActual);
                        }
                        else
                        {
                            faseAnterior.Inicio = fase.Total;
                        }
                    }
                }

                nuevoActual = new ConteoDatosFaseDTO();
                var totalInicio = 0;
                var totalMomento = 0;
                foreach (var dato in result)
                {
                    totalInicio = totalInicio + dato.Inicio;
                    totalMomento = totalMomento + dato.Momento;
                }
                nuevoActual.Fase = "Total";
                nuevoActual.Inicio = totalInicio;
                nuevoActual.Momento = totalMomento;
                result.Add(nuevoActual);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos de la tabla T_CronogramaPagoDetalleModLogFinal en base a una fecha fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeCambios(string FechaCongelamiento, string Usuario)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_DividirMensajeSistema", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base a una fecha  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorDia(string FechaCongelamiento, string Usuario)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_CongelarReportePagoPorDia", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de pagos en base al dia fiunal de un periodo 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="IdPeriod"> periodo</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDePagosPorPeriodo(string FechaCongelamiento, string Usuario, int IdPeriodo)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_CongelarReportePagoPorPeriodo", new { Usuario, FechaCongelamiento, IdPeriodo });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 07/05/2021
        /// <summary>
        /// Congela los datos del reporte de devoluciones en base a una fecha  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeDevoluciones(string FechaCongelamiento, string Usuario)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_CongelarReporteDevoluciones", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: ReporteRepositorio
        ///Autor: Jashin Salazar.
        ///Fecha: 22/04/2021
        /// <summary>
        /// Ejecuta el calculo para el reporte de tasa de conversion consolidada
        /// </summary>
        /// <returns>  objeto DTO : ResultadoFinalDTO </returns>
        public ResultadoFinalDTO CalculoReporteTasaConversionConsolidada()
        {
            try
            {
                var query = _dapper.QuerySPFirstOrDefault("com.SP_GenerarReporteOPortunidadTasaConversionHistoricaNuevoModelo", null);
                var rpta = JsonConvert.DeserializeObject<ResultadoFinalDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + (e.InnerException != null ? ("-" + e.InnerException.Message) : ""));
            }
        }

        public ReporteTasaDeCambioDTO ObtenerReporteTasaDeConversion(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                string Asesores = null;
                if (filtros.Asesores.Count() > 0)
                {
                    Asesores = String.Join(",", filtros.Asesores);
                }
                ReporteTasaDeCambioDTO respuesta = new ReporteTasaDeCambioDTO();
                List<TCRM_CambioDeFaseDTO> itemSemanal = new List<TCRM_CambioDeFaseDTO>();
                List<TCRM_CambioDeFaseDTO> itemMensual = new List<TCRM_CambioDeFaseDTO>();
                string querySemanal = _dapper.QuerySPDapper("com.SP_ObtenerTasaConsolidadaParaCambioDeFase", new { asesoresTCAP = Asesores, fechaFinTCAP = filtros.FechaFin, tipo = 1 });
                string queryMensual = _dapper.QuerySPDapper("com.SP_ObtenerTasaConsolidadaParaCambioDeFase", new { asesoresTCAP = Asesores, fechaFinTCAP = filtros.FechaFin, tipo = 0 });
                if (!string.IsNullOrEmpty(querySemanal))
                {
                    itemSemanal = JsonConvert.DeserializeObject<List<TCRM_CambioDeFaseDTO>>(querySemanal);
                }
                if (!string.IsNullOrEmpty(queryMensual))
                {
                    itemMensual = JsonConvert.DeserializeObject<List<TCRM_CambioDeFaseDTO>>(queryMensual);
                }
                respuesta.ReporteTasaDeCambioSemanal = itemSemanal;
                respuesta.ReporteTasaDeCambioMensual = itemMensual;
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jashin Salazar.
        /// Fecha: 11/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de OportundiadLog de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        /// <returns>List<ReporteSeguimientoOportunidadLogDTO></returns>

        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerListaOportunidadLogV2(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidadesLog = new List<ReporteSeguimientoOportunidadLogDTO>();
                var query = @"SELECT OOL.*,
                            CONCAT(p.Nombres, ' ', p.Apellidos) AS Personal
                            FROM com.V_ObtenerOportunidadLogReporteSeguimientoNW AS OOL
                            LEFT JOIN com.T_OportunidadLog AS OL ON OOL.IdOportunidadLog=OL.Id
                            LEFT JOIN gp.T_Personal AS P ON P.Id=OL.IdPersonal_Asignado
                            WHERE OOL.IdOportunidad = 1679184 AND OOL.EstadoOportunidadLog=1 AND (OOL.ComentarioActividad<>'Asignacion Manual' OR OOL.ComentarioActividad IS NULL)
                            ORDER BY FechaModificacion";
                var queryRespuesta = _dapper.QueryDapper(query, new { idOportunidad = idOportunidad });
                var oportunidades = new List<ReporteSeguimientoOportunidadLogDTO>();
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    oportunidadesLog = JsonConvert.DeserializeObject<List<ReporteSeguimientoOportunidadLogDTO>>(queryRespuesta);

                    oportunidades = (from p in oportunidadesLog
                                     group p by new
                                     {
                                         p.FaseInicio,
                                         p.FaseDestino,
                                         p.FechaModificacion,
                                         p.FechaSiguienteLlamada,
                                         p.IdFaseOportunidad,
                                         p.IdFaseOportunidadIP,
                                         p.IdFaseOportunidadPF,
                                         p.IdFaseOportunidadIC,
                                         p.FechaEnvioFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadPF,
                                         p.FechaPagoFaseOportunidadIC,
                                         p.IdOcurrencia,
                                         p.IdEstadoOcurrencia,
                                         p.IdOportunidadLog,
                                         p.NombreActividad,
                                         p.NombreOcurrencia,
                                         p.ComentarioActividad,
                                         p.IdFaseOportunidadInicial,
                                         p.Personal
                                     } into g
                                     select new ReporteSeguimientoOportunidadLogDTO
                                     {
                                         FaseInicio = g.Key.FaseInicio,
                                         FaseDestino = g.Key.FaseDestino,
                                         FechaModificacion = g.Key.FechaModificacion,
                                         FechaSiguienteLlamada = g.Key.FechaSiguienteLlamada,
                                         IdFaseOportunidad = g.Key.IdFaseOportunidad,
                                         IdFaseOportunidadIP = g.Key.IdFaseOportunidadIP,
                                         IdFaseOportunidadPF = g.Key.IdFaseOportunidadPF,
                                         IdFaseOportunidadIC = g.Key.IdFaseOportunidadIC,
                                         FechaEnvioFaseOportunidadPF = g.Key.FechaEnvioFaseOportunidadPF,
                                         FechaPagoFaseOportunidadPF = g.Key.FechaPagoFaseOportunidadPF,
                                         FechaPagoFaseOportunidadIC = g.Key.FechaPagoFaseOportunidadIC,
                                         IdOcurrencia = g.Key.IdOcurrencia,
                                         IdEstadoOcurrencia = g.Key.IdEstadoOcurrencia,
                                         IdOportunidadLog = g.Key.IdOportunidadLog,
                                         NombreActividad = g.Key.NombreActividad,
                                         NombreOcurrencia = g.Key.NombreOcurrencia,
                                         ComentarioActividad = g.Key.ComentarioActividad,
                                         IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                         Personal = g.Key.Personal,
                                         //TiempoDuracion = string.Join("\n", g.Select(x => new { x.TiempoDuracion, x.IdCentralLLamada })
                                         //                                    .GroupBy(i => i.IdCentralLLamada).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion)),
                                         //TiempoDuracion3CX = string.Join("\n", g.Select(x => new { x.TiempoDuracion3CX, x.IdTresCX })
                                         //                                    .GroupBy(i => i.IdTresCX).Select(i => i.FirstOrDefault()).Select(gr => gr.TiempoDuracion3CX)),
                                         LlamadaIntegra = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdCentralLLamada,
                                             TiempoDuracion = o.TiempoDuracion,
                                             TiempoDuracionMinutos = o.TiempoDuracionMinutos,
                                             FechaInicioLlamada = o.FechaIncioLlamadaIntegra,
                                             EstadoLlamada = o.EstadoLlamadaIntegra,
                                             FechaFinLlamada = o.FechaFinLlamadaIntegra,
                                             SubEstadoLlamada = o.SubEstadoLlamadaIntegra,
                                             NombreGrabacion = o.NombreGrabacionIntegra,
                                             Webphone = o.Webphone
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                         LlamadaTresCX = g.Select(o => new LlamadaIntegraDTO
                                         {
                                             Id = o.IdTresCX,
                                             TiempoDuracion = o.TiempoDuracion3CX,
                                             FechaInicioLlamada = o.FechaIncioLlamadaTresCX,
                                             EstadoLlamada = o.EstadoLlamadaTresCX,
                                             FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                             SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                             NombreGrabacion = o.NombreGrabacionTresCX
                                         }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                     }).ToList();
                }

                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

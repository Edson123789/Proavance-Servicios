using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Reportes.Comercial
{
    /// Repositorio: Reportes
    /// Autor: , Gian Miranda
    /// Fecha: 01/06/2021
    /// <summary>
    /// Gestión de Reportes
    /// </summary>
    public partial class Reportes
    {
        private ReportesRepositorio _repReportes;
        public Reportes()
        {
            _repReportes = new ReportesRepositorio();
        }

        /// <summary>
        /// Obtiene el log de las oportunidades
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadLogGridDTO</returns>
        public List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLog(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidades = _repReportes.ObtenerListaOportunidadLog(idOportunidad);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                List<ReporteActividadOcurrenciaDTO> listaActividades = _repReportes.ReporteActividadOcurrencia(idOportunidad);
                ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog;
                foreach (var oportunidad in oportunidades)
                {

                    auxOportunidadLog = new ReporteSeguimientoOportunidadLogGridDTO();
                    auxOportunidadLog.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.FaseInicio = oportunidad.FaseInicio;
                    auxOportunidadLog.FaseDestino = oportunidad.FaseDestino;
                    auxOportunidadLog.FechaModificacion = oportunidad.FechaModificacion;
                    auxOportunidadLog.FechaSiguienteLlamada = oportunidad.FechaSiguienteLlamada;
                    auxOportunidadLog.LlamadaIntegra = oportunidad.LlamadaIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.LlamadaTresCX = oportunidad.LlamadaTresCX.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.NombreActividad = oportunidad.NombreActividad;
                    auxOportunidadLog.NombreOcurrencia = oportunidad.NombreOcurrencia;
                    auxOportunidadLog.ComentarioActividad = oportunidad.ComentarioActividad;

                    if (oportunidad.IdFaseOportunidad == 8)
                    {
                        if (oportunidad.IdFaseOportunidadIP == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }
                    else if (oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.IdFaseOportunidadPF == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                        auxOportunidadLog.FechaEnvio = oportunidad.FechaEnvioFaseOportunidadPF;
                    }
                    else if (oportunidad.IdFaseOportunidad == 12)
                    {
                        if (oportunidad.IdFaseOportunidadIC == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }

                    if (oportunidad.IdFaseOportunidad == 12 || oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.FechaPagoFaseOportunidadPF != null) auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadPF;
                        else auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadIC;
                    }

                    if (oportunidad.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                    {
                        if (new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(oportunidad.IdOcurrencia ?? 0)) auxOportunidadLog.Estado = "REPROGRAMADO M.";
                        else auxOportunidadLog.Estado = "REPROGRAMADO AUT.";
                    }
                    else if (oportunidad.IdEstadoOcurrencia == 1) auxOportunidadLog.Estado = "EJECUTADO";
                    else if (oportunidad.IdEstadoOcurrencia == 7) auxOportunidadLog.Estado = "ASIGNACION";

                    auxOportunidadLog.TiempoDuracion = oportunidad.TiempoDuracion;
                    auxOportunidadLog.TiempoDuracion3CX = oportunidad.TiempoDuracion3CX;
                    listaSeguientoOportunidad.Add(auxOportunidadLog);
                }

                var ultimaOportunidad = _repReportes.ObtenerOportunidadCodigoFase(idOportunidad);
                ultimaOportunidad.Estado = "NO EJECUTADO";

                ultimaOportunidad.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();

                listaSeguientoOportunidad.Add(ultimaOportunidad);

                return listaSeguientoOportunidad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLogPorAlumno(int IdAlumno, int IdOportunidad, int IdPadre)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidades = _repReportes.ObtenerListaOportunidadLogPorIdAlumno(IdAlumno, IdOportunidad, IdPadre);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                List<ReporteActividadOcurrenciaDTO> listaActividades = _repReportes.ReporteActividadOcurrenciaPorIdAlumno(IdAlumno);
                ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog;
                foreach (var oportunidad in oportunidades)
                {

                    auxOportunidadLog = new ReporteSeguimientoOportunidadLogGridDTO();
                    auxOportunidadLog.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.FaseInicio = oportunidad.FaseInicio;
                    auxOportunidadLog.FaseDestino = oportunidad.FaseDestino;
                    auxOportunidadLog.FechaModificacion = oportunidad.FechaModificacion;
                    auxOportunidadLog.FechaSiguienteLlamada = oportunidad.FechaSiguienteLlamada;
                    auxOportunidadLog.LlamadaIntegra = oportunidad.LlamadaIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.LlamadaTresCX = oportunidad.LlamadaTresCX.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.NombreActividad = oportunidad.NombreActividad;
                    auxOportunidadLog.NombreOcurrencia = oportunidad.NombreOcurrencia;
                    auxOportunidadLog.ComentarioActividad = oportunidad.ComentarioActividad;

                    if (oportunidad.IdFaseOportunidad == 8)
                    {
                        if (oportunidad.IdFaseOportunidadIP == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }
                    else if (oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.IdFaseOportunidadPF == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                        auxOportunidadLog.FechaEnvio = oportunidad.FechaEnvioFaseOportunidadPF;
                    }
                    else if (oportunidad.IdFaseOportunidad == 12)
                    {
                        if (oportunidad.IdFaseOportunidadIC == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }

                    if (oportunidad.IdFaseOportunidad == 12 || oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.FechaPagoFaseOportunidadPF != null) auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadPF;
                        else auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadIC;
                    }

                    if (oportunidad.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                    {
                        if (new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(oportunidad.IdOcurrencia ?? 0)) auxOportunidadLog.Estado = "REPROGRAMADO M.";
                        else auxOportunidadLog.Estado = "REPROGRAMADO AUT.";
                    }
                    else if (oportunidad.IdEstadoOcurrencia == 1) auxOportunidadLog.Estado = "EJECUTADO";
                    else if (oportunidad.IdEstadoOcurrencia == 7) auxOportunidadLog.Estado = "ASIGNACION";

                    auxOportunidadLog.TiempoDuracion = oportunidad.TiempoDuracion;
                    auxOportunidadLog.TiempoDuracion3CX = oportunidad.TiempoDuracion3CX;
                    listaSeguientoOportunidad.Add(auxOportunidadLog);
                }

                var ultimaOportunidad = _repReportes.ObtenerOportunidadCodigoFaseporIdAlumno(IdAlumno, IdOportunidad);
                ultimaOportunidad.Estado = "NO EJECUTADO";

                ultimaOportunidad.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();

                listaSeguientoOportunidad.Add(ultimaOportunidad);

                return listaSeguientoOportunidad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLogRichard(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidades = _repReportes.ObtenerListaOportunidadLogRichard(idOportunidad);

                List<ReporteSeguimientoOportunidadLogGridDTO> listaSeguientoOportunidad = new List<ReporteSeguimientoOportunidadLogGridDTO>();
                List<ReporteActividadOcurrenciaDTO> listaActividades = _repReportes.ReporteActividadOcurrencia(idOportunidad);
                ReporteSeguimientoOportunidadLogGridDTO auxOportunidadLog;
                foreach (var oportunidad in oportunidades)
                {

                    auxOportunidadLog = new ReporteSeguimientoOportunidadLogGridDTO();
                    auxOportunidadLog.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == oportunidad.IdFaseOportunidadInicial && x.FechaReal < oportunidad.FechaModificacion.Value).Count();
                    auxOportunidadLog.FaseInicio = oportunidad.FaseInicio;
                    auxOportunidadLog.FaseDestino = oportunidad.FaseDestino;
                    auxOportunidadLog.FechaModificacion = oportunidad.FechaModificacion;
                    auxOportunidadLog.FechaSiguienteLlamada = oportunidad.FechaSiguienteLlamada;
                    auxOportunidadLog.LlamadaIntegra = oportunidad.LlamadaIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.LlamadaTresCX = oportunidad.LlamadaTresCX.OrderBy(x => x.FechaInicioLlamada).ToList();
                    auxOportunidadLog.NombreActividad = oportunidad.NombreActividad;
                    auxOportunidadLog.NombreOcurrencia = oportunidad.NombreOcurrencia;
                    auxOportunidadLog.ComentarioActividad = oportunidad.ComentarioActividad;

                    if (oportunidad.IdFaseOportunidad == 8)
                    {
                        if (oportunidad.IdFaseOportunidadIP == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }
                    else if (oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.IdFaseOportunidadPF == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                        auxOportunidadLog.FechaEnvio = oportunidad.FechaEnvioFaseOportunidadPF;
                    }
                    else if (oportunidad.IdFaseOportunidad == 12)
                    {
                        if (oportunidad.IdFaseOportunidadIC == 5) auxOportunidadLog.EstadoFase = "Solido";
                        else auxOportunidadLog.EstadoFase = "-";
                    }

                    if (oportunidad.IdFaseOportunidad == 12 || oportunidad.IdFaseOportunidad == 22)
                    {
                        if (oportunidad.FechaPagoFaseOportunidadPF != null) auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadPF;
                        else auxOportunidadLog.FechaPago = oportunidad.FechaPagoFaseOportunidadIC;
                    }

                    if (oportunidad.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado)
                    {
                        if (new int[] { 149, 162, 163, 164, 165, 168, 207, 209 }.Contains(oportunidad.IdOcurrencia ?? 0)) auxOportunidadLog.Estado = "REPROGRAMADO M.";
                        else auxOportunidadLog.Estado = "REPROGRAMADO AUT.";
                    }
                    else if (oportunidad.IdEstadoOcurrencia == 1) auxOportunidadLog.Estado = "EJECUTADO";
                    else if (oportunidad.IdEstadoOcurrencia == 7) auxOportunidadLog.Estado = "ASIGNACION";

                    auxOportunidadLog.TiempoDuracion = oportunidad.TiempoDuracion;
                    auxOportunidadLog.TiempoDuracion3CX = oportunidad.TiempoDuracion3CX;
                    listaSeguientoOportunidad.Add(auxOportunidadLog);
                }

                var ultimaOportunidad = _repReportes.ObtenerOportunidadCodigoFase(idOportunidad);
                ultimaOportunidad.Estado = "NO EJECUTADO";

                ultimaOportunidad.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();
                ultimaOportunidad.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 && x.IdFaseActual == ultimaOportunidad.IdFaseOportunidad).Count();

                listaSeguientoOportunidad.Add(ultimaOportunidad);

                return listaSeguientoOportunidad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimientoOportunidad(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }

                if (filtros.FasesOportunidad.Count() > 0)
                {
                    _new.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.FaseOportunidadOrigen.Count() > 0)
                {
                    _new.FasesOportunidadOrigen = String.Join(",", filtros.FaseOportunidadOrigen);
                }
                if (filtros.FaseOportunidadDestino.Count() > 0)
                {
                    _new.FasesOportunidadDestino = String.Join(",", filtros.FaseOportunidadDestino);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _new.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                _new.OpcionFase = filtros.OpcionFase;
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteSeguimiento(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSolicitudesVisualizacion(ReporteSolicitudesVisualizacionFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }

                if (filtros.FasesOportunidad.Count() > 0)
                {
                    _new.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _new.CentroCostos = String.Join(",", filtros.CentroCostos);
                }

                var data = _repReportes.ObtenerReporteSolicitudesVisualizacion(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSolicitudesVisualizacionOperaciones(ReporteSolicitudesVisualizacionFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }

                if (filtros.FasesOportunidad.Count() > 0)
                {
                    _new.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _new.CentroCostos = String.Join(",", filtros.CentroCostos);
                }

                var data = _repReportes.ObtenerReporteSolicitudesVisualizacionOperaciones(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public ResultadoFinalDTO AprobacionSolicitudVisualizacion(AprobacionSolicitudesVisualizacionFiltrosDTO aprobacion)
        {
            try
            {
                var data = _repReportes.AprobacionSolicitudVisualizacion(aprobacion);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteSeguimientoOportunidadesOperacionesDTO> ObtenerReporteSeguimientoOportunidadOperaciones(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.EstadosMatricula.Count() > 0)
                {
                    _new.EstadosMatricula = String.Join(",", filtros.EstadosMatricula);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    _new.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.FaseOportunidadOrigen.Count() > 0)
                {
                    _new.FasesOportunidadOrigen = String.Join(",", filtros.FaseOportunidadOrigen);
                }
                if (filtros.FaseOportunidadDestino.Count() > 0)
                {
                    _new.FasesOportunidadDestino = String.Join(",", filtros.FaseOportunidadDestino);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _new.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                _new.OpcionFase = filtros.OpcionFase;
                _new.ControlFiltroFecha = filtros.ControlFiltroFecha;
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.CodigoMatricula = filtros.CodigoMatricula;
                _new.DocumentoIdentidad = filtros.DocumentoIdentidad;

                var data = _repReportes.ObtenerReporteSeguimientoOperaciones(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de seguimiento de oportunidad por fecha de registro de la campania
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteSeguimientoOportunidadesFiltrosDTO</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadesDTO</returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimientoOportunidadFC(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO filtroSeguimientoFinal = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroSeguimientoFinal.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroSeguimientoFinal.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroSeguimientoFinal.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                filtroSeguimientoFinal.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroSeguimientoFinal.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteSeguimientoFC(filtroSeguimientoFinal);

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de seguimiento de oportunidad por fecha de registro de la campania
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteSeguimientoOportunidadesFiltrosDTO</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadesDTO</returns>
        public List<ReporteSeguimientoOportunidadesDTO> ObtenerReporteSeguimientoOportunidadFRC(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                // La vista manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);

                SeguimientoFiltroFinalDTO filtroSeguimientoFinal = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroSeguimientoFinal.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroSeguimientoFinal.FasesOportunidad = string.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroSeguimientoFinal.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                filtroSeguimientoFinal.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroSeguimientoFinal.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteSeguimientoFRC(filtroSeguimientoFinal);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteSeguimientoOportunidadesModeloDTO> ObtenerReporteSeguimientoOportunidadProbabilidad(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }

                if (filtros.FasesOportunidad.Count() > 0)
                {
                    _new.FasesOportunidad = String.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.FaseOportunidadOrigen.Count() > 0)
                {
                    _new.FasesOportunidadOrigen = String.Join(",", filtros.FaseOportunidadOrigen);
                }
                if (filtros.FaseOportunidadDestino.Count() > 0)
                {
                    _new.FasesOportunidadDestino = String.Join(",", filtros.FaseOportunidadDestino);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _new.CentroCostos = String.Join(",", filtros.CentroCostos);
                }
                _new.OpcionFase = filtros.OpcionFase;
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteSeguimientoProbabilidad(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public ReporteCambioDeFaseDataDTO ReporteCambioDeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                ReporteCambioFaseFiltroProcesadoDTO newFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                newFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                newFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = newFiltro.FechaFin;
                filtros.FechaInicio = newFiltro.FechaInicio;
                var _queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdCategoriaOrigen in (" + String.Join(", ", filtros.CategoriaDatos) + ")";
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdTipoDato in (" + String.Join(", ", filtros.TipoDatos) + ")";
                //}
                newFiltro.Filtro = _queryFiltro;
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();
                data.ReporteTasaContacto = ReporteTasaContacto(newFiltro);
                data.ReporteControlBICYE = ReporteCambiosDeFaseControlBICYEAcumulado(newFiltro);
                data.ReporteCalidadProcesamiento = ReporteCalidadProcesamiento(filtros);
                data.ReporteMetasObtenerTotalIS = ReporteMetasObtenerTotalIS(filtros);
                if (filtros.Acumulado)
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadAcumulado(newFiltro);
                }
                else
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadNoAcumulado(newFiltro);
                }

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a Replica
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataV2DTO </returns>
        public ReporteCambioDeFaseDataV2DTO ReporteCambioDeFaseV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                ReporteCambioFaseFiltroProcesadoDTO newFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                ReporteCambioFaseFiltroProcedimientoDTO newFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                newFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                newFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = newFiltro.FechaFin;
                filtros.FechaInicio = newFiltro.FechaInicio;

                newFiltroProcedimiento.FechaFin = newFiltro.FechaFin;
                newFiltroProcedimiento.FechaInicio = newFiltro.FechaInicio;

                var _queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                    newFiltroProcedimiento.IdPersonal = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                    newFiltroProcedimiento.IdCentroCosto = String.Join(",", filtros.CentroCostos);
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdCategoriaOrigen in (" + String.Join(", ", filtros.CategoriaDatos) + ")";
                //    newFiltroProcedimiento.IdCategoriaOrigen = String.Join(",", filtros.CategoriaDatos);
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdTipoDato in (" + String.Join(", ", filtros.TipoDatos) + ")";
                //    newFiltroProcedimiento.IdTipo = String.Join(",", filtros.TipoDatos);
                //}
                newFiltro.Filtro = _queryFiltro;
                ReporteCambioDeFaseDataV2DTO data = new ReporteCambioDeFaseDataV2DTO();
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                if (DateTime.Compare(newFiltro.FechaInicio, fechaActual) == 0)
                {
                    data.ReporteTasaContacto = ReporteTasaContacto(newFiltro);
                    data.ReporteTasaContactoRn2 = ReporteTasaContactoRn2(newFiltroProcedimiento);
                    data.ControlCambiodeFase = ReporteControlCambiodeFase(filtros);
                }
                else {
                    data.ReporteTasaContacto = ReporteTasaContactoCongelado(newFiltro);
                    data.ReporteTasaContactoRn2 = ReporteTasaContactoRn2Congelado(newFiltroProcedimiento);
                    data.ControlCambiodeFase = ReporteControlCambiodeFaseCongelado(filtros);
                }
                data.ReporteTasaContactoConySinLlamada = ReporteTasaContactoConySinLlamada(newFiltro);
                data.ReporteControlBICYE = ReporteCambiosDeFaseControlBICYEAcumulado(newFiltro);
                data.ReporteMetasObtenerTotalIS = ReporteMetasObtenerTotalIS(filtros);
                if (filtros.Acumulado)
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadAcumuladoV2(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadConLlamada = ReporteCambiosDeFaseOportunidadAcumuladoConLlamada(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadSinLlamada = ReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(newFiltro);
                    data.ReporteControlRN1yRN2 = ReporteControlAcumuladoRN1yRN(newFiltro);
                }
                else
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadNoAcumuladoV2(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadConLlamada = ReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadSinLlamada = ReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(newFiltro);
                    data.ReporteControlRN1yRN2 = ReporteControlNoAcumuladoRN1yRN(newFiltro);
                }

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Cambio de Fase consultando a IntegraDB
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataDTO </returns>
        public ReporteCambioDeFaseDataDTO ReporteCambioDeFaseV2Integra(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                ReporteCambioFaseFiltroProcesadoDTO newFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                ReporteCambioFaseFiltroProcedimientoDTO newFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                newFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                newFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = newFiltro.FechaFin;
                filtros.FechaInicio = newFiltro.FechaInicio;

                newFiltroProcedimiento.FechaFin = newFiltro.FechaFin;
                newFiltroProcedimiento.FechaInicio = newFiltro.FechaInicio;

                var _queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                    newFiltroProcedimiento.IdPersonal = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                    newFiltroProcedimiento.IdCentroCosto = String.Join(",", filtros.CentroCostos);
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdCategoriaOrigen in (" + String.Join(", ", filtros.CategoriaDatos) + ")";
                //    newFiltroProcedimiento.IdCategoriaOrigen = String.Join(",", filtros.CategoriaDatos);
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdTipoDato in (" + String.Join(", ", filtros.TipoDatos) + ")";
                //    newFiltroProcedimiento.IdTipo = String.Join(",", filtros.TipoDatos);
                //}
                newFiltro.Filtro = _queryFiltro;
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();

                data.EjecutadasSinCambiodeFase = ReporteEjecutadasSinCambiodeFase(filtros);
                data.ActividadVencidaporTab = ReporteActividadesVencidasporTab(filtros);
                data.ReporteTasaDeCambio=ReporteTasaDeConversion(filtros);

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public ReporteTasaDeCambioDTO ReporteTasaDeConversion(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaDeConversion(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Tasas de Contacto
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ReporteTasaContacto(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaContacto(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Reporte de Tasas de Contacto
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ReporteTasaContactoCongelado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaContactoCongelado(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Reporte de Tasas de Contacto RN2
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ReporteTasaContactoRn2(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaContactoRn2(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Reporte de Tasas de Contacto RN2
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoDTO </returns>
        public ReporteTasaContactoDTO ReporteTasaContactoRn2Congelado(ReporteCambioFaseFiltroProcedimientoDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaContactoRn2Congelado(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Reporte de de Tasa de Contacto Con y Sin Llamada
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteTasaContactoConySinLlamadaDTO </returns>
        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                ReporteTasaContactoConySinLlamadaDTO item = new ReporteTasaContactoConySinLlamadaDTO();
                item = _repReportes.ObtenerReporteTasaContactoConySinLlamada(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public ReporteTasaContactoConySinLlamadaDTO ReporteTasaContactoConySinLlamadaRn2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                ReporteTasaContactoConySinLlamadaDTO item = new ReporteTasaContactoConySinLlamadaDTO();
                item = _repReportes.ObtenerReporteTasaContactoConySinLlamadaRn2(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadNoAcumulado(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene de Cambios de Fase por Oportunidad No Acumulado
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadNoAcumuladoV2(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene de Cambios de Fase por Oportunidad No Acumulado Con Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene de Cambios de Fase por Oportunidad No Acumulado Sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadAcumulado(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los cambios de fases acumulados
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoV2(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                return _repReportes.ReporteCambiosDeFaseOportunidadAcumuladoV2(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Control Acumulado Fase RN1 y RN2
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteControlAcumuladoRN1yRN(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene de Reporte de Control No Acumulado de Fase RN1 y RN
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteControlNoAcumuladoRN1yRN(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteControlNoAcumuladoRN1yRN(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Cambio de Fase Acumulado Con Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoConLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadAcumuladoConLlamada(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Cambio de Fase Acumulado Sin Llamada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                List<ReporteCambiosDeFaseOportunidadDTO> item = new List<ReporteCambiosDeFaseOportunidadDTO>();
                item = _repReportes.ReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(filtros);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de cambios de fase control BIC y E acumulado
        /// </summary>
        /// <param name="filtros"> Filtro de búsqueda </param>
        /// <returns> Lista de ObjetoDTO : List<ReporteCambiosDeFaseOportunidadDTO> </returns>
        public List<ReporteCambiosDeFaseOportunidadDTO> ReporteCambiosDeFaseControlBICYEAcumulado(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                return _repReportes.ReporteCambiosDeFaseControlBICYEAcumulado(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el reporte de calidad procesamiento
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamiento(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteCalidadProcesamiento(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de IS
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> int </returns>
        public int ReporteMetasObtenerTotalIS(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteMetasObtenerTotalIS(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de IS
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambiodeFaseV2DTO> ReporteControlCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteControlCambiodeFase(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 18/03/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el total de IS, funcion con data congelada
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ControlCambiodeFaseV2DTO> </returns>
        public List<ControlCambiodeFaseV2DTO> ReporteControlCambiodeFaseCongelado(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteControlCambiodeFaseCongelado(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Reporte de Oportunidades programadas sin cambio de fase
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte</param>
        /// <returns> Lista de ObjetoDTO: List<EjecutadasSinCambiodeFaseDTO> </returns>
        public List<EjecutadasSinCambiodeFaseDTO> ReporteEjecutadasSinCambiodeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteEjecutadasSinCambiodeFase(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<ActividadVencidaporTabPorDiaAgrupadoDTO> ReporteActividadesVencidasporTab(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteActividadesVencidasporTab(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ReporteTasaConversionConsolidadaAsesoresDTO ReporteTasaConversionConsolidadoAsesores(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                ReporteTasaConversionConsolidadaAsesoresDTO item = new ReporteTasaConversionConsolidadaAsesoresDTO();
                item = _repReportes.ReporteTasaConversionConsolidadoAsesores(area, subarea, pgeneral, pespecifico, modalidades, ciudades, fecha, coordinadores, asesores, fechaInicio, fechaFin);
                return item;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Clase: Reportes
        /// Autor: _ _ _ _ _ _ _ _ _.
        /// Fecha: 17/04/2021
        /// <summary>
        /// Genera Reporte de Tasa de Conversión Consolidado por Asesor según periodo Semanal
        /// </summary>
        /// <param name="coordinadores"> Id de coordinadores </param>
        /// <param name="asesores"> Id de asesores </param>
        /// <param name="periodoIinicio"> Id periodo inicio </param>
        /// <param name="periodoFin"> Id de periodo fin </param>
        /// <returns> ReporteTasaConversionConsolidadaGraficasVistaDTO <returns>
        public ReporteTasaConversionConsolidadaGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGrafica(string coordinadores, string asesores, string periodoIinicio, string periodoFin)
        {
            try
            {
                ReporteTasaConversionConsolidadaGraficasVistaDTO respuesta = new ReporteTasaConversionConsolidadaGraficasVistaDTO();
                respuesta = _repReportes.ReporteTasaConversionConsolidadoAsesoresGraficas(coordinadores, asesores, periodoIinicio, periodoFin);
                return respuesta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Clase: Reportes
        /// Autor: Edgar S.
        /// Fecha: 17/04/2021
        /// <summary>
        /// Genera Reporte de Tasa de Conversión Consolidado por Asesor según periodo Mensual
        /// </summary>
        /// <param name="coordinadores"> Id de coordinadores </param>
        /// <param name="asesores"> Id de asesores </param>
        /// <param name="periodoIinicio"> Id periodo inicio </param>
        /// <param name="periodoFin"> Id de periodo fin </param>
        /// <returns> ReporteTasaConversionConsolidadaMensualGraficasVistaDTO <returns>
        public ReporteTasaConversionConsolidadaMensualGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficaMensual(string coordinadores, string asesores, string periodoIinicio, string periodoFin)
        {
            try
            {
                ReporteTasaConversionConsolidadaMensualGraficasVistaDTO respuesta = new ReporteTasaConversionConsolidadaMensualGraficasVistaDTO();
                respuesta = _repReportes.ReporteTasaConversionConsolidadoAsesoresGraficasMensual(coordinadores, asesores, periodoIinicio, periodoFin);
                return respuesta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<ReporteControlCalidadProcesamientoDTO> ReporteControlCalidadProcesamiento(ReporteControlCalidadProcesamientoFiltrosDTO filtros)
        {
            try
            {
                ControlCalidadProcesamientoFiltroFinalDTO _new = new ControlCalidadProcesamientoFiltroFinalDTO();
                List<int> asesoresFinal = new List<int>();
                if (filtros.Coordinadores.Count() > 0 && filtros.Asesores.Count() == 0)
                {
                    asesoresFinal = asesoresFinal.Union(filtros.Coordinadores).ToList();
                    foreach (var item in filtros.Coordinadores)
                    {
                        var asesores = _repReportes.ObtenerSubordinadosCoordinador(item);
                        asesoresFinal = asesoresFinal.Union(asesores.Select(x => x.Id)).ToList();
                    }
                    _new.Asesores = String.Join(",", asesoresFinal);
                }
                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.Grupos.Count() > 0)
                {
                    _new.Grupos = String.Join(",", filtros.Grupos);
                }
                var fechaActual = DateTime.Now;
                _new.FechaFin = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(fechaActual.Year, fechaActual.Month, 1, 0, 0, 0);

                var data = _repReportes.ReporteControlCalidadProcesamiento(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteCalidadProspectoDTO> ReporteCalidadProspecto(ReporteCalidadProspectoFiltroDTO filtros)
        {
            try
            {
                ReporteCalidadProspectoFiltroFinalDTO _new = new ReporteCalidadProspectoFiltroFinalDTO();
                List<int> asesoresFinal = new List<int>();
                if (filtros.Coordinadores.Count() > 0 && filtros.Asesores.Count() == 0)
                {

                    foreach (var item in filtros.Coordinadores)
                    {
                        var asesores = _repReportes.ObtenerSubordinadosCoordinador(item);
                        asesoresFinal = asesoresFinal.Union(asesores.Select(x => x.Id)).ToList();
                    }
                    _new.Asesores = String.Join(",", asesoresFinal);
                }
                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.Grupos.Count() > 0)
                {
                    _new.Grupos = String.Join(",", filtros.Grupos);
                }

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ReporteCalidadProspecto(_new);
                foreach (var item in data)
                {
                    item.ValidacionPerfil = Math.Round(item.ValidacionPerfil, 2);
                    item.HistorialFinanciero = Math.Round(item.HistorialFinanciero, 2);
                    item.PGeneral = Math.Round(item.PGeneral, 2);
                    item.PlazoInicio = Math.Round(item.PlazoInicio, 2);
                    item.Competidores = Math.Round(item.Competidores, 2);
                    item.PEspecifico = Math.Round(item.PEspecifico, 2);
                    item.Beneficios = Math.Round(item.Beneficios, 2);
                    item.ProblemaSeleccionado = Math.Round(item.ProblemaSeleccionado, 2);
                    item.ProblemaSolucionado = Math.Round(item.ProblemaSolucionado, 2);

                    item.CalidadPromedio = Math.Round((
                        item.ValidacionPerfil +
                        item.HistorialFinanciero +
                        item.PGeneral +
                        item.PlazoInicio +
                        item.Competidores +
                        item.PEspecifico +
                        item.Beneficios +
                        item.ProblemaSeleccionado +
                        item.ProblemaSolucionado
                        ) / 9, 2);
                }

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<TasaContactoDataDTO> GenerarReporteTasaContactoIndividual(ReporteTasaContactoConsolidadoFiltroDTO filtros)
        {
            ReporteTasaContactoConsolidadoFiltroFinalDTO _new = new ReporteTasaContactoConsolidadoFiltroFinalDTO();
            List<int> asesoresFinal = new List<int>();
            var asesores = _repReportes.ObtenerSubordinadosCoordinador(filtros.IdCoordinador).Select(x => x.Id);
            _new.Asesores = String.Join(",", asesoresFinal);

            _new.FechaInicio = filtros.FechaInicio;

            var lista = GenerarReporteTasaContacto(_new);
            return lista;
        }
        public TasaContactoDataConsolidadoDTO GenerarReporteTasaContactoConsolidado(ReporteTasaContactoConsolidadoFiltroDTO filtros)
        {
            ReporteTasaContactoConsolidadoFiltroFinalDTO _new = new ReporteTasaContactoConsolidadoFiltroFinalDTO();
            var asesores = _repReportes.ObtenerSubordinadosNombreCoordinador(filtros.IdCoordinador).ToList();
            _new.Asesores = String.Join(",", asesores.Select(x => x.Id).ToList());

            _new.FechaInicio = filtros.FechaInicio;

            var lista = GenerarReporteTasaContacto(_new);

            TasaContactoDataConsolidadoDTO final = new TasaContactoDataConsolidadoDTO();
            final.Data = lista;
            final.Asesores = asesores;

            return final;
        }
        public List<TasaContactoDataDTO> GenerarReporteTasaContactoAsesor(ReporteTasaContactoAsesorFiltroDTO filtros)
        {
            ReporteTasaContactoConsolidadoFiltroFinalDTO _new = new ReporteTasaContactoConsolidadoFiltroFinalDTO();
            _new.Asesores = filtros.IdAsesor.ToString();

            _new.FechaInicio = filtros.FechaInicio;

            var lista = GenerarReporteTasaContacto(_new);

            return lista;
        }
        public List<TasaContactoDataDTO> GenerarReporteTasaContacto(ReporteTasaContactoConsolidadoFiltroFinalDTO filtros)
        {
            try
            {

                var hace_1dia = filtros.FechaInicio.AddDays(-1);
                var hace_2dia = filtros.FechaInicio.AddDays(-2);
                var hace_3dia = filtros.FechaInicio.AddDays(-3);
                var hace_4dia = filtros.FechaInicio.AddDays(-4);
                var hace_5dia = filtros.FechaInicio.AddDays(-5);

                var indicadores = _repReportes.ObtenerIndicadoresReporteControlOperativo();
                var parte1 = _repReportes.ReporteTasaContactoConsolidadoIndicadoresPrimeraParte(filtros.Asesores, filtros.FechaInicio).ToList();
                var parte2 = _repReportes.ReporteTasaContactoConsolidadoIndicadoresSegundaParte(filtros.Asesores, filtros.FechaInicio).ToList();
                var parte3 = _repReportes.ReporteTasaContactoConsolidadoIndicadoresTerceraParte(filtros.Asesores, filtros.FechaInicio).ToList();
                var parte4 = _repReportes.ReporteTasaContactoConsolidadoIndicadoresCuartaParte(filtros.Asesores, filtros.FechaInicio).ToList();

                var listafinal = parte1.Union(parte2).Union(parte3).Union(parte4);
                var agrupado = (from p in listafinal
                                group p by new
                                {
                                    p.Indicador

                                } into g
                                select new ReporteTasaContactoIndicadorDTO
                                {
                                    Orden = g.Key.Indicador,

                                    Lista = g.Select(o => new ReporteTasaContactoIndicadoresDTO
                                    {
                                        Indicador = o.Indicador,
                                        Asesor = o.Asesor,
                                        Valor = o.Valor,
                                        Dia = o.Dia

                                    }).ToList()
                                }).ToList();
                List<TasaContactoDataDTO> final = new List<TasaContactoDataDTO>();

                foreach (var item in indicadores)
                {

                    if (item.Orden == 25)
                    {
                        var llamadas = agrupado.Where(s => s.Orden == 6).Select(s => s.Lista).FirstOrDefault();
                        var cambiosfase = agrupado.Where(s => s.Orden == 24).Select(s => s.Lista).FirstOrDefault();

                        //se crea un contenedor para el valor del indicador 25
                        List<ReporteTasaContactoIndicadoresDTO> listatasa = new List<ReporteTasaContactoIndicadoresDTO>();


                        if (llamadas != null)
                        {
                            foreach (var sub in llamadas)
                            {

                                ReporteTasaContactoIndicadoresDTO item_lista = new ReporteTasaContactoIndicadoresDTO()
                                {
                                    Indicador = sub.Indicador,
                                    Asesor = sub.Asesor,
                                    Valor = (int)Math.Round(((double)cambiosfase.Where(s => s.Dia == sub.Dia).Select(s => s.Valor).FirstOrDefault() / sub.Valor) * 100, MidpointRounding.AwayFromZero),
                                    Dia = sub.Dia

                                };
                                listatasa.Add(item_lista);
                            }
                        }

                        /**
                         * se crea un objeto item_indicador
                         * se agrega a final
                         **/
                        TasaContactoDataDTO item_indicador = new TasaContactoDataDTO()
                        {
                            Indicador = item.Nombre,
                            Orden = item.Orden,
                            Lista = listatasa,
                            Hoy_date = filtros.FechaInicio,
                            Hace_5_date = hace_5dia,
                            Hace_4_date = hace_4dia,
                            Hace_3_date = hace_3dia,
                            Hace_2_date = hace_2dia,
                            Hace_1_date = hace_1dia

                        };
                        final.Add(item_indicador);
                    }
                    else
                    {

                        TasaContactoDataDTO item_indicador = new TasaContactoDataDTO()
                        {
                            Indicador = item.Nombre,
                            Orden = item.Orden,
                            Lista = agrupado.Where(s => s.Orden == item.Orden).Select(s => s.Lista).FirstOrDefault(),
                            Hoy_date = filtros.FechaInicio,
                            Hace_5_date = hace_5dia,
                            Hace_4_date = hace_4dia,
                            Hace_3_date = hace_3dia,
                            Hace_2_date = hace_2dia,
                            Hace_1_date = hace_1dia

                        };
                        final.Add(item_indicador);

                    }
                }
                return final.OrderBy(o => o.Orden).ToList(); ;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<TCRM_CentroCostoByAsesorDTO> ObtenerCentroCostoPorAsesor(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<TCRM_CentroCostoByAsesorDTO> item = new List<TCRM_CentroCostoByAsesorDTO>();
                return _repReportes.ObtenerCentroCostoPorAsesor(area, subarea, pgeneral, pespecifico, modalidades, ciudades, fecha, coordinadores, asesores, fechaInicio, fechaFin);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        public List<TCRM_CentroCostoByAsesorDetallesDTO> ObtenerCentroCostoPorAsesorDetalles(string area, string subarea, string pgeneral, string pespecifico, string modalidades, string ciudades, bool fecha, string coordinadores, string asesores, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<TCRM_CentroCostoByAsesorDetallesDTO> item = new List<TCRM_CentroCostoByAsesorDetallesDTO>();
                return _repReportes.ObtenerCentroCostoPorAsesorDetalles(area, subarea, pgeneral, pespecifico, modalidades, ciudades, fecha, coordinadores, asesores, fechaInicio, fechaFin);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public ReporteContactabilidadDataDTO ReporteContactabilidad(ReporteContactabilidadFiltroDTO filtros)
        {
            try
            {
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                ReporteContactabilidadFiltroFinalDTO _new = new ReporteContactabilidadFiltroFinalDTO();
                List<int> asesoresFinal = new List<int>();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    _new.AsesoresComparativo = String.Join(",", filtros.AsesoresComparativos);
                }
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.Tipo = filtros.Tipo;



                var data = _repReportes.ObtenerReporteContactabilidad(_new);
                List<ReporteContactabilidadAsesorAgrupadoDTO> data2 = new List<ReporteContactabilidadAsesorAgrupadoDTO>();
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    var result2 = _repReportes.ObtenerReporteContactabilidadAsesorComparativo(_new);
                    data2 = (from p in result2
                             group p by new
                             {
                                 p.IdAsesor,
                                 p.NombreAsesor
                             } into g
                             select new ReporteContactabilidadAsesorAgrupadoDTO
                             {
                                 IdAsesor = g.Key.IdAsesor,
                                 NombreAsesor = g.Key.NombreAsesor,
                                 Lista = g.Select(o => new TasaContactabilidadDTO
                                 {
                                     Hora = o.Hora,
                                     TC = o.TC,
                                     Clave = o.Clave,
                                     AT = o.AT,
                                     TE = o.TE

                                 }).ToList()
                             }).ToList();
                }



                ReporteContactabilidadDataDTO result = new ReporteContactabilidadDataDTO();
                result.AsesorContactabilidad = data;
                result.ComparativoAsesor = data2;

                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public ReporteContactabilidadDataV2DTO ReporteContactabilidadV2(ReporteContactabilidadFiltroDTO filtros)
        {
            try
            {
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                filtros.AsesoresComparativos = filtros.AsesoresComparativos == null ? new List<int>() : filtros.AsesoresComparativos;

                ReporteContactabilidadFiltroFinalDTO _new = new ReporteContactabilidadFiltroFinalDTO();
                List<int> asesoresFinal = new List<int>();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    _new.AsesoresComparativo = String.Join(",", filtros.AsesoresComparativos);
                }
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.Tipo = filtros.Tipo;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);
                List<ReporteContactabilidadDTO> data = new List<ReporteContactabilidadDTO>();
                if (DateTime.Compare(_new.FechaInicio, fechaActual) ==0)
                {
                    data = _repReportes.ObtenerReporteContactabilidadV2(_new);
                }
                else
                {
                    data = _repReportes.ObtenerReporteContactabilidadCongelado(_new);
                }
                List<ReporteContactabilidadAgrupadoDTO> data2 = new List<ReporteContactabilidadAgrupadoDTO>();
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    var result2 = _repReportes.ObtenerReporteContactabilidadAsesorComparativoV2(_new);
                    data2 = (from p in result2
                             group p by new
                             {
                                 p.IdAsesor,
                             } into g
                             select new ReporteContactabilidadAgrupadoDTO
                             {
                                 IdAsesor = g.Key.IdAsesor,
                                 Lista = g.Select(o => new ReporteContactabilidadAsesorIndicadoresDTO
                                 {
                                     IdAsesor = o.IdAsesor,
                                     Hora = o.Hora,
                                     Clave = o.Clave,
                                     Valor = o.Valor,
                                     Tipo = o.Tipo,
                                     TotalLlamadas = o.TotalLlamadas,
                                 }).ToList()
                             }).ToList();
                }
                var data3 = _repReportes.ObtenerReporteContactabilidadMinutos(_new);


                ReporteContactabilidadDataV2DTO result = new ReporteContactabilidadDataV2DTO();
                result.AsesorContactabilidad = data;
                result.ComparativoAsesor = data2;
                result.MinutosContactabilidad = data3;

                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public ReporteContactabilidadDataV2DTO ReporteContactabilidadDesagregado(ReporteContactabilidadFiltroDTO filtros)
        {
            try
            {
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                ReporteContactabilidadFiltroFinalDTO _new = new ReporteContactabilidadFiltroFinalDTO();
                List<int> asesoresFinal = new List<int>();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    _new.AsesoresComparativo = String.Join(",", filtros.AsesoresComparativos);
                }
                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.Tipo = filtros.Tipo;

                var data = _repReportes.ObtenerReporteContactabilidadDesagregado(_new);
                List<ReporteContactabilidadAgrupadoDTO> data2 = new List<ReporteContactabilidadAgrupadoDTO>();
                if (filtros.AsesoresComparativos.Count() > 0)
                {
                    var result2 = _repReportes.ObtenerReporteContactabilidadAsesorComparativoV2(_new);
                    data2 = (from p in result2
                             group p by new
                             {
                                 p.IdAsesor,
                             } into g
                             select new ReporteContactabilidadAgrupadoDTO
                             {
                                 IdAsesor = g.Key.IdAsesor,
                                 Lista = g.Select(o => new ReporteContactabilidadAsesorIndicadoresDTO
                                 {
                                     IdAsesor = o.IdAsesor,
                                     Hora = o.Hora,
                                     Clave = o.Clave,
                                     Valor = o.Valor,
                                     Tipo = o.Tipo,
                                     TotalLlamadas = o.TotalLlamadas,
                                 }).ToList()
                             }).ToList();
                }



                ReporteContactabilidadDataV2DTO result = new ReporteContactabilidadDataV2DTO();
                result.AsesorContactabilidad = data;
                result.ComparativoAsesor = data2;

                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteSeguimientoOportunidadesRN2DTO> ObtenerReporteSeguimientoOportunidadRN2(ReporteSeguimientoOportunidadesRN2FiltrosDTO filtros)
        {
            try
            {
                //LA vista Manda cinco horas Adelantadas
                filtros.FechaFin = filtros.FechaFin.AddHours(-5);
                filtros.FechaInicio = filtros.FechaInicio.AddHours(-5);
                SeguimientoFiltroFinalDTO _new = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    _new.Asesores = String.Join(",", filtros.Asesores);
                }

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteSeguimientoRN2(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteContactabilidadPeriodoAgrupadoDTO> ReporteContactabilidadPeriodo(ReporteContactabilidadPeriodoFiltroDTO filtros)
        {
            try
            {
                string asesores = String.Empty;
                if (filtros.Asesores.Count() > 0)
                {
                    asesores = String.Join(",", filtros.Asesores);
                }
                List<ReporteContactabilidadPeriodoDTO> datosBD = new List<ReporteContactabilidadPeriodoDTO>();
                if (filtros.Asesores.Count() == 0)
                {
                    datosBD = _repReportes.ReporteContactabilidadPeriodo(filtros.Periodo);
                }
                else
                {
                    datosBD = _repReportes.ReporteContactabilidadPeriodoAsesor(filtros.Periodo, asesores);
                }

                List<ReporteContactabilidadPeriodoAgrupadoDTO> result = new List<ReporteContactabilidadPeriodoAgrupadoDTO>();

                result = (from p in datosBD
                          group p by new
                          {
                              p.IdCodigoPais,
                          } into g
                          select new ReporteContactabilidadPeriodoAgrupadoDTO
                          {
                              IdCodigoPais = g.Key.IdCodigoPais,
                              Lista = g.Select(o => new ReporteContactabilidadPeriodoDTO
                              {
                                  IdPersonal = o.IdPersonal,
                                  Dia = o.Dia,
                                  IdCodigoPais = o.IdCodigoPais,
                                  TotalActividades = o.TotalActividades,
                                  TotalEjecutadas = o.TotalEjecutadas,
                                  TasaConversion = o.TasaConversion,
                                  NombreAsesor = o.NombreAsesor
                              }).ToList()
                          }).ToList();
                return result;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ProcesadoDataActividadesRealizadasDTO> ReporteActividadesRealizadasOperaciones(ReporteActividadesRealizadasFiltrosDTO filtros)
        {
            try
            {
                filtros.Fecha = filtros.Fecha.AddHours(-5);
                DateTime fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 23, 59, 59);
                if (filtros.EstadoFiltroHora)
                {
                    fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraInicio, filtros.MinutosInicio, 0);
                    fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraFin, filtros.MinutosFin, 0);

                }
                var data = _repReportes.ObtenerReporteActividadesRealizadasOperaciones(filtros, fechaInicio, fechaFin);

                var result = (from p in data
                              group p by new
                              {
                                  p.IdActividad,
                                  p.NombreCentroCosto,
                                  p.NombreCompletoContacto,
                                  p.CodigoFaseFinal,
                                  p.NombreTipoDato,
                                  p.NombreOrigen,
                                  p.FechaProgramada,
                                  p.FechaReal,
                                  p.NombreActividadCabecera,
                                  p.NombreOcurrencia,
                                  p.ComentarioActividad,
                                  p.NombreCompletoAsesor,
                                  p.IdAlumno,
                                  p.IdOportunidad,
                                  p.ProbabilidadActual,
                                  p.CodigoFaseOrigen,
                                  p.IdFaseOportunidadInicial,
                                  p.FechaModificacion,
                                  p.NombreCategoriaOrigen,
                                  p.EstadoOcurrencia,
                                  p.NombreGrupo,
                              } into g
                              select new CompuestoActividadesRealizadasDTO
                              {
                                  IdActividad = g.Key.IdActividad,
                                  NombreCentroCosto = g.Key.NombreCentroCosto,
                                  NombreCompletoContacto = g.Key.NombreCompletoContacto,
                                  CodigoFaseFinal = g.Key.CodigoFaseFinal,
                                  NombreTipoDato = g.Key.NombreTipoDato,
                                  NombreOrigen = g.Key.NombreOrigen,
                                  FechaProgramada = g.Key.FechaProgramada,
                                  FechaReal = g.Key.FechaReal,
                                  NombreActividadCabecera = g.Key.NombreActividadCabecera,
                                  NombreOcurrencia = g.Key.NombreOcurrencia,
                                  ComentarioActividad = g.Key.ComentarioActividad,
                                  NombreCompletoAsesor = g.Key.NombreCompletoAsesor,
                                  IdAlumno = g.Key.IdAlumno,
                                  IdOportunidad = g.Key.IdOportunidad,
                                  ProbabilidadActual = g.Key.ProbabilidadActual,
                                  CodigoFaseOrigen = g.Key.CodigoFaseOrigen,
                                  IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                  FechaModificacion = g.Key.FechaModificacion,
                                  NombreCategoriaOrigen = g.Key.NombreCategoriaOrigen,
                                  EstadoOcurrencia = g.Key.EstadoOcurrencia,
                                  NombreGrupo = g.Key.NombreGrupo,

                                  LlamadasIntegra = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdLlamadaWebphone,
                                      DuracionTimbrado = o.DuracionTimbrado,
                                      DuracionContesto = o.DuracionContesto,
                                      FechaInicioLlamada = o.FechaInicioLlamada,
                                      FechaFinLlamada = o.FechaFinLlamada,
                                      EstadoLlamada = null,
                                      SubEstadoLlamada = null,
                                      NombreGrabacion = o.NombreGrabacionIntegra,
                                      Webphone = o.Webphone

                                  }).OrderByDescending(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                  LlamadasCentral = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdTresCX,
                                      DuracionTimbrado = o.DuracionTimbradoTresCx,
                                      DuracionContesto = o.DuracionContestoTresCx,
                                      FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                                      FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                      EstadoLlamada = o.EstadoLlamadaTresCX,
                                      SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                      NombreGrabacion = o.NombreGrabacionTresCX,

                                  }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                              }).OrderBy(x => x.FechaReal);

                List<ProcesadoDataActividadesRealizadasDTO> final = new List<ProcesadoDataActividadesRealizadasDTO>();

                //Variables Temporales ------------
                var flag = false;
                var count = 0;
                double minutos = 0;
                double totalContesto = 0;
                double totalTimbrado = 0;
                double totalPerdido = 0;
                double mayorPerdido = 0;
                double minutosTotalLlamada = 0;
                DateTime fechaTemp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

                int diaFecha = filtros.Fecha.Day;
                //Fin Variables Temporales --------

                foreach (var item in result)
                {
                    ProcesadoDataActividadesRealizadasDTO itemDetalle = new ProcesadoDataActividadesRealizadasDTO()
                    {
                        IdActividad = item.IdActividad,
                        NombreCentroCosto = item.NombreCentroCosto,
                        NombreCompletoContacto = item.NombreCompletoContacto,
                        CodigoFaseFinal = item.CodigoFaseFinal,
                        NombreTipoDato = item.NombreTipoDato,
                        NombreOrigen = item.NombreOrigen,
                        FechaProgramada = item.FechaProgramada,
                        FechaReal = item.FechaReal,
                        NombreActividadCabecera = item.NombreActividadCabecera,
                        NombreOcurrencia = item.NombreOcurrencia,
                        ComentarioActividad = item.ComentarioActividad,
                        NombreCompletoAsesor = item.NombreCompletoAsesor,
                        IdAlumno = item.IdAlumno,
                        IdOportunidad = item.IdOportunidad,
                        ProbabilidadActual = item.ProbabilidadActual,
                        CodigoFaseOrigen = item.CodigoFaseOrigen,
                        NombreCategoriaOrigen = item.NombreCategoriaOrigen,
                        EstadoOcurrencia = item.EstadoOcurrencia,
                        NombreGrupo = item.NombreGrupo,
                    };

                    if (item.LlamadasIntegra.Count() > 0)
                    {
                        var ordenLlamadas = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                        var fechaUltima = ordenLlamadas.Select(s => s.FechaInicioLlamada).FirstOrDefault();
                        var primeraLlamda = item.LlamadasIntegra.FirstOrDefault();
                        if (count > 0 && flag)
                        {
                            if (diaFecha == fechaTemp.Day)
                            {
                                var min = ((fechaUltima.Value - fechaTemp).TotalSeconds / 60).ToString("0.0");
                                minutos = Convert.ToDouble(min);
                            }
                            else
                            {
                                minutos = 0;
                            }
                        }
                        if (fechaUltima != null)
                        {
                            flag = true;
                            fechaTemp = item.LlamadasIntegra.Select(x => x.FechaInicioLlamada).FirstOrDefault().Value;
                            double contesto = Convert.ToDouble(primeraLlamda.DuracionContesto);
                            double timbrado = Convert.ToDouble(primeraLlamda.DuracionTimbrado);
                            fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                        }
                        totalTimbrado += ((item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum());
                        totalContesto += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();


                        if (minutos >= 0)
                        {
                            totalPerdido += minutos;
                        }

                        item.LlamadasIntegra = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();

                        var htmlGrabacionesCentral = "";
                        var htmlGrabacionesIntegra = "";

                        foreach (var llamada in item.LlamadasCentral)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                htmlGrabacionesCentral = htmlGrabacionesCentral + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                            }
                            htmlGrabacionesCentral = htmlGrabacionesCentral + "</br>";

                        }
                        var primeraFechaFin = DateTime.Now;
                        int ContadorLlamadas = 0;
                        double minutosLlamada = 0;

                        foreach (var llamada in item.LlamadasIntegra)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                               
                                if (llamada.Webphone == "Silcom")
                                {
                                    htmlGrabacionesIntegra = htmlGrabacionesIntegra + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamadaNuevoWebPhone('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";

                                }
                                else if (llamada.Webphone == "Silcom Migrado")
                                {
                                    htmlGrabacionesIntegra = htmlGrabacionesIntegra + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamadaNuevoWebPhoneMigrado('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";

                                }
                                else
                                {
                                    htmlGrabacionesIntegra = "";
                                }

                            }
                            htmlGrabacionesIntegra = htmlGrabacionesIntegra + "</br>";
                            llamada.MinutosPerdidos = "";
                            if (ContadorLlamadas > 0)
                            {

                                var min = ((llamada.FechaInicioLlamada.Value - primeraFechaFin).TotalSeconds / 60);
                                if (min < 0) min = 0;
                                var minTexto = min.ToString("0.0");
                                minutosLlamada = Convert.ToDouble(min);
                                string estilo = ClasificarColorLlamadaRealizadasActividadesMinutos(minutosLlamada);
                                llamada.MinutosPerdidos = "<div id = 'RowIntervalLlamada'" + estilo + ">" + minTexto + " min.</div>";
                            }
                            primeraFechaFin = llamada.FechaFinLlamada.Value;
                            minutosTotalLlamada += minutosLlamada;

                            ContadorLlamadas++;


                        }



                        itemDetalle.TiemposDuracionLlamadas = String.Concat(item.LlamadasIntegra.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " min.<strong > TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + " min.<br />"));
                        itemDetalle.FechaLlamada = String.Concat(item.LlamadasIntegra.Select(o => o.MinutosPerdidos + "<strong >I: </strong >" + o.FechaInicioLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaFinLlamada.Value.ToString("HH:mm:ss") + "<br />"));

                        itemDetalle.NombreGrabacionTresCX = htmlGrabacionesCentral;
                        itemDetalle.NombreGrabacionIntegra = htmlGrabacionesIntegra;
                    }
                    itemDetalle.MinutosTotalIntervaleLlamadas = minutosTotalLlamada;
                    itemDetalle.MinutosIntervale = minutos;
                    itemDetalle.MinutosTotalContesto = totalContesto;
                    itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                    itemDetalle.MinutosTotalPerdido = totalPerdido;
                    count++;

                    mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                    itemDetalle.MayorTiempo = mayorPerdido;

                    itemDetalle.TiemposTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " min. <strong >TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + " min. <br />"));
                    itemDetalle.EstadosTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                    itemDetalle.ExisteLlamadaExitosa = itemDetalle.EstadosTresCX.Contains("Llamada Exitosa");
                    //var listaActividades = _repReportes.ReporteActividadOcurrencia(item.IdOportunidad);
                    //itemDetalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion).Count();
                    //itemDetalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaNoEjecutado && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion).Count();
                    //itemDetalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == ValorEstatico.IdEstadoOcurrenciaAsignacionManual && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion).Count();
                    itemDetalle.TotalEjecutadas = 0;
                    itemDetalle.TotalNoEjecutadas = 0;
                    itemDetalle.TotalAsignacionManual = 0;


                    final.Add(itemDetalle);
                }
                if (filtros.EstadoLlamada != null)
                {
                    if (filtros.EstadoLlamada == 1)
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == true).ToList();
                    }
                    else
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == false).ToList();
                    }

                }

                var dataFinal = final.OrderByDescending(s => s.FechaReal).ToList();


                return dataFinal;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        /// <summary>
        /// Genera Reporte de Actividades Realizadas
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda </param>
        /// <returns> Información de Actividades Realizadas: List<ProcesadoDataActividadesRealizadasDTO> <returns>
        public List<ProcesadoDataActividadesRealizadasDTO> ReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtros)
        {
            try
            {
                filtros.Fecha = filtros.Fecha.AddHours(-5);
                var esActual = false;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);

                DateTime fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 0, 0, 0);
                if (DateTime.Compare(fechaInicio, fechaActual) == 0)
                {
                    esActual = true;
                }
                DateTime fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, 23, 59, 59);
                if (filtros.EstadoFiltroHora)
                {
                    fechaInicio = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraInicio, filtros.MinutosInicio, 0);
                    fechaFin = new DateTime(filtros.Fecha.Year, filtros.Fecha.Month, filtros.Fecha.Day, filtros.HoraFin, filtros.MinutosFin, 0);

                }
                var data = new List<ReporteRealizadaDataDTO>();
                
                if (esActual)
                {
                    data = _repReportes.ObtenerReporteActividadesRealizadas(filtros, fechaInicio, fechaFin);
                }
                else
                {
                    data = _repReportes.ObtenerReporteActividadesRealizadasCongelado(filtros, fechaInicio, fechaFin);

                }
                List<CompuestoActividadesRealizadasDTO> resultado = new List<CompuestoActividadesRealizadasDTO>();
                List<CompuestoActividadesRealizadasDTO> resultadoAux = new List<CompuestoActividadesRealizadasDTO>();
                var result = (from p in data
                              group p by new
                              {
                                  p.IdActividad,
                                  p.NombreCentroCosto,
                                  p.NombreCompletoContacto,
                                  p.CodigoFaseFinal,
                                  p.NombreTipoDato,
                                  p.NombreOrigen,
                                  p.FechaProgramada,
                                  p.FechaReal,
                                  p.NombreActividadCabecera,
                                  p.NombreOcurrencia,
                                  p.ComentarioActividad,
                                  p.NombreCompletoAsesor,
                                  p.IdAlumno,
                                  p.IdOportunidad,
                                  p.ProbabilidadActual,
                                  p.CodigoFaseOrigen,
                                  p.IdFaseOportunidadInicial,
                                  p.FechaModificacion,
                                  p.NombreCategoriaOrigen,
                                  p.EstadoOcurrencia,
                                  p.NombreGrupo,
                              } into g
                              select new CompuestoActividadesRealizadasDTO
                              {
                                  IdActividad = g.Key.IdActividad,
                                  NombreCentroCosto = g.Key.NombreCentroCosto,
                                  NombreCompletoContacto = g.Key.NombreCompletoContacto,
                                  CodigoFaseFinal = g.Key.CodigoFaseFinal,
                                  NombreTipoDato = g.Key.NombreTipoDato,
                                  NombreOrigen = g.Key.NombreOrigen,
                                  FechaProgramada = g.Key.FechaProgramada,
                                  FechaReal = g.Key.FechaReal,
                                  NombreActividadCabecera = g.Key.NombreActividadCabecera,
                                  NombreOcurrencia = g.Key.NombreOcurrencia,
                                  ComentarioActividad = g.Key.ComentarioActividad,
                                  NombreCompletoAsesor = g.Key.NombreCompletoAsesor,
                                  IdAlumno = g.Key.IdAlumno,
                                  IdOportunidad = g.Key.IdOportunidad,
                                  ProbabilidadActual = g.Key.ProbabilidadActual,
                                  CodigoFaseOrigen = g.Key.CodigoFaseOrigen,
                                  IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                  FechaModificacion = g.Key.FechaModificacion,
                                  NombreCategoriaOrigen = g.Key.NombreCategoriaOrigen,
                                  EstadoOcurrencia = g.Key.EstadoOcurrencia,
                                  NombreGrupo = g.Key.NombreGrupo,

                                  LlamadasIntegra = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdLlamadaWebphone,
                                      DuracionTimbrado = o.DuracionTimbrado,
                                      DuracionContesto = o.DuracionContesto,
                                      FechaInicioLlamada = o.FechaInicioLlamada,
                                      FechaFinLlamada = o.FechaFinLlamada,
                                      EstadoLlamada = null,
                                      SubEstadoLlamada = null,
                                      NombreGrabacion = o.NombreGrabacionIntegra,
                                      Webphone=o.Webphone
                                  }).OrderByDescending(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                  LlamadasCentral = g.Select(o => new InformacionLlamadaDTO
                                  {
                                      Id = o.IdTresCX,
                                      DuracionTimbrado = o.DuracionTimbradoTresCx,
                                      DuracionContesto = o.DuracionContestoTresCx,
                                      FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                                      FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                      EstadoLlamada = o.EstadoLlamadaTresCX,
                                      SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                      NombreGrabacion = o.NombreGrabacionTresCX,

                                  }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                              }).OrderBy(x => x.FechaReal);

                List<ProcesadoDataActividadesRealizadasDTO> final = new List<ProcesadoDataActividadesRealizadasDTO>();

                resultado = result.ToList();
                for (int i = 0; i < resultado.Count() ; i++)
                {
                    for (int j = i + 1; j < resultado.Count() ; j++)
                    {
                        if (resultado[i].IdActividad == resultado[j].IdActividad)
                        {
                            resultado[j].IdActividad = 0;
                            resultado[i].CodigoFaseFinal = resultado[j].CodigoFaseFinal;
                        }
                    }
                }

                foreach (var item in resultado)
                {
                    if (item.IdActividad != 0)
                    {
                        resultadoAux.Add(item);
                    }
                }

                //Variables Temporales ------------
                var flag = false;
                var count = 0;
                double minutos = 0;
                double totalContesto = 0;
                double totalTimbrado = 0;
                double totalPerdido = 0;
                double mayorPerdido = 0;
                double minutosTotalLlamada = 0;
                DateTime fechaTemp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

                int diaFecha = filtros.Fecha.Day;
                //Fin Variables Temporales --------

                foreach (var item in resultadoAux)
                {
                    ProcesadoDataActividadesRealizadasDTO itemDetalle = new ProcesadoDataActividadesRealizadasDTO()
                    {
                        IdActividad = item.IdActividad,
                        NombreCentroCosto = item.NombreCentroCosto,
                        NombreCompletoContacto = item.NombreCompletoContacto,
                        CodigoFaseFinal = item.CodigoFaseFinal,
                        NombreTipoDato = item.NombreTipoDato,
                        NombreOrigen = item.NombreOrigen,
                        FechaProgramada = item.FechaProgramada,
                        FechaReal = item.FechaReal,
                        NombreActividadCabecera = item.NombreActividadCabecera,
                        NombreOcurrencia = item.NombreOcurrencia,
                        ComentarioActividad = item.ComentarioActividad,
                        NombreCompletoAsesor = item.NombreCompletoAsesor,
                        IdAlumno = item.IdAlumno,
                        IdOportunidad = item.IdOportunidad,
                        ProbabilidadActual = item.ProbabilidadActual,
                        CodigoFaseOrigen = item.CodigoFaseOrigen,
                        NombreCategoriaOrigen = item.NombreCategoriaOrigen,
                        EstadoOcurrencia = item.EstadoOcurrencia,
                        NombreGrupo = item.NombreGrupo,
                    };

                    if (item.LlamadasIntegra.Count() > 0)
                    {
                        var ordenLlamadas = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                        var fechaUltima = ordenLlamadas.Select(s => s.FechaInicioLlamada).FirstOrDefault();
                        var primeraLlamda = item.LlamadasIntegra.FirstOrDefault();
                        if (count > 0 && flag)
                        {
                            if (diaFecha == fechaTemp.Day)
                            {
                                var min = ((fechaUltima.Value - fechaTemp).TotalSeconds).ToString("0.0");
                                minutos = Convert.ToDouble(min);
                            }
                            else
                            {
                                minutos = 0;
                            }
                        }
                        if (fechaUltima != null)
                        {
                            flag = true;
                            fechaTemp = item.LlamadasIntegra.Select(x => x.FechaInicioLlamada).FirstOrDefault().Value;
                            double contesto = Convert.ToDouble(primeraLlamda.DuracionContesto);
                            double timbrado = Convert.ToDouble(primeraLlamda.DuracionTimbrado);
                            fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                        }
                        totalTimbrado += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                        totalContesto += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                        if (minutos >= 0)
                        {
                            totalPerdido += minutos;
                        }

                        item.LlamadasIntegra = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();

                        var htmlGrabacionesCentral = "";
                        var htmlGrabacionesIntegra = "";

                        foreach (var llamada in item.LlamadasCentral)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                htmlGrabacionesCentral = htmlGrabacionesCentral + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamada3CX('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";
                            }
                            htmlGrabacionesCentral = htmlGrabacionesCentral + "</br>";

                        }
                        var primeraFechaFin = DateTime.Now;
                        int contadorLlamadas = 0;
                        double minutosLlamada = 0;

                        foreach (var llamada in item.LlamadasIntegra)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                if(llamada.Webphone=="Silcom")
                                {
                                    htmlGrabacionesIntegra = htmlGrabacionesIntegra + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamadaNuevoWebPhone('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";

                                }
                                else if (llamada.Webphone == "Silcom Migrado")
                                {
                                    htmlGrabacionesIntegra = htmlGrabacionesIntegra + "<a class='ex1' style='cursor: pointer;' onclick=\"return reproducirLlamadaNuevoWebPhoneMigrado('" + llamada.NombreGrabacion + "')\"><b>Escuchar</b></a>";

                                }
                                else
                                {
                                    htmlGrabacionesIntegra = "";
                                }

                            }
                            htmlGrabacionesIntegra = htmlGrabacionesIntegra + "</br>";
                            llamada.MinutosPerdidos = "";
                            if (contadorLlamadas > 0)
                            {

                                var min = ((llamada.FechaInicioLlamada.Value - primeraFechaFin).TotalMinutes);//segundos.
                                if (min < 0) min = 0;
                                var minTexto = min.ToString("0.0");
                                minutosLlamada = Convert.ToDouble(minTexto);
                                string estilo = ClasificarColorLlamadaRealizadasActividadesMinutos(minutosLlamada);
                                llamada.MinutosPerdidos = "<table '><tr><td style='margin: 3px; padding: 1px; width: 100 %; border: 1px solid black'><div id = 'RowIntervalLlamada'" + estilo + ">" + minTexto + " m</div></td></tr></table>";
                            }
                            primeraFechaFin = llamada.FechaFinLlamada.Value;
                            minutosTotalLlamada += minutosLlamada;
                            
                            contadorLlamadas++;


                        }
                        itemDetalle.TiemposDuracionLlamadas = String.Concat(item.LlamadasIntegra.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " m<strong > - TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + " m<br />"));
                        itemDetalle.FechaLlamada = String.Concat(item.LlamadasIntegra.Select(o => o.MinutosPerdidos + "<table '><tr><td style='margin: 3px; padding: 3px; width: 50 %; border: 1px solid black'><strong >I: </strong >" + o.FechaInicioLlamada.Value.ToString("HH:mm") + "<strong> - T: </strong >" + o.FechaFinLlamada.Value.ToString("HH:mm") + "</td></tr></table>"));

                        itemDetalle.NombreGrabacionTresCX = htmlGrabacionesCentral;
                        itemDetalle.NombreGrabacionIntegra = htmlGrabacionesIntegra;
                    }
                    itemDetalle.MinutosTotalIntervaleLlamadas = minutosTotalLlamada;
                    itemDetalle.MinutosIntervale = minutos;
                    itemDetalle.MinutosTotalContesto = totalContesto;
                    itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                    itemDetalle.MinutosTotalPerdido = totalPerdido;
                    count++;

                    mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                    itemDetalle.MayorTiempo = mayorPerdido;

                    itemDetalle.TiemposTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >TT: </strong >" + ((double)(o.DuracionTimbrado == null ? 0 : o.DuracionTimbrado) / 60).ToString("0.0") + " m <strong >TC:</strong > " + ((double)(o.DuracionContesto == null ? 0 : o.DuracionContesto) / 60).ToString("0.0") + "m <br />"));
                    itemDetalle.EstadosTresCX = String.Concat(item.LlamadasCentral.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                    itemDetalle.ExisteLlamadaExitosa = itemDetalle.EstadosTresCX.Contains("Llamada Exitosa");
                    itemDetalle.TotalEjecutadas = 0;
                    itemDetalle.TotalNoEjecutadas = 0;
                    itemDetalle.TotalAsignacionManual = 0;


                    final.Add(itemDetalle);
                }
                if (filtros.EstadoLlamada != null)
                {
                    if (filtros.EstadoLlamada == 1)
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == true).ToList();
                    }
                    else
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == false).ToList();
                    }

                }

                var dataFinal = final.OrderByDescending(s => s.FechaReal).ToList();


                return dataFinal;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene html con valores de estilo según tiempo
        /// </summary>
        /// <param name="minutosReal"></param>
        /// <returns> Html de configuración de estilo: String </returns>
        private string ClasificarColorLlamadaRealizadasActividadesSegundos(double minutosReal)
        {
            string stiloFinal = String.Empty;
            string colorRow = String.Empty;
            string colorTexto = String.Empty;
            if (minutosReal < 120) { colorRow = "blue"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 121 && minutosReal < 180) { colorRow = "skyblue"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 181 && minutosReal < 300) { colorRow = "yellow"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 301 && minutosReal <= 480) { colorRow = "orange"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal > 480) { colorRow = "red"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            return stiloFinal;
        }

        /// <summary>
        /// Obtiene html con valores de estilo según tiempo
        /// </summary>
        /// <param name="minutosReal"></param>
        /// <returns> Html de configuración de estilo: String </returns>
        private string ClasificarColorLlamadaRealizadasActividadesMinutos(double minutosReal)
        {
            string stiloFinal = String.Empty;
            string colorRow = String.Empty;
            string colorTexto = String.Empty;
            if (minutosReal < 2) { colorRow = "blue"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 2 && minutosReal < 3) { colorRow = "skyblue"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 3 && minutosReal < 5) { colorRow = "yellow"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal >= 5 && minutosReal <= 8) { colorRow = "orange"; colorTexto = "black"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            if (minutosReal > 8) { colorRow = "red"; colorTexto = "white"; stiloFinal = "Style='background-color:" + colorRow + ";color:" + colorTexto + "'"; }
            return stiloFinal;
        }

        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtros)
        {
            try
            {
                ReporteMensajesWhatsAppFiltrosDTO _new = new ReporteMensajesWhatsAppFiltrosDTO();
                List<int> asesoresFinal = new List<int>();

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.ObtenerReporteMensajesWhatsApp(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public ReporteCambioDeFaseDataDTO ReporteCambioDeFaseV2_prueba(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                ReporteCambioFaseFiltroProcesadoDTO newFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                ReporteCambioFaseFiltroProcedimientoDTO newFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                newFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                newFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = newFiltro.FechaFin;
                filtros.FechaInicio = newFiltro.FechaInicio;

                newFiltroProcedimiento.FechaFin = newFiltro.FechaFin;
                newFiltroProcedimiento.FechaInicio = newFiltro.FechaInicio;

                var _queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                    newFiltroProcedimiento.IdPersonal = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                    newFiltroProcedimiento.IdCentroCosto = String.Join(",", filtros.CentroCostos);
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdCategoriaOrigen in (" + String.Join(", ", filtros.CategoriaDatos) + ")";
                //    newFiltroProcedimiento.IdCategoriaOrigen = String.Join(",", filtros.CategoriaDatos);
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdTipoDato in (" + String.Join(", ", filtros.TipoDatos) + ")";
                //    newFiltroProcedimiento.IdTipo = String.Join(",", filtros.TipoDatos);
                //}
                newFiltro.Filtro = _queryFiltro;
                ReporteCambioDeFaseDataDTO data = new ReporteCambioDeFaseDataDTO();
                data.ReporteTasaContacto = ReporteTasaContacto_prueba(newFiltro);
                data.ReporteTasaContactoRn2 = ReporteTasaContactoRn2(newFiltroProcedimiento);
                data.ReporteTasaContactoConySinLlamada = ReporteTasaContactoConySinLlamada(newFiltro);//
                data.ReporteTasaContactoConySinLlamadaRn2 = ReporteTasaContactoConySinLlamadaRn2(newFiltro);//
                data.ReporteControlBICYE = ReporteCambiosDeFaseControlBICYEAcumulado(newFiltro);
                data.ReporteCalidadProcesamiento = ReporteCalidadProcesamiento(filtros);
                data.ReporteMetasObtenerTotalIS = ReporteMetasObtenerTotalIS(filtros);
                if (filtros.Acumulado)
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadAcumuladoV2(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadConLlamada = ReporteCambiosDeFaseOportunidadAcumuladoConLlamada(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadSinLlamada = ReporteCambiosDeFaseOportunidadAcumuladoSinLlamada(newFiltro);
                    data.ReporteControlRN1yRN2 = ReporteControlAcumuladoRN1yRN(newFiltro);
                }
                else
                {
                    data.ReporteCambiosDeFaseOportunidad = ReporteCambiosDeFaseOportunidadNoAcumuladoV2(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadConLlamada = ReporteCambiosDeFaseOportunidadNoAcumuladoConLlamada(newFiltro);
                    data.ReporteCambiosDeFaseOportunidadSinLlamada = ReporteCambiosDeFaseOportunidadNoAcumuladoSinLlamada(newFiltro);
                    data.ReporteControlRN1yRN2 = ReporteControlNoAcumuladoRN1yRN(newFiltro);
                }

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        //pruebas




        /// <summary>
        /// Obtiene el reporte de tasa de contacto
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public ReporteTasaContactoDTO ReporteTasaContacto_prueba(ReporteCambioFaseFiltroProcesadoDTO filtros)
        {
            try
            {
                return _repReportes.ObtenerReporteTasaContacto_prueba(filtros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtros)
        {
            try
            {
                ReporteWhatsAppMasivoFiltrosDTO _new = new ReporteWhatsAppMasivoFiltrosDTO();
                List<int> asesoresFinal = new List<int>();

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                _new.IdPersonal = filtros.IdPersonal;
                _new.IdPais = filtros.IdPais;

                var data = _repReportes.GenerarReporteMensajesMasivosConjuntoLista(_new);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivos(ReporteMensajesWhatsAppFiltrosDTO filtros)
        {
            try
            {
                ReporteMensajesWhatsAppFiltrosDTO _new = new ReporteMensajesWhatsAppFiltrosDTO();
                List<int> asesoresFinal = new List<int>();

                _new.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                _new.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _repReportes.GenerarReporteMensajesMasivos(_new);

                //data = data.Select(x =>
                //{
                //    x.ratioLeidoEntregado = (x.CantidadLeido / x.CantidadEntregado);
                //    x.ratioRespondidoLeido = (x.CantidadRespondidos / x.CantidadLeido);
                //    x.ratioOportunidadRecibido = (x.OportunidadesCreadas / x.CantidadRecibidos);
                //    return x;
                //}).ToList();

                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Nueva version de: public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivos(ReporteMensajesWhatsAppFiltrosDTO filtros)
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosPorArea(ReporteMensajesWhatsAppPorAreaFiltrosDTO filtros)
        {
            try
            {
                ReporteMensajesWhatsAppFiltrosDTO FiltroSoloFechas = new ReporteMensajesWhatsAppFiltrosDTO();
                FiltroSoloFechas.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                FiltroSoloFechas.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);



                List<ReporteWhatsAppEnvioMasivoDTO> data;
                if (filtros.IdArea == 0)
                    data = _repReportes.GenerarReporteMensajesMasivos(FiltroSoloFechas);
                else
                    data = _repReportes.GenerarReporteMensajesMasivosPorArea(filtros);


                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtienen el reporte de operaciones
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public ReporteOperacionesEstructuraDTO GenerarReporteOperaciones(ReporteOperacionesFiltroDTO filtros)
        {
            try
            {
                var reporteOperaciones = new ReporteOperacionesEstructuraDTO
                {
                    Cuadro1 = _repReportes.ObtenerReporteCuadro1(filtros),
                    Cuadro2 = _repReportes.ObtenerReporteCuadro2(filtros),
                    Cuadro3 = _repReportes.ObtenerReporteCuadro3(filtros)
                };
                return reporteOperaciones;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtienen el reporte detalles de asignacion coordinadoras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteOperacionesDetallesAsignacionCoordinadoraDTO> GenerarReporteOperacionesDetallesAsignacionCoordinadora(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteDetallesAsignacionCoordinadora(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteControlContactoTelefonicoDTO> GenerarReporteControlContactoTelefonico(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteControlContactoTelefonico(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> GenerarReporteEstadoAlumnosPagos(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteEstadoAlumnosPagos(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteIndicadoresOperativosDTO> GenerarReporteIndicadoresOperativos(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicio = filtros2.FechaInicio.Value.AddHours(-5);
                filtros2.FechaFin = filtros2.FechaFin.Value.AddHours(-5);


                filtros2.FechaInicio = new DateTime(filtros2.FechaInicio.Value.Year, filtros2.FechaInicio.Value.Month, filtros2.FechaInicio.Value.Day, 0, 0, 0);
                filtros2.FechaFin = new DateTime(filtros2.FechaFin.Value.Year, filtros2.FechaFin.Value.Month, filtros2.FechaFin.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteIndicadoresOperativos(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadora(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                //filtros2.FechaInicio = filtros2.FechaInicio.Value.AddHours(-5);
                //filtros2.FechaFin = filtros2.FechaFin.Value.AddHours(-5);


                filtros2.FechaInicio = new DateTime(filtros2.FechaInicio.Value.Year, filtros2.FechaInicio.Value.Month, filtros2.FechaInicio.Value.Day, 0, 0, 0);
                filtros2.FechaFin = new DateTime(filtros2.FechaFin.Value.Year, filtros2.FechaFin.Value.Month, filtros2.FechaFin.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteIndicadoresOperativosPorDiaCoordinadora(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> GenerarReporteEstadoAlumnos2(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteEstadoAlumnos2(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> GenerarReporteEstadoAlumnosPagosAtrasados(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteEstadoAlumnosPagosAtrasados(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoPagos(List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        EstadoMatricula = y.EstadoMatricula,
                        SubEstadoMatricula = y.SubEstadoMatricula,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> GenerarReporteIndicadoresOperativosAgrupado(List<ReporteIndicadoresOperativosDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteIndicadoresOperativosAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteIndicadoresOperativosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteIndicadoresOperativosDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        //public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoGeneral(List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> respuestaGeneral)
        //{
        //    try
        //    {
        //        IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> agrupado = null;


        //        agrupado = respuestaGeneral.GroupBy(x => x.Dia)
        //        .Select(g => new ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO
        //        {
        //            Dia = g.Key,
        //            Detalle = g.Select(y => new ReporteIndicadoresOperativosPorDiaCoorinadorDTO
        //            {
        //                Dia = y.Dia,
        //                Coordinadora = y.Coordinadora,
        //                Estado = y.Estado,
        //                Total = y.Total
        //            }).ToList()
        //        });

        //        return agrupado;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw new Exception(Ex.Message);
        //    }

        //}
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupado(List<ReporteIndicadoresOperativosPorDiaCoorinadorDTO> respuestaGeneral, string Coordinadora)
        {
            try
            {
                IEnumerable<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.Where(w => w.Coordinadora == Coordinadora).GroupBy(x => x.Dia)
                .Select(g => new ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoDTO
                {
                    Dia = g.Key,
                    Detalle = g.Select(y => new ReporteIndicadoresOperativosPorDiaCoorinadorDTO
                    {
                        Dia = y.Dia,
                        Coordinadora = y.Coordinadora,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO> GenerarReporteIndicadoresOperativosPorDiaCoordinadoraAgrupadoVersion2(IGrouping<string, ReporteIndicadoresOperativosPorDiaCoorinadorDTO> item)
        {
            try
            {
                var temporal = item;

                var agrupado=temporal.GroupBy(x => x.Dia)
                .Select(g => new ReporteIndicadoresOperativosPorDiaCoordinadorAgrupadoNuevoDTO
                {
                    Coordinadora=temporal.Key,
                    Dia = g.Key,
                    Detalle = g.Select(y => new ReporteIndicadoresOperativosPorDiaCoorinadorDTO
                    {
                        Dia = y.Dia,
                        Coordinadora = y.Coordinadora,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });
                return agrupado.ToList();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonline(List<ReporteAvanceAcademicoPresencialOnlineDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.Where(w => w.Tipo == "Aonline").GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoPresencialOnlineDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        Estado = y.Estado,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.Where(w => w.Tipo == "Aonline").GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        EstadoAcademico = y.EstadoAcademico,
                        EstadoPagos = y.EstadoPagos,
                        Total = y.Total
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de por dias hacia atras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(List<ReporteAvanceAcademicoAlumnosPagosAtrasados> respuestaGeneral)
        {
            try
            {
                IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> agrupado = null;


                agrupado = respuestaGeneral.GroupBy(x => x.Coordinadora)
                .Select(g => new ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO
                {
                    Coordinadora = g.Key,
                    Detalle = g.Select(y => new ReporteAvanceAcademicoAlumnosPagosAtrasados
                    {
                        Coordinadora = y.Coordinadora,
                        Tipo = y.Tipo,
                        Estado = y.Estado,
                        NumeroAlumnos = y.NumeroAlumnos,
                        NumeroCuotasAtrasadas = y.NumeroCuotasAtrasadas,
                        MontoTotalCuotasAtrasadas = y.MontoTotalCuotasAtrasadas
                    }).ToList()
                });

                return agrupado;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        /// Obtienen el reporte detalles de asignacion coordinadoras
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<ReporteOperacionesDetallesAsignacionCoordinadoraEstadosDTO> GenerarReporteOperacionesDetallesAsignacionCoordinadoraEstados(string filtros, ReporteTasaConversionConsolidadaFiltroDTO filtros2)
        {
            try
            {
                //ya en el primer sp ya lo manda con el formato correcto
                //filtros2.FechaInicioMatricula = filtros2.FechaInicioMatricula.Value.AddHours(-5);
                //filtros2.FechaFinMatricula = filtros2.FechaFinMatricula.Value.AddHours(-5);
                //filtros2.FechaInicioAsignacion = filtros2.FechaInicioAsignacion.Value.AddHours(-5);
                //filtros2.FechaFinAsignacion = filtros2.FechaFinAsignacion.Value.AddHours(-5);

                //filtros2.FechaInicioMatricula = new DateTime(filtros2.FechaInicioMatricula.Value.Year, filtros2.FechaInicioMatricula.Value.Month, filtros2.FechaInicioMatricula.Value.Day, 0, 0, 0);
                //filtros2.FechaFinMatricula = new DateTime(filtros2.FechaFinMatricula.Value.Year, filtros2.FechaFinMatricula.Value.Month, filtros2.FechaFinMatricula.Value.Day, 23, 59, 59);
                //filtros2.FechaInicioAsignacion = new DateTime(filtros2.FechaInicioAsignacion.Value.Year, filtros2.FechaInicioAsignacion.Value.Month, filtros2.FechaInicioAsignacion.Value.Day, 0, 0, 0);
                //filtros2.FechaFinAsignacion = new DateTime(filtros2.FechaFinAsignacion.Value.Year, filtros2.FechaFinAsignacion.Value.Month, filtros2.FechaFinAsignacion.Value.Day, 23, 59, 59);

                var data = _repReportes.GenerarReporteDetallesAsignacionCoordinadoraEstados(filtros, filtros2);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public List<ReporteProblemasAulaVirtualResultadoDTO> ReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtros)
        {
            try
            {
                //filtros.Fecha = filtros.Fecha.AddHours(-5);
                DateTime fechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);

                var data = _repReportes.ObtenerReporteProblemasAulaVirtual(filtros, fechaInicio, fechaFin);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el reporte de calidad procesamiento
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ReporteCalidadProcesamientoDTO> </returns>
        public List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamientoV2(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteCalidadProcesamientoV2(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Diferencia de Llamadas por Blqoue
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> ObjetoDTO : List<DiferenciaLlamadasBloqueDTO> </returns>
        public List<DiferenciaLlamadasBloqueDTO> ReporteDiferenciaLlamadasBloque(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteDiferenciaLlamadasBloque(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene conteo de Datos de Fase según Inicio del día y momento de la consulta
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns> Lista de Objeto DTO : List<ConteoDatosFaseDTO> </returns>
        public List<ConteoDatosFaseDTO> ReporteConteoDatosFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                return _repReportes.ReporteConteoDatosFase(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene Reporte de Calidad Consultando a Réplica
        /// </summary>
        /// <param name="filtros"> filtros de búsqueda para Reporte </param>
        /// <returns> ObjetoDTO: ReporteCambioDeFaseDataV2DTO </returns>
        public ReporteCalidadCambioDeFaseDTO ReporteCalidadCambioDeFase(ReporteCambioFaseFiltrosDTO filtros)
        {
            try
            {
                ReporteCambioFaseFiltroProcesadoDTO newFiltro = new ReporteCambioFaseFiltroProcesadoDTO();
                ReporteCambioFaseFiltroProcedimientoDTO newFiltroProcedimiento = new ReporteCambioFaseFiltroProcedimientoDTO();
                newFiltro.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                newFiltro.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);
                filtros.FechaFin = newFiltro.FechaFin;
                filtros.FechaInicio = newFiltro.FechaInicio;

                newFiltroProcedimiento.FechaFin = newFiltro.FechaFin;
                newFiltroProcedimiento.FechaInicio = newFiltro.FechaInicio;

                var _queryFiltro = "";

                if (filtros.Asesores.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdPersonalAsignado in (" + String.Join(",", filtros.Asesores) + ")";
                    newFiltroProcedimiento.IdPersonal = String.Join(",", filtros.Asesores);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    _queryFiltro = _queryFiltro + " and ";
                    _queryFiltro = _queryFiltro + "IdCentroCosto in (" + String.Join(",", filtros.CentroCostos) + ")";
                    newFiltroProcedimiento.IdCentroCosto = String.Join(",", filtros.CentroCostos);
                }
                //if (filtros.CategoriaDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdCategoriaOrigen in (" + String.Join(", ", filtros.CategoriaDatos) + ")";
                //    newFiltroProcedimiento.IdCategoriaOrigen = String.Join(",", filtros.CategoriaDatos);
                //}
                //if (filtros.TipoDatos.Count() > 0)
                //{
                //    _queryFiltro = _queryFiltro + " and ";
                //    _queryFiltro = _queryFiltro + "IdTipoDato in (" + String.Join(", ", filtros.TipoDatos) + ")";
                //    newFiltroProcedimiento.IdTipo = String.Join(",", filtros.TipoDatos);
                //}
                newFiltro.Filtro = _queryFiltro;
                ReporteCalidadCambioDeFaseDTO data = new ReporteCalidadCambioDeFaseDTO();
                data.ReporteCalidadProcesamiento = ReporteCalidadProcesamientoV2(filtros);
                data.DiferenciaLlamadasBloque = ReporteDiferenciaLlamadasBloque(filtros);
                data.ConteoDatosFase = ReporteConteoDatosFase(filtros);
                return data;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Procesa el calculo de Reporte Tasa de Conversion Consolidada 
        /// </summary>
        /// <returns> ObjetoDTO: ResultadoFinalDTO </returns>
        public ResultadoFinalDTO CalculoReporteTasaConversionConsolidada()
        {
            try
            {
                return _repReportes.CalculoReporteTasaConversionConsolidada();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + (e.InnerException != null ? ("-" + e.InnerException.Message) : ""));
            }
        }
        /// <summary>
        /// Obtiene el log de las oportunidades
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Lista de objetos de clase ReporteSeguimientoOportunidadLogGridDTO</returns>
        public List<ReporteSeguimientoOportunidadLogDTO> ObtenerOportunidadesLogV2(int idOportunidad)
        {
            try
            {
                List<ReporteSeguimientoOportunidadLogDTO> oportunidades = _repReportes.ObtenerListaOportunidadLogV2(idOportunidad);

                return oportunidades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

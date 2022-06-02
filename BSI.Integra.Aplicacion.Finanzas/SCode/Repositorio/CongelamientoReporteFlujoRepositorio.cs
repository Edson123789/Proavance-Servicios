using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Globalization;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTO;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CongelamientoReporteFlujoRepositorio : BaseRepository<TCronogramaPagoDetalleFinalPorPeriodo, ReporteFlujoBO>
    {
        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoDTO> FlujoCongelamiento)
        {
            try
            {
                bool items = false;
                foreach (var element in FlujoCongelamiento)
                {
                    var query = _dapper.QuerySPDapper("[fin].[SP_GenerarCongelamientoReporteFlujo]", new
                    {
                        fechaCongelamiento = element.fechaCongelamiento,
                        idMatriculaCabecera = element.idMatriculaCabecera,
                        idCoordAcademico = element.idCoordAcademico,
                        coordinadorAcademico = element.coordinadorAcademico,
                        idPespecifico = element.idPespecifico,
                        programa = element.programa,
                        codigoMatricula = element.codigoMatricula,
                        alumno = element.alumno,
                        fechaCuota = element.fechaCuota,
                        montoCuota = element.montoCuota,
                        fechaPago = element.fechaPago,
                        pago = element.pago,
                        saldoPendiente = element.saldoPendiente,
                        mora = element.mora,
                        nroCuota = element.nroCuota,
                        nroSubCuota = element.nroSubCuota,
                        moneda = element.moneda,
                        totalUSD = element.totalUSD,
                        realUSD = element.realUSD,
                        penUSD = element.penUSD,
                        Estado = element.Estado,
                        fechaCreacion = DateTime.Now,
                        fechaModificacion = DateTime.Now,
                        UsuarioCreacion = element.UsuarioCreacion,
                        UsuarioModificacion= element.UsuarioModificacion,
                    });
                    if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    {
                        items = true;
                    }
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        ///Repositorio: CongelamientoReporteFlujoRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 19/05/2021
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda la fecha
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeFlujoPorDia(string FechaCongelamiento, string Usuario)
        {
            try
            {

                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_GenerarCongelamientoReporteFlujoPorDia", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        ///Repositorio: CongelamientoReporteFlujoRepositorio
        ///Autor: Miguel Mora
        ///Fecha: 19/05/2021
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda el periodo  
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="IdPeriod"> periodo</param>
        public int CongelarReporteDeFlujoPorPeriodo(string Usuario, int IdPeriodo)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_GenerarCongelamientoReporteFlujoPorPeriodo", new { Usuario, IdPeriodo });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        ///Repositorio: CongelamientoReporteFlujoRepositorio
        ///Autor: Lisbeth Ortogorin
        ///Fecha: 07/02/2022
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda la fecha
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteOriginalesPorDia(string FechaCongelamiento, string Usuario)
        {
            try
            {

                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_CongelamientoCronogramasOriginales", new { Usuario, FechaCongelamiento });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: CongelamientoReporteFlujoRepositorio
        ///Autor: Lisbeth Ortogorin
        ///Fecha: 07/02/2022
        /// <summary>
        /// Congela los datos del reporte de flujo y guarda la fecha
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public List<ListaCambiosPorPeriodoDTO> ListaCambiosPorPeriodo()
        {
            try
            {
                List<ListaCambiosPorPeriodoDTO> cambio = new List<ListaCambiosPorPeriodoDTO>();
                var registrosDB = "SELECT * FROM fin.V_ListarCambiosPorPeriodo";
                var registros = _dapper.QueryDapper(registrosDB, new { });
                if (!string.IsNullOrEmpty(registros) && !registros.Contains("[]"))
                {
                    cambio = JsonConvert.DeserializeObject<List<ListaCambiosPorPeriodoDTO>>(registros);
                }
                return cambio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}

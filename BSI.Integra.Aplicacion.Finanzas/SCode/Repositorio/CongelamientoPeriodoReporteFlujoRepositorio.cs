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

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CongelamientoPeriodoReporteFlujoRepositorio : BaseRepository<TCronogramaPagoDetalleFinalPorPeriodo, ReporteFlujoBO>
    {
        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo)
        {
            try
            {
                bool items = false;
                foreach (var element in FlujoCongelamientoPeriodo)
                {
                    var query = _dapper.QuerySPDapper("[fin].[SP_GenerarCongelamientoPeriodoReporteFlujo]", new
                    {
                        idPeriodo = element.idPeriodo,
                        periodo = element.periodo,
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
                        UsuarioModificacion = element.UsuarioModificacion,
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
    }

}

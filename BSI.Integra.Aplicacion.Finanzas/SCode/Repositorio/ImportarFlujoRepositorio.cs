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
    public class ImportarFlujoRepositorio : BaseRepository<TCronogramaPagoDetalleFinalPorPeriodo, ReporteFlujoSubidoBO>
    {
        public bool InsertarFlujoReporte(string data)//ReporteFlujoSubidoDTO
        {
            try
            {
                bool items = false;
                var query = _dapper.QuerySPDapper("[fin].[SP_InsertarFlujoReporte]", new
                {
                    Texto=data
                });
                 if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                 {
                       items = true;
                 }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertarCambiosPeriodo(string data)
        {
            try
            {
                bool items = false;
                var query = _dapper.QuerySPDapper("[fin].[SP_InsertarCambiosReporte]", new
                {
                    Texto = data
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = true;
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

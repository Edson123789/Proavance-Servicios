using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Reportes.Repositorio
{
    /// Repositorio: Reportes/ReporteLibroReclamacion
    /// Autor: Abelson Quiñones Gutierrez
    /// Fecha: 01/06/2021
    /// <summary>
    /// El repositorio contiene la funcionalidad de consulta a la db segun lo solicitado por el controllador
    /// </summary>
    public class ReporteLibroReclamacionRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// obtener los nombres de la personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="nombre">parte del nombre para buscar coincidencias</param>
        /// <returns>Lista de los nombres en un List<ItemComboAutocompleDTO></returns>
        public List<ItemComboAutocompleDTO> ObtenerListaNombreReclamo(string nombre)
        {
            try
            {
                List<ItemComboAutocompleDTO> items = new List<ItemComboAutocompleDTO>();
                var query = string.Empty;
                query = "select DISTINCT Nombre as Valor,Nombre as Nombre from [40.76.216.5].integraDB_PortalWeb.dbo.T_LibroReclamacion where Nombre like" + "'%"+nombre+"%'";
                var lista = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ItemComboAutocompleDTO>>(lista);
                }
                return items;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener los dni de las personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="dni">parte del dni para buscar coincidencias</param>
        /// <returns>Lista de los dnis en un List<ItemComboAutocompleDTO></returns>
        public List<ItemComboAutocompleDTO> ObtenerListaDniReclamo(string dni)
        {
            try
            {
                List<ItemComboAutocompleDTO> items = new List<ItemComboAutocompleDTO>();
                var query = string.Empty;
                query = "select DISTINCT DNI as Valor,DNI as Nombre from [40.76.216.5].integraDB_PortalWeb.dbo.T_LibroReclamacion where DNI like" + "'%" + dni + "%'";
                var lista = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ItemComboAutocompleDTO>>(lista);
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de libro de reclamaciones segun el filtro ingresado
        /// </summary>
        /// <param name="filtroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte libro de reclamaciones en un List<ReporteLibroReclamacionDTO></returns>
        public List<ReporteLibroReclamacionDTO> GenerarReporteLibroReclamacion(ReporteLibroReclamacionFiltroDTO filtroReporte)
        {
            try
            {
                string nombre = "%%",dni= "%%";
                if (filtroReporte.Nombre != null && filtroReporte.Nombre != "") nombre = "%"+ filtroReporte.Nombre + "%";
                if (filtroReporte.DNI != null && filtroReporte.DNI != "") dni = "%" + filtroReporte.DNI + "%";
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ReporteLibroReclamacionDTO> reporteDescargaMaterial = new List<ReporteLibroReclamacionDTO>();
                var query = _dapper.QuerySPDapper("[mkt].[SP_ReporteLibroReclamacion]", new { fechainicio, fechafin, nombre, dni});
                if (!string.IsNullOrEmpty(query))
                {
                    reporteDescargaMaterial = JsonConvert.DeserializeObject<List<ReporteLibroReclamacionDTO>>(query);
                }
                return reporteDescargaMaterial;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

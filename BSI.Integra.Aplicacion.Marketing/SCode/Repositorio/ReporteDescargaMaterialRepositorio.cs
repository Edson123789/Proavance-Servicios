using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/ReporteDescargaMaterial
    /// Autor: Abelson Quiñones Gutierrez
    /// Fecha: 01/06/2021
    /// <summary>
    /// El repositorio contiene la funcionalidad de consulta a la db segun lo solicitado por el controllador
    /// </summary>

    public class ReporteDescargaMaterialRepositorio: BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public ReporteDescargaMaterialRepositorio() : base()
        {
        }
        public ReporteDescargaMaterialRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        #endregion

        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener las subareas segun el id del area de capacitacion
        /// </summary>
        /// <param name="idAreaCapacitacion">Id del area capacitacion</param>
        /// <returns>Lista de las subareas en un List<FiltroDTO></returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerSubAreasParaFiltro(int idAreaCapacitacion)
        {
            try
            {
                List<SubAreaCapacitacionFiltroDTO> subAreasCapacitacionFiltro = new List<SubAreaCapacitacionFiltroDTO>();
                var querySubAreaCapacitacion = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE IdAreaCapacitacion=@IdAreaCapacitacion and Estado = 1";
                var subAreaCapacitacionDB = _dapper.QueryDapper(querySubAreaCapacitacion, new {idAreaCapacitacion });
                if (!string.IsNullOrEmpty(subAreaCapacitacionDB) && !subAreaCapacitacionDB.Contains("[]"))
                {
                    subAreasCapacitacionFiltro = JsonConvert.DeserializeObject<List<SubAreaCapacitacionFiltroDTO>>(subAreaCapacitacionDB);
                }
                return subAreasCapacitacionFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// obtener la lista de los nombres del material de webinar y whitepaper segun el filtro enviado
        /// </summary>
        /// <param name="filtroMaterial">Filtro para la seleccion de nombres de material segun el tipoArticulo,area y subarea </param>
        /// <returns>Lista de los articulos el un List<ListaMatarial></returns>
        public List<ListaMatarial> ObtenerListaMateriales(ReporteMaterialFiltroDTO filtroMaterial)
        {
            try
            {
                string tipoArticulo = null, area = null, subarea = null;
                if (filtroMaterial.TipoArticulo.Count() > 0) tipoArticulo = String.Join(",", filtroMaterial.TipoArticulo);
                if (filtroMaterial.Area.Count() > 0) area = String.Join(",", filtroMaterial.Area);
                if (filtroMaterial.SubArea.Count() > 0) area = String.Join(",", filtroMaterial.SubArea);

                List<ListaMatarial> lista = new List<ListaMatarial>();
                var query= _dapper.QuerySPDapper("[mkt].[SP_ListaArticulosAreaySubArea]", new { tipoArticulo,areas=area,subareas=subarea });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ListaMatarial>>(query);
                }
                return lista;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: Abelson Quiñones Gutierrez
        /// Fecha: 01/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de Material descargado segun el liltro enviado
        /// </summary>
        /// <param name="filtroReporte">Filtro para la seleccion del reporte de material descargado  </param>
        /// <returns>El reporte de Material descargado en una Lista List<ReporteDescargaMaterialDTO></returns>
        public List<ReporteDescargaMaterialDTO> GenerarReporteDescargaContenido(ReporteDescargaMaterialFiltroDTO filtroReporte)
        {
            try
            {
                string categoria = null, areas = null, subareas = null,paises=null, nombreArticulo = null;
                if (filtroReporte.Categoria != null && filtroReporte.Categoria.Count() > 0  ) categoria = String.Join(",", filtroReporte.Categoria);
                if (filtroReporte.AreaCapacitacion !=null && filtroReporte.AreaCapacitacion.Count() > 0) areas = String.Join(",", filtroReporte.AreaCapacitacion);
                if (filtroReporte.SubAreas !=null && filtroReporte.SubAreas.Count() > 0) subareas = String.Join(",", filtroReporte.SubAreas);
                if (filtroReporte.Pais != null && filtroReporte.Pais.Count() > 0) paises = String.Join(",", filtroReporte.Pais);
                if (filtroReporte.Articulos !=null && filtroReporte.Articulos.Count() > 0) nombreArticulo = String.Join(",", filtroReporte.Articulos);
                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ReporteDescargaMaterialDTO> reporteDescargaMaterial = new List<ReporteDescargaMaterialDTO>();
                var query = _dapper.QuerySPDapper("[mkt].[SP_ReporteDescargaMaterial]", new {fechainicio, fechafin,areas,subareas,paises,categoria,nombreArticulo });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteDescargaMaterial = JsonConvert.DeserializeObject<List<ReporteDescargaMaterialDTO>>(query);
                }
                return reporteDescargaMaterial;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

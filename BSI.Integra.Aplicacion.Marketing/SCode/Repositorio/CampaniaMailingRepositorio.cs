using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/CampaniaMailing
    /// Autor: Gian Miranda
    /// Fecha: 26/07/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_CampaniaMailing
    /// </summary>
    /// 
    public class CampaniaMailingRepositorio : BaseRepository<TCampaniaMailing, CampaniaMailingBO>
    {
        #region Metodos Base
        public CampaniaMailingRepositorio() : base()
        {
        }
        public CampaniaMailingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaMailingBO> GetBy(Expression<Func<TCampaniaMailing, bool>> filter)
        {
            IEnumerable<TCampaniaMailing> listado = base.GetBy(filter);
            List<CampaniaMailingBO> listadoBO = new List<CampaniaMailingBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaMailingBO objetoBO = Mapper.Map<TCampaniaMailing, CampaniaMailingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaMailingBO FirstById(int id)
        {
            try
            {
                TCampaniaMailing entidad = base.FirstById(id);
                CampaniaMailingBO objetoBO = new CampaniaMailingBO();
                Mapper.Map<TCampaniaMailing, CampaniaMailingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaMailingBO FirstBy(Expression<Func<TCampaniaMailing, bool>> filter)
        {
            try
            {
                TCampaniaMailing entidad = base.FirstBy(filter);
                CampaniaMailingBO objetoBO = Mapper.Map<TCampaniaMailing, CampaniaMailingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaMailingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaMailing entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<CampaniaMailingBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(CampaniaMailingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaMailing entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<CampaniaMailingBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TCampaniaMailing entidad, CampaniaMailingBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TCampaniaMailing MapeoEntidad(CampaniaMailingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaMailing entidad = new TCampaniaMailing();
                entidad = Mapper.Map<CampaniaMailingBO, TCampaniaMailing>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaCampaniaMailingValorTipo != null && objetoBO.ListaCampaniaMailingValorTipo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaCampaniaMailingValorTipo)
                    {
                        TCampaniaMailingValorTipo entidadHijo = new TCampaniaMailingValorTipo();
                        entidadHijo = Mapper.Map<CampaniaMailingValorTipoBO, TCampaniaMailingValorTipo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TCampaniaMailingValorTipo.Add(entidadHijo);
                    }
                }
                if (objetoBO.listaCampaniaMailingDetalleBO != null && objetoBO.listaCampaniaMailingDetalleBO.Count > 0)
                {

                    foreach (var hijo in objetoBO.listaCampaniaMailingDetalleBO)
                    {
                        TCampaniaMailingDetalle entidadHijo = new TCampaniaMailingDetalle();
                        entidadHijo = Mapper.Map<CampaniaMailingDetalleBO, TCampaniaMailingDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TCampaniaMailingDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                        if (hijo.listaCampaniaMailingDetalleProgramaBO != null && hijo.listaCampaniaMailingDetalleProgramaBO.Count > 0)
                        {
                            foreach (var hijoPrograma in hijo.listaCampaniaMailingDetalleProgramaBO)
                            {
                                TCampaniaMailingDetallePrograma entidadHijoPrograma = new TCampaniaMailingDetallePrograma();
                                entidadHijoPrograma = Mapper.Map<CampaniaMailingDetalleProgramaBO, TCampaniaMailingDetallePrograma>(hijoPrograma,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TCampaniaMailingDetallePrograma.Add(entidadHijoPrograma);
                            }
                        }
                        if (hijo.AreaCampaniaMailingDetalle != null && hijo.AreaCampaniaMailingDetalle.Count > 0)
                        {
                            foreach (var subHijo in hijo.AreaCampaniaMailingDetalle)
                            {
                                TAreaCampaniaMailingDetalle AreaCampaniaMailingDetalle = new TAreaCampaniaMailingDetalle();
                                AreaCampaniaMailingDetalle = Mapper.Map<AreaCampaniaMailingDetalleBO, TAreaCampaniaMailingDetalle>(subHijo,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TAreaCampaniaMailingDetalle.Add(AreaCampaniaMailingDetalle);
                            }
                        }
                        if (hijo.SubAreaCampaniaMailingDetalle != null && hijo.SubAreaCampaniaMailingDetalle.Count > 0)
                        {
                            foreach (var subHijo in hijo.SubAreaCampaniaMailingDetalle)
                            {
                                TSubAreaCampaniaMailingDetalle AreaCampaniaMailingDetalle = new TSubAreaCampaniaMailingDetalle();
                                AreaCampaniaMailingDetalle = Mapper.Map<SubAreaCampaniaMailingDetalleBO, TSubAreaCampaniaMailingDetalle>(subHijo,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TSubAreaCampaniaMailingDetalle.Add(AreaCampaniaMailingDetalle);
                            }
                        }
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene una lista con los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase CampaniaMailingDTO</returns>
        public List<CampaniaMailingDTO> ObtenerListaCampaniaMailing()
        {
            try
            {
                string query = @"SELECT Id, 
                                   Nombre, 
                                   PrincipalValor, 
                                   PrincipalValorTiempo, 
                                   SecundarioValor, 
                                   SecundarioValorTiempo, 
                                   ActivaValor, 
                                   ActivaValorTiempo, 
                                   IdCategoriaOrigen, 
                                   FechaCreacion, 
                                   FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal, 
                                   FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal
                            FROM mkt.V_TCampaniaMailing_DatosCampania
                            WHERE Estado = 1; ";
                var respuestaQuery = _dapper.QueryDapper(query, null);
                List<CampaniaMailingDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<CampaniaMailingDTO>>(respuestaQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros para filtro
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtener la Campania Mailing por Id
        /// </summary>
        /// <param name="id">Id de la Campania Mailing a buscar (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>Objeto de clase CampaniaMailingDTO</returns>
        public CampaniaMailingDTO Obtener(int id)
        {
            try
            {
                var campaniaMailing = new CampaniaMailingDTO();
                var query = @"
                            SELECT Id, 
                            Nombre, 
                            PrincipalValor, 
                            PrincipalValorTiempo, 
                            SecundarioValor, 
                            SecundarioValorTiempo, 
                            ActivaValor, 
                            ActivaValorTiempo, 
                            IdCategoriaOrigen, 
                            FechaCreacion, 
                            FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal, 
                            FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal
                    FROM mkt.V_TCampaniaMailing_DatosCampania
                    WHERE Estado = 1 
                    AND  Id = @id ";
                var respuestaDB = _dapper.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(respuestaDB) && respuestaDB != "null")
                {
                    campaniaMailing = JsonConvert.DeserializeObject<CampaniaMailingDTO>(respuestaDB);
                }
                return campaniaMailing;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de anuncio Mailchimp metrica
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaDTO> ObtenerReporteCampaniaMailing(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaGeneral]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de anuncio Mailchimp metrica registros
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaRegistrosDTO> ObtenerReporteCampaniaMailingRegistros(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaRegistrosDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaRegistros]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaRegistrosDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de anuncio Mailchimp metrica oportunidades
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> ObtenerReporteCampaniaMailingOportunidades(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaOportunidadesDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaOportunidades]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaOportunidadesDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener el detalle de la fila del reporte de anuncio Mailchimp metrica
        /// </summary>
        /// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        /// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaDTO> ObtenerReporteCampaniaMailingDetalle(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaGeneralDetalle]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdCampaniaMailing = idCampaniaMailing, VersionMailing = versionMailing, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener el detalle de la fila del reporte de anuncio Mailchimp metrica de registros
        /// </summary>
        /// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        /// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaRegistrosDTO> ObtenerReporteCampaniaMailingRegistrosDetalle(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaRegistrosDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaGeneralRegistrosDetalle]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdCampaniaMailing = idCampaniaMailing, VersionMailing = versionMailing, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaRegistrosDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 05/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener el detalle de la fila del reporte de anuncio Mailchimp metrica de oportunidades
        /// </summary>
        /// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        /// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda</param>
        /// <param name="fechaFin">Fecha de fin de la busqueda</param>
        /// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        public List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> ObtenerReporteCampaniaMailingOportunidadesDetalle(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaResultadoReporte = new List<ReporteCampaniaMailchimpMetricaOportunidadesDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteMailChimpMetricaGeneralOportunidadesDetalle]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdCampaniaMailing = idCampaniaMailing, VersionMailing = versionMailing, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteCampaniaMailchimpMetricaOportunidadesDTO>>(resultadoReporte);

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

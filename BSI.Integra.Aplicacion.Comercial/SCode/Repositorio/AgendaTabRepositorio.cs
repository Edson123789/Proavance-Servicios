using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    /// Repositorio: AgendaTab
    /// Autor: Wilber Choque - Fischer Valdez - Richard Zenteno - Jose Villena - Carlos Crispin
    /// Fecha: 20/03/2021
    /// <summary>
    /// Gestion para los tabs de la agenda
    /// </summary>
    public class AgendaTabRepositorio : BaseRepository<TAgendaTab, AgendaTabBO>
    {
        #region Metodos Base
        public AgendaTabRepositorio() : base()
        {
        }
        public AgendaTabRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AgendaTabBO> GetBy(Expression<Func<TAgendaTab, bool>> filter)
        {
            IEnumerable<TAgendaTab> listado = base.GetBy(filter);
            List<AgendaTabBO> listadoBO = new List<AgendaTabBO>();
            foreach (var itemEntidad in listado)
            {
                AgendaTabBO objetoBO = Mapper.Map<TAgendaTab, AgendaTabBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AgendaTabBO FirstById(int id)
        {
            try
            {
                TAgendaTab entidad = base.FirstById(id);
                AgendaTabBO objetoBO = new AgendaTabBO();
                Mapper.Map<TAgendaTab, AgendaTabBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AgendaTabBO FirstBy(Expression<Func<TAgendaTab, bool>> filter)
        {
            try
            {
                TAgendaTab entidad = base.FirstBy(filter);
                AgendaTabBO objetoBO = Mapper.Map<TAgendaTab, AgendaTabBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AgendaTabBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAgendaTab entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AgendaTabBO> listadoBO)
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

        public bool Update(AgendaTabBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAgendaTab entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AgendaTabBO> listadoBO)
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
        private void AsignacionId(TAgendaTab entidad, AgendaTabBO objetoBO)
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

        private TAgendaTab MapeoEntidad(AgendaTabBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAgendaTab entidad = new TAgendaTab();
                entidad = Mapper.Map<AgendaTabBO, TAgendaTab>(objetoBO, opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        ///Repositorio: AgendaTabRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor </param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Lista Actividades Agenda: List<ActividadAgendaDTO> </returns>
        public List<ActividadAgendaDTO> ObtenerActividades(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltro(filtros);
                if (tabAgenda.Probabilidad.Contains("0"))
                {
                    var queryConIdAsesor = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                    var queryConFiltros = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    //_query = "SELECT @CamposVista FROM @VistaBaseDatos WHERE IdTipoCategoriaOrigen IN ( @IdTipoCategoriaOrigen ) AND IdCategoriaOrigen IN ( @IdCategoriaOrigen ) AND IdTipoDato IN ( @IdTipoDato ) AND IdFaseOportunidad IN ( @IdFaseOportunidad ) AND IdEstadoOportunidad IN ( @IdEstadoOportunidad ) AND IdPersonal_Asignado IN( @idAsesor ) " + _filtro;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    //var actividadesDB = _dapper.QueryDapper(_query, new { tabAgenda.CamposVista , tabAgenda.VistaBaseDatos, tabAgenda.IdTipoCategoriaOrigen, tabAgenda.IdCategoriaOrigen, tabAgenda.IdTipoDato , tabAgenda.IdFaseOportunidad , tabAgenda.IdEstadoOportunidad, idAsesor });
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                else
                {
                    var queryConIdAsesor = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") AND IdPersonal_Asignado in (" + idAsesor.ToString() + ") " + filtro;
                    var queryConFiltros = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, null);
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AgendaTabRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene una lista de actividades de todos los tabs de la agenda, excepto el tab de realizadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor </param>
        /// <param name="filtros">Objeto de tipo diccionario</param>
        /// <returns>Lista de Actividades Tabs Agenda Operaciones: List<ActividadAgendaDTO> </returns>
        public List<ActividadAgendaDTO> ObtenerActividadesOperaciones(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltro(filtros);

                var queryConIdAsesor = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                var queryConFiltros = "SELECT " + tabAgenda.CamposVista.ToString() + " FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                var actividadesDB = _dapper.QueryDapper(query, new { });
                actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);

                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AgendaTabRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene un string que contendra los filtros sql, para los tabs de la agenda
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Cadena con los filtros para construir la query </returns>
        private string ObtenerFiltro(Dictionary<string, string> filtros)
        {
            try
            {
                var filtroVista = string.Empty;
                var skip = "";
                var take = "";
                foreach (var prop in filtros)
                {
                    if (prop.Key.Equals("take") || prop.Key.Equals("page") || prop.Value.Equals("") || prop.Value == null || prop.Value.Length <= 0)
                    {
                        continue;
                    }
                    if (prop.Key.Equals("skip"))
                    {
                        skip = prop.Value;
                        continue;
                    }
                    if (prop.Key.Equals("pageSize"))
                    {
                        take = prop.Value;
                        continue;
                    }
                    if (prop.Key.Equals("IdProbabilidadRegistroPW"))
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                        continue;
                    }
                    if (prop.Key.Equals("CodigoMatricula") || prop.Key.Equals("dni"))
                    {
                        filtroVista += " AND " + prop.Key + " Like  '%" + prop.Value + "%'";
                        continue;
                    }
                    if (prop.Key.Equals("FechaLlamada"))
                    {
                        //_filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "  <= convert(datetime, '" + prop.Value + "', 101)";
                        filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "   <= convert(datetime, DATEADD(DAY, 1, Convert(date, '" + prop.Value + "')), 101)";
                        continue;
                    }
                    if (prop.Value.Contains(","))
                    {
                        filtroVista += " AND " + prop.Key + " IN (" + prop.Value + ")";
                    }
                    else
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                    }
                }
                if (skip != "" && take != "")
                {
                    filtroVista += " ORDER BY UltimaFechaProgramada ASC OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY;";
                    //filtroVista += " ORDER BY (select null) OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY;";
                }
                return filtroVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las actividades realizadas con los filtros enviados
        /// </summary>
        /// <param name="idsAsesor">Id de los asesores (PK de la tabla gp.T_Personal)</param>
        /// <param name="fecha">Cadena con la fecha para el SP</param>
        /// <param name="idCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idFaseOportunidad">Id de la fase de la oportunidad(PK de la tabla pla.T_FaseOportunidad)</param>
        /// <param name="idTipoDato">Id del tipo de dato (PK de la tabla mkt.T_TipoDato)</param>
        /// <param name="idOrigen">Id del origen del dato (PK de la tabla mkt.T_Origen)</param>
        /// <param name="take">Cantidad de datos para la paginacion</param>
        /// <param name="skip">Limite minimo para la paginacion</param>
        /// <param name="idsCategoriaOrigen">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="idProbabilidad">Id de la probilidad (PK de la tabla mkt.T_ProbabilidadRegistro_PW)</param>
        /// <param name="idEstado">Id del estado de la oportunidad(PK de la tabla com.T_EstadoOportunidad)</param>
        /// <returns>Lista de objetos de tipo PruebaActividadRealizadaDTO</returns>
        public List<PruebaActividadRealizadaDTO> ObtenerActividadesRealizadasSP(string idsAsesor, string fecha, int idCentroCosto, int idAlumno, int idFaseOportunidad, int idTipoDato, int idOrigen, int take, int skip, string idsCategoriaOrigen, int idProbabilidad, int idEstado)
        {
            try
            {
                List<PruebaActividadRealizadaDTO> actividadesRealizadas = new List<PruebaActividadRealizadaDTO>();
                take = take == 0 ? 20000 : take;
                idsCategoriaOrigen = idsCategoriaOrigen ?? "_";
                var actividadesDB = _dapper.QuerySPDapper("com.SP_ObtenerRealizadasV2NuevoModelo", new { IdAsesor = idsAsesor, Fecha = fecha, IdCentroCosto = idCentroCosto, IdAlumno = idAlumno, IdFaseOportunidad = idFaseOportunidad, IdTipoDato = idTipoDato, IdOrigen = idOrigen, Take = take, Skip = skip, IdCategoriaOrigen = idsCategoriaOrigen, IdProbabilidad = idProbabilidad, EstadoFilter = idEstado });
                actividadesRealizadas = JsonConvert.DeserializeObject<List<PruebaActividadRealizadaDTO>>(actividadesDB);
                return actividadesRealizadas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de actividades no programadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Retorna un entero con la cantidad de actividades no programadas</returns>
        public int ObtenerActividadesNoProgramadaCantidad(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                var cantidad = new Dictionary<string, int>();
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltro(filtros);
                if (tabAgenda.Nombre.Contains("1 Solicitud"))
                {
                    var queryConIdAsesor = "SELECT COUNT(Id) AS Cantidad  FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND Total=1 and FechaCreacion >FechaMenos_30 AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") AND  IdPersonal_Asignado IN (" + idAsesor.ToString() + " )" + filtro.ToString();
                    var queryConFiltros = "SELECT  COUNT(Id) AS Cantidad  FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND Total=1 and FechaCreacion > FechaMenos_30 AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") and IdEstadoOportunidad  IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    var cantidadActividadesDB = _dapper.FirstOrDefault(query, new { });
                    cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidadActividadesDB);

                }
                else
                {
                    var queryConIdAsesor = "SELECT COUNT(Id) AS Cantidad  FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") AND Total>1 and FechaCreacion>FechaMenos_30  and IdPersonal_Asignado IN (" + idAsesor.ToString() + ") " + filtro.ToString();
                    var queryConFiltros = "SELECT COUNT(Id) AS Cantidad  FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND Total>1 and FechaCreacion>FechaMenos_30 and  ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    var cantidadActividadesDB = _dapper.FirstOrDefault(query, new { });
                    cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidadActividadesDB);
                }
                return cantidad.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: Operaciones
        /// Autor: Fischer Valdez - Jose Villena
        /// Fecha: 18/01/2021
        /// <summary>
        /// Obtener las actividades programadas (Automaticas/Manuales)
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Retorna un entero con la cantidad de actividades programadas</returns>
        public List<ActividadAgendaDTO> ObtenerActividadesProgramada(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltro(filtros);
                if (tabAgenda.Nombre.Contains("Automatica"))
                {
                    var queryConIdAsesor = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND (IdEstadoOcurrencia = 2 OR IdEstadoOcurrencia is NULL)
                                                        AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND IdPersonal_Asignado IN ({idAsesor})
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 14
                                                        AND IdPersonal_Asignado IN ({idAsesor})
                                                ) AS T0";

                    filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                    queryConIdAsesor += string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro.Trim());

                    var queryConFiltros = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND (IdEstadoOcurrencia = 2 OR IdEstadoOcurrencia is NULL)
                                                        AND IdOportunidadRemarketingAgenda IS NULL
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 14
                                                ) AS T0";

                    queryConFiltros += string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro);

                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                else
                {
                    var queryConIdAsesor = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND (IdEstadoOcurrencia = 1 OR IdEstadoOcurrencia = 7)
                                                        AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND IdPersonal_Asignado IN ({idAsesor})
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 1
                                                        AND IdPersonal_Asignado IN ({idAsesor})
                                                ) AS T0";

                    filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                    queryConIdAsesor += string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro);

                    var queryConFiltros = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND (IdEstadoOcurrencia = 1 OR IdEstadoOcurrencia = 7)
                                                        AND IdOportunidadRemarketingAgenda IS NULL
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 1
                                                ) AS T0";

                    queryConFiltros += string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro);

                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AgendaTabRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene actividades no programadas
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor </param>
        /// <param name="filtros">Objeto de tipo diccionario</param>
        /// <returns>Lista de Actividades no programadas: List<ActividadAgendaDTO> </returns>
        public List<ActividadAgendaDTO> ObtenerActividadesNoProgramada(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                List<ActividadAgendaDTO> actividadesAgenda = new List<ActividadAgendaDTO>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltro(filtros);
                if (tabAgenda.Nombre.Contains("1 Solicitud"))
                {
                    var queryConIdAsesor = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND Total = 1 AND IdPersonal_Asignado IN ({idAsesor})
                                                        AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})
                                                        AND FechaCreacion > FechaMenos_30
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdPersonal_Asignado IN ({idAsesor})
                                                        AND IdOportunidadRemarketingAgenda = 2
                                                        AND FechaCreacion > FechaMenos_30
                                                ) AS T0";

                    filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                    queryConIdAsesor += (string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro)) + " ORDER BY Orden ASC";

                    var queryConFiltros = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND Total = 1 AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND FechaCreacion > FechaMenos_30
                                                        AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND FechaCreacion > FechaMenos_30
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 2
                                                ) AS T0";

                    queryConFiltros += (string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro)) + " ORDER BY Orden ASC";

                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                else
                {
                    var queryConIdAsesor = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})
                                                        AND Total > 1 AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND IdPersonal_Asignado IN ({idAsesor})
                                                        AND FechaCreacion > FechaMenos_30
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdPersonal_Asignado IN ({idAsesor})
                                                        AND IdOportunidadRemarketingAgenda = 11
                                                        AND FechaCreacion > FechaMenos_30
                                                ) AS T0";
                    filtro = filtro.Trim().StartsWith("AND") ? filtro.Trim().Substring(3).Trim() : filtro.Trim();

                    queryConIdAsesor += (string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro)) + " ORDER BY Orden ASC";

                    var queryConFiltros = $@"SELECT {tabAgenda.CamposVista}
                                                FROM (
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdTipoCategoriaOrigen IN ({tabAgenda.IdTipoCategoriaOrigen})
                                                        AND IdCategoriaOrigen IN ({tabAgenda.IdCategoriaOrigen})
                                                        AND IdTipoDato IN ({tabAgenda.IdTipoDato})
                                                        AND IdFaseOportunidad IN ({tabAgenda.IdFaseOportunidad})
                                                        AND IdEstadoOportunidad IN ({tabAgenda.IdEstadoOportunidad})
                                                        AND Total > 1 AND IdOportunidadRemarketingAgenda IS NULL
                                                        AND ProbabilidadActualDesc IN ({tabAgenda.Probabilidad})
                                                        AND FechaCreacion > FechaMenos_30
                                                    UNION
                                                    SELECT *
                                                    FROM {tabAgenda.VistaBaseDatos}
                                                    WHERE IdOportunidadRemarketingAgenda = 11
                                                        AND FechaCreacion > FechaMenos_30
                                                ) AS T0";

                    queryConFiltros += (string.IsNullOrEmpty(filtro) ? string.Empty : (" WHERE " + filtro)) + " ORDER BY Orden ASC";

                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var actividadesDB = _dapper.QueryDapper(query, new { });
                    actividadesAgenda = JsonConvert.DeserializeObject<List<ActividadAgendaDTO>>(actividadesDB);
                }
                return actividadesAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de las configuraciones de los tabs
        /// </summary>
        /// <param name="CodigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo TabAgendaDTO</returns>
        public List<TabAgendaDTO> ObtenerTabsConfigurados(string CodigoAreaTrabajo)
        {
            try
            {
                List<TabAgendaDTO> tabsAgenda = new List<TabAgendaDTO>();
                var query = "SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos, CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad, IdEstadoOportunidad, Probabilidad FROM com.V_ObtenerTabsAgendaConfigurado WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 and CodigoAreaTrabajo=@CodigoAreaTrabajo";
                var tabsAgendaDB = _dapper.QueryDapper(query, new { CodigoAreaTrabajo });
                tabsAgenda = JsonConvert.DeserializeObject<List<TabAgendaDTO>>(tabsAgendaDB);
                return tabsAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los tabs de la agenda que no requieren validacion
        /// </summary>
        /// <param name="CodigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo TabAgendaDTO</returns>
        public List<TabAgendaDTO> ObtenerTabsConfiguradosSinValidacion(string CodigoAreaTrabajo)
        {
            try
            {
                List<TabAgendaDTO> tabsAgenda = new List<TabAgendaDTO>();
                var query = "SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos, CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad, IdEstadoOportunidad, Probabilidad, Numeracion, ValidarFecha FROM com.V_ObtenerTabsAgendaConfigurado WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 AND Numeracion = 0 and CodigoAreaTrabajo=@CodigoAreaTrabajo";
                var tabsAgendaDB = _dapper.QueryDapper(query, new { CodigoAreaTrabajo });
                tabsAgenda = JsonConvert.DeserializeObject<List<TabAgendaDTO>>(tabsAgendaDB);
                return tabsAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los tabs de la agenda que requieren validacion
        /// </summary>
        /// <param name="CodigoAreaTrabajo">Cadena con el codigo del area de trabajo</param>
        /// <returns>Lista de objetos de tipo TabAgendaDTO</returns>
        public List<TabAgendaDTO> ObtenerTabsConfiguradoConValidacion(string CodigoAreaTrabajo)
        {
            try
            {
                List<TabAgendaDTO> tabsAgenda = new List<TabAgendaDTO>();
                var query = "SELECT Id, Nombre, VisualizarActividad, CargarInformacionInicial, VistaBaseDatos, CamposVista, IdTipoCategoriaOrigen, IdCategoriaOrigen, IdTipoDato, IdFaseOportunidad, IdEstadoOportunidad, Probabilidad, Numeracion, ValidarFecha FROM com.V_ObtenerTabsAgendaConfigurado WHERE  EstadoAgendaTab = 1 AND EstadoTabConfiguracion = 1 AND Numeracion > 0 and CodigoAreaTrabajo=@CodigoAreaTrabajo";
                var tabsAgendaDB = _dapper.QueryDapper(query, new { CodigoAreaTrabajo });
                tabsAgenda = JsonConvert.DeserializeObject<List<TabAgendaDTO>>(tabsAgendaDB);
                return tabsAgenda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la query para la peticion a la DB
        /// </summary>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Cadena con la query armada</returns>
        private string ObtenerFiltroCantidad(Dictionary<string, string> filtros)
        {
            try
            {
                var filtroVista = string.Empty;
                foreach (var prop in filtros)
                {
                    if (prop.Key.Equals("skip") || prop.Key.Equals("pageSize") || prop.Key.Equals("take") || prop.Key.Equals("page") || prop.Value.Equals("") || prop.Value == null || prop.Value.Length <= 0)
                    {
                        continue;
                    }
                    //if (prop.Key.Equals("skip"))
                    //{
                    //    skip = prop.Value;
                    //    continue;
                    //}
                    //if (prop.Key.Equals("pageSize"))
                    //{
                    //    take = prop.Value;
                    //    continue;
                    //}
                    if (prop.Key.Equals("IdProbabilidadRegistroPW"))
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                        continue;
                    }
                    if (prop.Key.Equals("CodigoMatricula") || prop.Key.Equals("dni"))
                    {
                        filtroVista += " AND " + prop.Key + " Like '%" + prop.Value + "%'";
                        continue;
                    }
                    if (prop.Key.Equals("FechaLlamada"))
                    {
                        //_filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "  <= convert(datetime, '" + prop.Value + "', 101)";
                        filtroVista += " AND " + prop.Key + " >= convert(datetime, '" + prop.Value + "', 101) AND " + prop.Key + "   <= convert(datetime, DATEADD(DAY, 1, Convert(date, '" + prop.Value + "')), 101)";
                        continue;
                    }
                    if (prop.Value.Contains(","))
                    {
                        filtroVista += " AND " + prop.Key + " IN (" + prop.Value + ")";
                    }
                    else
                    {
                        filtroVista += " AND " + prop.Key + " = " + prop.Value + "";
                    }
                }
                //if (skip != "" && take != "")
                //{
                //    filtroVista += " ORDER BY (select null) OFFSET " + skip + " ROWS FETCH NEXT " + take + " ROWS ONLY;";
                //}
                return filtroVista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de actividades por tab
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Retorna con la cantidad de actividades por tab</returns>
        public int CantidadActividadesPorTab(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                var cantidad = new Dictionary<string, int>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltroCantidad(filtros);

                if (tabAgenda.Probabilidad.Contains("0"))
                {
                    var queryConIdAsesor = "SELECT COUNT(Id) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                    var queryConFiltros = "SELECT COUNT(Id) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    //_query = "SELECT @CamposVista FROM @VistaBaseDatos WHERE IdTipoCategoriaOrigen IN ( @IdTipoCategoriaOrigen ) AND IdCategoriaOrigen IN ( @IdCategoriaOrigen ) AND IdTipoDato IN ( @IdTipoDato ) AND IdFaseOportunidad IN ( @IdFaseOportunidad ) AND IdEstadoOportunidad IN ( @IdEstadoOportunidad ) AND IdPersonal_Asignado IN( @idAsesor ) " + _filtro;
                    var cantidadActividadesDB = _dapper.FirstOrDefault(query, new { });
                    //var actividadesDB = _dapper.QueryDapper(_query, new { tabAgenda.CamposVista , tabAgenda.VistaBaseDatos, tabAgenda.IdTipoCategoriaOrigen, tabAgenda.IdCategoriaOrigen, tabAgenda.IdTipoDato , tabAgenda.IdFaseOportunidad , tabAgenda.IdEstadoOportunidad, idAsesor });
                    cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidadActividadesDB);
                }
                else
                {
                    var queryConIdAsesor = "SELECT COUNT(Id) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") AND IdPersonal_Asignado in (" + idAsesor.ToString() + ") " + filtro;
                    var queryConFiltros = "SELECT COUNT(Id) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdTipoCategoriaOrigen IN (" + tabAgenda.IdTipoCategoriaOrigen.ToString() + ") AND IdCategoriaOrigen IN (" + tabAgenda.IdCategoriaOrigen.ToString() + ") AND IdTipoDato IN (" + tabAgenda.IdTipoDato.ToString() + ") AND IdFaseOportunidad IN (" + tabAgenda.IdFaseOportunidad.ToString() + ") AND IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND ProbabilidadActualDesc IN (" + tabAgenda.Probabilidad.ToString() + ") " + filtro.ToString();
                    query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                    var cantidadActividadesDB = _dapper.FirstOrDefault(query, new { });
                    cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidadActividadesDB);
                }
                return cantidad.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: AgendaTabRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtiene la cantidad de actividades por tab de operaciones
        /// </summary>
        /// <param name="tabAgenda">Objeto de tipo TabAgendaDTO</param>
        /// <param name="idAsesor">Id del asesor </param>
        /// <param name="filtros">Objeto de tipo diccionario (string, string)</param>
        /// <returns>Retorna con la cantidad de actividades por tab de operaciones: Dictionary<string, int>() </returns>
        public int CantidadActividadesPorTabOperaciones(TabAgendaDTO tabAgenda, int idAsesor, Dictionary<string, string> filtros)
        {
            try
            {
                var cantidad = new Dictionary<string, int>();
                var query = string.Empty;
                var filtro = this.ObtenerFiltroCantidad(filtros);

                var queryConIdAsesor = "SELECT COUNT(*) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") AND IdPersonal_Asignado IN ( " + idAsesor.ToString() + ") " + filtro;
                var queryConFiltros = "SELECT COUNT(*) AS Cantidad FROM " + tabAgenda.VistaBaseDatos.ToString() + " WHERE IdEstadoOportunidad IN (" + tabAgenda.IdEstadoOportunidad.ToString() + ") " + filtro.ToString();
                query = idAsesor == 0 ? queryConFiltros : queryConIdAsesor;
                //_query = "SELECT @CamposVista FROM @VistaBaseDatos WHERE IdTipoCategoriaOrigen IN ( @IdTipoCategoriaOrigen ) AND IdCategoriaOrigen IN ( @IdCategoriaOrigen ) AND IdTipoDato IN ( @IdTipoDato ) AND IdFaseOportunidad IN ( @IdFaseOportunidad ) AND IdEstadoOportunidad IN ( @IdEstadoOportunidad ) AND IdPersonal_Asignado IN( @idAsesor ) " + _filtro;
                var cantidadActividadesDB = _dapper.FirstOrDefault(query, new { });
                //var actividadesDB = _dapper.QueryDapper(_query, new { tabAgenda.CamposVista , tabAgenda.VistaBaseDatos, tabAgenda.IdTipoCategoriaOrigen, tabAgenda.IdCategoriaOrigen, tabAgenda.IdTipoDato , tabAgenda.IdFaseOportunidad , tabAgenda.IdEstadoOportunidad, idAsesor });
                cantidad = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidadActividadesDB);

                return cantidad.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

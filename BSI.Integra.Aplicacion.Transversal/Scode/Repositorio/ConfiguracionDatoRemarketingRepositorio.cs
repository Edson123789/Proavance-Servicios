using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConfiguracionDatoRemarketing
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionDatoRemarketing
    /// </summary>
    public class ConfiguracionDatoRemarketingRepositorio : BaseRepository<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketingBO>
    {
        #region Metodos Base
        public ConfiguracionDatoRemarketingRepositorio() : base()
        {
        }
        public ConfiguracionDatoRemarketingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDatoRemarketingBO> GetBy(Expression<Func<TConfiguracionDatoRemarketing, bool>> filter)
        {
            IEnumerable<TConfiguracionDatoRemarketing> listado = base.GetBy(filter);
            List<ConfiguracionDatoRemarketingBO> listadoBO = new List<ConfiguracionDatoRemarketingBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionDatoRemarketingBO FirstById(int id)
        {
            try
            {
                TConfiguracionDatoRemarketing entidad = base.FirstById(id);
                ConfiguracionDatoRemarketingBO objetoBO = new ConfiguracionDatoRemarketingBO();
                Mapper.Map<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDatoRemarketingBO FirstBy(Expression<Func<TConfiguracionDatoRemarketing, bool>> filter)
        {
            try
            {
                TConfiguracionDatoRemarketing entidad = base.FirstBy(filter);
                ConfiguracionDatoRemarketingBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDatoRemarketingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDatoRemarketing entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDatoRemarketingBO> listadoBO)
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

        public bool Update(ConfiguracionDatoRemarketingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDatoRemarketing entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDatoRemarketingBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDatoRemarketing entidad, ConfiguracionDatoRemarketingBO objetoBO)
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

        private TConfiguracionDatoRemarketing MapeoEntidad(ConfiguracionDatoRemarketingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketing entidad = new TConfiguracionDatoRemarketing();
                entidad = Mapper.Map<ConfiguracionDatoRemarketingBO, TConfiguracionDatoRemarketing>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionDatoRemarketingBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionDatoRemarketing, bool>>> filters, Expression<Func<TConfiguracionDatoRemarketing, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionDatoRemarketing> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionDatoRemarketingBO> listadoBO = new List<ConfiguracionDatoRemarketingBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de las configuraciones guardadas
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionDatoRemarketingGrillaDTO</returns>
        public List<ConfiguracionDatoRemarketingGrillaDTO> ObtenerConfiguracionesDatoRemarketing()
        {
            try
            {
                List<ConfiguracionDatoRemarketingGrillaDTO> resultadoConsulta = new List<ConfiguracionDatoRemarketingGrillaDTO>();

                string consulta = "SELECT Id, IdAgendaTab, NombreAgendaTab, FechaInicio, FechaFin, Vigente, IdTipoDato, NombreTipoDato, IdTipoCategoriaOrigen, NombreTipoCategoriaOrigen, IdCategoriaOrigen, NombreCategoriaOrigen, IdProbabilidadRegistroPw, NombreProbabilidadRegistroPw FROM mkt.V_ObtenerListaConfiguracionDatoRemarketing";
                string resultadoConsultaSinProcesar = _dapper.QueryDapper(consulta, null);

                if (!string.IsNullOrEmpty(resultadoConsultaSinProcesar) && !resultadoConsultaSinProcesar.Contains("[]"))
                    resultadoConsulta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingGrillaDTO>>(resultadoConsultaSinProcesar);

                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 20/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de las configuraciones guardadas
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionDatoRemarketingGrillaDTO</returns>
        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ObtenerAgendaTabVentasParaConfiguracion()
        {
            try
            {
                List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> resultadoConsulta = new List<ConfiguracionDatoRemarketingAgendaTabVentasDTO>();

                string consulta = "SELECT IdAgendaTab, NombreAgendaTab FROM mkt.V_ObtenerListaAgendaTabVentas";
                string resultadoConsultaSinProcesar = _dapper.QueryDapper(consulta, null);

                if (!string.IsNullOrEmpty(resultadoConsultaSinProcesar) && !resultadoConsultaSinProcesar.Contains("[]"))
                    resultadoConsulta = JsonConvert.DeserializeObject<List<ConfiguracionDatoRemarketingAgendaTabVentasDTO>>(resultadoConsultaSinProcesar);

                return resultadoConsulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 08/09/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id del tab de redireccion
        /// </summary>
        /// <returns>Entero con el id del tab de redireccion</returns>
        public int ObtenerTabRedireccionRemarketing(int idTipoDato, int idSubCategoriaDato, int idProbabilidadRegistroPw)
        {
            try
            {
                ValorIntDTO idAgendaTab = new ValorIntDTO();

                string spPeticion = "[com].[SP_ObtenerTabRedireccionRemarketing]";
                string resultadoPeticion = _dapper.QuerySPFirstOrDefault(spPeticion, new { IdTipoDato = idTipoDato, IdSubCategoriaDato = idSubCategoriaDato, IdProbabilidadRegistroPw = idProbabilidadRegistroPw });

                if (!string.IsNullOrEmpty(resultadoPeticion) && !resultadoPeticion.Contains("[]"))
                    idAgendaTab = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoPeticion);

                return idAgendaTab != null ? idAgendaTab.Valor : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
    public class AgendaTabConfiguracionRepositorio : BaseRepository<TAgendaTabConfiguracion, AgendaTabConfiguracionBO>
    {
        #region Metodos Base
        public AgendaTabConfiguracionRepositorio() : base()
        {
        }
        public AgendaTabConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AgendaTabConfiguracionBO> GetBy(Expression<Func<TAgendaTabConfiguracion, bool>> filter)
        {
            IEnumerable<TAgendaTabConfiguracion> listado = base.GetBy(filter);
            List<AgendaTabConfiguracionBO> listadoBO = new List<AgendaTabConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                AgendaTabConfiguracionBO objetoBO = Mapper.Map<TAgendaTabConfiguracion, AgendaTabConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AgendaTabConfiguracionBO FirstById(int id)
        {
            try
            {
                TAgendaTabConfiguracion entidad = base.FirstById(id);
                AgendaTabConfiguracionBO objetoBO = new AgendaTabConfiguracionBO();
                Mapper.Map<TAgendaTabConfiguracion, AgendaTabConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AgendaTabConfiguracionBO FirstBy(Expression<Func<TAgendaTabConfiguracion, bool>> filter)
        {
            try
            {
                TAgendaTabConfiguracion entidad = base.FirstBy(filter);
                AgendaTabConfiguracionBO objetoBO = Mapper.Map<TAgendaTabConfiguracion, AgendaTabConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool Insert(AgendaTabConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAgendaTabConfiguracion entidad = MapeoEntidad(objetoBO);

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
        public bool Insert(IEnumerable<AgendaTabConfiguracionBO> listadoBO)
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
        public bool Update(AgendaTabConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAgendaTabConfiguracion entidad = MapeoEntidad(objetoBO);

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
        public bool Update(IEnumerable<AgendaTabConfiguracionBO> listadoBO)
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
        private void AsignacionId(TAgendaTabConfiguracion entidad, AgendaTabConfiguracionBO objetoBO)
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
        private TAgendaTabConfiguracion MapeoEntidad(AgendaTabConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAgendaTabConfiguracion entidad = new TAgendaTabConfiguracion();
                entidad = Mapper.Map<AgendaTabConfiguracionBO, TAgendaTabConfiguracion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        /// <summary>
        /// Obtiene todos los registros de T_AgendaTabConfiguracion (estado=1) para llenar la grilla de su CRUD
        /// </summary>
        /// <returns></returns>
        public List<AgendaTabConfiguracionDTO> ObtenerTodoParaGrilla()
        {
            try
            {
                List<AgendaTabConfiguracionDTO> data = new List<AgendaTabConfiguracionDTO>();
                var _query = "SELECT Id, IdAgendaTab,NombreAgendaTab,ListaTipoCategoriaOrigen,ListaCategoriaOrigen,ListaTipoDato,ListaFaseOportunidad,ListaEstadoOportunidad,ListaProbabilidad  " +
                    "FROM [mkt].[V_TAgendaTabConfiguracion] ";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<AgendaTabConfiguracionDTO>>(dataDB);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los tabs que aun no estan asociados a una configuracion de T_AgendaTabConfiguracion, usado para llenado de combobox del modulo Conf. Tabs Agenda
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTabsSinConfigurar()
        {
            try
            {
                List<FiltroDTO> data = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre  " +
                    "FROM [mkt].[V_TAgendaTabDisponible] ";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<FiltroDTO>>(dataDB);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

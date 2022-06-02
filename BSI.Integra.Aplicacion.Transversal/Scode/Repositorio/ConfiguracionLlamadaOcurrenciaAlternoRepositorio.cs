using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfiguracionLlamadaOcurrenciaAlternoRepositorio : BaseRepository<TConfiguracionLlamadaOcurrenciaAlterno, ConfiguracionLlamadaOcurrenciaAlternoBO>
    {
        #region Metodos Base
        public ConfiguracionLlamadaOcurrenciaAlternoRepositorio() : base()
        {
        }
        public ConfiguracionLlamadaOcurrenciaAlternoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionLlamadaOcurrenciaAlternoBO> GetBy(Expression<Func<TConfiguracionLlamadaOcurrenciaAlterno, bool>> filter)
        {
            IEnumerable<TConfiguracionLlamadaOcurrenciaAlterno> listado = base.GetBy(filter);
            List<ConfiguracionLlamadaOcurrenciaAlternoBO> listadoBO = new List<ConfiguracionLlamadaOcurrenciaAlternoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO = Mapper.Map<TConfiguracionLlamadaOcurrenciaAlterno, ConfiguracionLlamadaOcurrenciaAlternoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionLlamadaOcurrenciaAlternoBO FirstById(int id)
        {
            try
            {
                TConfiguracionLlamadaOcurrenciaAlterno entidad = base.FirstById(id);
                ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO = new ConfiguracionLlamadaOcurrenciaAlternoBO();
                Mapper.Map<TConfiguracionLlamadaOcurrenciaAlterno, ConfiguracionLlamadaOcurrenciaAlternoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionLlamadaOcurrenciaAlternoBO FirstBy(Expression<Func<TConfiguracionLlamadaOcurrenciaAlterno, bool>> filter)
        {
            try
            {
                TConfiguracionLlamadaOcurrenciaAlterno entidad = base.FirstBy(filter);
                ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO = Mapper.Map<TConfiguracionLlamadaOcurrenciaAlterno, ConfiguracionLlamadaOcurrenciaAlternoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionLlamadaOcurrenciaAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionLlamadaOcurrenciaAlternoBO> listadoBO)
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

        public bool Update(ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionLlamadaOcurrenciaAlterno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionLlamadaOcurrenciaAlternoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionLlamadaOcurrenciaAlterno entidad, ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO)
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

        private TConfiguracionLlamadaOcurrenciaAlterno MapeoEntidad(ConfiguracionLlamadaOcurrenciaAlternoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionLlamadaOcurrenciaAlterno entidad = new TConfiguracionLlamadaOcurrenciaAlterno();
                entidad = Mapper.Map<ConfiguracionLlamadaOcurrenciaAlternoBO, TConfiguracionLlamadaOcurrenciaAlterno>(objetoBO,
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
        /// Obtiene todas las configuraciones de llamada segun un Id de Ocurrencia
        /// </summary>
        /// <returns></returns>
        public List<ConfiguracionLlamadaOcurrenciaAlternoBO> ObtenerTodasConfigLlamadaPorIdOcurrencia(int IdOcurrencia)
        {
            try
            {

                List<ConfiguracionLlamadaOcurrenciaAlternoBO> configuraciones = new List<ConfiguracionLlamadaOcurrenciaAlternoBO>();
                string _query = "SELECT Id, IdOcurrencia, IdConectorOcurrenciaLlamada,NumeroLlamada, IdCondicionOcurrenciaLLamada, IdFaseTiempoLlamada, Duracion, Estado, FechaModificacion, UsuarioModificacion FROM mkt.T_ConfiguracionLlamadaOcurrenciaAlterno WHERE IdOcurrencia=" + IdOcurrencia + " AND Estado=1";
                var configuracionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(configuracionesDB) && !configuracionesDB.Contains("[]"))
                {
                    configuraciones = JsonConvert.DeserializeObject<List<ConfiguracionLlamadaOcurrenciaAlternoBO>>(configuracionesDB);
                }
                return configuraciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }


}

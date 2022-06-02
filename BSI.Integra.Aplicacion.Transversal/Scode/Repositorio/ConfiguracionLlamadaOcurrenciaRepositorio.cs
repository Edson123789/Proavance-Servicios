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
    public class ConfiguracionLlamadaOcurrenciaRepositorio : BaseRepository<TConfiguracionLlamadaOcurrencia, ConfiguracionLlamadaOcurrenciaBO>
    {
        #region Metodos Base
        public ConfiguracionLlamadaOcurrenciaRepositorio() : base()
        {
        }
        public ConfiguracionLlamadaOcurrenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionLlamadaOcurrenciaBO> GetBy(Expression<Func<TConfiguracionLlamadaOcurrencia, bool>> filter)
        {
            IEnumerable<TConfiguracionLlamadaOcurrencia> listado = base.GetBy(filter);
            List<ConfiguracionLlamadaOcurrenciaBO> listadoBO = new List<ConfiguracionLlamadaOcurrenciaBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionLlamadaOcurrenciaBO objetoBO = Mapper.Map<TConfiguracionLlamadaOcurrencia, ConfiguracionLlamadaOcurrenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionLlamadaOcurrenciaBO FirstById(int id)
        {
            try
            {
                TConfiguracionLlamadaOcurrencia entidad = base.FirstById(id);
                ConfiguracionLlamadaOcurrenciaBO objetoBO = new ConfiguracionLlamadaOcurrenciaBO();
                Mapper.Map<TConfiguracionLlamadaOcurrencia, ConfiguracionLlamadaOcurrenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionLlamadaOcurrenciaBO FirstBy(Expression<Func<TConfiguracionLlamadaOcurrencia, bool>> filter)
        {
            try
            {
                TConfiguracionLlamadaOcurrencia entidad = base.FirstBy(filter);
                ConfiguracionLlamadaOcurrenciaBO objetoBO = Mapper.Map<TConfiguracionLlamadaOcurrencia, ConfiguracionLlamadaOcurrenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionLlamadaOcurrenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionLlamadaOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionLlamadaOcurrenciaBO> listadoBO)
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

        public bool Update(ConfiguracionLlamadaOcurrenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionLlamadaOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionLlamadaOcurrenciaBO> listadoBO)
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
        private void AsignacionId(TConfiguracionLlamadaOcurrencia entidad, ConfiguracionLlamadaOcurrenciaBO objetoBO)
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

        private TConfiguracionLlamadaOcurrencia MapeoEntidad(ConfiguracionLlamadaOcurrenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionLlamadaOcurrencia entidad = new TConfiguracionLlamadaOcurrencia();
                entidad = Mapper.Map<ConfiguracionLlamadaOcurrenciaBO, TConfiguracionLlamadaOcurrencia>(objetoBO,
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
        public List<ConfiguracionLlamadaOcurrenciaBO> ObtenerTodasConfigLlamadaPorIdOcurrencia(int IdOcurrencia)
        {
            try
            {
                
                List<ConfiguracionLlamadaOcurrenciaBO> configuraciones = new List<ConfiguracionLlamadaOcurrenciaBO>();
                string _query = "SELECT Id, IdOcurrencia, IdConectorOcurrenciaLlamada,NumeroLlamada, IdCondicionOcurrenciaLLamada, IdFaseTiempoLlamada, Duracion, Estado, FechaModificacion, UsuarioModificacion FROM mkt.T_ConfiguracionLlamadaOcurrencia WHERE IdOcurrencia=" + IdOcurrencia + " AND Estado=1";
                var configuracionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(configuracionesDB) && !configuracionesDB.Contains("[]"))
                {
                    configuraciones = JsonConvert.DeserializeObject<List<ConfiguracionLlamadaOcurrenciaBO>>(configuracionesDB);
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

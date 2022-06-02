using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: ConfiguracionChat
    /// Autor: Gian Miranda
    /// Fecha: 15/04/2021
    /// <summary>
    /// Metodos para la interaccion con la db de la tabla mkt.T_ConfiguracionChat
    /// </summary>
    public class ConfiguracionChatRepositorio : BaseRepository<TConfiguracionChat, ConfiguracionChatBO>
    {
        #region Metodos Base
        public ConfiguracionChatRepositorio() : base()
        {
        }
        public ConfiguracionChatRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionChatBO> GetBy(Expression<Func<TConfiguracionChat, bool>> filter)
        {
            IEnumerable<TConfiguracionChat> listado = base.GetBy(filter);
            List<ConfiguracionChatBO> listadoBO = new List<ConfiguracionChatBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionChatBO objetoBO = Mapper.Map<TConfiguracionChat, ConfiguracionChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionChatBO FirstById(int id)
        {
            try
            {
                TConfiguracionChat entidad = base.FirstById(id);
                ConfiguracionChatBO objetoBO = new ConfiguracionChatBO();
                Mapper.Map<TConfiguracionChat, ConfiguracionChatBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionChatBO FirstBy(Expression<Func<TConfiguracionChat, bool>> filter)
        {
            try
            {
                TConfiguracionChat entidad = base.FirstBy(filter);
                ConfiguracionChatBO objetoBO = Mapper.Map<TConfiguracionChat, ConfiguracionChatBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionChatBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionChat entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionChatBO> listadoBO)
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

        public bool Update(ConfiguracionChatBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionChat entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionChatBO> listadoBO)
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
        private void AsignacionId(TConfiguracionChat entidad, ConfiguracionChatBO objetoBO)
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

        private TConfiguracionChat MapeoEntidad(ConfiguracionChatBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionChat entidad = new TConfiguracionChat();
                entidad = Mapper.Map<ConfiguracionChatBO, TConfiguracionChat>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionChatBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionChat, bool>>> filters, Expression<Func<TConfiguracionChat, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionChat> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionChatBO> listadoBO = new List<ConfiguracionChatBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionChatBO objetoBO = Mapper.Map<TConfiguracionChat, ConfiguracionChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene todas las configuraciones de chat
        /// </summary>
        /// <returns>Lista de objetos de clase ConfiguracionChatBO</returns>
        public List<ConfiguracionChatBO> ObtenerTodo() {
            try
            {
                return this.GetBy(x => x.Estado == true).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

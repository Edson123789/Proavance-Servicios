using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;
namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class ConfiguracionMarcadorMensajeRepositorio : BaseRepository<TConfiguracionMarcadorMensaje, ConfiguracionMarcadorMensajeBO>
    {
        #region Metodos Base
        public ConfiguracionMarcadorMensajeRepositorio() : base()
        {
        }
        public ConfiguracionMarcadorMensajeRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionMarcadorMensajeBO> GetBy(Expression<Func<TConfiguracionMarcadorMensaje, bool>> filter)
        {
            IEnumerable<TConfiguracionMarcadorMensaje> listado = base.GetBy(filter);
            List<ConfiguracionMarcadorMensajeBO> listadoBO = new List<ConfiguracionMarcadorMensajeBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionMarcadorMensajeBO objetoBO = Mapper.Map<TConfiguracionMarcadorMensaje, ConfiguracionMarcadorMensajeBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionMarcadorMensajeBO FirstById(int id)
        {
            try
            {
                TConfiguracionMarcadorMensaje entidad = base.FirstById(id);
                ConfiguracionMarcadorMensajeBO objetoBO = new ConfiguracionMarcadorMensajeBO();
                Mapper.Map<TConfiguracionMarcadorMensaje, ConfiguracionMarcadorMensajeBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionMarcadorMensajeBO FirstBy(Expression<Func<TConfiguracionMarcadorMensaje, bool>> filter)
        {
            try
            {
                TConfiguracionMarcadorMensaje entidad = base.FirstBy(filter);
                ConfiguracionMarcadorMensajeBO objetoBO = Mapper.Map<TConfiguracionMarcadorMensaje, ConfiguracionMarcadorMensajeBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionMarcadorMensajeBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionMarcadorMensaje entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionMarcadorMensajeBO> listadoBO)
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

        public bool Update(ConfiguracionMarcadorMensajeBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionMarcadorMensaje entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionMarcadorMensajeBO> listadoBO)
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
        private void AsignacionId(TConfiguracionMarcadorMensaje entidad, ConfiguracionMarcadorMensajeBO objetoBO)
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

        private TConfiguracionMarcadorMensaje MapeoEntidad(ConfiguracionMarcadorMensajeBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionMarcadorMensaje entidad = new TConfiguracionMarcadorMensaje();
                entidad = Mapper.Map<ConfiguracionMarcadorMensajeBO, TConfiguracionMarcadorMensaje>(objetoBO,
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
    }
}

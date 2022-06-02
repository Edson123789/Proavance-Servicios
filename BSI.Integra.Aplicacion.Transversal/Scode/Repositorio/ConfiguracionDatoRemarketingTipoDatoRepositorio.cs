using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConfiguracionDatoRemarketingTipoDato
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionDatoRemarketingTipoDato
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoDatoRepositorio : BaseRepository<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoBO>
    {
        #region Metodos Base
        public ConfiguracionDatoRemarketingTipoDatoRepositorio() : base()
        {
        }
        public ConfiguracionDatoRemarketingTipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDatoRemarketingTipoDatoBO> GetBy(Expression<Func<TConfiguracionDatoRemarketingTipoDato, bool>> filter)
        {
            IEnumerable<TConfiguracionDatoRemarketingTipoDato> listado = base.GetBy(filter);
            List<ConfiguracionDatoRemarketingTipoDatoBO> listadoBO = new List<ConfiguracionDatoRemarketingTipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingTipoDatoBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionDatoRemarketingTipoDatoBO FirstById(int id)
        {
            try
            {
                TConfiguracionDatoRemarketingTipoDato entidad = base.FirstById(id);
                ConfiguracionDatoRemarketingTipoDatoBO objetoBO = new ConfiguracionDatoRemarketingTipoDatoBO();
                Mapper.Map<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDatoRemarketingTipoDatoBO FirstBy(Expression<Func<TConfiguracionDatoRemarketingTipoDato, bool>> filter)
        {
            try
            {
                TConfiguracionDatoRemarketingTipoDato entidad = base.FirstBy(filter);
                ConfiguracionDatoRemarketingTipoDatoBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDatoRemarketingTipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDatoRemarketingTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDatoRemarketingTipoDatoBO> listadoBO)
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

        public bool Update(ConfiguracionDatoRemarketingTipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDatoRemarketingTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDatoRemarketingTipoDatoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDatoRemarketingTipoDato entidad, ConfiguracionDatoRemarketingTipoDatoBO objetoBO)
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

        private TConfiguracionDatoRemarketingTipoDato MapeoEntidad(ConfiguracionDatoRemarketingTipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingTipoDato entidad = new TConfiguracionDatoRemarketingTipoDato();
                entidad = Mapper.Map<ConfiguracionDatoRemarketingTipoDatoBO, TConfiguracionDatoRemarketingTipoDato>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionDatoRemarketingTipoDatoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionDatoRemarketingTipoDato, bool>>> filters, Expression<Func<TConfiguracionDatoRemarketingTipoDato, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionDatoRemarketingTipoDato> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionDatoRemarketingTipoDatoBO> listadoBO = new List<ConfiguracionDatoRemarketingTipoDatoBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingTipoDatoBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

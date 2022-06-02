using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConfiguracionDatoRemarketingTipoCategoriaOrigen
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionDatoRemarketingTipoCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio : BaseRepository<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>
    {
        #region Metodos Base
        public ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio() : base()
        {
        }
        public ConfiguracionDatoRemarketingTipoCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> GetBy(Expression<Func<TConfiguracionDatoRemarketingTipoCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> listado = base.GetBy(filter);
            List<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> listadoBO = new List<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionDatoRemarketingTipoCategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad = base.FirstById(id);
                ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO = new ConfiguracionDatoRemarketingTipoCategoriaOrigenBO();
                Mapper.Map<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDatoRemarketingTipoCategoriaOrigenBO FirstBy(Expression<Func<TConfiguracionDatoRemarketingTipoCategoriaOrigen, bool>> filter)
        {
            try
            {
                TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad = base.FirstBy(filter);
                ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> listadoBO)
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

        public bool Update(ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO)
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

        private TConfiguracionDatoRemarketingTipoCategoriaOrigen MapeoEntidad(ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingTipoCategoriaOrigen entidad = new TConfiguracionDatoRemarketingTipoCategoriaOrigen();
                entidad = Mapper.Map<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO, TConfiguracionDatoRemarketingTipoCategoriaOrigen>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionDatoRemarketingTipoCategoriaOrigen, bool>>> filters, Expression<Func<TConfiguracionDatoRemarketingTipoCategoriaOrigen, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO> listadoBO = new List<ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingTipoCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

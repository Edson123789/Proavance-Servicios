using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConfiguracionDatoRemarketingCategoriaOrigen
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionDatoRemarketingCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingCategoriaOrigenRepositorio : BaseRepository<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigenBO>
    {
        #region Metodos Base
        public ConfiguracionDatoRemarketingCategoriaOrigenRepositorio() : base()
        {
        }
        public ConfiguracionDatoRemarketingCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigenBO> GetBy(Expression<Func<TConfiguracionDatoRemarketingCategoriaOrigen, bool>> filter)
        {
            IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> listado = base.GetBy(filter);
            List<ConfiguracionDatoRemarketingCategoriaOrigenBO> listadoBO = new List<ConfiguracionDatoRemarketingCategoriaOrigenBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionDatoRemarketingCategoriaOrigenBO FirstById(int id)
        {
            try
            {
                TConfiguracionDatoRemarketingCategoriaOrigen entidad = base.FirstById(id);
                ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO = new ConfiguracionDatoRemarketingCategoriaOrigenBO();
                Mapper.Map<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDatoRemarketingCategoriaOrigenBO FirstBy(Expression<Func<TConfiguracionDatoRemarketingCategoriaOrigen, bool>> filter)
        {
            try
            {
                TConfiguracionDatoRemarketingCategoriaOrigen entidad = base.FirstBy(filter);
                ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDatoRemarketingCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigenBO> listadoBO)
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

        public bool Update(ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDatoRemarketingCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigenBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDatoRemarketingCategoriaOrigen entidad, ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO)
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

        private TConfiguracionDatoRemarketingCategoriaOrigen MapeoEntidad(ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingCategoriaOrigen entidad = new TConfiguracionDatoRemarketingCategoriaOrigen();
                entidad = Mapper.Map<ConfiguracionDatoRemarketingCategoriaOrigenBO, TConfiguracionDatoRemarketingCategoriaOrigen>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionDatoRemarketingCategoriaOrigen, bool>>> filters, Expression<Func<TConfiguracionDatoRemarketingCategoriaOrigen, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionDatoRemarketingCategoriaOrigenBO> listadoBO = new List<ConfiguracionDatoRemarketingCategoriaOrigenBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingCategoriaOrigenBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

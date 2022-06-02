using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConfiguracionDatoRemarketingProbabilidadRegistro
    /// Autor: Gian Miranda
    /// Fecha: 17/08/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionDatoRemarketingProbabilidadRegistro
    /// </summary>
    public class ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio : BaseRepository<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistroBO>
    {
        #region Metodos Base
        public ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio() : base()
        {
        }
        public ConfiguracionDatoRemarketingProbabilidadRegistroRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistroBO> GetBy(Expression<Func<TConfiguracionDatoRemarketingProbabilidadRegistro, bool>> filter)
        {
            IEnumerable<TConfiguracionDatoRemarketingProbabilidadRegistro> listado = base.GetBy(filter);
            List<ConfiguracionDatoRemarketingProbabilidadRegistroBO> listadoBO = new List<ConfiguracionDatoRemarketingProbabilidadRegistroBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionDatoRemarketingProbabilidadRegistroBO FirstById(int id)
        {
            try
            {
                TConfiguracionDatoRemarketingProbabilidadRegistro entidad = base.FirstById(id);
                ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO = new ConfiguracionDatoRemarketingProbabilidadRegistroBO();
                Mapper.Map<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionDatoRemarketingProbabilidadRegistroBO FirstBy(Expression<Func<TConfiguracionDatoRemarketingProbabilidadRegistro, bool>> filter)
        {
            try
            {
                TConfiguracionDatoRemarketingProbabilidadRegistro entidad = base.FirstBy(filter);
                ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionDatoRemarketingProbabilidadRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistroBO> listadoBO)
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

        public bool Update(ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionDatoRemarketingProbabilidadRegistro entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistroBO> listadoBO)
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
        private void AsignacionId(TConfiguracionDatoRemarketingProbabilidadRegistro entidad, ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO)
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

        private TConfiguracionDatoRemarketingProbabilidadRegistro MapeoEntidad(ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionDatoRemarketingProbabilidadRegistro entidad = new TConfiguracionDatoRemarketingProbabilidadRegistro();
                entidad = Mapper.Map<ConfiguracionDatoRemarketingProbabilidadRegistroBO, TConfiguracionDatoRemarketingProbabilidadRegistro>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistroBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionDatoRemarketingProbabilidadRegistro, bool>>> filters, Expression<Func<TConfiguracionDatoRemarketingProbabilidadRegistro, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionDatoRemarketingProbabilidadRegistro> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionDatoRemarketingProbabilidadRegistroBO> listadoBO = new List<ConfiguracionDatoRemarketingProbabilidadRegistroBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionDatoRemarketingProbabilidadRegistroBO objetoBO = Mapper.Map<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

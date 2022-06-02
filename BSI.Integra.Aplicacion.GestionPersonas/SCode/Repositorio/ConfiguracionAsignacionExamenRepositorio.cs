using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    public class ConfiguracionAsignacionExamenRepositorio: BaseRepository<TConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenBO>
    {
        #region Metodos Base
        public ConfiguracionAsignacionExamenRepositorio() : base()
        {
        }
        public ConfiguracionAsignacionExamenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionAsignacionExamenBO> GetBy(Expression<Func<TConfiguracionAsignacionExamen, bool>> filter)
        {
            IEnumerable<TConfiguracionAsignacionExamen> listado = base.GetBy(filter);
            List<ConfiguracionAsignacionExamenBO> listadoBO = new List<ConfiguracionAsignacionExamenBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionAsignacionExamenBO objetoBO = Mapper.Map<TConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionAsignacionExamenBO FirstById(int id)
        {
            try
            {
                TConfiguracionAsignacionExamen entidad = base.FirstById(id);
                ConfiguracionAsignacionExamenBO objetoBO = new ConfiguracionAsignacionExamenBO();
                Mapper.Map<TConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionAsignacionExamenBO FirstBy(Expression<Func<TConfiguracionAsignacionExamen, bool>> filter)
        {
            try
            {
                TConfiguracionAsignacionExamen entidad = base.FirstBy(filter);
                ConfiguracionAsignacionExamenBO objetoBO = Mapper.Map<TConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionAsignacionExamenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionAsignacionExamen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionAsignacionExamenBO> listadoBO)
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

        public bool Update(ConfiguracionAsignacionExamenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionAsignacionExamen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionAsignacionExamenBO> listadoBO)
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
        private void AsignacionId(TConfiguracionAsignacionExamen entidad, ConfiguracionAsignacionExamenBO objetoBO)
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

        private TConfiguracionAsignacionExamen MapeoEntidad(ConfiguracionAsignacionExamenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionAsignacionExamen entidad = new TConfiguracionAsignacionExamen();
                entidad = Mapper.Map<ConfiguracionAsignacionExamenBO, TConfiguracionAsignacionExamen>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionAsignacionExamenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionAsignacionExamen, bool>>> filters, Expression<Func<TConfiguracionAsignacionExamen, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionAsignacionExamen> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionAsignacionExamenBO> listadoBO = new List<ConfiguracionAsignacionExamenBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionAsignacionExamenBO objetoBO = Mapper.Map<TConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

    }
}

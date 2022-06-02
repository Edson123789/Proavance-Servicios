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
    public class ConfiguracionBicRepositorio : BaseRepository<TConfiguracionBic, ConfiguracionBicBO>
    {
        #region Metodos Base
        public ConfiguracionBicRepositorio() : base()
        {
        }
        public ConfiguracionBicRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionBicBO> GetBy(Expression<Func<TConfiguracionBic, bool>> filter)
        {
            IEnumerable<TConfiguracionBic> listado = base.GetBy(filter);
            List<ConfiguracionBicBO> listadoBO = new List<ConfiguracionBicBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionBicBO objetoBO = Mapper.Map<TConfiguracionBic, ConfiguracionBicBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionBicBO FirstById(int id)
        {
            try
            {
                TConfiguracionBic entidad = base.FirstById(id);
                ConfiguracionBicBO objetoBO = new ConfiguracionBicBO();
                Mapper.Map<TConfiguracionBic, ConfiguracionBicBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionBicBO FirstBy(Expression<Func<TConfiguracionBic, bool>> filter)
        {
            try
            {
                TConfiguracionBic entidad = base.FirstBy(filter);
                ConfiguracionBicBO objetoBO = Mapper.Map<TConfiguracionBic, ConfiguracionBicBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionBicBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBic entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionBicBO> listadoBO)
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

        public bool Update(ConfiguracionBicBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBic entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionBicBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBic entidad, ConfiguracionBicBO objetoBO)
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

        private TConfiguracionBic MapeoEntidad(ConfiguracionBicBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBic entidad = new TConfiguracionBic();
                entidad = Mapper.Map<ConfiguracionBicBO, TConfiguracionBic>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionBicBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionBic, bool>>> filters, Expression<Func<TConfiguracionBic, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionBic> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionBicBO> listadoBO = new List<ConfiguracionBicBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionBicBO objetoBO = Mapper.Map<TConfiguracionBic, ConfiguracionBicBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

    }
}

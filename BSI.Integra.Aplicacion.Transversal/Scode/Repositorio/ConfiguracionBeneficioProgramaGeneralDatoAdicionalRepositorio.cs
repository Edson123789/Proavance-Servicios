using System;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneralDatoAdicional, ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO>
    {
        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepositorio() : base()
        {
        }
        public ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralDatoAdicional, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneralDatoAdicional> listado = base.GetBy(filter);
            List<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO> listadoBO = new List<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralDatoAdicional, ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad = base.FirstById(id);
                ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO = new ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneralDatoAdicional, ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralDatoAdicional, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad = base.FirstBy(filter);
                ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralDatoAdicional, ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO> listadoBO)
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

        public bool Update(ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad, ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneralDatoAdicional MapeoEntidad(ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralDatoAdicional entidad = new TConfiguracionBeneficioProgramaGeneralDatoAdicional();
                entidad = Mapper.Map<ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO, TConfiguracionBeneficioProgramaGeneralDatoAdicional>(objetoBO,
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

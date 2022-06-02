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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConfiguracionBeneficioProgramaGeneralVersionRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneralVersion, TConfiguracionBeneficioProgramaGeneralVersionBO>
    {
        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneralVersionRepositorio() : base()
        {
        }
        public ConfiguracionBeneficioProgramaGeneralVersionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralVersionBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralVersion, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneralVersion> listado = base.GetBy(filter);
            List<TConfiguracionBeneficioProgramaGeneralVersionBO> listadoBO = new List<TConfiguracionBeneficioProgramaGeneralVersionBO>();
            foreach (var itemEntidad in listado)
            {
                TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralVersion, TConfiguracionBeneficioProgramaGeneralVersionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TConfiguracionBeneficioProgramaGeneralVersionBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralVersion entidad = base.FirstById(id);
                TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO = new TConfiguracionBeneficioProgramaGeneralVersionBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneralVersion, TConfiguracionBeneficioProgramaGeneralVersionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TConfiguracionBeneficioProgramaGeneralVersionBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralVersion, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralVersion entidad = base.FirstBy(filter);
                TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralVersion, TConfiguracionBeneficioProgramaGeneralVersionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TConfiguracionBeneficioProgramaGeneralVersionBO> listadoBO)
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

        public bool Update(TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralVersion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TConfiguracionBeneficioProgramaGeneralVersionBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneralVersion entidad, TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneralVersion MapeoEntidad(TConfiguracionBeneficioProgramaGeneralVersionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralVersion entidad = new TConfiguracionBeneficioProgramaGeneralVersion();
                entidad = Mapper.Map<TConfiguracionBeneficioProgramaGeneralVersionBO, TConfiguracionBeneficioProgramaGeneralVersion>(objetoBO,
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

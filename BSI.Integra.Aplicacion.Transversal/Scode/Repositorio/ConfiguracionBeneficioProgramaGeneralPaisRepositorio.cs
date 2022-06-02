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
    public class ConfiguracionBeneficioProgramaGeneralPaisRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneralPais, TConfiguracionBeneficioProgramaGeneralPaisBO>
    {
        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneralPaisRepositorio() : base()
        {
        }
        public ConfiguracionBeneficioProgramaGeneralPaisRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralPaisBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralPais, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneralPais> listado = base.GetBy(filter);
            List<TConfiguracionBeneficioProgramaGeneralPaisBO> listadoBO = new List<TConfiguracionBeneficioProgramaGeneralPaisBO>();
            foreach (var itemEntidad in listado)
            {
                TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralPais, TConfiguracionBeneficioProgramaGeneralPaisBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TConfiguracionBeneficioProgramaGeneralPaisBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralPais entidad = base.FirstById(id);
                TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO = new TConfiguracionBeneficioProgramaGeneralPaisBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneralPais, TConfiguracionBeneficioProgramaGeneralPaisBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TConfiguracionBeneficioProgramaGeneralPaisBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralPais, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralPais entidad = base.FirstBy(filter);
                TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralPais, TConfiguracionBeneficioProgramaGeneralPaisBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralPais entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TConfiguracionBeneficioProgramaGeneralPaisBO> listadoBO)
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

        public bool Update(TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralPais entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TConfiguracionBeneficioProgramaGeneralPaisBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneralPais entidad, TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneralPais MapeoEntidad(TConfiguracionBeneficioProgramaGeneralPaisBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralPais entidad = new TConfiguracionBeneficioProgramaGeneralPais();
                entidad = Mapper.Map<TConfiguracionBeneficioProgramaGeneralPaisBO, TConfiguracionBeneficioProgramaGeneralPais>(objetoBO,
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

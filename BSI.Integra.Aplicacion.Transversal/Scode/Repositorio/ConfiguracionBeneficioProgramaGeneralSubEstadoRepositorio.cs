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
    public class ConfiguracionBeneficioProgramaGeneralSubEstadoRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneralSubEstado, TConfiguracionBeneficioProgramaGeneralSubEstadoBO>
    {
        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneralSubEstadoRepositorio() : base()
        {
        }
        public ConfiguracionBeneficioProgramaGeneralSubEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstadoBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralSubEstado, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstado> listado = base.GetBy(filter);
            List<TConfiguracionBeneficioProgramaGeneralSubEstadoBO> listadoBO = new List<TConfiguracionBeneficioProgramaGeneralSubEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralSubEstado, TConfiguracionBeneficioProgramaGeneralSubEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TConfiguracionBeneficioProgramaGeneralSubEstadoBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralSubEstado entidad = base.FirstById(id);
                TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO = new TConfiguracionBeneficioProgramaGeneralSubEstadoBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneralSubEstado, TConfiguracionBeneficioProgramaGeneralSubEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TConfiguracionBeneficioProgramaGeneralSubEstadoBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralSubEstado, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralSubEstado entidad = base.FirstBy(filter);
                TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralSubEstado, TConfiguracionBeneficioProgramaGeneralSubEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralSubEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstadoBO> listadoBO)
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

        public bool Update(TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralSubEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TConfiguracionBeneficioProgramaGeneralSubEstadoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneralSubEstado entidad, TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneralSubEstado MapeoEntidad(TConfiguracionBeneficioProgramaGeneralSubEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralSubEstado entidad = new TConfiguracionBeneficioProgramaGeneralSubEstado();
                entidad = Mapper.Map<TConfiguracionBeneficioProgramaGeneralSubEstadoBO, TConfiguracionBeneficioProgramaGeneralSubEstado>(objetoBO,
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

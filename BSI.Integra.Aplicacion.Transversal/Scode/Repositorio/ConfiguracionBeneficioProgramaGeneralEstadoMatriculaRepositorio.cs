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
    public class ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO>
    {
        #region Metodos Base
        public ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepositorio() : base()
        {
        }
        public ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> listado = base.GetBy(filter);
            List<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO> listadoBO = new List<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO>();
            foreach (var itemEntidad in listado)
            {
                TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad = base.FirstById(id);
                TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO = new TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad = base.FirstBy(filter);
                TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO> listadoBO)
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

        public bool Update(TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad, TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneralEstadoMatricula MapeoEntidad(TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad = new TConfiguracionBeneficioProgramaGeneralEstadoMatricula();
                entidad = Mapper.Map<TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO, TConfiguracionBeneficioProgramaGeneralEstadoMatricula>(objetoBO,
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

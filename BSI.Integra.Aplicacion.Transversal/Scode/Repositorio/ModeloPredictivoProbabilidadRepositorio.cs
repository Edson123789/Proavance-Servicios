using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: ModeloPredictivoProbabilidad
    /// Autor: Gian Miranda
    /// Fecha: 07/05/2021
    /// <summary>
    /// Repositorio para la gestion de los modelos predictivos basado en probabilidad
    /// </summary>
    public class ModeloPredictivoProbabilidadRepositorio : BaseRepository<TModeloPredictivoProbabilidad, ModeloPredictivoProbabilidadBO>
    {
        #region Metodos Base
        public ModeloPredictivoProbabilidadRepositorio() : base()
        {
        }
        public ModeloPredictivoProbabilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoProbabilidadBO> GetBy(Expression<Func<TModeloPredictivoProbabilidad, bool>> filter)
        {
            IEnumerable<TModeloPredictivoProbabilidad> listado = base.GetBy(filter);
            List<ModeloPredictivoProbabilidadBO> listadoBO = new List<ModeloPredictivoProbabilidadBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoProbabilidadBO objetoBO = Mapper.Map<TModeloPredictivoProbabilidad, ModeloPredictivoProbabilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoProbabilidadBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoProbabilidad entidad = base.FirstById(id);
                ModeloPredictivoProbabilidadBO objetoBO = new ModeloPredictivoProbabilidadBO();
                Mapper.Map<TModeloPredictivoProbabilidad, ModeloPredictivoProbabilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoProbabilidadBO FirstBy(Expression<Func<TModeloPredictivoProbabilidad, bool>> filter)
        {
            try
            {
                TModeloPredictivoProbabilidad entidad = base.FirstBy(filter);
                ModeloPredictivoProbabilidadBO objetoBO = Mapper.Map<TModeloPredictivoProbabilidad, ModeloPredictivoProbabilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoProbabilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoProbabilidadBO> listadoBO)
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

        public bool Update(ModeloPredictivoProbabilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoProbabilidadBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoProbabilidad entidad, ModeloPredictivoProbabilidadBO objetoBO)
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

        private TModeloPredictivoProbabilidad MapeoEntidad(ModeloPredictivoProbabilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoProbabilidad entidad = new TModeloPredictivoProbabilidad();
                entidad = Mapper.Map<ModeloPredictivoProbabilidadBO, TModeloPredictivoProbabilidad>(objetoBO,
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

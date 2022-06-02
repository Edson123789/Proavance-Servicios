using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ModeloPredictivoCiudadRepositorio : BaseRepository<TModeloPredictivoCiudad, ModeloPredictivoCiudadBO>
    {
        #region Metodos Base
        public ModeloPredictivoCiudadRepositorio() : base()
        {
        }
        public ModeloPredictivoCiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoCiudadBO> GetBy(Expression<Func<TModeloPredictivoCiudad, bool>> filter)
        {
            IEnumerable<TModeloPredictivoCiudad> listado = base.GetBy(filter);
            List<ModeloPredictivoCiudadBO> listadoBO = new List<ModeloPredictivoCiudadBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoCiudadBO objetoBO = Mapper.Map<TModeloPredictivoCiudad, ModeloPredictivoCiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoCiudadBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoCiudad entidad = base.FirstById(id);
                ModeloPredictivoCiudadBO objetoBO = new ModeloPredictivoCiudadBO();
                Mapper.Map<TModeloPredictivoCiudad, ModeloPredictivoCiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoCiudadBO FirstBy(Expression<Func<TModeloPredictivoCiudad, bool>> filter)
        {
            try
            {
                TModeloPredictivoCiudad entidad = base.FirstBy(filter);
                ModeloPredictivoCiudadBO objetoBO = Mapper.Map<TModeloPredictivoCiudad, ModeloPredictivoCiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoCiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoCiudadBO> listadoBO)
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

        public bool Update(ModeloPredictivoCiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoCiudadBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoCiudad entidad, ModeloPredictivoCiudadBO objetoBO)
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

        private TModeloPredictivoCiudad MapeoEntidad(ModeloPredictivoCiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoCiudad entidad = new TModeloPredictivoCiudad();
                entidad = Mapper.Map<ModeloPredictivoCiudadBO, TModeloPredictivoCiudad>(objetoBO,
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

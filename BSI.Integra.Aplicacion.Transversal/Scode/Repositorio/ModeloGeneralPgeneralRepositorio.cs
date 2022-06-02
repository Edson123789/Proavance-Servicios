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
    public class ModeloGeneralPgeneralRepositorio : BaseRepository<TModeloGeneralPgeneral, ModeloGeneralPgeneralBO>
    {
        #region Metodos Base
        public ModeloGeneralPgeneralRepositorio() : base()
        {
        }
        public ModeloGeneralPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloGeneralPgeneralBO> GetBy(Expression<Func<TModeloGeneralPgeneral, bool>> filter)
        {
            IEnumerable<TModeloGeneralPgeneral> listado = base.GetBy(filter);
            List<ModeloGeneralPgeneralBO> listadoBO = new List<ModeloGeneralPgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloGeneralPgeneralBO objetoBO = Mapper.Map<TModeloGeneralPgeneral, ModeloGeneralPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloGeneralPgeneralBO FirstById(int id)
        {
            try
            {
                TModeloGeneralPgeneral entidad = base.FirstById(id);
                ModeloGeneralPgeneralBO objetoBO = new ModeloGeneralPgeneralBO();
                Mapper.Map<TModeloGeneralPgeneral, ModeloGeneralPgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloGeneralPgeneralBO FirstBy(Expression<Func<TModeloGeneralPgeneral, bool>> filter)
        {
            try
            {
                TModeloGeneralPgeneral entidad = base.FirstBy(filter);
                ModeloGeneralPgeneralBO objetoBO = Mapper.Map<TModeloGeneralPgeneral, ModeloGeneralPgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloGeneralPgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloGeneralPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloGeneralPgeneralBO> listadoBO)
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

        public bool Update(ModeloGeneralPgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloGeneralPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloGeneralPgeneralBO> listadoBO)
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
        private void AsignacionId(TModeloGeneralPgeneral entidad, ModeloGeneralPgeneralBO objetoBO)
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

        private TModeloGeneralPgeneral MapeoEntidad(ModeloGeneralPgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralPgeneral entidad = new TModeloGeneralPgeneral();
                entidad = Mapper.Map<ModeloGeneralPgeneralBO, TModeloGeneralPgeneral>(objetoBO,
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

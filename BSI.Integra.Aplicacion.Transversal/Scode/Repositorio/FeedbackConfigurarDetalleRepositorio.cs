using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FeedbackConfigurarDetalleRepositorio : BaseRepository<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleBO>
    {
        #region Metodos Base
        public FeedbackConfigurarDetalleRepositorio() : base()
        {
        }
        public FeedbackConfigurarDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackConfigurarDetalleBO> GetBy(Expression<Func<TFeedbackConfigurarDetalle, bool>> filter)
        {
            IEnumerable<TFeedbackConfigurarDetalle> listado = base.GetBy(filter);
            List<FeedbackConfigurarDetalleBO> listadoBO = new List<FeedbackConfigurarDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarDetalleBO objetoBO = Mapper.Map<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackConfigurarDetalleBO> GetAll()
        {
            IEnumerable<TFeedbackConfigurarDetalle> listado = base.GetAll();
            List<FeedbackConfigurarDetalleBO> listadoBO = new List<FeedbackConfigurarDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackConfigurarDetalleBO objetoBO = Mapper.Map<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackConfigurarDetalleBO FirstById(int id)
        {
            try
            {
                TFeedbackConfigurarDetalle entidad = base.FirstById(id);
                FeedbackConfigurarDetalleBO objetoBO = new FeedbackConfigurarDetalleBO();
                Mapper.Map<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackConfigurarDetalleBO FirstBy(Expression<Func<TFeedbackConfigurarDetalle, bool>> filter)
        {
            try
            {
                TFeedbackConfigurarDetalle entidad = base.FirstBy(filter);
                FeedbackConfigurarDetalleBO objetoBO = Mapper.Map<TFeedbackConfigurarDetalle, FeedbackConfigurarDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackConfigurarDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackConfigurarDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackConfigurarDetalleBO> listadoBO)
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

        public bool Update(FeedbackConfigurarDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackConfigurarDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackConfigurarDetalleBO> listadoBO)
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
        private void AsignacionId(TFeedbackConfigurarDetalle entidad, FeedbackConfigurarDetalleBO objetoBO)
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

        private TFeedbackConfigurarDetalle MapeoEntidad(FeedbackConfigurarDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackConfigurarDetalle entidad = new TFeedbackConfigurarDetalle();
                entidad = Mapper.Map<FeedbackConfigurarDetalleBO, TFeedbackConfigurarDetalle>(objetoBO,
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

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
    public class FeedbackGrupoPreguntaProgramaEspecificoRepositorio : BaseRepository<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoBO>
    {
        #region Metodos Base
        public FeedbackGrupoPreguntaProgramaEspecificoRepositorio() : base()
        {
        }
        public FeedbackGrupoPreguntaProgramaEspecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackGrupoPreguntaProgramaEspecificoBO> GetBy(Expression<Func<TFeedbackGrupoPreguntaProgramaEspecifico, bool>> filter)
        {
            IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> listado = base.GetBy(filter);
            List<FeedbackGrupoPreguntaProgramaEspecificoBO> listadoBO = new List<FeedbackGrupoPreguntaProgramaEspecificoBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackGrupoPreguntaProgramaEspecificoBO> GetAll()
        {
            IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> listado = base.GetAll();
            List<FeedbackGrupoPreguntaProgramaEspecificoBO> listadoBO = new List<FeedbackGrupoPreguntaProgramaEspecificoBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackGrupoPreguntaProgramaEspecificoBO FirstById(int id)
        {
            try
            {
                TFeedbackGrupoPreguntaProgramaEspecifico entidad = base.FirstById(id);
                FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO = new FeedbackGrupoPreguntaProgramaEspecificoBO();
                Mapper.Map<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackGrupoPreguntaProgramaEspecificoBO FirstBy(Expression<Func<TFeedbackGrupoPreguntaProgramaEspecifico, bool>> filter)
        {
            try
            {
                TFeedbackGrupoPreguntaProgramaEspecifico entidad = base.FirstBy(filter);
                FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackGrupoPreguntaProgramaEspecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackGrupoPreguntaProgramaEspecificoBO> listadoBO)
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

        public bool Update(FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackGrupoPreguntaProgramaEspecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackGrupoPreguntaProgramaEspecificoBO> listadoBO)
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
        private void AsignacionId(TFeedbackGrupoPreguntaProgramaEspecifico entidad, FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO)
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

        private TFeedbackGrupoPreguntaProgramaEspecifico MapeoEntidad(FeedbackGrupoPreguntaProgramaEspecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackGrupoPreguntaProgramaEspecifico entidad = new TFeedbackGrupoPreguntaProgramaEspecifico();
                entidad = Mapper.Map<FeedbackGrupoPreguntaProgramaEspecificoBO, TFeedbackGrupoPreguntaProgramaEspecifico>(objetoBO,
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

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
    public class FeedbackGrupoPreguntaProgramaGeneralRepositorio : BaseRepository<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneralBO>
    {
        #region Metodos Base
        public FeedbackGrupoPreguntaProgramaGeneralRepositorio() : base()
        {
        }
        public FeedbackGrupoPreguntaProgramaGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FeedbackGrupoPreguntaProgramaGeneralBO> GetBy(Expression<Func<TFeedbackGrupoPreguntaProgramaGeneral, bool>> filter)
        {
            IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> listado = base.GetBy(filter);
            List<FeedbackGrupoPreguntaProgramaGeneralBO> listadoBO = new List<FeedbackGrupoPreguntaProgramaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackGrupoPreguntaProgramaGeneralBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public IEnumerable<FeedbackGrupoPreguntaProgramaGeneralBO> GetAll()
        {
            IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> listado = base.GetAll();
            List<FeedbackGrupoPreguntaProgramaGeneralBO> listadoBO = new List<FeedbackGrupoPreguntaProgramaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                FeedbackGrupoPreguntaProgramaGeneralBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public FeedbackGrupoPreguntaProgramaGeneralBO FirstById(int id)
        {
            try
            {
                TFeedbackGrupoPreguntaProgramaGeneral entidad = base.FirstById(id);
                FeedbackGrupoPreguntaProgramaGeneralBO objetoBO = new FeedbackGrupoPreguntaProgramaGeneralBO();
                Mapper.Map<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FeedbackGrupoPreguntaProgramaGeneralBO FirstBy(Expression<Func<TFeedbackGrupoPreguntaProgramaGeneral, bool>> filter)
        {
            try
            {
                TFeedbackGrupoPreguntaProgramaGeneral entidad = base.FirstBy(filter);
                FeedbackGrupoPreguntaProgramaGeneralBO objetoBO = Mapper.Map<TFeedbackGrupoPreguntaProgramaGeneral, FeedbackGrupoPreguntaProgramaGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FeedbackGrupoPreguntaProgramaGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFeedbackGrupoPreguntaProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FeedbackGrupoPreguntaProgramaGeneralBO> listadoBO)
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

        public bool Update(FeedbackGrupoPreguntaProgramaGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFeedbackGrupoPreguntaProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FeedbackGrupoPreguntaProgramaGeneralBO> listadoBO)
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
        private void AsignacionId(TFeedbackGrupoPreguntaProgramaGeneral entidad, FeedbackGrupoPreguntaProgramaGeneralBO objetoBO)
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

        private TFeedbackGrupoPreguntaProgramaGeneral MapeoEntidad(FeedbackGrupoPreguntaProgramaGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFeedbackGrupoPreguntaProgramaGeneral entidad = new TFeedbackGrupoPreguntaProgramaGeneral();
                entidad = Mapper.Map<FeedbackGrupoPreguntaProgramaGeneralBO, TFeedbackGrupoPreguntaProgramaGeneral>(objetoBO,
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

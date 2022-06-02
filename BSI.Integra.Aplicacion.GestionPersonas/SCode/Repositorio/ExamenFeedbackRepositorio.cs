using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: ExamenFeedbackRepositorio
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Repositorio para la tabla T_ExamenFeedback
    /// </summary>
    public class ExamenFeedbackRepositorio : BaseRepository<TExamenFeedback, ExamenFeedbackBO>
    {
        #region Metodos Base
        public ExamenFeedbackRepositorio() : base()
        {
        }
        public ExamenFeedbackRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExamenFeedbackBO> GetBy(Expression<Func<TExamenFeedback, bool>> filter)
        {
            IEnumerable<TExamenFeedback> listado = base.GetBy(filter);
            List<ExamenFeedbackBO> listadoBO = new List<ExamenFeedbackBO>();
            foreach (var itemEntidad in listado)
            {
                ExamenFeedbackBO objetoBO = Mapper.Map<TExamenFeedback, ExamenFeedbackBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExamenFeedbackBO FirstById(int id)
        {
            try
            {
                TExamenFeedback entidad = base.FirstById(id);
                ExamenFeedbackBO objetoBO = new ExamenFeedbackBO();
                Mapper.Map<TExamenFeedback, ExamenFeedbackBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExamenFeedbackBO FirstBy(Expression<Func<TExamenFeedback, bool>> filter)
        {
            try
            {
                TExamenFeedback entidad = base.FirstBy(filter);
                ExamenFeedbackBO objetoBO = Mapper.Map<TExamenFeedback, ExamenFeedbackBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExamenFeedbackBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExamenFeedback entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExamenFeedbackBO> listadoBO)
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

        public bool Update(ExamenFeedbackBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExamenFeedback entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExamenFeedbackBO> listadoBO)
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
        private void AsignacionId(TExamenFeedback entidad, ExamenFeedbackBO objetoBO)
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

        private TExamenFeedback MapeoEntidad(ExamenFeedbackBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExamenFeedback entidad = new TExamenFeedback();
                entidad = Mapper.Map<ExamenFeedbackBO, TExamenFeedback>(objetoBO,
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

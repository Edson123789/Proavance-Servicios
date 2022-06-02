using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class AutoevaluacionPorCursoMoodleRepositorio : BaseRepository<TAutoevaluacionPorCursoMoodle, AutoevaluacionPorCursoMoodleBO>
    {
        #region Metodos Base
        public AutoevaluacionPorCursoMoodleRepositorio() : base()
        {
        }
        public AutoevaluacionPorCursoMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AutoevaluacionPorCursoMoodleBO> GetBy(Expression<Func<TAutoevaluacionPorCursoMoodle, bool>> filter)
        {
            IEnumerable<TAutoevaluacionPorCursoMoodle> listado = base.GetBy(filter);
            List<AutoevaluacionPorCursoMoodleBO> listadoBO = new List<AutoevaluacionPorCursoMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                AutoevaluacionPorCursoMoodleBO objetoBO = Mapper.Map<TAutoevaluacionPorCursoMoodle, AutoevaluacionPorCursoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AutoevaluacionPorCursoMoodleBO FirstById(int id)
        {
            try
            {
                TAutoevaluacionPorCursoMoodle entidad = base.FirstById(id);
                AutoevaluacionPorCursoMoodleBO objetoBO = new AutoevaluacionPorCursoMoodleBO();
                Mapper.Map<TAutoevaluacionPorCursoMoodle, AutoevaluacionPorCursoMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AutoevaluacionPorCursoMoodleBO FirstBy(Expression<Func<TAutoevaluacionPorCursoMoodle, bool>> filter)
        {
            try
            {
                TAutoevaluacionPorCursoMoodle entidad = base.FirstBy(filter);
                AutoevaluacionPorCursoMoodleBO objetoBO = Mapper.Map<TAutoevaluacionPorCursoMoodle, AutoevaluacionPorCursoMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AutoevaluacionPorCursoMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAutoevaluacionPorCursoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AutoevaluacionPorCursoMoodleBO> listadoBO)
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

        public bool Update(AutoevaluacionPorCursoMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAutoevaluacionPorCursoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AutoevaluacionPorCursoMoodleBO> listadoBO)
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
        private void AsignacionId(TAutoevaluacionPorCursoMoodle entidad, AutoevaluacionPorCursoMoodleBO objetoBO)
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

        private TAutoevaluacionPorCursoMoodle MapeoEntidad(AutoevaluacionPorCursoMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAutoevaluacionPorCursoMoodle entidad = new TAutoevaluacionPorCursoMoodle();
                entidad = Mapper.Map<AutoevaluacionPorCursoMoodleBO, TAutoevaluacionPorCursoMoodle>(objetoBO,
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

        /// <summary>
        /// Obtener autoevaluaciones por curso
        /// </summary>
        /// <param name="idCursoMoodle"></param>
        /// <returns></returns>
        public List<AutoevaluacionPorCursoMoodleBO> ObtenerPorCurso(int idCursoMoodle) {
            try
            {
                return this.GetBy(x => x.IdCurso == idCursoMoodle).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

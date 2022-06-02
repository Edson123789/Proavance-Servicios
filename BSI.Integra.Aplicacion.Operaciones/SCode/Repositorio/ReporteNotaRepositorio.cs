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
    public class ReporteNotaRepositorio : BaseRepository<TReporteNota, ReporteNotaBO>
    {
        #region Metodos Base
        public ReporteNotaRepositorio() : base()
        {
        }
        public ReporteNotaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ReporteNotaBO> GetBy(Expression<Func<TReporteNota, bool>> filter)
        {
            IEnumerable<TReporteNota> listado = base.GetBy(filter);
            List<ReporteNotaBO> listadoBO = new List<ReporteNotaBO>();
            foreach (var itemEntidad in listado)
            {
                ReporteNotaBO objetoBO = Mapper.Map<TReporteNota, ReporteNotaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ReporteNotaBO FirstById(int id)
        {
            try
            {
                TReporteNota entidad = base.FirstById(id);
                ReporteNotaBO objetoBO = new ReporteNotaBO();
                Mapper.Map<TReporteNota, ReporteNotaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ReporteNotaBO FirstBy(Expression<Func<TReporteNota, bool>> filter)
        {
            try
            {
                TReporteNota entidad = base.FirstBy(filter);
                ReporteNotaBO objetoBO = Mapper.Map<TReporteNota, ReporteNotaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ReporteNotaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TReporteNota entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ReporteNotaBO> listadoBO)
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

        public bool Update(ReporteNotaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TReporteNota entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ReporteNotaBO> listadoBO)
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
        private void AsignacionId(TReporteNota entidad, ReporteNotaBO objetoBO)
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

        private TReporteNota MapeoEntidad(ReporteNotaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TReporteNota entidad = new TReporteNota();
                entidad = Mapper.Map<ReporteNotaBO, TReporteNota>(objetoBO,
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
        /// Obtiene un listado de notas por usuario moodle y cursos
        /// </summary>
        /// <param name="idUsuarioMoodle"></param>
        /// <param name="idsCursos"></param>
        /// <returns></returns>
        public List<ReporteNotaBO> ObtenerNotasPorUsuarioMoodleCurso(int idUsuarioMoodle, List<int> idsCursos)
        {
            try
            {
                return this.GetBy(x => x.IdAlumnoMoodle.Value == idUsuarioMoodle && idsCursos.Contains(x.IdCursoMoodle.Value) && (x.NombreAutoevaluacion.Contains("utoevalua") || x.NombreAutoevaluacion.Contains("royect"))).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

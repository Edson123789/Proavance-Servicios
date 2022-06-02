using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class MatriculaAlumnoMoodleRepositorio : BaseRepository<TMatriculaAlumnoMoodle, MatriculaAlumnoMoodleBO>
    {
        #region Metodos Base
        public MatriculaAlumnoMoodleRepositorio() : base()
        {
        }
        public MatriculaAlumnoMoodleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MatriculaAlumnoMoodleBO> GetBy(Expression<Func<TMatriculaAlumnoMoodle, bool>> filter)
        {
            IEnumerable<TMatriculaAlumnoMoodle> listado = base.GetBy(filter);
            List<MatriculaAlumnoMoodleBO> listadoBO = new List<MatriculaAlumnoMoodleBO>();
            foreach (var itemEntidad in listado)
            {
                MatriculaAlumnoMoodleBO objetoBO = Mapper.Map<TMatriculaAlumnoMoodle, MatriculaAlumnoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MatriculaAlumnoMoodleBO FirstById(int id)
        {
            try
            {
                TMatriculaAlumnoMoodle entidad = base.FirstById(id);
                MatriculaAlumnoMoodleBO objetoBO = new MatriculaAlumnoMoodleBO();
                Mapper.Map<TMatriculaAlumnoMoodle, MatriculaAlumnoMoodleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MatriculaAlumnoMoodleBO FirstBy(Expression<Func<TMatriculaAlumnoMoodle, bool>> filter)
        {
            try
            {
                TMatriculaAlumnoMoodle entidad = base.FirstBy(filter);
                MatriculaAlumnoMoodleBO objetoBO = Mapper.Map<TMatriculaAlumnoMoodle, MatriculaAlumnoMoodleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MatriculaAlumnoMoodleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMatriculaAlumnoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MatriculaAlumnoMoodleBO> listadoBO)
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

        public bool Update(MatriculaAlumnoMoodleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMatriculaAlumnoMoodle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MatriculaAlumnoMoodleBO> listadoBO)
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
        private void AsignacionId(TMatriculaAlumnoMoodle entidad, MatriculaAlumnoMoodleBO objetoBO)
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

        private TMatriculaAlumnoMoodle MapeoEntidad(MatriculaAlumnoMoodleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMatriculaAlumnoMoodle entidad = new TMatriculaAlumnoMoodle();
                entidad = Mapper.Map<MatriculaAlumnoMoodleBO, TMatriculaAlumnoMoodle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MatriculaAlumnoMoodleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMatriculaAlumnoMoodle, bool>>> filters, Expression<Func<TMatriculaAlumnoMoodle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMatriculaAlumnoMoodle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MatriculaAlumnoMoodleBO> listadoBO = new List<MatriculaAlumnoMoodleBO>();

            foreach (var itemEntidad in listado)
            {
                MatriculaAlumnoMoodleBO objetoBO = Mapper.Map<TMatriculaAlumnoMoodle, MatriculaAlumnoMoodleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Permite obtener el curso o lista de cursos de Moodle por ProgramaGeneral 
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<MatriculaAlumnoMoodleDTO> ObtenerTodoDatoTestimonioPrograma(int idPGeneral)
        {
            try
            {
                List<MatriculaAlumnoMoodleDTO> datoProgramaTestimonio = new List<MatriculaAlumnoMoodleDTO>();
                var datoCursoMoodle = _dapper.QuerySPDapper("ope.SP_ListarProgramasMoodle", new { idPGeneral });
                datoProgramaTestimonio = JsonConvert.DeserializeObject<List<MatriculaAlumnoMoodleDTO>>(datoCursoMoodle);
                
                return datoProgramaTestimonio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

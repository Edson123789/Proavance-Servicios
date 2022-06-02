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
    public class RaCursoTrabajoAlumnoRepositorio : BaseRepository<TRaCursoTrabajoAlumno, RaCursoTrabajoAlumnoBO>
    {
        #region Metodos Base
        public RaCursoTrabajoAlumnoRepositorio() : base()
        {
        }
        public RaCursoTrabajoAlumnoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCursoTrabajoAlumnoBO> GetBy(Expression<Func<TRaCursoTrabajoAlumno, bool>> filter)
        {
            IEnumerable<TRaCursoTrabajoAlumno> listado = base.GetBy(filter);
            List<RaCursoTrabajoAlumnoBO> listadoBO = new List<RaCursoTrabajoAlumnoBO>();
            foreach (var itemEntidad in listado)
            {
                RaCursoTrabajoAlumnoBO objetoBO = Mapper.Map<TRaCursoTrabajoAlumno, RaCursoTrabajoAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCursoTrabajoAlumnoBO FirstById(int id)
        {
            try
            {
                TRaCursoTrabajoAlumno entidad = base.FirstById(id);
                RaCursoTrabajoAlumnoBO objetoBO = new RaCursoTrabajoAlumnoBO();
                Mapper.Map<TRaCursoTrabajoAlumno, RaCursoTrabajoAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCursoTrabajoAlumnoBO FirstBy(Expression<Func<TRaCursoTrabajoAlumno, bool>> filter)
        {
            try
            {
                TRaCursoTrabajoAlumno entidad = base.FirstBy(filter);
                RaCursoTrabajoAlumnoBO objetoBO = Mapper.Map<TRaCursoTrabajoAlumno, RaCursoTrabajoAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCursoTrabajoAlumnoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCursoTrabajoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCursoTrabajoAlumnoBO> listadoBO)
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

        public bool Update(RaCursoTrabajoAlumnoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCursoTrabajoAlumno entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCursoTrabajoAlumnoBO> listadoBO)
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
        private void AsignacionId(TRaCursoTrabajoAlumno entidad, RaCursoTrabajoAlumnoBO objetoBO)
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

        private TRaCursoTrabajoAlumno MapeoEntidad(RaCursoTrabajoAlumnoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCursoTrabajoAlumno entidad = new TRaCursoTrabajoAlumno();
                entidad = Mapper.Map<RaCursoTrabajoAlumnoBO, TRaCursoTrabajoAlumno>(objetoBO,
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
        /// Obtiene el listado minimo de curso trabajo alumno por curso
        /// </summary>
        /// <param name="idRaCurso"></param>
        /// <returns></returns>
        public List<RaListadoMinimoCursoTrabajoAlumnoDTO> ObtenerListadoMinimoPorCurso(int idRaCurso)
        {
            try
            {
                List<RaListadoMinimoCursoTrabajoAlumnoDTO> cursoTrabajoAlumno = new List<RaListadoMinimoCursoTrabajoAlumnoDTO>();
                var query = "SELECT Id, Nombre, TipoEntrega, Estado, FechaEntrega FROM ope.V_ObtenerListadoMinimoCursoTrabajoAlumno WHERE IdRaCurso = @idRaCurso";
                var cursoTrabajoAlumnoDB = _dapper.QueryDapper(query, new { idRaCurso });
                if (!string.IsNullOrEmpty(cursoTrabajoAlumnoDB) && !cursoTrabajoAlumnoDB.Contains("[]"))
                {
                    cursoTrabajoAlumno = JsonConvert.DeserializeObject<List<RaListadoMinimoCursoTrabajoAlumnoDTO>>(cursoTrabajoAlumnoDB);
                }
                return cursoTrabajoAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

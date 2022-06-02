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
    public class RaCursoObservacionRepositorio : BaseRepository<TRaCursoObservacion, RaCursoObservacionBO>
    {
        #region Metodos Base
        public RaCursoObservacionRepositorio() : base()
        {
        }
        public RaCursoObservacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaCursoObservacionBO> GetBy(Expression<Func<TRaCursoObservacion, bool>> filter)
        {
            IEnumerable<TRaCursoObservacion> listado = base.GetBy(filter);
            List<RaCursoObservacionBO> listadoBO = new List<RaCursoObservacionBO>();
            foreach (var itemEntidad in listado)
            {
                RaCursoObservacionBO objetoBO = Mapper.Map<TRaCursoObservacion, RaCursoObservacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaCursoObservacionBO FirstById(int id)
        {
            try
            {
                TRaCursoObservacion entidad = base.FirstById(id);
                RaCursoObservacionBO objetoBO = new RaCursoObservacionBO();
                Mapper.Map<TRaCursoObservacion, RaCursoObservacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaCursoObservacionBO FirstBy(Expression<Func<TRaCursoObservacion, bool>> filter)
        {
            try
            {
                TRaCursoObservacion entidad = base.FirstBy(filter);
                RaCursoObservacionBO objetoBO = Mapper.Map<TRaCursoObservacion, RaCursoObservacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaCursoObservacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaCursoObservacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaCursoObservacionBO> listadoBO)
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

        public bool Update(RaCursoObservacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaCursoObservacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaCursoObservacionBO> listadoBO)
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
        private void AsignacionId(TRaCursoObservacion entidad, RaCursoObservacionBO objetoBO)
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

        private TRaCursoObservacion MapeoEntidad(RaCursoObservacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaCursoObservacion entidad = new TRaCursoObservacion();
                entidad = Mapper.Map<RaCursoObservacionBO, TRaCursoObservacion>(objetoBO,
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
        /// Obtiene el listado minimo de curso observacion por curso
        /// </summary>
        /// <param name="idRaCurso"></param>
        /// <returns></returns>
        public List<RaListadoMinimoCursoObservacionDTO> ObtenerListadoMinimoPorCurso(int idRaCurso)
        {
            try
            {
                List<RaListadoMinimoCursoObservacionDTO> cursoObservacion = new List<RaListadoMinimoCursoObservacionDTO>();
                var query = "SELECT Id, Nombre, Observacion, UsuarioNombre, UsuarioApellidos, FechaCreacion FROM ope.V_ObtenerListadoMinimoCursoObservacion WHERE IdRaCurso = @idRaCurso";
                var cursoObservacionDB = _dapper.QueryDapper(query, new { idRaCurso });
                if (!string.IsNullOrEmpty(cursoObservacionDB) && !cursoObservacionDB.Contains("[]"))
                {
                    cursoObservacion = JsonConvert.DeserializeObject<List<RaListadoMinimoCursoObservacionDTO>>(cursoObservacionDB);
                }
                return cursoObservacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

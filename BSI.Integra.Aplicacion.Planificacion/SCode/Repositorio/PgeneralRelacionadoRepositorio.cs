using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PgeneralRelacionadoRepositorio : BaseRepository<TPgeneralRelacionado, PgeneralRelacionadoBO>
    {
        #region Metodos Base
        public PgeneralRelacionadoRepositorio() : base()
        {
        }
        public PgeneralRelacionadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PgeneralRelacionadoBO> GetBy(Expression<Func<TPgeneralRelacionado, bool>> filter)
        {
            IEnumerable<TPgeneralRelacionado> listado = base.GetBy(filter);
            List<PgeneralRelacionadoBO> listadoBO = new List<PgeneralRelacionadoBO>();
            foreach (var itemEntidad in listado)
            {
                PgeneralRelacionadoBO objetoBO = Mapper.Map<TPgeneralRelacionado, PgeneralRelacionadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PgeneralRelacionadoBO FirstById(int id)
        {
            try
            {
                TPgeneralRelacionado entidad = base.FirstById(id);
                PgeneralRelacionadoBO objetoBO = new PgeneralRelacionadoBO();
                Mapper.Map<TPgeneralRelacionado, PgeneralRelacionadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PgeneralRelacionadoBO FirstBy(Expression<Func<TPgeneralRelacionado, bool>> filter)
        {
            try
            {
                TPgeneralRelacionado entidad = base.FirstBy(filter);
                PgeneralRelacionadoBO objetoBO = Mapper.Map<TPgeneralRelacionado, PgeneralRelacionadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PgeneralRelacionadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralRelacionado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PgeneralRelacionadoBO> listadoBO)
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

        public bool Update(PgeneralRelacionadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralRelacionado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PgeneralRelacionadoBO> listadoBO)
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
        private void AsignacionId(TPgeneralRelacionado entidad, PgeneralRelacionadoBO objetoBO)
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

        private TPgeneralRelacionado MapeoEntidad(PgeneralRelacionadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralRelacionado entidad = new TPgeneralRelacionado();
                entidad = Mapper.Map<PgeneralRelacionadoBO, TPgeneralRelacionado>(objetoBO,
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
        /// Obtiene la  lista de cursos relacionados a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<CursoRelacionadoProgramaDTO> ObtenerCursosRelacionadosPorPrograma(int idPGeneral)
        {
           
            try
            {
                List<CursoRelacionadoProgramaDTO> cursos = new List<CursoRelacionadoProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdRelacionado,Nombre FROM pla.V_TPGeneralRelacionado WHERE " +
                    "EstadoPrograma = 1 and EstadoRelacionados = 1 and IdPGeneralPadre = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    cursos = JsonConvert.DeserializeObject<List<CursoRelacionadoProgramaDTO>>(respuestaDapper);
                }

                return cursos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
           
        }
        /// <summary>
        /// Obtiene la lista de cursos no relacionados para un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<PGeneralFiltroDTO> ObtenerCursosNoRelacionadosPorPrograma(int idPGeneral)
        {
            try
            {
                var cursosRelacionados = ObtenerCursosRelacionadosPorPrograma(idPGeneral);
                
                List<PGeneralFiltroDTO> programasGenerales = new List<PGeneralFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralFiltroDTO>>(pgeneralDB);
                    programasGenerales.RemoveAll(x => cursosRelacionados.Any(y => y.IdRelacionado == x.Id));
                    programasGenerales.RemoveAll(x => x.Id == idPGeneral);
                }

                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los programas Relacionados Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<CursoRelacionadoProgramaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPgeneral == idPGeneral && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Planificacion/PGeneralASubPGeneral
    /// Autor: Fischer Valdez - Ansoli Espinoza - Joao - Johan Cayo - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_PGeneralASubPGeneral
    /// </summary>
    public class PgeneralAsubPgeneralRepositorio : BaseRepository<TPgeneralAsubPgeneral, PGeneralASubPGeneralBO>
    {
        #region Metodos Base
        public PgeneralAsubPgeneralRepositorio() : base()
        {
        }
        public PgeneralAsubPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PGeneralASubPGeneralBO> GetBy(Expression<Func<TPgeneralAsubPgeneral, bool>> filter)
        {
            IEnumerable<TPgeneralAsubPgeneral> listado = base.GetBy(filter);
            List<PGeneralASubPGeneralBO> listadoBO = new List<PGeneralASubPGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                PGeneralASubPGeneralBO objetoBO = Mapper.Map<TPgeneralAsubPgeneral, PGeneralASubPGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PGeneralASubPGeneralBO FirstById(int id)
        {
            try
            {
                TPgeneralAsubPgeneral entidad = base.FirstById(id);
                PGeneralASubPGeneralBO objetoBO = new PGeneralASubPGeneralBO();
                Mapper.Map<TPgeneralAsubPgeneral, PGeneralASubPGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PGeneralASubPGeneralBO FirstBy(Expression<Func<TPgeneralAsubPgeneral, bool>> filter)
        {
            try
            {
                TPgeneralAsubPgeneral entidad = base.FirstBy(filter);
                PGeneralASubPGeneralBO objetoBO = Mapper.Map<TPgeneralAsubPgeneral, PGeneralASubPGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PGeneralASubPGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralAsubPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PGeneralASubPGeneralBO> listadoBO)
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

        public bool Update(PGeneralASubPGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralAsubPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PGeneralASubPGeneralBO> listadoBO)
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
        private void AsignacionId(TPgeneralAsubPgeneral entidad, PGeneralASubPGeneralBO objetoBO)
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

        private TPgeneralAsubPgeneral MapeoEntidad(PGeneralASubPGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralAsubPgeneral entidad = new TPgeneralAsubPgeneral();
                entidad = Mapper.Map<PGeneralASubPGeneralBO, TPgeneralAsubPgeneral>(objetoBO,
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


        ///Repositorio: PgeneralAsubPgeneralRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPgeneral">Id del Programa General del cual se desea averiguar los programas generales hijos (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Lista de objeto de tipo: List<PgeneralHijoDTO> </returns>
        public List<PgeneralHijoDTO> ObtenerPGeneralHijos(int idPgeneral)
        {
            try
            {
                string queryListaPGeneral = "SELECT Id, IdPgeneral, Nombre, Pg_titulo, pw_duracion FROM pla.V_TPgeneral_ObtenerHijos WHERE IdPGeneral_Padre = @IdPgeneral and Estado = 1";
                var listaprogramaGeneral = _dapper.QueryDapper(queryListaPGeneral, new { IdPgeneral = idPgeneral});
                return JsonConvert.DeserializeObject<List<PgeneralHijoDTO>>(listaprogramaGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }           
        }
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para el CRUD
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public List<PgeneralAsubPgeneralDTO> ObtenerCursosPorPrograma(int idPgeneral)
        {
            List<PgeneralAsubPgeneralDTO> cursos = new List<PgeneralAsubPgeneralDTO>();
            var _query = string.Empty;
            _query = "SELECT IdTroncalGeneral,Nombre,IdCurso,Orden FROM pla.V_TPGeneralASubPGeneral_CursosHijos WHERE EstadoTroncal = 1 and EstadoPGeneralASubPGeneral = 1 and IdPadre = " + idPgeneral;
            var respuestaDapper = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
            {
                cursos = JsonConvert.DeserializeObject<List<PgeneralAsubPgeneralDTO>>(respuestaDapper);
                foreach (var item in cursos)
                {
                    item.listaVersion = new List<PgeneralASubPgeneralVersionProgramaDTO>();
                    var _queryVersion = string.Empty;
                    _queryVersion = "SELECT Id,IdPgeneralASubPgeneral,IdVersionPrograma  FROM pla.V_TPGeneralASubPGeneral_VersionPrograma WHERE Estado = 1 and IdPgeneralASubPgeneral = @IdPgeneralASubPgeneral";
                    var version = _dapper.QueryDapper(_queryVersion, new { IdPgeneralASubPgeneral=item.IdCurso });
                    if (!string.IsNullOrEmpty(version) && !version.Contains("[]"))
                    {
                        item.listaVersion = JsonConvert.DeserializeObject<List<PgeneralASubPgeneralVersionProgramaDTO>>(version);
                    }
                }
                
                
            }

            return cursos;
        }
        /// <summary>
        /// Obtiene los cursos hijos de un programa General para EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<CursoHijoDuracionDTO> ObtenerCursosEstrucuraCurricular(int idMatriculaCabecera)
        {
            
            List<CursoHijoDuracionDTO> cursos = new List<CursoHijoDuracionDTO>();
            cursos = ObtenerCursosCongeladosEstrucuraCurricular(idMatriculaCabecera);
            if (cursos.Count <= 0)
            {
                var _query = string.Empty;
                _query = "SELECT Nombre,Duracion FROM pla.V_PgeneralCursosHijosporVersion WHERE IdMatriculaCabecera=@IdMatriculaCabecera and Estado=1 Order by FechaCreacion asc";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    cursos = JsonConvert.DeserializeObject<List<CursoHijoDuracionDTO>>(respuestaDapper);
                }
            }
            return cursos;
        }
        /// <summary>
        /// Obtiene los cursos hijos congelados de un programa General para EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<CursoHijoDuracionDTO> ObtenerCursosCongeladosEstrucuraCurricular(int idMatriculaCabecera)
        {
            List<CursoHijoDuracionDTO> cursos = new List<CursoHijoDuracionDTO>();
            var _query = string.Empty;
            _query = "SELECT Nombre,Duracion FROM pla.V_PgeneralCursosCongeladosHijosporVersion WHERE IdMatriculaCabecera=@IdMatriculaCabecera and Estado=1 Order by FechaCreacion asc";
            var respuestaDapper = _dapper.QueryDapper(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
            {
                cursos = JsonConvert.DeserializeObject<List<CursoHijoDuracionDTO>>(respuestaDapper);
            }

            return cursos;
        }

        /// <summary>
        /// Obtiene los cursos hijos de un programa General para Congelamiento EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<CursoHijoIdDTO> ObtenerCursosCongelamientoEstrucuraCurricular(int idMatriculaCabecera)
        {
            List<CursoHijoIdDTO> cursos = new List<CursoHijoIdDTO>();
            var _query = string.Empty;
            _query = "SELECT IdPGeneral as Id,GEN.Nombre,PEM.Duracion FROM ope.T_PEspecificoMatriculaAlumno PEM " +
                " INNER JOIN pla.T_PEspecifico PES ON PES.Id=PEM.IdPEspecifico " +
                " INNER JOIN pla.T_PGeneral GEN ON GEN.Id=PES.IdProgramaGeneral " +
                " WHERE PEM.IdMatriculaCabecera=@IdMatriculaCabecera and PEM.Estado=1 Order by PES.Id asc";
            var respuestaDapper = _dapper.QueryDapper(_query, new { IdMatriculaCabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
            {
                cursos = JsonConvert.DeserializeObject<List<CursoHijoIdDTO>>(respuestaDapper);


            }

            return cursos;
        }
    }
}

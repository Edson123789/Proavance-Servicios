using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Scode.BO;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class PGeneralCriterioEvaluacionHijoRepositorio : BaseRepository<TPgeneralCriterioEvaluacionHijo, PGeneralCriterioEvaluacionHijoBO>
    {
        #region Metodos Base
        public PGeneralCriterioEvaluacionHijoRepositorio() : base()
        {
        }
        public PGeneralCriterioEvaluacionHijoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PGeneralCriterioEvaluacionHijoBO> GetBy(Expression<Func<TPgeneralCriterioEvaluacionHijo, bool>> filter)
        {
            IEnumerable<TPgeneralCriterioEvaluacionHijo> listado = base.GetBy(filter);
            List<PGeneralCriterioEvaluacionHijoBO> listadoBO = new List<PGeneralCriterioEvaluacionHijoBO>();
            foreach (var itemEntidad in listado)
            {
                PGeneralCriterioEvaluacionHijoBO objetoBO = Mapper.Map<TPgeneralCriterioEvaluacionHijo, PGeneralCriterioEvaluacionHijoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PGeneralCriterioEvaluacionHijoBO FirstById(int id)
        {
            try
            {
                TPgeneralCriterioEvaluacionHijo entidad = base.FirstById(id);
                PGeneralCriterioEvaluacionHijoBO objetoBO = new PGeneralCriterioEvaluacionHijoBO();
                Mapper.Map<TPgeneralCriterioEvaluacionHijo, PGeneralCriterioEvaluacionHijoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PGeneralCriterioEvaluacionHijoBO FirstBy(Expression<Func<TPgeneralCriterioEvaluacionHijo, bool>> filter)
        {
            try
            {
                TPgeneralCriterioEvaluacionHijo entidad = base.FirstBy(filter);
                PGeneralCriterioEvaluacionHijoBO objetoBO = Mapper.Map<TPgeneralCriterioEvaluacionHijo, PGeneralCriterioEvaluacionHijoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PGeneralCriterioEvaluacionHijoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralCriterioEvaluacionHijo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PGeneralCriterioEvaluacionHijoBO> listadoBO)
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

        public bool Update(PGeneralCriterioEvaluacionHijoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralCriterioEvaluacionHijo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PGeneralCriterioEvaluacionHijoBO> listadoBO)
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
        private void AsignacionId(TPgeneralCriterioEvaluacionHijo entidad, PGeneralCriterioEvaluacionHijoBO objetoBO)
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

        private TPgeneralCriterioEvaluacionHijo MapeoEntidad(PGeneralCriterioEvaluacionHijoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCriterioEvaluacionHijo entidad = new TPgeneralCriterioEvaluacionHijo();
                entidad = Mapper.Map<PGeneralCriterioEvaluacionHijoBO, TPgeneralCriterioEvaluacionHijo>(objetoBO,
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

        public List<PCriterioGeneralHijoModalidadDTO> ListarCriteriosEvaluacionHijo(int idPgeneral)
        {
            try
            {
                List<PCriterioGeneralHijoModalidadDTO> programasGenerales = new List<PCriterioGeneralHijoModalidadDTO>();
                var _query = string.Empty;
                _query = "SELECT DISTINCT IdModalidadCurso,IdPgeneral FROM pla.T_PgeneralCriterioEvaluacionHijo WHERE IdPgeneral= @idPgeneral and Estado=1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PCriterioGeneralHijoModalidadDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosPresencial(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> programasGenerales = new List<PGeneralCursoCriterioHijoVistaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdProgramaGeneralHijo, Nombre,ConsiderarNota,Porcentaje,IdModalidadCurso,EsCurso,IdCriterioEvaluacion FROM pla.V_TPgeneralCE_ObtenerHijos WHERE IdModalidadCurso=0 and IdPgeneral_Padre = @idPgeneral";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosAonline(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> programasGenerales = new List<PGeneralCursoCriterioHijoVistaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdProgramaGeneralHijo, Nombre,ConsiderarNota,Porcentaje,IdModalidadCurso,EsCurso,IdCriterioEvaluacion FROM pla.V_TPgeneralCE_ObtenerHijos WHERE IdModalidadCurso=1 and IdPgeneral_Padre = @idPgeneral";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        //                
        //0
        //Presencial
        //1
        //Online Asincronica
        //2
        //Online Sincronica

        public List<PGeneralCursoCriterioHijoVistaDTO> ListarPadreCursosCriteriosOnline(int idPgeneral)
        {
            try
            {
                List<PGeneralCursoCriterioHijoVistaDTO> programasGenerales = new List<PGeneralCursoCriterioHijoVistaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdProgramaGeneralHijo, Nombre,ConsiderarNota,Porcentaje,IdModalidadCurso,EsCurso,IdCriterioEvaluacion FROM pla.V_TPgeneralCE_ObtenerHijos WHERE IdModalidadCurso=2 and IdPgeneral_Padre = @idPgeneral";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCursoCriterioHijoVistaDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        //Inserta modalidad al curso hijo 
        public void InsertarModalidadPGHIjo(PgeneralModalidadBO pgeneralModalidad)
        {
            try
            {
                var query = _dapper.QuerySPDapper("pla.SP_InsertarPgeneralCriterio", new
                {
                    IdPGeneral = pgeneralModalidad.IdPgeneral,
                    IdPGeneralModalidadCurso = pgeneralModalidad.IdModalidadCurso
                }); ;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        //Elimina logicamente  la modalidad del curso 
        public void EliminarLogico (int IdPgeneral, int idModalidadCurso)
        {
            try
            {
                var query = _dapper.QuerySPDapper("pla.SP_EliminacionCriterioEvaluacionHijo", new
                {
                    IdPGeneral = IdPgeneral,
                    IdPGeneralModalidadCurso = idModalidadCurso
                }); 
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

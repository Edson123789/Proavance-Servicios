using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PGeneralCriterioEvaluacionRepositorio : BaseRepository<TPgeneralCriterioEvaluacion, PGeneralCriterioEvaluacionBO>
    {
        #region Metodos Base
        public PGeneralCriterioEvaluacionRepositorio() : base()
        {
        }
        public PGeneralCriterioEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PGeneralCriterioEvaluacionBO> GetBy(Expression<Func<TPgeneralCriterioEvaluacion, bool>> filter)
        {
            IEnumerable<TPgeneralCriterioEvaluacion> listado = base.GetBy(filter);
            List<PGeneralCriterioEvaluacionBO> listadoBO = new List<PGeneralCriterioEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                PGeneralCriterioEvaluacionBO objetoBO = Mapper.Map<TPgeneralCriterioEvaluacion, PGeneralCriterioEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PGeneralCriterioEvaluacionBO FirstById(int id)
        {
            try
            {
                TPgeneralCriterioEvaluacion entidad = base.FirstById(id);
                PGeneralCriterioEvaluacionBO objetoBO = new PGeneralCriterioEvaluacionBO();
                Mapper.Map<TPgeneralCriterioEvaluacion, PGeneralCriterioEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PGeneralCriterioEvaluacionBO FirstBy(Expression<Func<TPgeneralCriterioEvaluacion, bool>> filter)
        {
            try
            {
                TPgeneralCriterioEvaluacion entidad = base.FirstBy(filter);
                PGeneralCriterioEvaluacionBO objetoBO = Mapper.Map<TPgeneralCriterioEvaluacion, PGeneralCriterioEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PGeneralCriterioEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPgeneralCriterioEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PGeneralCriterioEvaluacionBO> listadoBO)
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

        public bool Update(PGeneralCriterioEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPgeneralCriterioEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PGeneralCriterioEvaluacionBO> listadoBO)
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
        private void AsignacionId(TPgeneralCriterioEvaluacion entidad, PGeneralCriterioEvaluacionBO objetoBO)
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

        private TPgeneralCriterioEvaluacion MapeoEntidad(PGeneralCriterioEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPgeneralCriterioEvaluacion entidad = new TPgeneralCriterioEvaluacion();
                entidad = Mapper.Map<PGeneralCriterioEvaluacionBO, TPgeneralCriterioEvaluacion>(objetoBO,
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
        //Lista Criterio de Evaluacion (PGCriteriosEvaluacion)
        public List<PGeneralCriterioEvaluacionDTO> ListarPGCriteriosEvaluacion(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<PGeneralCriterioEvaluacionDTO> items = new List<PGeneralCriterioEvaluacionDTO>();
                List<Expression<Func<TPgeneralCriterioEvaluacion, bool>>> filters = new List<Expression<Func<TPgeneralCriterioEvaluacion, bool>>>();
                var total = 0;
                List<PGeneralCriterioEvaluacionBO> lista = new List<PGeneralCriterioEvaluacionBO>();
                if (filtro != null && filtro.Take != 0)
                {
                    if ((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        // Creamos la Lista de filtros
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "Nombre":
                                    filters.Add(o => o.Nombre.Contains(filterGrid.Value));
                                    break;
                                case "Porcentaje":
                                    filters.Add(o => o.Porcentaje.ToString().Contains(filterGrid.Value));
                                    break;
                                default:
                                    filters.Add(o => true);
                                    break;
                            }
                        }
                    }
                    lista = GetFiltered(filters, p => p.Id, false).ToList();
                    total = lista.Count();
                    lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
                }
                else
                {
                    lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                    total = lista.Count();
                }
                items = lista.Select(x => new PGeneralCriterioEvaluacionDTO
                {
                    Id = x.Id,
                    Porcentaje = x.Porcentaje,
                    Nombre = x.Nombre
                }).ToList();

                return items;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PGeneralCriterioEvaluacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPgeneralCriterioEvaluacion, bool>>> filters, Expression<Func<TPgeneralCriterioEvaluacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TPgeneralCriterioEvaluacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<PGeneralCriterioEvaluacionBO> listadoBO = new List<PGeneralCriterioEvaluacionBO>();

            foreach (var itemEntidad in listado)
            {
                PGeneralCriterioEvaluacionBO objetoBO = Mapper.Map<TPgeneralCriterioEvaluacion, PGeneralCriterioEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }

        public List<PGeneralCriterioEvaluacionDTO> ListarPGcriteriosEvaluacionPresencial(int idPgeneral)
        {
            try
            {
                List<PGeneralCriterioEvaluacionDTO> programasGenerales = new List<PGeneralCriterioEvaluacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 0 and IdPgeneral = @idPgeneral and Estado=1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<PGeneralCriterioEvaluacionDTO> ListarPGcriteriosEvaluacionOnline(int idPgeneral)
        {
            try
            {
                List<PGeneralCriterioEvaluacionDTO> programasGenerales = new List<PGeneralCriterioEvaluacionDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 2 and IdPgeneral = @idPgeneral and Estado=1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        public PGeneralCriterioEvaluacionDTO ObtenerPGCriEvaluacionOnline(int idPgeneral)
        {
            try
            {
                PGeneralCriterioEvaluacionDTO pgeneral = new PGeneralCriterioEvaluacionDTO();
                string _queryPGeneral = "SELECT Id,IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 2 and IdPegeneral =@Id and Estado=1";
                var _programaGeneral = _dapper.FirstOrDefault(_queryPGeneral, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(_programaGeneral) && !_programaGeneral.Equals("null"))
                {
                    pgeneral = JsonConvert.DeserializeObject<PGeneralCriterioEvaluacionDTO>(_programaGeneral);
                }
                return pgeneral;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Criterios Evaluacion Aonline
        public List<PGeneralCriterioEvaluacionDTO> ListarPGcriteriosEvaluacionAonline(int idPgeneral)
        {
            try
            {

                List<PGeneralCriterioEvaluacionDTO> programasGenerales = new List<PGeneralCriterioEvaluacionDTO>();
                var _query = string.Empty;
                _query = "SELECT IdPgeneral, IdModalidadCurso, Nombre, Porcentaje,IdCriterioEvaluacion,IdTipoPromedio FROM pla.T_PgeneralCriterioEvaluacion WHERE IdModalidadCurso = 1 and IdPgeneral = @idPgeneral and Estado=1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { IdPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        //eliminamos criterios de manera logica cuando la modalidad de un curso padre cambie 
        //Elimina logicamente  la modalidad del curso 
        public void EliminarLogico(int IdPgeneral, int idModalidadCurso)
        {
            try
            {
                var query = _dapper.QuerySPDapper("pla.SP_EliminacionCriterioEvaluacion", new
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

        //Listamos criterios segun id
        public List<PgeneralcriterioEvaluacionModalidadDTO> ListarCriteriosEvaluacion(int idPgeneral)
        {
            try
            {
                List<PgeneralcriterioEvaluacionModalidadDTO> programasGenerales = new List<PgeneralcriterioEvaluacionModalidadDTO>();
                var _query = string.Empty;
                _query = "SELECT DISTINCT IdModalidadCurso,IdPgeneral FROM pla.T_PgeneralCriterioEvaluacion WHERE IdPgeneral= @idPgeneral and Estado=1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { idPgeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PgeneralcriterioEvaluacionModalidadDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<PGeneralCriterioEvaluacionDTO> ListarCriteriosPorPespecificoModalidad(int idPgeneral, int idModalidad)
        {
            try
            {
                List<PGeneralCriterioEvaluacionDTO> programasGenerales = new List<PGeneralCriterioEvaluacionDTO>();
                var _query = string.Empty;
                _query =
                    @"SELECT IdPgeneral, IdModalidadCurso, T_CriterioEvaluacion.Nombre, Porcentaje, IdCriterioEvaluacion 
                            FROM pla.T_PgeneralCriterioEvaluacion 
                            JOIN pla.T_CriterioEvaluacion ON T_CriterioEvaluacion.Id = T_PGeneralCriterioEvaluacion.IdCriterioEvaluacion
                            WHERE IdModalidadCurso = @idModalidad and IdPgeneral = @idPgeneral and T_PgeneralCriterioEvaluacion.Estado = 1
                            AND T_CriterioEvaluacion.Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, new {IdPgeneral = idPgeneral, idModalidad = idModalidad});
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PGeneralCriterioEvaluacionDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }

   
}

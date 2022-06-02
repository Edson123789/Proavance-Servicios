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
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    public class CriterioEvaluacionModalidadCursoRepositorio : BaseRepository<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoBO>
    {
        #region Metodos Base
        public CriterioEvaluacionModalidadCursoRepositorio() : base()
        {
        }
        public CriterioEvaluacionModalidadCursoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionModalidadCursoBO> GetBy(Expression<Func<TCriterioEvaluacionModalidadCurso, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionModalidadCurso> listado = base.GetBy(filter);
            List<CriterioEvaluacionModalidadCursoBO> listadoBO = new List<CriterioEvaluacionModalidadCursoBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionModalidadCursoBO objetoBO = Mapper.Map<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<CriterioEvaluacionModalidadCursoBO> GetBy(Expression<Func<TCriterioEvaluacionModalidadCurso, bool>> filter, int skip, int take)
        {
            IEnumerable<TCriterioEvaluacionModalidadCurso> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<CriterioEvaluacionModalidadCursoBO> listadoBO = new List<CriterioEvaluacionModalidadCursoBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionModalidadCursoBO objetoBO = Mapper.Map<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public CriterioEvaluacionModalidadCursoBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionModalidadCurso entidad = base.FirstById(id);
                CriterioEvaluacionModalidadCursoBO objetoBO = new CriterioEvaluacionModalidadCursoBO();
                Mapper.Map<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionModalidadCursoBO FirstBy(Expression<Func<TCriterioEvaluacionModalidadCurso, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionModalidadCurso entidad = base.FirstBy(filter);
                CriterioEvaluacionModalidadCursoBO objetoBO = Mapper.Map<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionModalidadCursoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionModalidadCursoBO> listadoBO)
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

        public bool Update(CriterioEvaluacionModalidadCursoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionModalidadCurso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionModalidadCursoBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionModalidadCurso entidad, CriterioEvaluacionModalidadCursoBO objetoBO)
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

        private TCriterioEvaluacionModalidadCurso MapeoEntidad(CriterioEvaluacionModalidadCursoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionModalidadCurso entidad = new TCriterioEvaluacionModalidadCurso();
                entidad = Mapper.Map<CriterioEvaluacionModalidadCursoBO, TCriterioEvaluacionModalidadCurso>(objetoBO,
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

        public List<CriterioEvaluacionModalidadCursoDTO> ListarCriteriosEvaluacionModalidadCursoEspecifico(int IdCriterioEvaluacion)
        {
            try
            {
                List<CriterioEvaluacionModalidadCursoDTO> criteriosFiltro = new List<CriterioEvaluacionModalidadCursoDTO>();
                var _queryfiltromodalidad = "Select IdModalidadCurso FROM pla.T_CriterioEvaluacionModalidadCurso where Estado = 1 and IdCriterioEvaluacion=@IdCriterioEvaluacion";
                var SubfiltroModalidad = _dapper.QueryDapper(_queryfiltromodalidad, new { IdCriterioEvaluacion });
                if (!string.IsNullOrEmpty(SubfiltroModalidad) && !SubfiltroModalidad.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionModalidadCursoDTO>>(SubfiltroModalidad);
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        //Actualiza el estado a false 
        public void DeleteLogicoPorCriterio(int IdCriterioEvaluacion, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_CriterioEvaluacionModalidadCurso WHERE Estado = 1 and IdCriterioEvaluacion = @IdCriterioEvaluacion ";
                var query = _dapper.QueryDapper(_query, new { IdCriterioEvaluacion });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.Id));
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

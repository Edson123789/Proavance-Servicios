using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CriterioEvaluacionTipoPersonaRepositorio : BaseRepository<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaBO>
    {
        #region Metodos Base
        public CriterioEvaluacionTipoPersonaRepositorio() : base()
        {
        }
        public CriterioEvaluacionTipoPersonaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionTipoPersonaBO> GetBy(Expression<Func<TCriterioEvaluacionTipoPersona, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionTipoPersona> listado = base.GetBy(filter);
            List<CriterioEvaluacionTipoPersonaBO> listadoBO = new List<CriterioEvaluacionTipoPersonaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionTipoPersonaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<CriterioEvaluacionTipoPersonaBO> GetBy(Expression<Func<TCriterioEvaluacionTipoPersona, bool>> filter, int skip, int take)
        {
            IEnumerable<TCriterioEvaluacionTipoPersona> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<CriterioEvaluacionTipoPersonaBO> listadoBO = new List<CriterioEvaluacionTipoPersonaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionTipoPersonaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public CriterioEvaluacionTipoPersonaBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionTipoPersona entidad = base.FirstById(id);
                CriterioEvaluacionTipoPersonaBO objetoBO = new CriterioEvaluacionTipoPersonaBO();
                Mapper.Map<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionTipoPersonaBO FirstBy(Expression<Func<TCriterioEvaluacionTipoPersona, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionTipoPersona entidad = base.FirstBy(filter);
                CriterioEvaluacionTipoPersonaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionTipoPersonaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionTipoPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionTipoPersonaBO> listadoBO)
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

        public bool Update(CriterioEvaluacionTipoPersonaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionTipoPersona entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionTipoPersonaBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionTipoPersona entidad, CriterioEvaluacionTipoPersonaBO objetoBO)
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

        private TCriterioEvaluacionTipoPersona MapeoEntidad(CriterioEvaluacionTipoPersonaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionTipoPersona entidad = new TCriterioEvaluacionTipoPersona();
                entidad = Mapper.Map<CriterioEvaluacionTipoPersonaBO, TCriterioEvaluacionTipoPersona>(objetoBO,
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

        public void DeleteLogicoPorCriterio(int IdCriterioEvaluacion, string usuario, List<int> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_CriterioEvaluacionTipoPersona WHERE Estado = 1 and IdCriterioEvaluacion = @IdCriterioEvaluacion ";
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
        public List<CriterioEvaluacionTipoPersonaDTO> ListarCriteriosEvaluacionTipoPersona(int IdCriterioEvaluacion)
        {
            try
            {
                List<CriterioEvaluacionTipoPersonaDTO> criteriosFiltro = new List<CriterioEvaluacionTipoPersonaDTO>();
                var _queryfiltroTipoPersona = "Select IdTipoPersona FROM pla.T_CriterioEvaluacionTipoPersona where Estado = 1 and IdCriterioEvaluacion=@IdCriterioEvaluacion";
                var SubfiltroTipoPrograma = _dapper.QueryDapper(_queryfiltroTipoPersona, new { IdCriterioEvaluacion });
                if (!string.IsNullOrEmpty(SubfiltroTipoPrograma) && !SubfiltroTipoPrograma.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoPersonaDTO>>(SubfiltroTipoPrograma);
                }
                return criteriosFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}

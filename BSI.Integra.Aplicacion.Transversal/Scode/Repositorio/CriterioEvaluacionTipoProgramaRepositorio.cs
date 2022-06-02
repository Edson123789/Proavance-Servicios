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
    public class CriterioEvaluacionTipoProgramaRepositorio : BaseRepository<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaBO>
    {
        #region Metodos Base
        public CriterioEvaluacionTipoProgramaRepositorio() : base()
        {
        }
        public CriterioEvaluacionTipoProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CriterioEvaluacionTipoProgramaBO> GetBy(Expression<Func<TCriterioEvaluacionTipoPrograma, bool>> filter)
        {
            IEnumerable<TCriterioEvaluacionTipoPrograma> listado = base.GetBy(filter);
            List<CriterioEvaluacionTipoProgramaBO> listadoBO = new List<CriterioEvaluacionTipoProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionTipoProgramaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<CriterioEvaluacionTipoProgramaBO> GetBy(Expression<Func<TCriterioEvaluacionTipoPrograma, bool>> filter, int skip, int take)
        {
            IEnumerable<TCriterioEvaluacionTipoPrograma> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<CriterioEvaluacionTipoProgramaBO> listadoBO = new List<CriterioEvaluacionTipoProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                CriterioEvaluacionTipoProgramaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public CriterioEvaluacionTipoProgramaBO FirstById(int id)
        {
            try
            {
                TCriterioEvaluacionTipoPrograma entidad = base.FirstById(id);
                CriterioEvaluacionTipoProgramaBO objetoBO = new CriterioEvaluacionTipoProgramaBO();
                Mapper.Map<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CriterioEvaluacionTipoProgramaBO FirstBy(Expression<Func<TCriterioEvaluacionTipoPrograma, bool>> filter)
        {
            try
            {
                TCriterioEvaluacionTipoPrograma entidad = base.FirstBy(filter);
                CriterioEvaluacionTipoProgramaBO objetoBO = Mapper.Map<TCriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CriterioEvaluacionTipoProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCriterioEvaluacionTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CriterioEvaluacionTipoProgramaBO> listadoBO)
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

        public bool Update(CriterioEvaluacionTipoProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCriterioEvaluacionTipoPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CriterioEvaluacionTipoProgramaBO> listadoBO)
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
        private void AsignacionId(TCriterioEvaluacionTipoPrograma entidad, CriterioEvaluacionTipoProgramaBO objetoBO)
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

        private TCriterioEvaluacionTipoPrograma MapeoEntidad(CriterioEvaluacionTipoProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionTipoPrograma entidad = new TCriterioEvaluacionTipoPrograma();
                entidad = Mapper.Map<CriterioEvaluacionTipoProgramaBO, TCriterioEvaluacionTipoPrograma>(objetoBO,
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

        public List<CriterioEvaluacionTipoProgramaDTO> ListarCriteriosEvaluacionTipoProgramaEspecifico(int IdCriterioEvaluacion)
        {
            try
            {
                List<CriterioEvaluacionTipoProgramaDTO> criteriosFiltro = new List<CriterioEvaluacionTipoProgramaDTO>();
                var _queryfiltrocriterio = "Select IdTipoPrograma FROM pla.T_CriterioEvaluacionTipoPrograma where Estado = 1 and IdCriterioEvaluacion = @IdCriterioEvaluacion";
                var SubfiltroCriterio = _dapper.QueryDapper(_queryfiltrocriterio, new { IdCriterioEvaluacion });
                if (!string.IsNullOrEmpty(SubfiltroCriterio) && !SubfiltroCriterio.Contains("[]"))
                {
                    criteriosFiltro = JsonConvert.DeserializeObject<List<CriterioEvaluacionTipoProgramaDTO>>(SubfiltroCriterio);
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
                string _query = "SELECT Id FROM  pla.T_CriterioEvaluacionTipoPrograma WHERE Estado = 1 and IdCriterioEvaluacion = @IdCriterioEvaluacion ";
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

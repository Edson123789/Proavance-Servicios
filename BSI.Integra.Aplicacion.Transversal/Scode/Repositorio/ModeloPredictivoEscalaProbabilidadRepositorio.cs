using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ModeloPredictivoEscalaProbabilidadRepositorio : BaseRepository<TModeloPredictivoEscalaProbabilidad, ModeloPredictivoEscalaProbabilidadBO>
    {
        #region Metodos Base
        public ModeloPredictivoEscalaProbabilidadRepositorio() : base()
        {
        }
        public ModeloPredictivoEscalaProbabilidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoEscalaProbabilidadBO> GetBy(Expression<Func<TModeloPredictivoEscalaProbabilidad, bool>> filter)
        {
            IEnumerable<TModeloPredictivoEscalaProbabilidad> listado = base.GetBy(filter);
            List<ModeloPredictivoEscalaProbabilidadBO> listadoBO = new List<ModeloPredictivoEscalaProbabilidadBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoEscalaProbabilidadBO objetoBO = Mapper.Map<TModeloPredictivoEscalaProbabilidad, ModeloPredictivoEscalaProbabilidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoEscalaProbabilidadBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoEscalaProbabilidad entidad = base.FirstById(id);
                ModeloPredictivoEscalaProbabilidadBO objetoBO = new ModeloPredictivoEscalaProbabilidadBO();
                Mapper.Map<TModeloPredictivoEscalaProbabilidad, ModeloPredictivoEscalaProbabilidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoEscalaProbabilidadBO FirstBy(Expression<Func<TModeloPredictivoEscalaProbabilidad, bool>> filter)
        {
            try
            {
                TModeloPredictivoEscalaProbabilidad entidad = base.FirstBy(filter);
                ModeloPredictivoEscalaProbabilidadBO objetoBO = Mapper.Map<TModeloPredictivoEscalaProbabilidad, ModeloPredictivoEscalaProbabilidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoEscalaProbabilidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoEscalaProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoEscalaProbabilidadBO> listadoBO)
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

        public bool Update(ModeloPredictivoEscalaProbabilidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoEscalaProbabilidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoEscalaProbabilidadBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoEscalaProbabilidad entidad, ModeloPredictivoEscalaProbabilidadBO objetoBO)
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

        private TModeloPredictivoEscalaProbabilidad MapeoEntidad(ModeloPredictivoEscalaProbabilidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoEscalaProbabilidad entidad = new TModeloPredictivoEscalaProbabilidad();
                entidad = Mapper.Map<ModeloPredictivoEscalaProbabilidadBO, TModeloPredictivoEscalaProbabilidad>(objetoBO,
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
        /// Obtiene la escala (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoEscalaDTO> ObtenerEscalaPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoEscalaDTO> resultadoDTO = new List<ModeloPredictivoEscalaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Orden,Nombre,ProbabilidadActual,ProbabilidaIInicial FROM mkt.V_TModeloPredictivoEscalaProbabilidad WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoEscalaDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos de Escala a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoEscalaDTO> nuevos)
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

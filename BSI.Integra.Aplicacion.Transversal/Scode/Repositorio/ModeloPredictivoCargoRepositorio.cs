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
    public class ModeloPredictivoCargoRepositorio : BaseRepository<TModeloPredictivoCargo, ModeloPredictivoCargoBO>
    {
        #region Metodos Base
        public ModeloPredictivoCargoRepositorio() : base()
        {
        }
        public ModeloPredictivoCargoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoCargoBO> GetBy(Expression<Func<TModeloPredictivoCargo, bool>> filter)
        {
            IEnumerable<TModeloPredictivoCargo> listado = base.GetBy(filter);
            List<ModeloPredictivoCargoBO> listadoBO = new List<ModeloPredictivoCargoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoCargoBO objetoBO = Mapper.Map<TModeloPredictivoCargo, ModeloPredictivoCargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoCargoBO FirstById(int id)
        {
            try
            {
                TModeloPredictivoCargo entidad = base.FirstById(id);
                ModeloPredictivoCargoBO objetoBO = new ModeloPredictivoCargoBO();
                Mapper.Map<TModeloPredictivoCargo, ModeloPredictivoCargoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoCargoBO FirstBy(Expression<Func<TModeloPredictivoCargo, bool>> filter)
        {
            try
            {
                TModeloPredictivoCargo entidad = base.FirstBy(filter);
                ModeloPredictivoCargoBO objetoBO = Mapper.Map<TModeloPredictivoCargo, ModeloPredictivoCargoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoCargoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivoCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoCargoBO> listadoBO)
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

        public bool Update(ModeloPredictivoCargoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivoCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoCargoBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivoCargo entidad, ModeloPredictivoCargoBO objetoBO)
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

        private TModeloPredictivoCargo MapeoEntidad(ModeloPredictivoCargoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoCargo entidad = new TModeloPredictivoCargo();
                entidad = Mapper.Map<ModeloPredictivoCargoBO, TModeloPredictivoCargo>(objetoBO,
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
        /// Obtiene los Cargos (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ModeloPredictivoCargoDTO> ObtenerCargoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoCargoDTO> resultadoDTO = new List<ModeloPredictivoCargoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,IdCargo,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoCargo WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoCargoDTO>>(respuestaDapper);
                }

                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Modelos Cargo a un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorPrograma(int idPGeneral, string usuario, List<ModeloPredictivoCargoDTO> nuevos)
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

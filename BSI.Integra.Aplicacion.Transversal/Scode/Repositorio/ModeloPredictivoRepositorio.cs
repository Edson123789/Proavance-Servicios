using System;
using System.Collections.Generic;
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
    public class ModeloPredictivoRepositorio : BaseRepository<TModeloPredictivo, ModeloPredictivoBO>
    {
        #region Metodos Base
        public ModeloPredictivoRepositorio() : base()
        {
        }
        public ModeloPredictivoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloPredictivoBO> GetBy(Expression<Func<TModeloPredictivo, bool>> filter)
        {
            IEnumerable<TModeloPredictivo> listado = base.GetBy(filter);
            List<ModeloPredictivoBO> listadoBO = new List<ModeloPredictivoBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloPredictivoBO objetoBO = Mapper.Map<TModeloPredictivo, ModeloPredictivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloPredictivoBO FirstById(int id)
        {
            try
            {
                TModeloPredictivo entidad = base.FirstById(id);
                ModeloPredictivoBO objetoBO = new ModeloPredictivoBO();
                Mapper.Map<TModeloPredictivo, ModeloPredictivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloPredictivoBO FirstBy(Expression<Func<TModeloPredictivo, bool>> filter)
        {
            try
            {
                TModeloPredictivo entidad = base.FirstBy(filter);
                ModeloPredictivoBO objetoBO = Mapper.Map<TModeloPredictivo, ModeloPredictivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloPredictivoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloPredictivo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloPredictivoBO> listadoBO)
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

        public bool Update(ModeloPredictivoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloPredictivo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloPredictivoBO> listadoBO)
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
        private void AsignacionId(TModeloPredictivo entidad, ModeloPredictivoBO objetoBO)
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

        private TModeloPredictivo MapeoEntidad(ModeloPredictivoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivo entidad = new TModeloPredictivo();
                entidad = Mapper.Map<ModeloPredictivoBO, TModeloPredictivo>(objetoBO,
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
        /// Obtiene el intercepto (activo) para el modelo Predictivo por programa 
        /// registradas en el sistema
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public ModeloPredictivoInterceptoDTO ObtenerInterceptoPorPrograma(int idPGeneral)
        {

            try
            {
                ModeloPredictivoInterceptoDTO resultadoDTO = new ModeloPredictivoInterceptoDTO();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,PeIntercepto,PeEstado FROM pla.V_TModeloPredictivo WHERE " +
                    "Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapper.FirstOrDefault(_query, new { IdPGeneral = idPGeneral});
                if (!respuestaDapper.Equals("null"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<ModeloPredictivoInterceptoDTO>(respuestaDapper);
                }
                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

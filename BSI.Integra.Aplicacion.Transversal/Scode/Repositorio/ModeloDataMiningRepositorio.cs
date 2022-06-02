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
    public class ModeloDataMiningRepositorio : BaseRepository<TModeloDataMining, ModeloDataMiningBO>
    {
        #region Metodos Base
        public ModeloDataMiningRepositorio() : base()
        {
        }
        public ModeloDataMiningRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModeloDataMiningBO> GetBy(Expression<Func<TModeloDataMining, bool>> filter)
        {
            IEnumerable<TModeloDataMining> listado = base.GetBy(filter);
            List<ModeloDataMiningBO> listadoBO = new List<ModeloDataMiningBO>();
            foreach (var itemEntidad in listado)
            {
                ModeloDataMiningBO objetoBO = Mapper.Map<TModeloDataMining, ModeloDataMiningBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModeloDataMiningBO FirstById(int id)
        {
            try
            {
                TModeloDataMining entidad = base.FirstById(id);
                ModeloDataMiningBO objetoBO = new ModeloDataMiningBO();
                Mapper.Map<TModeloDataMining, ModeloDataMiningBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModeloDataMiningBO FirstBy(Expression<Func<TModeloDataMining, bool>> filter)
        {
            try
            {
                TModeloDataMining entidad = base.FirstBy(filter);
                ModeloDataMiningBO objetoBO = Mapper.Map<TModeloDataMining, ModeloDataMiningBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModeloDataMiningBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModeloDataMining entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModeloDataMiningBO> listadoBO)
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

        public bool Update(ModeloDataMiningBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModeloDataMining entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModeloDataMiningBO> listadoBO)
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
        private void AsignacionId(TModeloDataMining entidad, ModeloDataMiningBO objetoBO)
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

        private TModeloDataMining MapeoEntidad(ModeloDataMiningBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModeloDataMining entidad = new TModeloDataMining();
                entidad = Mapper.Map<ModeloDataMiningBO, TModeloDataMining>(objetoBO,
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
        /// Obtiene el programa general por centro de costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public ValorModeloDataMiningDTO ObtenerValoresProbabilidadProgramaGeneral(int idCentroCosto)
        {
            try
            {
                ValorModeloDataMiningDTO valorModeloDataMining = new ValorModeloDataMiningDTO();
                var _query = "SELECT IdProgramaGeneral FROM com.V_ObtenerValoresProbabilidad WHERE IdCentroCosto = @idCentroCosto AND EstadoPEspecifico = 1 AND EstadoPGeneral = 1AND EstadoModeloPredictivo = 1 AND EstadoModeloPredictivoTipoDato = 1";
                var valorModeloDataMiningDB = _dapper.FirstOrDefault(_query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(valorModeloDataMiningDB) && !valorModeloDataMiningDB.Contains("[]") && !valorModeloDataMiningDB.Contains("null"))
                {
                    valorModeloDataMining = JsonConvert.DeserializeObject<ValorModeloDataMiningDTO>(valorModeloDataMiningDB);
                }
                return valorModeloDataMining;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ProbabilidadModeloDataMiningDTO ObtenerProbabilidad(int idOportunidad) {
            try
            {
                ProbabilidadModeloDataMiningDTO valorModeloDataMining = new ProbabilidadModeloDataMiningDTO();
                var valorModeloDataMiningDB =  _dapper.QuerySPFirstOrDefault("com.SP_CalcularProbabilidad", new { idOportunidad });

                if (!string.IsNullOrEmpty(valorModeloDataMiningDB) && !valorModeloDataMiningDB.Contains("[]") && !valorModeloDataMiningDB.Contains("null"))
                {
                    valorModeloDataMining = JsonConvert.DeserializeObject<ProbabilidadModeloDataMiningDTO>(valorModeloDataMiningDB);
                }
                return valorModeloDataMining;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene  la probabilidad calculada para una oportunidad en base a los campos llenados por el alumno
        /// </summary>
        /// <param name="idAreaFormacion"></param>
        /// <param name="idCargo"></param>
        /// <param name="idIndustria"></param>
        /// <param name="idAreaTrabajo"></param>
        /// <param name="idCategoriaDato"></param>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public ProbabilidadInicialDTO ObtenerProbabilidad(int? idAreaFormacion, int? idCargo, int? idIndustria, int? idAreaTrabajo, int? idCategoriaDato, int idProgramaGeneral)
        {
            try
            {
                ProbabilidadInicialDTO probabilidadInicial = new ProbabilidadInicialDTO();
                var probabilidadDB = _dapper.QuerySPFirstOrDefault("com.sp_ObtenerProbabilidad", new { idAreaFormacion, idCargo,idIndustria, idAreaTrabajo, idCategoriaDato, idProgramaGeneral });
                if (!string.IsNullOrEmpty(probabilidadDB) && !probabilidadDB.Contains("[]") && !probabilidadDB.Contains("null"))
                {
                    probabilidadInicial = JsonConvert.DeserializeObject<ProbabilidadInicialDTO>(probabilidadDB);
                }
                return probabilidadInicial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}

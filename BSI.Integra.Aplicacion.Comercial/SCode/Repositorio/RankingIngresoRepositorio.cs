using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class RankingIngresoRepositorio : BaseRepository<TRankingIngreso, RankingIngresoBO>
    {
        #region Metodos Base
        public RankingIngresoRepositorio() : base()
        {
        }
        public RankingIngresoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RankingIngresoBO> GetBy(Expression<Func<TRankingIngreso, bool>> filter)
        {
            IEnumerable<TRankingIngreso> listado = base.GetBy(filter);
            List<RankingIngresoBO> listadoBO = new List<RankingIngresoBO>();
            foreach (var itemEntidad in listado)
            {
                RankingIngresoBO objetoBO = Mapper.Map<TRankingIngreso, RankingIngresoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RankingIngresoBO FirstById(int id)
        {
            try
            {
                TRankingIngreso entidad = base.FirstById(id);
                RankingIngresoBO objetoBO = new RankingIngresoBO();
                Mapper.Map<TRankingIngreso, RankingIngresoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RankingIngresoBO FirstBy(Expression<Func<TRankingIngreso, bool>> filter)
        {
            try
            {
                TRankingIngreso entidad = base.FirstBy(filter);
                RankingIngresoBO objetoBO = Mapper.Map<TRankingIngreso, RankingIngresoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RankingIngresoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRankingIngreso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RankingIngresoBO> listadoBO)
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

        public bool Update(RankingIngresoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRankingIngreso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RankingIngresoBO> listadoBO)
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
        private void AsignacionId(TRankingIngreso entidad, RankingIngresoBO objetoBO)
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

        private TRankingIngreso MapeoEntidad(RankingIngresoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRankingIngreso entidad = new TRankingIngreso();
                entidad = Mapper.Map<RankingIngresoBO, TRankingIngreso>(objetoBO,
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
        /// Obtiene una lista con el Ranking(primero, segundo, tercero) de los Asesores por cada tipo(Senior, Junior)
        /// </summary>
        /// <param name="rankingIngresoBO"></param>
        public List<RankingDTO> GetRanking(RankingIngresoBO rankingIngresoBO)
        {
            try
            {
                //var RankingBD = _dapper.QuerySPDapper("com.SP_ObtenerRankingV2", new { IdAsesor = rankingIngresoBO.IdPersonal });
                var RankingBD = _dapper.QuerySPDapper("com.SP_ObtenerRankingV2", new { IdAsesor = rankingIngresoBO.IdPersonal });

                if (RankingBD != "null")
                {
                    return JsonConvert.DeserializeObject<List<RankingDTO>>(RankingBD);
                }
                throw new Exception(ErrorSistema.Instance.MensajeError(206));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

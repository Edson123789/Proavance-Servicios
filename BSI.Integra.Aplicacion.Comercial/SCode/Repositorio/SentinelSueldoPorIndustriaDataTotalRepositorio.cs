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

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelSueldoPorIndustriaDataTotalRepositorio : BaseRepository<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotalBO>
    {
        #region Metodos Base
        public SentinelSueldoPorIndustriaDataTotalRepositorio() : base()
        {
        }
        public SentinelSueldoPorIndustriaDataTotalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSueldoPorIndustriaDataTotalBO> GetBy(Expression<Func<TSentinelSueldoPorIndustriaDataTotal, bool>> filter)
        {
            IEnumerable<TSentinelSueldoPorIndustriaDataTotal> listado = base.GetBy(filter);
            List<SentinelSueldoPorIndustriaDataTotalBO> listadoBO = new List<SentinelSueldoPorIndustriaDataTotalBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSueldoPorIndustriaDataTotalBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSueldoPorIndustriaDataTotalBO FirstById(int id)
        {
            try
            {
                TSentinelSueldoPorIndustriaDataTotal entidad = base.FirstById(id);
                SentinelSueldoPorIndustriaDataTotalBO objetoBO = new SentinelSueldoPorIndustriaDataTotalBO();
                Mapper.Map<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSueldoPorIndustriaDataTotalBO FirstBy(Expression<Func<TSentinelSueldoPorIndustriaDataTotal, bool>> filter)
        {
            try
            {
                TSentinelSueldoPorIndustriaDataTotal entidad = base.FirstBy(filter);
                SentinelSueldoPorIndustriaDataTotalBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSueldoPorIndustriaDataTotalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSueldoPorIndustriaDataTotal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSueldoPorIndustriaDataTotalBO> listadoBO)
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

        public bool Update(SentinelSueldoPorIndustriaDataTotalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSueldoPorIndustriaDataTotal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSueldoPorIndustriaDataTotalBO> listadoBO)
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
        private void AsignacionId(TSentinelSueldoPorIndustriaDataTotal entidad, SentinelSueldoPorIndustriaDataTotalBO objetoBO)
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

        private TSentinelSueldoPorIndustriaDataTotal MapeoEntidad(SentinelSueldoPorIndustriaDataTotalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustriaDataTotal entidad = new TSentinelSueldoPorIndustriaDataTotal();
                entidad = Mapper.Map<SentinelSueldoPorIndustriaDataTotalBO, TSentinelSueldoPorIndustriaDataTotal>(objetoBO,
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

        public SentinelSueldoPorIndustriaValorDTO ObtenerValorDeSueldoPorInsustria(int idCargo, int idIndustria)
        {
            try
            {
                string _querysueldosporindustriat = "Select Id,Valor from com.V_TSentinelSueldoPorIndustriaDataTotal_Valor Where IdCargo=@IdCargo and IdIndustria=@IdIndustria";
                var querysueldosporindustriat = _dapper.FirstOrDefault(_querysueldosporindustriat, new { IdCargo = idCargo, IdIndustria = idIndustria });
                return  JsonConvert.DeserializeObject<SentinelSueldoPorIndustriaValorDTO>(querysueldosporindustriat);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }


}

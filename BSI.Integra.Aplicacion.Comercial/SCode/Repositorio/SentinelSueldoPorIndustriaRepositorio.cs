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
    public class SentinelSueldoPorIndustriaRepositorio : BaseRepository<TSentinelSueldoPorIndustria, SentinelSueldoPorIndustriaBO>
    {
        #region Metodos Base
        public SentinelSueldoPorIndustriaRepositorio() : base()
        {
        }
        public SentinelSueldoPorIndustriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSueldoPorIndustriaBO> GetBy(Expression<Func<TSentinelSueldoPorIndustria, bool>> filter)
        {
            IEnumerable<TSentinelSueldoPorIndustria> listado = base.GetBy(filter);
            List<SentinelSueldoPorIndustriaBO> listadoBO = new List<SentinelSueldoPorIndustriaBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSueldoPorIndustriaBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustria, SentinelSueldoPorIndustriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSueldoPorIndustriaBO FirstById(int id)
        {
            try
            {
                TSentinelSueldoPorIndustria entidad = base.FirstById(id);
                SentinelSueldoPorIndustriaBO objetoBO = new SentinelSueldoPorIndustriaBO();
                Mapper.Map<TSentinelSueldoPorIndustria, SentinelSueldoPorIndustriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSueldoPorIndustriaBO FirstBy(Expression<Func<TSentinelSueldoPorIndustria, bool>> filter)
        {
            try
            {
                TSentinelSueldoPorIndustria entidad = base.FirstBy(filter);
                SentinelSueldoPorIndustriaBO objetoBO = Mapper.Map<TSentinelSueldoPorIndustria, SentinelSueldoPorIndustriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSueldoPorIndustriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSueldoPorIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSueldoPorIndustriaBO> listadoBO)
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

        public bool Update(SentinelSueldoPorIndustriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSueldoPorIndustria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSueldoPorIndustriaBO> listadoBO)
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
        private void AsignacionId(TSentinelSueldoPorIndustria entidad, SentinelSueldoPorIndustriaBO objetoBO)
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

        private TSentinelSueldoPorIndustria MapeoEntidad(SentinelSueldoPorIndustriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustria entidad = new TSentinelSueldoPorIndustria();
                entidad = Mapper.Map<SentinelSueldoPorIndustriaBO, TSentinelSueldoPorIndustria>(objetoBO,
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

        public SentinelSueldoPorIndustriTipoDTO ObtenerSueldoPorIndustria( int idCargo,int idIndustria)
        {
            try
            {
                string _querysueldosporindustria = "Select Id,Tipo from com.V_TSentinelSueldoPorIndustria_Tipo Where IdCargo=@IdCargo and IdIndustria=@IdIndustria and Estado=1";
                var querysueldosporindustria = _dapper.FirstOrDefault(_querysueldosporindustria, new { IdCargo = idCargo, IdIndustria = idIndustria });
                return JsonConvert.DeserializeObject<SentinelSueldoPorIndustriTipoDTO>(querysueldosporindustria);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

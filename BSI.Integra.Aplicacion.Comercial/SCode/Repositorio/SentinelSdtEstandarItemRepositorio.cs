using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelSdtEstandarItemRepositorio : BaseRepository<TSentinelSdtEstandarItem, SentinelSdtEstandarItemBO>
    {
        #region Metodos Base
        public SentinelSdtEstandarItemRepositorio() : base()
        {
        }
        public SentinelSdtEstandarItemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtEstandarItemBO> GetBy(Expression<Func<TSentinelSdtEstandarItem, bool>> filter)
        {
            IEnumerable<TSentinelSdtEstandarItem> listado = base.GetBy(filter);
            List<SentinelSdtEstandarItemBO> listadoBO = new List<SentinelSdtEstandarItemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtEstandarItemBO objetoBO = Mapper.Map<TSentinelSdtEstandarItem, SentinelSdtEstandarItemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtEstandarItemBO FirstById(int id)
        {
            try
            {
                TSentinelSdtEstandarItem entidad = base.FirstById(id);
                SentinelSdtEstandarItemBO objetoBO = new SentinelSdtEstandarItemBO();
                Mapper.Map<TSentinelSdtEstandarItem, SentinelSdtEstandarItemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtEstandarItemBO FirstBy(Expression<Func<TSentinelSdtEstandarItem, bool>> filter)
        {
            try
            {
                TSentinelSdtEstandarItem entidad = base.FirstBy(filter);
                SentinelSdtEstandarItemBO objetoBO = Mapper.Map<TSentinelSdtEstandarItem, SentinelSdtEstandarItemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtEstandarItemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtEstandarItem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtEstandarItemBO> listadoBO)
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

        public bool Update(SentinelSdtEstandarItemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtEstandarItem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtEstandarItemBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtEstandarItem entidad, SentinelSdtEstandarItemBO objetoBO)
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

        private TSentinelSdtEstandarItem MapeoEntidad(SentinelSdtEstandarItemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtEstandarItem entidad = new TSentinelSdtEstandarItem();
                entidad = Mapper.Map<SentinelSdtEstandarItemBO, TSentinelSdtEstandarItem>(objetoBO,
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

        public List<SentinelSdtEstandarItemDniRucDTO> ObtenerDniRucSentinel(int idSentinel)
        {
            try
            {
                string _queryDniRuc = "Select FechaProceso,TipoDocumento, Documento,RazonSocial,Score,DeudaTotal, SemanaActual,SemanaPrevio,Semaforos From com.V_TSentinelSdtEstandarItem_DniRuc Where IdSentinel=@IdSentinel and Estado = 1 ORDER BY FechaCreacion desc";
                var queryDniRuc = _dapper.QueryDapper(_queryDniRuc, new { IdSentinel = idSentinel });
				return JsonConvert.DeserializeObject<List<SentinelSdtEstandarItemDniRucDTO>>(queryDniRuc);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

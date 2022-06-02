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
    public class SentinelSdtPoshisItemRepositorio : BaseRepository<TSentinelSdtPoshisItem, SentinelSdtPoshisItemBO>
    {
        #region Metodos Base
        public SentinelSdtPoshisItemRepositorio() : base()
        {
        }
        public SentinelSdtPoshisItemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtPoshisItemBO> GetBy(Expression<Func<TSentinelSdtPoshisItem, bool>> filter)
        {
            IEnumerable<TSentinelSdtPoshisItem> listado = base.GetBy(filter);
            List<SentinelSdtPoshisItemBO> listadoBO = new List<SentinelSdtPoshisItemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtPoshisItemBO objetoBO = Mapper.Map<TSentinelSdtPoshisItem, SentinelSdtPoshisItemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtPoshisItemBO FirstById(int id)
        {
            try
            {
                TSentinelSdtPoshisItem entidad = base.FirstById(id);
                SentinelSdtPoshisItemBO objetoBO = new SentinelSdtPoshisItemBO();
                Mapper.Map<TSentinelSdtPoshisItem, SentinelSdtPoshisItemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtPoshisItemBO FirstBy(Expression<Func<TSentinelSdtPoshisItem, bool>> filter)
        {
            try
            {
                TSentinelSdtPoshisItem entidad = base.FirstBy(filter);
                SentinelSdtPoshisItemBO objetoBO = Mapper.Map<TSentinelSdtPoshisItem, SentinelSdtPoshisItemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtPoshisItemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtPoshisItem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtPoshisItemBO> listadoBO)
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

        public bool Update(SentinelSdtPoshisItemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtPoshisItem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtPoshisItemBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtPoshisItem entidad, SentinelSdtPoshisItemBO objetoBO)
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

        private TSentinelSdtPoshisItem MapeoEntidad(SentinelSdtPoshisItemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtPoshisItem entidad = new TSentinelSdtPoshisItem();
                entidad = Mapper.Map<SentinelSdtPoshisItemBO, TSentinelSdtPoshisItem>(objetoBO,
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

        public List<SentinelSdtPoshisItemPosicionHistoriaDTO> ObtenerPosicionHistoria(int idSentinel)
        {
            try
            {
                string _queryPosicionHitorica = "select FechaProceso,SemanaActual,Score,CodigoVariacion,NumeroEntidades,DeudaTotal,ProgresoRegistro,DocImpuesto,DeudaTributaria,DeudaLab" +
                                                "  ,CuentaCorriente,TarjetaCredito,ReporteNegativo,PorcentajeCalificacion,PeorCalificacion  " +
												" from com.V_TSentinelSdtPoshisItem_PosocionHistoria Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryPosicionHistorica = _dapper.QueryDapper(_queryPosicionHitorica, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtPoshisItemPosicionHistoriaDTO>>(queryPosicionHistorica);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

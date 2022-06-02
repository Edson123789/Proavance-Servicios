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
    public class SentinelSdtResVenItemRepositorio : BaseRepository<TSentinelSdtResVenItem, SentinelSdtResVenItemBO>
    {
        #region Metodos Base
        public SentinelSdtResVenItemRepositorio() : base()
        {
        }
        public SentinelSdtResVenItemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtResVenItemBO> GetBy(Expression<Func<TSentinelSdtResVenItem, bool>> filter)
        {
            IEnumerable<TSentinelSdtResVenItem> listado = base.GetBy(filter);
            List<SentinelSdtResVenItemBO> listadoBO = new List<SentinelSdtResVenItemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtResVenItemBO objetoBO = Mapper.Map<TSentinelSdtResVenItem, SentinelSdtResVenItemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtResVenItemBO FirstById(int id)
        {
            try
            {
                TSentinelSdtResVenItem entidad = base.FirstById(id);
                SentinelSdtResVenItemBO objetoBO = new SentinelSdtResVenItemBO();
                Mapper.Map<TSentinelSdtResVenItem, SentinelSdtResVenItemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtResVenItemBO FirstBy(Expression<Func<TSentinelSdtResVenItem, bool>> filter)
        {
            try
            {
                TSentinelSdtResVenItem entidad = base.FirstBy(filter);
                SentinelSdtResVenItemBO objetoBO = Mapper.Map<TSentinelSdtResVenItem, SentinelSdtResVenItemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtResVenItemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtResVenItem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtResVenItemBO> listadoBO)
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

        public bool Update(SentinelSdtResVenItemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtResVenItem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtResVenItemBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtResVenItem entidad, SentinelSdtResVenItemBO objetoBO)
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

        private TSentinelSdtResVenItem MapeoEntidad(SentinelSdtResVenItemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtResVenItem entidad = new TSentinelSdtResVenItem();
                entidad = Mapper.Map<SentinelSdtResVenItemBO, TSentinelSdtResVenItem>(objetoBO,
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
        /// Obtiene Los Datos Vencidos Para El detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtResVenItemDatosVencidosDTO> ObtenerDatosVencidos(int idSentinel)
        {
            try
            {
                string _queryDatosVencido = "Select TipoDocumento,NroDocumento,CantidadDocs,Fuente,Monto,Cantidad,DiasVencidos,Entidad from com.V_TSentinelSdtResVenItem_ObtenerDatosVencidos Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDatosVencido = _dapper.QueryDapper(_queryDatosVencido, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtResVenItemDatosVencidosDTO>>(queryDatosVencido);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

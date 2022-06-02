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
    public class SentinelSdtLincreItemRepositorio : BaseRepository<TSentinelSdtLincreItem, SentinelSdtLincreItemBO>
    {
        #region Metodos Base
        public SentinelSdtLincreItemRepositorio() : base()
        {
        }
        public SentinelSdtLincreItemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtLincreItemBO> GetBy(Expression<Func<TSentinelSdtLincreItem, bool>> filter)
        {
            IEnumerable<TSentinelSdtLincreItem> listado = base.GetBy(filter);
            List<SentinelSdtLincreItemBO> listadoBO = new List<SentinelSdtLincreItemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtLincreItemBO objetoBO = Mapper.Map<TSentinelSdtLincreItem, SentinelSdtLincreItemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtLincreItemBO FirstById(int id)
        {
            try
            {
                TSentinelSdtLincreItem entidad = base.FirstById(id);
                SentinelSdtLincreItemBO objetoBO = new SentinelSdtLincreItemBO();
                Mapper.Map<TSentinelSdtLincreItem, SentinelSdtLincreItemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtLincreItemBO FirstBy(Expression<Func<TSentinelSdtLincreItem, bool>> filter)
        {
            try
            {
                TSentinelSdtLincreItem entidad = base.FirstBy(filter);
                SentinelSdtLincreItemBO objetoBO = Mapper.Map<TSentinelSdtLincreItem, SentinelSdtLincreItemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtLincreItemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtLincreItem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtLincreItemBO> listadoBO)
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

        public bool Update(SentinelSdtLincreItemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtLincreItem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtLincreItemBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtLincreItem entidad, SentinelSdtLincreItemBO objetoBO)
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

        private TSentinelSdtLincreItem MapeoEntidad(SentinelSdtLincreItemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtLincreItem entidad = new TSentinelSdtLincreItem();
                entidad = Mapper.Map<SentinelSdtLincreItemBO, TSentinelSdtLincreItem>(objetoBO,
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

        public List<AlumnosSentinelLineasCreditoDTO> ObtenerLineaDeCredito(int idSentinel)
        {
            try
            {
                string _queryLineaCredito = "Select Id,IdSentinel,TipoDocumento,NumeroDocumento,CnsEntNomRazLn,TipoCuenta,LineaCredito,LineaCreditoNoUtil," +
					"LineaUtil From com.V_TSentinelSdtLincreItem_DatosAlumno Where IdSentinel=@IdSentinel and TipoDocumento='D'  AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryLineaCredito = _dapper.QueryDapper(_queryLineaCredito, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<AlumnosSentinelLineasCreditoDTO>>(queryLineaCredito);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

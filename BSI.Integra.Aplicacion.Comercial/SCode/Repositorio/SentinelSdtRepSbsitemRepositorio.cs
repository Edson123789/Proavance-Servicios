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
    public class SentinelSdtRepSbsitemRepositorio : BaseRepository<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitemBO>
    {
        #region Metodos Base
        public SentinelSdtRepSbsitemRepositorio() : base()
        {
        }
        public SentinelSdtRepSbsitemRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtRepSbsitemBO> GetBy(Expression<Func<TSentinelSdtRepSbsitem, bool>> filter)
        {
            IEnumerable<TSentinelSdtRepSbsitem> listado = base.GetBy(filter);
            List<SentinelSdtRepSbsitemBO> listadoBO = new List<SentinelSdtRepSbsitemBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtRepSbsitemBO objetoBO = Mapper.Map<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitemBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtRepSbsitemBO FirstById(int id)
        {
            try
            {
                TSentinelSdtRepSbsitem entidad = base.FirstById(id);
                SentinelSdtRepSbsitemBO objetoBO = new SentinelSdtRepSbsitemBO();
                Mapper.Map<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitemBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtRepSbsitemBO FirstBy(Expression<Func<TSentinelSdtRepSbsitem, bool>> filter)
        {
            try
            {
                TSentinelSdtRepSbsitem entidad = base.FirstBy(filter);
                SentinelSdtRepSbsitemBO objetoBO = Mapper.Map<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitemBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtRepSbsitemBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtRepSbsitem entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtRepSbsitemBO> listadoBO)
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

        public bool Update(SentinelSdtRepSbsitemBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtRepSbsitem entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtRepSbsitemBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtRepSbsitem entidad, SentinelSdtRepSbsitemBO objetoBO)
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

        private TSentinelSdtRepSbsitem MapeoEntidad(SentinelSdtRepSbsitemBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtRepSbsitem entidad = new TSentinelSdtRepSbsitem();
                entidad = Mapper.Map<SentinelSdtRepSbsitemBO, TSentinelSdtRepSbsitem>(objetoBO,
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
        /// obtiene la linea de deuda de un Contacto Por IdSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<AlumnosSentinelLineasDeudaDTO> ObtenerLineaDeuda(int idSentinel)
        {
            try
            {
                var queryLineaDeuda = _dapper.QuerySPDapper("com.SP_SentinelLineasDeudasByAlumno", new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<AlumnosSentinelLineasDeudaDTO>>(queryLineaDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene La Linea de Deuda Para Detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtRepSbsitemLineaDeudaDTO> ObtenerLineaDeudaSentinel(int idSentinel)
        {
            try
            {
                string _queryDeuda = "Select TipoDoc,NombreRazonSocial,Calificacion,MontoDeuda,DiasVencidos,FechaReporte from com.T_SentinelSdtRepSbsitem Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDeuda = _dapper.QueryDapper(_queryDeuda, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtRepSbsitemLineaDeudaDTO>>(queryDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
            

    }
}

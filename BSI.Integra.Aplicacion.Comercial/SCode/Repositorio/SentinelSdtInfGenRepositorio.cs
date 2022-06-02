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
    public class SentinelSdtInfGenRepositorio : BaseRepository<TSentinelSdtInfGen, SentinelSdtInfGenBO>
    {
        #region Metodos Base
        public SentinelSdtInfGenRepositorio() : base()
        {
        }
        public SentinelSdtInfGenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSdtInfGenBO> GetBy(Expression<Func<TSentinelSdtInfGen, bool>> filter)
        {
            IEnumerable<TSentinelSdtInfGen> listado = base.GetBy(filter);
            List<SentinelSdtInfGenBO> listadoBO = new List<SentinelSdtInfGenBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSdtInfGenBO objetoBO = Mapper.Map<TSentinelSdtInfGen, SentinelSdtInfGenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSdtInfGenBO FirstById(int id)
        {
            try
            {
                TSentinelSdtInfGen entidad = base.FirstById(id);
                SentinelSdtInfGenBO objetoBO = new SentinelSdtInfGenBO();
                Mapper.Map<TSentinelSdtInfGen, SentinelSdtInfGenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSdtInfGenBO FirstBy(Expression<Func<TSentinelSdtInfGen, bool>> filter)
        {
            try
            {
                TSentinelSdtInfGen entidad = base.FirstBy(filter);
                SentinelSdtInfGenBO objetoBO = Mapper.Map<TSentinelSdtInfGen, SentinelSdtInfGenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSdtInfGenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSdtInfGen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSdtInfGenBO> listadoBO)
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

        public bool Update(SentinelSdtInfGenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSdtInfGen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSdtInfGenBO> listadoBO)
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
        private void AsignacionId(TSentinelSdtInfGen entidad, SentinelSdtInfGenBO objetoBO)
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

        private TSentinelSdtInfGen MapeoEntidad(SentinelSdtInfGenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtInfGen entidad = new TSentinelSdtInfGen();
                entidad = Mapper.Map<SentinelSdtInfGenBO, TSentinelSdtInfGen>(objetoBO,
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
        /// Obtiene datos generales relacionados al idSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtInfGenDatosGeneralesDTO> ObtenerDatosGenerales(int idSentinel)
        {
            try
            {
                string _queryDatosGenerales = "select  Id,FechaNacimiento,Sexo,Digito,DigitoAnterior,RUC,RazonSocial,NombreComercial,TipoContribuyente,EstadoContribuyente,CondicionContribuyente,Dependencia,CIIU,FechaActividad,Patron"
                                           + ", Folio, Asiento from com.V_TSentinelSdtInfGen_ObtenerDatosGenerales Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDatosGenerales = _dapper.QueryDapper(_queryDatosGenerales, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtInfGenDatosGeneralesDTO>>(queryDatosGenerales);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);  
            }
        }

            
    }
}

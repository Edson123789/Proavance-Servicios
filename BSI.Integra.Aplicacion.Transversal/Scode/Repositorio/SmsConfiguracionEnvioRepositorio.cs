using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/SmsConfiguracionEnvioRepositorio
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_SmsConfiguracionEnvio
    /// </summary>
    public class SmsConfiguracionEnvioRepositorio : BaseRepository<TSmsConfiguracionEnvio, SmsConfiguracionEnvioBO>
    {
        #region Metodos Base
        public SmsConfiguracionEnvioRepositorio() : base()
        {
        }
        public SmsConfiguracionEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SmsConfiguracionEnvioBO> GetBy(Expression<Func<TSmsConfiguracionEnvio, bool>> filter)
        {
            IEnumerable<TSmsConfiguracionEnvio> listado = base.GetBy(filter);
            List<SmsConfiguracionEnvioBO> listadoBO = new List<SmsConfiguracionEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionEnvioBO objetoBO = Mapper.Map<TSmsConfiguracionEnvio, SmsConfiguracionEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SmsConfiguracionEnvioBO FirstById(int id)
        {
            try
            {
                TSmsConfiguracionEnvio entidad = base.FirstById(id);
                SmsConfiguracionEnvioBO objetoBO = new SmsConfiguracionEnvioBO();
                Mapper.Map<TSmsConfiguracionEnvio, SmsConfiguracionEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SmsConfiguracionEnvioBO FirstBy(Expression<Func<TSmsConfiguracionEnvio, bool>> filter)
        {
            try
            {
                TSmsConfiguracionEnvio entidad = base.FirstBy(filter);
                SmsConfiguracionEnvioBO objetoBO = Mapper.Map<TSmsConfiguracionEnvio, SmsConfiguracionEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SmsConfiguracionEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSmsConfiguracionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SmsConfiguracionEnvioBO> listadoBO)
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

        public bool Update(SmsConfiguracionEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSmsConfiguracionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SmsConfiguracionEnvioBO> listadoBO)
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
        private void AsignacionId(TSmsConfiguracionEnvio entidad, SmsConfiguracionEnvioBO objetoBO)
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

        private TSmsConfiguracionEnvio MapeoEntidad(SmsConfiguracionEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSmsConfiguracionEnvio entidad = new TSmsConfiguracionEnvio();
                entidad = Mapper.Map<SmsConfiguracionEnvioBO, TSmsConfiguracionEnvio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SmsConfiguracionEnvioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSmsConfiguracionEnvio, bool>>> filters, Expression<Func<TSmsConfiguracionEnvio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSmsConfiguracionEnvio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SmsConfiguracionEnvioBO> listadoBO = new List<SmsConfiguracionEnvioBO>();

            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionEnvioBO objetoBO = Mapper.Map<TSmsConfiguracionEnvio, SmsConfiguracionEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene la configuracion de Sms basado en la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase SmsEnvioAnexoDTO</returns>
        public SmsEnvioAnexoDTO ConfiguracionSmsOportunidad(int idOportunidad)
        {
            try
            {
                SmsEnvioAnexoDTO smsOportunidad = null;

                string queryDapper = "SELECT IdPersonal, IdAlumno, IdOportunidad, IdCodigoPais, Celular, Servidor, Tipo, Puerto FROM [mkt].[V_ConfiguracionSmsOportunidad] WHERE IdOportunidad = @IdOportunidad";

                string resultadoQuery = _dapper.FirstOrDefault(queryDapper, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]") && resultadoQuery != "null")
                {
                    smsOportunidad = JsonConvert.DeserializeObject<SmsEnvioAnexoDTO>(resultadoQuery);
                }

                return smsOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los dias sin contacto
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase OportunidadDiasSinContactoDTO</returns>
        public OportunidadDiasSinContactoDTO ObtenerDiasSinContacto(int idOportunidad)
        {
            try
            {
                OportunidadDiasSinContactoDTO diasSinContacto = new OportunidadDiasSinContactoDTO();

                string spQuery = "[mkt].[SP_CalcularDiasSinContactoPorIdOportunidad]";

                string resultadoQuery = _dapper.QuerySPFirstOrDefault(spQuery, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]") && resultadoQuery != "null")
                {
                    diasSinContacto = JsonConvert.DeserializeObject<OportunidadDiasSinContactoDTO>(resultadoQuery);
                }

                return diasSinContacto;
            }
            catch (Exception ex)
            {
                OportunidadDiasSinContactoDTO diasSinContacto = new OportunidadDiasSinContactoDTO()
                {
                    IdOportunidad = idOportunidad,
                    DiasSinContacto = 0
                };

                return diasSinContacto;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppConfiguracionRepositorio : BaseRepository<TWhatsAppConfiguracion, WhatsAppConfiguracionBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionBO> GetBy(Expression<Func<TWhatsAppConfiguracion, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracion> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionBO> listadoBO = new List<WhatsAppConfiguracionBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionBO objetoBO = Mapper.Map<TWhatsAppConfiguracion, WhatsAppConfiguracionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracion entidad = base.FirstById(id);
                WhatsAppConfiguracionBO objetoBO = new WhatsAppConfiguracionBO();
                Mapper.Map<TWhatsAppConfiguracion, WhatsAppConfiguracionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionBO FirstBy(Expression<Func<TWhatsAppConfiguracion, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracion entidad = base.FirstBy(filter);
                WhatsAppConfiguracionBO objetoBO = Mapper.Map<TWhatsAppConfiguracion, WhatsAppConfiguracionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracion entidad, WhatsAppConfiguracionBO objetoBO)
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

        private TWhatsAppConfiguracion MapeoEntidad(WhatsAppConfiguracionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracion entidad = new TWhatsAppConfiguracion();
                entidad = Mapper.Map<WhatsAppConfiguracionBO, TWhatsAppConfiguracion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <param name="idPais">Id del pais (PK de la tabla conf.T_Pais)</param>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        public WhatsAppHostDatosDTO ObtenerCredencialHost(int idPais)
        {
            try
            {
                WhatsAppHostDatosDTO HostDatos = new WhatsAppHostDatosDTO();
                var Query = string.Empty;
                Query = "SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE IdPais = @idPais AND Estado = 1";
                var WhatsAppConfiguracionDB = _dapper.FirstOrDefault(Query, new { idPais });
                HostDatos = JsonConvert.DeserializeObject<WhatsAppHostDatosDTO>(WhatsAppConfiguracionDB);
                return HostDatos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        public List<WhatsAppHostDatosDTO> ObtenerCredencialesHost()
        {
            try
            {
                List<WhatsAppHostDatosDTO> hostDatos = new List<WhatsAppHostDatosDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE Estado = 1";
                var WhatsAppConfiguracionDB = _dapper.QueryDapper(_query, null);
                hostDatos = JsonConvert.DeserializeObject<List<WhatsAppHostDatosDTO>>(WhatsAppConfiguracionDB);
                return hostDatos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

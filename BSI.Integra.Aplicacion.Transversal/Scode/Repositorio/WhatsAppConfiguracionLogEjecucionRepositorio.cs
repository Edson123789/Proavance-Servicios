using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Repositorio: Marketing/WhatsAppConfiguracionLogEjecucion
    /// Autor: Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_WhatsAppConfiguracionLogEjecucion
    /// </summary>
    public class WhatsAppConfiguracionLogEjecucionRepositorio : BaseRepository<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucionBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionLogEjecucionRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionLogEjecucionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionLogEjecucionBO> GetBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionLogEjecucion> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionLogEjecucionBO> listadoBO = new List<WhatsAppConfiguracionLogEjecucionBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionLogEjecucionBO objetoBO = Mapper.Map<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionLogEjecucionBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionLogEjecucion entidad = base.FirstById(id);
                WhatsAppConfiguracionLogEjecucionBO objetoBO = new WhatsAppConfiguracionLogEjecucionBO();
                Mapper.Map<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionLogEjecucionBO FirstBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionLogEjecucion entidad = base.FirstBy(filter);
                WhatsAppConfiguracionLogEjecucionBO objetoBO = Mapper.Map<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionLogEjecucionBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionLogEjecucionBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionLogEjecucion entidad, WhatsAppConfiguracionLogEjecucionBO objetoBO)
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

        private TWhatsAppConfiguracionLogEjecucion MapeoEntidad(WhatsAppConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionLogEjecucion entidad = new TWhatsAppConfiguracionLogEjecucion();
                entidad = Mapper.Map<WhatsAppConfiguracionLogEjecucionBO, TWhatsAppConfiguracionLogEjecucion>(objetoBO,
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
        /// Descripcion: Valida si el numero ya ha sido enviado para no duplicar el envio caso exita no procede caso contrario si procede para el envio en whatsapp 
        /// </summary>
        /// <param name="Celular">Numero de celular del alumno</param>
        /// <returns>Retorna verdadero si existe el numero ya enviado caso contrario falso</returns>
        public bool VerificadEnvioDuplicado(string Celular)
        {
            try
            {
                //ValorStringDTO rpta = new ValorStringDTO();
                string Query = "Select Celular From mkt.V_VerificarEnvioFechaActual Where Celular=@Celular";
                string QueryRespuesta = _dapper.FirstOrDefault(Query, new { Celular });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucionBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsappConfiguracionLogEjecucion(WhatsAppConfiguracionLogEjecucionBO filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionLogEjecucion]";

            var query = _dapper.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.FechaInicio,
                filtro.FechaFin,
                filtro.IdWhatsAppConfiguracionEnvio,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return resultado.Valor;
        }

        /// <summary>
        /// Actualiza en mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucionBO</param>
        /// <returns>Id de la transaccion</returns>
        public bool ActualizarWhatsappConfiguracionLogEjecucionFechaFin(WhatsAppConfiguracionLogEjecucionBO filtro)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarWhatsAppConfiguracionLogEjecucionFechaFin]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    filtro.Id,
                    filtro.FechaFin
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

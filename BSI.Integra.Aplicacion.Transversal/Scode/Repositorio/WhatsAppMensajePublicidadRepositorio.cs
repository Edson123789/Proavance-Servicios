using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/WhatsAppMensajePublicidad
    /// Autor: Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_WhatsAppMensajePublicidad
    /// </summary>
    public class WhatsAppMensajePublicidadRepositorio : BaseRepository<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidadBO>
    {
        #region Metodos Base
        public WhatsAppMensajePublicidadRepositorio() : base()
        {
        }
        public WhatsAppMensajePublicidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppMensajePublicidadBO> GetBy(Expression<Func<TWhatsAppMensajePublicidad, bool>> filter)
        {
            IEnumerable<TWhatsAppMensajePublicidad> listado = base.GetBy(filter);
            List<WhatsAppMensajePublicidadBO> listadoBO = new List<WhatsAppMensajePublicidadBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppMensajePublicidadBO objetoBO = Mapper.Map<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppMensajePublicidadBO FirstById(int id)
        {
            try
            {
                TWhatsAppMensajePublicidad entidad = base.FirstById(id);
                WhatsAppMensajePublicidadBO objetoBO = new WhatsAppMensajePublicidadBO();
                Mapper.Map<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppMensajePublicidadBO FirstBy(Expression<Func<TWhatsAppMensajePublicidad, bool>> filter)
        {
            try
            {
                TWhatsAppMensajePublicidad entidad = base.FirstBy(filter);
                WhatsAppMensajePublicidadBO objetoBO = Mapper.Map<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppMensajePublicidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppMensajePublicidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppMensajePublicidadBO> listadoBO)
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

        public bool Update(WhatsAppMensajePublicidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppMensajePublicidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppMensajePublicidadBO> listadoBO)
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
        private void AsignacionId(TWhatsAppMensajePublicidad entidad, WhatsAppMensajePublicidadBO objetoBO)
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

        private TWhatsAppMensajePublicidad MapeoEntidad(WhatsAppMensajePublicidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppMensajePublicidad entidad = new TWhatsAppMensajePublicidad();
                entidad = Mapper.Map<WhatsAppMensajePublicidadBO, TWhatsAppMensajePublicidad>(objetoBO,
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
        /// Inserta en mkt.T_WhatsAppMensajePublicidad mediante una lista
        /// </summary>
        /// <param name="listaNuevoWhatsAppMensajePublicidad">Lista de objetos de clase WhatsAppMensajePublicidadBO</param>
        /// <returns>Booleano</returns>
        public bool InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(List<WhatsAppMensajePublicidadBO> listaNuevoWhatsAppMensajePublicidad)
        {
            try
            {
                string spQuery = "[mkt].[SP_InsertarWhatsAppMensajePublicidadMasivo]";

                var subListasBloque =
                        listaNuevoWhatsAppMensajePublicidad.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Index / 500)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();

                foreach (var bloque in subListasBloque)
                {
                    _dapper.QuerySPFirstOrDefault(spQuery, new
                    {
                        ListaIdAlumno = string.Join(",", bloque.Select(s => s.IdAlumno)),
                        ListaIdPersonal = string.Join(",", bloque.Select(s => s.IdPersonal)),
                        ListaIdPrioridadMailChimpListaCorreo = string.Join(",", bloque.Select(s => s.IdPrioridadMailChimpListaCorreo)),
                        ListaIdWhatsAppEstadoValidacion = string.Join(",", bloque.Select(s => s.IdWhatsAppEstadoValidacion)),
                        ListaIdWhatsAppConfiguracionEnvio = string.Join(",", bloque.Select(s => s.IdWhatsAppConfiguracionEnvio)),
                        ListaIdPais = string.Join(",", bloque.Select(s => s.IdPais)),
                        ListaCelular = string.Join(",", bloque.Select(s => s.Celular)),
                        ListaEsValido = string.Join(",", bloque.Select(s => s.EsValido ? "1" : "0"))
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppMensajePublicidad
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppMensajePublicidadBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppMensajePublicidad(WhatsAppMensajePublicidadBO filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppMensajePublicidadMailingGeneral]";

            var query = _dapper.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.IdAlumno,
                filtro.IdPersonal,
                filtro.IdConjuntoListaResultado,
                filtro.IdPrioridadMailChimpListaCorreo,
                filtro.IdWhatsAppEstadoValidacion,
                filtro.IdWhatsAppConfiguracionEnvio,
                filtro.IdPais,
                filtro.Celular,
                filtro.EsValido,
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
        /// Actualiza los contactos del primer preprocesamiento de campania general
        /// </summary>
        /// <param name="preprocesamientoWhatsAppCampaniaGeneral">Objeto de clase PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO</param>
        /// <returns>Boolean</returns>
        public bool ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO preprocesamientoWhatsAppCampaniaGeneral)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarPersonalWhatsAppPublicidadCampaniaGeneral]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalle = preprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle,
                    ListaIdPersonal = string.Join(",", preprocesamientoWhatsAppCampaniaGeneral.ListaResponsableReal.Select(s => s.IdResponsable)),
                    ListaCantidad = string.Join(",", preprocesamientoWhatsAppCampaniaGeneral.ListaResponsableReal.Select(s => s.Total)),
                    Usuario = preprocesamientoWhatsAppCampaniaGeneral.Usuario
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la lista de WhatsApp resultante del primero procesado
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general</param>
        /// <returns>Lista de objetos de clase WhatsAppResultadoCampaniaGeneralDTO</returns>
        public List<WhatsAppResultadoCampaniaGeneralDTO> ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<WhatsAppResultadoCampaniaGeneralDTO> resultadoFinal = new List<WhatsAppResultadoCampaniaGeneralDTO>();

                string spQuery = "[mkt].[SP_ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral]";

                var resultado = _dapper.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    resultadoFinal = JsonConvert.DeserializeObject<List<WhatsAppResultadoCampaniaGeneralDTO>>(resultado);

                return resultadoFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

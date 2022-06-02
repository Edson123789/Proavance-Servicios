using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/PrioridadMailChimpLista
    /// Autor: Johan Cayo - Ansoli Espinoza - Fischer Valdez - Wilber Choque - Gian Miranda
    /// Fecha: 09/04/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_PrioridadMailChimpLista
    /// </summary>
    public class PrioridadMailChimpListaRepositorio : BaseRepository<TPrioridadMailChimpLista, PrioridadMailChimpListaBO>
    {
        #region Metodos Base
        public PrioridadMailChimpListaRepositorio() : base()
        {
        }
        public PrioridadMailChimpListaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PrioridadMailChimpListaBO> GetBy(Expression<Func<TPrioridadMailChimpLista, bool>> filter)
        {
            IEnumerable<TPrioridadMailChimpLista> listado = base.GetBy(filter);
            List<PrioridadMailChimpListaBO> listadoBO = new List<PrioridadMailChimpListaBO>();
            foreach (var itemEntidad in listado)
            {
                PrioridadMailChimpListaBO objetoBO = Mapper.Map<TPrioridadMailChimpLista, PrioridadMailChimpListaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PrioridadMailChimpListaBO FirstById(int id)
        {
            try
            {
                TPrioridadMailChimpLista entidad = base.FirstById(id);
                PrioridadMailChimpListaBO objetoBO = new PrioridadMailChimpListaBO();
                Mapper.Map<TPrioridadMailChimpLista, PrioridadMailChimpListaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PrioridadMailChimpListaBO FirstBy(Expression<Func<TPrioridadMailChimpLista, bool>> filter)
        {
            try
            {
                TPrioridadMailChimpLista entidad = base.FirstBy(filter);
                PrioridadMailChimpListaBO objetoBO = Mapper.Map<TPrioridadMailChimpLista, PrioridadMailChimpListaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PrioridadMailChimpListaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPrioridadMailChimpLista entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PrioridadMailChimpListaBO> listadoBO)
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

        public bool Update(PrioridadMailChimpListaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPrioridadMailChimpLista entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PrioridadMailChimpListaBO> listadoBO)
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
        private void AsignacionId(TPrioridadMailChimpLista entidad, PrioridadMailChimpListaBO objetoBO)
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

        private TPrioridadMailChimpLista MapeoEntidad(PrioridadMailChimpListaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPrioridadMailChimpLista entidad = new TPrioridadMailChimpLista();
                entidad = Mapper.Map<PrioridadMailChimpListaBO, TPrioridadMailChimpLista>(objetoBO,
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
        /// Obtiene una Lista PrioridadMailChimpLista por un rango de fechas
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<PrioridadMailChimpListaBO> PrioridadesMailChimpListaPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<PrioridadMailChimpListaBO> prioridadesMAilchimp = new List<PrioridadMailChimpListaBO>();
                prioridadesMAilchimp = GetBy(x => x.Enviado == true && x.IdCampaniaMailchimp != null && x.IdListaMailchimp != null
                                                                && x.FechaEnvio != null
                                                                && x.FechaEnvio > fechaInicio
                                                                && x.FechaEnvio <= fechaFin).ToList();
                return prioridadesMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ValorStringDTO> PrioridadesMailChimpPorIntervaloFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var prioridadesMailchimp = new List<ValorStringDTO>();
                string spReporte = "mkt.SP_ObtenerPrioridadesPorFecha";

                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    prioridadesMailchimp = JsonConvert.DeserializeObject<List<ValorStringDTO>>(resultadoReporte);
                }

                return prioridadesMailchimp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene una Lista Id de Campanias Mailchimp de todas la campanias generales
        /// </summary>
        /// <returns></returns>
        public List<ValorStringDTO> PrioridadesMailChimpPorProcedimientoAlmacenado()
        {
            try
            {
                var prioridadesMailchimp = new List<ValorStringDTO>();
                string spReporte = "mkt.SP_ListaMailChimpPersonalizada";

                string resultadoReporte = _dapper.QuerySPDapper(spReporte, null);

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    prioridadesMailchimp = JsonConvert.DeserializeObject<List<ValorStringDTO>>(resultadoReporte);
                }

                return prioridadesMailchimp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene una Lista PrioridadMailChimpLista de todas la campanias generales
        /// </summary>
        /// <returns></returns>
        public List<PrioridadMailChimpListaBO> PrioridadesMailChimpListaCampaniaGeneral()
        {
            try
            {
                List<PrioridadMailChimpListaBO> prioridadesMailchimp = new List<PrioridadMailChimpListaBO>();
                prioridadesMailchimp = GetBy(x => x.Enviado == true && x.IdCampaniaGeneralDetalle != null && x.IdListaMailchimp != null
                                                                && x.FechaCreacion >= DateTime.Now.Date.AddDays(-7)).ToList();
                return prioridadesMailchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ValorStringDTO> ObtenerListaMailchimpIdParaMiembros()
        {
            try
            {
                var listaMailChimpId = new List<ValorStringDTO>();
                string spReporte = "mkt.SP_ObtenerListasParaMiembroMailchimp";

                string resultadoReporte = _dapper.QuerySPDapper(spReporte, null);

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    listaMailChimpId = JsonConvert.DeserializeObject<List<ValorStringDTO>>(resultadoReporte);
                }

                return listaMailChimpId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene una Lista PrioridadMailChimpLista por un rango de 5 dias anteriores
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<PrioridadMailChimpListaBO> PrioridadesMailChimpListaPorCincoDias()
        {
            try
            {
                DateTime fecha5dias_antes = DateTime.Now.Date.AddDays(-5);
                List<PrioridadMailChimpListaBO> prioridadesMAilchimp = new List<PrioridadMailChimpListaBO>();
                prioridadesMAilchimp = GetBy(x => x.Enviado == true && x.IdCampaniaMailchimp != null && x.IdListaMailchimp != null
                                                                && x.FechaEnvio != null
                                                                && x.FechaEnvio > fecha5dias_antes).ToList();
                return prioridadesMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una PrioridadMailChimpLista no Enviado por su Iddetalle
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public PrioridadMailChimpListaBO PrioridadesMailChimpListaPorMailchimpDetalleNoEnviado(int IdCampaniaMailingDetalle)
        {
            try
            {
                PrioridadMailChimpListaBO prioridadMAilchimp = new PrioridadMailChimpListaBO();
                prioridadMAilchimp = FirstBy(x => x.IdCampaniaMailingDetalle == IdCampaniaMailingDetalle
                                                && x.Enviado != true && x.Estado == true);
                return prioridadMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una PrioridadMailChimpLista no Enviado por su Id
        /// </summary>
        /// <param name="IdCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de clase PrioridadMailChimpListaBO</returns>
        public PrioridadMailChimpListaBO PrioridadesMailChimpListaPorMailchimpDetalleCampaniaGeneralNoEnviado(int idCampaniaGeneralDetalle)
        {
            try
            {
                PrioridadMailChimpListaBO prioridadMailchimp = new PrioridadMailChimpListaBO();
                prioridadMailchimp = FirstBy(x => x.IdCampaniaGeneralDetalle == idCampaniaGeneralDetalle
                                                && x.Enviado != true && x.Estado == true);
                return prioridadMailchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una PrioridadMailChimpLista por su Iddetalle
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public PrioridadMailChimpListaBO PrioridadesMailChimpListaPorMailchimpDetalle(int IdCampaniaMailingDetalle)
        {
            try
            {
                PrioridadMailChimpListaBO prioridadMAilchimp = new PrioridadMailChimpListaBO();
                prioridadMAilchimp = FirstBy(x => x.IdCampaniaMailingDetalle == IdCampaniaMailingDetalle
                                                && x.Enviado != true && x.IdCampaniaMailchimp != null
                                                && x.IdListaMailchimp != null);
                return prioridadMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una PrioridadMailChimpLista por su Iddetalle
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public PrioridadMailChimpListaBO PrioridadesMailChimpListaPorMailchimpDetalleAutomatico(int IdCampaniaMailingDetalle)
        {
            try
            {
                PrioridadMailChimpListaBO prioridadMAilchimp = new PrioridadMailChimpListaBO();
                prioridadMAilchimp = FirstBy(x => x.IdCampaniaMailingDetalle == IdCampaniaMailingDetalle
                                                && x.Enviado == true && x.IdCampaniaMailchimp != null
                                                && x.IdListaMailchimp != null && x.FechaEnvio == null);
                return prioridadMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una PrioridadMailChimpLista por su Iddetalle
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public PrioridadMailChimpListaBO PrioridadesMailChimpListaPorMailchimpDetalleCampaniaGeneralAutomatico(int IdCampaniaGeneralDetalle)
        {
            try
            {
                PrioridadMailChimpListaBO prioridadMAilchimp = new PrioridadMailChimpListaBO();
                prioridadMAilchimp = FirstBy(x => x.IdCampaniaGeneralDetalle == IdCampaniaGeneralDetalle
                                                && x.Enviado == true && x.IdCampaniaMailchimp != null
                                                && x.IdListaMailchimp != null && x.FechaEnvio == null);
                return prioridadMAilchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de Prioridades  por su IdCampaniaMailing
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<PrioridadMailChimpListaBO> PrioridadesMailChimpListaPorIdCampaniaMailing(int IdCampaniaMailing)
        {
            try
            {
                List<PrioridadMailChimpListaBO> prioridades = new List<PrioridadMailChimpListaBO>();
                prioridades = GetBy(x => x.IdCampaniaMailing == IdCampaniaMailing &&
                    x.Enviado != true && x.Estado == true && x.IdCampaniaMailchimp == null && x.IdListaMailchimp == null).ToList();
                return prioridades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene una lista de Prioridades  por su IdCampaniaMailing
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<PrioridadMailChimpListaBO> PrioridadesMailChimpListaPorIdCampaniaGeneral(int IdCampaniaGeneralDetalle)
        {
            try
            {
                List<PrioridadMailChimpListaBO> prioridades = new List<PrioridadMailChimpListaBO>();
                prioridades = GetBy(x => x.IdCampaniaGeneralDetalle == IdCampaniaGeneralDetalle &&
                    x.Enviado != true && x.Estado == true && x.IdCampaniaMailchimp == null && x.IdListaMailchimp == null).ToList();
                return prioridades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los precios al contado por un DetalleMailing
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        /// <returns></returns>
        public List<CampaniasMailingPrecioDTO> ObtenerPreciosAlContadoPorIdDetalle(int idCampaniaMailingDetalle)
        {
            try
            {
                List<CampaniasMailingPrecioDTO> precios = new List<CampaniasMailingPrecioDTO>();
                string _query = string.Empty;

                _query = "select * from mkt.V_ObtenerDetallePrecioByCampaniasMailingDetalle Where IdCampaniaMailingDetalle = @ParametroIdCampaniaMailingDetalle AND Nombre='Contado' and MontoPagoEstado = 1";
                var query = _dapper.QueryDapper(_query, new { ParametroIdCampaniaMailingDetalle = idCampaniaMailingDetalle });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    precios = JsonConvert.DeserializeObject<List<CampaniasMailingPrecioDTO>>(query);
                }
                return precios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los precios al contado por un DetalleGeneral
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle"></param>
        /// <returns></returns>
        public List<CampaniasGeneralPrecioDTO> ObtenerPreciosAlContadoPorIdDetalleGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniasGeneralPrecioDTO> precios = new List<CampaniasGeneralPrecioDTO>();
                string _query = string.Empty;

                _query = "select * from mkt.V_ObtenerDetallePrecioByCampaniasGeneralDetalle Where IdCampaniaGeneralDetalle = @ParametroIdCampaniaGeneralDetalle AND Nombre='Contado' and MontoPagoEstado = 1";
                var query = _dapper.QueryDapper(_query, new { ParametroIdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    precios = JsonConvert.DeserializeObject<List<CampaniasGeneralPrecioDTO>>(query);
                }
                return precios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los precios al Credito por un DetalleMailing
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        /// <returns></returns>
        public List<CampaniasMailingPrecioDTO> ObtenerPreciosAlCreditoPorIdDetalle(int idCampaniaMailingDetalle)
        {
            try
            {
                List<CampaniasMailingPrecioDTO> precios = new List<CampaniasMailingPrecioDTO>();
                string _query = string.Empty;

                _query = "select * from mkt.V_ObtenerDetallePrecioByCampaniasMailingDetalle Where IdCampaniaMailingDetalle = @ParametroIdCampaniaMailingDetalle AND Nombre='Crédito' and MontoPagoEstado = 1";
                var query = _dapper.QueryDapper(_query, new { ParametroIdCampaniaMailingDetalle = idCampaniaMailingDetalle });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    precios = JsonConvert.DeserializeObject<List<CampaniasMailingPrecioDTO>>(query);
                }
                return precios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los precios al Credito por un DetalleGeneral
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase CampaniasGeneralPrecioDTO</returns>
        public List<CampaniasGeneralPrecioDTO> ObtenerPreciosAlCreditoPorIdDetalleGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniasGeneralPrecioDTO> precios = new List<CampaniasGeneralPrecioDTO>();
                string _query = string.Empty;

                _query = "select * from mkt.V_ObtenerDetallePrecioByCampaniasGeneralDetalle Where IdCampaniaGeneralDetalle = @ParametroIdCampaniaGeneralDetalle AND Nombre='Crédito' and MontoPagoEstado = 1";
                var query = _dapper.QueryDapper(_query, new { ParametroIdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    precios = JsonConvert.DeserializeObject<List<CampaniasGeneralPrecioDTO>>(query);
                }
                return precios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina la lista mailchimp sin enviar por id de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general</param>
        public void EliminarListaMailchimpSinEnviar(int idCampaniaMailingDetalle)
        {
            try
            {
                string sp = "mkt.SP_EliminarListaMailchimpSinEnviar";
                var query = _dapper.QuerySPDapper(sp, new { IdCampaniasMailingDetalle = idCampaniaMailingDetalle });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina la lista mailchimp sin enviar por id de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general</param>
        /// <param name="usuario">Usuario que realiza el eliminado</param>
        public void EliminarListaMailchimpSinEnviarPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, string usuario)
        {
            try
            {
                string sp = "mkt.SP_EliminarListaMailchimpSinEnviarCampaniaGeneralDetalle";
                var query = _dapper.QuerySPDapper(sp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, Usuario = usuario });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina todos los registros de PrioridadMailChimpListaCorreo segun una campania mailing 
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        public void EliminarListaMailChimpListaCorreos(int idCampaniaMailing)
        {
            try
            {
                string sp = "mkt.SP_EliminarListaMailChimpListaCorreos";
                var query = _dapper.QuerySPDapper(sp, new { IdCampaniaMailing = idCampaniaMailing });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina todos los registros de PrioridadMailChimpListaCorreo segun una campania general
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle la campania general (mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="usuario">Usuario que realiza el eliminado de los contactos</param>
        /// <returns>Booleano con true si en caso fue exitoso</returns>
        public bool EliminarContactosPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, string usuarioResponsable)
        {
            try
            {
                string spEliminado = "mkt.SP_EliminarContactosPorIdCampaniaGeneralDetalle";
                var query = _dapper.QuerySPDapper(spEliminado, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, Usuario = usuarioResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene las campañas enviadas dentro de un periodo de tiempo
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio de la fecha de envio</param>
        /// <param name="fechaFin">Fecha de fin de la fecha de envio</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpListaBO</returns>
        public List<PrioridadMailChimpListaBO> ObtenerCampaniasPorFechaEnvio(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return this.GetBy(x => x.Enviado == true && x.IdCampaniaMailchimp != null && x.IdListaMailchimp != null
                                                                && x.FechaEnvio != null
                                                                && x.FechaEnvio >= fechaInicio
                                                                && x.FechaEnvio <= fechaFin).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene por campania
        /// </summary>
        /// <param name="IdCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMAiling)</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpListaBO</returns>
        public List<PrioridadMailChimpListaBO> ObtenerPorCampaniaMailing(int IdCampaniaMailing)
        {
            try
            {
                return this.GetBy(x => x.IdCampaniaMailing == IdCampaniaMailing).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene las prioridades mailchimp segun las listas de la campanias generales detalle
        /// </summary>
        /// <param name="IdCampaniaMailing">Id de la campania (PK de la tabla mkt)</param>
        /// <returns>Lista de objetos de clase PrioridadMailChimpListBO</returns>
        public List<PrioridadMailChimpListaBO> ObtenerPorCampaniaGeneralDetalle(List<int> IdCampaniaGeneralDetalle)
        {
            try
            {
                return this.GetBy(x => IdCampaniaGeneralDetalle.Contains(x.IdCampaniaGeneralDetalle.Value)).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta PrioridadMailChimpLista, datos (CantidadContactos, UsuarioModificacion, FechaModificacion)
        /// </summary>
        /// <param name="prioridadMailChimpListaInsercion">Objeto de clase PrioridadMailChimpListaInsercionDTO </param>
        /// <returns>Int</returns>
        public int InsertarPrioridadMailChimpListaFiltro(PrioridadMailChimpListaInsercionDTO prioridadMailChimpListaInsercion)
        {
            try
            {
                PrioridadMailChimpListaInsercionDTO objResultado = new PrioridadMailChimpListaInsercionDTO();

                string spQuery = "[mkt].[SP_InsertarPrioridadMailChimpLista]";
                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    prioridadMailChimpListaInsercion.IdCampaniaMailing,
                    prioridadMailChimpListaInsercion.IdCampaniaMailingDetalle,
                    prioridadMailChimpListaInsercion.Asunto,
                    prioridadMailChimpListaInsercion.Contenido,
                    prioridadMailChimpListaInsercion.AsuntoLista,
                    prioridadMailChimpListaInsercion.IdPersonal,
                    prioridadMailChimpListaInsercion.NombreAsesor,
                    prioridadMailChimpListaInsercion.Alias,
                    prioridadMailChimpListaInsercion.Etiquetas,
                    prioridadMailChimpListaInsercion.Enviado,
                    prioridadMailChimpListaInsercion.UsuarioCreacion,
                    prioridadMailChimpListaInsercion.UsuarioModificacion
                });
                if (!string.IsNullOrEmpty(query))
                {
                    objResultado = JsonConvert.DeserializeObject<PrioridadMailChimpListaInsercionDTO>(query);
                }

                return objResultado.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserta PrioridadMailChimpLista, datos (CantidadContactos, UsuarioModificacion, FechaModificacion)
        /// </summary>
        /// <param name="prioridadMailChimpListaInsercion">Objeto de tipo PrioridadMailChimpListaInsercionDTO</param>
        /// <returns>Entero con el Id del scope</returns>
        public int InsertarPrioridadMailChimpListaFiltroCampaniaGeneral(PrioridadMailChimpListaInsercionDTO prioridadMailChimpListaInsercion)
        {
            try
            {
                PrioridadMailChimpListaInsercionDTO objResultado = new PrioridadMailChimpListaInsercionDTO();

                string spQuery = "[mkt].[SP_InsertarPrioridadMailChimpListaCampaniaGeneral]";
                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    prioridadMailChimpListaInsercion.IdCampaniaMailing,
                    prioridadMailChimpListaInsercion.IdCampaniaMailingDetalle,
                    prioridadMailChimpListaInsercion.IdCampaniaGeneralDetalle,
                    prioridadMailChimpListaInsercion.Asunto,
                    prioridadMailChimpListaInsercion.Contenido,
                    prioridadMailChimpListaInsercion.AsuntoLista,
                    prioridadMailChimpListaInsercion.IdPersonal,
                    prioridadMailChimpListaInsercion.NombreAsesor,
                    prioridadMailChimpListaInsercion.Alias,
                    prioridadMailChimpListaInsercion.Etiquetas,
                    prioridadMailChimpListaInsercion.Enviado,
                    prioridadMailChimpListaInsercion.UsuarioCreacion,
                    prioridadMailChimpListaInsercion.UsuarioModificacion
                });
                if (!string.IsNullOrEmpty(query))
                {
                    objResultado = JsonConvert.DeserializeObject<PrioridadMailChimpListaInsercionDTO>(query);
                }

                return objResultado.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Metodo para obtener las listas a descargar interacciones
        /// </summary>
        /// <returns>Lista de objetos de clase ValorStringDTO</returns>
        public List<ValorStringDTO> ObtenerListasDescargar()
        {
            try
            {
                List<ValorStringDTO> _lista = new List<ValorStringDTO>();
                string _query = string.Empty;

                _query = "select IdListaMailchimp as Valor from mkt.V_ObtenerListaMailChimp";
                var query = _dapper.QueryDapper(_query, new { });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    _lista = JsonConvert.DeserializeObject<List<ValorStringDTO>>(query);
                }
                return _lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

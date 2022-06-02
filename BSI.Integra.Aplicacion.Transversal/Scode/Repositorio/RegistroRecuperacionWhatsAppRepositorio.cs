using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/RegistroRecuperacionWhatsApp
    /// Autor: Gian Miranda
    /// Fecha: 11/01/2022
    /// <summary>
    /// Repositorio para consultas de mkt.T_RegistroRecuperacionWhatsApp
    /// </summary>
    public class RegistroRecuperacionWhatsAppRepositorio : BaseRepository<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppBO>
    {
        #region Metodos Base
        public RegistroRecuperacionWhatsAppRepositorio() : base()
        {
        }
        public RegistroRecuperacionWhatsAppRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegistroRecuperacionWhatsAppBO> GetBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            IEnumerable<TRegistroRecuperacionWhatsApp> listado = base.GetBy(filter);
            List<RegistroRecuperacionWhatsAppBO> listadoBO = new List<RegistroRecuperacionWhatsAppBO>();
            foreach (var itemEntidad in listado)
            {
                RegistroRecuperacionWhatsAppBO objetoBO = Mapper.Map<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RegistroRecuperacionWhatsAppBO FirstById(int id)
        {
            try
            {
                TRegistroRecuperacionWhatsApp entidad = base.FirstById(id);
                RegistroRecuperacionWhatsAppBO objetoBO = new RegistroRecuperacionWhatsAppBO();
                Mapper.Map<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegistroRecuperacionWhatsAppBO FirstBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter)
        {
            try
            {
                TRegistroRecuperacionWhatsApp entidad = base.FirstBy(filter);
                RegistroRecuperacionWhatsAppBO objetoBO = Mapper.Map<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegistroRecuperacionWhatsAppBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegistroRecuperacionWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegistroRecuperacionWhatsAppBO> listadoBO)
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

        public bool Update(RegistroRecuperacionWhatsAppBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegistroRecuperacionWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegistroRecuperacionWhatsAppBO> listadoBO)
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
        private void AsignacionId(TRegistroRecuperacionWhatsApp entidad, RegistroRecuperacionWhatsAppBO objetoBO)
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

        private TRegistroRecuperacionWhatsApp MapeoEntidad(RegistroRecuperacionWhatsAppBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegistroRecuperacionWhatsApp entidad = new TRegistroRecuperacionWhatsApp();
                entidad = Mapper.Map<RegistroRecuperacionWhatsAppBO, TRegistroRecuperacionWhatsApp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<RegistroRecuperacionWhatsAppBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TRegistroRecuperacionWhatsApp, bool>>> filters, Expression<Func<TRegistroRecuperacionWhatsApp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TRegistroRecuperacionWhatsApp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<RegistroRecuperacionWhatsAppBO> listadoBO = new List<RegistroRecuperacionWhatsAppBO>();

            foreach (var itemEntidad in listado)
            {
                RegistroRecuperacionWhatsAppBO objetoBO = Mapper.Map<TRegistroRecuperacionWhatsApp, RegistroRecuperacionWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="idCampaniaGeneralDetalleResponsable">Id del responsable la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalleResponsable)</param
        /// <returns>Booleano con true o false</returns>
        public bool ActualizarCompletadoRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            try
            {
                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ActualizarCompletadoRegistroWhatsApp]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdCampaniaGeneralDetalleResponsable = idCampaniaGeneralDetalleResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de peticiones rechazadas por caida del servidor (Servicios 2)
        /// </summary>
        /// <returns>Obtiene la cantidad de solicitudes erradas</returns>
        public int ObtenerCantidadCaidaRecuperacionWhatsApp()
        {
            try
            {
                int cantidadCaida = 0;

                var consultaCantidadServidorInhabilitado = "SELECT Valor FROM mkt.V_ObtenerCantidadCaidaMinuto";
                var resultadoVista = _dapper.FirstOrDefault(consultaCantidadServidorInhabilitado, null);

                if (!string.IsNullOrEmpty(resultadoVista) && !resultadoVista.Contains("[]") && resultadoVista != "null")
                {
                    var cantidadResultante = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoVista);

                    cantidadCaida = cantidadResultante.Valor;
                }

                return cantidadCaida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la cantidad de WhatsApp Preprocesada
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <param name="fechaInicio">Fecha de inicio de la busqueda de envios</param>
        /// <param name="fechaFin">Fecha de finalizacion de la busqueda de envios</param>
        /// <returns>Booleano con true o false</returns>
        public int ObtenerCantidadWhatsAppPreprocesadoRealizado(int idCampaniaGeneralDetalle, int idPersonal, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                int cantidadWhatsAppRealizada = 0;

                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ObtenerCantidadWhatsAppPreprocesado]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdPersonal = idPersonal, FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]") && resultadoSp != "null")
                {
                    var cantidadResultante = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoSp);

                    cantidadWhatsAppRealizada = cantidadResultante.Valor;
                }

                return cantidadWhatsAppRealizada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <param name="idCampaniaGeneralDetalleResponsable">Id del responsable la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalleResponsable)</param
        /// <returns>Booleano con true o false</returns>
        public bool ActualizarFalloRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable)
        {
            try
            {
                var spActualizarCompletadoWhatsApp = "[mkt].[SP_ActualizarFalloRegistroWhatsApp]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spActualizarCompletadoWhatsApp, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdCampaniaGeneralDetalleResponsable = idCampaniaGeneralDetalleResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el estado de completado de WhatsApp
        /// </summary>
        /// <param name="usuario">Usuario responsable</param>
        /// <returns>Booleano con true o false</returns>
        public bool DesactivarCompletadoRegistroWhatsApp(string usuario)
        {
            try
            {
                var spDesactivarCompletadoWhatsApp = "[mkt].[SP_DesactivarHistorialCompletadoRegistroWhatsApp]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spDesactivarCompletadoWhatsApp, new { UsuarioResponsable = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

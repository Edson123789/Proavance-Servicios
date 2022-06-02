using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/WhatsAppConfiguracionEnvioDetalle
    /// Autor: Joao Benavente - Fischer Valdez - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_WhatsAppConfiguracionEnvioDetalle
    /// </summary>
    public class WhatsAppConfiguracionEnvioDetalleRepositorio : BaseRepository<TWhatsAppConfiguracionEnvioDetalle, WhatsAppConfiguracionEnvioDetalleBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionEnvioDetalleRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionEnvioDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionEnvioDetalleBO> GetBy(Expression<Func<TWhatsAppConfiguracionEnvioDetalle, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionEnvioDetalle> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionEnvioDetalleBO> listadoBO = new List<WhatsAppConfiguracionEnvioDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionEnvioDetalleBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioDetalle, WhatsAppConfiguracionEnvioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionEnvioDetalleBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionEnvioDetalle entidad = base.FirstById(id);
                WhatsAppConfiguracionEnvioDetalleBO objetoBO = new WhatsAppConfiguracionEnvioDetalleBO();
                Mapper.Map<TWhatsAppConfiguracionEnvioDetalle, WhatsAppConfiguracionEnvioDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionEnvioDetalleBO FirstBy(Expression<Func<TWhatsAppConfiguracionEnvioDetalle, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionEnvioDetalle entidad = base.FirstBy(filter);
                WhatsAppConfiguracionEnvioDetalleBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioDetalle, WhatsAppConfiguracionEnvioDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionEnvioDetalleBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionEnvioDetalleBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionEnvioDetalle entidad, WhatsAppConfiguracionEnvioDetalleBO objetoBO)
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

        private TWhatsAppConfiguracionEnvioDetalle MapeoEntidad(WhatsAppConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionEnvioDetalle entidad = new TWhatsAppConfiguracionEnvioDetalle();
                entidad = Mapper.Map<WhatsAppConfiguracionEnvioDetalleBO, TWhatsAppConfiguracionEnvioDetalle>(objetoBO,
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
        /// Obtiene los mensajes de whatsapp respondidos, previas salidas mediante el modulo de conjuntolista y actividadcabecera
        /// </summary>
        /// <param name="filtro">Objeto de tipo FiltroMensajesWhatsAppRespondidosDTO</param>
        /// <returns>Objeto de clase MensajesWhatsAppRespondidosDTO</returns>
        public MensajesWhatsAppRespondidosDTO ObtenerMensajesWhatsAppRespondidos(FiltroMensajesWhatsAppRespondidosDTO filtro)
        {
            try
            {
                string nombreCentroCosto = string.Empty;
                string programa = string.Empty;
                string nombreAlumno = string.Empty;
                string celular = string.Empty;
                string asesor = string.Empty;
                string ultimoMensaje = string.Empty;
                DateTime fecha = DateTime.Now;
                bool filtroFecha = false;
                bool seleccionada = false;
                bool noSeleccionada = false;

                if (filtro.ListaEstadoCreacionOportunidad.Count > 0)
                {
                    foreach (var item in filtro.ListaEstadoCreacionOportunidad)
                    {
                        if (item.Valor == 2) seleccionada = true;
                        else if (item.Valor == 1) noSeleccionada = true;
                    }
                }

                if (filtro.FiltroKendo != null)
                {
                    foreach (var item in filtro.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "NombreCentroCosto":
                                nombreCentroCosto = item.Value;
                                break;
                            case "NombrePrograma":
                                programa = item.Value;
                                break;
                            case "NombreAlumno":
                                nombreAlumno = item.Value;
                                break;
                            case "Celular":
                                celular = item.Value;
                                break;
                            case "NombrePersonal":
                                asesor = item.Value;
                                break;
                            case "UltimoMensajeRecibido":
                                ultimoMensaje = item.Value;
                                break;
                            case "FechaUltimoMensaje":
                                filtroFecha = true;
                                fecha = Convert.ToDateTime(item.Value);
                                break;
                        }
                    }
                }

                var correosGmail = _dapper.QuerySPDapper("mkt.SP_ObtenerMensajesWhatsAppRespondidos",
                    new
                    {
                        Skip = filtro.Skip,
                        Take = filtro.Take,
                        NombreCentroCosto = nombreCentroCosto,
                        Programa = programa,
                        NombreAlumno = nombreAlumno,
                        Celular = celular,
                        Asesor = asesor,
                        Fecha = fecha,
                        UltimoMensajeRecibido = ultimoMensaje,
                        FiltroFecha = filtroFecha,
                        Seleccionada = seleccionada,
                        NoSeleccionada = noSeleccionada,
                        BandejaRespondidos = filtro.BandejaRespondidos
                    });

                MensajesWhatsAppRespondidosDTO mensajesWhatsAppRespondidosDTO = new MensajesWhatsAppRespondidosDTO();
                if (!string.IsNullOrEmpty(correosGmail) && correosGmail != "[]")
                {
                    mensajesWhatsAppRespondidosDTO.ListaMensajesWhatsAppRespondidos = JsonConvert.DeserializeObject<List<ResumenMensajesWhatsAppRespondidosDTO>>(correosGmail);

                    var cantidad = _dapper.QuerySPFirstOrDefault("mkt.SP_ObtenerMensajesWhatsAppRespondidosCantidad",
                    new
                    {
                        Skip = filtro.Skip,
                        Take = filtro.Take,
                        NombreCentroCosto = nombreCentroCosto,
                        Programa = programa,
                        NombreAlumno = nombreAlumno,
                        Celular = celular,
                        Asesor = asesor,
                        Fecha = fecha,
                        UltimoMensajeRecibido = ultimoMensaje,
                        FiltroFecha = filtroFecha,
                        Seleccionada = seleccionada,
                        NoSeleccionada = noSeleccionada,
                        BandejaRespondidos = filtro.BandejaRespondidos
                    });
                    var diccionario = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidad);

                    mensajesWhatsAppRespondidosDTO.Total = diccionario.Select(x => x.Value).FirstOrDefault();

                    return mensajesWhatsAppRespondidosDTO;
                }
                mensajesWhatsAppRespondidosDTO.Total = 0;

                return mensajesWhatsAppRespondidosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppConfiguracionEnvioDetalle
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionEnvioDetalleBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppConfiguracionEnvioDetalle(WhatsAppConfiguracionEnvioDetalleBO filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionEnvioDetalle]";

            var query = _dapper.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.IdWhatsAppConfiguracionLogEjecucion,
                filtro.EnviadoCorrectamente,
                filtro.MensajeError,
                filtro.IdConjuntoListaResultado,
                filtro.ConjuntoListaNroEjecucion,
                filtro.Mensaje,
                filtro.WhatsAppId,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return resultado.Valor;
        }
    }
}

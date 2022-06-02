using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/WhatsAppConfiguracionEnvioDetalleOportunidad
    /// Autor: Joao Benavente - Gian Miranda
    /// Fecha: 09/04/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_WhatsAppConfiguracionEnvioDetalleOportunidad
    /// </summary>
    public class WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio : BaseRepository<TWhatsAppConfiguracionEnvioDetalleOportunidad, WhatsAppConfiguracionEnvioDetalleOportunidadBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionEnvioDetalleOportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionEnvioDetalleOportunidadBO> GetBy(Expression<Func<TWhatsAppConfiguracionEnvioDetalleOportunidad, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionEnvioDetalleOportunidad> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionEnvioDetalleOportunidadBO> listadoBO = new List<WhatsAppConfiguracionEnvioDetalleOportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioDetalleOportunidad, WhatsAppConfiguracionEnvioDetalleOportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionEnvioDetalleOportunidadBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionEnvioDetalleOportunidad entidad = base.FirstById(id);
                WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO = new WhatsAppConfiguracionEnvioDetalleOportunidadBO();
                Mapper.Map<TWhatsAppConfiguracionEnvioDetalleOportunidad, WhatsAppConfiguracionEnvioDetalleOportunidadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionEnvioDetalleOportunidadBO FirstBy(Expression<Func<TWhatsAppConfiguracionEnvioDetalleOportunidad, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionEnvioDetalleOportunidad entidad = base.FirstBy(filter);
                WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvioDetalleOportunidad, WhatsAppConfiguracionEnvioDetalleOportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioDetalleOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionEnvioDetalleOportunidadBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionEnvioDetalleOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionEnvioDetalleOportunidadBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionEnvioDetalleOportunidad entidad, WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO)
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

        private TWhatsAppConfiguracionEnvioDetalleOportunidad MapeoEntidad(WhatsAppConfiguracionEnvioDetalleOportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionEnvioDetalleOportunidad entidad = new TWhatsAppConfiguracionEnvioDetalleOportunidad();
                entidad = Mapper.Map<WhatsAppConfiguracionEnvioDetalleOportunidadBO, TWhatsAppConfiguracionEnvioDetalleOportunidad>(objetoBO,
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
        /// Obtiene una lista de objetos de clase WhatsAppConfiguracionEnvioDetalleOportunidadBO
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario del cual se desea obtener las oportunidades guardadas</param>
        /// <returns>Lista de objetos (WhatsAppConfiguracionEnvioDetalleOportunidadBO)</returns>
        public List<WhatsAppConfiguracionEnvioDetalleOportunidadBO> ObtenerDatosWhatsAppPais(string nombreUsuario)
        {
            try
            {
                List<WhatsAppConfiguracionEnvioDetalleOportunidadBO> resultadoWhatsappPeticion = new List<WhatsAppConfiguracionEnvioDetalleOportunidadBO>();
                var consultaResultadoWhatsApp = @"SELECT Id,
                                                        IdWhatsAppConfiguracionEnvioDetalle,
                                                        IdOportunidad,
                                                        IdCentroCosto,
                                                        IdPersonal,
                                                        Estado,
                                                        FechaCreacion,
                                                        FechaModificacion,
                                                        UsuarioCreacion,
                                                        UsuarioModificacion,
                                                        RowVersion,
                                                        IdMigracion,
                                                        IdCodigoPais
                                                    FROM mkt.V_ObtenerWhatsAppConfiguracionEnvioDetallePais
                                                    WHERE UsuarioCreacion = @UsuarioCreacion";
                var listaRegistros = _dapper.QueryDapper(consultaResultadoWhatsApp, new { UsuarioCreacion = nombreUsuario });
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    resultadoWhatsappPeticion = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionEnvioDetalleOportunidadBO>>(listaRegistros);
                }

                return resultadoWhatsappPeticion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

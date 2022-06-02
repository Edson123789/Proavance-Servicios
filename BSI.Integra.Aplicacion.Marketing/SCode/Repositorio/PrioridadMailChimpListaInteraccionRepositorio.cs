using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class PrioridadMailChimpListaInteraccionRepositorio : BaseRepository<TPrioridadMailChimpListaInteraccion, PrioridadMailChimpListaInteraccionBO>
    {
        #region Metodos Base
        public PrioridadMailChimpListaInteraccionRepositorio() : base()
        {
        }
        public PrioridadMailChimpListaInteraccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PrioridadMailChimpListaInteraccionBO> GetBy(Expression<Func<TPrioridadMailChimpListaInteraccion, bool>> filter)
        {
            IEnumerable<TPrioridadMailChimpListaInteraccion> listado = base.GetBy(filter);
            List<PrioridadMailChimpListaInteraccionBO> listadoBO = new List<PrioridadMailChimpListaInteraccionBO>();
            foreach (var itemEntidad in listado)
            {
                PrioridadMailChimpListaInteraccionBO objetoBO = Mapper.Map<TPrioridadMailChimpListaInteraccion, PrioridadMailChimpListaInteraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PrioridadMailChimpListaInteraccionBO FirstById(int id)
        {
            try
            {
                TPrioridadMailChimpListaInteraccion entidad = base.FirstById(id);
                PrioridadMailChimpListaInteraccionBO objetoBO = new PrioridadMailChimpListaInteraccionBO();
                Mapper.Map<TPrioridadMailChimpListaInteraccion, PrioridadMailChimpListaInteraccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PrioridadMailChimpListaInteraccionBO FirstBy(Expression<Func<TPrioridadMailChimpListaInteraccion, bool>> filter)
        {
            try
            {
                TPrioridadMailChimpListaInteraccion entidad = base.FirstBy(filter);
                PrioridadMailChimpListaInteraccionBO objetoBO = Mapper.Map<TPrioridadMailChimpListaInteraccion, PrioridadMailChimpListaInteraccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PrioridadMailChimpListaInteraccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPrioridadMailChimpListaInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PrioridadMailChimpListaInteraccionBO> listadoBO)
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

        public bool Update(PrioridadMailChimpListaInteraccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPrioridadMailChimpListaInteraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PrioridadMailChimpListaInteraccionBO> listadoBO)
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
        private void AsignacionId(TPrioridadMailChimpListaInteraccion entidad, PrioridadMailChimpListaInteraccionBO objetoBO)
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

        private TPrioridadMailChimpListaInteraccion MapeoEntidad(PrioridadMailChimpListaInteraccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPrioridadMailChimpListaInteraccion entidad = new TPrioridadMailChimpListaInteraccion();
                entidad = Mapper.Map<PrioridadMailChimpListaInteraccionBO, TPrioridadMailChimpListaInteraccion>(objetoBO,
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
        /// Obtiene la lista de Interacciones de lista por un rango de fechas
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<PrioridadMailChimpListaInteraccionBO> PrioridadesListaInteraccionesPorRango(DateTime fechaIninio, DateTime fechaFin)
        {
            try
            {
                List<PrioridadMailChimpListaInteraccionBO> listaInteraccion = new List<PrioridadMailChimpListaInteraccionBO>();
                string _query = string.Empty;
                _query = "SELECT Id,IdPrioridadMailchimpLista,ClickRate,Clicks,OpenRate,SubscriberClicks," +
                    "Opens,UniqueOpens,MemberCount,CleanedCount,EmailSend,Estado,UsuarioCreacion,UsuarioModificacion," +
                    "RowVersion,IdMigracion " +
                    "FROM mkt.V_TInteraccionesLista_Mailchimp " +
                    "WHERE Estado = 1 " +
                    "and EstadoLista = 1 " +
                    "and Enviado = true  " +
                    "and IdCampaniaMailchimp != null " +
                    "and IdListaMailchimp != null  " +
                    "and FechaEnvio != null " +
                    "FechaEnvio > " + fechaIninio.ToString() + " " +
                    "and FechaEnvio <= " + fechaFin.ToString();
                var listaRegistros = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    listaInteraccion = JsonConvert.DeserializeObject<List<PrioridadMailChimpListaInteraccionBO>>(listaRegistros);
                }
                return listaInteraccion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Interacciones de lista por un rango cinco dias anteriores
        /// </summary>
        /// <returns></returns>
        public List<PrioridadMailChimpListaInteraccionBO> PrioridadesListaInteraccionesCincoDias()
        {
            try
            {
                DateTime fecha5dias_antes = DateTime.Now.Date.AddDays(-5);
                List<PrioridadMailChimpListaInteraccionBO> listaInteraccion = new List<PrioridadMailChimpListaInteraccionBO>();
                string _query = string.Empty;
                _query = "SELECT Id,IdPrioridadMailchimpLista,ClickRate,Clicks,OpenRate,SubscriberClicks," +
                    "Opens,UniqueOpens,MemberCount,CleanedCount,EmailSend,Estado,UsuarioCreacion,UsuarioModificacion," +
                    "RowVersion,IdMigracion " +
                    "FROM mkt.V_TInteraccionesLista_Mailchimp " +
                    "WHERE Estado = 1 " +
                    "and EstadoLista = 1 " +
                    "and Enviado = true  " +
                    "and IdCampaniaMailchimp != null " +
                    "and IdListaMailchimp != null  " +
                    "and FechaEnvio != null " +
                    "FechaEnvio > " + fecha5dias_antes;
                var listaRegistros = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(listaRegistros) && !listaRegistros.Contains("[]"))
                {
                    listaInteraccion = JsonConvert.DeserializeObject<List<PrioridadMailChimpListaInteraccionBO>>(listaRegistros);
                }
                return listaInteraccion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

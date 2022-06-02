using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/SmsMensajeEnviado
    /// Autor: Gian Miranda
    /// Fecha: 31/12/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_SmsMensajeEnviado
    /// </summary>

    public class SmsMensajeEnviadoRepositorio : BaseRepository<TSmsMensajeEnviado, SmsMensajeEnviadoBO>
    {
        #region Metodos Base
        public SmsMensajeEnviadoRepositorio() : base()
        {
        }
        public SmsMensajeEnviadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SmsMensajeEnviadoBO> GetBy(Expression<Func<TSmsMensajeEnviado, bool>> filter)
        {
            IEnumerable<TSmsMensajeEnviado> listado = base.GetBy(filter);
            List<SmsMensajeEnviadoBO> listadoBO = new List<SmsMensajeEnviadoBO>();
            foreach (var itemEntidad in listado)
            {
                SmsMensajeEnviadoBO objetoBO = Mapper.Map<TSmsMensajeEnviado, SmsMensajeEnviadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SmsMensajeEnviadoBO FirstById(int id)
        {
            try
            {
                TSmsMensajeEnviado entidad = base.FirstById(id);
                SmsMensajeEnviadoBO objetoBO = new SmsMensajeEnviadoBO();
                Mapper.Map<TSmsMensajeEnviado, SmsMensajeEnviadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SmsMensajeEnviadoBO FirstBy(Expression<Func<TSmsMensajeEnviado, bool>> filter)
        {
            try
            {
                TSmsMensajeEnviado entidad = base.FirstBy(filter);
                SmsMensajeEnviadoBO objetoBO = Mapper.Map<TSmsMensajeEnviado, SmsMensajeEnviadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SmsMensajeEnviadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSmsMensajeEnviado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SmsMensajeEnviadoBO> listadoBO)
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

        public bool Update(SmsMensajeEnviadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSmsMensajeEnviado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SmsMensajeEnviadoBO> listadoBO)
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
        private void AsignacionId(TSmsMensajeEnviado entidad, SmsMensajeEnviadoBO objetoBO)
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

        private TSmsMensajeEnviado MapeoEntidad(SmsMensajeEnviadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSmsMensajeEnviado entidad = new TSmsMensajeEnviado();
                entidad = Mapper.Map<SmsMensajeEnviadoBO, TSmsMensajeEnviado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SmsMensajeEnviadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSmsMensajeEnviado, bool>>> filters, Expression<Func<TSmsMensajeEnviado, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSmsMensajeEnviado> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SmsMensajeEnviadoBO> listadoBO = new List<SmsMensajeEnviadoBO>();

            foreach (var itemEntidad in listado)
            {
                SmsMensajeEnviadoBO objetoBO = Mapper.Map<TSmsMensajeEnviado, SmsMensajeEnviadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de ultimos mensajes recibidos
        /// </summary>
        /// <param name="idPersonal">Id del personal (PK de la tabla gp.T_Personal)</param>
        /// <returns>Lista de objetos de clase SmsMensajesDTO</returns>
        public List<SmsMensajesDTO> ListaUltimoMensajeSmsRecibido(int idPersonal, int idAlumno)
        {
            try
            {
                List<SmsMensajesDTO> listaResultadoReporte = new List<SmsMensajesDTO>();
                string spReporte = "[mkt].[SP_ObtenerHistorialUltimosMensajesSMS]";

                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdPersonal = idPersonal, IdAlumno = idAlumno });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<SmsMensajesDTO>>(resultadoReporte);
                }

                return listaResultadoReporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las plantillas de SMS
        /// </summary>
        /// <returns>Lista de objetos de clase PlantillaSmsComboDTO</returns>
        public List<PlantillaSmsComboDTO> ObtenerPlantillaSmsHabilitada()
        {
            try
            {
                List<PlantillaSmsComboDTO> listaResultadoPlantilla = new List<PlantillaSmsComboDTO>();
                string query = "SELECT IdPlantilla, NombrePlantilla FROM mkt.V_PlantillaSmsHabilitada";

                string resultado = _dapper.QueryDapper(query, null);

                if (resultado != "null" && !string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    listaResultadoPlantilla = JsonConvert.DeserializeObject<List<PlantillaSmsComboDTO>>(resultado);
                }

                return listaResultadoPlantilla;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de chat de una persona en especifico
        /// </summary>
        /// <param name="celular">Celular a detectar el chat completo</param>
        /// <returns>Lista de objetos de clase SmsHistorialMensajeIndividualDTO</returns>
        public List<SmsHistorialMensajeIndividualDTO> ObtenerHistorialSmsPorCelular(string celular)
        {
            try
            {
                List<SmsHistorialMensajeIndividualDTO> listaResultadoReporte = new List<SmsHistorialMensajeIndividualDTO>();
                string spReporte = "[mkt].[SP_ObtenerHistorialSmsPorCelular]";

                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { Celular = celular });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<SmsHistorialMensajeIndividualDTO>>(resultadoReporte);
                }

                return listaResultadoReporte;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

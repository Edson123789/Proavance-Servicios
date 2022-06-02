using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/AnuncioFacebookMetrica
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_AnuncioFacebookMetrica
    /// </summary>
    public class AnuncioFacebookMetricaRepositorio : BaseRepository<TAnuncioFacebookMetrica, AnuncioFacebookMetricaBO>
    {
        #region Metodos Base
        public AnuncioFacebookMetricaRepositorio() : base()
        {
        }
        public AnuncioFacebookMetricaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AnuncioFacebookMetricaBO> GetBy(Expression<Func<TAnuncioFacebookMetrica, bool>> filter)
        {
            IEnumerable<TAnuncioFacebookMetrica> listado = base.GetBy(filter);
            List<AnuncioFacebookMetricaBO> listadoBO = new List<AnuncioFacebookMetricaBO>();
            foreach (var itemEntidad in listado)
            {
                AnuncioFacebookMetricaBO objetoBO = Mapper.Map<TAnuncioFacebookMetrica, AnuncioFacebookMetricaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AnuncioFacebookMetricaBO FirstById(int id)
        {
            try
            {
                TAnuncioFacebookMetrica entidad = base.FirstById(id);
                AnuncioFacebookMetricaBO objetoBO = new AnuncioFacebookMetricaBO();
                Mapper.Map<TAnuncioFacebookMetrica, AnuncioFacebookMetricaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AnuncioFacebookMetricaBO FirstBy(Expression<Func<TAnuncioFacebookMetrica, bool>> filter)
        {
            try
            {
                TAnuncioFacebookMetrica entidad = base.FirstBy(filter);
                AnuncioFacebookMetricaBO objetoBO = Mapper.Map<TAnuncioFacebookMetrica, AnuncioFacebookMetricaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AnuncioFacebookMetricaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAnuncioFacebookMetrica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AnuncioFacebookMetricaBO> listadoBO)
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

        public bool Update(AnuncioFacebookMetricaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAnuncioFacebookMetrica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AnuncioFacebookMetricaBO> listadoBO)
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
        private void AsignacionId(TAnuncioFacebookMetrica entidad, AnuncioFacebookMetricaBO objetoBO)
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

        private TAnuncioFacebookMetrica MapeoEntidad(AnuncioFacebookMetricaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAnuncioFacebookMetrica entidad = new TAnuncioFacebookMetrica();
                entidad = Mapper.Map<AnuncioFacebookMetricaBO, TAnuncioFacebookMetrica>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AnuncioFacebookMetricaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TAnuncioFacebookMetrica, bool>>> filters, Expression<Func<TAnuncioFacebookMetrica, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TAnuncioFacebookMetrica> listado = base.GetFiltered(filters, orderBy, ascending);
            List<AnuncioFacebookMetricaBO> listadoBO = new List<AnuncioFacebookMetricaBO>();

            foreach (var itemEntidad in listado)
            {
                AnuncioFacebookMetricaBO objetoBO = Mapper.Map<TAnuncioFacebookMetrica, AnuncioFacebookMetricaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener reporte de anuncio Facebook metrica
        /// </summary>
        /// <param name="idAreaCapacitacion">Objeto de clase ParametroReporteAnuncioFacebookMetricaDTO</param>
        /// <returns>Lista de objetos de clase ReporteAnuncioFacebookMetricaDTO</returns>
        public List<ReporteAnuncioFacebookMetricaDTO> ObtenerReporteAnuncioFacebookMetrica(int? idAreaCapacitacion)
        {
            try
            {
                List<ReporteAnuncioFacebookMetricaDTO> listaResultadoReporte = new List<ReporteAnuncioFacebookMetricaDTO>();

                string spReporte = "[mkt].[SP_ObtenerReporteAnuncioFacebookMetrica]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdAreaCapacitacion = idAreaCapacitacion });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]"))
                {
                    listaResultadoReporte = JsonConvert.DeserializeObject<List<ReporteAnuncioFacebookMetricaDTO>>(resultadoReporte);
                }

                return listaResultadoReporte;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener las areas para el reporte de anuncio Facebook metrica basado en grupo de filtro de programas criticos
        /// </summary>
        /// <returns>Lista de objetos de clase AreaAnuncioFacebookMetricaDTO</returns>
        public List<AreaAnuncioFacebookMetricaDTO> ObtenerComboAreaAnuncioFacebookMetrica()
        {
            try
            {
                List<AreaAnuncioFacebookMetricaDTO> listaArea = new List<AreaAnuncioFacebookMetricaDTO>();

                string consultaArea = "SELECT IdGrupoFiltroProgramaCritico, NombreGrupoFiltroProgramaCritico FROM mkt.V_TGrupoFiltroProgramaCritico_ComboFacebook";
                string resultadoArea = _dapper.QueryDapper(consultaArea, null);

                if (!string.IsNullOrEmpty(resultadoArea) && !resultadoArea.Contains("[]"))
                {
                    listaArea = JsonConvert.DeserializeObject<List<AreaAnuncioFacebookMetricaDTO>>(resultadoArea);
                }

                return listaArea;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la ultima modificacion
        /// </summary>
        /// <returns>String</returns>
        public string ObtenerUltimaModificacion()
        {
            try
            {
                ValorStringDTO resultadoCadena = new ValorStringDTO();

                string consultaFechaModificacion = "SELECT Valor FROM mkt.V_ObtenerUltimaModificacionMetricaFacebook";
                string resultadoFechaModificacion = _dapper.FirstOrDefault(consultaFechaModificacion, null);

                if (!string.IsNullOrEmpty(resultadoFechaModificacion) && !resultadoFechaModificacion.Contains("[]"))
                {
                    resultadoCadena = JsonConvert.DeserializeObject<ValorStringDTO>(resultadoFechaModificacion);
                }

                return resultadoCadena.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 15/06/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado a 0 de una fecha especifica
        /// </summary>
        /// <returns>Bool</returns>
        public bool EliminarDatosPorFecha(DateTime fechaConsulta, string usuario)
        {
            try
            {
                string spReporte = "[mkt].[SP_EliminarFacebookMetricaPorFecha]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { FechaConsulta = fechaConsulta, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

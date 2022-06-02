using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class GastoFinancieroCronogramaRepositorio : BaseRepository<TGastoFinancieroCronograma, GastoFinancieroCronogramaBO>
    {
        #region Metodos Base
        public GastoFinancieroCronogramaRepositorio() : base()
        {
        }
        public GastoFinancieroCronogramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GastoFinancieroCronogramaBO> GetBy(Expression<Func<TGastoFinancieroCronograma, bool>> filter)
        {
            IEnumerable<TGastoFinancieroCronograma> listado = base.GetBy(filter);
            List<GastoFinancieroCronogramaBO> listadoBO = new List<GastoFinancieroCronogramaBO>();
            foreach (var itemEntidad in listado)
            {
                GastoFinancieroCronogramaBO objetoBO = Mapper.Map<TGastoFinancieroCronograma, GastoFinancieroCronogramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GastoFinancieroCronogramaBO FirstById(int id)
        {
            try
            {
                TGastoFinancieroCronograma entidad = base.FirstById(id);
                GastoFinancieroCronogramaBO objetoBO = new GastoFinancieroCronogramaBO();
                Mapper.Map<TGastoFinancieroCronograma, GastoFinancieroCronogramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GastoFinancieroCronogramaBO FirstBy(Expression<Func<TGastoFinancieroCronograma, bool>> filter)
        {
            try
            {
                TGastoFinancieroCronograma entidad = base.FirstBy(filter);
                GastoFinancieroCronogramaBO objetoBO = Mapper.Map<TGastoFinancieroCronograma, GastoFinancieroCronogramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GastoFinancieroCronogramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGastoFinancieroCronograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GastoFinancieroCronogramaBO> listadoBO)
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

        public bool Update(GastoFinancieroCronogramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGastoFinancieroCronograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GastoFinancieroCronogramaBO> listadoBO)
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
        private void AsignacionId(TGastoFinancieroCronograma entidad, GastoFinancieroCronogramaBO objetoBO)
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

        private TGastoFinancieroCronograma MapeoEntidad(GastoFinancieroCronogramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGastoFinancieroCronograma entidad = new TGastoFinancieroCronograma();
                entidad = Mapper.Map<GastoFinancieroCronogramaBO, TGastoFinancieroCronograma>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                if (objetoBO.GastoFinancieroCronogramaDetalle != null && objetoBO.GastoFinancieroCronogramaDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.GastoFinancieroCronogramaDetalle)
                    {
                        TGastoFinancieroCronogramaDetalle entidadHijo = new TGastoFinancieroCronogramaDetalle();
                        entidadHijo = Mapper.Map<GastoFinancieroCronogramaDetalleBO, TGastoFinancieroCronogramaDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TGastoFinancieroCronogramaDetalle.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion


        /// <summary>
        /// Obtiene [Id, Nombre, IdEntidadFinanciera, NombreEntidadFinanciera, IdMoneda, NombreMoneda, CapitalTotal, InteresTotal, FechaInicio] de las GastoFinancieroCronogramaes existentes en una lista 
        /// </summary>
        /// <returns></returns>
        public List<GastoFinancieroCronogramaDatosDTO> ObtenerGastoFinancieroCronograma()
        {
            try
            {
                List<GastoFinancieroCronogramaDatosDTO> GastoFinancieroCronogramas = new List<GastoFinancieroCronogramaDatosDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdEntidadFinanciera, NombreEntidadFinanciera, IdMoneda, NombreMoneda, CapitalTotal, InteresTotal, FechaInicio FROM fin.V_ObtenerGastoFinancieroCronograma WHERE Estado = 1";
                var GastoFinancieroCronogramasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(GastoFinancieroCronogramasDB) && !GastoFinancieroCronogramasDB.Contains("[]"))
                {
                    GastoFinancieroCronogramas = JsonConvert.DeserializeObject<List<GastoFinancieroCronogramaDatosDTO>>(GastoFinancieroCronogramasDB);
                }
                return GastoFinancieroCronogramas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Genera el reporte de Prestamos segun los filtros Dados
        /// </summary>
        /// <returns></returns>
        public List<ReporteDePrestamoDTO> ObtenerReportePrestamos(int IdEntidadFinanciera, int IdPrestamo)
        {
            try
            {
                List<ReporteDePrestamoDTO> GastoFinancieroCronogramas = new List<ReporteDePrestamoDTO>();
                var _query = string.Empty;
                _query = "SELECT NumeroCuota, FechaVencimientoCuota, CapitalCuota, InteresCuota, TotalCuota, NombreMoneda FROM fin.V_ObtenerGastoFinancieroParaReportePrestamos WHERE Estado = 1 AND IdEntidadFinanciera=" +IdEntidadFinanciera+ "  and IdPrestamo=" + IdPrestamo + "  ORDER BY NumeroCuota";
                var GastoFinancieroCronogramasDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(GastoFinancieroCronogramasDB) && !GastoFinancieroCronogramasDB.Contains("[]"))
                {
                    GastoFinancieroCronogramas = JsonConvert.DeserializeObject<List<ReporteDePrestamoDTO>>(GastoFinancieroCronogramasDB);
                }
                return GastoFinancieroCronogramas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene la Lista de Prestamos realizados registrados en GastoFinancieroCronograma
        /// </summary>
        /// <returns></returns>
        public List<PrestamoFiltroDTO> ObtenerListaPrestamosFiltro()
        {
            try
            {
                List<PrestamoFiltroDTO> Prestamos = new List<PrestamoFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdEntidadFinanciera FROM fin.V_ObtenerPrestamosFiltro";
                var PrestamosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(PrestamosDB) && !PrestamosDB.Contains("[]"))
                {
                    Prestamos = JsonConvert.DeserializeObject<List<PrestamoFiltroDTO>>(PrestamosDB);
                }
                return Prestamos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/Moneda
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Obtiene las monedas usando diferentes filtros y funciones
    /// </summary>
    public class MonedaRepositorio : BaseRepository<TMoneda, MonedaBO>
    {
        #region Metodos Base
        public MonedaRepositorio() : base()
        {
        }
        public MonedaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MonedaBO> GetBy(Expression<Func<TMoneda, bool>> filter)
        {
            IEnumerable<TMoneda> listado = base.GetBy(filter);
            List<MonedaBO> listadoBO = new List<MonedaBO>();
            foreach (var itemEntidad in listado)
            {
                MonedaBO objetoBO = Mapper.Map<TMoneda, MonedaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MonedaBO FirstById(int id)
        {
            try
            {
                TMoneda entidad = base.FirstById(id);
                MonedaBO objetoBO = new MonedaBO();
                Mapper.Map<TMoneda, MonedaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MonedaBO FirstBy(Expression<Func<TMoneda, bool>> filter)
        {
            try
            {
                TMoneda entidad = base.FirstBy(filter);
                MonedaBO objetoBO = Mapper.Map<TMoneda, MonedaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MonedaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMoneda entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MonedaBO> listadoBO)
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

        public bool Update(MonedaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMoneda entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MonedaBO> listadoBO)
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
        private void AsignacionId(TMoneda entidad, MonedaBO objetoBO)
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

        private TMoneda MapeoEntidad(MonedaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMoneda entidad = new TMoneda();
                entidad = Mapper.Map<MonedaBO, TMoneda>(objetoBO,
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

        /// Repositorio: MonedaRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>
        /// <param name="id">Id de moneda </param>
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public MonedaCostoTotalConDescuentoDTO ObtenerMonedaPorId(int id)
        {
            try
            {
                string querymoneda = "SELECT Id,NombrePlural,Simbolo FROM pla.V_TMoneda_ObtenercamposDocumento WHERE Id=@Id AND Estado=1";
                var respuesta = _dapper.FirstOrDefault(querymoneda, new { Id = id });
                return JsonConvert.DeserializeObject<MonedaCostoTotalConDescuentoDTO>(respuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: MonedaRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene Moneda por Id
        /// </summary>        
        /// <returns> MonedaCostoTotalConDescuentoDTO </returns>
        public List<MonedaCostoTotalConDescuentoDTO> ObtenerMonedaTodo()
        {
            try
            {
                List<MonedaCostoTotalConDescuentoDTO> items = new List<MonedaCostoTotalConDescuentoDTO>();
                string querymoneda = "SELECT Id,NombrePlural,Simbolo FROM pla.V_TMoneda_ObtenercamposDocumento WHERE Estado=1";
                var respuesta = _dapper.QueryDapper(querymoneda, new { });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<MonedaCostoTotalConDescuentoDTO>>(respuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene las monedas(activos) con campos Id, Nombre, IdPais  registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<MonedaPaisFiltroDTO> ObtenerMonedaPais()
        {
            try
            {
                List<MonedaPaisFiltroDTO> items = new List<MonedaPaisFiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre,IdPais,NombreCorto,NombrePlural FROM pla.V_TMonedaPais_Filtro WHERE Estado=1";
                var query = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<MonedaPaisFiltroDTO>>(query);
                }
                return items;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de los monedas para ser usadas en combobox (obtiene los nombres plurales)
        /// </summary>
        /// <returns></returns>
		public List<FiltroGenericoDTO> ObtenerFiltroMoneda()
		{
			try
			{
				return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.NombrePlural }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene una lista de los monedas para ser usadas en combobox (obtiene los nombres en singular)
        /// </summary>
        /// <returns></returns>
		public List<FiltroGenericoDTO> ObtenerFiltroMonedaSingular()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MonedaDTO> ObtenerFiltroMonedaId()
        {
            try
            {
                List<MonedaDTO> moneda = new List<MonedaDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre  FROM [ope].[V_MonedaNombrePlural] ";
                var query = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    moneda = JsonConvert.DeserializeObject<List<MonedaDTO>>(query);
                }
                return moneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Nombre del desarrollador
        /// Fecha: día/mes/año
        /// Version: 1.0
        /// <summary>
        /// Descripción o resumen corta y precisa
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo</param>
        /// <returns>Tipo de objeto que retorna la función</returns>
        public CodigoMonedaDTO ObtenerMonedaPorAlumno(int idAlumno)
        {
            try
            {
                CodigoMonedaDTO moneda = new CodigoMonedaDTO();
                string _query = string.Empty;
                _query = "SELECT * FROM com.V_ObtenerMonedaAlumno WHERE Id=@Id";
                var query = _dapper.FirstOrDefault(_query, new {Id=idAlumno });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    moneda = JsonConvert.DeserializeObject<CodigoMonedaDTO>(query);
                }
                return moneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

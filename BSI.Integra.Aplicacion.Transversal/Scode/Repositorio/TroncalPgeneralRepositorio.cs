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
    public class TroncalPgeneralRepositorio : BaseRepository<TTroncalPgeneral, TroncalPgeneralBO>
    {
        #region Metodos Base
        public TroncalPgeneralRepositorio() : base()
        {
        }
        public TroncalPgeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TroncalPgeneralBO> GetBy(Expression<Func<TTroncalPgeneral, bool>> filter)
        {
            IEnumerable<TTroncalPgeneral> listado = base.GetBy(filter);
            List<TroncalPgeneralBO> listadoBO = new List<TroncalPgeneralBO>();
            foreach (var itemEntidad in listado)
            {
                TroncalPgeneralBO objetoBO = Mapper.Map<TTroncalPgeneral, TroncalPgeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TroncalPgeneralBO FirstById(int id)
        {
            try
            {
                TTroncalPgeneral entidad = base.FirstById(id);
                TroncalPgeneralBO objetoBO = new TroncalPgeneralBO();
                Mapper.Map<TTroncalPgeneral, TroncalPgeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TroncalPgeneralBO FirstBy(Expression<Func<TTroncalPgeneral, bool>> filter)
        {
            try
            {
                TTroncalPgeneral entidad = base.FirstBy(filter);
                TroncalPgeneralBO objetoBO = Mapper.Map<TTroncalPgeneral, TroncalPgeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TroncalPgeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTroncalPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TroncalPgeneralBO> listadoBO)
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

        public bool Update(TroncalPgeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTroncalPgeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TroncalPgeneralBO> listadoBO)
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
        private void AsignacionId(TTroncalPgeneral entidad, TroncalPgeneralBO objetoBO)
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

        private TTroncalPgeneral MapeoEntidad(TroncalPgeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTroncalPgeneral entidad = new TTroncalPgeneral();
                entidad = Mapper.Map<TroncalPgeneralBO, TTroncalPgeneral>(objetoBO,
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
		/// Obtiene Lista de codigo de ciudades
		/// </summary>
		/// <returns></returns>
		public List<CiudadTroncalPaisFiltroDTO> ObtenerTroncalCiudadFiltro()
		{
			try
			{
				List<CiudadTroncalPaisFiltroDTO> troncalFiltro = new List<CiudadTroncalPaisFiltroDTO>();
				string _queryTroncal = string.Empty;
				_queryTroncal = "SELECT CodigoCiudad,NombreCiudad,IdPais FROM pla.V_TSede_Filtro WHERE EstadoSede=1 and EstadoTroncal=1";
				var queryTroncal = _dapper.QueryDapper(_queryTroncal, null);
				if (!string.IsNullOrEmpty(queryTroncal) && !queryTroncal.Contains("[]"))
				{
					troncalFiltro = JsonConvert.DeserializeObject<List<CiudadTroncalPaisFiltroDTO>>(queryTroncal);
				}
				return troncalFiltro;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		/// <summary>
		/// Obtiene Lista de codigo de ciudades
		/// </summary>
		/// <returns></returns>
		public List<CiudadTroncalPaisFiltroDTO> ObtenerCiudadPorFeriado(int idCiudad, int IdPais)
		{
			try
			{
				var ciudades = ObtenerTroncalCiudadFiltro();
				var ciudad = ciudades.Where(x => x.CodigoCiudad == idCiudad).FirstOrDefault();
				var listaciudades = ciudades.Where(x => x.IdPais == IdPais).ToList();

				return (listaciudades);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		/// <summary>
		/// Obtiene lista de todos los troncales ciudad
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerTroncalCiudad()
		{
			try
			{
				string _queryTroncal = string.Empty;
				_queryTroncal = "Select Id,Nombre From pla.V_TTroncalCiudad_IdNombre  Where Estado=1";
				var CentroCostoTroncal = _dapper.QueryDapper(_queryTroncal, null);
				return JsonConvert.DeserializeObject<List<FiltroDTO>>(CentroCostoTroncal);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene lista de P general por idSubArea
		/// </summary>
		/// <param name="idSubArea"></param>
		/// <returns></returns>
		public List<AreaCentroCostoDTO> ObtenerTroncalPgeneralPorSubArea(int idSubArea)
        {
            try
            {
                string _queryTroncalSubArea = string.Empty;
                _queryTroncalSubArea = "Select Id,Nombre,Codigo From pla.V_TTroncalPgeneral_PorIdSubArea Where Estado=1 and IdSubArea=@IdSubArea";
                var TroncalSubArea = _dapper.QueryDapper(_queryTroncalSubArea, new { IdSubArea = idSubArea });
                return JsonConvert.DeserializeObject<List<AreaCentroCostoDTO>>(TroncalSubArea);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<LocacionTroncalDTO> ObtenerListaLocacionTroncal()
		{
			try
			{
				var _query = "Select Id, Nombre, IdCiudad, CodigoBS, DenominacionBS from conf.V_ObtenerLocacionTroncal where Estado = 1";
				var LocacionTroncal = _dapper.QueryDapper(_query,null);
				return JsonConvert.DeserializeObject<List<LocacionTroncalDTO>>(LocacionTroncal);
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene todos los registros de TroncalPgeneral con los campos de Id, Nombre y IdSubArea.
        /// </summary>
        /// <returns></returns>
        public List<TroncalPgeneralFiltroDTO> ObtenerTroncalPgeneralFiltro()
        {
            try
            {
                var lista = GetBy(x => true, y => new TroncalPgeneralFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdSubArea = y.IdSubArea,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Se obtiene los registros de TroncalPGeneral para ser filtrados 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<TroncalPGeneralIntegraDTO> ObtenerTodoPGeneralIntegra(string nombre)
        {
            try
            {
                List<TroncalPGeneralIntegraDTO> obtenerFiltroNombre = new List<TroncalPGeneralIntegraDTO>();
                var _query = "SELECT  Id, Nombre, Codigo, IdTroncalPartner, Duracion, IdArea, IdSubArea, IdCategoria, NombreCategoria FROM pla.V_ObtenerTodoPGeneral WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND EstadoTrocalPGeneral = 1 ";
                var obtenerFiltroNombreDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(obtenerFiltroNombreDB) && !obtenerFiltroNombreDB.Contains("[]"))
                {
                    obtenerFiltroNombre = JsonConvert.DeserializeObject<List<TroncalPGeneralIntegraDTO>>(obtenerFiltroNombreDB);
                }
                return obtenerFiltroNombre;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

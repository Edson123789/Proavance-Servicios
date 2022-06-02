using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.Util.Reports.v201809;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util.Reports;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using System.Data.Entity.Validation;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class ConjuntoAnuncioRepositorio : BaseRepository<TConjuntoAnuncio, ConjuntoAnuncioBO>
    {
        #region Metodos Base
        public ConjuntoAnuncioRepositorio() : base()
        {
        }
        public ConjuntoAnuncioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoAnuncioBO> GetBy(Expression<Func<TConjuntoAnuncio, bool>> filter)
        {
            IEnumerable<TConjuntoAnuncio> listado = base.GetBy(filter);
            List<ConjuntoAnuncioBO> listadoBO = new List<ConjuntoAnuncioBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoAnuncioBO objetoBO = Mapper.Map<TConjuntoAnuncio, ConjuntoAnuncioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoAnuncioBO FirstById(int id)
        {
            try
            {
                TConjuntoAnuncio entidad = base.FirstById(id);
                ConjuntoAnuncioBO objetoBO = new ConjuntoAnuncioBO();
                Mapper.Map<TConjuntoAnuncio, ConjuntoAnuncioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoAnuncioBO FirstBy(Expression<Func<TConjuntoAnuncio, bool>> filter)
        {
            try
            {
                TConjuntoAnuncio entidad = base.FirstBy(filter);
                ConjuntoAnuncioBO objetoBO = Mapper.Map<TConjuntoAnuncio, ConjuntoAnuncioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoAnuncioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoAnuncio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoAnuncioBO> listadoBO)
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

        public bool Update(ConjuntoAnuncioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoAnuncio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoAnuncioBO> listadoBO)
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
        private void AsignacionId(TConjuntoAnuncio entidad, ConjuntoAnuncioBO objetoBO)
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

        private TConjuntoAnuncio MapeoEntidad(ConjuntoAnuncioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoAnuncio entidad = new TConjuntoAnuncio();
                entidad = Mapper.Map<ConjuntoAnuncioBO, TConjuntoAnuncio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<ConjuntoAnuncioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConjuntoAnuncio, bool>>> filters, Expression<Func<TConjuntoAnuncio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConjuntoAnuncio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConjuntoAnuncioBO> listadoBO = new List<ConjuntoAnuncioBO>();

            foreach (var itemEntidad in listado)
            {
                ConjuntoAnuncioBO objetoBO = Mapper.Map<TConjuntoAnuncio, ConjuntoAnuncioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene conjunto de anuncios registrados mediante el nombre del anuncio
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public ConjuntoAnuncioDTO obtenerAnuncio(string Nombre)
		{
			try
			{
				string query = "Select Id,Nombre,IdCategoriaOrigen,Origen,IdConjuntoAnuncio_Facebook,FechaCreacionCampania from mkt.V_TConjuntoAnuncio_ObtenerDatos WHERE Estado = 1 and Nombre Like CONCAT('%',@nombre,'%') ";
				var Anuncio = _dapper.FirstOrDefault(query, new { nombre = Nombre });
				return JsonConvert.DeserializeObject<ConjuntoAnuncioDTO>(Anuncio);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message);
			}
		}

		/// <summary>
		/// Obtiene conjunto de anuncios registrados mediante el id de campania integra
		/// </summary>
		/// <param name="Campania"></param>
		/// <returns></returns>
		public ConjuntoAnuncioDTO ObtenerAnuncioPorIdCampaniaIntegra(int? Campania)
		{
			try
			{
				string _query = "Select Id,Nombre,IdCategoriaOrigen,Origen,IdConjuntoAnuncio_Facebook,FechaCreacionCampania, FechaCreacion from mkt.V_TConjuntoAnuncio_ObtenerDatos WHERE Estado = 1 and Id = @campania ";
				var Anuncio = _dapper.FirstOrDefault(_query, new { campania = Campania });

				return JsonConvert.DeserializeObject<ConjuntoAnuncioDTO>(Anuncio);
			}
			catch(Exception Ex)
			{
				throw new Exception(Ex.Message);
			}

		}
   
        /// <summary>
        ///  Obtiene la lista de conjunto Anuncios(activos) registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoAnuncioPanelDTO> ListarConjuntoAnuncios(FiltroPaginadorDTO filtro)
        {
            try
            {
                List<ConjuntoAnuncioPanelDTO> items = new List<ConjuntoAnuncioPanelDTO>();
                //var _query = string.Empty;
                //_query = "SELECT Id, IdCampaniaFacebook, IdProveedor, FechaCreacionCampania, Nombre FROM mkt.V_TConjuntoAnuncioPanel " +
                //    "WHERE Estado = 1 order by Id desc";
                //var pgeneralDB = _dapper.QueryDapper(_query);
                //if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                //{
                //    items = JsonConvert.DeserializeObject<List<ConjuntoAnuncioPanelDTO>>(pgeneralDB);
                //}
                List<Expression<Func<TConjuntoAnuncio, bool>>> filters = new List<Expression<Func<TConjuntoAnuncio, bool>>>();
                var total = 0;
                List<ConjuntoAnuncioBO> lista = new List<ConjuntoAnuncioBO>();
                if (filtro != null && filtro.Take != 0 )
                {
                    if((filtro.FiltroKendo != null && filtro.FiltroKendo.Filters.Count > 0))
                    {
                        // Creamos la Lista de filtros
                        foreach (var filterGrid in filtro.FiltroKendo.Filters)
                        {
                            switch (filterGrid.Field)
                            {
                                case "Nombre":
                                    filters.Add(o => o.Nombre.Contains(filterGrid.Value));
                                    break;
                                case "IdCampaniaFacebook":
                                    filters.Add(o => o.IdConjuntoAnuncioFacebook.Contains(filterGrid.Value));
                                    break;
                                case "FechaCreacionCampania":
                                    filters.Add(o => o.FechaCreacionCampania.ToString().Contains(filterGrid.Value));
                                    break;
                                case "Id":
                                    filters.Add(o => o.Id.ToString().Contains(filterGrid.Value));
                                    break;
                                default:
                                    filters.Add(o => true);
                                    break;
                            }
                        }
                        
                    }
                    lista = GetFiltered(filters, p => p.Id, false).ToList();
                    total = lista.Count();
                    lista = lista.Skip(filtro.Skip).Take(filtro.Take).ToList();
                }
                else
                {
                    lista = GetBy(o => true).OrderByDescending(x => x.Id).ToList();
                    total = lista.Count();
                }
                items = lista.Select(x => new ConjuntoAnuncioPanelDTO
                {
                    Id = x.Id,
                    IdCampaniaFacebook = x.IdConjuntoAnuncioFacebook,
                    IdProveedor = x.IdCategoriaOrigen.Value,
                    FechaCreacionCampania = x.FechaCreacionCampania,
                    Nombre = x.Nombre,
                    Total = total.ToString()
                }).ToList();

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Inserta el conjunto de anuncion en v3
        /// </summary>
        /// <returns></returns>
        public int InsertarConjuntoAnuncioV3(int idConjuntoAnuncio)
        {
            try
            {
                int value = 0;
                var query = _dapper.QuerySPFirstOrDefault("mkt.SP_InsertarConjuntoAnuncioV3", new { IdConjuntoAnuncio = idConjuntoAnuncio });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    var item = JsonConvert.DeserializeObject<Dictionary<string, int>>(query);
                    value = item.Select(x => x.Value).FirstOrDefault();
                }
                return value;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Actualiza el conjunto de anuncion en v3
        /// </summary>
        /// <returns></returns>
        public int ActualizarConjuntoAnuncioV3(int idConjuntoAnuncio)
        {
            try
            {
                int value = 0;
                var query = _dapper.QuerySPFirstOrDefault("mkt.SP_ActualizarConjuntoAnuncioV3", new { IdConjuntoAnuncio = idConjuntoAnuncio});
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    var item = JsonConvert.DeserializeObject<Dictionary<string, int>>(query);
                    value = item.Select(x => x.Value).FirstOrDefault();
                }
                return value;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        /// Obtiene datos de un registro determinado de mkt.T_ConjuntoAnuncio
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ConjuntoAnuncioDTO> ObtenerDatosConjuntoAnuncio(int IdConjuntoAnuncio)
        {
            try
            {
                List<ConjuntoAnuncioDTO> Registros = new List<ConjuntoAnuncioDTO>();
                var _query = "SELECT Id, Nombre,EnlaceFormulario,IdCentroCosto,NombreCentroCosto,IdCategoriaOrigen," +
                    "IdConjuntoAnuncioTipoObjetivo,IdConjuntoAnuncioFuente,IdFormularioPlantilla,NombreFormularioPlantilla,Adicional," +
                    "EsGrupal,EsPaginaWeb,EsPrelanzamiento, IdConjuntoAnuncioTipoGenero, IdConjuntoAnuncioSegmento, IdPais, NroAnuncio, NroSemana, DiaEnvio, Propietario  " +
                    "FROM [mkt].[V_TConjuntoAnuncioV2] WHERE Id=@IdConjuntoAnuncio";
                var result = _dapper.QueryDapper(_query, new { IdConjuntoAnuncio=IdConjuntoAnuncio });
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<ConjuntoAnuncioDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// obtiene la lista de generos para conjuntoAnuncio (mkt.T_ConjuntoAnuncioTipoGenero)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerConjuntoAnuncioListaGeneros()
        {
            try
            {
                List<FiltroDTO> data = new List<FiltroDTO>();
                var _query = "SELECT Id,Nombre,Codigo FROM mkt.V_TConjuntoAnuncioTipoGenero";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<FiltroDTO>>(dataDB);
                }
                return data;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// obtiene la lista de segmentos para conjuntoAnuncio (mkt.T_ConjuntoAnuncioSegmento)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerConjuntoAnuncioListaSegmentos()
        {
            try
            {
                List<FiltroDTO> data = new List<FiltroDTO>();
                var _query = "SELECT Id,Nombre,Codigo FROM mkt.V_TConjuntoAnuncioSegmento";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<FiltroDTO>>(dataDB);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// obtiene los nombres para construir el nombre de un conjuntoAnuncio especifico
        /// </summary>
        /// <returns></returns>
        public List<ConjuntoAnuncioCodigosDTO> ObtenerCodigos(int IdConjuntoAnuncio)
        {
            try
            {
                List<ConjuntoAnuncioCodigosDTO> data = new List<ConjuntoAnuncioCodigosDTO>();
                var _query = "SELECT Id,CentroCosto,Pais,Sexo,Segmento,Plantilla,CategoriaOrigen FROM mkt.V_ConjuntoAnuncioCodigosNombre WHERE Id=@IdConjuntoAnuncio";
                var dataDB = _dapper.QueryDapper(_query, new { IdConjuntoAnuncio= IdConjuntoAnuncio });
                if (!string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<ConjuntoAnuncioCodigosDTO>>(dataDB);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene el último dato landing Page registrado mediante el id conjunto anuncio
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public DatoLandingPageDTO obtenerDatoLandingPagePorIdConjuntoAnuncio(int IdConjuntoAnuncio)
        {
            try
            {
                var _query = "Select TOP 1 Id,IdFormularioLandingPage,PeCentroCosto,IdConjuntoAnuncio from mkt.T_DatoLandingPage WHERE Estado = 1 and IdConjuntoAnuncio=@IdConjuntoAnuncio ORDER BY Id Desc";
                var query = _dapper.FirstOrDefault(_query, new { IdConjuntoAnuncio = IdConjuntoAnuncio });
                return JsonConvert.DeserializeObject<DatoLandingPageDTO>(query);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el último dato landing Page registrado mediante el id conjunto anuncio
        /// </summary>
        /// <param name="Nombre"></param>
        /// <returns></returns>
        public List<ConjuntoAnuncioAsociacionDTO> ObtenerConjuntoAnuncioAsociacion()
        {
            try
            {
                List<ConjuntoAnuncioAsociacionDTO> ConjuntoAnuncio = new List<ConjuntoAnuncioAsociacionDTO>();
                var campos = "Id,NombreConjuntoAnuncio,IdCategoriaOrigen,NombreCategoriaOrigen,IdConjuntoAnuncioFacebook," +
                             "NombreConjuntoAnuncioFB,FechaCreacionCampania,FechaModificacion,UsuarioModificacion";

                var _query = "SELECT " + campos + " FROM  mkt.V_ObtenerConjuntoAnuncioAsociacion WHERE Estado = 1";
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    ConjuntoAnuncio = JsonConvert.DeserializeObject<List<ConjuntoAnuncioAsociacionDTO>>(dataDB);
                }
                return ConjuntoAnuncio;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

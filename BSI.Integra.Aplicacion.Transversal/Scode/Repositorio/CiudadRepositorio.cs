using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: CiudadRepositorio
    /// Autor: Fischer Valdez - Johan Cayo - Luis Huallpa - Wilber Choque - Esthephany Tanco - Ansoli Espinoza - Richard Zenteno - Britsel Calluchi.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_Ciudad
    /// </summary>
    public class CiudadRepositorio : BaseRepository<TCiudad, CiudadBO>
    {
        #region Metodos Base
        public CiudadRepositorio() : base()
        {
        }
        public CiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CiudadBO> GetBy(Expression<Func<TCiudad, bool>> filter)
        {
            IEnumerable<TCiudad> listado = base.GetBy(filter);
            List<CiudadBO> listadoBO = new List<CiudadBO>();
            foreach (var itemEntidad in listado)
            {
                CiudadBO objetoBO = Mapper.Map<TCiudad, CiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CiudadBO FirstById(int id)
        {
            try
            {
                TCiudad entidad = base.FirstById(id);
                CiudadBO objetoBO = new CiudadBO();
                Mapper.Map<TCiudad, CiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CiudadBO FirstBy(Expression<Func<TCiudad, bool>> filter)
        {
            try
            {
                TCiudad entidad = base.FirstBy(filter);
                CiudadBO objetoBO = Mapper.Map<TCiudad, CiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CiudadBO> listadoBO)
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

        public bool Update(CiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CiudadBO> listadoBO)
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
        private void AsignacionId(TCiudad entidad, CiudadBO objetoBO)
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

        private TCiudad MapeoEntidad(CiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCiudad entidad = new TCiudad();
                entidad = Mapper.Map<CiudadBO, TCiudad>(objetoBO,
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
        /// Repositorio: CiudadRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene nombre de ciudad por id con estado 1
        /// </summary>
        /// <param name="id"> Id de Ciudad </param>
        /// <returns> CiudadNombreDTO </returns>
        public CiudadNombreDTO ObtenerNombreCiudadPorId(int id)
        {
            try
            {
                string _queryCiudad = "select Nombre From conf.V_TCiudad_Nombre where Id=@Id and Estado=1";
                var Ciudad = _dapper.FirstOrDefault(_queryCiudad, new { Id=id});
                return JsonConvert.DeserializeObject<CiudadNombreDTO>(Ciudad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de ciudades (activas) registradas en el sistema, y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<CiudadFiltroDTO> ObtenerCiudadFiltro()
        {
            try
            {
                List<CiudadFiltroDTO> ciudades = new List<CiudadFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_TCiudad_Filtro WHERE Estado = 1";
                var ciudadesDB = _dapper.QueryDapper(_query,null);
                if (!string.IsNullOrEmpty(ciudadesDB) && !ciudadesDB.Contains("[]"))
                {
                    ciudades = JsonConvert.DeserializeObject<List<CiudadFiltroDTO>>(ciudadesDB);
                }
                return ciudades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Repositorio: CiudadRepositorio
        /// Autor: Luis H, Edgar S.
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene lista de ciudades para filtro en el formulario
        /// </summary>
        /// <returns> List<CiudadDatosDTO> </returns>
        public List<CiudadDatosDTO> ObtenerCiudadesPorPais ()
        {
            try
            {
                string queryCiudad = "SELECT Id, Nombre, IdPais, Codigo FROM conf.V_TCiudad_ObtenerDatos  WHERE Estado = 1";
                var ciudad = _dapper.QueryDapper(queryCiudad,null);
                return JsonConvert.DeserializeObject<List<CiudadDatosDTO>>(ciudad);
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Valida si la longitud del celular es correcta
        /// </summary>
        /// <param name="idPais"></param>
        /// <param name="numeroCelular"></param>
        /// <returns></returns>
        public bool LongitudCelularPorPaisCorrecta(int? idPais, string numeroCelular) {
            try
            {
                var temp = GetBy(x => x.IdPais == idPais, x => new { x.LongCelular });
                var longitudCorrecta = false;
                foreach (var item in temp)
                {
                    if (item.LongCelular == numeroCelular.Length) {
                        longitudCorrecta = true;
                        break;
                    }
                }
                return longitudCorrecta;
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: CiudadRepositorio
        /// Autor: Luis Huallpa - Edgar Serruto.
        /// Fecha: 05/08/2021
        /// <summary>
        /// Obtiene lista de ciudades para lista desplagable
        /// </summary>
        /// <returns>List<CiudadFiltroComboDTO></returns>
        public List<CiudadFiltroComboDTO> ObtenerCiudadesFiltro()
        {
            try
            {
                var listaPaises = GetBy(x => true, y => new CiudadFiltroComboDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdPais = y.IdPais,

                }).ToList();
                return listaPaises;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de ciudades filtradas por un valor
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public List<CiudadFiltroDTO> ObtenerCiudadFiltroAutocomplete(string valor)
        {
            try
            {
                List<CiudadFiltroDTO> ciudades = new List<CiudadFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_TCiudad_Filtro WHERE Estado = 1 and Nombre LIKE CONCAT('%',@valor,'%')  ORDER By Nombre ASC";
                var ciudadesDB = _dapper.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(ciudadesDB) && !ciudadesDB.Contains("[]"))
                {
                    ciudades = JsonConvert.DeserializeObject<List<CiudadFiltroDTO>>(ciudadesDB);
                }
                return ciudades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las ciudades para ser mostrados en una grilla (para CRUD Propio)
        /// </summary>
        /// <returns></returns>
        public List<CiudadDTO> ObtenerTodoCiudades()
        {
            try
            {
                List<CiudadDTO> ciudades = new List<CiudadDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Codigo, Nombre, IdPais, LongCelular, LongTelefono FROM conf.T_Ciudad WHERE Estado = 1";
                var ciudadesDB = _dapper.QueryDapper(_query, null);
                ciudades = JsonConvert.DeserializeObject<List<CiudadDTO>>(ciudadesDB);
                return ciudades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public List<FiltroDTO> ObtenerCiudadesDeSedesExistentes()
        {
            try
            {
                List<FiltroDTO> ciudades = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM fin.V_ObtenerCiudadSedes";
                var ciudadesDB = _dapper.QueryDapper(_query, null);
                ciudades = JsonConvert.DeserializeObject<List<FiltroDTO>>(ciudadesDB);
                return ciudades;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

		public List<FiltroDTO> ObtenerCiudadesScrapingAerolinea()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM pla.V_ObtenerNombreCiudadesConCodigoAeropuerto WHERE Estado = 1";
				var dapper = _dapper.QueryDapper(query,null);
				var res = JsonConvert.DeserializeObject<List<FiltroDTO>>(dapper);
				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<FiltroDTO> ObtenerCiudadPorSede()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM [conf].[VT_ObtenerCiudadPorSede] WHERE Estado = 1";
				var dapper = _dapper.QueryDapper(query, null);
				var res = JsonConvert.DeserializeObject<List<FiltroDTO>>(dapper);
				return res;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
	}
}

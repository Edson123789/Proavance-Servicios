using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Maestros.Repositorio
{
    public class RegionCiudadRepositorio : BaseRepository<TRegionCiudad, RegionCiudadBO>
    {
        #region Metodos Base
        public RegionCiudadRepositorio() : base()
        {
        }
        public RegionCiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RegionCiudadBO> GetBy(Expression<Func<TRegionCiudad, bool>> filter)
        {
            IEnumerable<TRegionCiudad> listado = base.GetBy(filter);
            List<RegionCiudadBO> listadoBO = new List<RegionCiudadBO>();
            foreach (var itemEntidad in listado)
            {
                RegionCiudadBO objetoBO = Mapper.Map<TRegionCiudad, RegionCiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RegionCiudadBO FirstById(int id)
        {
            try
            {
                TRegionCiudad entidad = base.FirstById(id);
                RegionCiudadBO objetoBO = new RegionCiudadBO();
                Mapper.Map<TRegionCiudad, RegionCiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RegionCiudadBO FirstBy(Expression<Func<TRegionCiudad, bool>> filter)
        {
            try
            {
                TRegionCiudad entidad = base.FirstBy(filter);
                RegionCiudadBO objetoBO = Mapper.Map<TRegionCiudad, RegionCiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RegionCiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRegionCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RegionCiudadBO> listadoBO)
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

        public bool Update(RegionCiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRegionCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RegionCiudadBO> listadoBO)
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
        private void AsignacionId(TRegionCiudad entidad, RegionCiudadBO objetoBO)
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

        private TRegionCiudad MapeoEntidad(RegionCiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRegionCiudad entidad = new TRegionCiudad();
                entidad = Mapper.Map<RegionCiudadBO, TRegionCiudad>(objetoBO,
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
        /// Obtiene todas las ciudades para combobox
        /// </summary>
        /// <returns></returns>
        public List<RegionCiudadFiltroComboDTO> ObtenerRegionCiudadFiltro()
        {
            try
            {
                var listaRegionCiudades = GetBy(x => true, y => new RegionCiudadFiltroComboDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdCiudad = y.IdCiudad

                }).ToList();

                return listaRegionCiudades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        


        /// <summary>
        /// Obtiene todas las Regiones-Ciudades para ser mostrados en una grilla (para CRUD Propio)
        /// </summary>
        /// <returns></returns>
        public List<RegionCiudadDTO> ObtenerTodoRegionPais()
        {
            try
            {
                List<RegionCiudadDTO> RegionCiudad = new List<RegionCiudadDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, IdCiudad, IdPais, CodigoBS, DenominacionBS, NombreCorto FROM conf.T_RegionCiudad WHERE Estado = 1";
                var RegionCiudadDB = _dapper.QueryDapper(_query, null);
                RegionCiudad = JsonConvert.DeserializeObject<List<RegionCiudadDTO>>(RegionCiudadDB);
                return RegionCiudad;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene el Id, Nombre, CodigoBS, DenominacionBS de regionCiudad 
        /// </summary>
        /// <returns></returns>
        public List<RegionCiudadConLocacionDTO> ObtenerRegionCiudadConLocacion()
        {
            try
            {
                List<RegionCiudadConLocacionDTO> obtenerRegionCiudadConLocacion = new List<RegionCiudadConLocacionDTO>();
                var _query = "SELECT Id, Nombre, CodigoBS, DenominacionBS FROM pla.V_ObtenertodoCiudadConLocacion WHERE EstadoCiudad = 1 AND EstadoLocacion = 1";
                var obtenerRegionCiudadConLocacionDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerRegionCiudadConLocacionDB) && !obtenerRegionCiudadConLocacionDB.Contains("[]"))
                {
                    obtenerRegionCiudadConLocacion = JsonConvert.DeserializeObject<List<RegionCiudadConLocacionDTO>>(obtenerRegionCiudadConLocacionDB);
                }
                return obtenerRegionCiudadConLocacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene lista de ciudades con denominacion BS para programa especifico
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerListaCiudadesBs()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM [conf].[V_TRegionCiudad_ObtenerCiudadBs] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<FiltroDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }


}

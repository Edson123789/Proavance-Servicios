using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class LocacionRepositorio : BaseRepository<TLocacion, LocacionBO>
    {
        #region Metodos Base
        public LocacionRepositorio() : base()
        {
        }
        public LocacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<LocacionBO> GetBy(Expression<Func<TLocacion, bool>> filter)
        {
            IEnumerable<TLocacion> listado = base.GetBy(filter);
            List<LocacionBO> listadoBO = new List<LocacionBO>();
            foreach (var itemEntidad in listado)
            {
                LocacionBO objetoBO = Mapper.Map<TLocacion, LocacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public LocacionBO FirstById(int id)
        {
            try
            {
                TLocacion entidad = base.FirstById(id);
                LocacionBO objetoBO = new LocacionBO();
                Mapper.Map<TLocacion, LocacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LocacionBO FirstBy(Expression<Func<TLocacion, bool>> filter)
        {
            try
            {
                TLocacion entidad = base.FirstBy(filter);
                LocacionBO objetoBO = Mapper.Map<TLocacion, LocacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(LocacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TLocacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<LocacionBO> listadoBO)
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

        public bool Update(LocacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TLocacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<LocacionBO> listadoBO)
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
        private void AsignacionId(TLocacion entidad, LocacionBO objetoBO)
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

        private TLocacion MapeoEntidad(LocacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TLocacion entidad = new TLocacion();
                entidad = Mapper.Map<LocacionBO, TLocacion>(objetoBO,
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
		/// Obtiene todas las locaciones para filtro en Programa Especifico
		/// </summary>
		/// <returns></returns>
		public List<LocacionParaFiltroDTO> ObtenerLocacionParaFiltro()
		{
			try
			{
				string _queryLocacion = "SELECT Id,Nombre,IdCiudad FROM pla.V_TLocacion_ParaFiltro WHERE Estado=1";
				var Locacion = _dapper.QueryDapper(_queryLocacion, null);
				return JsonConvert.DeserializeObject<List<LocacionParaFiltroDTO>>(Locacion);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene todas las locaciones para combobox
        /// </summary>
        /// <returns></returns>
        public List<LocacionComboFiltroDTO> ObtenerLocacionFiltro()
        {
            try
            {
                var listaLocacion = GetBy(x => true, y => new LocacionComboFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,

                }).ToList();

                return listaLocacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene todos los registros 
        /// </summary>
        /// <returns></returns>
        public List<LocacionFiltroDTO> ObtenerTodoGrid()
        {
            try
            {
                var listaLocacion = GetBy(x => true, y => new LocacionFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdPais = y.IdPais,
                    IdRegion = y.IdRegion,
                    IdCiudad = y.IdCiudad,
                    Direccion = y.Direccion
                }).OrderByDescending(x => x.Id).ToList();

                return listaLocacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///  Obtiene todas las registros de locaciones para combobox en base a Region
        /// </summary>
        /// <returns></returns>
        public List<LocacionComboFiltroRegionDTO> ObtenerTodoComboLocacion()
        {
            try
            {
                var listaLocacion = GetBy(x => true, y => new LocacionComboFiltroRegionDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdRegion = y.IdRegion
                }).ToList();

                return listaLocacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

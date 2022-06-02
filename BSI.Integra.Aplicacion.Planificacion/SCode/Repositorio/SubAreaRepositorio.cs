using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class SubAreaRepositorio : BaseRepository<TSubArea, SubAreaBO>
    {
        #region Metodos Base
        public SubAreaRepositorio() : base()
        {
        }
        public SubAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubAreaBO> GetBy(Expression<Func<TSubArea, bool>> filter)
        {
            IEnumerable<TSubArea> listado = base.GetBy(filter);
            List<SubAreaBO> listadoBO = new List<SubAreaBO>();
            foreach (var itemEntidad in listado)
            {
                SubAreaBO objetoBO = Mapper.Map<TSubArea, SubAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubAreaBO FirstById(int id)
        {
            try
            {
                TSubArea entidad = base.FirstById(id);
                SubAreaBO objetoBO = new SubAreaBO();
                Mapper.Map<TSubArea, SubAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubAreaBO FirstBy(Expression<Func<TSubArea, bool>> filter)
        {
            try
            {
                TSubArea entidad = base.FirstBy(filter);
                SubAreaBO objetoBO = Mapper.Map<TSubArea, SubAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubAreaBO> listadoBO)
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

        public bool Update(SubAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubAreaBO> listadoBO)
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
        private void AsignacionId(TSubArea entidad, SubAreaBO objetoBO)
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

        private TSubArea MapeoEntidad(SubAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubArea entidad = new TSubArea();
                entidad = Mapper.Map<SubAreaBO, TSubArea>(objetoBO,
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
        /// Obtiene lista de sub areas por id area
        /// </summary>
        /// <param name="idArea"></param>
        /// <returns></returns>
        public List<SubAreaCentroCostoDTO> ObtenerSubAreaPorArea(int idArea)
        {
            try
            {
                string _querySubArea = string.Empty;
                _querySubArea = "Select Id,Nombre From pla.V_TSubArea_IdNombre Where Estado = 1 and IdArea=@IdArea";
                var SubArea = _dapper.QueryDapper(_querySubArea, new { IdArea = idArea });
                return JsonConvert.DeserializeObject<List<SubAreaCentroCostoDTO>>(SubArea);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene lista de sub areas
		/// </summary>
		/// <returns></returns>
		public List<SubAreaCentroCostoDTO> ObtenerListaSubAreas()
		{
			try
			{
				var query = "SELECT Id,Nombre, IdArea FROM pla.V_TSubArea_IdNombre WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<SubAreaCentroCostoDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}

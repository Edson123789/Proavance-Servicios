using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	/// Repositorio: Marketing/GrupoFiltroProgramaCriticoCentroCostoRepositorio
	/// Autor: Gian Miranda
	/// Fecha: 23/04/2021
	/// <summary>
	/// Repositorio para consultas de mkt.T_GrupoFiltroProgramaCriticoCentroCosto
	/// </summary>
	public class GrupoFiltroProgramaCriticoCentroCostoRepositorio : BaseRepository<TGrupoFiltroProgramaCriticoCentroCosto, GrupoFiltroProgramaCriticoCentroCostoBO>
	{
		#region Metodos Base
		public GrupoFiltroProgramaCriticoCentroCostoRepositorio() : base()
		{
		}
		public GrupoFiltroProgramaCriticoCentroCostoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<GrupoFiltroProgramaCriticoCentroCostoBO> GetBy(Expression<Func<TGrupoFiltroProgramaCriticoCentroCosto, bool>> filter)
		{
			IEnumerable<TGrupoFiltroProgramaCriticoCentroCosto> listado = base.GetBy(filter);
			List<GrupoFiltroProgramaCriticoCentroCostoBO> listadoBO = new List<GrupoFiltroProgramaCriticoCentroCostoBO>();
			foreach (var itemEntidad in listado)
			{
				GrupoFiltroProgramaCriticoCentroCostoBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoCentroCosto, GrupoFiltroProgramaCriticoCentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public GrupoFiltroProgramaCriticoCentroCostoBO FirstById(int id)
		{
			try
			{
				TGrupoFiltroProgramaCriticoCentroCosto entidad = base.FirstById(id);
				GrupoFiltroProgramaCriticoCentroCostoBO objetoBO = new GrupoFiltroProgramaCriticoCentroCostoBO();
				Mapper.Map<TGrupoFiltroProgramaCriticoCentroCosto, GrupoFiltroProgramaCriticoCentroCostoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public GrupoFiltroProgramaCriticoCentroCostoBO FirstBy(Expression<Func<TGrupoFiltroProgramaCriticoCentroCosto, bool>> filter)
		{
			try
			{
				TGrupoFiltroProgramaCriticoCentroCosto entidad = base.FirstBy(filter);
				GrupoFiltroProgramaCriticoCentroCostoBO objetoBO = Mapper.Map<TGrupoFiltroProgramaCriticoCentroCosto, GrupoFiltroProgramaCriticoCentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(GrupoFiltroProgramaCriticoCentroCostoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TGrupoFiltroProgramaCriticoCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<GrupoFiltroProgramaCriticoCentroCostoBO> listadoBO)
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

		public bool Update(GrupoFiltroProgramaCriticoCentroCostoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TGrupoFiltroProgramaCriticoCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<GrupoFiltroProgramaCriticoCentroCostoBO> listadoBO)
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
		private void AsignacionId(TGrupoFiltroProgramaCriticoCentroCosto entidad, GrupoFiltroProgramaCriticoCentroCostoBO objetoBO)
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

		private TGrupoFiltroProgramaCriticoCentroCosto MapeoEntidad(GrupoFiltroProgramaCriticoCentroCostoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TGrupoFiltroProgramaCriticoCentroCosto entidad = new TGrupoFiltroProgramaCriticoCentroCosto();
				entidad = Mapper.Map<GrupoFiltroProgramaCriticoCentroCostoBO, TGrupoFiltroProgramaCriticoCentroCosto>(objetoBO,
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
        /// SE eliminan los centros de costos asociados a un grupo que haya sido eliminado por el usuario.
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void DeleteLogicoPorGrupo(int idGrupo, string usuario, List<CentroCostoSubAreaDTO> nuevos)
        {
            try
            {
                List<EliminacionGrupoFiltroCentroCostoDTO> listaBorrar = new List<EliminacionGrupoFiltroCentroCostoDTO>();
                listaBorrar = GetBy(x => x.IdGrupoFiltroProgramaCritico == idGrupo, y => new EliminacionGrupoFiltroCentroCostoDTO()
                {
                    Id = y.Id,
                    IdCentroCosto = y.IdCentroCosto
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.IdCentroCosto));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Se obtienen los centros de costo por GrupoFiltroProgramaCritico
		/// </summary>
		/// <param name="idGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
		/// <returns>Lista de objetos de clase CentroCostoSubAreaDTO</returns>
		public List<CentroCostoSubAreaDTO> ObtenerPorIdGrupo(int idGrupo)
        {
            try
            {
                List<CentroCostoSubAreaDTO> lista = new List<CentroCostoSubAreaDTO>();
                var query = "SELECT Id, Nombre, NombreAreaCapacitacion, NombreSubAreaCapacitacion FROM mkt.V_ObtenerCentroCostoPorGrupoFiltro WHERE IdGrupoFiltro = @idGrupo AND Estado=1";
                var respuestaDapper = _dapper.QueryDapper(query, new { idGrupo = idGrupo });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<CentroCostoSubAreaDTO>>(respuestaDapper);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

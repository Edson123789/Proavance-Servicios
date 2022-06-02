using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class AccionFormularioPorCategoriaOrigenRepositorio : BaseRepository<TAccionFormularioPorCategoriaOrigen, AccionFormularioPorCategoriaOrigenBO>
	{
		#region Metodos Base
		public AccionFormularioPorCategoriaOrigenRepositorio() : base()
		{
		}
		public AccionFormularioPorCategoriaOrigenRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AccionFormularioPorCategoriaOrigenBO> GetBy(Expression<Func<TAccionFormularioPorCategoriaOrigen, bool>> filter)
		{
			IEnumerable<TAccionFormularioPorCategoriaOrigen> listado = base.GetBy(filter);
			List<AccionFormularioPorCategoriaOrigenBO> listadoBO = new List<AccionFormularioPorCategoriaOrigenBO>();
			foreach (var itemEntidad in listado)
			{
				AccionFormularioPorCategoriaOrigenBO objetoBO = Mapper.Map<TAccionFormularioPorCategoriaOrigen, AccionFormularioPorCategoriaOrigenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AccionFormularioPorCategoriaOrigenBO FirstById(int id)
		{
			try
			{
				TAccionFormularioPorCategoriaOrigen entidad = base.FirstById(id);
				AccionFormularioPorCategoriaOrigenBO objetoBO = new AccionFormularioPorCategoriaOrigenBO();
				Mapper.Map<TAccionFormularioPorCategoriaOrigen, AccionFormularioPorCategoriaOrigenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AccionFormularioPorCategoriaOrigenBO FirstBy(Expression<Func<TAccionFormularioPorCategoriaOrigen, bool>> filter)
		{
			try
			{
				TAccionFormularioPorCategoriaOrigen entidad = base.FirstBy(filter);
				AccionFormularioPorCategoriaOrigenBO objetoBO = Mapper.Map<TAccionFormularioPorCategoriaOrigen, AccionFormularioPorCategoriaOrigenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AccionFormularioPorCategoriaOrigenBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAccionFormularioPorCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AccionFormularioPorCategoriaOrigenBO> listadoBO)
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

		public bool Update(AccionFormularioPorCategoriaOrigenBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAccionFormularioPorCategoriaOrigen entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AccionFormularioPorCategoriaOrigenBO> listadoBO)
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
		private void AsignacionId(TAccionFormularioPorCategoriaOrigen entidad, AccionFormularioPorCategoriaOrigenBO objetoBO)
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

		private TAccionFormularioPorCategoriaOrigen MapeoEntidad(AccionFormularioPorCategoriaOrigenBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAccionFormularioPorCategoriaOrigen entidad = new TAccionFormularioPorCategoriaOrigen();
				entidad = Mapper.Map<AccionFormularioPorCategoriaOrigenBO, TAccionFormularioPorCategoriaOrigen>(objetoBO,
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
        /// Obtiene las CategoriasOrigenes por el IdAccionFormulario asociado
        /// </summary>
        /// <param name="IdAccionFormulario"></param>
        /// <returns></returns>
        public List<AccionFormularioPorCategoriaOrigenDTO> ObtenerPorIdAccionFormulario(int IdAccionFormulario)
        {
            try
            {
                List<AccionFormularioPorCategoriaOrigenDTO> data = new List<AccionFormularioPorCategoriaOrigenDTO>();
                var query = _dapper.QueryDapper("SELECT IdCategoriaOrigen FROM [mkt].[V_AccionFormularioPorCategoriaOrigen] where IdAccionFormulario=@IdAccionFormulario", new {IdAccionFormulario=IdAccionFormulario });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<AccionFormularioPorCategoriaOrigenDTO>>(query);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Eliminacion logica de los registros eliminados por el usuario.
        /// </summary>
        /// <param name="IdAccionFormulario"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void DeleteLogicoPorAccionFormulario(int IdAccionFormulario, string usuario, List<AccionFormularioPorCategoriaOrigenDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdAccionFormulario == IdAccionFormulario, y => new AccionFormularioPorCategoriaOrigenDTO
                {
                    Id = y.Id,
                    IdCategoriaOrigen = y.IdCategoriaOrigen
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdCategoriaOrigen == x.IdCategoriaOrigen));
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
    }
}

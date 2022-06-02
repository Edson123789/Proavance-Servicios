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
	public class AccionFormularioPorCampoContactoRepositorio : BaseRepository<TAccionFormularioPorCampoContacto, AccionFormularioPorCampoContactoBO>
	{
		#region Metodos Base
		public AccionFormularioPorCampoContactoRepositorio() : base()
		{
		}
		public AccionFormularioPorCampoContactoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AccionFormularioPorCampoContactoBO> GetBy(Expression<Func<TAccionFormularioPorCampoContacto, bool>> filter)
		{
			IEnumerable<TAccionFormularioPorCampoContacto> listado = base.GetBy(filter);
			List<AccionFormularioPorCampoContactoBO> listadoBO = new List<AccionFormularioPorCampoContactoBO>();
			foreach (var itemEntidad in listado)
			{
				AccionFormularioPorCampoContactoBO objetoBO = Mapper.Map<TAccionFormularioPorCampoContacto, AccionFormularioPorCampoContactoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AccionFormularioPorCampoContactoBO FirstById(int id)
		{
			try
			{
				TAccionFormularioPorCampoContacto entidad = base.FirstById(id);
				AccionFormularioPorCampoContactoBO objetoBO = new AccionFormularioPorCampoContactoBO();
				Mapper.Map<TAccionFormularioPorCampoContacto, AccionFormularioPorCampoContactoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AccionFormularioPorCampoContactoBO FirstBy(Expression<Func<TAccionFormularioPorCampoContacto, bool>> filter)
		{
			try
			{
				TAccionFormularioPorCampoContacto entidad = base.FirstBy(filter);
				AccionFormularioPorCampoContactoBO objetoBO = Mapper.Map<TAccionFormularioPorCampoContacto, AccionFormularioPorCampoContactoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AccionFormularioPorCampoContactoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAccionFormularioPorCampoContacto entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AccionFormularioPorCampoContactoBO> listadoBO)
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

		public bool Update(AccionFormularioPorCampoContactoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAccionFormularioPorCampoContacto entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AccionFormularioPorCampoContactoBO> listadoBO)
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
		private void AsignacionId(TAccionFormularioPorCampoContacto entidad, AccionFormularioPorCampoContactoBO objetoBO)
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

		private TAccionFormularioPorCampoContacto MapeoEntidad(AccionFormularioPorCampoContactoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAccionFormularioPorCampoContacto entidad = new TAccionFormularioPorCampoContacto();
				entidad = Mapper.Map<AccionFormularioPorCampoContactoBO, TAccionFormularioPorCampoContacto>(objetoBO,
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
        /// Obtiene los registros por el IdAccionFormulario
        /// </summary>
        /// <param name="IdAccionFormulario"></param>
        /// <returns></returns>
        public List<AccionFormularioPorCampoContactoDTO> ObtenerPorIdAccionFormulario(int IdAccionFormulario)
        {
            try
            {
                List<AccionFormularioPorCampoContactoDTO> data = new List<AccionFormularioPorCampoContactoDTO>();
                var query = _dapper.QueryDapper("SELECT " +
                       "Id," +
                       "IdCampoContacto," +
                       "Orden," +
                       "Campo,"+
                       "EsSiempreVisible,"+
                       "EsInteligente,"+
                       "TieneProbabilidad " +
                    " FROM [mkt].[V_AccionFormularioPorCampoContacto] where IdAccionFormulario=@IdAccionFormulario", new { IdAccionFormulario = IdAccionFormulario });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<AccionFormularioPorCampoContactoDTO>>(query);
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
        public void DeleteLogicoPorAccionFormulario(int IdAccionFormulario, string usuario, List<AccionFormularioPorCampoContactoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdAccionFormulario == IdAccionFormulario, y => new EliminacionIdsDTO
                {
                    Id = y.Id
                }).ToList();

                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
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

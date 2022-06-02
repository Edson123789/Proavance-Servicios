using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs.Comercial;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class AvatarRepositorio : BaseRepository<TAvatar, AvatarBO>
    {
		#region Metodos Base
		public AvatarRepositorio() : base()
		{
		}
		public AvatarRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AvatarBO> GetBy(Expression<Func<TAvatar, bool>> filter)
		{
			IEnumerable<TAvatar> listado = base.GetBy(filter);
			List<AvatarBO> listadoBO = new List<AvatarBO>();
			foreach (var itemEntidad in listado)
			{
				AvatarBO objetoBO = Mapper.Map<TAvatar, AvatarBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AvatarBO FirstById(int id)
		{
			try
			{
				TAvatar entidad = base.FirstById(id);
				AvatarBO objetoBO = new AvatarBO();
				Mapper.Map<TAvatar, AvatarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AvatarBO FirstById(int id, integraDBContext _integraDBContext)
		{
			try
			{
				TAvatar entidad = base.FirstById(id);
				AvatarBO objetoBO = new AvatarBO(_integraDBContext);
				Mapper.Map<TAvatar, AvatarBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AvatarBO FirstBy(Expression<Func<TAvatar, bool>> filter)
		{
			try
			{
				TAvatar entidad = base.FirstBy(filter);
				AvatarBO objetoBO = Mapper.Map<TAvatar, AvatarBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AvatarBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAvatar entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AvatarBO> listadoBO)
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

		public bool Update(AvatarBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAvatar entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AvatarBO> listadoBO)
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
		private void AsignacionId(TAvatar entidad, AvatarBO objetoBO)
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

		private TAvatar MapeoEntidad(AvatarBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAvatar entidad = new TAvatar();
				entidad = Mapper.Map<AvatarBO, TAvatar>(objetoBO,
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
		/// Autor: Jashin Salazar
        /// Fecha: 30/07/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las caracteristicas para la construccion del avatar
        /// </summary>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
		public List<AvatarCaracteristicaDTO> ObtenerCaracteristicas()
        {
			try
			{
				List<AvatarCaracteristicaDTO> caracteristicas = new List<AvatarCaracteristicaDTO>();
				var query = "SELECT TipoCaracteristica,Etiqueta,Valor FROM com.T_AvatarCaracteristica WHERE Estado=1";
				var respuesta = _dapper.QueryDapper(query, null);

				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					caracteristicas = JsonConvert.DeserializeObject<List<AvatarCaracteristicaDTO>>(respuesta);
				}
				return caracteristicas;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 30/07/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene avatar por usuario
		/// </summary>
		/// <param name="Usuario"> Nombre de usuario </param>
		/// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
		public AvatarDTO ObtenerAvatar(string Usuario)
        {
			try
			{
				AvatarDTO avatar = new AvatarDTO();
				var query = "SELECT * FROM com.V_ObtenerAvatarPorUsuario WHERE UserName=@Usuario";
				var respuesta = _dapper.FirstOrDefault(query, new { Usuario});

				if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
				{
					avatar = JsonConvert.DeserializeObject<AvatarDTO>(respuesta);
				}
				return avatar;

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
	public class TipoIdentificadorRepositorio : BaseRepository<TTipoIdentificador, TipoIdentificadorBO>
	{
		#region Metodos Base
		public TipoIdentificadorRepositorio() : base()
		{
		}
		public TipoIdentificadorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TipoIdentificadorBO> GetBy(Expression<Func<TTipoIdentificador, bool>> filter)
		{
			IEnumerable<TTipoIdentificador> listado = base.GetBy(filter);
			List<TipoIdentificadorBO> listadoBO = new List<TipoIdentificadorBO>();
			foreach (var itemEntidad in listado)
			{
				TipoIdentificadorBO objetoBO = Mapper.Map<TTipoIdentificador, TipoIdentificadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TipoIdentificadorBO FirstById(int id)
		{
			try
			{
				TTipoIdentificador entidad = base.FirstById(id);
				TipoIdentificadorBO objetoBO = new TipoIdentificadorBO();
				Mapper.Map<TTipoIdentificador, TipoIdentificadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TipoIdentificadorBO FirstBy(Expression<Func<TTipoIdentificador, bool>> filter)
		{
			try
			{
				TTipoIdentificador entidad = base.FirstBy(filter);
				TipoIdentificadorBO objetoBO = Mapper.Map<TTipoIdentificador, TipoIdentificadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TipoIdentificadorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTipoIdentificador entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TipoIdentificadorBO> listadoBO)
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

		public bool Update(TipoIdentificadorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTipoIdentificador entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TipoIdentificadorBO> listadoBO)
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
		private void AsignacionId(TTipoIdentificador entidad, TipoIdentificadorBO objetoBO)
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

		private TTipoIdentificador MapeoEntidad(TipoIdentificadorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTipoIdentificador entidad = new TTipoIdentificador();
				entidad = Mapper.Map<TipoIdentificadorBO, TTipoIdentificador>(objetoBO,
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
        /// Obtiene una los datos de TipoIdentificador (Estado=1) para ser usados en una grilla (CRUD propio)
        /// </summary>
        /// <returns></returns>
        public List<TipoIdentificadorDTO> ObtenerTodoTipoIdentificador()
        {
            try
            {
                List<TipoIdentificadorDTO> TipoIdentificadores = new List<TipoIdentificadorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, IdPais FROM fin.T_TipoIdentificador WHERE Estado = 1";
                var TipoIdentificadoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(TipoIdentificadoresDB) && !TipoIdentificadoresDB.Contains("[]"))
                {
                    TipoIdentificadores = JsonConvert.DeserializeObject<List<TipoIdentificadorDTO>>(TipoIdentificadoresDB);
                }
                return TipoIdentificadores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}

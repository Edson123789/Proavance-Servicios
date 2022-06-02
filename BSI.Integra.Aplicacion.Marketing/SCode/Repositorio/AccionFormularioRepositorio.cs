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
	public class AccionFormularioRepositorio : BaseRepository<TAccionFormulario, AccionFormularioBO>
	{
		#region Metodos Base
		public AccionFormularioRepositorio() : base()
		{
		}
		public AccionFormularioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AccionFormularioBO> GetBy(Expression<Func<TAccionFormulario, bool>> filter)
		{
			IEnumerable<TAccionFormulario> listado = base.GetBy(filter);
			List<AccionFormularioBO> listadoBO = new List<AccionFormularioBO>();
			foreach (var itemEntidad in listado)
			{
				AccionFormularioBO objetoBO = Mapper.Map<TAccionFormulario, AccionFormularioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AccionFormularioBO FirstById(int id)
		{
			try
			{
				TAccionFormulario entidad = base.FirstById(id);
				AccionFormularioBO objetoBO = new AccionFormularioBO();
				Mapper.Map<TAccionFormulario, AccionFormularioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AccionFormularioBO FirstBy(Expression<Func<TAccionFormulario, bool>> filter)
		{
			try
			{
				TAccionFormulario entidad = base.FirstBy(filter);
				AccionFormularioBO objetoBO = Mapper.Map<TAccionFormulario, AccionFormularioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AccionFormularioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAccionFormulario entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AccionFormularioBO> listadoBO)
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

		public bool Update(AccionFormularioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAccionFormulario entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AccionFormularioBO> listadoBO)
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
		private void AsignacionId(TAccionFormulario entidad, AccionFormularioBO objetoBO)
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

		private TAccionFormulario MapeoEntidad(AccionFormularioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAccionFormulario entidad = new TAccionFormulario();
				entidad = Mapper.Map<AccionFormularioBO, TAccionFormulario>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaCategoriaOrigen != null && objetoBO.ListaCategoriaOrigen.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaCategoriaOrigen)
                    {
                        TAccionFormularioPorCategoriaOrigen entidadHijo = new TAccionFormularioPorCategoriaOrigen();
                        entidadHijo = Mapper.Map<AccionFormularioPorCategoriaOrigenBO, TAccionFormularioPorCategoriaOrigen>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TAccionFormularioPorCategoriaOrigen.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaCampoContacto != null && objetoBO.ListaCampoContacto.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaCampoContacto)
                    {
                        TAccionFormularioPorCampoContacto entidadHijo = new TAccionFormularioPorCampoContacto();
                        entidadHijo = Mapper.Map<AccionFormularioPorCampoContactoBO, TAccionFormularioPorCampoContacto>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TAccionFormularioPorCampoContacto.Add(entidadHijo);
                    }
                }

                return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<AccionFormularioDTO> ObtenerTodoPanel()
        {
            try
            {
                List<AccionFormularioDTO> data = new List<AccionFormularioDTO>();
                var query = _dapper.QueryDapper("SELECT Id,Nombre,UltimaLlamadaEjecutada,CamposSinValores,NombreCamposSinValores,TiempoRedirecionamiento,CamposSegunProbabilidad,NombreCamposSegunProbabilidad,NumeroClics FROM [mkt].[V_AccionFormulario]", null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<AccionFormularioDTO>>(query);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

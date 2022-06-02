using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
		public class TamanioEmpresaRepositorio : BaseRepository<TTamanioEmpresa, TamanioEmpresaBO>
		{
			#region Metodos Base
			public TamanioEmpresaRepositorio() : base()
			{
			}
			public TamanioEmpresaRepositorio(integraDBContext contexto) : base(contexto)
			{
			}
			public IEnumerable<TamanioEmpresaBO> GetBy(Expression<Func<TTamanioEmpresa, bool>> filter)
			{
				IEnumerable<TTamanioEmpresa> listado = base.GetBy(filter);
				List<TamanioEmpresaBO> listadoBO = new List<TamanioEmpresaBO>();
				foreach (var itemEntidad in listado)
				{
					TamanioEmpresaBO objetoBO = Mapper.Map<TTamanioEmpresa, TamanioEmpresaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
					listadoBO.Add(objetoBO);
				}

				return listadoBO;
			}
			public TamanioEmpresaBO FirstById(int id)
			{
				try
				{
					TTamanioEmpresa entidad = base.FirstById(id);
					TamanioEmpresaBO objetoBO = new TamanioEmpresaBO();
					Mapper.Map<TTamanioEmpresa, TamanioEmpresaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

					return objetoBO;
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
			public TamanioEmpresaBO FirstBy(Expression<Func<TTamanioEmpresa, bool>> filter)
			{
				try
				{
					TTamanioEmpresa entidad = base.FirstBy(filter);
					TamanioEmpresaBO objetoBO = Mapper.Map<TTamanioEmpresa, TamanioEmpresaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

					return objetoBO;
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}

			public bool Insert(TamanioEmpresaBO objetoBO)
			{
				try
				{
					//mapeo de la entidad
					TTamanioEmpresa entidad = MapeoEntidad(objetoBO);

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

			public bool Insert(IEnumerable<TamanioEmpresaBO> listadoBO)
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

			public bool Update(TamanioEmpresaBO objetoBO)
			{
				try
				{
					if (objetoBO == null)
					{
						throw new ArgumentNullException("Entidad nula");
					}

					//mapeo de la entidad
					TTamanioEmpresa entidad = MapeoEntidad(objetoBO);

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

			public bool Update(IEnumerable<TamanioEmpresaBO> listadoBO)
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
			private void AsignacionId(TTamanioEmpresa entidad, TamanioEmpresaBO objetoBO)
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

			private TTamanioEmpresa MapeoEntidad(TamanioEmpresaBO objetoBO)
			{
				try
				{
					//crea la entidad padre
					TTamanioEmpresa entidad = new TTamanioEmpresa();
					entidad = Mapper.Map<TamanioEmpresaBO, TTamanioEmpresa>(objetoBO,
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
            /// Obtiene una los datos de TamanioEmpresa (activos) para ser usados en una grilla (CRUD propio)
            /// </summary>
            /// <returns></returns>
            public List<TamanioEmpresaDTO> ObtenerTodoTamanioEmpresaes()
            {
                try
                {
                    List<TamanioEmpresaDTO> TamanioEmpresas = new List<TamanioEmpresaDTO>();
                    var _query = string.Empty;
                    _query = "SELECT Id, Nombre, Descripcion FROM pla.T_TamanioEmpresa WHERE Estado = 1";
                    var TamanioEmpresasDB = _dapper.QueryDapper(_query, null);
                    if (!string.IsNullOrEmpty(TamanioEmpresasDB) && !TamanioEmpresasDB.Contains("[]"))
                    {
                        TamanioEmpresas = JsonConvert.DeserializeObject<List<TamanioEmpresaDTO>>(TamanioEmpresasDB);
                    }
                    return TamanioEmpresas;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	public class MaterialRegistroEntregaAlumnoRepositorio : BaseRepository<TMaterialRegistroEntregaAlumno, MaterialRegistroEntregaAlumnoBO>
	{
		#region Metodos Base
		public MaterialRegistroEntregaAlumnoRepositorio() : base()
		{
		}
		public MaterialRegistroEntregaAlumnoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MaterialRegistroEntregaAlumnoBO> GetBy(Expression<Func<TMaterialRegistroEntregaAlumno, bool>> filter)
		{
			IEnumerable<TMaterialRegistroEntregaAlumno> listado = base.GetBy(filter);
			List<MaterialRegistroEntregaAlumnoBO> listadoBO = new List<MaterialRegistroEntregaAlumnoBO>();
			foreach (var itemEntidad in listado)
			{
				MaterialRegistroEntregaAlumnoBO objetoBO = Mapper.Map<TMaterialRegistroEntregaAlumno, MaterialRegistroEntregaAlumnoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MaterialRegistroEntregaAlumnoBO FirstById(int id)
		{
			try
			{
				TMaterialRegistroEntregaAlumno entidad = base.FirstById(id);
				MaterialRegistroEntregaAlumnoBO objetoBO = new MaterialRegistroEntregaAlumnoBO();
				Mapper.Map<TMaterialRegistroEntregaAlumno, MaterialRegistroEntregaAlumnoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MaterialRegistroEntregaAlumnoBO FirstBy(Expression<Func<TMaterialRegistroEntregaAlumno, bool>> filter)
		{
			try
			{
				TMaterialRegistroEntregaAlumno entidad = base.FirstBy(filter);
				MaterialRegistroEntregaAlumnoBO objetoBO = Mapper.Map<TMaterialRegistroEntregaAlumno, MaterialRegistroEntregaAlumnoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(MaterialRegistroEntregaAlumnoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMaterialRegistroEntregaAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MaterialRegistroEntregaAlumnoBO> listadoBO)
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

		public bool Update(MaterialRegistroEntregaAlumnoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMaterialRegistroEntregaAlumno entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MaterialRegistroEntregaAlumnoBO> listadoBO)
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
		private void AsignacionId(TMaterialRegistroEntregaAlumno entidad, MaterialRegistroEntregaAlumnoBO objetoBO)
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

		private TMaterialRegistroEntregaAlumno MapeoEntidad(MaterialRegistroEntregaAlumnoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMaterialRegistroEntregaAlumno entidad = new TMaterialRegistroEntregaAlumno();
				entidad = Mapper.Map<MaterialRegistroEntregaAlumnoBO, TMaterialRegistroEntregaAlumno>(objetoBO,
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

		public RegistroEntregaMaterialTipoDTO ObtenerMaterialRegistroEntregaAlumno(int idMatriculaCabecera, int idMaterialRegistroEntregaAlumno)
		{
			try
			{
				var query = "SELECT Id, IdMatriculaCabecera, IdMaterialRegistroEntregaAlumno, IdEstadoEntregaMaterialAlumno FROM [ope].[V_TMaterialRegistroEntregaAlumno_ObtenerMaterialRegistradoAlumno] WHERE Estado = 1 AND IdMatriculaCabecera = @IdMatriculaCabecera AND IdMaterialRegistroEntregaAlumno = @IdMaterialRegistroEntregaAlumno";
				var res = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera, IdMaterialRegistroEntregaAlumno = idMaterialRegistroEntregaAlumno });
				var rpta = JsonConvert.DeserializeObject<RegistroEntregaMaterialTipoDTO>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}

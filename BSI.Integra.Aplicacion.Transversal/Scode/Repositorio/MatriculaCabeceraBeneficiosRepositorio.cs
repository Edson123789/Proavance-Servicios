using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Helper;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	/// Repositorio: Transversal/MatriculaCabeceraBeneficios
	/// Autor: Luis Huallpa.
	/// Fecha: 02/07/2021
	/// <summary>
	/// Gestión de Beneficios de Matricula Cabecera
	/// </summary>
	public class MatriculaCabeceraBeneficiosRepositorio : BaseRepository<TMatriculaCabeceraBeneficios, MatriculaCabeceraBeneficiosBO>
	{
		#region Metodos Base
		public MatriculaCabeceraBeneficiosRepositorio() : base()
		{
		}
		public MatriculaCabeceraBeneficiosRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<MatriculaCabeceraBeneficiosBO> GetBy(Expression<Func<TMatriculaCabeceraBeneficios, bool>> filter)
		{
			IEnumerable<TMatriculaCabeceraBeneficios> listado = base.GetBy(filter);
			List<MatriculaCabeceraBeneficiosBO> listadoBO = new List<MatriculaCabeceraBeneficiosBO>();
			foreach (var itemEntidad in listado)
			{
				MatriculaCabeceraBeneficiosBO objetoBO = Mapper.Map<TMatriculaCabeceraBeneficios, MatriculaCabeceraBeneficiosBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public MatriculaCabeceraBeneficiosBO FirstById(int id)
		{
			try
			{
				TMatriculaCabeceraBeneficios entidad = base.FirstById(id);
				MatriculaCabeceraBeneficiosBO objetoBO = new MatriculaCabeceraBeneficiosBO();
				Mapper.Map<TMatriculaCabeceraBeneficios, MatriculaCabeceraBeneficiosBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public MatriculaCabeceraBeneficiosBO FirstBy(Expression<Func<TMatriculaCabeceraBeneficios, bool>> filter)
		{
			try
			{
				TMatriculaCabeceraBeneficios entidad = base.FirstBy(filter);
				MatriculaCabeceraBeneficiosBO objetoBO = Mapper.Map<TMatriculaCabeceraBeneficios, MatriculaCabeceraBeneficiosBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public bool Insert(MatriculaCabeceraBeneficiosBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TMatriculaCabeceraBeneficios entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<MatriculaCabeceraBeneficiosBO> listadoBO)
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

		public bool Update(MatriculaCabeceraBeneficiosBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TMatriculaCabeceraBeneficios entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<MatriculaCabeceraBeneficiosBO> listadoBO)
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
		private void AsignacionId(TMatriculaCabeceraBeneficios entidad, MatriculaCabeceraBeneficiosBO objetoBO)
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

		private TMatriculaCabeceraBeneficios MapeoEntidad(MatriculaCabeceraBeneficiosBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TMatriculaCabeceraBeneficios entidad = new TMatriculaCabeceraBeneficios();
				entidad = Mapper.Map<MatriculaCabeceraBeneficiosBO, TMatriculaCabeceraBeneficios>(objetoBO,
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
		/// Obtiene lista de beneficios congelados por idmatriculacabecera
		/// </summary>
		/// <param name="idMatriculaCabecera"></param>
		/// <returns></returns>
		public List<string> ObtenerBeneficiosPorMatriculaCabecera(int idMatriculaCabecera)
		{
			try
			{
				return this.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabecera).OrderBy(x => x.IdSuscripcionProgramaGeneral).Select(x => x.Nombre).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

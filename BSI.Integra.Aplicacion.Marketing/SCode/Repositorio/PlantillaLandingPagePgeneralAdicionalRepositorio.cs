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
	public class PlantillaLandingPagePgeneralAdicionalRepositorio : BaseRepository<TPlantillaLandingPagePgeneralAdicional, PlantillaLandingPagePgeneralAdicionalBO>
	{
		#region Metodos Base
		public PlantillaLandingPagePgeneralAdicionalRepositorio() : base()
		{
		}
		public PlantillaLandingPagePgeneralAdicionalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PlantillaLandingPagePgeneralAdicionalBO> GetBy(Expression<Func<TPlantillaLandingPagePgeneralAdicional, bool>> filter)
		{
			IEnumerable<TPlantillaLandingPagePgeneralAdicional> listado = base.GetBy(filter);
			List<PlantillaLandingPagePgeneralAdicionalBO> listadoBO = new List<PlantillaLandingPagePgeneralAdicionalBO>();
			foreach (var itemEntidad in listado)
			{
				PlantillaLandingPagePgeneralAdicionalBO objetoBO = Mapper.Map<TPlantillaLandingPagePgeneralAdicional, PlantillaLandingPagePgeneralAdicionalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PlantillaLandingPagePgeneralAdicionalBO FirstById(int id)
		{
			try
			{
				TPlantillaLandingPagePgeneralAdicional entidad = base.FirstById(id);
				PlantillaLandingPagePgeneralAdicionalBO objetoBO = new PlantillaLandingPagePgeneralAdicionalBO();
				Mapper.Map<TPlantillaLandingPagePgeneralAdicional, PlantillaLandingPagePgeneralAdicionalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PlantillaLandingPagePgeneralAdicionalBO FirstBy(Expression<Func<TPlantillaLandingPagePgeneralAdicional, bool>> filter)
		{
			try
			{
				TPlantillaLandingPagePgeneralAdicional entidad = base.FirstBy(filter);
				PlantillaLandingPagePgeneralAdicionalBO objetoBO = Mapper.Map<TPlantillaLandingPagePgeneralAdicional, PlantillaLandingPagePgeneralAdicionalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PlantillaLandingPagePgeneralAdicionalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPlantillaLandingPagePgeneralAdicional entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PlantillaLandingPagePgeneralAdicionalBO> listadoBO)
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

		public bool Update(PlantillaLandingPagePgeneralAdicionalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPlantillaLandingPagePgeneralAdicional entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PlantillaLandingPagePgeneralAdicionalBO> listadoBO)
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
		private void AsignacionId(TPlantillaLandingPagePgeneralAdicional entidad, PlantillaLandingPagePgeneralAdicionalBO objetoBO)
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

		private TPlantillaLandingPagePgeneralAdicional MapeoEntidad(PlantillaLandingPagePgeneralAdicionalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPlantillaLandingPagePgeneralAdicional entidad = new TPlantillaLandingPagePgeneralAdicional();
				entidad = Mapper.Map<PlantillaLandingPagePgeneralAdicionalBO, TPlantillaLandingPagePgeneralAdicional>(objetoBO,
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
        /// Obtieene los registros por el IdPlatillaLandingPage
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public List<PlantillaLandingPagePgeneralAdicionalDTO> ObtenerAdicionales (int idPlantilla)
        {
            try
            {
                return GetBy(x => x.IdPlantillaLandingPage == idPlantilla, y => new PlantillaLandingPagePgeneralAdicionalDTO
                {
                    Id = y.Id,
                    IdTitulo = y.IdTitulo,
                    NombreTitulo = y.NombreTitulo,
                    ColorTitulo = y.ColorTitulo,
                    ColorDescripcion = y.ColorDescripcion
                }
                ).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los adicionales por PlantillaLandingPage
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPlantillaLAndingPage, string usuario, List<PlantillaLandingPagePgeneralAdicionalDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                listaBorrar = GetBy(x => x.IdPlantillaLandingPage == idPlantillaLAndingPage, y => new EliminacionIdsDTO
                {
                    Id = y.Id
                }
                ).ToList();
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

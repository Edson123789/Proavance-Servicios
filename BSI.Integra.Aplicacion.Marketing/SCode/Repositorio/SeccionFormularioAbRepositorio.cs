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
	public class SeccionFormularioAbRepositorio : BaseRepository<TSeccionFormularioAb, SeccionFormularioAbBO>
	{
		#region Metodos Base
		public SeccionFormularioAbRepositorio() : base()
		{
		}
		public SeccionFormularioAbRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<SeccionFormularioAbBO> GetBy(Expression<Func<TSeccionFormularioAb, bool>> filter)
		{
			IEnumerable<TSeccionFormularioAb> listado = base.GetBy(filter);
			List<SeccionFormularioAbBO> listadoBO = new List<SeccionFormularioAbBO>();
			foreach (var itemEntidad in listado)
			{
				SeccionFormularioAbBO objetoBO = Mapper.Map<TSeccionFormularioAb, SeccionFormularioAbBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public SeccionFormularioAbBO FirstById(int id)
		{
			try
			{
				TSeccionFormularioAb entidad = base.FirstById(id);
				SeccionFormularioAbBO objetoBO = new SeccionFormularioAbBO();
				Mapper.Map<TSeccionFormularioAb, SeccionFormularioAbBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public SeccionFormularioAbBO FirstBy(Expression<Func<TSeccionFormularioAb, bool>> filter)
		{
			try
			{
				TSeccionFormularioAb entidad = base.FirstBy(filter);
				SeccionFormularioAbBO objetoBO = Mapper.Map<TSeccionFormularioAb, SeccionFormularioAbBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(SeccionFormularioAbBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TSeccionFormularioAb entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<SeccionFormularioAbBO> listadoBO)
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

		public bool Update(SeccionFormularioAbBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TSeccionFormularioAb entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<SeccionFormularioAbBO> listadoBO)
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
		private void AsignacionId(TSeccionFormularioAb entidad, SeccionFormularioAbBO objetoBO)
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

		private TSeccionFormularioAb MapeoEntidad(SeccionFormularioAbBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TSeccionFormularioAb entidad = new TSeccionFormularioAb();
				entidad = Mapper.Map<SeccionFormularioAbBO, TSeccionFormularioAb>(objetoBO,
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

        public List<SeccionFormularioAbDTO> ObtenerPorIdFormularioLandingAB(int idFormularioLandingAB)
        {
            try
            {
                var lista = GetBy(x => x.IdFormularioLandingAb == idFormularioLandingAB, y => new SeccionFormularioAbDTO
                {
                    Id = y.Id,
                    NombreTitulo = y.NombreTitulo,
                    Descripcion = y.Descripcion,
                    NombreImagen = y.Imagen,
                    ColorTitulo = y.ColorTitulo,
                    ColorDescripcion = y.ColorDescripcion
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteLogicoPorIdFormularioLanding(int idFormularioLanding, string usuario, List<SeccionFormularioAbDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdFormularioLandingAb == idFormularioLanding, y => new EliminacionIdsDTO
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

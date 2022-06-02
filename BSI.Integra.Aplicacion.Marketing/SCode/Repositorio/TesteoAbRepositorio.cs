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
	public class TesteoAbRepositorio : BaseRepository<TTesteoAb, TesteoAbBO>
	{
		#region Metodos Base
		public TesteoAbRepositorio() : base()
		{
		}
		public TesteoAbRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TesteoAbBO> GetBy(Expression<Func<TTesteoAb, bool>> filter)
		{
			IEnumerable<TTesteoAb> listado = base.GetBy(filter);
			List<TesteoAbBO> listadoBO = new List<TesteoAbBO>();
			foreach (var itemEntidad in listado)
			{
				TesteoAbBO objetoBO = Mapper.Map<TTesteoAb, TesteoAbBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TesteoAbBO FirstById(int id)
		{
			try
			{
				TTesteoAb entidad = base.FirstById(id);
				TesteoAbBO objetoBO = new TesteoAbBO();
				Mapper.Map<TTesteoAb, TesteoAbBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TesteoAbBO FirstBy(Expression<Func<TTesteoAb, bool>> filter)
		{
			try
			{
				TTesteoAb entidad = base.FirstBy(filter);
				TesteoAbBO objetoBO = Mapper.Map<TTesteoAb, TesteoAbBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TesteoAbBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTesteoAb entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TesteoAbBO> listadoBO)
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

		public bool Update(TesteoAbBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTesteoAb entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TesteoAbBO> listadoBO)
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
		private void AsignacionId(TTesteoAb entidad, TesteoAbBO objetoBO)
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

		private TTesteoAb MapeoEntidad(TesteoAbBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTesteoAb entidad = new TTesteoAb();
				entidad = Mapper.Map<TesteoAbBO, TTesteoAb>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.FormularioLandingAb != null )
                {
                    TFormularioLandingAb entidadHijo = new TFormularioLandingAb();
                    entidadHijo = Mapper.Map<FormularioLandingAbBO, TFormularioLandingAb>(objetoBO.FormularioLandingAb,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TFormularioLandingAb.Add(entidadHijo);

                    //mapea al hijo interno
                    if (objetoBO.FormularioLandingAb.ListaSeccionFormularioAbBO != null && objetoBO.FormularioLandingAb.ListaSeccionFormularioAbBO.Count > 0)
                    {
                        foreach (var hijo in objetoBO.FormularioLandingAb.ListaSeccionFormularioAbBO)
                        {
                            TSeccionFormularioAb entidadHijo2 = new TSeccionFormularioAb();
                            entidadHijo2 = Mapper.Map<SeccionFormularioAbBO, TSeccionFormularioAb>(
                                hijo,
                                opt => opt.ConfigureMap(MemberList.None)
                                    );
                            entidadHijo.TSeccionFormularioAb.Add(entidadHijo2);
                        }
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
        /// Obtiene los Registros por IdFormularioLandingPage
        /// </summary>
        /// <param name="idFormularioLandingPage"></param>
        /// <returns></returns>
        public List<TesteoAbDTO> ObtenerPorIdFormularioLandingPage(int idFormularioLandingPage)
        {
            try
            {
                var lista = GetBy(x => x.IdFormularioLandingPage == idFormularioLandingPage, y => new TesteoAbDTO
                {
                    Id = y.Id,
                    IdPlantillaLandingPage = y.IdPlantillaLandingPage,
                    NombrePlantilla = y.NombrePlantilla,
                    Cantidad = y.Cantidad,
                    Nombre = y.Nombre,
                    Porcentaje = y.Porcentaje,
                }).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

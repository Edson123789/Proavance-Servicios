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
	public class FormularioLandingAbRepositorio : BaseRepository<TFormularioLandingAb, FormularioLandingAbBO>
	{
		#region Metodos Base
		public FormularioLandingAbRepositorio() : base()
		{
		}
		public FormularioLandingAbRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FormularioLandingAbBO> GetBy(Expression<Func<TFormularioLandingAb, bool>> filter)
		{
			IEnumerable<TFormularioLandingAb> listado = base.GetBy(filter);
			List<FormularioLandingAbBO> listadoBO = new List<FormularioLandingAbBO>();
			foreach (var itemEntidad in listado)
			{
				FormularioLandingAbBO objetoBO = Mapper.Map<TFormularioLandingAb, FormularioLandingAbBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FormularioLandingAbBO FirstById(int id)
		{
			try
			{
				TFormularioLandingAb entidad = base.FirstById(id);
				FormularioLandingAbBO objetoBO = new FormularioLandingAbBO();
				Mapper.Map<TFormularioLandingAb, FormularioLandingAbBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FormularioLandingAbBO FirstBy(Expression<Func<TFormularioLandingAb, bool>> filter)
		{
			try
			{
				TFormularioLandingAb entidad = base.FirstBy(filter);
				FormularioLandingAbBO objetoBO = Mapper.Map<TFormularioLandingAb, FormularioLandingAbBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FormularioLandingAbBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFormularioLandingAb entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FormularioLandingAbBO> listadoBO)
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

		public bool Update(FormularioLandingAbBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFormularioLandingAb entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FormularioLandingAbBO> listadoBO)
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
		private void AsignacionId(TFormularioLandingAb entidad, FormularioLandingAbBO objetoBO)
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

		private TFormularioLandingAb MapeoEntidad(FormularioLandingAbBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFormularioLandingAb entidad = new TFormularioLandingAb();
				entidad = Mapper.Map<FormularioLandingAbBO, TFormularioLandingAb>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaSeccionFormularioAbBO != null && objetoBO.ListaSeccionFormularioAbBO.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaSeccionFormularioAbBO)
                    {
                        TSeccionFormularioAb entidadHijo = new TSeccionFormularioAb();
                        entidadHijo = Mapper.Map<SeccionFormularioAbBO, TSeccionFormularioAb>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSeccionFormularioAb.Add(entidadHijo);
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

        public FormularioLandingAbDTO ObtenerPorIdTesteoAB(int idTesteoAB)
        {
            try
            {
                var lista = FirstBy(x => x.IdTesteoAb == idTesteoAB, y => new FormularioLandingAbDTO
                {
                    Id = y.Id,
                    TextoFormulario = y.TextoFormulario,
                    NombrePrograma = y.NombrePrograma,
                    Descripcion = y.Descripcion
                });

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

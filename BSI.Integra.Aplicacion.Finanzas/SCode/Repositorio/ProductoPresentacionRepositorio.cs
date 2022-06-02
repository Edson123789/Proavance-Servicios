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
    public class ProductoPresentacionRepositorio : BaseRepository<TProductoPresentacion, ProductoPresentacionBO>
    {
        #region Metodos Base
        public ProductoPresentacionRepositorio() : base()
        {
        }
        public ProductoPresentacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProductoPresentacionBO> GetBy(Expression<Func<TProductoPresentacion, bool>> filter)
        {
            IEnumerable<TProductoPresentacion> listado = base.GetBy(filter);
            List<ProductoPresentacionBO> listadoBO = new List<ProductoPresentacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProductoPresentacionBO objetoBO = Mapper.Map<TProductoPresentacion, ProductoPresentacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProductoPresentacionBO FirstById(int id)
        {
            try
            {
                TProductoPresentacion entidad = base.FirstById(id);
                ProductoPresentacionBO objetoBO = new ProductoPresentacionBO();
                Mapper.Map<TProductoPresentacion, ProductoPresentacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProductoPresentacionBO FirstBy(Expression<Func<TProductoPresentacion, bool>> filter)
        {
            try
            {
                TProductoPresentacion entidad = base.FirstBy(filter);
                ProductoPresentacionBO objetoBO = Mapper.Map<TProductoPresentacion, ProductoPresentacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProductoPresentacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProductoPresentacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProductoPresentacionBO> listadoBO)
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

        public bool Update(ProductoPresentacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProductoPresentacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProductoPresentacionBO> listadoBO)
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
        private void AsignacionId(TProductoPresentacion entidad, ProductoPresentacionBO objetoBO)
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

        private TProductoPresentacion MapeoEntidad(ProductoPresentacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProductoPresentacion entidad = new TProductoPresentacion();
                entidad = Mapper.Map<ProductoPresentacionBO, TProductoPresentacion>(objetoBO,
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
		/// Obtiene lista de producto presentacion para combo en programa especifico sesion fur
		/// </summary>
		/// <returns></returns>
		public ICollection<FiltroDTO> ObtenerProductoPresentacionParaCombo()
		{
			try
			{
				var combo = GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre });
				return combo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}

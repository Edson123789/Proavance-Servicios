using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class ProductoRepositorio : BaseRepository<TProducto, ProductoBO>
    {
        #region Metodos Base
        public ProductoRepositorio() : base()
        {
        }
        public ProductoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProductoBO> GetBy(Expression<Func<TProducto, bool>> filter)
        {
            IEnumerable<TProducto> listado = base.GetBy(filter);
            List<ProductoBO> listadoBO = new List<ProductoBO>();
            foreach (var itemEntidad in listado)
            {
                ProductoBO objetoBO = Mapper.Map<TProducto, ProductoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProductoBO FirstById(int id)
        {
            try
            {
                TProducto entidad = base.FirstById(id);
                ProductoBO objetoBO = new ProductoBO();
                Mapper.Map<TProducto, ProductoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProductoBO FirstBy(Expression<Func<TProducto, bool>> filter)
        {
            try
            {
                TProducto entidad = base.FirstBy(filter);
                ProductoBO objetoBO = Mapper.Map<TProducto, ProductoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProductoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProducto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProductoBO> listadoBO)
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

        public bool Update(ProductoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProducto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProductoBO> listadoBO)
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
        private void AsignacionId(TProducto entidad, ProductoBO objetoBO)
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

        private TProducto MapeoEntidad(ProductoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProducto entidad = new TProducto();
                entidad = Mapper.Map<ProductoBO, TProducto>(objetoBO,
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

        public List<ProductoCuentaContableDTO> ObtenerProductoCuentaContable(int idProducto)
        {
            try
            {
                List<ProductoCuentaContableDTO> ProductoCuentaContableObjeto = new List<ProductoCuentaContableDTO>();
                var _query = "SELECT IdProducto,NombreProducto,DescripcionProducto,CuentaEspecifica,IdProductoPresentacion FROM FIN.V_TProductoCuentaContable where IdProducto=@idProducto order by IdProducto desc";
                var planContableBD = _dapper.QueryDapper(_query, new { idProducto });
                if (!planContableBD.Contains("[]") && !string.IsNullOrEmpty(planContableBD))
                {
                    ProductoCuentaContableObjeto = JsonConvert.DeserializeObject<List<ProductoCuentaContableDTO>>(planContableBD);
                }   
                return ProductoCuentaContableObjeto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ProductoDTO InsertarProducto(ProductoDTO objetoProducto,integraDBContext context)
        {
           
            try
            {
                ProductoRepositorio _repProductoRep = new ProductoRepositorio(context);
                ProductoBO producto = new ProductoBO();
                    producto.Nombre = objetoProducto.Nombre;
                    producto.Descripcion = objetoProducto.Descripcion;
                    producto.CuentaGeneral = objetoProducto.CuentaGeneral;
                    producto.CuentaGeneralCodigo = objetoProducto.CuentaGeneralCodigo;
                    producto.CuentaEspecifica = objetoProducto.CuentaEspecifica;
                    producto.CuentaEspecificaCodigo = objetoProducto.CuentaEspecificaCodigo;
                    producto.IdProductoPresentacion = objetoProducto.IdProductoPresentacion;
                    producto.Estado = true;
                    producto.UsuarioCreacion = objetoProducto.UsuarioModificacion;
                    producto.UsuarioModificacion = objetoProducto.UsuarioModificacion;
                    producto.FechaModificacion = DateTime.Now;
                    producto.FechaCreacion = DateTime.Now;

                    _repProductoRep.Insert(producto);
                objetoProducto.Id = producto.Id;
                return objetoProducto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarProducto(ProductoDTO objetoProducto, integraDBContext context)
        {
            try
            {
                ProductoRepositorio _repProductoRep = new ProductoRepositorio(context);
                ProductoBO producto = new ProductoBO();
                producto = _repProductoRep.FirstById(objetoProducto.Id);
                    producto.Nombre = objetoProducto.Nombre;
                    producto.Descripcion = objetoProducto.Descripcion;
                    producto.CuentaGeneral = objetoProducto.CuentaGeneral;
                    producto.CuentaGeneralCodigo = objetoProducto.CuentaGeneralCodigo;
                    producto.CuentaEspecifica = objetoProducto.CuentaEspecifica;
                    producto.CuentaEspecificaCodigo = objetoProducto.CuentaEspecificaCodigo;
                    producto.IdProductoPresentacion = objetoProducto.IdProductoPresentacion;
                    producto.UsuarioModificacion = objetoProducto.UsuarioModificacion;
                    producto.FechaModificacion = DateTime.Now;

                    _repProductoRep.Update(producto);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los campos Id , Nombre del Producto filtrado por el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ProveedorPorProductoAutocompleteDTO> ObtenerProductoAutocomplete(string nombre)
        {
            try
            {
                List<ProveedorPorProductoAutocompleteDTO> obtenerProductoFiltroNombre = new List<ProveedorPorProductoAutocompleteDTO>();
                var _query = "SELECT  Id, Nombre  FROM fin.V_AutoCompleteProducto WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ";
                var obtenerProductoFiltroNombreDB = _dapper.QueryDapper(_query, new { nombre });
                if (!string.IsNullOrEmpty(obtenerProductoFiltroNombreDB) && !obtenerProductoFiltroNombreDB.Contains("[]"))
                {
                    obtenerProductoFiltroNombre = JsonConvert.DeserializeObject<List<ProveedorPorProductoAutocompleteDTO>>(obtenerProductoFiltroNombreDB);
                }
                return obtenerProductoFiltroNombre;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DetalleHistoricoFiltroDTO> ObtenerDetalleHistorio(int idProducto, int idProveedor)
        {
            try
            {
                List<DetalleHistoricoFiltroDTO> obtenerDetalleHistorico = new List<DetalleHistoricoFiltroDTO>();
                var obtenerDetalleHistoricoDB = _dapper.QuerySPDapper("pla.SP_ObtenerDetalleHistorico", new { idProducto, idProveedor});
                if (!string.IsNullOrEmpty(obtenerDetalleHistoricoDB) && !obtenerDetalleHistoricoDB.Contains("[]"))
                {
                    obtenerDetalleHistorico = JsonConvert.DeserializeObject<List<DetalleHistoricoFiltroDTO>>(obtenerDetalleHistoricoDB);
                }
                return obtenerDetalleHistorico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene lista de productos para combo de programa especifico FUR
		/// </summary>
		/// <returns></returns>
		public ICollection<ProveedorPorProductoAutocompleteDTO> ObtenerListaProductoParaCombo()
		{
			try
			{
				var combo = GetBy(x => x.Estado == true, x => new ProveedorPorProductoAutocompleteDTO { Id = x.Id, Nombre = x.Nombre });
				return combo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene lista de productos de materiales para combo de programa especifico FUR
		/// </summary>
		/// <returns></returns>
		public ICollection<ProveedorPorProductoAutocompleteDTO> ObtenerListaProductoMaterialesParaCombo()
		{
			try
			{
				var combo = GetBy(x => x.Estado == true && x.Descripcion.Contains("MATERIALES DE ENSEÑANZA ALUMNOS"), x => new ProveedorPorProductoAutocompleteDTO { Id = x.Id, Nombre = x.Nombre });
				return combo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

	}
}

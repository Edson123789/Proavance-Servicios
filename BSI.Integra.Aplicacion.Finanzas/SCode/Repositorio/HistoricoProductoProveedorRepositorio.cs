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
    public class HistoricoProductoProveedorRepositorio : BaseRepository<THistoricoProductoProveedor, HistoricoProductoProveedorBO>
    {
        #region Metodos Base
        public HistoricoProductoProveedorRepositorio() : base()
        {
        }
        public HistoricoProductoProveedorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<HistoricoProductoProveedorBO> GetBy(Expression<Func<THistoricoProductoProveedor, bool>> filter)
        {
            IEnumerable<THistoricoProductoProveedor> listado = base.GetBy(filter);
            List<HistoricoProductoProveedorBO> listadoBO = new List<HistoricoProductoProveedorBO>();
            foreach (var itemEntidad in listado)
            {
                HistoricoProductoProveedorBO objetoBO = Mapper.Map<THistoricoProductoProveedor, HistoricoProductoProveedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public HistoricoProductoProveedorBO FirstById(int id)
        {
            try
            {
                THistoricoProductoProveedor entidad = base.FirstById(id);
                HistoricoProductoProveedorBO objetoBO = new HistoricoProductoProveedorBO();
                Mapper.Map<THistoricoProductoProveedor, HistoricoProductoProveedorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public HistoricoProductoProveedorBO FirstBy(Expression<Func<THistoricoProductoProveedor, bool>> filter)
        {
            try
            {
                THistoricoProductoProveedor entidad = base.FirstBy(filter);
                HistoricoProductoProveedorBO objetoBO = Mapper.Map<THistoricoProductoProveedor, HistoricoProductoProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(HistoricoProductoProveedorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                THistoricoProductoProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<HistoricoProductoProveedorBO> listadoBO)
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

        public bool Update(HistoricoProductoProveedorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                THistoricoProductoProveedor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<HistoricoProductoProveedorBO> listadoBO)
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
        private void AsignacionId(THistoricoProductoProveedor entidad, HistoricoProductoProveedorBO objetoBO)
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

        private THistoricoProductoProveedor MapeoEntidad(HistoricoProductoProveedorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                THistoricoProductoProveedor entidad = new THistoricoProductoProveedor();
                entidad = Mapper.Map<HistoricoProductoProveedorBO, THistoricoProductoProveedor>(objetoBO,
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

        public List<HistoricoProductoProveedorVersionDTO> ObtenerHistoricoUltimaVersion(int? IdHistoricoPP)
        {
            try
            {
                int? idHistorico = IdHistoricoPP;
                List<HistoricoProductoProveedorVersionDTO> historicoUltimaVersion = new List<HistoricoProductoProveedorVersionDTO>();
                var _query = "";
                if (idHistorico != null && idHistorico != 0)
                {
                    _query = "SELECT Id,Producto,IdProducto,Proveedor,IdProveedor,IdCondicionPago,CondicionPago,Moneda,IdMoneda,Precio,IdTipoPago,TipoPago,Observaciones,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_ObtenerProductosPrecioHistorico where Estado = 1 and Id=@idHistorico ORDER BY Id desc";
                }
                else
                {
                    _query = "SELECT Id,Producto,IdProducto,Proveedor,IdProveedor,IdCondicionPago,CondicionPago,Moneda,IdMoneda,Precio,IdTipoPago,TipoPago,Observaciones,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_ObtenerProductosPrecioHistorico where Estado = 1 ORDER BY Id desc";
                }
                var historicoDB = _dapper.QueryDapper(_query, new { idHistorico });
                if (!historicoDB.Contains("[]") && !string.IsNullOrEmpty(historicoDB))
                {
                    historicoUltimaVersion = JsonConvert.DeserializeObject<List<HistoricoProductoProveedorVersionDTO>>(historicoDB);
                }
                return historicoUltimaVersion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<HistoricoPPFiltroDTO> ObtenerNombreHistoricoAutocomplete(string valor)
        {
            try
            {
                List<HistoricoPPFiltroDTO> historico = new List<HistoricoPPFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id ,Nombre FROM FIN.V_ObtenerNombreHistoricoPP WHERE Nombre LIKE CONCAT('%',@valor,'%') ORDER By Nombre ASC";
                var historicoDB = _dapper.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(historicoDB) && !historicoDB.Contains("[]"))
                {
                    historico = JsonConvert.DeserializeObject<List<HistoricoPPFiltroDTO>>(historicoDB);
                }
                return historico;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       

		/// <summary>
		/// Obtiene detalle del fur a registrar mediante idproducto y id proveedor
		/// </summary>
		/// <param name="idProducto"></param>
		/// <param name="idProveedor"></param>
		/// <returns></returns>
		public DetalleFurDTO ObtenerDetalleFUR(int idProducto, int idProveedor)
		{
			try
			{
				var res = _dapper.QuerySPFirstOrDefault("fin.SP_ObtenerDetalleSesionesFUR", new { IdProducto = idProducto, IdProveedor = idProveedor });
				return JsonConvert.DeserializeObject<DetalleFurDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


        /// <summary>
		/// Obtiene lista de productos con su precio asociados a un proveedor para (Utilizado para llenado de ComboBox en modulo CostosFijos)
		/// </summary>
		/// <returns></returns>
		public ICollection<ProductoPorProveedorDTO> ObtenerListaProductoPorProveedor(int IdProveedor)
        {
            try
            {
                List<ProductoPorProveedorDTO> productos = new List<ProductoPorProveedorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id as IdHistoricoProveedorProducto, IdProducto As Id, Nombre, Precio, IdMoneda FROM fin.V_ObtenerProductoPorProveedor WHERE IdProveedor=" + IdProveedor;
                var productosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(productosDB) && !productosDB.Contains("[]"))
                {
                    productos = JsonConvert.DeserializeObject<List<ProductoPorProveedorDTO>>(productosDB);
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

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
    public class FurTipoPedidoRepositorio : BaseRepository<TFurTipoPedido, FurTipoPedidoBO>
    {
        #region Metodos Base
        public FurTipoPedidoRepositorio() : base()
        {
        }
        public FurTipoPedidoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurTipoPedidoBO> GetBy(Expression<Func<TFurTipoPedido, bool>> filter)
        {
            IEnumerable<TFurTipoPedido> listado = base.GetBy(filter);
            List<FurTipoPedidoBO> listadoBO = new List<FurTipoPedidoBO>();
            foreach (var itemEntidad in listado)
            {
                FurTipoPedidoBO objetoBO = Mapper.Map<TFurTipoPedido, FurTipoPedidoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurTipoPedidoBO FirstById(int id)
        {
            try
            {
                TFurTipoPedido entidad = base.FirstById(id);
                FurTipoPedidoBO objetoBO = new FurTipoPedidoBO();
                Mapper.Map<TFurTipoPedido, FurTipoPedidoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurTipoPedidoBO FirstBy(Expression<Func<TFurTipoPedido, bool>> filter)
        {
            try
            {
                TFurTipoPedido entidad = base.FirstBy(filter);
                FurTipoPedidoBO objetoBO = Mapper.Map<TFurTipoPedido, FurTipoPedidoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurTipoPedidoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurTipoPedido entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurTipoPedidoBO> listadoBO)
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

        public bool Update(FurTipoPedidoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurTipoPedido entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurTipoPedidoBO> listadoBO)
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
        private void AsignacionId(TFurTipoPedido entidad, FurTipoPedidoBO objetoBO)
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

        private TFurTipoPedido MapeoEntidad(FurTipoPedidoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurTipoPedido entidad = new TFurTipoPedido();
                entidad = Mapper.Map<FurTipoPedidoBO, TFurTipoPedido>(objetoBO,
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
        /// Se obtiene todos los tipos de pedidos de fur registrados en la tabla (utilizado para llenado de ComboBox)
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFurTiposPedidos()
        {
            try
            {
                var _query = "SELECT  Id, Nombre FROM fin.T_FurTipoPedido WHERE (Nombre like '%Gasto Inmediato%' or Nombre like '%Compras a Credito%' or Nombre like '%Otro Tipo%') and Estado = 1";
                var TiposPedidoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(TiposPedidoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

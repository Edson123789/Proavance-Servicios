using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoPagoCategoriaRepositorio : BaseRepository<TTipoPagoCategoria, TipoPagoCategoriaBO>
    {
        #region Metodos Base
        public TipoPagoCategoriaRepositorio() : base()
        {
        }
        public TipoPagoCategoriaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoPagoCategoriaBO> GetBy(Expression<Func<TTipoPagoCategoria, bool>> filter)
        {
            IEnumerable<TTipoPagoCategoria> listado = base.GetBy(filter);
            List<TipoPagoCategoriaBO> listadoBO = new List<TipoPagoCategoriaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoPagoCategoriaBO objetoBO = Mapper.Map<TTipoPagoCategoria, TipoPagoCategoriaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoPagoCategoriaBO FirstById(int id)
        {
            try
            {
                TTipoPagoCategoria entidad = base.FirstById(id);
                TipoPagoCategoriaBO objetoBO = new TipoPagoCategoriaBO();
                Mapper.Map<TTipoPagoCategoria, TipoPagoCategoriaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoPagoCategoriaBO FirstBy(Expression<Func<TTipoPagoCategoria, bool>> filter)
        {
            try
            {
                TTipoPagoCategoria entidad = base.FirstBy(filter);
                TipoPagoCategoriaBO objetoBO = Mapper.Map<TTipoPagoCategoria, TipoPagoCategoriaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoPagoCategoriaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPagoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoPagoCategoriaBO> listadoBO)
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

        public bool Update(TipoPagoCategoriaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPagoCategoria entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoPagoCategoriaBO> listadoBO)
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
        private void AsignacionId(TTipoPagoCategoria entidad, TipoPagoCategoriaBO objetoBO)
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

        private TTipoPagoCategoria MapeoEntidad(TipoPagoCategoriaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPagoCategoria entidad = new TTipoPagoCategoria();
                entidad = Mapper.Map<TipoPagoCategoriaBO, TTipoPagoCategoria>(objetoBO,
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
        /// Elimina (Actualiza estado a false ) todos los Beneficios asociados a un programa
        /// </summary>
        /// <param name="idTipoPago"></param>
        /// <returns></returns>
        public List<int> EliminacionLogicoPorTipoPagoCategoria(int idTipoPago, string usuario, List<TipoPagoCategoriaDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdTipoPago == idTipoPago && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.IdCategoriaPrograma == x.IdCategoriaPrograma));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
                List<int> result = new List<int>();
                result = listaBorrar.Select(x => x.Id).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        ///  Obtiene la lista de tipo pagos (activo) con Id, Nombre  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public List<TipoPagoCategoriaFiltroDTO> ObtenerTipoPagoPorCategoria(int categoria)
        {
            try
            {
                List<TipoPagoCategoriaFiltroDTO> items = new List<TipoPagoCategoriaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_TTipoPagoCategoria_Filtro WHERE EstadoTipoPago = 1 and EstadoCategoriaPrograma = 1 and IdCategoriaPrograma = @IdCategoriaPrograma";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdCategoriaPrograma = categoria});
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<TipoPagoCategoriaFiltroDTO>>(respuestaDapper);
                    items = items.GroupBy(i => i.Id).Select(i => i.FirstOrDefault()).ToList();
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

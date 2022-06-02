using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoPagoRepositorio : BaseRepository<TTipoPago, TipoPagoBO>
    {
        #region Metodos Base
        public TipoPagoRepositorio() : base()
        {
        }
        public TipoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoPagoBO> GetBy(Expression<Func<TTipoPago, bool>> filter)
        {
            IEnumerable<TTipoPago> listado = base.GetBy(filter);
            List<TipoPagoBO> listadoBO = new List<TipoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoPagoBO objetoBO = Mapper.Map<TTipoPago, TipoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoPagoBO FirstById(int id)
        {
            try
            {
                TTipoPago entidad = base.FirstById(id);
                TipoPagoBO objetoBO = new TipoPagoBO();
                Mapper.Map<TTipoPago, TipoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoPagoBO FirstBy(Expression<Func<TTipoPago, bool>> filter)
        {
            try
            {
                TTipoPago entidad = base.FirstBy(filter);
                TipoPagoBO objetoBO = Mapper.Map<TTipoPago, TipoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoPagoBO> listadoBO)
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

        public bool Update(TipoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoPagoBO> listadoBO)
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
        private void AsignacionId(TTipoPago entidad, TipoPagoBO objetoBO)
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

        private TTipoPago MapeoEntidad(TipoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoPago entidad = new TTipoPago();
                entidad = Mapper.Map<TipoPagoBO, TTipoPago>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.TipoPagoCategoria != null && objetoBO.TipoPagoCategoria.Count > 0)
                {
                    foreach (var hijo in objetoBO.TipoPagoCategoria)
                    {
                        TTipoPagoCategoria entidadHijo = new TTipoPagoCategoria();
                        entidadHijo = Mapper.Map<TipoPagoCategoriaBO, TTipoPagoCategoria>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TTipoPagoCategoria.Add(entidadHijo);
                        
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
        ///  Obtiene la lista de tipos de pagos con sus categoria de Programa registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<TipoPagoDTO> ListarTiposPagosPanel()
        {
            try
            {
                List<TipoPagoDTO> tipoPagos = new List<TipoPagoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion, Cuotas, Suscripciones, PorDefecto" +
                    " FROM pla.T_TipoPago WHERE Estado = 1 order by Id desc";
                var pgeneralDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    tipoPagos = JsonConvert.DeserializeObject<List<TipoPagoDTO>>(pgeneralDB);
                }

                return tipoPagos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
        /// <summary>
        ///  Obtiene la lista de tipos de pagos con sus categoria de Programa por tipo pago registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<TipoPagoCategoriaProgramaDTO> ObtenerCategoriaProgramaPorTipoPago(int idTipoPago)
        {
            try
            {
                List<TipoPagoCategoriaProgramaDTO> tipoPagos = new List<TipoPagoCategoriaProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, IdCategoriaPrograma" +
                    " FROM pla.V_TTipoPagoCategoria_CategoriaFiltro WHERE Estado = 1 and IdTipoPago = @IdTipoPago";
                var query = _dapper.QueryDapper(_query, new { IdTipoPago = idTipoPago});
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tipoPagos = JsonConvert.DeserializeObject<List<TipoPagoCategoriaProgramaDTO>>(query);
                }

                return tipoPagos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        /// <summary>
        ///  Obtiene la lista de tipos de pagos(actual)  registrados en el sistema
        ///  con todos sus campos excepto los de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<TipoPagoFiltroDTO> TipoPagoFiltro()
        {
            try
            {
                List<TipoPagoFiltroDTO> tipoPagos = new List<TipoPagoFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre,Cuotas FROM pla.V_TipoPago_Filtro WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    tipoPagos = JsonConvert.DeserializeObject<List<TipoPagoFiltroDTO>>(pgeneralDB);
                }

                return tipoPagos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        /// <summary>
		/// Obtiene registro Id, Nombre para filtro
		/// </summary>
		/// <returns></returns>
		public List<FiltroGenericoDTO> ObtenerFiltroTipoPago()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroGenericoDTO { Value = x.Id, Text = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

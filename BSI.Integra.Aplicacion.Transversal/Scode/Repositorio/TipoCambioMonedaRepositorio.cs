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
    /// Repositorio: Finanzas/TipoCambio
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Contiene funciones para obtener los tipos de cambio
    /// </summary>
    public class TipoCambioMonedaRepositorio : BaseRepository<TTipoCambioMoneda, TipoCambioMonedaBO>
    {
        #region Metodos Base
        public TipoCambioMonedaRepositorio() : base()
        {
        }
        public TipoCambioMonedaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCambioMonedaBO> GetBy(Expression<Func<TTipoCambioMoneda, bool>> filter)
        {
            IEnumerable<TTipoCambioMoneda> listado = base.GetBy(filter);
            List<TipoCambioMonedaBO> listadoBO = new List<TipoCambioMonedaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCambioMonedaBO objetoBO = Mapper.Map<TTipoCambioMoneda, TipoCambioMonedaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCambioMonedaBO FirstById(int id)
        {
            try
            {
                TTipoCambioMoneda entidad = base.FirstById(id);
                TipoCambioMonedaBO objetoBO = new TipoCambioMonedaBO();
                Mapper.Map<TTipoCambioMoneda, TipoCambioMonedaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCambioMonedaBO FirstBy(Expression<Func<TTipoCambioMoneda, bool>> filter)
        {
            try
            {
                TTipoCambioMoneda entidad = base.FirstBy(filter);
                TipoCambioMonedaBO objetoBO = Mapper.Map<TTipoCambioMoneda, TipoCambioMonedaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCambioMonedaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCambioMoneda entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCambioMonedaBO> listadoBO)
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

        public bool Update(TipoCambioMonedaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCambioMoneda entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCambioMonedaBO> listadoBO)
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
        private void AsignacionId(TTipoCambioMoneda entidad, TipoCambioMonedaBO objetoBO)
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

        private TTipoCambioMoneda MapeoEntidad(TipoCambioMonedaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCambioMoneda entidad = new TTipoCambioMoneda();
                entidad = Mapper.Map<TipoCambioMonedaBO, TTipoCambioMoneda>(objetoBO,
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
        /// Obtiene los campos principales 
        /// </summary>
        /// <returns></returns>
        public List<TipoCambioMonedaGridDTO> Obtener()
        {
            try
            {
                List<TipoCambioMonedaGridDTO> tipoCambioMoneda = new List<TipoCambioMonedaGridDTO>();
                var _query = "SELECT Id, NombreMoneda, IdMoneda, DolarAMoneda, MonedaADolar, Fecha, FechaCreacion FROM fin.V_ObtenerTipoCambioMoneda WHERE EstadoTipoCambioMoneda = 1 and EstadoMoneda = 1";
                var tipoCambioMonedaDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(tipoCambioMonedaDB) && !tipoCambioMonedaDB.Contains("[]"))
                {
                    tipoCambioMoneda = JsonConvert.DeserializeObject<List<TipoCambioMonedaGridDTO>>(tipoCambioMonedaDB);
                    tipoCambioMoneda = tipoCambioMoneda.OrderByDescending(x => x.Fecha).ToList();
                }
                return tipoCambioMoneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

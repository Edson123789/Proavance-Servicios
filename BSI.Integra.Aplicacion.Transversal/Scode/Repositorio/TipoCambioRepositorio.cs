using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/TipoCambio
    /// Autor: Lisbeth Ortogorin Condori
    /// Fecha: 20/02/2021
    /// <summary>
    /// Contiene funciones para obtener los tipos de cambio
    /// </summary>

    public class TipoCambioRepositorio : BaseRepository<TTipoCambio, TipoCambioBO>
    {
        #region Metodos Base
        public TipoCambioRepositorio() : base()
        {
        }
        public TipoCambioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCambioBO> GetBy(Expression<Func<TTipoCambio, bool>> filter)
        {
            IEnumerable<TTipoCambio> listado = base.GetBy(filter);
            List<TipoCambioBO> listadoBO = new List<TipoCambioBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCambioBO objetoBO = Mapper.Map<TTipoCambio, TipoCambioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCambioBO FirstById(int id)
        {
            try
            {
                TTipoCambio entidad = base.FirstById(id);
                TipoCambioBO objetoBO = new TipoCambioBO();
                Mapper.Map<TTipoCambio, TipoCambioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCambioBO FirstBy(Expression<Func<TTipoCambio, bool>> filter)
        {
            try
            {
                TTipoCambio entidad = base.FirstBy(filter);
                TipoCambioBO objetoBO = Mapper.Map<TTipoCambio, TipoCambioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCambioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCambioBO> listadoBO)
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

        public bool Update(TipoCambioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCambioBO> listadoBO)
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
        private void AsignacionId(TTipoCambio entidad, TipoCambioBO objetoBO)
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

        private TTipoCambio MapeoEntidad(TipoCambioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCambio entidad = new TTipoCambio();
                entidad = Mapper.Map<TipoCambioBO, TTipoCambio>(objetoBO,
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
        /// Obtiene el valor de cambio del dia de acuerdo al tipo de cambio a realizar
        /// </summary>
        /// <param name="tipoCambio"></param>
        /// <returns></returns>
        public TipoCambioFechaDTO ObtenerTipoCambio(int tipoCambio)
        {
            try
            {
                TipoCambioFechaDTO _item = new TipoCambioFechaDTO();
                var fecha = DateTime.Now;

                if (tipoCambio == 1)
                {
                    var temp =   GetBy(x => x.Fecha == fecha.Date, x => new TipoCambioFechaDTO { Cambio = x.SolesDolares,  Fecha = x.Fecha });
                    foreach (var item in temp)
                    {
                        _item = item;
                    }
                }
                else if (tipoCambio == 2)
                {
                    var temp = GetBy(x => x.Fecha == fecha.Date, x => new TipoCambioFechaDTO { Cambio = x.DolaresSoles, Fecha = x.Fecha });
                    foreach (var item in temp)
                    {
                        _item = item;
                    }
                }

                return _item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 20/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de cambio del dia de acuerdo al tipo de cambio escogido
        /// </summary>
        /// <param name="tipoCambio"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<TipoCambioReporteDTO> ObtenerTipoCambioFiltro(int tipoCambio, DateTime? fecha)
        {

            try
            {
                List<TipoCambioReporteDTO> lista = new List<TipoCambioReporteDTO>();

                var query = _dapper.QuerySPDapper("[fin].[SP_ReporteTasasCambio]", new { idMoneda = tipoCambio,Fecha=fecha });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<TipoCambioReporteDTO>>(query);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

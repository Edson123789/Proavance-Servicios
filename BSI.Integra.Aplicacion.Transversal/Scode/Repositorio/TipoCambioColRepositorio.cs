using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
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
    /// Contiene funciones para obtener los tipos de cambio de moneda colombiana
    /// </summary>
    public class TipoCambioColRepositorio : BaseRepository<TTipoCambioCol, TipoCambioColBO>
    {
        #region Metodos Base
        public TipoCambioColRepositorio() : base()
        {
        }
        public TipoCambioColRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoCambioColBO> GetBy(Expression<Func<TTipoCambioCol, bool>> filter)
        {
            IEnumerable<TTipoCambioCol> listado = base.GetBy(filter);
            List<TipoCambioColBO> listadoBO = new List<TipoCambioColBO>();
            foreach (var itemEntidad in listado)
            {
                TipoCambioColBO objetoBO = Mapper.Map<TTipoCambioCol, TipoCambioColBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoCambioColBO FirstById(int id)
        {
            try
            {
                TTipoCambioCol entidad = base.FirstById(id);
                TipoCambioColBO objetoBO = new TipoCambioColBO();
                Mapper.Map<TTipoCambioCol, TipoCambioColBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoCambioColBO FirstBy(Expression<Func<TTipoCambioCol, bool>> filter)
        {
            try
            {
                TTipoCambioCol entidad = base.FirstBy(filter);
                TipoCambioColBO objetoBO = Mapper.Map<TTipoCambioCol, TipoCambioColBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoCambioColBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoCambioCol entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoCambioColBO> listadoBO)
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

        public bool Update(TipoCambioColBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoCambioCol entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoCambioColBO> listadoBO)
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
        private void AsignacionId(TTipoCambioCol entidad, TipoCambioColBO objetoBO)
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

        private TTipoCambioCol MapeoEntidad(TipoCambioColBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoCambioCol entidad = new TTipoCambioCol();
                entidad = Mapper.Map<TipoCambioColBO, TTipoCambioCol>(objetoBO,
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
        /// Obtiene el ultimo tipo cambio colombiano
        /// </summary>
        /// <returns></returns>
        public double ObtenerPesosDolaresUltimoTipoCambioColombia()
        {
            try
            {
                string query = @" SELECT TOP 1 PesosDolares FROM fin.T_TipoCambioCol ORDER BY FechaCreacion DESC";
                var registrosDB = _dapper.FirstOrDefault(query, null);
                return JsonConvert.DeserializeObject<double>(registrosDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

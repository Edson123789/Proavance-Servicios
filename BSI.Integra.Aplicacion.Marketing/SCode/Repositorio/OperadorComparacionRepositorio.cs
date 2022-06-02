
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: OperadorComparacionRepositorio
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Operador de Comparación
    /// </summary>
    public class OperadorComparacionRepositorio : BaseRepository<TOperadorComparacion, OperadorComparacionBO>
    {
        #region Metodos Base
        public OperadorComparacionRepositorio() : base()
        {
        }
        public OperadorComparacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OperadorComparacionBO> GetBy(Expression<Func<TOperadorComparacion, bool>> filter)
        {
            IEnumerable<TOperadorComparacion> listado = base.GetBy(filter);
            List<OperadorComparacionBO> listadoBO = new List<OperadorComparacionBO>();
            foreach (var itemEntidad in listado)
            {
                OperadorComparacionBO objetoBO = Mapper.Map<TOperadorComparacion, OperadorComparacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OperadorComparacionBO FirstById(int id)
        {
            try
            {
                TOperadorComparacion entidad = base.FirstById(id);
                OperadorComparacionBO objetoBO = new OperadorComparacionBO();
                Mapper.Map<TOperadorComparacion, OperadorComparacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OperadorComparacionBO FirstBy(Expression<Func<TOperadorComparacion, bool>> filter)
        {
            try
            {
                TOperadorComparacion entidad = base.FirstBy(filter);
                OperadorComparacionBO objetoBO = Mapper.Map<TOperadorComparacion, OperadorComparacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OperadorComparacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOperadorComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OperadorComparacionBO> listadoBO)
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

        public bool Update(OperadorComparacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOperadorComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OperadorComparacionBO> listadoBO)
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
        private void AsignacionId(TOperadorComparacion entidad, OperadorComparacionBO objetoBO)
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

        private TOperadorComparacion MapeoEntidad(OperadorComparacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOperadorComparacion entidad = new TOperadorComparacion();
                entidad = Mapper.Map<OperadorComparacionBO, TOperadorComparacion>(objetoBO,
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
        /// Obtiene toda la lista de OperadorComparacion  para ser usado en combobox
        /// </summary>
        /// <returns> id, nombre, descripcion, meta</returns>
        public List<FiltroDTO> ObtenerListaOperadorComparacion()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: OperadorComparacionRepositorio
        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene un listado de operador comparacion retornando el simbolo
        /// </summary>
        /// <returns> List<OperadorComparacionDTO> </returns>
        public List<OperadorComparacionDTO> ObtenerListado()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new OperadorComparacionDTO { Id = x.Id, Simbolo = x.Simbolo}).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de Operadores de acuerdo al modulo 
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaOperadorComparacionPorNombreModulo(int IdModulo)
        {
            try
            {
                List<FiltroDTO> lista = new List<FiltroDTO>();
                string query = "SELECT Id, Nombre FROM mkt.V_TOperadorComparacionModuloSistema_ObtenerOperador Where EstadoOperadorComparacion=1 and IdModulo=@IdModulo";
                string respuestaQuery = _dapper.QueryDapper(query, new { IdModulo });
                if (respuestaQuery != "null" && !respuestaQuery.Contains("{}") && !respuestaQuery.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuestaQuery);
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

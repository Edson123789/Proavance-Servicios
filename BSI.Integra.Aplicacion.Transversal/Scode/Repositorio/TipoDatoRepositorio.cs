using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Repositorio: TipoDatoRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/02/2021
    /// <summary>
    /// Gestión de Tipo de Datos
    /// </summary>
    public class TipoDatoRepositorio : BaseRepository<TTipoDato, TipoDatoBO>
    {
        #region Metodos Base
        public TipoDatoRepositorio() : base()
        {
        }
        public TipoDatoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoDatoBO> GetBy(Expression<Func<TTipoDato, bool>> filter)
        {
            IEnumerable<TTipoDato> listado = base.GetBy(filter);
            List<TipoDatoBO> listadoBO = new List<TipoDatoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoDatoBO objetoBO = Mapper.Map<TTipoDato, TipoDatoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoDatoBO FirstById(int id)
        {
            try
            {
                TTipoDato entidad = base.FirstById(id);
                TipoDatoBO objetoBO = new TipoDatoBO();
                Mapper.Map<TTipoDato, TipoDatoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoDatoBO FirstBy(Expression<Func<TTipoDato, bool>> filter)
        {
            try
            {
                TTipoDato entidad = base.FirstBy(filter);
                TipoDatoBO objetoBO = Mapper.Map<TTipoDato, TipoDatoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoDatoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoDatoBO> listadoBO)
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

        public bool Update(TipoDatoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoDato entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoDatoBO> listadoBO)
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
        private void AsignacionId(TTipoDato entidad, TipoDatoBO objetoBO)
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

        private TTipoDato MapeoEntidad(TipoDatoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoDato entidad = new TTipoDato();
                entidad = Mapper.Map<TipoDatoBO, TTipoDato>(objetoBO,
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
		/// Obtiene Lista de Tipo de Datos para filtro en formularios (solo los que tienen por nombre 'lanzamiento')
		/// </summary>
		/// <returns></returns>
        public List<FiltroDTO> CargarTipoDatoChat()
        {
            try
            {
                string _queryTipoDato = "SELECT Id, Nombre FROM mkt.V_TTipoDato WHERE Nombre = 'lanzamiento' and estado = 1";
                var TipoDato = _dapper.QueryDapper(_queryTipoDato, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(TipoDato);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<TipoDatoPrincipalDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new TipoDatoPrincipalDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion,
                    Prioridad = y.Prioridad,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Lista de Tipo de Datos con estado activo
        /// </summary>
        /// <param></param>
        /// <returns>Id, Nombre</returns> 
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

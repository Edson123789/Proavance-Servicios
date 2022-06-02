using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class TipoMovimientoCajaRepositorio : BaseRepository<TTipoMovimientoCaja, TipoMovimientoCajaBO>
    {
        #region Metodos Base
        public TipoMovimientoCajaRepositorio() : base()
        {
        }
        public TipoMovimientoCajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoMovimientoCajaBO> GetBy(Expression<Func<TTipoMovimientoCaja, bool>> filter)
        {
            IEnumerable<TTipoMovimientoCaja> listado = base.GetBy(filter);
            List<TipoMovimientoCajaBO> listadoBO = new List<TipoMovimientoCajaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoMovimientoCajaBO objetoBO = Mapper.Map<TTipoMovimientoCaja, TipoMovimientoCajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoMovimientoCajaBO FirstById(int id)
        {
            try
            {
                TTipoMovimientoCaja entidad = base.FirstById(id);
                TipoMovimientoCajaBO objetoBO = new TipoMovimientoCajaBO();
                Mapper.Map<TTipoMovimientoCaja, TipoMovimientoCajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoMovimientoCajaBO FirstBy(Expression<Func<TTipoMovimientoCaja, bool>> filter)
        {
            try
            {
                TTipoMovimientoCaja entidad = base.FirstBy(filter);
                TipoMovimientoCajaBO objetoBO = Mapper.Map<TTipoMovimientoCaja, TipoMovimientoCajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoMovimientoCajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoMovimientoCajaBO> listadoBO)
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

        public bool Update(TipoMovimientoCajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoMovimientoCajaBO> listadoBO)
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
        private void AsignacionId(TTipoMovimientoCaja entidad, TipoMovimientoCajaBO objetoBO)
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

        private TTipoMovimientoCaja MapeoEntidad(TipoMovimientoCajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoMovimientoCaja entidad = new TTipoMovimientoCaja();
                entidad = Mapper.Map<TipoMovimientoCajaBO, TTipoMovimientoCaja>(objetoBO,
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
        /// Obtiene los TipoMovimientoCaja (para ser usada en Combobox)
        /// </summary>
        /// <returns></returns>
        public List<TipoMovimientoCajaDTO> ObtenerListaTipoMovimientoCaja()
        {
            try
            {
                List<TipoMovimientoCajaDTO> Lista = new List<TipoMovimientoCajaDTO>();
                var _query = "SELECT Id, Nombre FROM fin.T_TipoMovimientoCaja WHERE Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    Lista = JsonConvert.DeserializeObject<List<TipoMovimientoCajaDTO>>(listaDB);
                }
                return Lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

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
    public class SubTipoMovimientoCajaRepositorio : BaseRepository<TSubTipoMovimientoCaja, SubTipoMovimientoCajaBO>
    {
        #region Metodos Base
        public SubTipoMovimientoCajaRepositorio() : base()
        {
        }
        public SubTipoMovimientoCajaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SubTipoMovimientoCajaBO> GetBy(Expression<Func<TSubTipoMovimientoCaja, bool>> filter)
        {
            IEnumerable<TSubTipoMovimientoCaja> listado = base.GetBy(filter);
            List<SubTipoMovimientoCajaBO> listadoBO = new List<SubTipoMovimientoCajaBO>();
            foreach (var itemEntidad in listado)
            {
                SubTipoMovimientoCajaBO objetoBO = Mapper.Map<TSubTipoMovimientoCaja, SubTipoMovimientoCajaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SubTipoMovimientoCajaBO FirstById(int id)
        {
            try
            {
                TSubTipoMovimientoCaja entidad = base.FirstById(id);
                SubTipoMovimientoCajaBO objetoBO = new SubTipoMovimientoCajaBO();
                Mapper.Map<TSubTipoMovimientoCaja, SubTipoMovimientoCajaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SubTipoMovimientoCajaBO FirstBy(Expression<Func<TSubTipoMovimientoCaja, bool>> filter)
        {
            try
            {
                TSubTipoMovimientoCaja entidad = base.FirstBy(filter);
                SubTipoMovimientoCajaBO objetoBO = Mapper.Map<TSubTipoMovimientoCaja, SubTipoMovimientoCajaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SubTipoMovimientoCajaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSubTipoMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SubTipoMovimientoCajaBO> listadoBO)
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

        public bool Update(SubTipoMovimientoCajaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSubTipoMovimientoCaja entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SubTipoMovimientoCajaBO> listadoBO)
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
        private void AsignacionId(TSubTipoMovimientoCaja entidad, SubTipoMovimientoCajaBO objetoBO)
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

        private TSubTipoMovimientoCaja MapeoEntidad(SubTipoMovimientoCajaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSubTipoMovimientoCaja entidad = new TSubTipoMovimientoCaja();
                entidad = Mapper.Map<SubTipoMovimientoCajaBO, TSubTipoMovimientoCaja>(objetoBO,
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
        /// Obtiene los SubTipoMovimientoCaja (para ser usada en Combobox)
        /// </summary>
        /// <returns></returns>
        public List<SubTipoMovimientoCajaDTO> ObtenerListaSubTipoMovimientoCaja()
        {
            try
            {
                List<SubTipoMovimientoCajaDTO> Lista = new List<SubTipoMovimientoCajaDTO>();
                var _query = "SELECT Id, IdTipoMovimientoCaja, Nombre FROM fin.T_SubTipoMovimientoCaja WHERE Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    Lista = JsonConvert.DeserializeObject<List<SubTipoMovimientoCajaDTO>>(listaDB);
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

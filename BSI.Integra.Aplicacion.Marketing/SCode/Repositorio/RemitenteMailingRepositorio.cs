using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/FiltroSegmento
    /// Autor: Joao Benavente - Wilber Choque - Ansoli Espinoza - Richard Zenteno - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_RemitenteMailing
    /// </summary>
    public class RemitenteMailingRepositorio : BaseRepository<TRemitenteMailing, RemitenteMailingBO>
    {
        #region Metodos Base
        public RemitenteMailingRepositorio() : base()
        {
        }
        public RemitenteMailingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RemitenteMailingBO> GetBy(Expression<Func<TRemitenteMailing, bool>> filter)
        {
            IEnumerable<TRemitenteMailing> listado = base.GetBy(filter);
            List<RemitenteMailingBO> listadoBO = new List<RemitenteMailingBO>();
            foreach (var itemEntidad in listado)
            {
                RemitenteMailingBO objetoBO = Mapper.Map<TRemitenteMailing, RemitenteMailingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RemitenteMailingBO FirstById(int id)
        {
            try
            {
                TRemitenteMailing entidad = base.FirstById(id);
                RemitenteMailingBO objetoBO = new RemitenteMailingBO();
                Mapper.Map<TRemitenteMailing, RemitenteMailingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RemitenteMailingBO FirstBy(Expression<Func<TRemitenteMailing, bool>> filter)
        {
            try
            {
                TRemitenteMailing entidad = base.FirstBy(filter);
                RemitenteMailingBO objetoBO = Mapper.Map<TRemitenteMailing, RemitenteMailingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RemitenteMailingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRemitenteMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RemitenteMailingBO> listadoBO)
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

        public bool Update(RemitenteMailingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRemitenteMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RemitenteMailingBO> listadoBO)
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
        private void AsignacionId(TRemitenteMailing entidad, RemitenteMailingBO objetoBO)
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

        private TRemitenteMailing MapeoEntidad(RemitenteMailingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRemitenteMailing entidad = new TRemitenteMailing();
                entidad = Mapper.Map<RemitenteMailingBO, TRemitenteMailing>(objetoBO,
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
        /// Obtiene una lista con los registros de la tabla mkt.T_RemitenteMailing
        /// </summary>
        /// <returns>Lista de objetos de clase RemitenteMailingDTO</returns>
        public List<RemitenteMailingDTO> ObtenerListaRemitenteMailing()
        {
            try
            {
                string query = $@"
                                SELECT Id, 
                                       Nombre
                                FROM mkt.V_TRemitenteMailing_Nombre
                                ORDER BY FechaCreacion DESC
                                ";
                var responseQuery = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<RemitenteMailingDTO>>(responseQuery);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los RemitenteMailing para ser mostrados en una grilla (para su propio CRUD), 
        /// </summary>
        /// <returns></returns>
        public List<RemitenteMailingDTO> ObtenerTodosRemitenteMailing()
        {
            try
            {
                List<RemitenteMailingDTO> RemitenteMailing = new List<RemitenteMailingDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion FROM mkt.T_RemitenteMailing WHERE  Estado = 1";
                var RemitenteMailingDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(RemitenteMailingDB) && !RemitenteMailingDB.Contains("[]"))
                {
                    RemitenteMailing = JsonConvert.DeserializeObject<List<RemitenteMailingDTO>>(RemitenteMailingDB);
                }
                return RemitenteMailing;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

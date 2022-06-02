using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/RemitenteMailingAsesor
    /// Autor: Joao Benavente - Wilber Choque - Ansoli Espinoza - Richard Zenteno - Gian Miranda
    /// Fecha: 13/05/2021
    /// <summary>
    /// Repositorio para consultas de la tabla mkt.T_RemitenteMailingAsesor
    /// </summary>
    public class RemitenteMailingAsesorRepositorio : BaseRepository<TRemitenteMailingAsesor, RemitenteMailingAsesorBO>
    {
        #region Metodos Base
        public RemitenteMailingAsesorRepositorio() : base()
        {
        }
        public RemitenteMailingAsesorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RemitenteMailingAsesorBO> GetBy(Expression<Func<TRemitenteMailingAsesor, bool>> filter)
        {
            IEnumerable<TRemitenteMailingAsesor> listado = base.GetBy(filter);
            List<RemitenteMailingAsesorBO> listadoBO = new List<RemitenteMailingAsesorBO>();
            foreach (var itemEntidad in listado)
            {
                RemitenteMailingAsesorBO objetoBO = Mapper.Map<TRemitenteMailingAsesor, RemitenteMailingAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RemitenteMailingAsesorBO FirstById(int id)
        {
            try
            {
                TRemitenteMailingAsesor entidad = base.FirstById(id);
                RemitenteMailingAsesorBO objetoBO = new RemitenteMailingAsesorBO();
                Mapper.Map<TRemitenteMailingAsesor, RemitenteMailingAsesorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RemitenteMailingAsesorBO FirstBy(Expression<Func<TRemitenteMailingAsesor, bool>> filter)
        {
            try
            {
                TRemitenteMailingAsesor entidad = base.FirstBy(filter);
                RemitenteMailingAsesorBO objetoBO = Mapper.Map<TRemitenteMailingAsesor, RemitenteMailingAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RemitenteMailingAsesorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRemitenteMailingAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RemitenteMailingAsesorBO> listadoBO)
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

        public bool Update(RemitenteMailingAsesorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRemitenteMailingAsesor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RemitenteMailingAsesorBO> listadoBO)
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
        private void AsignacionId(TRemitenteMailingAsesor entidad, RemitenteMailingAsesorBO objetoBO)
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

        private TRemitenteMailingAsesor MapeoEntidad(RemitenteMailingAsesorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRemitenteMailingAsesor entidad = new TRemitenteMailingAsesor();
                entidad = Mapper.Map<RemitenteMailingAsesorBO, TRemitenteMailingAsesor>(objetoBO,
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
        /// Obtiene una lista con los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase RemitenteMailingAsesorDTO</returns>
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor()
        {
            try
            { 
                string query = "SELECT IdRemitenteMailing, IdPersonal, NombreCompleto FROM mkt.V_TRemitenteMailingAsesor_NombreCompleto";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<RemitenteMailingAsesorDTO> listaRemitenteMailingAsesor = JsonConvert.DeserializeObject<List<RemitenteMailingAsesorDTO>>(responseQuery);
                return listaRemitenteMailingAsesor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista con los asesores y sus emails dado el Id de RemitenteMailing
        /// </summary>
        /// <returns></returns>
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing)
        {
            try
            {
                string query = "SELECT IdRemitenteMailing, IdPersonal, NombreCompleto, CorreoElectronico FROM [mkt].[V_ObtenerRemitenteMeilingPersonalActivo] WHERE IdRemitenteMailing=" + IdRemitenteMailing+" AND Estado = 1";
                var responseQuery = _dapper.QueryDapper(query, null);
                List<RemitenteMailingAsesorDTO> listaRemitenteMailingAsesor = JsonConvert.DeserializeObject<List<RemitenteMailingAsesorDTO>>(responseQuery);
                return listaRemitenteMailingAsesor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

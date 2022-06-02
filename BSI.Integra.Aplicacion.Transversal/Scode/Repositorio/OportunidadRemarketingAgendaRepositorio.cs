using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;


namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Transversal/OportunidadRemarketingAgenda
    /// Autor: Gian Miranda
    /// Fecha: 09/09/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_OportunidadRemarketingAgenda
    /// </summary>
    public class OportunidadRemarketingAgendaRepositorio : BaseRepository<TOportunidadRemarketingAgenda, OportunidadRemarketingAgendaBO>
    {
        #region Metodos Base
        public OportunidadRemarketingAgendaRepositorio() : base()
        {
        }
        public OportunidadRemarketingAgendaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadRemarketingAgendaBO> GetBy(Expression<Func<TOportunidadRemarketingAgenda, bool>> filter)
        {
            IEnumerable<TOportunidadRemarketingAgenda> listado = base.GetBy(filter);
            List<OportunidadRemarketingAgendaBO> listadoBO = new List<OportunidadRemarketingAgendaBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadRemarketingAgendaBO objetoBO = Mapper.Map<TOportunidadRemarketingAgenda, OportunidadRemarketingAgendaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadRemarketingAgendaBO FirstById(int id)
        {
            try
            {
                TOportunidadRemarketingAgenda entidad = base.FirstById(id);
                OportunidadRemarketingAgendaBO objetoBO = new OportunidadRemarketingAgendaBO();
                Mapper.Map<TOportunidadRemarketingAgenda, OportunidadRemarketingAgendaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadRemarketingAgendaBO FirstBy(Expression<Func<TOportunidadRemarketingAgenda, bool>> filter)
        {
            try
            {
                TOportunidadRemarketingAgenda entidad = base.FirstBy(filter);
                OportunidadRemarketingAgendaBO objetoBO = Mapper.Map<TOportunidadRemarketingAgenda, OportunidadRemarketingAgendaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadRemarketingAgendaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidadRemarketingAgenda entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadRemarketingAgendaBO> listadoBO)
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

        public bool Update(OportunidadRemarketingAgendaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidadRemarketingAgenda entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadRemarketingAgendaBO> listadoBO)
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
        private void AsignacionId(TOportunidadRemarketingAgenda entidad, OportunidadRemarketingAgendaBO objetoBO)
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

        private TOportunidadRemarketingAgenda MapeoEntidad(OportunidadRemarketingAgendaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidadRemarketingAgenda entidad = new TOportunidadRemarketingAgenda();
                entidad = Mapper.Map<OportunidadRemarketingAgendaBO, TOportunidadRemarketingAgenda>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OportunidadRemarketingAgendaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TOportunidadRemarketingAgenda, bool>>> filters, Expression<Func<TOportunidadRemarketingAgenda, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TOportunidadRemarketingAgenda> listado = base.GetFiltered(filters, orderBy, ascending);
            List<OportunidadRemarketingAgendaBO> listadoBO = new List<OportunidadRemarketingAgendaBO>();

            foreach (var itemEntidad in listado)
            {
                OportunidadRemarketingAgendaBO objetoBO = Mapper.Map<TOportunidadRemarketingAgenda, OportunidadRemarketingAgendaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Gian Miranda
        /// Fecha: 08/09/2021
        /// Version: 1.0
        /// <summary>
        /// Desactivar la redireccion de remarketing
        /// </summary>
        /// <returns>Bool</returns>
        public bool DesactivarRedireccionRemarketingAnterior(int idOportunidad)
        {
            try
            {
                string spPeticion = "[com].[SP_DesactivarRedireccionRemarketingAnterior]";
                string resultadoPeticion = _dapper.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 08/09/2021
        /// Version: 1.0
        /// <summary>
        /// Eliminar la redireccion de remarketing
        /// </summary>
        /// <returns>Bool</returns>
        public bool EliminarRedireccionRemarketingAnterior(int idOportunidad)
        {
            try
            {
                string spPeticion = "[com].[SP_EliminarRedireccionRemarketingAnterior]";
                string resultadoPeticion = _dapper.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    /// Repositorio: Marketing/MailchimpEstadisticaPorPrioridad
    /// Autor: Gian Miranda
    /// Fecha: 26/07/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_MailchimpEstadisticaPorPrioridad
    /// </summary>
    public class MailchimpEstadisticaPorPrioridadRepositorio : BaseRepository<TMailchimpEstadisticaPorPrioridad, MailchimpEstadisticaPorPrioridadBO>
    {
        #region Metodos Base
        public MailchimpEstadisticaPorPrioridadRepositorio() : base()
        {
        }
        public MailchimpEstadisticaPorPrioridadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MailchimpEstadisticaPorPrioridadBO> GetBy(Expression<Func<TMailchimpEstadisticaPorPrioridad, bool>> filter)
        {
            IEnumerable<TMailchimpEstadisticaPorPrioridad> listado = base.GetBy(filter);
            List<MailchimpEstadisticaPorPrioridadBO> listadoBO = new List<MailchimpEstadisticaPorPrioridadBO>();
            foreach (var itemEntidad in listado)
            {
                MailchimpEstadisticaPorPrioridadBO objetoBO = Mapper.Map<TMailchimpEstadisticaPorPrioridad, MailchimpEstadisticaPorPrioridadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MailchimpEstadisticaPorPrioridadBO FirstById(int id)
        {
            try
            {
                TMailchimpEstadisticaPorPrioridad entidad = base.FirstById(id);
                MailchimpEstadisticaPorPrioridadBO objetoBO = new MailchimpEstadisticaPorPrioridadBO();
                Mapper.Map<TMailchimpEstadisticaPorPrioridad, MailchimpEstadisticaPorPrioridadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MailchimpEstadisticaPorPrioridadBO FirstBy(Expression<Func<TMailchimpEstadisticaPorPrioridad, bool>> filter)
        {
            try
            {
                TMailchimpEstadisticaPorPrioridad entidad = base.FirstBy(filter);
                MailchimpEstadisticaPorPrioridadBO objetoBO = Mapper.Map<TMailchimpEstadisticaPorPrioridad, MailchimpEstadisticaPorPrioridadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MailchimpEstadisticaPorPrioridadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMailchimpEstadisticaPorPrioridad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MailchimpEstadisticaPorPrioridadBO> listadoBO)
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

        public bool Update(MailchimpEstadisticaPorPrioridadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMailchimpEstadisticaPorPrioridad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MailchimpEstadisticaPorPrioridadBO> listadoBO)
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
        private void AsignacionId(TMailchimpEstadisticaPorPrioridad entidad, MailchimpEstadisticaPorPrioridadBO objetoBO)
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

        private TMailchimpEstadisticaPorPrioridad MapeoEntidad(MailchimpEstadisticaPorPrioridadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMailchimpEstadisticaPorPrioridad entidad = new TMailchimpEstadisticaPorPrioridad();
                entidad = Mapper.Map<MailchimpEstadisticaPorPrioridadBO, TMailchimpEstadisticaPorPrioridad>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MailchimpEstadisticaPorPrioridadBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMailchimpEstadisticaPorPrioridad, bool>>> filters, Expression<Func<TMailchimpEstadisticaPorPrioridad, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMailchimpEstadisticaPorPrioridad> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MailchimpEstadisticaPorPrioridadBO> listadoBO = new List<MailchimpEstadisticaPorPrioridadBO>();

            foreach (var itemEntidad in listado)
            {
                MailchimpEstadisticaPorPrioridadBO objetoBO = Mapper.Map<TMailchimpEstadisticaPorPrioridad, MailchimpEstadisticaPorPrioridadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

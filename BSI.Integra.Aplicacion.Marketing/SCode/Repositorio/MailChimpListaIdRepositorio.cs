using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class MailChimpListaIdRepositorio : BaseRepository<TMailChimpListaId, MailChimpListaIdBO>
    {
        #region Metodos Base
        public MailChimpListaIdRepositorio() : base()
        {
        }
        public MailChimpListaIdRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MailChimpListaIdBO> GetBy(Expression<Func<TMailChimpListaId, bool>> filter)
        {
            IEnumerable<TMailChimpListaId> listado = base.GetBy(filter);
            List<MailChimpListaIdBO> listadoBO = new List<MailChimpListaIdBO>();
            foreach (var itemEntidad in listado)
            {
                MailChimpListaIdBO objetoBO = Mapper.Map<TMailChimpListaId, MailChimpListaIdBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MailChimpListaIdBO FirstById(int id)
        {
            try
            {
                TMailChimpListaId entidad = base.FirstById(id);
                MailChimpListaIdBO objetoBO = new MailChimpListaIdBO();
                Mapper.Map<TMailChimpListaId, MailChimpListaIdBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MailChimpListaIdBO FirstBy(Expression<Func<TMailChimpListaId, bool>> filter)
        {
            try
            {
                TMailChimpListaId entidad = base.FirstBy(filter);
                MailChimpListaIdBO objetoBO = Mapper.Map<TMailChimpListaId, MailChimpListaIdBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MailChimpListaIdBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMailChimpListaId entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MailChimpListaIdBO> listadoBO)
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

        public bool Update(MailChimpListaIdBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMailChimpListaId entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MailChimpListaIdBO> listadoBO)
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
        private void AsignacionId(TMailChimpListaId entidad, MailChimpListaIdBO objetoBO)
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

        private TMailChimpListaId MapeoEntidad(MailChimpListaIdBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMailChimpListaId entidad = new TMailChimpListaId();
                entidad = Mapper.Map<MailChimpListaIdBO, TMailChimpListaId>(objetoBO,
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
        /// Obtiene un  MailChimpListaId Por IdPrioridadMailChimpLista
        /// </summary>
        /// <param name="idPrioridadMailChimpLista">Id de la prioridad MailChimpLista (PK de la tabla mkt.T_MailChimpListaId)</param>
        /// <returns>Objeto de clase MailChimpListaIdBO</returns>
        public MailChimpListaIdBO ObtenerMailChimpListaIdPorLista(int idPrioridadMailChimpLista)
        {
            try
            {
                MailChimpListaIdBO prioridadMailchimp = new MailChimpListaIdBO();
                prioridadMailchimp = FirstBy(x => x.IdCampaniaMailingLista == idPrioridadMailChimpLista && x.Estado == true);
                return prioridadMailchimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de MailChimpListaIds 
        /// </summary>
        /// <param name="IdCampaniaMailing"></param>
        /// <returns></returns>
        public List<MailChimpListaIdBO> ObtenerIdListasMailchimp(int idPrioridadMailChimpLista)
        {
            try
            {
                List<MailChimpListaIdBO> listaIdBOs = new List<MailChimpListaIdBO>();
                listaIdBOs = GetBy(x => x.IdCampaniaMailingLista == idPrioridadMailChimpLista && x.Estado == true).ToList();
                    
                return listaIdBOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: WhatsAppDesuscrito
    /// Autor: Fischer Valdez
    /// Fecha: 09/05/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_WhatsAppDesuscrito
    /// </summary>
    public class WhatsAppDesuscritoRepositorio : BaseRepository<TWhatsAppDesuscrito, WhatsAppDesuscritoBO>
    {
        #region Metodos Base
        public WhatsAppDesuscritoRepositorio() : base()
        {
        }
        public WhatsAppDesuscritoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppDesuscritoBO> GetBy(Expression<Func<TWhatsAppDesuscrito, bool>> filter)
        {
            IEnumerable<TWhatsAppDesuscrito> listado = base.GetBy(filter);
            List<WhatsAppDesuscritoBO> listadoBO = new List<WhatsAppDesuscritoBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppDesuscritoBO objetoBO = Mapper.Map<TWhatsAppDesuscrito, WhatsAppDesuscritoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppDesuscritoBO FirstById(int id)
        {
            try
            {
                TWhatsAppDesuscrito entidad = base.FirstById(id);
                WhatsAppDesuscritoBO objetoBO = new WhatsAppDesuscritoBO();
                Mapper.Map<TWhatsAppDesuscrito, WhatsAppDesuscritoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppDesuscritoBO FirstBy(Expression<Func<TWhatsAppDesuscrito, bool>> filter)
        {
            try
            {
                TWhatsAppDesuscrito entidad = base.FirstBy(filter);
                WhatsAppDesuscritoBO objetoBO = Mapper.Map<TWhatsAppDesuscrito, WhatsAppDesuscritoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppDesuscritoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppDesuscrito entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppDesuscritoBO> listadoBO)
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

        public bool Update(WhatsAppDesuscritoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppDesuscrito entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppDesuscritoBO> listadoBO)
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
        private void AsignacionId(TWhatsAppDesuscrito entidad, WhatsAppDesuscritoBO objetoBO)
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

        private TWhatsAppDesuscrito MapeoEntidad(WhatsAppDesuscritoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppDesuscrito entidad = new TWhatsAppDesuscrito();
                entidad = Mapper.Map<WhatsAppDesuscritoBO, TWhatsAppDesuscrito>(objetoBO,
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
    }
}

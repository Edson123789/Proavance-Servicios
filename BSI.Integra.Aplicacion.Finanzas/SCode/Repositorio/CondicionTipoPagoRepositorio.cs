using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CondicionTipoPagoRepositorio : BaseRepository<TCondicionTipoPago, CondicionTipoPagoBO>
    {
        #region Metodos Base
        public CondicionTipoPagoRepositorio() : base()
        {
        }
        public CondicionTipoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CondicionTipoPagoBO> GetBy(Expression<Func<TCondicionTipoPago, bool>> filter)
        {
            IEnumerable<TCondicionTipoPago> listado = base.GetBy(filter);
            List<CondicionTipoPagoBO> listadoBO = new List<CondicionTipoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                CondicionTipoPagoBO objetoBO = Mapper.Map<TCondicionTipoPago, CondicionTipoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CondicionTipoPagoBO FirstById(int id)
        {
            try
            {
                TCondicionTipoPago entidad = base.FirstById(id);
                CondicionTipoPagoBO objetoBO = new CondicionTipoPagoBO();
                Mapper.Map<TCondicionTipoPago, CondicionTipoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CondicionTipoPagoBO FirstBy(Expression<Func<TCondicionTipoPago, bool>> filter)
        {
            try
            {
                TCondicionTipoPago entidad = base.FirstBy(filter);
                CondicionTipoPagoBO objetoBO = Mapper.Map<TCondicionTipoPago, CondicionTipoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CondicionTipoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCondicionTipoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CondicionTipoPagoBO> listadoBO)
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

        public bool Update(CondicionTipoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCondicionTipoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CondicionTipoPagoBO> listadoBO)
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
        private void AsignacionId(TCondicionTipoPago entidad, CondicionTipoPagoBO objetoBO)
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

        private TCondicionTipoPago MapeoEntidad(CondicionTipoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCondicionTipoPago entidad = new TCondicionTipoPago();
                entidad = Mapper.Map<CondicionTipoPagoBO, TCondicionTipoPago>(objetoBO,
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

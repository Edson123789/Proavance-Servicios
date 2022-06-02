using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ComisionMontoPagoRepositorio : BaseRepository<TComisionMontoPago, ComisionMontoPagoBO>
    {
        #region Metodos Base
        public ComisionMontoPagoRepositorio() : base()
        {
        }
        public ComisionMontoPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ComisionMontoPagoBO> GetBy(Expression<Func<TComisionMontoPago, bool>> filter)
        {
            IEnumerable<TComisionMontoPago> listado = base.GetBy(filter);
            List<ComisionMontoPagoBO> listadoBO = new List<ComisionMontoPagoBO>();
            foreach (var itemEntidad in listado)
            {
                ComisionMontoPagoBO objetoBO = Mapper.Map<TComisionMontoPago, ComisionMontoPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ComisionMontoPagoBO FirstById(int id)
        {
            try
            {
                TComisionMontoPago entidad = base.FirstById(id);
                ComisionMontoPagoBO objetoBO = new ComisionMontoPagoBO();
                Mapper.Map<TComisionMontoPago, ComisionMontoPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComisionMontoPagoBO FirstBy(Expression<Func<TComisionMontoPago, bool>> filter)
        {
            try
            {
                TComisionMontoPago entidad = base.FirstBy(filter);
                ComisionMontoPagoBO objetoBO = Mapper.Map<TComisionMontoPago, ComisionMontoPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ComisionMontoPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TComisionMontoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ComisionMontoPagoBO> listadoBO)
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

        public bool Update(ComisionMontoPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TComisionMontoPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ComisionMontoPagoBO> listadoBO)
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
        private void AsignacionId(TComisionMontoPago entidad, ComisionMontoPagoBO objetoBO)
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

        private TComisionMontoPago MapeoEntidad(ComisionMontoPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TComisionMontoPago entidad = new TComisionMontoPago();
                entidad = Mapper.Map<ComisionMontoPagoBO, TComisionMontoPago>(objetoBO,
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

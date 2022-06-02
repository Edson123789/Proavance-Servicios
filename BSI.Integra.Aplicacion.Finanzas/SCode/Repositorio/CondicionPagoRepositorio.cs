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
    public class CondicionPagoRepositorio : BaseRepository<TCondicionPago, CondicionPagoBO>
    {
        #region Metodos Base
        public CondicionPagoRepositorio() : base()
        {
        }
        public CondicionPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CondicionPagoBO> GetBy(Expression<Func<TCondicionPago, bool>> filter)
        {
            IEnumerable<TCondicionPago> listado = base.GetBy(filter);
            List<CondicionPagoBO> listadoBO = new List<CondicionPagoBO>();
            foreach (var itemEntidad in listado)
            {
                CondicionPagoBO objetoBO = Mapper.Map<TCondicionPago, CondicionPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CondicionPagoBO FirstById(int id)
        {
            try
            {
                TCondicionPago entidad = base.FirstById(id);
                CondicionPagoBO objetoBO = new CondicionPagoBO();
                Mapper.Map<TCondicionPago, CondicionPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CondicionPagoBO FirstBy(Expression<Func<TCondicionPago, bool>> filter)
        {
            try
            {
                TCondicionPago entidad = base.FirstBy(filter);
                CondicionPagoBO objetoBO = Mapper.Map<TCondicionPago, CondicionPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CondicionPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCondicionPago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CondicionPagoBO> listadoBO)
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

        public bool Update(CondicionPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCondicionPago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CondicionPagoBO> listadoBO)
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
        private void AsignacionId(TCondicionPago entidad, CondicionPagoBO objetoBO)
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

        private TCondicionPago MapeoEntidad(CondicionPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCondicionPago entidad = new TCondicionPago();
                entidad = Mapper.Map<CondicionPagoBO, TCondicionPago>(objetoBO,
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

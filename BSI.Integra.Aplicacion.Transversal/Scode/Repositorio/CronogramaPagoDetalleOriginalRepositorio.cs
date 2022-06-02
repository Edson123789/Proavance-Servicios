using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/CronogramaPagoDetalleOriginal
    /// Autor: Jose Villena
    /// Fecha: 01/05/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_CronogramaPagoDetalleOriginal
    /// </summary>
    public class CronogramaPagoDetalleOriginalRepositorio : BaseRepository<TCronogramaPagoDetalleOriginal, CronogramaPagoDetalleOriginalBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleOriginalRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleOriginalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleOriginalBO> GetBy(Expression<Func<TCronogramaPagoDetalleOriginal, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalleOriginal> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleOriginalBO> listadoBO = new List<CronogramaPagoDetalleOriginalBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleOriginalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleOriginal, CronogramaPagoDetalleOriginalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoDetalleOriginalBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleOriginal entidad = base.FirstById(id);
                CronogramaPagoDetalleOriginalBO objetoBO = new CronogramaPagoDetalleOriginalBO();
                Mapper.Map<TCronogramaPagoDetalleOriginal, CronogramaPagoDetalleOriginalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleOriginalBO FirstBy(Expression<Func<TCronogramaPagoDetalleOriginal, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleOriginal entidad = base.FirstBy(filter);
                CronogramaPagoDetalleOriginalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleOriginal, CronogramaPagoDetalleOriginalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleOriginalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleOriginal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleOriginalBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleOriginalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleOriginal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleOriginalBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleOriginal entidad, CronogramaPagoDetalleOriginalBO objetoBO)
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

        private TCronogramaPagoDetalleOriginal MapeoEntidad(CronogramaPagoDetalleOriginalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleOriginal entidad = new TCronogramaPagoDetalleOriginal();
                entidad = Mapper.Map<CronogramaPagoDetalleOriginalBO, TCronogramaPagoDetalleOriginal>(objetoBO,
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

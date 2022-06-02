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
    public class CronogramaPagoDetalleRepositorio : BaseRepository<TCronogramaPagoDetalle, CronogramaPagoDetalleBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleBO> GetBy(Expression<Func<TCronogramaPagoDetalle, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalle> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleBO> listadoBO = new List<CronogramaPagoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleBO objetoBO = Mapper.Map<TCronogramaPagoDetalle, CronogramaPagoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoDetalleBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalle entidad = base.FirstById(id);
                CronogramaPagoDetalleBO objetoBO = new CronogramaPagoDetalleBO();
                Mapper.Map<TCronogramaPagoDetalle, CronogramaPagoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleBO FirstBy(Expression<Func<TCronogramaPagoDetalle, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalle entidad = base.FirstBy(filter);
                CronogramaPagoDetalleBO objetoBO = Mapper.Map<TCronogramaPagoDetalle, CronogramaPagoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalle entidad, CronogramaPagoDetalleBO objetoBO)
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

        private TCronogramaPagoDetalle MapeoEntidad(CronogramaPagoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalle entidad = new TCronogramaPagoDetalle();
                entidad = Mapper.Map<CronogramaPagoDetalleBO, TCronogramaPagoDetalle>(objetoBO,
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

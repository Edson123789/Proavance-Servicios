using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CronogramaPagoDetalleModLogFinalRepositorio:BaseRepository<TCronogramaPagoDetalleModLogFinal, CronogramaPagoDetalleModLogFinalBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleModLogFinalRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleModLogFinalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoDetalleModLogFinalBO> GetBy(Expression<Func<TCronogramaPagoDetalleModLogFinal, bool>> filter)
        {
            IEnumerable<TCronogramaPagoDetalleModLogFinal> listado = base.GetBy(filter);
            List<CronogramaPagoDetalleModLogFinalBO> listadoBO = new List<CronogramaPagoDetalleModLogFinalBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoDetalleModLogFinalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleModLogFinal, CronogramaPagoDetalleModLogFinalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        public CronogramaPagoDetalleModLogFinalBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleModLogFinal entidad = base.FirstById(id);
                CronogramaPagoDetalleModLogFinalBO objetoBO = new CronogramaPagoDetalleModLogFinalBO();
                Mapper.Map<TCronogramaPagoDetalleModLogFinal, CronogramaPagoDetalleModLogFinalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleModLogFinalBO FirstBy(Expression<Func<TCronogramaPagoDetalleModLogFinal, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleModLogFinal entidad = base.FirstBy(filter);
                CronogramaPagoDetalleModLogFinalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleModLogFinal, CronogramaPagoDetalleModLogFinalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleModLogFinalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleModLogFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleModLogFinalBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleModLogFinalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleModLogFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleModLogFinalBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleModLogFinal entidad, CronogramaPagoDetalleModLogFinalBO objetoBO)
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

        private TCronogramaPagoDetalleModLogFinal MapeoEntidad(CronogramaPagoDetalleModLogFinalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleModLogFinal entidad = new TCronogramaPagoDetalleModLogFinal();
                entidad = Mapper.Map<CronogramaPagoDetalleModLogFinalBO, TCronogramaPagoDetalleModLogFinal>(objetoBO,
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

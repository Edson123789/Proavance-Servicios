using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class CronogramaPagoRepositorio:BaseRepository<TCronogramaPago, CronogramaPagoBO>
    {
        #region Metodos Base
        public CronogramaPagoRepositorio() : base()
        {
        }
        public CronogramaPagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaPagoBO> GetBy(Expression<Func<TCronogramaPago, bool>> filter)
        {
            IEnumerable<TCronogramaPago> listado = base.GetBy(filter);
            List<CronogramaPagoBO> listadoBO = new List<CronogramaPagoBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaPagoBO objetoBO = Mapper.Map<TCronogramaPago, CronogramaPagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaPagoBO FirstById(int id)
        {
            try
            {
                TCronogramaPago entidad = base.FirstById(id);
                CronogramaPagoBO objetoBO = new CronogramaPagoBO();
                Mapper.Map<TCronogramaPago, CronogramaPagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoBO FirstBy(Expression<Func<TCronogramaPago, bool>> filter)
        {
            try
            {
                TCronogramaPago entidad = base.FirstBy(filter);
                CronogramaPagoBO objetoBO = Mapper.Map<TCronogramaPago, CronogramaPagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPago entidad = MapeoEntidad(objetoBO);

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
        public bool Insert(IEnumerable<CronogramaPagoBO> listadoBO)
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

        public bool Update(CronogramaPagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPago entidad = MapeoEntidad(objetoBO);

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
        public bool Update(IEnumerable<CronogramaPagoBO> listadoBO)
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
        private void AsignacionId(TCronogramaPago entidad, CronogramaPagoBO objetoBO)
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
        private TCronogramaPago MapeoEntidad(CronogramaPagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPago entidad = new TCronogramaPago();
                entidad = Mapper.Map<CronogramaPagoBO, TCronogramaPago>(objetoBO,
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

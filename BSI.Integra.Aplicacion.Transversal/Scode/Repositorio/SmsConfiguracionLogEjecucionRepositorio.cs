using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    /// Repositorio: Marketing/SmsConfiguracionLogEjecucion
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_SmsConfiguracionLogEjecucion
    /// </summary>
    public class SmsConfiguracionLogEjecucionRepositorio : BaseRepository<TSmsConfiguracionLogEjecucion, SmsConfiguracionLogEjecucionBO>
    {
        #region Metodos Base
        public SmsConfiguracionLogEjecucionRepositorio() : base()
        {
        }
        public SmsConfiguracionLogEjecucionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SmsConfiguracionLogEjecucionBO> GetBy(Expression<Func<TSmsConfiguracionLogEjecucion, bool>> filter)
        {
            IEnumerable<TSmsConfiguracionLogEjecucion> listado = base.GetBy(filter);
            List<SmsConfiguracionLogEjecucionBO> listadoBO = new List<SmsConfiguracionLogEjecucionBO>();
            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionLogEjecucionBO objetoBO = Mapper.Map<TSmsConfiguracionLogEjecucion, SmsConfiguracionLogEjecucionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SmsConfiguracionLogEjecucionBO FirstById(int id)
        {
            try
            {
                TSmsConfiguracionLogEjecucion entidad = base.FirstById(id);
                SmsConfiguracionLogEjecucionBO objetoBO = new SmsConfiguracionLogEjecucionBO();
                Mapper.Map<TSmsConfiguracionLogEjecucion, SmsConfiguracionLogEjecucionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SmsConfiguracionLogEjecucionBO FirstBy(Expression<Func<TSmsConfiguracionLogEjecucion, bool>> filter)
        {
            try
            {
                TSmsConfiguracionLogEjecucion entidad = base.FirstBy(filter);
                SmsConfiguracionLogEjecucionBO objetoBO = Mapper.Map<TSmsConfiguracionLogEjecucion, SmsConfiguracionLogEjecucionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SmsConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSmsConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SmsConfiguracionLogEjecucionBO> listadoBO)
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

        public bool Update(SmsConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSmsConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SmsConfiguracionLogEjecucionBO> listadoBO)
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
        private void AsignacionId(TSmsConfiguracionLogEjecucion entidad, SmsConfiguracionLogEjecucionBO objetoBO)
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

        private TSmsConfiguracionLogEjecucion MapeoEntidad(SmsConfiguracionLogEjecucionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSmsConfiguracionLogEjecucion entidad = new TSmsConfiguracionLogEjecucion();
                entidad = Mapper.Map<SmsConfiguracionLogEjecucionBO, TSmsConfiguracionLogEjecucion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SmsConfiguracionLogEjecucionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSmsConfiguracionLogEjecucion, bool>>> filters, Expression<Func<TSmsConfiguracionLogEjecucion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSmsConfiguracionLogEjecucion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SmsConfiguracionLogEjecucionBO> listadoBO = new List<SmsConfiguracionLogEjecucionBO>();

            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionLogEjecucionBO objetoBO = Mapper.Map<TSmsConfiguracionLogEjecucion, SmsConfiguracionLogEjecucionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

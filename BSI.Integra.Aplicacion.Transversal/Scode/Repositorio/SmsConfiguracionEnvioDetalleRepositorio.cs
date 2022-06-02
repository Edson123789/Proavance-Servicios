using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Scode.Repositorio
{
    /// Repositorio: Marketing/SmsConfiguracionEnvioDetalleRepositorio
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_SmsConfiguracionEnvioDetalle
    /// </summary>
    public class SmsConfiguracionEnvioDetalleRepositorio : BaseRepository<TSmsConfiguracionEnvioDetalle, SmsConfiguracionEnvioDetalleBO>
    {
        #region Metodos Base
        public SmsConfiguracionEnvioDetalleRepositorio() : base()
        {
        }
        public SmsConfiguracionEnvioDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SmsConfiguracionEnvioDetalleBO> GetBy(Expression<Func<TSmsConfiguracionEnvioDetalle, bool>> filter)
        {
            IEnumerable<TSmsConfiguracionEnvioDetalle> listado = base.GetBy(filter);
            List<SmsConfiguracionEnvioDetalleBO> listadoBO = new List<SmsConfiguracionEnvioDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionEnvioDetalleBO objetoBO = Mapper.Map<TSmsConfiguracionEnvioDetalle, SmsConfiguracionEnvioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SmsConfiguracionEnvioDetalleBO FirstById(int id)
        {
            try
            {
                TSmsConfiguracionEnvioDetalle entidad = base.FirstById(id);
                SmsConfiguracionEnvioDetalleBO objetoBO = new SmsConfiguracionEnvioDetalleBO();
                Mapper.Map<TSmsConfiguracionEnvioDetalle, SmsConfiguracionEnvioDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SmsConfiguracionEnvioDetalleBO FirstBy(Expression<Func<TSmsConfiguracionEnvioDetalle, bool>> filter)
        {
            try
            {
                TSmsConfiguracionEnvioDetalle entidad = base.FirstBy(filter);
                SmsConfiguracionEnvioDetalleBO objetoBO = Mapper.Map<TSmsConfiguracionEnvioDetalle, SmsConfiguracionEnvioDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SmsConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSmsConfiguracionEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SmsConfiguracionEnvioDetalleBO> listadoBO)
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

        public bool Update(SmsConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSmsConfiguracionEnvioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SmsConfiguracionEnvioDetalleBO> listadoBO)
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
        private void AsignacionId(TSmsConfiguracionEnvioDetalle entidad, SmsConfiguracionEnvioDetalleBO objetoBO)
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

        private TSmsConfiguracionEnvioDetalle MapeoEntidad(SmsConfiguracionEnvioDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSmsConfiguracionEnvioDetalle entidad = new TSmsConfiguracionEnvioDetalle();
                entidad = Mapper.Map<SmsConfiguracionEnvioDetalleBO, TSmsConfiguracionEnvioDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SmsConfiguracionEnvioDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSmsConfiguracionEnvioDetalle, bool>>> filters, Expression<Func<TSmsConfiguracionEnvioDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSmsConfiguracionEnvioDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SmsConfiguracionEnvioDetalleBO> listadoBO = new List<SmsConfiguracionEnvioDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                SmsConfiguracionEnvioDetalleBO objetoBO = Mapper.Map<TSmsConfiguracionEnvioDetalle, SmsConfiguracionEnvioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

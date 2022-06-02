using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/SmsMensajeRecibido
    /// Autor: Gian Miranda
    /// Fecha: 31/12/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_SmsMensajeRecibido
    /// </summary>
    public class SmsMensajeRecibidoRepositorio : BaseRepository<TSmsMensajeRecibido, SmsMensajeRecibidoBO>
    {
        #region Metodos Base
        public SmsMensajeRecibidoRepositorio() : base()
        {
        }
        public SmsMensajeRecibidoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SmsMensajeRecibidoBO> GetBy(Expression<Func<TSmsMensajeRecibido, bool>> filter)
        {
            IEnumerable<TSmsMensajeRecibido> listado = base.GetBy(filter);
            List<SmsMensajeRecibidoBO> listadoBO = new List<SmsMensajeRecibidoBO>();
            foreach (var itemEntidad in listado)
            {
                SmsMensajeRecibidoBO objetoBO = Mapper.Map<TSmsMensajeRecibido, SmsMensajeRecibidoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SmsMensajeRecibidoBO FirstById(int id)
        {
            try
            {
                TSmsMensajeRecibido entidad = base.FirstById(id);
                SmsMensajeRecibidoBO objetoBO = new SmsMensajeRecibidoBO();
                Mapper.Map<TSmsMensajeRecibido, SmsMensajeRecibidoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SmsMensajeRecibidoBO FirstBy(Expression<Func<TSmsMensajeRecibido, bool>> filter)
        {
            try
            {
                TSmsMensajeRecibido entidad = base.FirstBy(filter);
                SmsMensajeRecibidoBO objetoBO = Mapper.Map<TSmsMensajeRecibido, SmsMensajeRecibidoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SmsMensajeRecibidoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSmsMensajeRecibido entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SmsMensajeRecibidoBO> listadoBO)
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

        public bool Update(SmsMensajeRecibidoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSmsMensajeRecibido entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SmsMensajeRecibidoBO> listadoBO)
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
        private void AsignacionId(TSmsMensajeRecibido entidad, SmsMensajeRecibidoBO objetoBO)
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

        private TSmsMensajeRecibido MapeoEntidad(SmsMensajeRecibidoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSmsMensajeRecibido entidad = new TSmsMensajeRecibido();
                entidad = Mapper.Map<SmsMensajeRecibidoBO, TSmsMensajeRecibido>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SmsMensajeRecibidoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSmsMensajeRecibido, bool>>> filters, Expression<Func<TSmsMensajeRecibido, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSmsMensajeRecibido> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SmsMensajeRecibidoBO> listadoBO = new List<SmsMensajeRecibidoBO>();

            foreach (var itemEntidad in listado)
            {
                SmsMensajeRecibidoBO objetoBO = Mapper.Map<TSmsMensajeRecibido, SmsMensajeRecibidoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs.Scode;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MailChimpListaControlSubidaRepositorio : BaseRepository<TMailChimpListaControlSubida, MailChimpListaControlSubidaBO>
    {
        private int _cantidadMaximaPermitida = 1;

        /// <summary>
        /// Indicara si se supero lo permitido (2 peticiones en paralelo)
        /// </summary>
        public bool SeSuperoNroMaximoPeticionesPermitidosEnParalelo
        {
            get
            {
                return this.GetBy(x => x.EnProceso).Count() >= _cantidadMaximaPermitida;
            }
        }

        #region Metodos Base
        public MailChimpListaControlSubidaRepositorio() : base()
        {
        }
        public MailChimpListaControlSubidaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MailChimpListaControlSubidaBO> GetBy(Expression<Func<TMailChimpListaControlSubida, bool>> filter)
        {
            IEnumerable<TMailChimpListaControlSubida> listado = base.GetBy(filter);
            List<MailChimpListaControlSubidaBO> listadoBO = new List<MailChimpListaControlSubidaBO>();
            foreach (var itemEntidad in listado)
            {
                MailChimpListaControlSubidaBO objetoBO = Mapper.Map<TMailChimpListaControlSubida, MailChimpListaControlSubidaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MailChimpListaControlSubidaBO FirstById(int id)
        {
            try
            {
                TMailChimpListaControlSubida entidad = base.FirstById(id);
                MailChimpListaControlSubidaBO objetoBO = new MailChimpListaControlSubidaBO();
                Mapper.Map<TMailChimpListaControlSubida, MailChimpListaControlSubidaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MailChimpListaControlSubidaBO FirstBy(Expression<Func<TMailChimpListaControlSubida, bool>> filter)
        {
            try
            {
                TMailChimpListaControlSubida entidad = base.FirstBy(filter);
                MailChimpListaControlSubidaBO objetoBO = Mapper.Map<TMailChimpListaControlSubida, MailChimpListaControlSubidaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MailChimpListaControlSubidaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMailChimpListaControlSubida entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MailChimpListaControlSubidaBO> listadoBO)
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

        public bool Update(MailChimpListaControlSubidaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMailChimpListaControlSubida entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MailChimpListaControlSubidaBO> listadoBO)
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
        private void AsignacionId(TMailChimpListaControlSubida entidad, MailChimpListaControlSubidaBO objetoBO)
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

        private TMailChimpListaControlSubida MapeoEntidad(MailChimpListaControlSubidaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMailChimpListaControlSubida entidad = new TMailChimpListaControlSubida();
                entidad = Mapper.Map<MailChimpListaControlSubidaBO, TMailChimpListaControlSubida>(objetoBO,
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

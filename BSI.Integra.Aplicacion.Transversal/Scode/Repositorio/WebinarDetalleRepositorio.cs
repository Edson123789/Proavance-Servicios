using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WebinarDetalleRepositorio : BaseRepository<TWebinarDetalle, WebinarDetalleBO>
    {
        #region Metodos Base
        public WebinarDetalleRepositorio() : base()
        {
        }
        public WebinarDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebinarDetalleBO> GetBy(Expression<Func<TWebinarDetalle, bool>> filter)
        {
            IEnumerable<TWebinarDetalle> listado = base.GetBy(filter);
            List<WebinarDetalleBO> listadoBO = new List<WebinarDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                WebinarDetalleBO objetoBO = Mapper.Map<TWebinarDetalle, WebinarDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebinarDetalleBO FirstById(int id)
        {
            try
            {
                TWebinarDetalle entidad = base.FirstById(id);
                WebinarDetalleBO objetoBO = new WebinarDetalleBO();
                Mapper.Map<TWebinarDetalle, WebinarDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebinarDetalleBO FirstBy(Expression<Func<TWebinarDetalle, bool>> filter)
        {
            try
            {
                TWebinarDetalle entidad = base.FirstBy(filter);
                WebinarDetalleBO objetoBO = Mapper.Map<TWebinarDetalle, WebinarDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebinarDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebinarDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebinarDetalleBO> listadoBO)
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

        public bool Update(WebinarDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebinarDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebinarDetalleBO> listadoBO)
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
        private void AsignacionId(TWebinarDetalle entidad, WebinarDetalleBO objetoBO)
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

        private TWebinarDetalle MapeoEntidad(WebinarDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebinarDetalle entidad = new TWebinarDetalle();
                entidad = Mapper.Map<WebinarDetalleBO, TWebinarDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaWebinarAsistencia != null && objetoBO.ListaWebinarAsistencia.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaWebinarAsistencia)
                    {
                        TWebinarAsistencia entidadHijo = new TWebinarAsistencia();
                        entidadHijo = Mapper.Map<WebinarAsistenciaBO, TWebinarAsistencia>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TWebinarAsistencia.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WebinarDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWebinarDetalle, bool>>> filters, Expression<Func<TWebinarDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWebinarDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WebinarDetalleBO> listadoBO = new List<WebinarDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                WebinarDetalleBO objetoBO = Mapper.Map<TWebinarDetalle, WebinarDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los centro de costos asociados a un webinar
        /// </summary>
        /// <param name="idWebinar"></param>
        /// <returns></returns>
        public List<WebinarDetalleBO> Obtener(int idWebinar)
        {
            try
            {
                return this.GetBy(x => x.Estado && x.IdWebinar == idWebinar).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Indica si el webinar es pasado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EsWebinarPasado(int id) {
            try
            {
                return this.Exist(x => x.Id == id && x.FechaInicio < DateTime.Now);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
